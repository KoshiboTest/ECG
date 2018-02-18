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
    }

    public class NpcWeaponAttack : NpcAttack
    {
        public Weapon Weapon;
    }

    public class NpcAmpAttack : NpcAttack
    {
        public Amp Amp;
        public NpcAmpAttack()
        {
            IsSpell = true;
        }        
    }
}
