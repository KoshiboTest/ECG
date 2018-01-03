﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model.Vehicles
{
    public class VehicleMod
    {
        public string Name
        { get; set; }
        public int Cost
        { get; set; }
        public string Effect
        { get; set; }
        public string ApplyError
        { get; protected set; }

        public virtual bool ApplyTo(Vehicle w)
        {
            //ApplyError = string.Empty;
            //if ((w.Properties & WeaponProperty.NoMods) == WeaponProperty.NoMods)
            //{
            //    ApplyError = "A weapon with the no mods attribute cannot have mods applied.";
            //    return false;
            //}
            //if (w.Quality == WeaponQuality.Poor)
            //{
            //    ApplyError = "Poor quality weapons cannot have mods applied.";
            //    return false;
            //}
            //else if (w.Mods.Count >= (int)w.Quality)
            //{
            //    ApplyError = "The weapon has the maximum number of mods.  Raise the quality prior to adding this mod.";
            //    return false;
            //}
            //else if (w.Mods.Contains(this) && this.Name != "Additional Fire Mode" && this.Name != "Heavy Bore")
            //{
            //    ApplyError = "The weapon already has this modification.  It cannot be added again.";
            //    return false;
            //}
            //else if (this.Name == "Heavy Bore" || this.Name == "Multi-Shot" && w.Mods.Count(m => m.Name == this.Name) > 1)
            //{
            //    ApplyError = "The weapon cannot have this mod applied more than twice.";
            //    return false;
            //}
            //w.Mods.Add(this);
            //w.NotifyPropertyChanged("Cost");
            //w.NotifyPropertyChanged("Mods");
            return true;
        }
    }
}
