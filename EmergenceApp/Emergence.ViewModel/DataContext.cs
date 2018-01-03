using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class DataContext : INotifyPropertyChanged
    {
        private Armory armory;
        public Armory Armory
        { get
            {
                return armory;
            }
            set
            {
                armory = value;
                NotifyPropertyChanged("Armory");
            }
        }

        private Barracks barracks;
        public Barracks Barracks
        {
            get
            {
                return barracks;
            }
            set
            {
                barracks = value;
                NotifyPropertyChanged("Barracks");
            }
        }

        private Lair lair;
        public Lair Lair
        {
            get
            {
                return lair;
            }
                set
            {
                lair = value;
                NotifyPropertyChanged("Lair");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
