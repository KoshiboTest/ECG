using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class Armor
    {
        public string Name;
        public string Class;
        public string Type;
        public int ArmorValue;
        public int ArmorPenalty;
        public int SpeedPenalty;
        private int baseCost;
        public int Cost
        {
            get
            {
                int modCost = 0;
                foreach (ArmorMod wm in Mods)
                {
                    modCost += wm.Cost;
                }
                switch (Quality)
                {
                    case ItemQuality.Poor:
                        return baseCost / 2 + modCost;
                    case ItemQuality.Standard:
                        return baseCost + modCost;
                    case ItemQuality.High:
                        return baseCost + 1000 + modCost;
                    case ItemQuality.Master:
                        return baseCost + 5000 + modCost;
                    case ItemQuality.Exquisite:
                        return baseCost + 25000 + modCost;
                    default:
                        return baseCost;
                }
            }
            set
            {
                baseCost = value;
            }
        }
        public ArmorProperty Properties;
        public List<ArmorMod> Mods;
        public ItemQuality Quality;

        public Armor()
        {
            Name = "None";
            Mods = new List<ArmorMod>();
        }
    }

    public class NaturalArmor : Armor
    {
        private NaturalArmorClass naClass;
        public NaturalArmorClass NAClass
        {
            get
            {
                return naClass;
            }
            set
            {
                naClass = value;
                switch (naClass)
                {
                    case NaturalArmorClass.None:
                        ArmorValue = 0;
                        ArmorPenalty = 0;
                        SpeedPenalty = 0;
                        break;
                    case NaturalArmorClass.Light:
                        ArmorValue = 3;
                        ArmorPenalty = 1;
                        SpeedPenalty = 0;
                        break;
                    case NaturalArmorClass.Medium:
                        ArmorValue = 5;
                        ArmorPenalty = 2;
                        SpeedPenalty = 1;
                        break;
                    case NaturalArmorClass.Heavy:
                        ArmorValue = 7;
                        ArmorPenalty = 3;
                        SpeedPenalty = 1;
                        break;
                    case NaturalArmorClass.Extreme:
                        ArmorValue = 9;
                        ArmorPenalty = 3;
                        SpeedPenalty = 2;
                        break;
                    default:
                        throw new Exception("Natural Armor Class has undefined values.");
                }
            }
        }

        public NaturalArmor()
        {
        }

        public NaturalArmor(NaturalArmorClass c)
        {
            NAClass = c;
        }
    }
}
