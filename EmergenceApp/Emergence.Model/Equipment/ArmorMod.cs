using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class ArmorMod
    {
        public string Name
        { get; set; }
        public int Cost
        { get; set; }
        public string Effect
        { get; set; }
        public string ApplyError
        { get; set; }

        public virtual bool ApplyTo(Armor a)
        {
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
            return true;
        }

        public virtual void RemoveFrom(Armor w)
        {
            if (w.Mods.Contains(this))
            {
                w.Mods.Remove(this);
            }
        }
    }
}
