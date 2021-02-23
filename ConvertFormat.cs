using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils
{
    public class ConvertFormat
    {
        /// <summary>
        /// 인코딩 타입
        /// </summary>
        public class EncodingType
        {
            /// <summary>
            /// UTF-8
            /// </summary>
            public const int UTF8 = 0;
            /// <summary>
            /// 아스키 코드
            /// </summary>
            public const int ASCII = 1;
            /// <summary>
            /// 유니코드
            /// </summary>
            public const int UNICODE = 2;
            /// <summary>
            /// 기본
            /// </summary>
            public const int DEFAULT = 3;
        }

        public string ValidFormatString(string sString)
        {
            if (null == sString)
            {
                return "";
            }

            return sString;
        }

        /// <summary>
        /// 문자열을 byte[]로 변환(UTF-8)
        /// </summary>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public byte[] StringToByte(string sString)
        {
            byte[] arrResult;

            try
            {
                if (null == sString)
                {
                    throw null;
                }

                arrResult = Encoding.UTF8.GetBytes(sString);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return arrResult;
        }

        /// <summary>
        /// 문자열을 byte[]로 변환(인코딩 선택)
        /// </summary>
        /// <param name="nEncodingType">인코딩 타입</param>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public byte[] StringToByte(int nEncodingType,string sString)
        {
            byte[] arrResult;
            try
            {
                if (null == sString)
                {
                    throw null;
                }

                if (EncodingType.UTF8 == nEncodingType)
                {
                    arrResult = Encoding.UTF8.GetBytes(sString);
                }
                else if (EncodingType.ASCII == nEncodingType)
                {
                    arrResult = Encoding.ASCII.GetBytes(sString);
                }
                else if (EncodingType.UNICODE == nEncodingType)
                {
                    arrResult = Encoding.Unicode.GetBytes(sString);
                }
                else
                {
                    arrResult = Encoding.UTF8.GetBytes(sString);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return arrResult;
        }

        /// <summary>
        /// byte[]를 문자열로 변환(UTF-8)
        /// </summary>
        /// <param name="nEncodingType">인코딩 타입</param>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public string ByteToString(byte[] arrString)
        {
            string SResult;
            try
            {
                if (null == arrString)
                {
                    throw null;
                }

                SResult = Encoding.UTF8.GetString(arrString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SResult;
        }

        /// <summary>
        /// byte[]를 문자열로 변환(인코딩 선택)
        /// </summary>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public string ByteToString(int nEncodingType, byte[] arrString)
        {
            string sResult;
            try
            {
                if (null == arrString)
                {
                    throw null;
                }

                if (EncodingType.UTF8 == nEncodingType)
                {
                    sResult = Encoding.UTF8.GetString(arrString);
                }
                else if (EncodingType.ASCII == nEncodingType)
                {
                    sResult = Encoding.ASCII.GetString(arrString);
                }
                else if (EncodingType.UNICODE == nEncodingType)
                {
                    sResult = Encoding.Unicode.GetString(arrString);
                }
                else
                {
                    sResult = Encoding.UTF8.GetString(arrString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        /// <summary>
        /// 문자열을 Base64로 인코딩(UTF-8)
        /// </summary>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public string StringToBase64(string sString)
        {
            string sResult;
            sString = ValidFormatString(sString);
            try
            {
                if (null == sString)
                {
                    throw null;
                }

                sResult = Convert.ToBase64String(StringToByte(sString));
            }
            catch(Exception ex)
            {
                throw ex;
            }
             
            return sResult;
        }

        /// <summary>
        /// 문자열을 Base64로 인코딩(인코딩 선택)
        /// </summary>
        /// <param name="nEncodingType">인코딩 타입</param>
        /// <param name="sString">변환 할 문자열</param>
        /// <returns></returns>
        public string StringToBase64(int nEncodingType, string sString)
        {
            string sResult;

            try
            {
                if (null == sString)
                {
                    throw null;
                }
              
                sResult = Convert.ToBase64String(StringToByte(nEncodingType, sString));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        /// <summary>
        /// Base64를 문자열로 디코딩(UTF-8 선택)
        /// </summary>
        /// <param name="sString">변환 할 Base64 문자열</param>
        /// <returns></returns>
        public string Base64ToString(string sString)
        {
            string sResult;

            try
            {
                if (null == sString)
                {
                    throw null;
                }

                sResult = ByteToString(Convert.FromBase64String(sString));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        /// <summary>
        /// Base64를 문자열로 디코딩(인코딩 선택)
        /// </summary>
        /// <param name="nEncodingType">인코딩 타입</param>
        /// <param name="sString">변환 할 Base64 문자열</param>
        /// <returns></returns>
        public string Base64ToString(int nEncodingType, string sString)
        {
            string sResult;

            try
            {
                if (null == sString)
                {
                    throw null; 
                }

                sResult = ByteToString(nEncodingType, Convert.FromBase64String(sString));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sResult;
        }

        
    }


}
