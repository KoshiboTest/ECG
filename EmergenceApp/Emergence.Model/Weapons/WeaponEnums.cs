using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    [Flags]
    public enum WeaponProperty : long
    {
        Agile = 1,
        ArmorPiercing = 2,
        Attached = 4,
        Blasting = 8,
        Brutal = 16,
        BurstFire = 32,
        Cone = 64,
        Confounding = 128,
        Consistent3 = 256,
        CritModLess1 = 512,
        CritModPlus1 = 1024,
        CritModPlus2 = 2048,
        CritModPlus3 = 4096,
        CritKnockback = 8192,
        Defensive = 16384,
        Defensive2 = 32768,
        Entangling = 65536,
        FullAuto15 = 131072,
        Hafted = 262144,
        Hindering = 524288,
        Lethal1 = 1048576,
        Limited0 = 2097152,
        Limited4 = 4194304,
        LowImpact = 8388608,
        ManualLoad = 16777216,
        NoMods = 33554432,
        NonLethal = 67108864,
        OneHanded = 134217728,
        OrdinanceFiring = 268435456,
        Penetrating = 536870912,
        QuickDraw = 1073741824,
        Range20 = 2147483648,
        RangeShotgun = 4294967296,
        RangeRifle = 8589934592,
        RangeHeavyRifle = 17179869184,
        Reach = 34359738368,
        Reusable = 68719476736,
        SemiAuto3 = 137438953472,
        SemiAuto4 = 274877906944,
        SemiAuto5 = 549755813888,
        ShieldingL2 = 1099511627776,
        ShieldingL4 = 2199023255552,
        ShieldingH4 = 4398046511104,
        Strapped = 8796093022208,
        Thrown = 17592186044416,
        Thundering = 35184372088832,
        TwoHanded = 70368744177664,
        Unarmed = 140737488355328,
        VariableDamage = 281474976710656,
        Vicious1 = 562949953421312,
        Vicious2 = 1125899906842624,
        Vicious3 = 2251799813685248,
        Consistent4 = 4503599627370496,
        Consistent6 = 9007199254740992
    }

    public enum WeaponQuality
    {
        Poor = 0,
        Standard,
        High,
        Master,
        Exquisite
    }

    [Flags]
    public enum AmpProperty
    { 
        Infusing = 1,
        Battering = 2,
        Dueling = 4,
        Compulsive = 8,
        Reaching = 16,
        Destructive = 32
    }
}
