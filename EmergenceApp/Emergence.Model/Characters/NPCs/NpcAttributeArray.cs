using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
namespace Emergence.Model
{
    public class NpcAttributeArray
    {
        public NpcAttributeArray()
        {
            AttributeAdjustments = new List<AttributeModifier>();
        }

        public NpcAttributeArray(int level, NpcClass npcClass)
        {
            this.Level = level;
            this.npcClass = npcClass;
            AttributeAdjustments = new List<AttributeModifier>();
        }

        //Level
        public int Level;
        public NpcClass npcClass;

        //Primary
        public int baseSpirit;
        public int Spirit
        { get; }

        public int baseStaminaRegen
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 4 + ConvertToInt(Level / 2M);
                    case NpcClass.Grunt:
                        return 0;
                    case NpcClass.Antagonist:
                        return 4 + ConvertToInt(Level / 2M);
                    case NpcClass.Companion:
                        return 0;
                    default:
                        throw new ArgumentOutOfRangeException("Base Stamina Regen is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int StaminaRegen
        { get; }

        //Derived
        private int baseSpeed
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + ConvertToInt(Level / 2M);
                    case NpcClass.Grunt:
                        return 6 + ConvertToInt(Level / 2M);
                    case NpcClass.Antagonist:
                        return 6 + ConvertToInt(Level / 2M);
                    case NpcClass.Companion:
                        return 6 + ConvertToInt(Level / 2M);
                    default:
                        throw new ArgumentOutOfRangeException("Base Speed is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int Speed
        {
            get { return ApplyModifiers("Speed", baseSpeed); }
        }
        private int baseInitiative
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 8 + Level;
                    case NpcClass.Grunt:
                        return 6 + Level;
                    case NpcClass.Antagonist:
                        return 10 + Level;
                    case NpcClass.Companion:
                        return 8 + Level;
                    default:
                        throw new ArgumentOutOfRangeException("Base Initiave is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int Initiative
        {
            get { return ApplyModifiers("Initiative", baseInitiative); }
        }
        private int baseHealthPoints
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level * 2;
                    case NpcClass.Grunt:
                        return 6 + Level * 2;
                    case NpcClass.Antagonist:
                        return 6 + Level * 5;
                    case NpcClass.Companion:
                        return 6 + Level * 2;
                    default:
                        throw new ArgumentOutOfRangeException("Base Health Points is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int HealthPoints
        {
            get { return ApplyModifiers("HealthPoints", baseHealthPoints); }
        }
        public int baseNumberOfDamageTracks
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 3;
                    case NpcClass.Grunt:
                        return 1;
                    case NpcClass.Antagonist:
                        return 3;
                    case NpcClass.Companion:
                        return 3;
                    default:
                        throw new ArgumentOutOfRangeException("Base Number Of Damage Tracks is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int NumberOfDamageTracks
        {
            get { return ApplyModifiers("NumberOfDamageTracks", baseNumberOfDamageTracks); }
        }
        private int baseStaminaPool
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 0;
                    case NpcClass.Grunt:
                        return 0;
                    case NpcClass.Antagonist:
                        return 10 + Level * 2;
                    case NpcClass.Companion:
                        return 0;
                    default:
                        throw new ArgumentOutOfRangeException("Base Stamina Pool is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int StaminaPool
        {
            get { return ApplyModifiers("StaminaPool", baseStaminaPool); }
        }
        private int basePerception
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 5 + Level;
                    case NpcClass.Grunt:
                        return 4 + Level;
                    case NpcClass.Antagonist:
                        return 6 + Level;
                    case NpcClass.Companion:
                        return 5 + Level;
                    default:
                        throw new ArgumentOutOfRangeException("Base Perception is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int Perception
        {
            get { return ApplyModifiers("Perception", basePerception); }
        }
        private int baseDurability
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 5 + ConvertToInt(Level / 2M);
                    case NpcClass.Grunt:
                        return 3 + ConvertToInt(Level / 2M);
                    case NpcClass.Antagonist:
                        return 7 + ConvertToInt(Level / 2M);
                    case NpcClass.Companion:
                        return 5 + ConvertToInt(Level / 2M);
                    default:
                        throw new ArgumentOutOfRangeException("Base Speed is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int Durability
        {
            get { return ApplyModifiers("Durability", baseDurability); }
        }
        private int baseResistance
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 5 + ConvertToInt(Level / 2M);
                    case NpcClass.Grunt:
                        return 3 + ConvertToInt(Level / 2M);
                    case NpcClass.Antagonist:
                        return 7 + ConvertToInt(Level / 2M);
                    case NpcClass.Companion:
                        return 5 + ConvertToInt(Level / 2M);
                    default:
                        throw new ArgumentOutOfRangeException("Base Resistance is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int Resistance
        {
            get { return ApplyModifiers("Resistance", baseResistance); }
        }

        //Defenses
        public int baseMeleePhysical
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Melee/Physical Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int MeleePhysical
        {
            get
            {
                return ApplyModifiers("MeleePhysical", baseMeleePhysical);
            }
        }
        public int baseAreaPhysical
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Area/Physical Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int AreaPhysical
        {
            get
            {
                return ApplyModifiers("AreaPhysical", baseAreaPhysical);
            }
        }
        public int baseRangedPhysical
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Ranged/Physical Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int RangedPhysical
        {
            get
            {
                return ApplyModifiers("RangedPhysical", baseRangedPhysical);
            }
        }
        public int baseMeleeResolve
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Melee/Resolve Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int MeleeResolve
        {
            get
            {
                return ApplyModifiers("MeleeResolve", baseMeleeResolve);
            }
        }
        public int baseAreaResolve
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Area/Resolve Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int AreaResolve
        {
            get
            {
                return ApplyModifiers("AreaResolve", baseAreaResolve);
            }
        }
        public int baseRangedResolve
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Ranged/Resolve Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int RangedResolve
        {
            get
            {
                return ApplyModifiers("RangedResolve", baseRangedResolve);
            }
        }
        public int baseMeleeBody
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Melee/Body Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int MeleeBody
        {
            get
            {
                return ApplyModifiers("MeleeBody", baseMeleeBody);
            }
        }
        public int baseAreaBody
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Area/Body Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int AreaBody
        {
            get
            {
                return ApplyModifiers("AreaBody", baseAreaBody);
            }
        }
        public int baseRangedBody
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 6 + Level + 10;
                    case NpcClass.Grunt:
                        return 5 + Level + 10;
                    case NpcClass.Antagonist:
                        return 7 + Level + 10;
                    case NpcClass.Companion:
                        return 6 + Level + 10;
                    default:
                        throw new ArgumentOutOfRangeException("Base Ranged/Body Defense is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int RangedBody
        {
            get
            {
                return ApplyModifiers("RangedBody", baseRangedBody);
            }
        }

        //Armor
        public int ArmorValue;

        //Secondary
        public int basePrimaryAttack
        {
            get
            {
                return 0;//skill + accuracy is taken care of in the NpcAttack, this is just to hold modifiers
                //switch (this.npcClass)
                //{
                //    case NpcClass.Foe:
                //        return 5 + Level;
                //    case NpcClass.Grunt:
                //        return 4 + Level;
                //    case NpcClass.Antagonist:
                //        return 6 + Level;
                //    case NpcClass.Companion:
                //        return 5 + Level;
                //    default:
                //        throw new ArgumentOutOfRangeException("Base Primary Attack is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                //}
            }
        }
        public int PrimaryAttack
        {
            get
            {
                return ApplyModifiers("PrimaryAttack", basePrimaryAttack);
            }
        }
        public int basePrimaryAttackDamage
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 2 + Level;
                    case NpcClass.Grunt:
                        return 0 + Level;
                    case NpcClass.Antagonist:
                        return 4 + Level;
                    case NpcClass.Companion:
                        return 2 + Level;
                    default:
                        throw new ArgumentOutOfRangeException("Base Primary Damage is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int PrimaryAttackDamage
        {
            get
            {
                return ApplyModifiers("PrimaryAttackDamage", basePrimaryAttackDamage);
            }
        }
        public int baseSecondaryAttack
        {
            get
            {
                return 0;//skill + accuracy is taken care of in the NpcAttack, this is just to hold modifiers
                //switch (this.npcClass)
                //{
                //    case NpcClass.Foe:
                //        return 4 + Level;
                //    case NpcClass.Grunt:
                //        return 3 + Level;
                //    case NpcClass.Antagonist:
                //        return 5 + Level;
                //    case NpcClass.Companion:
                //        return 4 + Level;
                //    default:
                //        throw new ArgumentOutOfRangeException("Base Secondary Attack is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                //}
            }
        }
        public int SecondaryAttack
        {
            get
            {
                return ApplyModifiers("SecondaryAttack", baseSecondaryAttack);
            }
        }
        public int baseSecondaryAttackDamage
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 2 + Level;
                    case NpcClass.Grunt:
                        return 0 + Level;
                    case NpcClass.Antagonist:
                        return 4 + Level;
                    case NpcClass.Companion:
                        return 2 + Level;
                    default:
                        throw new ArgumentOutOfRangeException("Base Secondary Damage is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int SecondaryAttackDamage
        {
            get
            {
                return ApplyModifiers("SecondaryAttackDamage", baseSecondaryAttackDamage);
            }
        }
        public int baseCM
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 0;
                    case NpcClass.Grunt:
                        return -1;
                    case NpcClass.Antagonist:
                        return 1;
                    case NpcClass.Companion:
                        return 0;
                    default:
                        throw new ArgumentOutOfRangeException("Base Crit Mod is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public int CM
        {
            get
            {
                return ApplyModifiers("CM", baseCM);
            }
        }
        public int maxTier
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return 2 + ConvertToInt(Level / 3M);
                    case NpcClass.Grunt:
                        return 1 + ConvertToInt(Level / 3M);
                    case NpcClass.Antagonist:
                        return 3 + ConvertToInt(Level / 3M);
                    case NpcClass.Companion:
                        return 2 + ConvertToInt(Level / 3M);
                    default:
                        throw new ArgumentOutOfRangeException("Base Speed is not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public string Notes
        {
            get
            {
                switch (this.npcClass)
                {
                    case NpcClass.Foe:
                        return "None";
                    case NpcClass.Grunt:
                        return "Can make one free strike per round at no Stamina Cost.  Can use only 1 Quick Action per turn.  While suffering from a Condition, cannot take Combat actions.  -2 to all MCR for Resistance Checks caused by this creature.  Can have 1 Stance or Enhancement active at a time with no Stamina Cost.  Can activate 1 Stamina requiring ability every other round with no Stamina cost.";
                    case NpcClass.Antagonist:
                        return "+2 to all MCR for Resistance Checks caused by this creature.";
                    case NpcClass.Companion:
                        return "Can use only 1 Quick Action per turn.  Must be commanded while in Visual or Audio Range.  Can have 1 Stance or Enhancement active at a time with no Stamina Cost.  Can use one Quick Action and one Combat Action per round that require Stamina.";
                    default:
                        throw new ArgumentOutOfRangeException("Notes are not defined for this NpcClass '" + this.npcClass.ToString() + "'.");
                }
            }
        }
        public string Special;

        //Adjustments
        public List<AttributeModifier> AttributeAdjustments
        { get; set; }

        private int ApplyModifiers(string attributeName, int baseAttribute)
        {
            List<AttributeModifier> additiveModsToApplyToThisAttribute = AttributeAdjustments.Where(aa => aa.AttributeName == attributeName && aa.Type == ModifierType.Additive).ToList();
            List<AttributeModifier> multiplicativeModsToApplyToThisAttribute = AttributeAdjustments.Where(aa => aa.AttributeName == attributeName && aa.Type == ModifierType.Multiplicative).ToList();

            decimal finalAttribute = baseAttribute;
            foreach (var mod in additiveModsToApplyToThisAttribute)
            {
                finalAttribute = finalAttribute + mod.ModifierValue;
            }
            foreach (var mod in multiplicativeModsToApplyToThisAttribute)
            {
                finalAttribute = finalAttribute * mod.ModifierValue;
            }
            return ConvertToInt(finalAttribute);
        }

        private int ConvertToInt(decimal v)
        {
            //Apply base rounding principle (round down)
            return decimal.ToInt32(Math.Floor(v));
        }
    }
}
