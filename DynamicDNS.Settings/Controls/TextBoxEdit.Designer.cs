namespace DynamicDNS.Settings.Controls {
    partial class TextBoxEdit {
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.textEdit = new System.Windows.Forms.TextBox();
            this.placeholder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textEdit
            // 
            this.textEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textEdit.Location = new System.Drawing.Point(6, 10);
            this.textEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textEdit.Name = "textEdit";
            this.textEdit.Size = new System.Drawing.Size(238, 14);
            this.textEdit.TabIndex = 1;
            this.textEdit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit_KeyUp);
            // 
            // placeholder
            // 
            this.placeholder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.placeholder.AutoSize = true;
            this.placeholder.BackColor = System.Drawing.Color.Transparent;
            this.placeholder.ForeColor = System.Drawing.Color.Gray;
            this.placeholder.Location = new System.Drawing.Point(8, 10);
            this.placeholder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.placeholder.Name = "placeholder";
            this.placeholder.Size = new System.Drawing.Size(0, 12);
            this.placeholder.TabIndex = 2;
            this.placeholder.Click += new System.EventHandler(this.placeholder_Click);
            // 
            // TextBoxEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.placeholder);
            this.Controls.Add(this.textEdit);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TextBoxEdit";
            this.Size = new System.Drawing.Size(250, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textEdit;
        private System.Windows.Forms.Label placeholder;
    }
}
