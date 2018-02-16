using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model.Weapons
{
    public class AmpMod
    {
        public string Name
        { get; set; }
        public int Cost
        { get; set; }
        public string Effect
        { get; set; }
        public string ApplyError
        { get; set; }

        public virtual bool ApplyTo(Amp a)
        {
            ApplyError = string.Empty;
            if (a.Quality == WeaponQuality.Poor)
            {
                ApplyError = "Poor quality weapons cannot have mods applied.";
                return false;
            }
            else if (a.Mods.Count >= (int)a.Quality)
            {
                ApplyError = "The weapon has the maximum number of mods.  Raise the quality prior to adding this mod.";
                return false;
            }
            else if (a.Mods.Contains(this))
            {
                ApplyError = "The weapon already has this modification.  It cannot be added again.";
                return false;
            }
            a.Mods.Add(this);
            return true;
        }
        public virtual void RemoveFrom(Amp a)
        {
            if (a.Mods.Contains(this))
            {
                a.Mods.Remove(this);
            }
        }
    }
}
