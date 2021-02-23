using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils.Model
{
    public class UdtBook
    {
        private string mBookName;
        private string mBookAuth;
        private string mBookDesc;
        private string mImgPath;

        /// <summary>
        /// 책 이름
        /// </summary>
        public string BookName
        {
            get => mBookName;
            set => mBookName = value;
        }

        /// <summary>
        /// 저자
        /// </summary>
        public string BookAuth
        {
            get => mBookAuth;
            set => mBookAuth = value;
        }

        /// <summary>
        /// 책 소개
        /// </summary>
        public string BookDesc
        {
            get => mBookDesc;
            set => mBookDesc = value;
        }

        /// <summary>
        /// 이미지 URL
        /// </summary>
        public string ImgPath
        {
            get => mImgPath;
            set => mImgPath = value;
        }
    }
}
