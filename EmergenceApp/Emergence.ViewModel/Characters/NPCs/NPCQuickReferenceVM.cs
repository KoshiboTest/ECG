﻿using Emergence.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.ViewModel
{
    public class NPCQuickReferenceVM : INotifyPropertyChanged
    {
        public NonPlayerCharacter model;

        public NPCQuickReferenceVM()
        {
            model = new NonPlayerCharacter();
        }

        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public int Level
        {
            get
            {
                return model.Level;
            }
            set
            {
                model.Level = value;
                model.Attributes.Level = value;
                NotifyAll();
            }
        }
        public int Size
        {
            get
            {
                return model.Size;
            }
            set
            {
                model.Size = value;
                NotifyAll();
            }
        }
        public string NpcClass
        {
            get
            {
                switch (model.NpcClass)
                {
                    case Model.NpcClass.Antagonist:
                        return "Antagonist";
                    case Model.NpcClass.Companion:
                        return "Companion";
                    case Model.NpcClass.Foe:
                        return "Foe";
                    case Model.NpcClass.Grunt:
                        return "Grunt";
                    default:
                        throw new Exception("Class name not defined.");
                }
            }
            set
            {
                switch (value)
                {
                    case "Antagonist":
                        model.NpcClass = Model.NpcClass.Antagonist;
                        break;
                    case "Companion":
                        model.NpcClass = Model.NpcClass.Companion;
                        break;
                    case "Foe":
                        model.NpcClass = Model.NpcClass.Foe;
                        break;
                    case "Grunt":
                        model.NpcClass = Model.NpcClass.Grunt;
                        break;
                    default:
                        break;
                }
                NotifyPropertyChanged("NpcClass");
                NotifyPropertyChanged("Stamina");
                NotifyPropertyChanged("StaminaRegen");
                NotifyPropertyChanged("Perception");
                NotifyPropertyChanged("Initiative");
                NotifyPropertyChanged("Durability");
                NotifyPropertyChanged("DurabilityParenthesis");
                NotifyPropertyChanged("Armor");
                NotifyPropertyChanged("Speed");
                NotifyPropertyChanged("SpeedParenthesis");
                NotifyPropertyChanged("Defenses");
                NotifyPropertyChanged("MeleePhysical");
                NotifyPropertyChanged("MeleeResolve");
                NotifyPropertyChanged("MeleeBody");
                NotifyPropertyChanged("AreaPhysical");
                NotifyPropertyChanged("AreaResolve");
                NotifyPropertyChanged("AreaBody");
                NotifyPropertyChanged("RangedPhysical");
                NotifyPropertyChanged("RangedResolve");
                NotifyPropertyChanged("RangedBody");
                NotifyPropertyChanged("Resistance");
                NotifyPropertyChanged("TotalHealth");
                NotifyPropertyChanged("DamageTracks");
                NotifyPropertyChanged("PrimaryAttack");
                NotifyPropertyChanged("PrimaryAttackDamage");
                NotifyPropertyChanged("PrimaryAttackCM");
                NotifyPropertyChanged("PrimaryAttackRange");
                NotifyPropertyChanged("PrimaryAttackArea");
                NotifyPropertyChanged("SecondaryAttack");
                NotifyPropertyChanged("SecondaryAttackDamage");
                NotifyPropertyChanged("SecondaryAttackCM");
                NotifyPropertyChanged("SecondaryAttackRange");
                NotifyPropertyChanged("SecondaryAttackArea");
                NotifyPropertyChanged("TertiaryAttack");
                NotifyPropertyChanged("TertiaryAttackDamage");
                NotifyPropertyChanged("TertiaryAttackCM");
                NotifyPropertyChanged("TertiaryAttackRange");
                NotifyPropertyChanged("TertiaryAttackArea");
            }
        }
        public string NpcType
        {
            get
            {
                return model.Type.ToString();
            }
            set
            {
                switch (value)
                {
                    case "Energy":
                        model.Type = Model.NpcType.Energy;
                        break;
                    case "Flesh":
                        model.Type = Model.NpcType.Flesh;
                        break;
                    case "Fluid":
                        model.Type = Model.NpcType.Fluid;
                        break;
                    case "Machine":
                        model.Type = Model.NpcType.Machine;
                        break;
                    case "Natural":
                        model.Type = Model.NpcType.Natural;
                        break;
                    case "Plant":
                        model.Type = Model.NpcType.Plant;
                        break;
                    case "Solid":
                        model.Type = Model.NpcType.Solid;
                        break;
                    case "Swarm":
                        model.Type = Model.NpcType.Swarm;
                        break;
                    default:
                        break;
                }
                NotifyPropertyChanged("NpcType");
            }
        }
        public string Archetype
        {
            get
            {
                return model.Archetype.ToString();
            }
            set
            {
                switch (value)
                {
                    case "Beast":
                        model.Archetype = Model.Archetype.Beast;
                        break;
                    case "Demonic":
                        model.Archetype = Model.Archetype.Demonic;
                        break;
                    case "Dragonkin":
                        model.Archetype = Model.Archetype.Flying_aka_Dragonkin;
                        break;
                    case "Elemental":
                        model.Archetype = Model.Archetype.Elemental;
                        break;
                    case "Humanoid":
                        model.Archetype = Model.Archetype.Humanoid;
                        break;
                    case "Risen":
                        model.Archetype = Model.Archetype.Risen;
                        break;
                    default:
                        break;
                }
                NotifyPropertyChanged("Archetype");
            }
        }
        public int Stamina
        {
            get
            {
                switch (model.NpcClass)
                {
                    case Model.NpcClass.Grunt:
                        return 0;
                    case Model.NpcClass.Foe:
                        return StaminaRegen;
                    case Model.NpcClass.Antagonist:
                        return 10 + model.Level * 2;
                    case Model.NpcClass.Companion:
                        return 0;
                    default:
                        throw new Exception("Set NpcClass before getting the Stamina value.");
                }
            }
            set
            {
                NotifyPropertyChanged("Stamina");
            }
        }
        public int StaminaRegen
        {
            get
            {
                switch (model.NpcClass)
                {
                    case Model.NpcClass.Grunt:
                        return 0;
                    case Model.NpcClass.Foe:
                        return 4 + decimal.ToInt32(Math.Floor(model.Level / 2.0M));
                    case Model.NpcClass.Antagonist:
                        return 4 + decimal.ToInt32(Math.Floor(model.Level / 2.0M));
                    case Model.NpcClass.Companion:
                        return 0;
                    default:
                        throw new Exception("Set NpcClass before getting the Stamina Regen value.");
                }
            }
            set
            {
                NotifyPropertyChanged("StaminaRegen");
            }
        }
        public int Perception
        {
            get
            {
                return model.Attributes.Perception;
            }
            set
            {
                AttributeModifier m = new AttributeModifier();
                m.AttributeName = "Perception";
                m.ModifierValue = value - model.Attributes.Perception;
                m.Type = ModifierType.Additive;
                model.Attributes.AttributeAdjustments.Add(m);
                NotifyPropertyChanged("Perception");
            }
        }
        public int Initiative
        {
            get
            {
                return model.Attributes.Initiative;
            }
            set
            {
                AttributeModifier m = new AttributeModifier();
                m.AttributeName = "Initiative";
                m.ModifierValue = value - model.Attributes.Initiative;
                m.Type = ModifierType.Additive;
                model.Attributes.AttributeAdjustments.Add(m);
                NotifyPropertyChanged("Initiative");
            }
        }
        public int Durability
        {
            get
            {
                return model.Attributes.Durability;
            }
            set
            {
                AttributeModifier m = new AttributeModifier();
                m.AttributeName = "Durability";
                m.ModifierValue = value - model.Attributes.Durability;
                m.Type = ModifierType.Additive;
                model.Attributes.AttributeAdjustments.Add(m);
                NotifyPropertyChanged("Durability");
            }
        }
        public int Armor
        {
            get
            {
                return model.Armor.ArmorValue;
            }
        }
        public int Speed
        {
            get
            {
                return model.Attributes.Speed + model.Armor.SpeedPenalty;
            }
            set
            {
                AttributeModifier m = new AttributeModifier();
                m.AttributeName = "Speed";
                m.ModifierValue = value - (model.Attributes.Speed - model.Armor.SpeedPenalty);
                m.Type = ModifierType.Additive;
                model.Attributes.AttributeAdjustments.Add(m);
                NotifyPropertyChanged("Speed");
            }
        }
        public int EffectiveStrength
        {
            get
            {
                return model.EffectiveStrength;
            }
        }
        public int Resistance
        {
            get
            {
                return model.Attributes.Resistance;
            }
        }
        public int TotalHealth
        {
            get
            {
                return model.Attributes.HealthPoints * DamageTracks;
            }
        }
        public int DamageTracks
        {
            get
            {
                switch (model.NpcClass)
                {
                    case Model.NpcClass.Grunt:
                        return 1;
                    case Model.NpcClass.Foe:
                        return 3;
                    case Model.NpcClass.Antagonist:
                        return 3;
                    case Model.NpcClass.Companion:
                        return 3;
                    default:
                        throw new Exception("Set NpcClass before getting the Damage Tracks value.");
                }
            }
        }

        public int MeleePhysical
        {
            get
            {
                return model.Attributes.MeleePhysical + model.Armor.ArmorPenalty;
            }
        }
        public int MeleeResolve
        {
            get
            {
                return model.Attributes.MeleeResolve + model.Armor.ArmorPenalty;
            }
        }
        public int MeleeBody
        {
            get
            {
                return model.Attributes.MeleeBody + model.Armor.ArmorPenalty;
            }
        }
        public int AreaPhysical
        {
            get
            {
                return model.Attributes.AreaPhysical + model.Armor.ArmorPenalty;
            }
        }
        public int AreaResolve
        {
            get
            {
                return model.Attributes.AreaResolve + model.Armor.ArmorPenalty;
            }
        }
        public int AreaBody
        {
            get
            {
                return model.Attributes.AreaBody + model.Armor.ArmorPenalty;
            }
        }
        public int RangedPhysical
        {
            get
            {
                return model.Attributes.RangedPhysical + model.Armor.ArmorPenalty;
            }
        }
        public int RangedResolve
        {
            get
            {
                return model.Attributes.RangedResolve + model.Armor.ArmorPenalty;
            }
        }
        public int RangedBody
        {
            get
            {
                return model.Attributes.RangedBody + model.Armor.ArmorPenalty;
            }
        }

        public string Qualities
        {
            get
            {
                if (model.Qualities == null)
                {
                    return string.Empty;
                }
                return "QUALITIES: " + string.Join(", ", model.Qualities.Select(q => q.Name));
            }
        }
        public string Abilities
        {
            get
            {
                if (model.Abilities == null)
                {
                    return string.Empty;
                }
                return "ABILITIES: " + string.Join(", ", model.Abilities.Select(a => string.Format("{0}: {1} [{2}/{3}]", a.Name, a.Description, a.StaminaCost, a.UpkeepCost)));
            }
        }
        public string Talents
        {
            get
            {
                if (model.Talents == null)
                {
                    return string.Empty;
                }
                return "TALENTS: " + string.Join(Environment.NewLine, model.Talents.Select(t => string.Format("{0}: {1} {2} {3}", t.Name, t.Description, t.ClarifyingText, t.TierBenefitDescription)));
            }
        }
        public string Skills
        {
            get
            {
                //TODO: Implement Skills (other than combat/weapon)
                //NOTE: Whenever this is implemented apply armor penalty to skills
                return "SKILLS: " + "";
            }
        }

        public string Notes
        {
            get
            {
                return string.Join(Environment.NewLine, Qualities, Skills, Abilities, Talents, "NOTES: " + model.Attributes.Notes);
            }
        }

        public string PrimaryAttack
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        return model.Attacks[0].Name + ": +" + (model.Attacks[0].Attack + model.Attributes.PrimaryAttack);
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string PrimaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        return model.Attacks[0].DamageType.ToString() + ": +" + model.Attacks[0].Damage;
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string PrimaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        return model.Attacks[0].CM.ToString() + " CM";
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string PrimaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        if (model.Attacks[0] is NpcMeleeAttack || model.Attacks[0] is NpcMeleeAreaAttack)
                        {
                            return "Melee";
                        }
                        else if (model.Attacks[0] is NpcRangedAttack)
                        {
                            return ((NpcRangedAttack)model.Attacks[0]).RangeType.ToString();
                        }
                        else if (model.Attacks[0] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[0]).RangeType.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string PrimaryAttackArea
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        if (model.Attacks[0] is NpcMeleeAttack || model.Attacks[0] is NpcRangedAttack)
                        {
                            return "None";
                        }
                        else if (model.Attacks[0] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[0]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[0]).Shape.ToString();
                        }
                        else if (model.Attacks[0] is NpcMeleeAreaAttack)
                        {
                            return ((NpcMeleeAreaAttack)model.Attacks[0]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[0]).Shape.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string PrimaryAttackProperties
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        return model.Attacks[0].Weapon.Properties.ToString();
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }

        public string SecondaryAttack
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        return model.Attacks[1].Name + ": +" + (model.Attacks[1].Attack + model.Attributes.SecondaryAttack);
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string SecondaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        return model.Attacks[1].DamageType.ToString() + ": +" + model.Attacks[1].Damage;
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string SecondaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        return model.Attacks[1].CM.ToString() + " CM";
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string SecondaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcMeleeAttack || model.Attacks[1] is NpcMeleeAreaAttack)
                        {
                            return "Melee";
                        }
                        else if (model.Attacks[1] is NpcRangedAttack)
                        {
                            return ((NpcRangedAttack)model.Attacks[1]).RangeType.ToString();
                        }
                        else if (model.Attacks[1] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[1]).RangeType.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string SecondaryAttackArea
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcMeleeAttack || model.Attacks[1] is NpcRangedAttack)
                        {
                            return "None";
                        }
                        else if (model.Attacks[1] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[1]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[1]).Shape.ToString();
                        }
                        else if (model.Attacks[1] is NpcMeleeAreaAttack)
                        {
                            return ((NpcMeleeAreaAttack)model.Attacks[1]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[1]).Shape.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string SecondaryAttackProperties
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        return model.Attacks[1].Weapon.Properties.ToString();
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }

        public string TertiaryAttack
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        return model.Attacks[2].Name + ": +" + (model.Attacks[2].Attack + model.Attributes.SecondaryAttack);
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string TertiaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        return model.Attacks[2].DamageType.ToString() + ": +" + model.Attacks[2].Damage;
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string TertiaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        return model.Attacks[2].CM.ToString() + " CM";
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string TertiaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcMeleeAttack || model.Attacks[2] is NpcMeleeAreaAttack)
                        {
                            return "Melee";
                        }
                        else if (model.Attacks[2] is NpcRangedAttack)
                        {
                            return ((NpcRangedAttack)model.Attacks[2]).RangeType.ToString();
                        }
                        else if (model.Attacks[2] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[2]).RangeType.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string TertiaryAttackArea
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcMeleeAttack || model.Attacks[2] is NpcRangedAttack)
                        {
                            return "None";
                        }
                        else if (model.Attacks[2] is NpcRangedAreaAttack)
                        {
                            return ((NpcRangedAreaAttack)model.Attacks[2]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[2]).Shape.ToString();
                        }
                        else if (model.Attacks[2] is NpcMeleeAreaAttack)
                        {
                            return ((NpcMeleeAreaAttack)model.Attacks[2]).RadiusInFeet.ToString() + " ft. " + ((NpcMeleeAreaAttack)model.Attacks[2]).Shape.ToString();
                        }
                        else
                        {
                            return "None";
                        }
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }
        public string TertiaryAttackProperties
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        return model.Attacks[2].Weapon.Properties.ToString();
                    }
                    else
                    {
                        return "None";
                    }
                }
                catch
                {
                    return "None";
                }
            }
        }

        public void NotifyDefenses()
        {
            NotifyPropertyChanged("MeleePhysical");
            NotifyPropertyChanged("MeleeResolve");
            NotifyPropertyChanged("MeleeBody");
            NotifyPropertyChanged("AreaPhysical");
            NotifyPropertyChanged("AreaResolve");
            NotifyPropertyChanged("AreaBody");
            NotifyPropertyChanged("RangedPhysical");
            NotifyPropertyChanged("RangedResolve");
            NotifyPropertyChanged("RangedBody");
        }

        public void NotifyAttacks()
        {
            NotifyPropertyChanged("PrimaryAttack");
            NotifyPropertyChanged("PrimaryAttackDamage");
            NotifyPropertyChanged("PrimaryAttackCM");
            NotifyPropertyChanged("PrimaryAttackRange");
            NotifyPropertyChanged("PrimaryAttackArea");
            NotifyPropertyChanged("PrimaryAttackProperties");

            NotifyPropertyChanged("SecondaryAttack");
            NotifyPropertyChanged("SecondaryAttackDamage");
            NotifyPropertyChanged("SecondaryAttackCM");
            NotifyPropertyChanged("SecondaryAttackRange");
            NotifyPropertyChanged("SecondaryAttackArea");
            NotifyPropertyChanged("SecondaryAttackProperties");

            NotifyPropertyChanged("TertiaryAttack");
            NotifyPropertyChanged("TertiaryAttackDamage");
            NotifyPropertyChanged("TertiaryAttackCM");
            NotifyPropertyChanged("TertiaryAttackRange");
            NotifyPropertyChanged("TertiaryAttackArea");
            NotifyPropertyChanged("TertiaryAttackProperties");
        }

        public void NotifyStats()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Level");
            NotifyPropertyChanged("Size");
            NotifyPropertyChanged("NpcClass");
            NotifyPropertyChanged("NpcType");
            NotifyPropertyChanged("Archetype");
            NotifyPropertyChanged("Stamina");
            NotifyPropertyChanged("StaminaRegen");
            NotifyPropertyChanged("Perception");
            NotifyPropertyChanged("Initiative");
            NotifyPropertyChanged("Durability");
            NotifyPropertyChanged("Armor");
            NotifyPropertyChanged("Speed");
            NotifyPropertyChanged("EffectiveStrength");
            NotifyPropertyChanged("Resistance");
            NotifyPropertyChanged("TotalHealth");
            NotifyPropertyChanged("DamageTracks");
        }

        public void NotifyAll()
        {
            NotifyDefenses();
            NotifyAttacks();
            NotifyStats();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
