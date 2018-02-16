using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Emergence.Model
{
    public class NpcAttack
    {
        public string Name;
        public bool IsSpell = false;
        public Weapon Weapon;
    }

    public class NpcMeleeAttack : NpcAttack
    {
    }

    public class NpcRangedAttack : NpcAttack
    {
        public Range RangeType;
    }

    public class NpcMeleeAreaAttack : NpcAttack
    {
        public int RadiusInFeet;
        public AreaShape Shape;
    }

    public class NpcRangedAreaAttack : NpcAttack
    {
        public Range RangeType;
        public int RadiusInFeet;
        public AreaShape Shape;
    }
}
