using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DynamicDNS.Core {

    /// <summary>
    /// 服务管理器
    /// </summary>
    public class ServiceManager {

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string Path { get; set; }

        public ServiceManager() {}



        public ServiceManager(string path) : this(System.IO.Path.GetFileName(path), path) { }

        public ServiceManager(string name, string path) {
            this.Name = name;
            this.Path = path;
        }

        /// <summary>
        /// 服务是否存在
        /// </summary>
        public bool Exist() {
            var services = ServiceController.GetServices();

            foreach (var s in services) {
                if (s.ServiceName == Name) 
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 安装
        /// </summary>
        public void Install() {
            if (!Exist()) {
                var stateSaver =  new Hashtable();
                using (var myAssemblyInstaller = new AssemblyInstaller()) {
                    myAssemblyInstaller.UseNewContext = true;
                    myAssemblyInstaller.Path = Path;
                    myAssemblyInstaller.Install(stateSaver);
                    myAssemblyInstaller.Commit(stateSaver);
                }
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void UnInstall() {
            if (Exist()) {
                var stateSaver = new Hashtable();
                using (var myAssemblyInstaller = new AssemblyInstaller()) {
                    myAssemblyInstaller.UseNewContext = true;
                    myAssemblyInstaller.Path = Path;
                    myAssemblyInstaller.Uninstall(stateSaver);
                    myAssemblyInstaller.Commit(stateSaver);
                }
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start() {

            if(!Exist())
                throw new Exception("服务不存在！");

            using (var sc = new ServiceController(Name)) {
                sc.Start();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop() {
            if (!Exist())
                throw new Exception("服务不存在！");

            using (var sc = new ServiceController(Name)) {

                if(sc.CanStop)
                    sc.Stop();
            }
        }

        public bool CanStop() {
            if (!Exist())
                return false;

            using (var sc = new ServiceController(Name)) {
                return sc.CanStop;
            }
        }

        public ServiceControllerStatus GetStatus() {
            if (!Exist())
                return ServiceControllerStatus.Stopped;

            if (Process.GetProcesses().Count(t => "DynamicDNS.Service".Equals(t.ProcessName, StringComparison.CurrentCultureIgnoreCase)) > 0)
                return ServiceControllerStatus.Running;

            using (var sc = new ServiceController(Name)) {
                return sc.Status;
            }
        }
    }
}
