using Emergence.Model;
using Emergence.Model.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class NonPlayerCharacter
    {
        public string Name;
        public int Level
        {
            get
            {
                return Attributes.Level;
            }
            set
            {
                Attributes.Level = value;
            }
        }
        public int Size
        {
            get
            {
                return Attributes.Size;
            }
            set
            {
                Attributes.Size = value;
            }
        }
        public NpcClass NpcClass
        {
            get
            {
                return Attributes.npcClass;
            }
            set
            {
                Attributes.npcClass = value;
            }
        }
        public NpcType Type;
        public Archetype Archetype;
        public NpcAttributeArray Attributes;
        public List<NpcQuality> Qualities;
        public List<NpcAbility> Abilities;
        public List<KeyValuePair<WeaponSkill, int>> CombatSkills;
        public List<KeyValuePair<SpecializedSkill, int>> SpecializedSkills;
        public List<KeyValuePair<KnowledgeSkill, int>> KnowledgeSkills;
        public List<Talent> Talents;
        public int EffectiveStrength;
        public List<NpcAttack> Attacks;
        public Armor Armor = new Armor();

        public NonPlayerCharacter()
        {
            Name = "Blank Name";
            Attributes = new NpcAttributeArray();
            Qualities = new List<NpcQuality>();
            Abilities = new List<NpcAbility>();
            CombatSkills = new List<KeyValuePair<Model.WeaponSkill, int>>();
            SpecializedSkills = new List<KeyValuePair<SpecializedSkill, int>>();
            KnowledgeSkills = new List<KeyValuePair<KnowledgeSkill, int>>();
            Talents = new List<Talent>();
            Attacks = new List<NpcAttack>();
        }

        public NonPlayerCharacter(Archetype archetype, int level, NpcClass npcClass, int size, NpcType type)
        {
            //Base
            Name = "Default Name";
            this.Archetype = archetype;
            this.Type = type;

            //Attributes
            Attributes = new NpcAttributeArray(level, npcClass, size);

            //Skills
            CombatSkills = new List<KeyValuePair<WeaponSkill, int>>();
            SpecializedSkills = new List<KeyValuePair<SpecializedSkill, int>>();
            KnowledgeSkills = new List<KeyValuePair<KnowledgeSkill, int>>();

            //Attacks
            Attacks = new List<NpcAttack>();

            //Abilities
            Abilities = new List<NpcAbility>();

            //Talents
            Talents = new List<Talent>();
        }
    }
}
