using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Model
{
    public class AttributeArray
    {
        public AttributeArray()
        {
        }

        public AttributeArray(int level, int str, int agi, int fort, int foc, int wil, int pre, int spi, int sr)
        {
            Level = level;
            baseStrength = str;
            baseAgility = agi;
            baseFortitude = fort;
            baseFocus = foc;
            baseWillpower = wil;
            basePresence = pre;
            baseSpirit = spi;
            baseStaminaRegen = sr;
        }

        //Level
        public int Level;

        //Primary
        public int baseStrength;
        public int Strength
        { get; }
        public int baseAgility;
        public int Agility
        { get; }
        public int baseFortitude;
        public int Fortitude
        { get; }
        public int baseFocus;
        public int Focus
        { get; }
        public int baseWillpower;
        public int Willpower
        { get; }
        public int basePresence;
        public int Presence
        { get; }
        public int baseSpirit;
        public int Spirit
        { get; }
        public int baseStaminaRegen;
        public int StaminaRegen
        { get; }

        //Derived
        private int baseSpeed
        {
            get { return 2 + ((Strength + Agility + Fortitude) / 2); }
        }
        public int Speed
        {
            get { return ApplyModifiers("Speed", baseSpeed); }
        }
        private int baseInitiative
        {
            get { return Agility + Focus + Presence; }
        }
        public int Initiative
        {
            get { return ApplyModifiers("Initiative", baseInitiative); }
        }
        private int baseHealthPoints
        {
            get { return Fortitude + Fortitude + Willpower + Level; }
        }
        public int HealthPoints
        {
            get { return ApplyModifiers("HealthPoints", baseHealthPoints); }
        }
        private int baseStaminaPool
        {
            get { return 10 + Level + Level; }
        }
        public int StaminaPool
        {
            get { return ApplyModifiers("StaminaPool", baseStaminaPool); }
        }
        private int basePerception
        {
            get { return Focus + Willpower; }
        }
        public int Perception
        {
            get { return ApplyModifiers("Perception", basePerception); }
        }
        private int baseConnection
        {
            get { return Presence + Level; }
        }
        public int Connection
        {
            get { return ApplyModifiers("Connection", baseConnection); }
        }
        private int baseDurability
        {
            get { return 5 + ArmorValue; }
        }
        public int Durability
        {
            get { return ApplyModifiers("Durability", baseDurability); }
        }
        private int baseResistance
        {
            get { return Fortitude + Willpower; }
        }
        public int Resistance
        {
            get { return ApplyModifiers("Resistance", baseResistance); }
        }

        //Defenses
        public int baseMeleePhysical
        { get { return Strength + Agility; } }
        public int MeleePhysical
        { get; }
        public int baseAreaPhysical
        { get { return Presence + Agility; } }
        public int AreaPhysical
        { get; }
        public int baseRangedPhysical
        { get { return Focus + Agility; } }
        public int RangedPhysical
        { get; }
        public int baseMeleeResolve
        { get { return Strength + Willpower; } }
        public int MeleeResolve
        { get; }
        public int baseAreaResolve
        { get { return Presence + Willpower; } }
        public int AreaResolve
        { get; }
        public int baseRangedResolve
        { get { return Focus + Willpower; } }
        public int RangedResolve
        { get; }
        public int baseMeleeBody
        { get { return Strength + Fortitude; } }
        public int MeleeBody
        { get; }
        public int baseAreaBody
        { get { return Presence + Fortitude; } }
        public int AreaBody
        { get; }
        public int baseRangedBody
        { get { return Focus + Fortitude; } }
        public int RangedBody
        { get; }

        //Armor
        public int ArmorValue;

        //Secondary
        public int RecoveryModifier
        { get; set; }
        public int Influence
        { get; set; }
        public int StreetCred
        { get; set; }

        //Adjustments
        List<AttributeModifier> AttributeAdjustments
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
