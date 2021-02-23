using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils
{
    public class CreateParameter
    {
        JObject jObject;
        public CreateParameter()
        {
            Init();
        }

        /// <summary>
        /// 초기화 함수
        /// </summary>
        private void Init()
        {
            jObject = new JObject();
        }

        /// <summary>
        /// 서비스 호출 파라미터 생성 함수(JSON)
        /// </summary>
        /// <param name="udtServiceInfo">서비스 호출 통합 객체</param>
        public string CreateParams(UdtServiceInfo udtServiceInfo)
        {
            try
            {
                if (null == jObject)
                {
                    Init();
                }

                jObject = JObject.FromObject(udtServiceInfo);

                return jObject.ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
