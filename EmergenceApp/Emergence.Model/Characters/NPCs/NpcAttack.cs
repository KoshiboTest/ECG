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
        public int Attack
        {
            get
            {
                KeyValuePair<WeaponSkill, int> OwnersSkill = AttackOwner.CombatSkills.FirstOrDefault(cs => cs.Key == Weapon.Skill);
                if (OwnersSkill.Value == 0) // NPCs are assumed to have the skill they use with any weapons
                {
                    int skillValue = 0;
                    switch (AttackOwner.NpcClass)
                    {
                        case NpcClass.Foe:
                            skillValue = 4 + (IsPrimary ? 1 : 0) + AttackOwner.Level;
                            break;
                        case NpcClass.Grunt:
                            skillValue = 3 + (IsPrimary ? 1 : 0) + AttackOwner.Level;
                            break;
                        case NpcClass.Antagonist:
                            skillValue = 5 + (IsPrimary ? 1 : 0) + AttackOwner.Level;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("NpcClass", "Attack skill not defined for this npc class.");
                    }
                    OwnersSkill = new KeyValuePair<WeaponSkill, int>(Weapon.Skill, skillValue);
                    AttackOwner.CombatSkills.Add(OwnersSkill);
                }
                return Weapon.Accuracy + OwnersSkill.Value;
            }
        }
        public int Damage
        {
            get
            {
                return Weapon.Damage + (IsPrimary ? AttackOwner.Attributes.PrimaryAttackDamage : AttackOwner.Attributes.SecondaryAttackDamage);
            }
        }
        public int CM
        {
            get
            {
                return Weapon.CM + AttackOwner.Attributes.CM;
            }
        }
        public DamageType DamageType
        {
            get
            {
                return Weapon.Type;
            }
        }
        public bool IsSpell = false;
        public bool IsPrimary = true;
        public Weapon Weapon;
        [XmlIgnore]
        public NonPlayerCharacter AttackOwner;
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
