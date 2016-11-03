using DNSPod.Api;
using DNSPod.Api.Content;
using DynamicDNS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace DynamicDNS.Settings {
    public partial class frmMain : Form {

        #region 属性
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
            set { AppHelper.WriteSetting("Email", string.IsNullOrWhiteSpace(value) ? value : CryptHelper.AESEncrypt(value)); }
        }

        private string Password {
            get {
                var password = AppHelper.GetSetting("Password");

                if (!string.IsNullOrWhiteSpace(password))
                    password = CryptHelper.AESDecrypt(password);

                return password;
            }
            set { AppHelper.WriteSetting("Password", string.IsNullOrWhiteSpace(value) ? value : CryptHelper.AESEncrypt(value)); }
        }

        private string Token {
            get {
                var token = AppHelper.GetSetting("Token");

                if (!string.IsNullOrWhiteSpace(token))
                    token = CryptHelper.AESDecrypt(token);

                return token;
            }
            set { AppHelper.WriteSetting("Token", string.IsNullOrWhiteSpace(value) ? value : CryptHelper.AESEncrypt(value)); }
        }

        private string TokenID {
            get { return AppHelper.GetSetting("TokenID"); }
            set { AppHelper.WriteSetting("TokenID", value); }
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

        public frmMain() {
            InitializeComponent();

            // 不让修改窗口大小
            this.MinimumSize = this.MaximumSize = this.Size;
            serviceManger = new ServiceManager("DynamicDNSService", string.Format("{0}\\DynamicDNS.Service.exe", AppDomain.CurrentDomain.BaseDirectory));

            // 设置按钮状态
            ToggleBtns();

            // 更改验证状态
            ToggleValidateState(!string.IsNullOrWhiteSpace(Token));

            // 初始化控件值
            InitValue();

            // 设置默认焦点
            if (!string.IsNullOrWhiteSpace(Token)) {
                tbeToken.Focus();
            }
            else {
                tbeAccount.Focus();
            }
        }



        /// <summary>
        ///  初始化控件值
        /// </summary>
        private void InitValue() {
            tbeAccount.Text = Email;
            tbePwd.Text = Password;
            tbeToken.Text = Token;
            tbeTokenID.Text = TokenID;
            tbeDomain.Text = Domain;
            tbeSubDomain.Text = SubDomain;
            tbeInterval.Text = UpdateInterval.ToString();

        }

        /// <summary>
        /// 设置验证状态
        /// </summary>
        private void ToggleValidateState(bool useToken) {
            if (useToken) {
                tbeToken.Visible = true;
                tbeTokenID.Visible = true;
                cbUseToken.Checked = true;
                tbeAccount.Visible = false;
                tbePwd.Visible = false;
                cbUseAccount.Checked = false;
            }
            else {
                tbeToken.Visible = false;
                tbeTokenID.Visible = false;
                cbUseToken.Checked = false;
                tbeAccount.Visible = true;
                tbePwd.Visible = true;
                cbUseAccount.Checked = true;
            }
        }

        /// <summary>
        /// 设置按钮的状态
        /// </summary>
        private void ToggleBtns() {
            var isExist = serviceManger.Exist();

            btnInstall.Enabled = !isExist && HasConfig();
            btnUninstall.Enabled = isExist;
            btnBoot.Enabled = false;
            btnBoot.Text = "请稍候...";

            AppHelper.SetTimeout(() => {
                btnBoot.Invoke(new SetText(() => {
                    btnBoot.Enabled = isExist;
                    btnBoot.Text = (serviceManger.GetStatus() == ServiceControllerStatus.Running ? "停止" : "启动");
                }), null);
            }, 1000);
        }

        /// <summary>
        /// 是否有配置
        /// </summary>
        /// <returns></returns>
        private bool HasConfig() {
            return ((!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)) ||
                (!string.IsNullOrWhiteSpace(Token) && !string.IsNullOrWhiteSpace(TokenID)))
                && !string.IsNullOrWhiteSpace(Domain) && !string.IsNullOrWhiteSpace(SubDomain);
        }

        /// <summary>
        /// 点击启动
        /// </summary>
        private void btnBoot_Click(object sender, EventArgs e) {

            try {
                if (btnBoot.Text == "启动") {

                    if (serviceManger.Exist()) {
                        serviceManger.Start();
                    }
                    else {
                        AppHelper.Alert("在您的配置保存之前，我们无法为您启动！");
                    }
                }
                else {
                    serviceManger.Stop();
                }

                ToggleBtns();
            }
            catch (Exception ex) {
                AppHelper.Alert(ex.Message);
            }
        }

        /// <summary>
        /// 点击安装服务
        /// </summary>
        private void btnInstall_Click(object sender, EventArgs e) {
            if (serviceManger.Exist()) {
                AppHelper.Alert("服务已存在！");
                return;
            }

            serviceManger.Install();
            ToggleBtns();
        }

        /// <summary>
        /// 点击卸载服务
        /// </summary>
        private void btnUninstall_Click(object sender, EventArgs e) {
            if (serviceManger.CanStop()) {
                serviceManger.Stop();
            }

            try {
                serviceManger.UnInstall();
            }
            catch { }

            ToggleBtns();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e) {

            if (cbUseToken.Checked) {

                if (string.IsNullOrWhiteSpace(tbeToken.Text)) {
                    AppHelper.Alert("请填写您的Token！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(tbeTokenID.Text)) {
                    AppHelper.Alert("请填写您的TokenID！");
                    return;
                }
            }
            else if (cbUseAccount.Checked) {

                if (string.IsNullOrWhiteSpace(tbeAccount.Text)) {
                    AppHelper.Alert("请填写您的邮箱！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(tbePwd.Text)) {
                    AppHelper.Alert("请填写您的密码！");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(tbeDomain.Text)) {
                AppHelper.Alert("请填写您的域名！");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbeSubDomain.Text)) {
                AppHelper.Alert("请填写您的主机头！");
                return;
            }


            Domain = tbeDomain.Text;
            SubDomain = tbeSubDomain.Text;

            if (cbUseToken.Checked) {
                Token = tbeToken.Text;
                TokenID = tbeTokenID.Text;
                Email = "";
                Password = "";
            }
            else {
                Email = tbeAccount.Text;
                Password = tbePwd.Text;
                Token = "";
                TokenID = "";
            }

            if (!string.IsNullOrWhiteSpace(tbeInterval.Text)) {
                updateInterval = int.Parse(tbeInterval.Text);
                UpdateInterval = updateInterval;
            }

            AppHelper.Alert("保存成功！");
            ToggleBtns();
        }

        /// <summary>
        /// 在验证方式更改后更改视图
        /// </summary>
        private void validateHandler(object sender, EventArgs e) {
            ToggleValidateState(((CheckBox)sender).Name == cbUseToken.Name);
        }
    }
}
