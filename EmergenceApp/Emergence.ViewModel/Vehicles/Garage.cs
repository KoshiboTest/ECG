using Emergence.Model.Vehicles;
using MvvmFoundation.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class Garage : INotifyPropertyChanged
    {
        private ObservableCollection<Vehicle> baseVehicles;
        public ObservableCollection<Vehicle> BaseVehicles
        {
            get
            {
                return baseVehicles;
            }
            private set
            {
                baseVehicles = value;
                NotifyPropertyChanged("BaseVehicles");
            }
        }

        public ObservableCollection<VehicleMod> vehicleMods;
        public ObservableCollection<VehicleMod> VehicleMods
        {
            get
            {
                return vehicleMods;
            }
            private set
            {
                vehicleMods = value;
                NotifyPropertyChanged("VehicleMods");
            }
        }

        private ObservableCollection<Vehicle> yourVehicles;
        public ObservableCollection<Vehicle> YourVehicles
        {
            get
            {
                return yourVehicles;
            }
            private set
            {
                yourVehicles = value;
                NotifyPropertyChanged("YourVehicles");
            }
        }

        public RelayCommand<Vehicle> BuyCommand;

        public void Buy(object w)
        {
            if (w is Vehicle)
            {
                yourVehicles.Add(w as Vehicle);
            }
        }

        public string Upgrade(object w, object m)
        {
            if (w is Vehicle && m is VehicleMod)
            {
                Vehicle ww = w as Vehicle;
                VehicleMod wm = m as VehicleMod;
                if (wm.ApplyTo(ww))
                {
                    return string.Format("Applied {0} mod to {1}.", wm.Name, ww.Name);
                }
                else
                {
                    return wm.ApplyError;
                }
            }
            else
            {
                return "Args are fucked.";
            }
        }

        public Garage()
        {
            InitializeVehicles();
            InitializeVehicleMods();
        }

        private void InitializeVehicles()
        {
            yourVehicles = new ObservableCollection<Vehicle>();
            baseVehicles = new ObservableCollection<Vehicle>();
        }
        private void InitializeVehicleMods()
        {
            VehicleMods = new ObservableCollection<VehicleMod>();

            vehicleMods.OrderBy(wm => wm.Name);
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
