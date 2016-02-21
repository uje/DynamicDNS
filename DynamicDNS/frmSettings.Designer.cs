namespace DynamicDNS {
    partial class frmSettings {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.textRefreshTime = new DynamicDNS.Controls.TextBoxEdit();
            this.textSubDomain = new DynamicDNS.Controls.TextBoxEdit();
            this.textDomain = new DynamicDNS.Controls.TextBoxEdit();
            this.textPassword = new DynamicDNS.Controls.TextBoxEdit();
            this.textEmail = new DynamicDNS.Controls.TextBoxEdit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(434, 29);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 63);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRun
            // 
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.Location = new System.Drawing.Point(298, 29);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(142, 63);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInstall.Location = new System.Drawing.Point(16, 29);
            this.btnInstall.Margin = new System.Windows.Forms.Padding(2);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(142, 63);
            this.btnInstall.TabIndex = 8;
            this.btnInstall.Text = "安装服务";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUninstall.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUninstall.Location = new System.Drawing.Point(157, 29);
            this.btnUninstall.Margin = new System.Windows.Forms.Padding(2);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(142, 63);
            this.btnUninstall.TabIndex = 9;
            this.btnUninstall.Text = "卸载服务";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // textRefreshTime
            // 
            this.textRefreshTime.BackColor = System.Drawing.Color.White;
            this.textRefreshTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textRefreshTime.Location = new System.Drawing.Point(16, 325);
            this.textRefreshTime.Margin = new System.Windows.Forms.Padding(1);
            this.textRefreshTime.Name = "textRefreshTime";
            this.textRefreshTime.PasswordMode = false;
            this.textRefreshTime.Placeholder = "刷新时间（分钟）";
            this.textRefreshTime.Size = new System.Drawing.Size(565, 37);
            this.textRefreshTime.TabIndex = 5;
            // 
            // textSubDomain
            // 
            this.textSubDomain.BackColor = System.Drawing.Color.White;
            this.textSubDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSubDomain.Location = new System.Drawing.Point(16, 271);
            this.textSubDomain.Margin = new System.Windows.Forms.Padding(1);
            this.textSubDomain.Name = "textSubDomain";
            this.textSubDomain.PasswordMode = false;
            this.textSubDomain.Placeholder = "主机头";
            this.textSubDomain.Size = new System.Drawing.Size(566, 37);
            this.textSubDomain.TabIndex = 3;
            // 
            // textDomain
            // 
            this.textDomain.BackColor = System.Drawing.Color.White;
            this.textDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textDomain.Location = new System.Drawing.Point(16, 216);
            this.textDomain.Margin = new System.Windows.Forms.Padding(1);
            this.textDomain.Name = "textDomain";
            this.textDomain.PasswordMode = false;
            this.textDomain.Placeholder = "域名";
            this.textDomain.Size = new System.Drawing.Size(566, 37);
            this.textDomain.TabIndex = 2;
            // 
            // textPassword
            // 
            this.textPassword.BackColor = System.Drawing.Color.White;
            this.textPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPassword.Location = new System.Drawing.Point(16, 167);
            this.textPassword.Margin = new System.Windows.Forms.Padding(1);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordMode = true;
            this.textPassword.Placeholder = "您的密码";
            this.textPassword.Size = new System.Drawing.Size(566, 34);
            this.textPassword.TabIndex = 1;
            // 
            // textEmail
            // 
            this.textEmail.BackColor = System.Drawing.Color.White;
            this.textEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textEmail.Location = new System.Drawing.Point(16, 117);
            this.textEmail.Margin = new System.Windows.Forms.Padding(1);
            this.textEmail.Name = "textEmail";
            this.textEmail.PasswordMode = false;
            this.textEmail.Placeholder = "您的邮箱地址";
            this.textEmail.Size = new System.Drawing.Size(566, 34);
            this.textEmail.TabIndex = 0;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 380);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.textRefreshTime);
            this.Controls.Add(this.textSubDomain);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textDomain);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textEmail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(613, 419);
            this.MinimumSize = new System.Drawing.Size(613, 419);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "动态DNS设置";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TextBoxEdit textEmail;
        private Controls.TextBoxEdit textPassword;
        private Controls.TextBoxEdit textDomain;
        private System.Windows.Forms.Button btnSave;
        private Controls.TextBoxEdit textSubDomain;
        private Controls.TextBoxEdit textRefreshTime;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
    }
}

