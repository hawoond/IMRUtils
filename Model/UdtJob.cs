using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils.Model
{
    public class UdtJob
    {
        private string mJobName;
        private string mJobDesc;

        /// <summary>
        /// 직업명
        /// </summary>
        public string JobName
        {
            get => this.mJobName;
            set => this.mJobName = value;
        }
        
        /// <summary>
        /// 직업 설명
        /// </summary>
        public string JobDesc
        {
            get => this.mJobDesc;
            set => this.mJobDesc = value;
        }
    }
}
