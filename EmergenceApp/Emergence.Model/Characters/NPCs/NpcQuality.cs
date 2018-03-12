using System.Collections.Generic;

namespace Emergence.Model
{
    public class NpcQuality
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private List<AttributeModifier> modifiers = new List<AttributeModifier>();
        public List<AttributeModifier> Modifiers
        {
            get
            {
                return modifiers;
            }
            set
            {
                modifiers = value;
            }
        }
        /// <summary>
        /// Suggested: +1 at first level, +2 at third, +3 at sixth, +4 at tenth.
        /// CM/Lethal: +1 at first level, +2 at fifth, +3 at tenth
        /// </summary>
        private int xvariable;
        public int xVariable
        {
            get
            {
                return xvariable;
            }
            set
            {
                if (name.Contains("(" + xvariable.ToString() + ")"))
                {
                    name.Replace("(" + xvariable.ToString() + ")", "(" + value.ToString() + ")");
                }
                xvariable = value;
            }
        }
        public List<Weapon> GrantedWeapons = new List<Weapon>();
        public List<Armor> GrantedArmors = new List<Armor>();
    }

    //Properties
    //Fortified: +4 armor vs (X)
    //Susceptible: Takes +4 damage from (X)
    //Prime Defense: +2 to defense (X)
    //Lesser Defense: -2 to defense (X)
    //Immunities: Damage from (X) is reduced to zero
}