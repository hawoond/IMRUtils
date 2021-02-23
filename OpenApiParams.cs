using System;

namespace IMRUtils
{
    public static class OpenApiParams
    {
        /// <summary>
        /// 직업 정보 API 파라미터
        /// </summary>
        public static class JOB
        {
            private static string URL = "http://www.career.go.kr/cnet/openapi/getOpenApi";
            private static string API_KEY = "8cbca624a2ef72482e89f4dc16f664df";
            private static string SVC_TYPE = "api";
            private static string SVC_CODE = "JOB";
            private static string CONTENT_TYPE = "xml";
            private static string RETURN_TYPE = "job_dic_list";
            private static string PER_PAGE = "1000";

            public static string GetQuery()
            {
                return URL + "?apiKey=" + API_KEY + "&svcType=" + SVC_TYPE + "&svcCode=" + SVC_CODE + "&contentType=" + CONTENT_TYPE + "&gubun=" + RETURN_TYPE + "&perPage=" + PER_PAGE;
            }
        }

        /// <summary>
        /// 미세먼지 API 파라미터
        /// </summary>
        public static class FINE_DUST
        {
            private static string URL = "http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getMinuDustFrcstDspth";
            private static string API_KEY = "M5JQI7c0SoBd357mnqMA2Nl48thAZrqFmhhU46jv0O1z8l0OOSndOsI8rNxCK0KTSf0GETxVQ25iXvawwSSIHw%3D%3D";
            private static string SEARCH_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            private static string INFORM_CODE = "PM10";
            private static string PAGE_NO = "1";
            private static string NUM_OF_ROWS = "10";


            public static string GetQuery()
            {
                return URL + "?serviceKey=" + API_KEY + "&numOfRows=" + NUM_OF_ROWS + "&pageNo=" + PAGE_NO + "&searchDate=" + SEARCH_DATE + "&InformCode=" + INFORM_CODE;
            }
        }

        /// <summary>
        /// 초미세먼지 API 파라미터
        /// </summary>
        public static class ULTRA_FINE_PARTICLE
        {
            private static string URL = "http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getMinuDustFrcstDspth";
            private static string API_KEY = "M5JQI7c0SoBd357mnqMA2Nl48thAZrqFmhhU46jv0O1z8l0OOSndOsI8rNxCK0KTSf0GETxVQ25iXvawwSSIHw%3D%3D";
            private static string SEARCH_DATE = DateTime.Now.ToString("yyyy-MM-dd");
            private static string INFORM_CODE = "PM25";
            private static string PAGE_NO = "1";
            private static string NUM_OF_ROWS = "10";


            public static string GetQuery()
            {
                return URL + "?serviceKey=" + API_KEY + "&numOfRows=" + NUM_OF_ROWS + "&pageNo=" + PAGE_NO + "&searchDate=" + SEARCH_DATE + "&InformCode=" + INFORM_CODE;
            }
        }

        /// <summary>
        /// 날씨 API 파라미터
        /// </summary>
        public static class WEATHER
        {
            private static string URL = "http://www.kma.go.kr/wid/queryDFSRSS.jsp";
            private static string ZONE = "3611051500";

            public static string GetQuery()
            {
                return URL + "?zone=" + ZONE;
            }
        }

        /// <summary>
        /// 책 검색 API 파라미터
        /// </summary>
        public static class BOOK
        {
            private static string URL = "https://dapi.kakao.com/v3/search/book";
            private static string TARGET = "title";
            private static string API_KEY = "ed424f8feffb420e90aeb8332c0652ba";
            private static string HEADER = "KakaoAK " + API_KEY;
            public static string BOOK_NAME = "";

            public static string GetQuery(string sBookName)
            {
                BOOK_NAME = sBookName;
                return URL + "?target=" + TARGET + "&query=" + BOOK_NAME;
            }

            public static string GetHeader()
            {
                return HEADER;
            }
        }

        ///// <summary>
        ///// 날씨 예보 API 파라미터
        ///// </summary>
        //public static class WEATHER
        //{
        //    private static string URL = "https://dapi.kakao.com/v3/search/book?target=title";
        //    private static string TARGET = "title";
        //    private static string API_KEY = "ed424f8feffb420e90aeb8332c0652ba";
        //    private static string HEADER = "KakaoAK" + API_KEY;
        //    public static string BOOK_NAME = "";

        //    public static string GetQuery(string sBookName)
        //    {
        //        BOOK_NAME = sBookName;
        //        return URL + "?target=" + TARGET + "&query=" + BOOK_NAME;
        //    }

        //    public static string GetHeader()
        //    {
        //        return HEADER;
        //    }
        //}
    }
}
