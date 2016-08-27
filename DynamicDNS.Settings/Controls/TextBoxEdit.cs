using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDNS.Settings.Controls {

    /// <summary>
    /// 包含提示文本的文本框
    /// </summary>
    public partial class TextBoxEdit : UserControl {
        public TextBoxEdit() {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置提示文本
        /// </summary>
        [Category("外观")]
        [DescriptionAttribute("提示文本")]
        public string Placeholder {
            get { return placeholder.Text; }
            set { placeholder.Text = value; }
        }

        /// <summary>
        /// 设置文本框文本
        /// </summary>
        [Category("外观")]
        [DescriptionAttribute("文本内容")]
        public override string Text {
            get { return textEdit.Text; }
            set { 
                textEdit.Text = value;

                if (!string.IsNullOrWhiteSpace(value))
                    placeholder.Hide();
                else
                    placeholder.Show();
            }
        }

        [Category("外观")]
        [DescriptionAttribute("密码模式")]
        public bool PasswordMode {
            get { return passwordMode; }
            set {
                passwordMode = value;
                textEdit.PasswordChar = value ? '*' : '\0';
            }
        }
        private bool passwordMode = false;

        [Category("外观")]
        [DescriptionAttribute("输入模式")]
        public new ImeMode ImeMode {
            get { return textEdit.ImeMode; }
            set { textEdit.ImeMode = value; }
        }

        private void textEdit_KeyUp(object sender, KeyEventArgs e) {

            if (textEdit.Text.Length > 0)
                placeholder.Hide();
            else
                placeholder.Show();
        }

        private void placeholder_Click(object sender, EventArgs e) {
            textEdit.Focus();
        }
    }
}
