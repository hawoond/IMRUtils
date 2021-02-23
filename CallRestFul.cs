using IMRUtils.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace IMRUtils
{
    public class CallRestFul
    {
        public string BOOK_NAME = string.Empty;

        #region API 호출(사용자 노출)

        /// <summary>
        /// 책 정보 가져오기
        /// </summary>
        /// <param name="sBookName">책이름</param>
        /// <returns></returns>
        public List<UdtBook> CallBookApi(string sBookName)
        {
            BOOK_NAME = sBookName;

            return SetBookInfo(CallApi(Constants.OPEN_API.BOOK));
        }

        /// <summary>
        /// 직업 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public List<UdtJob> CallJobApi()
        {
            return SetJobInfo(CallApi(Constants.OPEN_API.JOB));
        }

        /// <summary>
        /// 날씨 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public UdtWeather CallWeatherApi()
        {
            List<string> arrWeatherCallResult = new List<string>();
            arrWeatherCallResult.Add(CallApi(Constants.OPEN_API.WEATHER));
            arrWeatherCallResult.Add(CallApi(Constants.OPEN_API.FINE_DUST));
            arrWeatherCallResult.Add(CallApi(Constants.OPEN_API.ULTRA_FINE_PARTICLE));

            return SetWeatherInfo(arrWeatherCallResult);
        }

        #endregion

        #region API 호출(공통)

        /// <summary>
        /// OpenApi 호출
        /// </summary>
        /// <param name="API_NAME"></param>
        private string CallApi(Constants.OPEN_API API_NAME)
        {
            HttpWebRequest request;
            Stream stream;
            StreamReader reader;
            string sCallUrl = string.Empty;
            string sReadData = string.Empty;
            switch (API_NAME)
            {
                case Constants.OPEN_API.WEATHER:
                {
                    sCallUrl = OpenApiParams.WEATHER.GetQuery();
                    break;
                }
                case Constants.OPEN_API.JOB:
                {
                    sCallUrl = OpenApiParams.JOB.GetQuery();
                    break;
                }
                case Constants.OPEN_API.BOOK:
                {
                    if (BOOK_NAME != null || BOOK_NAME.Length != 0)
                    {
                        sCallUrl = OpenApiParams.BOOK.GetQuery(BOOK_NAME);
                    }
                    else
                    {
                        return "책 이름 없음";
                    }
                    break;
                }
                case Constants.OPEN_API.FINE_DUST:
                {
                    sCallUrl = OpenApiParams.FINE_DUST.GetQuery();
                    break;
                }
                case Constants.OPEN_API.ULTRA_FINE_PARTICLE:
                {
                    sCallUrl = OpenApiParams.ULTRA_FINE_PARTICLE.GetQuery();
                    break;
                }
                default:
                {
                    break;
                }
            }

            request = HttpWebRequest.Create(sCallUrl) as HttpWebRequest;

            if (Constants.OPEN_API.BOOK == API_NAME)
            {
                WebRequest requestBook = WebRequest.Create(sCallUrl);
                requestBook.Headers.Add("Authorization", OpenApiParams.BOOK.GetHeader());
                stream = requestBook.GetResponse().GetResponseStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                sReadData = reader.ReadToEnd();
                stream.Close();
                return sReadData;
            }

            stream = request.GetResponse().GetResponseStream();
            reader = new StreamReader(stream, Encoding.UTF8);
            sReadData = reader.ReadToEnd();
            stream.Close();
            return sReadData;
        }

        #endregion

        #region api 결과 파싱

        /// <summary>
        /// 책 검색 API 결과를 UdtBook에 저장
        /// </summary>
        /// <param name="sApiResultData">API 결과 json</param>
        /// <returns></returns>
        private List<UdtBook> SetBookInfo(string sApiResultData)
        {
            List<UdtBook> arrUdtBook = new List<UdtBook>();
            UdtBook udtBookInfo;

            JavaScriptSerializer js = new JavaScriptSerializer();

            dynamic dob = js.Deserialize<dynamic>(sApiResultData);

            dynamic docs = dob["documents"];

            object[] buf = docs;

            int length = buf.Length;
            StringBuilder sbResult = new StringBuilder();
            //for (int i = 0; i < length; i++)
            //{

            if (length == 0)
            {
                return null;
            }

            for (int i = 0; i < 10; i++)
            {
                udtBookInfo = new UdtBook();
                // 도서 제목
                udtBookInfo.BookName = docs[i]["title"];
                //도서 소개
                udtBookInfo.BookDesc = docs[i]["contents"];
                //저자
                string sAuthors = string.Empty;
                for (int j = 0; j < (docs[i]["authors"] as object[]).Length; j++)
                {
                    sAuthors += (docs[i]["authors"] as object[])[j].ToString() + " ";
                }
                udtBookInfo.BookAuth = sAuthors;

                //썸네일
                udtBookInfo.ImgPath = docs[i]["thumbnail"];

                arrUdtBook.Add(udtBookInfo);
            }

            #region 이미지 변환(URL to Byte)
            //Byte[] buffer;
            //if (sThumbnail.Length > 0)
            //{
            //    if (sThumbnail.Substring(0, 4).Equals("http"))
            //    {
            //        WebClient wc = new WebClient();
            //        buffer = wc.DownloadData(new Uri(sThumbnail, UriKind.Absolute));
            //        wc.Dispose();
            //    }
            //    else
            //    {
            //        buffer = System.IO.File.ReadAllBytes(sThumbnail);
            //    }

            //    MemoryStream ms = new MemoryStream(buffer);
            //    BitmapImage img = new BitmapImage();

            //    img.BeginInit();
            //    img.CacheOption = BitmapCacheOption.OnLoad;
            //    img.StreamSource = ms;
            //    img.EndInit();

            //    imgResult.Source = img;
            //}
            #endregion

            return arrUdtBook;
        }

        /// <summary>
        /// 직업 정보 조회 결과를 UdtJob에 저장
        /// </summary>
        /// <param name="sApiResultData">API 결과 xml</param>
        /// <returns></returns>
        private List<UdtJob> SetJobInfo(string sApiResultData)
        {
            List<UdtJob> arrUdtJobInfo = new List<UdtJob>();
            UdtJob udtJobInfo;

            XElement xmlMain = XElement.Parse(sApiResultData);

            var result = from xe in xmlMain.Elements("content") select xe;

            StringBuilder sbResult = new StringBuilder();


            foreach (var sElemnetResult in result)
            {
                udtJobInfo = new UdtJob();

                udtJobInfo.JobName = sElemnetResult.Element("job").Value;
                udtJobInfo.JobDesc = sElemnetResult.Element("summary").Value;

                arrUdtJobInfo.Add(udtJobInfo);
            }

            return arrUdtJobInfo;
        }

        /// <summary>
        /// 날씨 정보 조회 결과를 UdtJob에 저장
        /// </summary>
        /// <param name="arrWeatherResultData">날씨 예보, 미세먼지, 초미세먼지 순서대로 호출 결과 List</param>
        /// <returns></returns>
        private UdtWeather SetWeatherInfo(List<string> arrWeatherResultData)
        {
            UdtWeather udtWeatherInfo = new UdtWeather();
            Utils utils = new Utils();

            XElement xmlMain;
            XElement xmlBody;

            string sTomorrowDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            #region 날씨 조회
            xmlMain = XElement.Parse(arrWeatherResultData[0]);
            xmlBody = xmlMain.Descendants("body").First();

            var result = from xe in xmlBody.Elements("data") select xe;

            foreach (var sElemnetResult in result)
            {
                if (sElemnetResult.Element("day").Value == "1")
                {
                    if (int.Parse(sElemnetResult.Element("hour").Value) > int.Parse("7"))
                    {
                        udtWeatherInfo.Hour = sElemnetResult.Element("hour").Value;
                        udtWeatherInfo.WeatherDateCd = sElemnetResult.Element("day").Value;
                        udtWeatherInfo.Temp = sElemnetResult.Element("temp").Value;
                        udtWeatherInfo.SensTemp = (Math.Round(utils.GetWindChillTemperature(double.Parse(sElemnetResult.Element("temp").Value), utils.ConvertKsToMs(double.Parse(sElemnetResult.Element("ws").Value))) * 10) / 10).ToString();
                        udtWeatherInfo.Pop = sElemnetResult.Element("pop").Value;

                        // 처음꺼 하나만 조회
                        break;
                    }
                }
            }
            #endregion

            #region 미세먼지 조회

            xmlMain = XElement.Parse(arrWeatherResultData[1]);
            xmlBody = xmlMain.Descendants("items").First();

            var xElemnt = from xe in xmlBody.Elements("item") select xe;

            foreach (var dustResult in xElemnt)
            {
                if (dustResult.Element("informData").Value.Equals(sTomorrowDate))
                {
                    string[] sLocationValue = dustResult.Element("informGrade").Value.Trim().Split(',');

                    foreach (var slocation in sLocationValue)
                    {
                        if (slocation.Contains("세종"))
                        {
                            udtWeatherInfo.FDust = slocation.Trim().Split(':')[1].ToString();
                            break;
                        }
                    }

                    break;
                }
            }

            #endregion

            #region 초미세먼지 조회

            xmlMain = XElement.Parse(arrWeatherResultData[2]);
            xmlBody = xmlMain.Descendants("items").First();

            xElemnt = from xe in xmlBody.Elements("item") select xe;

            foreach (var dustResult in xElemnt)
            {
                if (dustResult.Element("informData").Value.Equals(sTomorrowDate))
                {
                    string[] sLocationValue = dustResult.Element("informGrade").Value.Trim().Split(',');

                    foreach (var slocation in sLocationValue)
                    {
                        if (slocation.Contains("세종"))
                        {
                            udtWeatherInfo.UfDust = slocation.Trim().Split(':')[1].ToString();
                            break;
                        }
                    }

                    break;
                }
            }

            #endregion

            return udtWeatherInfo;
        }

        #endregion
    }
}
