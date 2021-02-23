using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils
{
    public class Utils
    {
        /// <summary>
        /// 로컬 아이피 가져오기
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string localIP = "IP를 찾을 수 없습니다.";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        /// <summary>
        /// 체감온도 계산
        /// </summary>
        /// <param name="T">온도</param>
        /// <param name="V">풍속(km/h)</param>
        /// <returns></returns>
        public double GetWindChillTemperature(double T, double V)
        {
            return 13.12 + 0.6215 * T - 11.37 * Math.Pow(V, 0.16) + 0.3965 * Math.Pow(V, 0.16) * T;
        }

        /// <summary>
        /// 풍속 변환
        /// m/s -> km/s
        /// </summary>
        /// <param name="wind"></param>
        /// <returns></returns>
        public double ConvertKsToMs(double wind)
        {
            return (wind / 1000) / (wind / 3600);
        }

        /// <summary>
        /// 폴더 내의 파일리스트 가져오기
        /// </summary>
        /// <param name="FolderPath"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetFileList(string FolderPath)
        {
            Dictionary<string, string> dicFileList = new Dictionary<string, string>();

            string FolderName = FolderPath;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".xml") == 0 || File.Extension.ToLower().CompareTo(".png") == 0 || File.Extension.ToLower().CompareTo(".mp4") == 0)
                {
                    String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    String FullFileName = File.FullName;

                    dicFileList.Add(FileNameOnly, FullFileName);
                }
                else if (File.Extension.ToLower().CompareTo(".json") == 0 )
                {
                    String FileNameOnly = File.Name.Substring(0, File.Name.Length - 5);
                    String FullFileName = File.FullName;

                    dicFileList.Add(FileNameOnly, FullFileName);
                }
            }
            return dicFileList;
        }
    }
}
