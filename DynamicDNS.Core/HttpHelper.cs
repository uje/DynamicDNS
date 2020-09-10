using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DynamicDNS.Api.Core {
    public static class HttpHelper {

        private static HttpWebRequest CreateRequest(string url, string method = "post", string contentType = "application/json; charset=utf-8") {
            var requrest = (HttpWebRequest)WebRequest.Create(url);
            requrest.Method = method;
            requrest.UserAgent = "curl/7.55.1";
            requrest.Timeout = 60000;
            requrest.ContentType = contentType;
            requrest.ProtocolVersion = HttpVersion.Version11;

            return requrest;
        }

        public static string Fetch(string url, string data, string method) {
            HttpWebRequest request = CreateRequest(url, method);

            // 如果请求方式不为GET且data不为空
            if (data != null && !"GET".Equals(method, StringComparison.OrdinalIgnoreCase)) {
                using (var reqStream = request.GetRequestStream()) {
                    var bytes = Encoding.UTF8.GetBytes(data);
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Close();
                }
            }

            return GetResponseAsString(request.GetResponse());
        }

        public static string Get(string url) {
            return Fetch(url, null, "GET");
        }

        public static string Post(string url, string data) {
            return Fetch(url, data, "POST");
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        public static string Post(string url, Dictionary<string, PostFileInfo> files) {

            HttpWebRequest request = CreateRequest(url, "POST");

            using (MemoryStream postStream = new MemoryStream()) {

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";

                foreach (var file in files) {
                    try {

                        //准备文件流
                        using (var fileStream = new MemoryStream(file.Value.Data)) {
                            var formdata = string.Format(formdataTemplate, file.Key, file.Value.FileName);
                            var formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0) {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                //结尾
                var footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                request.ContentLength = postStream != null ? postStream.Length : 0;


                if (postStream != null) {
                    postStream.Position = 0;

                    //直接写入流
                    Stream requestStream = request.GetRequestStream();

                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0) {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    postStream.Close();//关闭文件访问
                }


                return GetResponseAsString(request.GetResponse());
            }

        }

        public static string GetResponseAsString(WebResponse response) {
            HttpWebResponse rsp = (HttpWebResponse)response;
            Encoding encoding = Encoding.GetEncoding(string.IsNullOrWhiteSpace(rsp.CharacterSet) ? Encoding.UTF8.WebName : rsp.CharacterSet);

            // 以字符流的方式读取HTTP响应
            using (StreamReader reader = new StreamReader(rsp.GetResponseStream(), encoding)) {
                return reader.ReadToEnd();
            }
        }
    }


    /// <summary>
    /// 发送的文件信息
    /// </summary>
    public class PostFileInfo {

        public PostFileInfo() { }

        public PostFileInfo(string fileName, byte[] data) {
            FileName = fileName;
            Data = data;
        }

        /// <summary>
        /// 文件名称
        /// </summary>

        public string FileName { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] Data { get; set; }
    }
}
