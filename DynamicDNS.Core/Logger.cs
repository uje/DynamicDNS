using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDNS.Core {
    public static class Logger {

        private static string logPath;
        private static object logState;
        private static string logFormat = "[{0}] {1}";
        static Logger() {
            logState = new object();
            logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        }

        public static string GetLogPath() {
            return logPath;
        }

        public static string GetLogName() {
            return string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 可用变量{0}执行时间，{1}消息 
        /// </summary>
        /// <param name="format"></param>
        public static void SetLogFormat(string format) {
            logFormat = format;
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Write(string msg, params object[] parms) {

            msg = string.Format(msg, parms);
            var time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            var fullName = Path.Combine(logPath, GetLogName());

            Console.WriteLine(logFormat, time, msg);
            if (!Directory.Exists(logPath)) {
                Directory.CreateDirectory(logPath);
            }
            lock (logState) {
                using (StreamWriter sw = new StreamWriter(fullName, true)) {
                    sw.WriteLine(logFormat, time, msg);
                }
            }
        }


        public static string Get(DateTime? date) {
            if (date.HasValue) {
                return Get(string.Format("{0}.txt", date.Value.ToString("yyyy-MM-dd")));
            }
            throw new Exception("missing argument:date");
        }

        public static string Get(string logName) {

            var fullName = Path.Combine(logPath, logName);

            if (!string.IsNullOrEmpty(logName) && File.Exists(fullName)) {
                lock (logState) {
                    return File.ReadAllText(fullName);
                }
            }
            return "";
        }

    }
}
