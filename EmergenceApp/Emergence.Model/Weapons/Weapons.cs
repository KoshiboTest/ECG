using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    [Flags]
    public enum DamageType
    {
        Untyped = 0,
        Piercing = 1,
        Bludgeoning = 2,
        Slashing = 4,
        Ballistic = 8,
        Electricity = 16,
        Fire = 32,
        Holy = 64,
        Unholy = 128,
    }

    public enum WeaponSkill
    {
        Unarmed,
        CloseCombat,
        Dueling,
        Heavy,
        Shortarms,
        Longarms,
        Bows,
        Thrown,
        Launcher,
        Alteration,
        Conjuration,
        Invocation,
        WeaponSystems
    }

    public enum SpecializedSkill
    {
        Athletics,
        Healing,
        PilotLand,
        PilotSea,
        PilotAir,
        Stealth,
        Survival,
        Thievery,
        Deception,
        Intimidation,
        Negotiation,
        Persuasion,
        Artistry,
        Construction,
        Demolitons,
        Electronics,
        Enchanting,
        Husbandry,
        Mechanics,
        Smithing
    }

    public enum KnowledgeSkill
    {
        Arcana,
        Business,
        Geography,
        GovernmentAndLaw,
        MathAndScience,
        Technology
    }

    public enum Range
    {
        Pistol,
        SMG,
        Shotgun,
        Rifle,
        HeavyRifle,
        Bows,
        Thrown,
        TwentyFeet,
        ThirtyFootCone
    }

    public enum NaturalWeaponClass
    {
        Light,
        Medium,
        Heavy,
        Extreme,
        Ranged
    }

    public class Weapon
    {

        public Weapon()
        {
            Mods = new List<WeaponMod>();
        }

        public string Name
        { get; set; }
        public int Accuracy
        { get; set; }
        public int Damage
        { get; set; }
        public int CM
        { get; set; }
        public int Size
        { get; set; }
        public DamageType Type
        { get; set; }
        private int baseCost;
        public int Cost
        {
            get
            {
                int modCost = 0;
                foreach(WeaponMod wm in Mods)
                {
                    modCost += wm.Cost;
                }
                switch (Quality)
                {
                    case WeaponQuality.Poor:
                        return baseCost/2 + modCost;
                    case WeaponQuality.Standard:
                        return baseCost + modCost;
                    case WeaponQuality.High:
                        return baseCost + 1000 + modCost;
                    case WeaponQuality.Master:
                        return baseCost + 5000 + modCost;
                    case WeaponQuality.Exquisite:
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
        public WeaponSkill Skill
        { get; set; }
        public WeaponProperty Properties
        { get; set; }
        public WeaponQuality Quality
        { get; set; }
        public List<WeaponMod> Mods
        { get; set; }

    }

    public class RangedWeapon : Weapon
    {
        public int AmmoCapacity
        { get; set; }
        public Range RangeType
        { get; set; }
    }

    public class NaturalWeapon : Weapon
    {
        private NaturalWeaponClass nwClass;
        public NaturalWeaponClass NWClass
        {
            get
            { return nwClass; }
            set
            {
                nwClass = value;
                switch (nwClass)
                {
                    case NaturalWeaponClass.Light:
                        Accuracy = 1;
                        Damage = 2;
                        Size = 2; //NOTE: Random number chosen here.
                        Skill = WeaponSkill.Unarmed;
                        break;
                    case NaturalWeaponClass.Medium:
                        Accuracy = 0;
                        Damage = 4;
                        Size = 4; //NOTE: Random number chosen here.
                        Skill = WeaponSkill.CloseCombat;
                        break;
                    case NaturalWeaponClass.Heavy:
                        Accuracy = -1;
                        Damage = 6;
                        Size = 6; //NOTE: Random number chosen here.
                        Skill = WeaponSkill.Dueling;
                        break;
                    case NaturalWeaponClass.Extreme:
                        Accuracy = -2;
                        Damage = 8;
                        Size = 8; //NOTE: Random number chosen here.
                        Skill = WeaponSkill.Heavy;
                        break;
                    case NaturalWeaponClass.Ranged:
                        Accuracy = 0;
                        Damage = 4;
                        Size = 3;
                        Skill = WeaponSkill.Unarmed;
                        break;
                    default:
                        throw new Exception("Natural Weapon Class doesn't have accuracy, damage and skill defined.");
                }
                CM = 3;
            }
        }

        public NaturalWeapon()
        {
        }
        public NaturalWeapon(NaturalWeaponClass c)
        {
            NWClass = c;
        }
    }
}
