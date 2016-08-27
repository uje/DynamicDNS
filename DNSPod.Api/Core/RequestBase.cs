using DynamicDNS.Api.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Core {
    public abstract class RequestBase<T> : IRequest<T> where T : IResponse {

        [Parameter("login_email")]
        public string Email { get; set; }


        [Parameter("login_password")]
        public string Password { get; set; }

        /// <summary>
        /// token验证与帐号密码验证任选其一
        /// </summary>
        [Parameter("login_token")]
        public string Token { get; set; }

        [Parameter("format")]
        public string Format {
            get { return "json"; }
            set { throw new ArgumentException("未提供其它数据格式！"); }
        }

        [Parameter("lang")]
        public string Lang {
            get { return lang; }
            set { lang = value; }
        }
        private string lang = "cn";

        [Parameter("error_on_empty")]
        public string ErrorOnEpmty {
            get { return errorOnEmpty; }
            set { errorOnEmpty = value; }
        }
        private string errorOnEmpty = "yes";

        /// <summary>
        /// 请求的地址
        /// </summary>
        protected abstract string Url { get; }

        protected virtual NameValueCollection BuildData() {
            NameValueCollection parameters = new NameValueCollection();
            var t = this.GetType();
            var pType = typeof(ParameterAttribute);
            var properties = t.GetProperties();

            foreach (var p in properties) {
                var attrs = p.GetCustomAttributes(pType, true);
                foreach (var att in attrs) {

                    if (att is ParameterAttribute) {
                        var attr = att as ParameterAttribute;
                        var name = (attr != null ? attr.Name : p.Name).ToLower();
                        var value = p.GetValue(this, null);

                        if (value != null)
                            parameters.Add(name, p.GetValue(this, null).ToString());
                    }
                }
            }

            return parameters;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns></returns>
        public virtual T Execute() {
            var data = BuildData();

            using (WebClient wc = new WebClient()) {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["proxy"])) {
                    var proxy = ConfigurationManager.AppSettings["proxy"].Split(':');
                    var ip = proxy[0];
                    var port = int.Parse(proxy[1]);
                    wc.Proxy = new WebProxy(ip, port);
                }
                
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var result = Encoding.UTF8.GetString(wc.UploadValues(Url, "POST", data));
                var response = JsonConvert.DeserializeObject<T>(result); 
                if (response.Status.Code != 1) throw new DNSPodException(response.Status.Code, response.Status.Message);

                return response;
            }
        }
    }
}
