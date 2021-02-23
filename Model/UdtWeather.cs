using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils.Model
{
    public class UdtWeather
    {
        private string mHour;
        private string mWeatherDateCd;
        private string mTemp;
        private string mPop;
        private string mFDust;
        private string mUfDust;
        private string mSensTemp;
        private string mWeatherState;

        public string WeatherDateCd
        {
            get => mWeatherDateCd;
            set => mWeatherDateCd = value;
        }

        /// <summary>
        /// 날씨 예보 일자 상태 코드
        /// 0 : 오늘, 1 : 내일, 2 : 모레
        /// </summary>
        public string Hour
        {
            get => mHour;
            set => mHour = value;
        }

        /// <summary>
        /// 온도
        /// </summary>
        public string Temp
        {
            get => mTemp;
            set => mTemp = value;
        }

        /// <summary>
        /// 강수확률
        /// </summary>
        public string Pop
        {
            get => mPop;
            set => mPop = value;
        }

        /// <summary>
        /// 미세먼지
        /// </summary>
        public string FDust
        {
            get => mFDust;
            set => mFDust = value;
        }

        /// <summary>
        /// 초미세먼지
        /// </summary>
        public string UfDust
        {
            get => mUfDust;
            set => mUfDust = value;
        }

        /// <summary>
        /// 체감온도
        /// </summary>
        public string SensTemp
        {
            get => mSensTemp;
            set => mSensTemp = value;
        }

        public string WeatherState
        {
            get => mWeatherState;
            set => mWeatherState = value;
        }
    }
}
