using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils
{
    class FTPConnecter
    {
        /// <summary>
        /// FTP 파일 업로드
        /// </summary>
        /// <param name="uUploadUri"></param>
        /// <param name="sPort"></param>
        /// <param name="sUserID"></param>
        /// <param name="sUserPassword"></param>
        /// <param name="sFilename"></param>
        /// <param name="stFileStream"></param>
        /// <returns></returns>
        public bool FileUpload(Uri uUploadUri, string sPort, string sUserID, string sUserPassword, string sFilename, Stream stFileStream)
        {
            try
            {
                if (stFileStream.Position > 0)
                {
                    if (sFilename != "" && sFilename != null)
                    {
                        var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uUploadUri + ":" + sPort + "/" + sFilename));
                        reqFTP.UsePassive = true;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(sUserID, sUserPassword);

                        reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                        int bufferLength = 2048;
                        byte[] buffer = new byte[bufferLength];

                        Stream uploadStream = reqFTP.GetRequestStream();
                        int contentLength = stFileStream.Read(buffer, 0, bufferLength);

                        while (contentLength != 0)
                        {
                            uploadStream.Write(buffer, 0, bufferLength);
                            contentLength = stFileStream.Read(buffer, 0, bufferLength);
                        }
                        //성공로그자리
                    }
                }
                else
                {
                    //로구ㅡ자리statusMessage = "File is not uploaded..";
                    return false;
                }
            }
            catch (System.IO.IOException ex)
            {
                //실패로그자리 statusMessage = ex.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// FTP 파일 다운로드
        /// </summary>
        /// <param name="uDownloadUri"></param>
        /// <param name="sPort"></param>
        /// <param name="sUserID"></param>
        /// <param name="sUserPassword"></param>
        /// <param name="sFilename"></param>
        /// <returns></returns>
        public Stream FileDownload(Uri uDownloadUri, string sPort, string sUserID, string sUserPassword, string sFilename)
        {
            Stream streamResult = null;

            try
            {
                var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uDownloadUri + ":" + sPort + "/" + sFilename));
                reqFTP.UsePassive = true;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(sUserID, sUserPassword);

                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                // FTP Request 결과를 가져온다.
                using (FtpWebResponse resp = (FtpWebResponse)reqFTP.GetResponse())
                {
                    // FTP 결과 스트림
                    streamResult = resp.GetResponseStream();
                }
            }
            catch (System.IO.IOException ex)
            {
                //실패로그자리 statusMessage = ex.Message;
                return null;
            }

            return streamResult;
        }
    }
}
