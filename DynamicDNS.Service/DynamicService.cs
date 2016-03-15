using DNSPod.Api;
using DNSPod.Api.Content;
using DynamicDNS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Xml;

namespace DynamicDNS.Service {
    public partial class DynamicService : ServiceBase {

        #region 属性
        private static DNSPodClient client = null;
        private int updateInterval = 5;
        private Timer timer;
        private string email;
        private string password;
        private string domain;
        private string subDomain;
        private bool isLock = false;

        #endregion


        public DynamicService() {
            InitializeComponent();

            try {
                var doc = new XmlDocument();
                doc.Load(string.Format("{0}DynamicDNS.exe.config", AppDomain.CurrentDomain.BaseDirectory));
                var appSettings = doc.GetElementsByTagName("add").OfType<XmlNode>().ToDictionary(t => t.Attributes["key"].Value.ToLower(), (t) => t.Attributes["value"].Value);

                if (appSettings.ContainsKey("email"))
                    email = CryptHelper.AESDecrypt(appSettings["email"]);

                if (appSettings.ContainsKey("password"))
                    password = CryptHelper.AESDecrypt(appSettings["password"]);

                if (appSettings.ContainsKey("domain"))
                    domain = CryptHelper.AESDecrypt(appSettings["domain"]);

                if (appSettings.ContainsKey("subdomain"))
                    subDomain = CryptHelper.AESDecrypt(appSettings["subdomain"]);

                if (appSettings.ContainsKey("updateinterval")) {
                    var _updateInterval = appSettings["updateinterval"];
                    int.TryParse(_updateInterval, out updateInterval);
                    updateInterval = Math.Max(updateInterval, 5);
                }
            }
            catch (Exception ex) {
                Logger.Write("配置文件不存在或配置不正确：{0}", ex.Message);
            }

            timer = new Timer();
            timer.Elapsed += timer_Elapsed;
        }

        protected override void OnStart(string[] args) {

            Logger.Write("服务启动！");

            if (string.IsNullOrWhiteSpace(email)) {
                Logger.Write("Missing Email");
                this.Stop();
                return;
            }

            if (string.IsNullOrWhiteSpace(password)) {
                Logger.Write("Missing Password");
                this.Stop();
                return;
            }

            if (string.IsNullOrWhiteSpace(domain)) {
                Logger.Write("Missing Domain");
                this.Stop();
                return;
            }

            if (string.IsNullOrWhiteSpace(subDomain)) {
                Logger.Write("Missing SubDomain");
                this.Stop();
                return;
            }

            client = new DNSPodClient(email, password);
            timer.Interval = updateInterval * 60 * 1000;
            timer.Start();

            AppHelper.SetTimeout(() => {
                DDNS(client, domain, subDomain);
            }, 1000);
        }

        protected override void OnStop() {
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e) {
            DDNS(client, domain, subDomain);
        }

        private bool DDNS(DNSPodClient client, string domainName, string subDomain) {

            if (!isLock) {
                isLock = true;
                Logger.Write("获取本地IP");
                var ip = DNSHelper.GetLocalIP();
                Logger.Write("本地IP为：{0}，IP比对中...", ip);

                try {
                    Domain domain = client.GetDomain(domainName);
                    Record record = null;

                    try {
                        record = client.GetRecord(domain.Id.ToString(), subDomain);
                    }
                    catch (DNSPodException ex) {

                        // 如果记录不存在则创建一个
                        if (ex.Code == 22) {
                            Logger.Write("主机头不存在，创建记录");
                            record = client.CreateRecord(domain.Id.ToString(), subDomain, ip);
                            client.Clear();
                            Logger.Write("已创建记录，ID为：{0}", record.Id);
                        }
                        else
                            throw ex;
                    }

                    // 如果本地IP与服务器不一样则更新
                    if (ip != record.Value) {
                        Logger.Write("IP变动，刷新DNS。IP地址为：{0}", ip);
                        client.DDNS(domain.Id.ToString(), subDomain, record.Id);
                        client.Clear();
                        Logger.Write("已更换IP：{0}", ip);
                    }
                    else {
                        Logger.Write("本地IP与服务器IP一致，无需更新");
                    }


                    isLock = false;
                    return true;
                }
                catch (DNSPodException ex) {
                    Logger.Write("出错：{0}", ex.Message);
                    isLock = false;
                    return false;
                }
            }

            return true;
        }
    }
}
