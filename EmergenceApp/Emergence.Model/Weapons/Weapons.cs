using Emergence.Model.Weapons;
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
        Untyped = 0, // All Defs

        Piercing = 1, //Physical (-4 vs fluid)
        Bludgeoning = 2, //Physical (-4 vs soft)
        Slashing = 4, //Physical (-4 vs metal or stone)
        Ballistic = 8, //Physical (-4 vs inanimate without components ex: doors, walls)

        Fire = 16, //Resolve (-4 vs metal or stone) CM -3
        Acid = 32, //Resolve (-4 vs glass or plastic) CM -3
        Psychic = 1024, //Resolve (No dmg to objects) CM -3
        Unholy = 2048, //Resolve (-4 dmg and -1 CM vs unholy) (+2 CM vs holy)
        Holy = 4096, //Resolve (-4 dmg and -1 CM vs holy) (+2 CM vs unholy and undead)

        Cold = 64, //Body (-4 vs non-living objects) CM -3
        Force = 128, //Body (+4 vs solid objects and structures) CM -1
        Electricity = 256, //Body (-4 vs stone) CM -2
        Poison = 512, //Body (No dmg to objects) CM -1
        Necrotic = 8192 //Body (-4 dmg to objects) +2 CM vs living, +2 MCR
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

    [Flags]
    public enum Range
    {
        Melee=1,
        Pistol=2,
        SMG=4,
        Shotgun=8,
        Rifle=16,
        HeavyRifle=32,
        Bows=64,
        Thrown=128,
        TwentyFeet=256,
        ThirtyFootCone=512,
        FiveFootRadius=1024,
        TenFootRadius=2048,
        FifteenFootCone=4096
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
        public Range Range
        { get; set; }
        public int? AmmoCapacity
        { get; set; }
        private int baseCost;
        public int Cost
        {
            get
            {
                int modCost = 0;
                foreach (WeaponMod wm in Mods)
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
        public WeaponSkill Skill
        { get; set; }
        public WeaponProperty Properties
        { get; set; }
        public ItemQuality Quality
        { get; set; }
        public List<WeaponMod> Mods
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

    public class Amp
    {
        public Amp()
        {
            Mods = new List<AmpMod>();
        }

        public string Name
        { get; set; }
        public int Accuracy
        { get; set; }
        public int Damage
        { get; set; }
        public int Charges
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
                foreach (AmpMod wm in Mods)
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
        //public WeaponSkill Skill
        //{ get; set; }
        public AmpProperty Properties
        { get; set; }
        public ItemQuality Quality
        { get; set; }
        public List<AmpMod> Mods
        { get; set; }
        public Range Range
        { get; set; }
    }

}
