using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDNS.Core {
    public static class AppHelper {

        /// <summary>
        /// 警告对话框
        /// </summary>
        /// <param name="msg"></param>
        public static void Alert(string msg) {
            MessageBox.Show(msg, "程序提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Success(string msg) {
            MessageBox.Show(msg, "程序提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 询问对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Confirm(string msg) {
            return MessageBox.Show(msg, "程序提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// 批量设置配置文件AppSetting
        /// </summary>
        /// <param name="ass">键与值</param>
        public static void WriteSetting(params KeyValuePair<string, object>[] parms) {
            WriteSetting(null, parms);
        }
        /// <summary>
        /// 批量设置配置文件AppSetting
        /// </summary>
        /// <param name="configPath">配置文件路径</param>
        /// <param name="ass">键与值</param>
        public static void WriteSetting(string configPath, params KeyValuePair<string, object>[] parms) {
            Configuration config;

            if (string.IsNullOrEmpty(configPath))
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            else
                config = ConfigurationManager.OpenExeConfiguration(configPath);

            foreach (var appStringElement in parms) {
                if (!string.IsNullOrEmpty(appStringElement.Key)) {
                    try {
                        config.AppSettings.Settings[appStringElement.Key].Value = appStringElement.Value.ToString();
                    }
                    catch {
                        config.AppSettings.Settings.Add(appStringElement.Key, appStringElement.Value.ToString());
                    }
                }
            }
            config.Save();
        }
        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void WriteSetting(string key, object value) {
            WriteSetting(new KeyValuePair<string, object>(key, value));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">要获取的项的名称</param>
        /// <param name="configPath">配置文件路径</param>
        public static string GetSetting(string key, string configPath = null) {
            Configuration config;
            if (string.IsNullOrEmpty(configPath))
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            else
                config = ConfigurationManager.OpenExeConfiguration(configPath);

            var setting = config.AppSettings.Settings[key];
            return (setting == null ? null : setting.Value);
        }



        /// <summary>
        /// 关闭指定名字的进程
        /// </summary>
        /// <param name="name"></param>
        public static void KillProcess(string name) {
            var p = Process.GetProcessesByName(name);
            p.ToList().ForEach(t => {
                try {
                    t.Kill();
                }
                catch {
                }
            });
        }

        /// <summary>
        /// 执行应用程序 
        /// </summary>
        /// <param name="appPath">程序路径</param>
        /// <param name="param">参数</param>
        public static void Execute(string appPath, string param = null, bool isSilence = true) {
            var p = new Process();
            p.StartInfo.FileName = appPath;

            if (!string.IsNullOrEmpty(param))
                p.StartInfo.Arguments = param;

            if (isSilence) {
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.RedirectStandardOutput = true;
            }

            p.StartInfo.UseShellExecute = false;

            try {
                p.Start();
            }
            catch {
            }
            finally {
                p.WaitForExit();
            }

        }

        public static void KillAppIfIsRun() {
            ExitIfAppIsRun(Process.GetCurrentProcess().ProcessName);
        }

        public static void ExitIfAppIsRun(string appName) {
            var apps = Process.GetProcesses().Where(t => appName.Equals(t.ProcessName, StringComparison.CurrentCultureIgnoreCase));
            if (apps.Count() > 1) {
                System.Environment.Exit(0);
            }
        }

        public static void SetTimeout(Action execution, int interval) {
            var timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.Enabled = true;
            timer.Elapsed += delegate {
                timer.Stop();
                execution();
            };
            timer.Start();
        }
        /// <summary>
        /// 在指定的时间运行一次
        /// </summary>
        public static void SetTimeout<T>(Action<T> execution, int interval, T data) {
            var timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.Enabled = true;
            timer.Elapsed += delegate {
                timer.Stop();
                execution(data);
            };
            timer.Start();
        }
    }
}
