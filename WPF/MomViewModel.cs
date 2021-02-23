using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRUtils.WPF
{
    public class MomViewModel : INotifyPropertyChanged
    {
        #region OnPropertyChanged 구현
        /// <summary>
        /// PropertyChanged 이벤트 인터페이스
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
