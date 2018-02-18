using Emergence.Model.Weapons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
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

        public virtual bool ApplyTo(AmpVM a)
        {
            ApplyError = string.Empty;

            if (a.Quality == WeaponQuality.Poor)
            {
                ApplyError = "Poor quality Amps cannot have mods applied.";
                return false;
            }
            else if (a.Mods.Count >= (int)a.Quality)
            {
                ApplyError = "The Amp has the maximum number of mods.  Raise the quality prior to adding this mod.";
                return false;
            }
            else if (a.Mods.Contains(this))
            {
                ApplyError = "The Amp already has this modification.  It cannot be added again.";
                return false;
            }
            a.Mods.Add(this);
            a.NotifyPropertyChanged("Cost");
            a.NotifyPropertyChanged("Mods");
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

    public class StaffModVM : AmpModVM
    {
        public override bool ApplyTo(AmpVM a)
        {
            if (a.Name != "Staff")
            {
                ApplyError = "This mod can only be applied to the staff amp.";
                return false;
            }
            else
            {
                return base.ApplyTo(a);
            }
        }
    }

    public class RequiresPowerRuneAmpModVM : AmpModVM
    {
        public override bool ApplyTo(AmpVM a)
        {
            bool hasRequiredMod = false;
            foreach (AmpModVM m in a.Mods)
            {
                if (m.Name == "Power Rune" || m.Name == "Heavy Power Rune")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Power Rune.";
                return false;
            }
            else
            {
                return base.ApplyTo(a);
            }
        }
    }

    public class RequiresHeavyPowerRuneAmpModVM : AmpModVM
    {
        public override bool ApplyTo(AmpVM a)
        {
            bool hasRequiredMod = false;
            foreach (AmpModVM m in a.Mods)
            {
                if (m.Name == "Heavy Power Rune")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Heavy Power Rune.";
                return false;
            }
            else
            {
                return base.ApplyTo(a);
            }
        }
    }
}
