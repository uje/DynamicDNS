using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Core {

    /// <summary>
    /// 表示这是一个参数, 在将属性转换为参数提交时会转换为Name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute {

        public ParameterAttribute() {

        }

        public ParameterAttribute(string name) {
            this.Name = name;
        }

        /// <summary>
        /// 格式化后的属性名称
        /// </summary>
        public string Name { get; set; }
    }
}
