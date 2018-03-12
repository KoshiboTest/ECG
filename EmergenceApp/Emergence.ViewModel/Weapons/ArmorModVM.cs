using Emergence.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class ArmorModVM : INotifyPropertyChanged
    {
        internal ArmorMod model;

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
            set
            {
                model.ApplyError = value;
                NotifyPropertyChanged("ApplyError");
            }
        }

        public ArmorModVM(ArmorMod mod)
        {
            this.model = mod;
        }

        public ArmorModVM()
        {
            this.model = new ArmorMod();
        }
        
        public virtual bool ApplyTo(ArmorVM a)
        {
            ApplyError = string.Empty;
            if (a.Quality == ItemQuality.Poor)
            {
                ApplyError = "Poor quality armors cannot have mods applied.";
                return false;
            }
            else if (a.Mods.Count >= (int)a.Quality)
            {
                ApplyError = "The armor has the maximum number of mods.  Raise the quality prior to adding this mod.";
                return false;
            }
            else if (a.Mods.Contains(this) && this.Name != "Additional Fire Mode" && this.Name != "Heavy Bore")
            {
                ApplyError = "The armor already has this modification.  It cannot be added again.";
                return false;
            }
            a.Mods.Add(this);
            a.NotifyPropertyChanged("Cost");
            a.NotifyPropertyChanged("Mods");
            return true;
        }
        public virtual void RemoveFrom(ArmorVM a)
        {
            if (a.model.Mods.Contains(this.model))
            {
                a.model.Mods.Remove(this.model);
                a.NotifyPropertyChanged("Cost");
                a.NotifyPropertyChanged("Mods");
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

    public class RequiresPowerRuneArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Power Rune"))
                {
                    hasPower = true;
                    break;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Power Rune";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresMageCellPoweredArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Magecell-Powered"))
                {
                    hasPower = true;
                    break;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Magecell-Powered";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresHeavyPowerRuneArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Heavy Power Rune"))
                {
                    hasPower = true;
                    break;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Heavy Power Rune";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresPoweredServosArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Powered Servos"))
                {
                    hasPower = true;
                    break;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Powered Servos";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class HeavyServosArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Powered Servos"))
                {
                    ApplyError = "Cannot be combined with Powered Servos.";
                    return false;
                }
                if (w.Mods[i].Name.Contains("Powered Servos"))
                {
                    hasPower = true;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Powered Servos";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresHeavyMagecellPoweredArmorModVM : ArmorModVM
    {
        public override bool ApplyTo(ArmorVM w)
        {
            bool hasPower = false;
            for (int i = 0; i < w.Mods.Count; i++)
            {
                if (w.Mods[i].Name.Contains("Heavy Magecell"))
                {
                    hasPower = true;
                }
            }
            if (!hasPower)
            {
                ApplyError = "Requires Heavy Magecell";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }
}
