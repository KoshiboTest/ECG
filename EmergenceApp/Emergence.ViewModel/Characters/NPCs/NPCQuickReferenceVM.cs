using Emergence.Model;
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
                        throw new ArgumentOutOfRangeException("Class name not defined.");
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
                    case "Flesh_aka_Unliving":
                        model.Type = Model.NpcType.Flesh_aka_Unliving;
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
                if (model.Archetype.ToString().Equals(value))
                {
                    return;
                }
                else
                {
                    model.Qualities.Clear();
                    model.SpecializedSkills.Clear();
                }
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
                Lair l = new Lair();
                l.AddArchetypeToEnemy(this, model.Archetype);
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
                        throw new ArgumentOutOfRangeException("Set NpcClass before getting the Stamina value.");
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
                        throw new ArgumentOutOfRangeException("Set NpcClass before getting the Stamina Regen value.");
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
                return model.Attributes.Speed - model.Armor.SpeedPenalty;
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
                var multiplier = 1.0;
                var additive = 0;
                if (Size < 3)
                {
                    multiplier = .25 * Size + .25;
                }
                if (Size == 4)
                {
                    additive = 5;
                }
                if (Size > 4)
                {
                    additive = (Size - 4) * 10;
                }
                return ConvertToInt(model.Attributes.HealthPoints * multiplier + additive) * DamageTracks;
            }
        }

        private int ConvertToInt(double v)
        {            
            //Apply base rounding principle (round up)
            return Convert.ToInt32(Math.Ceiling(v));            
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
                        throw new ArgumentOutOfRangeException("Set NpcClass before getting the Damage Tracks value.");
                }
            }
        }

        public int MeleePhysical
        {
            get
            {
                return model.Attributes.MeleePhysical - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int MeleeResolve
        {
            get
            {
                return model.Attributes.MeleeResolve - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int MeleeBody
        {
            get
            {
                return model.Attributes.MeleeBody - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int AreaPhysical
        {
            get
            {
                return model.Attributes.AreaPhysical - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int AreaResolve
        {
            get
            {
                return model.Attributes.AreaResolve - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int AreaBody
        {
            get
            {
                return model.Attributes.AreaBody - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int RangedPhysical
        {
            get
            {
                return model.Attributes.RangedPhysical - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int RangedResolve
        {
            get
            {
                return model.Attributes.RangedResolve - model.Armor.ArmorPenalty - model.Size + 3;
            }
        }
        public int RangedBody
        {
            get
            {
                return model.Attributes.RangedBody - model.Armor.ArmorPenalty - model.Size + 3;
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
                StringBuilder skillsString = new StringBuilder();
                skillsString.Append("SKILLS: ");
                foreach (var cs in model.CombatSkills)
                {
                    skillsString.Append(cs.Key.ToString());
                    skillsString.Append(": ");
                    skillsString.Append(cs.Value);
                    skillsString.Append("  ");
                }
                foreach (var cs in model.SpecializedSkills)
                {
                    skillsString.Append(cs.Key.ToString());
                    skillsString.Append(": ");
                    skillsString.Append(cs.Value);
                    skillsString.Append("  ");
                }
                foreach (var cs in model.KnowledgeSkills)
                {
                    skillsString.Append(cs.Key.ToString());
                    skillsString.Append(": ");
                    skillsString.Append(cs.Value);
                    skillsString.Append("  ");
                }
                return skillsString.ToString();
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
                        if (model.Attacks[0] is NpcWeaponAttack)
                        {
                            var weapon = (model.Attacks[0] as NpcWeaponAttack).Weapon;
                            int baseAttack = weapon.Accuracy + model.Attributes.PrimaryAttack - Size + 3;
                            string rangeString = "";
                            switch (weapon.Range)
                            {
                                //Add ranged breakdown
                                case Range.Pistol:
                                    rangeString = (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.SMG:
                                    rangeString = (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Shotgun:
                                    rangeString = (baseAttack - 4).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Rifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.HeavyRifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString();
                                    break;
                                case Range.Bows:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.Thrown:
                                    rangeString = (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                default:
                                    rangeString = baseAttack.ToString();
                                    break;
                            }
                            return model.Attacks[0].Name + ": +" + rangeString;
                        }
                        else if (model.Attacks[0] is NpcAmpAttack)
                        {
                            return model.Attacks[0].Name + ": +" + ((model.Attacks[0] as NpcAmpAttack).Amp.Accuracy + model.Attributes.PrimaryAttack - Size + 3);
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string PrimaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        if (model.Attacks[0] is NpcWeaponAttack)
                        {
                            return (model.Attacks[0] as NpcWeaponAttack).Weapon.Type.ToString() + ": +" + ((model.Attacks[0] as NpcWeaponAttack).Weapon.Damage + model.Attributes.PrimaryAttackDamage + 2 * (Size - 3));
                        }
                        else if (model.Attacks[0] is NpcAmpAttack)
                        {
                            return (model.Attacks[0] as NpcAmpAttack).Amp.Type.ToString() + ": +" + ((model.Attacks[0] as NpcAmpAttack).Amp.Damage + model.Attributes.PrimaryAttackDamage + 2 * (Size - 3));
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string PrimaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        if (model.Attacks[0] is NpcWeaponAttack)
                        {
                            return ((model.Attacks[0] as NpcWeaponAttack).Weapon.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else if (model.Attacks[0] is NpcAmpAttack)
                        {
                            return ((model.Attacks[0] as NpcAmpAttack).Amp.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string PrimaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 0)
                    {
                        if (model.Attacks[0] is NpcWeaponAttack)
                        {
                            return (model.Attacks[0] as NpcWeaponAttack).Weapon.Range.ToString();
                        }
                        else if (model.Attacks[0] is NpcAmpAttack)
                        {
                            return (model.Attacks[0] as NpcAmpAttack).Amp.Range.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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
                        if (model.Attacks[0] is NpcWeaponAttack)
                        {
                            return (model.Attacks[0] as NpcWeaponAttack).Weapon.Properties.ToString();
                        }
                        else if (model.Attacks[0] is NpcAmpAttack)
                        {
                            return (model.Attacks[0] as NpcAmpAttack).Amp.Properties.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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

        public string SecondaryAttack
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcWeaponAttack)
                        {
                            var weapon = (model.Attacks[1] as NpcWeaponAttack).Weapon;
                            int baseAttack = weapon.Accuracy + model.Attributes.SecondaryAttack - Size + 3;
                            string rangeString = "";
                            switch (weapon.Range)
                            {
                                //Add ranged breakdown
                                case Range.Pistol:
                                    rangeString = (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.SMG:
                                    rangeString = (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Shotgun:
                                    rangeString = (baseAttack - 4).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Rifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.HeavyRifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString();
                                    break;
                                case Range.Bows:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.Thrown:
                                    rangeString = (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                default:
                                    rangeString = baseAttack.ToString();
                                    break;
                            }
                            return model.Attacks[1].Name + ": +" + rangeString;
                        }
                        else if (model.Attacks[1] is NpcAmpAttack)
                        {
                            return model.Attacks[1].Name + ": +" + ((model.Attacks[1] as NpcAmpAttack).Amp.Accuracy + model.Attributes.SecondaryAttack - Size + 3);
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string SecondaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcWeaponAttack)
                        {
                            return (model.Attacks[1] as NpcWeaponAttack).Weapon.Type.ToString() + ": +" + ((model.Attacks[1] as NpcWeaponAttack).Weapon.Damage + model.Attributes.SecondaryAttackDamage + 2 * (Size - 3));
                        }
                        else if (model.Attacks[1] is NpcAmpAttack)
                        {
                            return (model.Attacks[1] as NpcAmpAttack).Amp.Type.ToString() + ": +" + ((model.Attacks[1] as NpcAmpAttack).Amp.Damage + model.Attributes.SecondaryAttackDamage + 2 * (Size - 3));
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string SecondaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcWeaponAttack)
                        {
                            return ((model.Attacks[1] as NpcWeaponAttack).Weapon.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else if (model.Attacks[1] is NpcAmpAttack)
                        {
                            return ((model.Attacks[1] as NpcAmpAttack).Amp.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string SecondaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 1)
                    {
                        if (model.Attacks[1] is NpcWeaponAttack)
                        {
                            return (model.Attacks[1] as NpcWeaponAttack).Weapon.Range.ToString();
                        }
                        else if (model.Attacks[1] is NpcAmpAttack)
                        {
                            return (model.Attacks[1] as NpcAmpAttack).Amp.Range.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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
                        if (model.Attacks[1] is NpcWeaponAttack)
                        {
                            return (model.Attacks[1] as NpcWeaponAttack).Weapon.Properties.ToString();
                        }
                        else if (model.Attacks[1] is NpcAmpAttack)
                        {
                            return (model.Attacks[1] as NpcAmpAttack).Amp.Properties.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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

        public string TertiaryAttack
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcWeaponAttack)
                        {
                            var weapon = (model.Attacks[2] as NpcWeaponAttack).Weapon;
                            int baseAttack = weapon.Accuracy + model.Attributes.SecondaryAttack - Size + 3;
                            string rangeString = "";
                            switch (weapon.Range)
                            {
                                //Add ranged breakdown
                                case Range.Pistol:
                                    rangeString = (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.SMG:
                                    rangeString = (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Shotgun:
                                    rangeString = (baseAttack - 4).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                case Range.Rifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.HeavyRifle:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString();
                                    break;
                                case Range.Bows:
                                    rangeString = (baseAttack - 6).ToString() + "/" + (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString();
                                    break;
                                case Range.Thrown:
                                    rangeString = (baseAttack - 0).ToString() + "/" + (baseAttack - 1).ToString() + "/" + (baseAttack - 2).ToString() + "/" + (baseAttack - 4).ToString() + "/" + (baseAttack - 6).ToString();
                                    break;
                                default:
                                    rangeString = baseAttack.ToString();
                                    break;
                            }
                            return model.Attacks[2].Name + ": +" + rangeString;
                        }
                        else if (model.Attacks[2] is NpcAmpAttack)
                        {
                            return model.Attacks[2].Name + ": +" + ((model.Attacks[2] as NpcAmpAttack).Amp.Accuracy + model.Attributes.SecondaryAttack - Size + 3);
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string TertiaryAttackDamage
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcWeaponAttack)
                        {
                            return (model.Attacks[2] as NpcWeaponAttack).Weapon.Type.ToString() + ": +" + ((model.Attacks[2] as NpcWeaponAttack).Weapon.Damage + model.Attributes.SecondaryAttackDamage + 2 * (Size - 3));
                        }
                        else if (model.Attacks[2] is NpcAmpAttack)
                        {
                            return (model.Attacks[2] as NpcAmpAttack).Amp.Type.ToString() + ": +" + ((model.Attacks[2] as NpcAmpAttack).Amp.Damage + model.Attributes.SecondaryAttackDamage + 2 * (Size - 3));
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string TertiaryAttackCM
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcWeaponAttack)
                        {
                            return ((model.Attacks[2] as NpcWeaponAttack).Weapon.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else if (model.Attacks[2] is NpcAmpAttack)
                        {
                            return ((model.Attacks[2] as NpcAmpAttack).Amp.CM + model.Attributes.CM).ToString() + " CM";
                        }
                        else
                        {
                            throw new NotImplementedException();
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
        public string TertiaryAttackRange
        {
            get
            {
                try
                {
                    if (model.Attacks.Count > 2)
                    {
                        if (model.Attacks[2] is NpcWeaponAttack)
                        {
                            return (model.Attacks[2] as NpcWeaponAttack).Weapon.Range.ToString();
                        }
                        else if (model.Attacks[2] is NpcAmpAttack)
                        {
                            return (model.Attacks[2] as NpcAmpAttack).Amp.Range.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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
                        if (model.Attacks[2] is NpcWeaponAttack)
                        {
                            return (model.Attacks[2] as NpcWeaponAttack).Weapon.Properties.ToString();
                        }
                        else if (model.Attacks[2] is NpcAmpAttack)
                        {
                            return (model.Attacks[2] as NpcAmpAttack).Amp.Properties.ToString();
                        }
                        else
                        {
                            throw new NotImplementedException();
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
            NotifyPropertyChanged("Notes");
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
