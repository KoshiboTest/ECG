using Emergence.Model.Weapons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel.Weapons
{
    public class AmpModVM : INotifyPropertyChanged
    {
        internal AmpMod model;

        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public int Cost
        {
            get
            {
                return model.Cost;
            }
            set
            {
                model.Cost = value;
                NotifyPropertyChanged("Cost");
            }
        }
        public string Effect
        {
            get
            {
                return model.Effect;
            }
            set
            {
                model.Effect = value;
                NotifyPropertyChanged("Effect");
            }
        }
        public string ApplyError
        {
            get
            {
                return model.ApplyError;
            }
            protected set
            {
                model.ApplyError = value;
            }
        }

        public AmpModVM(AmpMod mod)
        {
            this.model = mod;
        }

        public AmpModVM()
        {
            this.model = new AmpMod();
        }

        public virtual bool ApplyTo(AmpVM w)
        {
            ApplyError = string.Empty;

            if (w.Quality == WeaponQuality.Poor)
            {
                ApplyError = "Poor quality Amps cannot have mods applied.";
                return false;
            }
            else if (w.Mods.Count >= (int)w.Quality)
            {
                ApplyError = "The Amp has the maximum number of mods.  Raise the quality prior to adding this mod.";
                return false;
            }
            else if (w.Mods.Contains(this))
            {
                ApplyError = "The Amp already has this modification.  It cannot be added again.";
                return false;
            }
            w.Mods.Add(this);
            w.NotifyPropertyChanged("Cost");
            w.NotifyPropertyChanged("Mods");
            return true;
        }
        public virtual void RemoveFrom(AmpVM w)
        {
            if (w.model.Mods.Contains(this.model))
            {
                w.model.Mods.Remove(this.model);
                w.NotifyPropertyChanged("Cost");
                w.NotifyPropertyChanged("Mods");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
