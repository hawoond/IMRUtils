using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMRUtils.OverrideType;

namespace IMRUtils.WPF
{
    public class MomModel : INotifyPropertyChanged
    {
        public MomModel()
        {
            MomModelCollection = new MTObservableCollection<MomModel>();
        }

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

        private MTObservableCollection<MomModel> mMomModelCollection;
        public MTObservableCollection<MomModel> MomModelCollection
        {
            get
            {
                if (null == this.mMomModelCollection)
                {
                    this.mMomModelCollection = new MTObservableCollection<MomModel>();
                }
                return this.mMomModelCollection;
            }
            set
            {
                this.mMomModelCollection = value;
                OnPropertyChanged("MomModelCollection");
            }
        }
    }
}
