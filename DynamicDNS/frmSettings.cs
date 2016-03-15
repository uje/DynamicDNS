using DNSPod.Api;
using DNSPod.Api.Content;
using DNSPod.Api.Request;
using DynamicDNS.Api.Core;
using DynamicDNS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDNS {
    public partial class frmSettings : Form {
        public frmSettings() {
            InitializeComponent();
            serviceManger = new ServiceManager("DynamicDNSService", string.Format("{0}\\DynamicDNS.Service.exe", AppDomain.CurrentDomain.BaseDirectory));
            ToggleBtns();
        }

        #region 属性
        private static DNSPodClient client = null;
        private int updateInterval = 5;
        private ServiceManager serviceManger;
        private delegate void SetText();

        private string Email {
            get {
                var email = AppHelper.GetSetting("Email");

                if (!string.IsNullOrWhiteSpace(email))
                    email = CryptHelper.AESDecrypt(email);

                return email;
            }
            set { AppHelper.WriteSetting("Email", CryptHelper.AESEncrypt(value)); }
        }

        private string Password {
            get {
                var password = AppHelper.GetSetting("Password");

                if (!string.IsNullOrWhiteSpace(password))
                    password = CryptHelper.AESDecrypt(password);

                return password;
            }
            set { AppHelper.WriteSetting("Password", CryptHelper.AESEncrypt(value)); }
        }

        private string Domain {
            get {
                var domain = AppHelper.GetSetting("Domain");

                if (!string.IsNullOrWhiteSpace(domain))
                    domain = CryptHelper.AESDecrypt(domain);

                return domain;
            }
            set { AppHelper.WriteSetting("Domain", CryptHelper.AESEncrypt(value)); }
        }

        private string SubDomain {
            get {
                var subDomain = AppHelper.GetSetting("SubDomain");

                if (!string.IsNullOrWhiteSpace(subDomain))
                    subDomain = CryptHelper.AESDecrypt(subDomain);

                return subDomain;
            }
            set { AppHelper.WriteSetting("SubDomain", CryptHelper.AESEncrypt(value)); }
        }

        private int UpdateInterval {
            get {
                var _value = AppHelper.GetSetting("UpdateInterval");

                if (!string.IsNullOrWhiteSpace(_value))
                    updateInterval = int.Parse(_value);

                return updateInterval;
            }
            set {
                AppHelper.WriteSetting("UpdateInterval", value);
            }
        }

        #endregion

        private void ToggleBtns() {
            var isExist = serviceManger.Exist();

            btnInstall.Enabled = !isExist && HasConfig();
            btnUninstall.Enabled = isExist;
            btnRun.Enabled = false;
            btnRun.Text = "请稍候...";

            AppHelper.SetTimeout(() => {
                btnRun.Invoke(new SetText(() => {
                    btnRun.Enabled = isExist;
                    btnRun.Text = (serviceManger.GetStatus() == ServiceControllerStatus.Running ? "停止" : "启动");
                }), null);
            }, 1000);
        }


        private void Form1_Load(object sender, EventArgs e) {

            if (HasConfig()) {
                textEmail.Text = Email;
                textPassword.Text = Password;
                textDomain.Text = Domain;
                textSubDomain.Text = SubDomain;
                textRefreshTime.Text = UpdateInterval.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {

            if (string.IsNullOrWhiteSpace(textEmail.Text)) {
                AppHelper.Alert("请填写您的邮箱！");
                return;
            }

            if (string.IsNullOrWhiteSpace(textPassword.Text)) {
                AppHelper.Alert("请填写您的密码！");
                return;
            }

            if (string.IsNullOrWhiteSpace(textDomain.Text)) {
                AppHelper.Alert("请填写您的域名！");
                return;
            }

            if (string.IsNullOrWhiteSpace(textSubDomain.Text)) {
                AppHelper.Alert("请填写您的主机头！");
                return;
            }

            Email = textEmail.Text;
            Password = textPassword.Text;
            Domain = textDomain.Text;
            SubDomain = textSubDomain.Text;

            if (!string.IsNullOrWhiteSpace(textRefreshTime.Text)) {
                updateInterval = int.Parse(textRefreshTime.Text);
                UpdateInterval = updateInterval;
            }

            AppHelper.Alert("保存成功！");
            ToggleBtns();
        }

        private bool DDNS(DNSPodClient client, string domainName, string subDomain) {


            try {
                Domain domain = client.GetDomain(domainName);
                Record record = null;

                try {
                    record = client.GetRecord(domain.Id.ToString(), subDomain);
                }
                catch (DNSPodException ex) {

                    // 如果记录不存在则创建一个
                    if (ex.Code == 22) {
                        record = client.CreateRecord(domain.Id.ToString(), subDomain, DNSHelper.GetLocalIP());
                        client.Clear();
                        Logger.Write("主机头不存在，创建记录");
                    }
                    else
                        throw ex;
                }

                // 如果本地IP与服务器不一样则更新
                var ip = DNSHelper.GetLocalIP();
                if (ip != record.Value) {
                    client.DDNS(domain.Id.ToString(), subDomain, record.Id);
                    client.Clear();
                    Logger.Write("IP变动，刷新DNS。IP地址为：{0}", ip);
                }
                else {
                    Logger.Write("本地IP与服务器IP一致，无需更新");
                }

                return true;
            }
            catch (DNSPodException ex) {
                AppHelper.Alert(ex.Message);
                Logger.Write("出错：{0}", ex.Message);
                return false;
            }
        }

        private bool HasConfig() {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(Domain) && !string.IsNullOrWhiteSpace(SubDomain);
        }


        private void tsmExit_Click(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        private void updateTimer_Tick(object sender, EventArgs e) {
            DDNS(client, Domain, SubDomain);
        }

        private void btnRun_Click(object sender, EventArgs e) {

            try {
                if (btnRun.Text == "启动") {

                    if (serviceManger.Exist())
                        serviceManger.Start();

                    else
                        AppHelper.Alert("在您的配置保存之前，我们无法为您启动！");

                }
                else
                    serviceManger.Stop();

                ToggleBtns();
            }
            catch (Exception ex) {
                AppHelper.Alert(ex.Message);
            }


        }

        private void btnInstall_Click(object sender, EventArgs e) {
            if (serviceManger.Exist()) {
                AppHelper.Alert("服务已存在！");
                return;
            }

            serviceManger.Install();
            ToggleBtns();
        }

        private void btnUninstall_Click(object sender, EventArgs e) {
            if (serviceManger.CanStop())
                serviceManger.Stop();

            try {
                serviceManger.UnInstall();
            }
            catch { }

            ToggleBtns();
        }

    }
}
