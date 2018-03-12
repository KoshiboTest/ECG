using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Model;
using System.ComponentModel;

namespace Emergence.ViewModel
{
    public class WeaponModVM : INotifyPropertyChanged
    {
        internal WeaponMod model;

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

        public WeaponModVM(WeaponMod mod)
        {
            this.model = mod;
        }

        public WeaponModVM()
        {
            this.model = new WeaponMod();
        }

        public virtual bool ApplyTo(WeaponVM w)
        {
            ApplyError = string.Empty;
            if ((w.Properties & WeaponProperty.NoMods) == WeaponProperty.NoMods)
            {
                ApplyError = "A weapon with the no mods attribute cannot have mods applied.";
                return false;
            }
            if (w.Quality == ItemQuality.Poor)
            {
                ApplyError = "Poor quality weapons cannot have mods applied.";
                return false;
            }
            else if (w.Mods.Count >= (int)w.Quality)
            {
                ApplyError = "The weapon has the maximum number of mods.  Raise the quality prior to adding this mod.";
                return false;
            }
            else if (w.Mods.Contains(this) && this.Name != "Additional Fire Mode" && this.Name != "Heavy Bore")
            {
                ApplyError = "The weapon already has this modification.  It cannot be added again.";
                return false;
            }
            else if (this.Name == "Heavy Bore" || this.Name == "Multi-Shot" && w.Mods.Count(m => m.Name == this.Name) > 1)
            {
                ApplyError = "The weapon cannot have this mod applied more than twice.";
                return false;
            }
            w.Mods.Add(this);
            w.NotifyPropertyChanged("Cost");
            w.NotifyPropertyChanged("Mods");
            return true;
        }
        public virtual void RemoveFrom(WeaponVM w)
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

    public class MeleeWeaponModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if ((w.Range & Range.Melee) != 0)
            {
                ApplyError = "This mod cannot be applied to a ranged weapon.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class HurlingModVM : MeleeWeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (base.ApplyTo(w))
            {
                w.Properties = w.Properties | WeaponProperty.Thrown;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RemoveFrom(WeaponVM w)
        {
            base.RemoveFrom(w);
            w.Properties = w.Properties ^ WeaponProperty.Thrown;
        }
    }

    public class BalancedModVM : HurlingModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (w.Size > 3)
            {
                ApplyError = "This mod cannot be applied to weapons of size 4 or greater.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class PistolModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (!w.Name.Contains("Pistol"))
            {
                ApplyError = "This mod can only be applied to pistols.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class PBSModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (((w.Type & DamageType.Piercing) != DamageType.Piercing) &&
                ((w.Type & DamageType.Bludgeoning) != DamageType.Bludgeoning) &&
                ((w.Type & DamageType.Slashing) != DamageType.Slashing))
            {
                ApplyError = "This mod can only be applied to Piercing, Budgeoning or Slashing weapons.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class HamVibModVM : PBSModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Magecell-Powered" || m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Magecell-Powered.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class FirearmsModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (w.Skill != WeaponSkill.Shortarms &&
                w.Skill != WeaponSkill.Longarms)
            {
                ApplyError = "This mod can only be applied to firearms.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class CylIModVM : FirearmsModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (!w.Name.Contains("Cannon") && !w.Name.Contains("Shotgun") && !(w.Name.Contains("Rifle") && w.Name.Contains("Heavy")))
            {
                ApplyError = "This mod applies to cylinder and internal reload firearms only.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class LRifleShotgunModVM : FirearmsModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (!w.Name.Contains("Shotgun") && !(w.Name.Contains("Light") && w.Name.Contains("Rifle")))
            {
                ApplyError = "This mod can only be applied to shotguns and light rifles.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresPowerRuneModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
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

    public class RequiresMagecellPoweredVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Magecell-Powered" || m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Magecell-Powered.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresHeavyPowerRuneModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
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
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresHeavyMagecellPoweredModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Heavy Magecell-Powered.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class RequiresPoweredCollapsingVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Powered Collapsing")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Powered Collapsing.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class ThrowingWeaponModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (!((w.Properties & WeaponProperty.Thrown) == WeaponProperty.Thrown))
            {
                ApplyError = "Can only be applied to a thrown weapon.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class CrossbowModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if (!w.Name.Contains("Crossbow"))
            {
                ApplyError = "This mod must be aplied to a crossbow.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class PoweredDrawModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Magecell-Powered" || m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (w.Skill == WeaponSkill.Bows)
            {
                ApplyError = "This mod can only be applied to bows.";
                return false;
            }
            else if (!hasRequiredMod)
            {
                ApplyError = "This mod requires Magecell-Powered.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class BowOrMeleeModVM : WeaponModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            if ((w.Range & Range.Melee) != Range.Melee || (w.Range & Range.Bows) != Range.Bows)
            {
                ApplyError = "This mod can only be applied to melee or bow weapons.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class EnergyFieldModVM : BowOrMeleeModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requries Heavy Magecell-Powered.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }

    public class EnergyFormModVM : BowOrMeleeModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
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
                return base.ApplyTo(w);
            }
        }
    }

    public class HighTechModVM : FirearmsModVM
    {
        public override bool ApplyTo(WeaponVM w)
        {
            bool hasRequiredMod = false;
            foreach (WeaponModVM m in w.Mods)
            {
                if (m.Name == "Plasma" || m.Name == "Particle Beam" || m.Name == "Laser")
                {
                    ApplyError = "This mod cannot be combined with Plasma, Particle Beam or Laser mods.";
                    return false;
                }
                else if (m.Name == "Heavy Magecell-Powered")
                {
                    hasRequiredMod = true;
                }
            }
            if (!hasRequiredMod)
            {
                ApplyError = "This mod requires Heavy Magecell-Powered mod.";
                return false;
            }
            else
            {
                return base.ApplyTo(w);
            }
        }
    }
}
