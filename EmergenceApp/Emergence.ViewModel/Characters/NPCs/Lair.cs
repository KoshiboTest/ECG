using Emergence.Model;
using Emergence.ViewModel.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Model.Equipment;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace Emergence.ViewModel
{
    //TODO: Replace these text values everywhere at the end
    //TODO: Fix the fact that natual weapons are not saved to disk
    // Th = " "
    // th = " "
    // ff = " "
    // fi = " "
    // ft = " "

    /// <summary>
    /// NPC spawn
    /// </summary>
    public class Lair : INotifyPropertyChanged
    {
        Random r = new Random();
        private ObservableCollection<NPCQuickReferenceVM> enemies = new ObservableCollection<NPCQuickReferenceVM>();
        public ObservableCollection<NPCQuickReferenceVM> Enemies
        {
            get
            {
                return enemies;
            }
            private set
            {
                enemies = value;
                NotifyPropertyChanged("Enemies");
            }
        }

        public void AddNewNPC(string name, int level, int size, NpcClass npcClass, NpcType npcType, Archetype archetype, List<int> abilityIndexes, List<int> qualityIndexes, List<int> talentIndexes)
        {
            NPCQuickReferenceVM enemy = new NPCQuickReferenceVM();
            //Archetype
            Archetype ra = archetype;

            //Level
            NpcClass rc = npcClass;
            NpcType rt = npcType;
            enemy.model = new NonPlayerCharacter(ra, level, rc, size, rt);
            enemy.model.Qualities = new List<NpcQuality>();
            AddArchetypeToEnemy(enemy, ra);

            enemy.Name = name;

            //Add type qualities
            #region
            switch (rt)
            {
                case NpcType.Flesh_aka_Unliving:
                    {
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);

                        NpcQuality susceptibleFire = new NpcQuality();
                        susceptibleFire.Name = "Susceptible (Fire)";
                        susceptibleFire.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleFire);
                        NpcQuality susceptibleAcid = new NpcQuality();
                        susceptibleAcid.Name = "Susceptible (Acid)";
                        susceptibleAcid.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleAcid);

                        AttributeModifier primeMeleeBody = new AttributeModifier();
                        primeMeleeBody.AttributeName = "MeleeBody";
                        primeMeleeBody.ModifierValue = 2;
                        primeMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                        AttributeModifier primeAreaBody = new AttributeModifier();
                        primeAreaBody.AttributeName = "AreaBody";
                        primeAreaBody.ModifierValue = 2;
                        primeAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                        AttributeModifier primeRangedBody = new AttributeModifier();
                        primeRangedBody.AttributeName = "RangedBody";
                        primeRangedBody.ModifierValue = 2;
                        primeRangedBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);

                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserRangedResolve = new AttributeModifier();
                        lesserRangedResolve.AttributeName = "RangedResolve";
                        lesserRangedResolve.ModifierValue = -2;
                        lesserRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Secondary Crit Effects)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        AttributeModifier hp2 = new AttributeModifier();
                        hp2.AttributeName = "HealthPoints";
                        hp2.ModifierValue = 2;
                        hp2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(hp2);
                        break;
                    }
                case NpcType.Plant:
                    {
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedPoison = new NpcQuality();
                        fortifiedPoison.Name = "Fortified (Poison)";
                        fortifiedPoison.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPoison);

                        NpcQuality susceptibleFire = new NpcQuality();
                        susceptibleFire.Name = "Susceptible (Fire)";
                        susceptibleFire.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleFire);
                        NpcQuality susceptibleSlashing = new NpcQuality();
                        susceptibleSlashing.Name = "Susceptible (Slashing)";
                        susceptibleSlashing.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleSlashing);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserMeleeBody = new AttributeModifier();
                        lesserMeleeBody.AttributeName = "MeleeBody";
                        lesserMeleeBody.ModifierValue = -2;
                        lesserMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);

                        enemy.model.Attributes.Special = "-1 CM against Ranged Attacks";
                        break;
                    }
                case NpcType.Fluid:
                    {
                        NpcQuality fortifiedPhysical = new NpcQuality();
                        fortifiedPhysical.Name = "Fortified (Physical)";
                        fortifiedPhysical.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPhysical);

                        NpcQuality susceptibleCold = new NpcQuality();
                        susceptibleCold.Name = "Susceptible (Cold)";
                        susceptibleCold.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleCold);

                        AttributeModifier primeMeleePhysical = new AttributeModifier();
                        primeMeleePhysical.AttributeName = "MeleePhysical";
                        primeMeleePhysical.ModifierValue = 2;
                        primeMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                        AttributeModifier primeAreaPhysical = new AttributeModifier();
                        primeAreaPhysical.AttributeName = "AreaPhysical";
                        primeAreaPhysical.ModifierValue = 2;
                        primeAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                        AttributeModifier primeRangedPhysical = new AttributeModifier();
                        primeRangedPhysical.AttributeName = "RangedPhysical";
                        primeRangedPhysical.ModifierValue = 2;
                        primeRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);

                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserAreaBody = new AttributeModifier();
                        lesserAreaBody.AttributeName = "AreaBody";
                        lesserAreaBody.ModifierValue = -2;
                        lesserAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);

                        NpcQuality immunePiercing = new NpcQuality();
                        immunePiercing.Name = "Immune (Piercing)";
                        immunePiercing.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immunePiercing);

                        enemy.model.Attributes.Special = "+5 to Athletics checks to escape";
                        break;
                    }
                case NpcType.Swarm:
                    {
                        NpcQuality fortifiedMelee = new NpcQuality();
                        fortifiedMelee.Name = "Fortified (Melee)";
                        fortifiedMelee.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedMelee);
                        NpcQuality fortifiedRanged = new NpcQuality();
                        fortifiedRanged.Name = "Fortified (Ranged)";
                        fortifiedRanged.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedRanged);

                        NpcQuality susceptibleArea = new NpcQuality();
                        susceptibleArea.Name = "Susceptible (Area)";
                        susceptibleArea.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleArea);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Mind Control)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "+5 to defense on incoming Social Skill Attacks";
                        break;
                    }
                case NpcType.Machine:
                    {
                        NpcQuality fortifiedFire = new NpcQuality();
                        fortifiedFire.Name = "Fortified (Fire)";
                        fortifiedFire.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedFire);
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedForce = new NpcQuality();
                        fortifiedForce.Name = "Fortified (Force)";
                        fortifiedForce.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedForce);

                        NpcQuality susceptibleAcid = new NpcQuality();
                        susceptibleAcid.Name = "Susceptible (Acid)";
                        susceptibleAcid.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleAcid);
                        NpcQuality susceptibleElectricity = new NpcQuality();
                        susceptibleElectricity.Name = "Susceptible (Electricity)";
                        susceptibleElectricity.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleElectricity);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Mind Control)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);
                        NpcQuality immune2 = new NpcQuality();
                        immune2.Name = "Immune (Social Skill Attacks)";
                        immune2.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune2);

                        enemy.model.Attributes.Special = "-1 CM to incoming reaction attacks";
                        break;
                    }
                case NpcType.Energy:
                    {
                        NpcQuality fortifiedReactionAttacks = new NpcQuality();
                        fortifiedReactionAttacks.Name = "Fortified (Reaction Attacks)";
                        fortifiedReactionAttacks.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedReactionAttacks);

                        if (r.NextDouble() < .5)
                        {
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }
                        else
                        {
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);

                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        }

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Same Energy Type)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "All attacks gain damage type matching energy type";
                        break;
                    }
                case NpcType.Solid:
                    {
                        NpcQuality fortifiedPiercing = new NpcQuality();
                        fortifiedPiercing.Name = "Fortified (Piercing)";
                        fortifiedPiercing.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPiercing);
                        NpcQuality fortifiedBallistic = new NpcQuality();
                        fortifiedBallistic.Name = "Fortified (Ballistic)";
                        fortifiedBallistic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedBallistic);
                        NpcQuality fortifiedSlashing = new NpcQuality();
                        fortifiedSlashing.Name = "Fortified (Slashing)";
                        fortifiedSlashing.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedSlashing);
                        NpcQuality fortifiedPsycic = new NpcQuality();
                        fortifiedPsycic.Name = "Fortified (Psycic)";
                        fortifiedPsycic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPsycic);
                        NpcQuality fortifiedUnholy = new NpcQuality();
                        fortifiedUnholy.Name = "Fortified (Unholy)";
                        fortifiedUnholy.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedUnholy);
                        NpcQuality fortifiedHoly = new NpcQuality();
                        fortifiedHoly.Name = "Fortified (Holy)";
                        fortifiedHoly.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedHoly);
                        NpcQuality fortifiedFire = new NpcQuality();
                        fortifiedFire.Name = "Fortified (Fire)";
                        fortifiedFire.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedFire);
                        NpcQuality fortifiedAcid = new NpcQuality();
                        fortifiedAcid.Name = "Fortified (Acid)";
                        fortifiedAcid.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedAcid);
                        NpcQuality fortifiedElectricity = new NpcQuality();
                        fortifiedElectricity.Name = "Fortified (Electricity)";
                        fortifiedElectricity.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedElectricity);
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedNecrotic = new NpcQuality();
                        fortifiedNecrotic.Name = "Fortified (Necrotic)";
                        fortifiedNecrotic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedNecrotic);
                        NpcQuality fortifiedPoison = new NpcQuality();
                        fortifiedPoison.Name = "Fortified (Poison)";
                        fortifiedPoison.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPoison);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserMeleeBody = new AttributeModifier();
                        lesserMeleeBody.AttributeName = "MeleeBody";
                        lesserMeleeBody.ModifierValue = -2;
                        lesserMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserAreaBody = new AttributeModifier();
                        lesserAreaBody.AttributeName = "AreaBody";
                        lesserAreaBody.ModifierValue = -2;
                        lesserAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                        AttributeModifier lesserRangedResolve = new AttributeModifier();
                        lesserRangedResolve.AttributeName = "RangedResolve";
                        lesserRangedResolve.ModifierValue = -2;
                        lesserRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        AttributeModifier lesserRangedBody = new AttributeModifier();
                        lesserRangedBody.AttributeName = "RangedBody";
                        lesserRangedBody.ModifierValue = -2;
                        lesserRangedBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);

                        NpcQuality susceptibleBludgeoning = new NpcQuality();
                        susceptibleBludgeoning.Name = "Susceptible (Bludgeoning)";
                        susceptibleBludgeoning.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleBludgeoning);
                        NpcQuality susceptibleForce = new NpcQuality();
                        susceptibleForce.Name = "Susceptible (Force)";
                        susceptibleForce.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleForce);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Secondary Crit Effects)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "-1 CM on incoming attacks";

                        AttributeModifier durability2 = new AttributeModifier();
                        durability2.AttributeName = "Durability";
                        durability2.ModifierValue = 2;
                        durability2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(durability2);

                        AttributeModifier speed2 = new AttributeModifier();
                        speed2.AttributeName = "Speed";
                        speed2.ModifierValue = -2;
                        speed2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(speed2);
                        break;
                    }
                case NpcType.Natural:
                    {
                        ChooseDefense d = new ChooseDefense("Pick Prime Defense");
                        d.ShowDialog();
                        int randPrimeDef = d.ChosenDefense;
                        if (randPrimeDef == 0)
                        {
                            // Melee
                            AttributeModifier primeMeleePhysical = new AttributeModifier();
                            primeMeleePhysical.AttributeName = "MeleePhysical";
                            primeMeleePhysical.ModifierValue = 2;
                            primeMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                        }
                        else if (randPrimeDef == 1)
                        {
                            // Area
                            AttributeModifier primeAreaPhysical = new AttributeModifier();
                            primeAreaPhysical.AttributeName = "AreaPhysical";
                            primeAreaPhysical.ModifierValue = 2;
                            primeAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                        }
                        else if (randPrimeDef == 2)
                        {
                            //Ranged
                            AttributeModifier primeRangedPhysical = new AttributeModifier();
                            primeRangedPhysical.AttributeName = "RangedPhysical";
                            primeRangedPhysical.ModifierValue = 2;
                            primeRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);
                        }
                        else if (randPrimeDef == 3)
                        {
                            //Physical
                            AttributeModifier primeMeleePhysical = new AttributeModifier();
                            primeMeleePhysical.AttributeName = "MeleePhysical";
                            primeMeleePhysical.ModifierValue = 2;
                            primeMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                            AttributeModifier primeAreaPhysical = new AttributeModifier();
                            primeAreaPhysical.AttributeName = "AreaPhysical";
                            primeAreaPhysical.ModifierValue = 2;
                            primeAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                            AttributeModifier primeRangedPhysical = new AttributeModifier();
                            primeRangedPhysical.AttributeName = "RangedPhysical";
                            primeRangedPhysical.ModifierValue = 2;
                            primeRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);
                        }
                        else if (randPrimeDef == 4)
                        {
                            //Resolve
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);
                        }
                        else
                        {
                            //Body
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);
                        }

                        //int randLesserDef = r.Next(0, 6);
                        ChooseDefense l = new ChooseDefense("Pick Lesser Defense");
                        l.ShowDialog();
                        int randLesserDef = l.ChosenDefense;
                        if (randLesserDef == 0)
                        {
                            // Melee
                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                        }
                        else if (randLesserDef == 1)
                        {
                            // Area
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                        }
                        else if (randLesserDef == 2)
                        {
                            //Ranged
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }
                        else if (randLesserDef == 3)
                        {
                            //Physical
                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                        }
                        else if (randLesserDef == 4)
                        {
                            //Resolve
                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        }
                        else
                        {
                            //Body
                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }

                        AttributeModifier skill1 = new AttributeModifier();
                        skill1.AttributeName = "PrimaryAttack";
                        skill1.Type = ModifierType.Additive;
                        skill1.ModifierValue = 1;
                        enemy.model.Attributes.AttributeAdjustments.Add(skill1);

                        AttributeModifier skill2 = new AttributeModifier();
                        skill2.AttributeName = "SecondaryAttack";
                        skill2.Type = ModifierType.Additive;
                        skill2.ModifierValue = 1;
                        enemy.model.Attributes.AttributeAdjustments.Add(skill2);

                        enemy.model.Attributes.Special = "+1 to all skills";
                        break;
                    }
            }
            #endregion

            //Add selected stuff
            foreach (var i in abilityIndexes)
            {
                if (i >= 0 && i < CreatureAbilities.Count())
                {
                    try
                    {
                        AddAbilityByIndex(enemy, i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            foreach (var i in talentIndexes)
            {
                if (i >= 0 && i < Talents.Count())
                {
                    try
                    {
                        AddTalentByIndex(enemy, i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            foreach (var i in qualityIndexes)
            {
                if (i >= 0 && i < CreatureQualities.Count())
                {
                    try
                    {
                        AddQualityByIndex(enemy, i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            Enemies.Add(enemy);
        }

        private ObservableCollection<NpcQuality> creatureQualities = new ObservableCollection<NpcQuality>();
        public ObservableCollection<NpcQuality> CreatureQualities
        {
            get
            {
                return creatureQualities;
            }
            private set
            {
                creatureQualities = value;
                NotifyPropertyChanged("CreatureQualities");
            }
        }

        public ObservableCollection<NpcAbility> CreatureAbilities { get; set; }
        public ObservableCollection<Talent> Talents
        {
            get; set;
        }

        public Lair()
        {
            Talents = new ObservableCollection<Talent>();
            InitializeCreatureQualities();
            InitializeAbilities();
            InitializeTalents();
            Load();
        }

        private void InitializeAbilities()
        {
            CreatureAbilities = new ObservableCollection<NpcAbility>();

            #region AreaAttack
            NpcAbility areaAttack = new Model.NpcAbility();
            areaAttack.Name = "Area Attack";
            NpcWeaponAttack grantedAttack = new NpcWeaponAttack();
            grantedAttack.Weapon = new NaturalWeapon(NaturalWeaponClass.Light);
            switch (r.Next(0, 11))
            {
                case 0:
                    grantedAttack.Weapon.Range = Range.Melee | Range.FifteenFootCone;
                    break;
                case 1:
                    grantedAttack.Weapon.Range = Range.Melee | Range.ThirtyFootCone;
                    break;
                case 2:
                    grantedAttack.Weapon.Range = Range.Melee | Range.FiveFootRadius;
                    break;
                case 3:
                    grantedAttack.Weapon.Range = Range.Melee | Range.TenFootRadius;
                    break;
                case 4:
                    grantedAttack.Weapon.Range = Range.Pistol | Range.TenFootRadius;
                    break;
                case 5:
                    grantedAttack.Weapon.Range = Range.Rifle | Range.TenFootRadius;
                    break;
                case 6:
                    grantedAttack.Weapon.Range = Range.Shotgun | Range.TenFootRadius;
                    break;
                case 7:
                    grantedAttack.Weapon.Range = Range.SMG | Range.TenFootRadius;
                    break;
                case 8:
                    grantedAttack.Weapon.Range = Range.HeavyRifle | Range.TenFootRadius;
                    break;
                case 9:
                    grantedAttack.Weapon.Range = Range.Thrown | Range.TenFootRadius;
                    break;
                case 10:
                    grantedAttack.Weapon.Range = Range.Bows | Range.TenFootRadius;
                    break;
            }
            grantedAttack.Name = "Natural Area Attack Ability";
            areaAttack.GrantedAttack = grantedAttack;
            areaAttack.Description = "Creature gains an Area attack that has the listed area and damage type.";
            areaAttack.StaminaCost = 5; //should be level + 2
            areaAttack.Tier = 1;
            CreatureAbilities.Add(areaAttack);
            #endregion

            #region Defender
            NpcAbility defender = new NpcAbility();
            defender.Name = "Defender";
            defender.Description = "Once per turn when an adjacent ally is hit by an attack, the creature can take the hit for its ally.  The creature suffers all effects of the attack.";
            defender.StaminaCost = 2;
            defender.Tier = 1;
            defender.UpkeepCost = null;

            CreatureAbilities.Add(defender);
            #endregion

            #region Full Attack
            NpcAbility fullAttack = new NpcAbility();
            fullAttack.Name = "Full Attack";
            fullAttack.Description = "(Attack Augment): The creature’s next Combat Action simultaneously uses all of its Natural Weapons. For each weapon used in this attack the creature suffers a -1 to attack and damage. If a given weapon is Light to the creature, reduce the total penalty by 1. If a given weapon is Two-Handed, increase the total penalty by 2.";
            fullAttack.StaminaCost = 6;
            fullAttack.Tier = 4;
            CreatureAbilities.Add(fullAttack);
            #endregion

            #region Gang Fighter
            NpcAbility gangFighter = new NpcAbility();
            gangFighter.Name = "Gang Fighter";
            gangFighter.Description = "Creature gains a +1 to attack and damage when adjacent to any ally of the same Archetype.";
            gangFighter.Tier = 1;
            CreatureAbilities.Add(gangFighter);
            #endregion

            #region Jumper
            NpcAbility jumper = new NpcAbility();
            jumper.Name = "Jumper";
            jumper.Description = "When the creatuer spends MI to move, it can instead disappear from its current location and appear in a location a number of MI away from where it started.  This distance can be less than or equal to the number of MI spent.  Movement made in this way is not subject to terrain and never creates openings.  The creature must have LOS to the destination to use this ability.";
            jumper.Tier = 1;
            CreatureAbilities.Add(jumper);
            #endregion

            #region Mount
            NpcAbility mount = new NpcAbility();
            mount.Name = "Mount";
            mount.Description = "Creature can be used to bear a rider at least two sizes smaller than it.  A rider must use a quick action to remain mounted on each of its turns.  This quick action allows the rider to spend the mounts MI at this time instead of on the mounts turn.  The rider can also command the mount to allow it to perform other actions.  Separate commands must be issued for each action.  The mount cannot make more than one combat action per turn.";
            mount.Tier = 1;
            CreatureAbilities.Add(mount);
            #endregion

            #region Powerful Attack
            NpcAbility powerfulAttack = new NpcAbility();
            powerfulAttack.Name = "Powerful Attack";
            powerfulAttack.Description = "(Attack Augment): Creature gains its Level to damage and ½ of its Level to attack.";
            powerfulAttack.Tier = 4;
            powerfulAttack.StaminaCost = 6;
            CreatureAbilities.Add(powerfulAttack);
            #endregion

            #region Ranged Attack
            NpcAbility rangedAttack = new Model.NpcAbility();
            rangedAttack.Name = "Ranged Attack";
            NpcWeaponAttack grantedAttack2 = new NpcWeaponAttack();
            grantedAttack2.Weapon = new NaturalWeapon(NaturalWeaponClass.Ranged);
            grantedAttack2.Weapon.Type = (DamageType)Math.Pow(2, r.Next(0, 14));
            switch (r.Next(0, 7))
            {
                case 0:
                    grantedAttack2.Weapon.Range = Range.Pistol;
                    break;
                case 1:
                    grantedAttack2.Weapon.Range = Range.Rifle;
                    break;
                case 2:
                    grantedAttack2.Weapon.Range = Range.Shotgun;
                    break;
                case 3:
                    grantedAttack2.Weapon.Range = Range.SMG;
                    break;
                case 4:
                    grantedAttack2.Weapon.Range = Range.HeavyRifle;
                    break;
                case 5:
                    grantedAttack2.Weapon.Range = Range.Thrown;
                    break;
                case 6:
                    grantedAttack2.Weapon.Range = Range.Bows;
                    break;
            }
            grantedAttack2.Name = "Natural Ranged Attack Ability";
            rangedAttack.GrantedAttack = grantedAttack2;
            rangedAttack.Description = "Creature gains a Ranged attack that has the listed range and damage type.";
            rangedAttack.StaminaCost = 3; //should be level
            rangedAttack.Tier = 1;
            CreatureAbilities.Add(rangedAttack);
            #endregion

            #region Shapechanger
            NpcAbility shapechanger = new NpcAbility();
            shapechanger.Name = "Shapechanger";
            shapechanger.Description = "(Combat Action): Creature can adopt a disguise as part of this action.  Any disguise adopted in this fashion gains a +5 to the roll to determine the MCR to see through the disguise.";
            shapechanger.StaminaCost = 6;
            shapechanger.Tier = 1;
            CreatureAbilities.Add(shapechanger);
            #endregion

            #region Talent
            NpcAbility talentAbility = new NpcAbility();
            talentAbility.Name = "Talent Ability";
            talentAbility.Description = "Trade 1 Ability for 1 Talent";
            CreatureAbilities.Add(talentAbility);
            #endregion

            #region Quality
            NpcAbility qualityAbility = new NpcAbility();
            qualityAbility.Name = "Quality Ability";
            qualityAbility.Description = "Trade 1 Ability for 1 Quality";
            CreatureAbilities.Add(qualityAbility);
            #endregion
        }

        private void InitializeTalents()
        {
            #region Archery (Bows)
            #region T1
            Talent archery1a = new Talent();
            archery1a.Action = ActionType.Combat;
            archery1a.Description = "Weapon {Ranged -2/+4} [4 Stamina]";
            archery1a.DescriptionFluff = "You take a deep breath and skillfully shoot for the heart.";
            archery1a.IsSpell = false;
            archery1a.LinkedAttribute = null;
            archery1a.LinkedSkill = WeaponSkill.Bows;
            archery1a.Name = "Vitals Shot";
            archery1a.StaminaCost = 4;
            archery1a.Tier = 1;
            archery1a.TierBenefitDescription = "Your first arrow fired each round does not have an Increment cost from the Manual Load property";
            archery1a.Tree = TalentTree.Archery;
            archery1a.TreeName = "Archery";
            archery1a.Type = TalentType.Maneuver;
            archery1a.UpkeepCost = null;
            Talents.Add(archery1a);

            Talent archery1b = new Talent();
            archery1b.Action = ActionType.Quick;
            archery1b.Description = "Gain +1 to attack and damage; you cannot spend MI to move.";
            archery1b.DescriptionFluff = "You plant your feet, ready to knock down anyone you see.";
            archery1b.IsSpell = false;
            archery1b.LinkedAttribute = null;
            archery1b.LinkedSkill = WeaponSkill.Bows;
            archery1b.Name = "Archer’s Stance";
            archery1b.StaminaCost = 4;
            archery1b.Tier = 1;
            archery1b.TierBenefitDescription = "Your first arrow fired each round does not have an Increment cost from the Manual Load property";
            archery1b.Tree = TalentTree.Archery;
            archery1b.TreeName = "Archery";
            archery1b.Type = TalentType.Stance;
            archery1b.UpkeepCost = 1;
            Talents.Add(archery1b);

            Talent archery1c = new Talent();
            archery1c.Action = ActionType.None;
            archery1c.Description = "Gain +1 to Perception.";
            archery1c.DescriptionFluff = "Your keen senses give you an even greater edge.";
            archery1c.IsSpell = false;
            archery1c.LinkedAttribute = null;
            archery1c.LinkedSkill = WeaponSkill.Bows;
            archery1c.Name = "Hawkeye";
            archery1c.StaminaCost = null;
            archery1c.Tier = 1;
            archery1c.TierBenefitDescription = "Your first arrow fired each round does not have an Increment cost from the Manual Load property";
            archery1c.Tree = TalentTree.Archery;
            archery1c.TreeName = "Archery";
            archery1c.Type = TalentType.Benefit;
            archery1c.UpkeepCost = null;
            Talents.Add(archery1c);
            #endregion
            #region T2
            Talent archery2a = new Talent();
            archery2a.Action = ActionType.Reaction;
            archery2a.ClarifyingText = "Draw an arrow and make an attack as if wielding a knife, using Bows Skill in place of Close Combat Skill for the attack. The arrow used in the attack is not expended.";
            archery2a.Description = "(Triggered Action: an opponent creates an Opening) Weapon {Melee +1/+1 Agile, Penetrating} [3 Stamina]";
            archery2a.DescriptionFluff = "Your enemy steps the wrong way, and you jab an arrow toward him.";
            archery2a.IsSpell = false;
            archery2a.LinkedAttribute = null;
            archery2a.LinkedSkill = WeaponSkill.Bows;
            archery2a.Name = "Arrow Stab";
            archery2a.StaminaCost = 3;
            archery2a.Tier = 2;
            archery2a.TierBenefitDescription = "+1 damage with Bows Skill";
            archery2a.Tree = TalentTree.Archery;
            archery2a.TreeName = "Archery";
            archery2a.Type = TalentType.TriggeredAction;
            archery2a.UpkeepCost = null;
            Talents.Add(archery2a);

            Talent archery2b = new Talent();
            archery2b.Action = ActionType.Combat;
            archery2b.ClarifyingText = "Make one attack against two different targets. Both targets must be within 30’ of each other.";
            archery2b.Description = "Weapon {Ranged +0/+0} at 2 targets within 30' of eachother [6 Stamina]";
            archery2b.DescriptionFluff = "You move with surprising Speed as you loose two arrows at your enemies.";
            archery2b.IsSpell = false;
            archery2b.LinkedAttribute = null;
            archery2b.LinkedSkill = WeaponSkill.Bows;
            archery2b.Name = "Rapid Shot";
            archery2b.StaminaCost = 6;
            archery2b.Tier = 2;
            archery2b.TierBenefitDescription = "+1 damage with Bows Skill";
            archery2b.Tree = TalentTree.Archery;
            archery2b.TreeName = "Archery";
            archery2b.Type = TalentType.Maneuver;
            archery2b.UpkeepCost = null;
            Talents.Add(archery2b);

            Talent archery2c = new Talent();
            archery2c.Action = ActionType.Reaction;
            archery2a.ClarifyingText = "On a hit, the opponent suffers -2 to attack and damage on the Triggering attack.";
            archery2c.Description = "(Triggered Action: an opponent within range makes a Melee attack against an ally) Weapon {Ranged +0/+0} [3 Stamina]";
            archery2c.DescriptionFluff = "You protect your friends from range.";
            archery2c.IsSpell = false;
            archery2c.LinkedAttribute = null;
            archery2c.LinkedSkill = WeaponSkill.Bows;
            archery2c.Name = "Seize the Moment";
            archery2c.StaminaCost = 3;
            archery2c.Tier = 3;
            archery2c.TierBenefitDescription = "+1 damage with Bows Skill";
            archery2c.Tree = TalentTree.Archery;
            archery2c.TreeName = "Archery";
            archery2c.Type = TalentType.TriggeredAction;
            archery2c.UpkeepCost = null;
            Talents.Add(archery2c);
            #endregion
            #region T3
            Talent archery3a = new Talent();
            archery3a.Action = ActionType.None;
            archery3a.ClarifyingText = "Use Bows Skill in place of Heavy Skill when making attack in this manner.";
            archery3a.Description = "You can wield a Bow as if it were a Staff(+0/+4 Hafted, Defensive 1, Reaching, CM -1).";
            archery3a.DescriptionFluff = "You hold your own in Melee and at range.";
            archery3a.IsSpell = false;
            archery3a.LinkedAttribute = null;
            archery3a.LinkedSkill = WeaponSkill.Bows;
            archery3a.Name = "Give ‘Em the Shaff";
            archery3a.StaminaCost = null;
            archery3a.Tier = 3;
            archery3a.TierBenefitDescription = "Gain +1 CM with Bows";
            archery3a.Tree = TalentTree.Archery;
            archery3a.TreeName = "Archery";
            archery3a.Type = TalentType.Benefit;
            archery3a.UpkeepCost = null;
            Talents.Add(archery3a);

            Talent archery3b = new Talent();
            archery3b.Action = ActionType.Combat;
            archery3b.ClarifyingText = "The attack gains Knockback. If the targets movement causes them to come adjacent to an obstacle the target gets pinned to the object. The target must spend 3 MI unpinning themselves before any MI can be spent to move.";
            archery3b.Description = "Weapon {Ranged +0/+0} [8 Stamina]";
            archery3b.DescriptionFluff = "Your arrow hits with the force of a cannon.";
            archery3b.IsSpell = false;
            archery3b.LinkedAttribute = null;
            archery3b.LinkedSkill = WeaponSkill.Bows;
            archery3b.Name = "Pinning Shot";
            archery3b.StaminaCost = 8;
            archery3b.Tier = 3;
            archery3b.TierBenefitDescription = "Gain +1 CM with Bows";
            archery3b.Tree = TalentTree.Archery;
            archery3b.TreeName = "Archery";
            archery3b.Type = TalentType.Maneuver;
            archery3b.UpkeepCost = null;
            Talents.Add(archery3b);

            Talent archery3c = new Talent();
            archery3c.Action = ActionType.Combat;
            archery3c.ClarifyingText = "Choose a target within line of sight that you have previously struck with a bow attack in this encounter to be automatically hit by this attack. Roll the damage from this attack normally. Crits and Fumbles are not possible on this attack(as an attack roll was not made). This attack circumvents all Cover and Concealment(except Total Cover).";
            archery3c.Description = "Weapon {Ranged (automatic hit)/+0} [8 Stamina]";
            archery3c.DescriptionFluff = "Your aim is truest when following another hit.";
            archery3c.IsSpell = false;
            archery3c.LinkedAttribute = null;
            archery3c.LinkedSkill = WeaponSkill.Bows;
            archery3c.Name = "Split the Arrow";
            archery3c.StaminaCost = 8;
            archery3c.Tier = 3;
            archery3c.TierBenefitDescription = "Gain +1 CM with Bows";
            archery3c.Tree = TalentTree.Archery;
            archery3c.TreeName = "Archery";
            archery3c.Type = TalentType.Maneuver;
            archery3c.UpkeepCost = null;
            Talents.Add(archery3c);
            #endregion
            #region T4
            Talent archery4a = new Talent();
            archery4a.Action = ActionType.Quick;
            archery4a.ClarifyingText = "";
            archery4a.Description = "Reduce all Cover Values by 1 and all Cover Toughnesses by 4 when making bow attacks in this Stance. [10/2 Stamina]";
            archery4a.DescriptionFluff = "Your arrows pass where no one would expect.";
            archery4a.IsSpell = false;
            archery4a.LinkedAttribute = null;
            archery4a.LinkedSkill = WeaponSkill.Bows;
            archery4a.Name = "Bull’s-eye";
            archery4a.StaminaCost = 10;
            archery4a.Tier = 4;
            archery4a.TierBenefitDescription = "Bows wielded by you lose the Ml quality and gain Semi-Auto 2";
            archery4a.Tree = TalentTree.Archery;
            archery4a.TreeName = "Archery";
            archery4a.Type = TalentType.Stance;
            archery4a.UpkeepCost = 2;
            Talents.Add(archery4a);

            Talent archery4b = new Talent();
            archery4b.Action = ActionType.Combat;
            archery4b.ClarifyingText = "Make a Ranged attack against a Thying opponent. On a hit deal normal damage. The target loses the Flight Quality for 1 round. Any fall damage incurred as a result of this Maneuver is halved.";
            archery4b.Description = "Weapon {Ranged +0/+0} [10 Stamina]";
            archery4b.DescriptionFluff = "You can take down a bird with surprising grace.";
            archery4b.IsSpell = false;
            archery4b.LinkedAttribute = null;
            archery4b.LinkedSkill = WeaponSkill.Bows;
            archery4b.Name = "Out of the Sky";
            archery4b.StaminaCost = 10;
            archery4b.Tier = 4;
            archery4b.TierBenefitDescription = "Bows wielded by you lose the Ml quality and gain Semi-Auto 2";
            archery4b.Tree = TalentTree.Archery;
            archery4b.TreeName = "Archery";
            archery4b.Type = TalentType.Maneuver;
            archery4b.UpkeepCost = null;
            Talents.Add(archery4b);

            Talent archery4c = new Talent();
            archery4c.Action = ActionType.None;
            archery4c.ClarifyingText = "This Talent requires 24 hours to apply to a weapon, or to change the weapon to which it applies. This Talent can be taken multiple times, each time applying it to a different weapon in your arsenal.";
            archery4c.Description = "Choose 1 weapon that you own that uses the skill linked to this Talent Tree. You gain an additional Mod Slot in that weapon.";
            archery4c.DescriptionFluff = "Your constant tinkering with your weapon have ffne tuned it to perfection.";
            archery4c.IsSpell = false;
            archery4c.LinkedAttribute = null;
            archery4c.LinkedSkill = WeaponSkill.Bows;
            archery4c.Name = "Custom Weapon";
            archery4c.StaminaCost = null;
            archery4c.Tier = 4;
            archery4c.TierBenefitDescription = "Bows wielded by you lose the Ml quality and gain Semi-Auto 2";
            archery4c.Tree = TalentTree.Archery;
            archery4c.TreeName = "Archery";
            archery4c.Type = TalentType.Benefit;
            archery4c.UpkeepCost = null;
            Talents.Add(archery4c);
            #endregion
            #region T5
            Talent archery5a = new Talent();
            archery5a.Action = ActionType.Combat;
            archery5a.ClarifyingText = "Make one attack against 3 different targets; each one must be within 30’ of the last.";
            archery5a.Description = "Weapon {Ranged +0/+0} [12 Stamina]";
            archery5a.DescriptionFluff = "Your supernatural Speed helps you loose three arrows at your enemies.";
            archery5a.IsSpell = false;
            archery5a.LinkedAttribute = null;
            archery5a.LinkedSkill = WeaponSkill.Bows;
            archery5a.Name = "Triple Attack";
            archery5a.StaminaCost = 12;
            archery5a.Tier = 5;
            archery5a.TierBenefitDescription = "Bow attacks gain the AP quality";
            archery5a.Tree = TalentTree.Archery;
            archery5a.TreeName = "Archery";
            archery5a.Type = TalentType.Maneuver;
            archery5a.UpkeepCost = null;
            Talents.Add(archery5a);

            Talent archery5b = new Talent();
            archery5b.Action = ActionType.Combat;
            archery5b.ClarifyingText = "If the opponent is damaged by the attack, it is Weakened (until Resisted), Vulnerable (until Resisted), and Slowed (until Resisted).";
            archery5b.Description = "Weapon {Ranged +0/+0} [12 Stamina]";
            archery5b.DescriptionFluff = "Your quick aim and concentration take an opponent out of the ffght.";
            archery5b.IsSpell = false;
            archery5b.LinkedAttribute = null;
            archery5b.LinkedSkill = WeaponSkill.Bows;
            archery5b.Name = "Crippling Shot";
            archery5b.StaminaCost = 12;
            archery5b.Tier = 5;
            archery5b.TierBenefitDescription = "Bow attacks gain the AP quality";
            archery5b.Tree = TalentTree.Archery;
            archery5b.TreeName = "Archery";
            archery5b.Type = TalentType.Maneuver;
            archery5b.UpkeepCost = null;
            Talents.Add(archery5b);

            Talent archery5c = new Talent();
            archery5c.Action = ActionType.Combat;
            archery5c.ClarifyingText = "Make an attack against a target that was within line of sight during this encounter. This attack ignores all Concealment and Cover (including Total Cover).";
            archery5c.Description = "Weapon {Ranged +0/+0} [12 Stamina]";
            archery5c.DescriptionFluff = "As if by magic, you hit an opponent you cannot see.";
            archery5c.IsSpell = false;
            archery5c.LinkedAttribute = null;
            archery5c.LinkedSkill = WeaponSkill.Bows;
            archery5c.Name = "Impossible Shot";
            archery5c.StaminaCost = 12;
            archery5c.Tier = 5;
            archery5c.TierBenefitDescription = "Bow attacks gain the AP quality";
            archery5c.Tree = TalentTree.Archery;
            archery5c.TreeName = "Archery";
            archery5c.Type = TalentType.Maneuver;
            archery5c.UpkeepCost = null;
            Talents.Add(archery5c);
            #endregion
            #endregion
            #region Assassination (Close Combat)
            #region T1
            Talent assassination1a = new Talent();
            assassination1a.Action = ActionType.None;
            assassination1a.ClarifyingText = "If the weapon is already Defensive, the bonus applies to all Defenses.";
            assassination1a.Description = "When armed with a knife in the off-hand that you did not attack with during your last turn, that weapon receives a + 1 to its Defensive property.";
            assassination1a.DescriptionFluff = "Your assassin’s knife guards against your enemy.";
            assassination1a.IsSpell = false;
            assassination1a.LinkedAttribute = null;
            assassination1a.LinkedSkill = WeaponSkill.CloseCombat;
            assassination1a.Name = "Main Gauche";
            assassination1a.StaminaCost = null;
            assassination1a.Tier = 1;
            assassination1a.TierBenefitDescription = "+1 damage with Close Combat Weapons";
            assassination1a.Tree = TalentTree.Assassination;
            assassination1a.TreeName = "Assassination";
            assassination1a.Type = TalentType.Benefit;
            assassination1a.UpkeepCost = null;
            Talents.Add(assassination1a);

            Talent assassination1b = new Talent();
            assassination1b.Action = ActionType.Quick;
            assassination1b.ClarifyingText = "You must make a new Stealth Check at a -2 and must have Cover or Concealment to remain hidden.All other creatures in the area get a new Perception Check against the new MCR.";
            assassination1b.Description = "Your next Melee attack does not automatically cause you to lose the hidden state from bystanders (though it does from the target of that attack).";
            assassination1b.DescriptionFluff = "You strike from the shadows, and retreat back into them.";
            assassination1b.IsSpell = false;
            assassination1b.LinkedAttribute = null;
            assassination1b.LinkedSkill = WeaponSkill.CloseCombat;
            assassination1b.Name = "Shadow Strike";
            assassination1b.StaminaCost = 2;
            assassination1b.Tier = 1;
            assassination1b.TierBenefitDescription = "+1 damage with Close Combat Weapons";
            assassination1b.Tree = TalentTree.Assassination;
            assassination1b.TreeName = "Assassination";
            assassination1b.Type = TalentType.AttackAugment;
            assassination1b.UpkeepCost = null;
            Talents.Add(assassination1b);

            Talent assassination1c = new Talent();
            assassination1c.Action = ActionType.None;
            assassination1c.ClarifyingText = "";
            assassination1c.Description = "All Close Combat weapons are considered 2 sizes smaller when determining their Concealment Modifier.";
            assassination1c.DescriptionFluff = "You are adept at hiding your most valued possession.";
            assassination1c.IsSpell = false;
            assassination1c.LinkedAttribute = null;
            assassination1c.LinkedSkill = WeaponSkill.CloseCombat;
            assassination1c.Name = "Hidden Blade";
            assassination1c.StaminaCost = null;
            assassination1c.Tier = 1;
            assassination1c.TierBenefitDescription = "+1 damage with Close Combat Weapons";
            assassination1c.Tree = TalentTree.Assassination;
            assassination1c.TreeName = "Assassination";
            assassination1c.Type = TalentType.Benefit;
            assassination1c.UpkeepCost = null;
            Talents.Add(assassination1c);
            #endregion
            #region T2
            Talent assassination2a = new Talent();
            assassination2a.Action = ActionType.Combat;
            assassination2a.ClarifyingText = "Make 2 attacks against the same target.";
            assassination2a.Description = "Weapon {Melee +0/+0} [6 Stamina]";
            assassination2a.DescriptionFluff = "Your quickness allows you to gouge your opponent again.";
            assassination2a.IsSpell = false;
            assassination2a.LinkedAttribute = null;
            assassination2a.LinkedSkill = WeaponSkill.CloseCombat;
            assassination2a.Name = "Double Cut";
            assassination2a.StaminaCost = 6;
            assassination2a.Tier = 2;
            assassination2a.TierBenefitDescription = "+1 CM with Close Combat weapons";
            assassination2a.Tree = TalentTree.Assassination;
            assassination2a.TreeName = "Assassination";
            assassination2a.Type = TalentType.Maneuver;
            assassination2a.UpkeepCost = null;
            Talents.Add(assassination2a);

            Talent assassination2b = new Talent();
            assassination2b.Action = ActionType.None;
            assassination2b.ClarifyingText = "";
            assassination2b.Description = "You gain a +2 to Poison damage and the ongoing Poison damage caused by Crits.";
            assassination2b.DescriptionFluff = "Your knives become all the more deadly.";
            assassination2b.IsSpell = false;
            assassination2b.LinkedAttribute = null;
            assassination2b.LinkedSkill = WeaponSkill.CloseCombat;
            assassination2b.Name = "Poison Training";
            assassination2b.StaminaCost = null;
            assassination2b.Tier = 2;
            assassination2b.TierBenefitDescription = "+1 CM with Close Combat weapons";
            assassination2b.Tree = TalentTree.Assassination;
            assassination2b.TreeName = "Assassination";
            assassination2b.Type = TalentType.Benefit;
            assassination2b.UpkeepCost = null;
            Talents.Add(assassination2b);

            Talent assassination2c = new Talent();
            assassination2c.Action = ActionType.None;
            assassination2c.ClarifyingText = "If you draw and attack with the weapon in the 1st round of combat, gain a + 2 to attack and damage with the 1st attack you make.";
            assassination2c.Description = "When you draw a concealed Close Combat weapon, do so as if the weapon were holstered.";
            assassination2c.DescriptionFluff = "Your blade is ever at the ready.";
            assassination2c.IsSpell = false;
            assassination2c.LinkedAttribute = null;
            assassination2c.LinkedSkill = WeaponSkill.CloseCombat;
            assassination2c.Name = "Palmed Blade";
            assassination2c.StaminaCost = null;
            assassination2c.Tier = 2;
            assassination2c.TierBenefitDescription = "+1 CM with Close Combat weapons";
            assassination2c.Tree = TalentTree.Assassination;
            assassination2c.TreeName = "Assassination";
            assassination2c.Type = TalentType.Benefit;
            assassination2c.UpkeepCost = null;
            Talents.Add(assassination2c);
            #endregion
            #region T3
            Talent assassination3a = new Talent();
            assassination3a.Action = ActionType.Combat;
            assassination3a.ClarifyingText = "Make an attack with a Close Combat weapon. On a hit, the attack is considered 1 Stage Crit higher than is indicated by the roll.";
            assassination3a.Description = "Weapon {Melee +0/+0} [8 Stamina]";
            assassination3a.DescriptionFluff = "You go for the jugular, and slice more deeply than even you expected.";
            assassination3a.IsSpell = false;
            assassination3a.LinkedAttribute = null;
            assassination3a.LinkedSkill = WeaponSkill.CloseCombat;
            assassination3a.Name = "Arterial Cut";
            assassination3a.StaminaCost = 8;
            assassination3a.Tier = 3;
            assassination3a.TierBenefitDescription = "You Check at the end of your turn(instead of at the end of your movement) to determine if you lose your hidden state from moving out of Cover or Concealment";
            assassination3a.Tree = TalentTree.Assassination;
            assassination3a.TreeName = "Assassination";
            assassination3a.Type = TalentType.Maneuver;
            assassination3a.UpkeepCost = null;
            Talents.Add(assassination3a);

            Talent assassination3b = new Talent();
            assassination3b.Action = ActionType.Quick;
            assassination3b.ClarifyingText = "";
            assassination3b.Description = "Your next Melee attack gains the Armor Piercing quality. [4 Stamina]";
            assassination3b.DescriptionFluff = "Even a knight is vulnerable to your blade.";
            assassination3b.IsSpell = false;
            assassination3b.LinkedAttribute = null;
            assassination3b.LinkedSkill = WeaponSkill.CloseCombat;
            assassination3b.Name = "Find the Gap";
            assassination3b.StaminaCost = 4;
            assassination3b.Tier = 3;
            assassination3b.TierBenefitDescription = "You Check at the end of your turn(instead of at the end of your movement) to determine if you lose your hidden state from moving out of Cover or Concealment";
            assassination3b.Tree = TalentTree.Assassination;
            assassination3b.TreeName = "Assassination";
            assassination3b.Type = TalentType.AttackAugment;
            assassination3b.UpkeepCost = null;
            Talents.Add(assassination3b);

            Talent assassination3c = new Talent();
            assassination3c.Action = ActionType.None;
            assassination3c.ClarifyingText = "";
            assassination3c.Description = "You can re-roll all 1s on Stealth and Perception checks.";
            assassination3c.DescriptionFluff = "You are one with the shadows.";
            assassination3c.IsSpell = false;
            assassination3c.LinkedAttribute = null;
            assassination3c.LinkedSkill = WeaponSkill.CloseCombat;
            assassination3c.Name = "Stalker";
            assassination3c.StaminaCost = null;
            assassination3c.Tier = 3;
            assassination3c.TierBenefitDescription = "You Check at the end of your turn(instead of at the end of your movement) to determine if you lose your hidden state from moving out of Cover or Concealment";
            assassination3c.Tree = TalentTree.Assassination;
            assassination3c.TreeName = "Assassination";
            assassination3c.Type = TalentType.Benefit;
            assassination3c.UpkeepCost = null;
            Talents.Add(assassination3c);
            #endregion
            #region T4
            Talent assassination4a = new Talent();
            assassination4a.Name = "Assassin’s Strike";
            assassination4a.Type = TalentType.Maneuver;
            assassination4a.Action = ActionType.Combat;
            assassination4a.DescriptionFluff = "You use your opponent’s weakness to cause additional pain.";
            assassination4a.Description = "Weapon {Melee +0/+Agility} [10 Stamina]";
            assassination4a.ClarifyingText = "Make a Melee attack against a Surprised, Vulnerable, or Unaware opponent. Gain a +2 CM and Lethal + 1.";
            assassination4a.LinkedSkill = WeaponSkill.CloseCombat;
            assassination4a.StaminaCost = 10;
            assassination4a.Tier = 4;
            assassination4a.TierBenefitDescription = "All attacks with Close Combat weapons gain Lethal + 1 vs.Surprised and unaware opponents";
            assassination4a.Tree = TalentTree.Assassination;
            assassination4a.TreeName = "Assassination";
            assassination4a.UpkeepCost = null;
            Talents.Add(assassination4a);

            Talent assassination4b = new Talent();
            assassination4b.Name = "Flurry of Strikes";
            assassination4b.Type = TalentType.Maneuver;
            assassination4b.Action = ActionType.Combat;
            assassination4b.DescriptionFluff = "Your hands move too quickly to comprehend.";
            assassination4b.Description = "Weapon {Melee +0/+0} [8 Stamina]";
            assassination4b.ClarifyingText = "Make 3 attacks against the same target.";
            assassination4b.LinkedSkill = WeaponSkill.CloseCombat;
            assassination4b.StaminaCost = 8;
            assassination4b.Tier = 4;
            assassination4b.TierBenefitDescription = "All attacks with Close Combat weapons gain Lethal + 1 vs.Surprised and unaware opponents";
            assassination4b.Tree = TalentTree.Assassination;
            assassination4b.TreeName = "Assassination";
            assassination4b.UpkeepCost = null;
            Talents.Add(assassination4b);

            Talent assassination4c = new Talent();
            assassination4c.Name = "Poison Master";
            assassination4c.Type = TalentType.Benefit;
            assassination4c.Action = ActionType.None;
            assassination4c.DescriptionFluff = "Poison is your knife’s best friend.";
            assassination4c.Description = "Gain a +2 to Durability vs. Poison and on Resistance Checks caused by Poison attacks. The MCR for Resistance Checks vs.your Poison attacks are +2.Your Poison attacks gain Lethal +1.";
            assassination4c.ClarifyingText = "";
            assassination4c.LinkedSkill = WeaponSkill.CloseCombat;
            assassination4c.StaminaCost = null;
            assassination4c.Tier = 4;
            assassination4c.TierBenefitDescription = "All attacks with Close Combat weapons gain Lethal + 1 vs.Surprised and unaware opponents";
            assassination4c.Tree = TalentTree.Assassination;
            assassination4c.TreeName = "Assassination";
            assassination4c.UpkeepCost = null;
            Talents.Add(assassination4c);
            #endregion
            #region T5
            Talent assassination5a = new Talent();
            assassination5a.Name = "Shadow";
            assassination5a.Type = TalentType.Stance;
            assassination5a.Action = ActionType.Quick;
            assassination5a.DescriptionFluff = "You take advantage of every nook and cranny you ffnd.";
            assassination5a.Description = "";
            assassination5a.ClarifyingText = "While in this stance, you gain Heavy Concealment and any time you move at least 3 MI you can make a Stealth Check to become hidden.As usual you lose the Hidden state aff er resolving an attack or other action that would make you lose your Cover or Concealment.";
            assassination5a.LinkedSkill = WeaponSkill.CloseCombat;
            assassination5a.StaminaCost = 12;
            assassination5a.Tier = 5;
            assassination5a.TierBenefitDescription = "Add Agility and Strength to damage of Close Combat weapons";
            assassination5a.Tree = TalentTree.Assassination;
            assassination5a.TreeName = "Assassination";
            assassination5a.UpkeepCost = 3;
            Talents.Add(assassination5a);

            Talent assassination5b = new Talent();
            assassination5b.Name = "Coup de Grace";
            assassination5b.Type = TalentType.AttackAugment;
            assassination5b.Action = ActionType.Quick;
            assassination5b.DescriptionFluff = "You deliver a killing blow to your opponent.";
            assassination5b.Description = "If the augmented attack is against a Surprised, Vulnerable, or Unaware opponent, The attack gains +6 CM.";
            assassination5b.ClarifyingText = "";
            assassination5b.LinkedSkill = WeaponSkill.CloseCombat;
            assassination5b.StaminaCost = 6;
            assassination5b.Tier = 5;
            assassination5b.TierBenefitDescription = "Add Agility and Strength to damage of Close Combat weapons";
            assassination5b.Tree = TalentTree.Assassination;
            assassination5b.TreeName = "Assassination";
            assassination5b.UpkeepCost = null;
            Talents.Add(assassination5b);

            Talent assassination5c = new Talent();
            assassination5c.Name = "Assassin’s Touch";
            assassination5c.Type = TalentType.Benefit;
            assassination5c.Action = ActionType.None;
            assassination5c.DescriptionFluff = "Even your deadliest strikes are silent.";
            assassination5c.Description = "When you kill an opponent with a Melee attack, that opponent cannot make noise of any kind, and you remain Hidden from all enemies you still have Concealment or Cover from.";
            assassination5c.ClarifyingText = "";
            assassination5c.LinkedSkill = WeaponSkill.CloseCombat;
            assassination5c.StaminaCost = null;
            assassination5c.UpkeepCost = null;
            assassination5c.Tier = 5;
            assassination5c.TierBenefitDescription = "Add Agility and Strength to damage of Close Combat weapons";
            assassination5c.Tree = TalentTree.Assassination;
            assassination5c.TreeName = "Assassination";
            Talents.Add(assassination5c);
            #endregion
            #endregion
            //TODO: Finish Automatics at some point.. Fuck NPCs with auto talents anyways
            #region Automatics (Longarms)
            #region T1
            Talent automatics1a = new Talent();
            automatics1a.Name = "Tactical Crouch";
            automatics1a.Type = TalentType.Stance;
            automatics1a.Action = ActionType.Quick;
            automatics1a.DescriptionFluff = "You take on a defensive Stance that does not impair your aim.";
            automatics1a.Description = "+1 to Ranged and Area Defenses. [4/1 Stamina]";
            automatics1a.ClarifyingText = "";
            automatics1a.StaminaCost = 4;
            automatics1a.UpkeepCost = 1;
            automatics1a.Tier = 1;
            automatics1a.TierBenefitDescription = "You may choose the number of bullets used in a Full-Auto attack up to the maximum rate of fire";
            automatics1a.Tree = TalentTree.Automatics;
            automatics1a.TreeName = "Automatics";
            automatics1a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics1a);

            Talent automatics1b = new Talent();
            automatics1b.Name = "Spray and Pray";
            automatics1b.Type = TalentType.AttackAugment;
            automatics1b.Action = ActionType.Quick;
            automatics1b.DescriptionFluff = "You use Full-Auto to its fullest benefft.";
            automatics1b.Description = "Your next Full-Auto attack can cross 5 additional feet between targets without allocating groupings to open space. [2 Stamina]";
            automatics1b.ClarifyingText = "";
            automatics1b.StaminaCost = 2;
            automatics1b.UpkeepCost = null;
            automatics1b.Tier = 1;
            automatics1b.TierBenefitDescription = "You may choose the number of bullets used in a Full-Auto attack up to the maximum rate of fire";
            automatics1b.Tree = TalentTree.Automatics;
            automatics1b.TreeName = "Automatics";
            automatics1b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics1b);

            Talent automatics1c = new Talent();
            automatics1c.Name = "Recoil Compensation";
            automatics1c.Type = TalentType.AttackAugment;
            automatics1c.Action = ActionType.Quick;
            automatics1c.DescriptionFluff = "You steady yourself to absorb as much recoil as possible.";
            automatics1c.Description = "Use one less round to produce a Full-Auto Grouping. [1 Stamina per grouping fired]";
            automatics1c.ClarifyingText = "";
            automatics1c.StaminaCost = 1;
            automatics1c.UpkeepCost = null;
            automatics1c.Tier = 1;
            automatics1c.TierBenefitDescription = "You may choose the number of bullets used in a Full-Auto attack up to the maximum rate of fire";
            automatics1c.Tree = TalentTree.Automatics;
            automatics1c.TreeName = "Automatics";
            automatics1c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics1c);
            #endregion
            #region T2
            Talent automatics2a = new Talent();
            automatics2a.Name = "Frightening Suppression";
            automatics2a.Type = TalentType.AttackAugment;
            automatics2a.Action = ActionType.Quick;
            automatics2a.DescriptionFluff = "The intensity of your suppression surprises your targets.";
            automatics2a.Description = "Targets of the augmented Suppressive Fire attack become Vulnerable to the next attack against them.";
            automatics2a.ClarifyingText = "";
            automatics2a.StaminaCost = 2;
            automatics2a.UpkeepCost = null;
            automatics2a.Tier = 2;
            automatics2a.TierBenefitDescription = "Ignore a single 5-foot space between targets of a Full-Auto attack";
            automatics2a.Tree = TalentTree.Automatics;
            automatics2a.TreeName = "Automatics";
            automatics2a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics2a);

            Talent automatics2b = new Talent();
            automatics2b.Name = "Wide Spray";
            automatics2b.Type = TalentType.AttackAugment;
            automatics2b.Action = ActionType.Quick;
            automatics2b.DescriptionFluff = "You widen your ffeld of ffre to encapsulate your opponent with lead.";
            automatics2b.Description = "The augmented Full-Auto attack gains a +1 to attack but suffers a -1 to damage and CM. [2 Stamina]";
            automatics2b.ClarifyingText = "'How the fuck is this widening your field of fire?'-Neums";
            automatics2b.StaminaCost = 2;
            automatics2b.UpkeepCost = null;
            automatics2b.Tier = 2;
            automatics2b.TierBenefitDescription = "Ignore a single 5-foot space between targets of a Full-Auto attack";
            automatics2b.Tree = TalentTree.Automatics;
            automatics2b.TreeName = "Automatics";
            automatics2b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics2b);

            Talent automatics2c = new Talent();
            automatics2c.Name = "Bump Fire";
            automatics2c.Type = TalentType.Benefit;
            automatics2c.Action = ActionType.None;
            automatics2c.DescriptionFluff = "You feather the trigger to conserve ammunition.";
            automatics2c.Description = "You gain the ability to use Burst Fire (4) on Longarm or Shortarm Full-Auto weapons.";
            automatics2c.ClarifyingText = "";
            automatics2c.StaminaCost = null;
            automatics2c.UpkeepCost = null;
            automatics2c.Tier = 2;
            automatics2c.TierBenefitDescription = "Ignore a single 5-foot space between targets of a Full-Auto attack";
            automatics2c.Tree = TalentTree.Automatics;
            automatics2c.TreeName = "Automatics";
            automatics2c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics2c);
            #endregion
            #region T3
            Talent automatics3a = new Talent();
            automatics3a.Name = "Withering Suppression";
            automatics3a.Type = TalentType.AttackAugment;
            automatics3a.Action = ActionType.Quick;
            automatics3a.DescriptionFluff = "You target the terrain with your suppressive ffre to break it apart.";
            automatics3a.Description = "At the conclusion of your next suppression or Full-Auto attack, reduce the Cover Value of all obstructions in the area by one Grade.";
            automatics3a.ClarifyingText = "-2 or dash => 6";
            automatics3a.StaminaCost = 4;
            automatics3a.UpkeepCost = null;
            automatics3a.Tier = 3;
            automatics3a.TierBenefitDescription = "When you target only one creature with a Full-Auto attack, the attack gains Vicious +2";
            automatics3a.Tree = TalentTree.Automatics;
            automatics3a.TreeName = "Automatics";
            automatics3a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(automatics3a);
            #endregion
            #endregion
            #region Automatics (Shortarms)
            //TODO: Copy
            #endregion
            #region Brawling (Unarmed)
            #region T1
            Talent brawling1a = new Talent();
            brawling1a.Name = "Combat Adaptability";
            brawling1a.Type = TalentType.Benefit;
            brawling1a.Action = ActionType.None;
            brawling1a.DescriptionFluff = "Chair, lamp, screwdriver—everything is a weapon to you.";
            brawling1a.Description = "+1 to attack and damage with Improvised attacks. Can use Unarmed Skill to make Improvised attacks.";
            brawling1a.ClarifyingText = "";
            brawling1a.StaminaCost = null;
            brawling1a.UpkeepCost = null;
            brawling1a.Tier = 1;
            brawling1a.TierBenefitDescription = "+1 to Durability against Melee attacks";
            brawling1a.Tree = TalentTree.Brawling;
            brawling1a.TreeName = "Brawling";
            brawling1a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling1a);

            Talent brawling1b = new Talent();
            brawling1b.Name = "Headbutt";
            brawling1b.Type = TalentType.Maneuver;
            brawling1b.Action = ActionType.Combat;
            brawling1b.DescriptionFluff = "You smash your forehead into the face of your opponent.";
            brawling1b.Description = "Weapon {Melee +0/+0} [4 Stamina]";
            brawling1b.ClarifyingText = "On a hit the target becomes Dazed for 1 round in addition to suffering normal damage for the attack. You gain a + 1 to hit with this Maneuver against any opponent you are Wrestling.";
            brawling1b.StaminaCost = 4;
            brawling1b.UpkeepCost = null;
            brawling1b.Tier = 1;
            brawling1b.TierBenefitDescription = "+1 to Durability against Melee attacks";
            brawling1b.Tree = TalentTree.Brawling;
            brawling1b.TreeName = "Brawling";
            brawling1b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling1b);

            Talent brawling1c = new Talent();
            brawling1c.Name = "Foot Stomp";
            brawling1c.Type = TalentType.Trick;
            brawling1c.Action = ActionType.Quick;
            brawling1c.DescriptionFluff = "Crushing the arc of someones foot can severely impair their ability to ffght.";
            brawling1c.Description = "Unarmed {Melee +0/+0} [4 Stamina]";
            brawling1c.ClarifyingText = "Make an Unarmed attack; if the opponent would have taken damage from it, it instead becomes Slowed and Vulnerable(until Resisted). Crits increase the Resistance MCR by 1, 2, or 4, depending on Crit Stage. This attack causes no damage.";
            brawling1c.StaminaCost = 4;
            brawling1c.UpkeepCost = null;
            brawling1c.Tier = 1;
            brawling1c.TierBenefitDescription = "+1 to Durability against Melee attacks";
            brawling1c.Tree = TalentTree.Brawling;
            brawling1c.TreeName = "Brawling";
            brawling1c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling1c);
            #endregion
            #region T2
            Talent brawling2a = new Talent();
            brawling2a.Name = "Boxer’s Stance";
            brawling2a.Type = TalentType.Stance;
            brawling2a.Action = ActionType.Quick;
            brawling2a.DescriptionFluff = "You take an aggressive Stance that focuses on crushing power.";
            brawling2a.Description = "You receive a -2 to attack and a +4 to damage while making Unarmed attacks. You also gain a +1 CM of Unarmed attacks. [6/2 Stamina]";
            brawling2a.ClarifyingText = "";
            brawling2a.StaminaCost = 6;
            brawling2a.UpkeepCost = 2;
            brawling2a.Tier = 2;
            brawling2a.TierBenefitDescription = "+1 to Unarmed damage";
            brawling2a.Tree = TalentTree.Brawling;
            brawling2a.TreeName = "Brawling";
            brawling2a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling2a);

            Talent brawling2b = new Talent();
            brawling2b.Name = "Wicked Hook";
            brawling2b.Type = TalentType.Maneuver;
            brawling2b.Action = ActionType.Combat;
            brawling2b.DescriptionFluff = "You throw a wide swing meant to deliver as much force as possible.";
            brawling2b.Description = "Weapon {Melee +0/+Strength} [6 Stamina]";
            brawling2b.ClarifyingText = "Increase the CM of the attack by 2. On a Crit, target is also knocked Prone.";
            brawling2b.StaminaCost = 6;
            brawling2b.UpkeepCost = null;
            brawling2b.Tier = 2;
            brawling2b.TierBenefitDescription = "+1 to Unarmed damage";
            brawling2b.Tree = TalentTree.Brawling;
            brawling2b.TreeName = "Brawling";
            brawling2b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling2b);

            Talent brawling2c = new Talent();
            brawling2c.Name = "Bustin’ Heads";
            brawling2c.Type = TalentType.Benefit;
            brawling2c.Action = ActionType.None;
            brawling2c.DescriptionFluff = "Many bar brawls have sharpened your eff ectiveness with potential weapons lying around.";
            brawling2c.Description = "Your Improvised Melee attacks gain Vicious 2 and +1 CM.";
            brawling2c.ClarifyingText = "";
            brawling2c.StaminaCost = null;
            brawling2c.UpkeepCost = null;
            brawling2c.Tier = 2;
            brawling2c.TierBenefitDescription = "+1 to Unarmed damage";
            brawling2c.Tree = TalentTree.Brawling;
            brawling2c.TreeName = "Brawling";
            brawling2c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling2c);
            #endregion
            #region T3
            Talent brawling3a = new Talent();
            brawling3a.Name = "Throat Punch";
            brawling3a.Type = TalentType.Maneuver;
            brawling3a.Action = ActionType.Combat;
            brawling3a.DescriptionFluff = "A ffst to the throat leaves your opponent in terrible pain.";
            brawling3a.Description = "Weapon {Melee +0/+0} [8 Stamina]";
            brawling3a.ClarifyingText = "This attack gains the Armor Piercing property, and targets damaged by this attack become Vulnerable and Weakened (until Resisted). Crits increase the Resistance MCR by 1, 2, or 4 depending on Stage.";
            brawling3a.StaminaCost = 8;
            brawling3a.UpkeepCost = null;
            brawling3a.Tier = 3;
            brawling3a.TierBenefitDescription = "+1 CM of Unarmed attacks";
            brawling3a.Tree = TalentTree.Brawling;
            brawling3a.TreeName = "Brawling";
            brawling3a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling3a);

            Talent brawling3b = new Talent();
            brawling3b.Name = "Iron Knuckles";
            brawling3b.Type = TalentType.Benefit;
            brawling3b.Action = ActionType.None;
            brawling3b.DescriptionFluff = "Your rough and calloused knuckles are hard as iron.";
            brawling3b.Description = "Your Unarmed attacks gain the Blasting property.";
            brawling3b.ClarifyingText = "Blasting = +2 dmg vs rigid or no armor.";
            brawling3b.StaminaCost = null;
            brawling3b.UpkeepCost = null;
            brawling3b.Tier = 3;
            brawling3b.TierBenefitDescription = "+1 CM of Unarmed attacks";
            brawling3b.Tree = TalentTree.Brawling;
            brawling3b.TreeName = "Brawling";
            brawling3b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling3b);

            Talent brawling3c = new Talent();
            brawling3c.Name = "Cover Up";
            brawling3c.Type = TalentType.Stance;
            brawling3c.Action = ActionType.Quick;
            brawling3c.DescriptionFluff = "Being no stranger to a beating, you have learned to endure the worst when needed.";
            brawling3c.Description = "Gain Girded against Melee attacks and +2 Durability against Unarmed attacks. [8/2 Stamina]";
            brawling3c.ClarifyingText = "Girded = +2 to Durability and +1 to all Defenses";
            brawling3c.StaminaCost = 8;
            brawling3c.UpkeepCost = 2;
            brawling3c.Tier = 3;
            brawling3c.TierBenefitDescription = "+1 CM of Unarmed attacks";
            brawling3c.Tree = TalentTree.Brawling;
            brawling3c.TreeName = "Brawling";
            brawling3c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling3c);
            #endregion
            #region T4
            Talent brawling4a = new Talent();
            brawling4a.Name = "Wind Up";
            brawling4a.Type = TalentType.AttackAugment;
            brawling4a.Action = ActionType.Quick;
            brawling4a.DescriptionFluff = "Your telegraphed punch is risky, but it might be worth it.";
            brawling4a.Description = "The augmented attack is made at -1 to attack and a +4 to damage. [5 Stamina]";
            brawling4a.ClarifyingText = "";
            brawling4a.StaminaCost = 5;
            brawling4a.UpkeepCost = null;
            brawling4a.Tier = 4;
            brawling4a.TierBenefitDescription = "+2 to Resistance Check for all Conditions caused by Unarmed attacks";
            brawling4a.Tree = TalentTree.Brawling;
            brawling4a.TreeName = "Brawling";
            brawling4a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling4a);

            Talent brawling4b = new Talent();
            brawling4b.Name = "Vitals Strike";
            brawling4b.Type = TalentType.Maneuver;
            brawling4b.Action = ActionType.Combat;
            brawling4b.DescriptionFluff = "You deliver a crushing blow to a part of the body often considered off limits to more civilized ffghters.";
            brawling4b.Description = "Weapon {Melee +0/+0} [10 Stamina]";
            brawling4b.ClarifyingText = "If the augmented attack is made against a Vulnerable target, any damage that exceeds the targets Durability is doubled.  On a hit, the target becomes Vulnerable(until Resisted).";
            brawling4b.StaminaCost = 10;
            brawling4b.UpkeepCost = null;
            brawling4b.Tier = 4;
            brawling4b.TierBenefitDescription = "+2 to Resistance Check for all Conditions caused by Unarmed attacks";
            brawling4b.Tree = TalentTree.Brawling;
            brawling4b.TreeName = "Brawling";
            brawling4b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling4b);

            Talent brawling4c = new Talent();
            brawling4c.Name = "Reciprocity";
            brawling4c.Type = TalentType.Benefit;
            brawling4c.Action = ActionType.None;
            brawling4c.DescriptionFluff = "The harder they hit you, the angrier you become.";
            brawling4c.Description = "Gain a +1 to attack and damage against anyone that caused damage against you in Melee since your last turn.";
            brawling4c.ClarifyingText = "";
            brawling4c.StaminaCost = null;
            brawling4c.UpkeepCost = null;
            brawling4c.Tier = 4;
            brawling4c.TierBenefitDescription = "+2 to Resistance Check for all Conditions caused by Unarmed attacks";
            brawling4c.Tree = TalentTree.Brawling;
            brawling4c.TreeName = "Brawling";
            brawling4c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling4c);
            #endregion
            #region T5
            Talent brawling5a = new Talent();
            brawling5a.Name = "Curb Kick";
            brawling5a.Type = TalentType.AttackAugment;
            brawling5a.Action = ActionType.Quick;
            brawling5a.DescriptionFluff = "You’re not above making someone bite the curb.";
            brawling5a.Description = "If the augmented attack is against a Prone target, the attack gains Lethal 1, +2 CM, and the attack is considered 1 higher Stage Crit than the roll would indicate. [6 Stamina]";
            brawling5a.ClarifyingText = "";
            brawling5a.StaminaCost = 6;
            brawling5a.UpkeepCost = null;
            brawling5a.Tier = 5;
            brawling5a.TierBenefitDescription = "+1 to damage, CM with Unarmed attacks and Durability against Unarmed attacks";
            brawling5a.Tree = TalentTree.Brawling;
            brawling5a.TreeName = "Brawling";
            brawling5a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling5a);

            Talent brawling5b = new Talent();
            brawling5b.Name = "Smash";
            brawling5b.Type = TalentType.Maneuver;
            brawling5b.Action = ActionType.Combat;
            brawling5b.DescriptionFluff = "You’re giving it all you’ve got.";
            brawling5b.Description = "Make an Improvised Melee attack. Double the total damage roll for this attack (roll and modifier). The weapon you use to make the attack is destroyed during the attack.";
            brawling5b.ClarifyingText = "'I'm not sure we considered the consequences of being able to instantly destroy anything that's not a weapon.  Or did we?'-Neums";
            brawling5b.StaminaCost = 12;
            brawling5b.UpkeepCost = null;
            brawling5b.Tier = 5;
            brawling5b.TierBenefitDescription = "+1 to damage, CM with Unarmed attacks and Durability against Unarmed attacks";
            brawling5b.Tree = TalentTree.Brawling;
            brawling5b.TreeName = "Brawling";
            brawling5b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling5b);

            Talent brawling5c = new Talent();
            brawling5c.Name = "Gimme That";
            brawling5c.Type = TalentType.TriggeredAction;
            brawling5c.Action = ActionType.Reaction;
            brawling5c.DescriptionFluff = "That attack was laughable.";
            brawling5c.Description = "Triggered Action: you are attacked in Melee and do not take damage. Weapon {Melee +2/+4) [10 Stamina]";
            brawling5c.ClarifyingText = "On a hit you grab the weapon used to Trigger this attack. At your option, you can either toss the weapon 20’ in any direction, or you can become armed with the weapon.";
            brawling5c.StaminaCost = 10;
            brawling5c.UpkeepCost = null;
            brawling5c.Tier = 5;
            brawling5c.TierBenefitDescription = "+1 to damage, CM with Unarmed attacks and Durability against Unarmed attacks";
            brawling5c.Tree = TalentTree.Brawling;
            brawling5c.TreeName = "Brawling";
            brawling5c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(brawling5c);
            #endregion
            #endregion
            #region Brutality (Heavy)
            #region T1
            Talent brutality1a = new Talent();
            brutality1a.Name = "Bowling Strike";
            brutality1a.Type = TalentType.Maneuver;
            brutality1a.Action = ActionType.Combat;
            brutality1a.DescriptionFluff = "You are the sling; your enemy is the stone.";
            brutality1a.Description = "Weapon {Melee +0/+0} [4 Stamina]";
            brutality1a.ClarifyingText = "Aft er moving at least 3 MI, make this attack while in All-Out Stance, Brute Stance, or Berserker Stance. This attack gains Knockback.";
            brutality1a.StaminaCost = 4;
            brutality1a.UpkeepCost = null;
            brutality1a.Tier = 1;
            brutality1a.TierBenefitDescription = "+1 Vicious with Heavy Melee weapons";
            brutality1a.Tree = TalentTree.Brutality;
            brutality1a.TreeName = "Brutality";
            brutality1a.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality1a);

            Talent brutality1b = new Talent();
            brutality1b.Name = "Low Blow";
            brutality1b.Type = TalentType.TriggeredAction;
            brutality1b.Action = ActionType.Reaction;
            brutality1b.DescriptionFluff = "You capitalize on your own mistake.";
            brutality1b.Description = "(Triggered Action: you miss with a Melee weapon) Unarmed {Melee +0/+2} [2 Stamina]";
            brutality1b.ClarifyingText = "Make this attack against the missed target. 'Whoever put this talent at tier 1 for 2 stamina alongside Bowling Strike has no concept of power level'-Neums";
            brutality1b.StaminaCost = 2;
            brutality1b.UpkeepCost = null;
            brutality1b.Tier = 1;
            brutality1b.TierBenefitDescription = "+1 Vicious with Heavy Melee weapons";
            brutality1b.Tree = TalentTree.Brutality;
            brutality1b.TreeName = "Brutality";
            brutality1b.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality1b);

            Talent brutality1c = new Talent();
            brutality1c.Name = "Pommel Strike";
            brutality1c.Type = TalentType.Maneuver;
            brutality1c.Action = ActionType.Combat;
            brutality1c.DescriptionFluff = "You smash your foe in the face with the pommel of your weapon, staggering it.";
            brutality1c.Description = "Weapon {Melee +0/Bludgeoning +0} [4 Stamina]";
            brutality1c.ClarifyingText = "If damaged by this attack, target is also Weakened (until Resisted).";
            brutality1c.StaminaCost = 4;
            brutality1c.UpkeepCost = null;
            brutality1c.Tier = 1;
            brutality1c.TierBenefitDescription = "+1 Vicious with Heavy Melee weapons";
            brutality1c.Tree = TalentTree.Brutality;
            brutality1c.TreeName = "Brutality";
            brutality1c.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality1c);
            #endregion
            #region T2
            Talent brutality2a = new Talent();
            brutality2a.Name = "Brute Stance";
            brutality2a.Type = TalentType.Stance;
            brutality2a.Action = ActionType.Quick;
            brutality2a.DescriptionFluff = "You lose yourself in the chaos of battle.";
            brutality2a.Description = "You gain the Enraged condition as well as a +2 to Melee damage with weapons held in 2 hands. [6/2 Stamina]";
            brutality2a.ClarifyingText = "Enraged = +2 melee damage, +2 durability, +2 Str non-combat skill checks.";
            brutality2a.StaminaCost = 6;
            brutality2a.UpkeepCost = 2;
            brutality2a.Tier = 2;
            brutality2a.TierBenefitDescription = "+1 to damage of Heavy Melee weapons";
            brutality2a.Tree = TalentTree.Brutality;
            brutality2a.TreeName = "Brutality";
            brutality2a.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality2a);

            Talent brutality2b = new Talent();
            brutality2b.Name = "Powerful Swing";
            brutality2b.Type = TalentType.Benefit;
            brutality2b.Action = ActionType.None;
            brutality2b.DescriptionFluff = "The strength of your swing throws your foes oft balance.";
            brutality2b.Description = "When you miss on an attack with a weapon wielded in 2 hands, the target of the attack becomes Vulnerable to the next attack made against it.";
            brutality2b.ClarifyingText = "";
            brutality2b.StaminaCost = null;
            brutality2b.UpkeepCost = null;
            brutality2b.Tier = 2;
            brutality2b.TierBenefitDescription = "+1 to damage of Heavy Melee weapons";
            brutality2b.Tree = TalentTree.Brutality;
            brutality2b.TreeName = "Brutality";
            brutality2b.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality2b);

            Talent brutality2c = new Talent();
            brutality2c.Name = "Brutal Strike";
            brutality2c.Type = TalentType.Maneuver;
            brutality2c.Action = ActionType.Combat;
            brutality2c.DescriptionFluff = "You lash out at your foe with everything you’ve got.";
            brutality2c.Description = "Weapon {Melee/+[size of weapon]} [6 Stamina]";
            brutality2c.ClarifyingText = "";
            brutality2c.StaminaCost = 6;
            brutality2c.UpkeepCost = null;
            brutality2c.Tier = 2;
            brutality2c.TierBenefitDescription = "+1 to damage of Heavy Melee weapons";
            brutality2c.Tree = TalentTree.Brutality;
            brutality2c.TreeName = "Brutality";
            brutality2c.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality2c);
            #endregion
            #region T3
            Talent brutality3a = new Talent();
            brutality3a.Name = "Lay ‘Em Flat";
            brutality3a.Type = TalentType.Maneuver;
            brutality3a.Action = ActionType.Combat;
            brutality3a.DescriptionFluff = "You hammer your foe, smashing it to the ground.";
            brutality3a.Description = "Weapon {Melee/+[size of weapon]} [8 Stamina]";
            brutality3a.ClarifyingText = "On a hit, in addition to damage, knock the target Prone. On a Stage 2 or higher Crit, the Prone condition persists until Resisted.";
            brutality3a.StaminaCost = 8;
            brutality3a.UpkeepCost = null;
            brutality3a.Tier = 3;
            brutality3a.TierBenefitDescription = "+1 CM of Heavy Melee weapons";
            brutality3a.Tree = TalentTree.Brutality;
            brutality3a.TreeName = "Brutality";
            brutality3a.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality3a);

            Talent brutality3b = new Talent();
            brutality3b.Name = "Carve aka Cleave";
            brutality3b.Type = TalentType.TriggeredAction;
            brutality3b.Action = ActionType.Reaction;
            brutality3b.DescriptionFluff = "You are overcome with adrenaline after Slashing through an opponent.";
            brutality3b.Description = "Weapon {Melee/-2} [4 Stamina]";
            brutality3b.ClarifyingText = "Make another attack against an adjacent target.";
            brutality3b.StaminaCost = 4;
            brutality3b.UpkeepCost = null;
            brutality3b.Tier = 3;
            brutality3b.TierBenefitDescription = "+1 CM of Heavy Melee weapons";
            brutality3b.Tree = TalentTree.Brutality;
            brutality3b.TreeName = "Brutality";
            brutality3b.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality3b);

            Talent brutality3c = new Talent();
            brutality3c.Name = "Vicious Thrust";
            brutality3c.Type = TalentType.AttackAugment;
            brutality3c.Action = ActionType.Quick;
            brutality3c.DescriptionFluff = "You focus your energies on your next attack.";
            brutality3c.Description = "The augmented attack gains Lethal +1. [4 Stamina]";
            brutality3c.ClarifyingText = "";
            brutality3c.StaminaCost = 4;
            brutality3c.UpkeepCost = null;
            brutality3c.Tier = 3;
            brutality3c.TierBenefitDescription = "+1 CM of Heavy Melee weapons";
            brutality3c.Tree = TalentTree.Brutality;
            brutality3c.TreeName = "Brutality";
            brutality3c.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality3c);
            #endregion
            #region T4
            Talent brutality4a = new Talent();
            brutality4a.Name = "Residual Effects";
            brutality4a.Type = TalentType.TriggeredAction;
            brutality4a.Action = ActionType.Reaction;
            brutality4a.DescriptionFluff = "Despite its Resistance to your blow, you manage to pull vigor from your enemy.";
            brutality4a.Description = "The target loses HP equal to your Strength Attribute.";
            brutality4a.ClarifyingText = " This damage cannot be reduced.";
            brutality4a.StaminaCost = 5;
            brutality4a.UpkeepCost = null;
            brutality4a.Tier = 4;
            brutality4a.TierBenefitDescription = "Can use Vicious re-rolls on rolls of 2 or lower when using Heavy Melee weapons";
            brutality4a.Tree = TalentTree.Brutality;
            brutality4a.TreeName = "Brutality";
            brutality4a.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality4a);

            Talent brutality4b = new Talent();
            brutality4b.Name = "Sweeping Strike";
            brutality4b.Type = TalentType.Maneuver;
            brutality4b.Action = ActionType.Combat;
            brutality4b.DescriptionFluff = "Your weapon hacks through your enemies in swaths.";
            brutality4b.Description = "Weapon {Area +0/+0} [10 Stamina]";
            brutality4b.ClarifyingText = "Attack up to 4 adjacent creatures.";
            brutality4b.StaminaCost = 10;
            brutality4b.UpkeepCost = null;
            brutality4b.Tier = 4;
            brutality4b.TierBenefitDescription = "Can use Vicious re-rolls on rolls of 2 or lower when using Heavy Melee weapons";
            brutality4b.Tree = TalentTree.Brutality;
            brutality4b.TreeName = "Brutality";
            brutality4b.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality4b);

            Talent brutality4c = new Talent();
            brutality4c.Name = "Berserker Stance";
            brutality4c.Type = TalentType.Stance;
            brutality4c.Action = ActionType.Quick;
            brutality4c.DescriptionFluff = "Your savagery rises to new heights.";
            brutality4c.Description = "You gain Enraged as well as a +2 to damage and +2CM on all attacks made with weapons held in 2 hands. [10/3 Stamina]";
            brutality4c.ClarifyingText = "Prerequisite: Brute Stance.  Enraged = +2 melee damage, +2 durability, +2 Str non-combat skill checks.";
            brutality4c.StaminaCost = 10;
            brutality4c.UpkeepCost = 3;
            brutality4c.Tier = 4;
            brutality4c.TierBenefitDescription = "Can use Vicious re-rolls on rolls of 2 or lower when using Heavy Melee weapons";
            brutality4c.Tree = TalentTree.Brutality;
            brutality4c.TreeName = "Brutality";
            brutality4c.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality4c);
            #endregion
            #region T5
            Talent brutality5a = new Talent();
            brutality5a.Name = "Whirlwind";
            brutality5a.Type = TalentType.Maneuver;
            brutality5a.Action = ActionType.Combat;
            brutality5a.DescriptionFluff = "Your weapon leads the charge as you turn in mighty arcs, annihilating all within your reach.";
            brutality5a.Description = "Weapon {Area (10’ radius) +0/+0} [12 Stamina]";
            brutality5a.ClarifyingText = "";
            brutality5a.StaminaCost = 12;
            brutality5a.UpkeepCost = null;
            brutality5a.Tier = 5;
            brutality5a.TierBenefitDescription = "+1 to Vicious, damage, and CM of Heavy Melee weapons";
            brutality5a.Tree = TalentTree.Brutality;
            brutality5a.TreeName = "Brutality";
            brutality5a.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality5a);

            Talent brutality5b = new Talent();
            brutality5b.Name = "Titan Grip";
            brutality5b.Type = TalentType.Benefit;
            brutality5b.Action = ActionType.None;
            brutality5b.DescriptionFluff = "You become a bastion of strength and vitality.";
            brutality5b.Description = "You cannot be disarmed. You Gain +2 Durability vs. all Melee attacks and +2 to your effective Strength for determining the ?weight? => handedness of a Melee weapon.";
            brutality5b.ClarifyingText = "";
            brutality5b.StaminaCost = null;
            brutality5b.UpkeepCost = null;
            brutality5b.Tier = 5;
            brutality5b.TierBenefitDescription = "+1 to Vicious, damage, and CM of Heavy Melee weapons";
            brutality5b.Tree = TalentTree.Brutality;
            brutality5b.TreeName = "Brutality";
            brutality5b.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality5b);

            Talent brutality5c = new Talent();
            brutality5c.Name = "Carve a Path";
            brutality5c.Type = TalentType.TriggeredAction;
            brutality5c.Action = ActionType.Reaction;
            brutality5c.DescriptionFluff = "You are an avatar of death and scatter foes like playthings.";
            brutality5c.Description = "Weapon {Melee +0/+[Size of Weapon]} [8 Stamina]";
            brutality5c.ClarifyingText = "You can continue to make this attack against targets within range until your attack fails to incapacitate an opponent. You may spend MI in between these attacks. Prerequisite: Carve";
            brutality5c.StaminaCost = 8;
            brutality5c.UpkeepCost = null;
            brutality5c.Tier = 5;
            brutality5c.TierBenefitDescription = "+1 to Vicious, damage, and CM of Heavy Melee weapons";
            brutality5c.Tree = TalentTree.Brutality;
            brutality5c.TreeName = "Brutality";
            brutality5c.LinkedSkill = WeaponSkill.Heavy;
            Talents.Add(brutality5c);
            #endregion
            #endregion
            #region Bulwark (Dueling)
            #region T1
            Talent bulwark1a = new Talent();
            bulwark1a.Name = "Guardian";
            bulwark1a.Type = TalentType.Benefit;
            bulwark1a.Action = ActionType.None;
            bulwark1a.DescriptionFluff = "You are your allies’ best defense.";
            bulwark1a.Description = "When you gain your Tier 1 Benefit from this tree, you can also apply the Benefit to an ally within 10’ of you.";
            bulwark1a.ClarifyingText = "";
            bulwark1a.StaminaCost = null;
            bulwark1a.UpkeepCost = null;
            bulwark1a.Tier = 1;
            bulwark1a.TierBenefitDescription = "When you attack an enemy, it cannot gain the “Allies adjacent to target” bonus against you for 1 round";
            bulwark1a.Tree = TalentTree.Bulwark;
            bulwark1a.TreeName = "Bulwark";
            bulwark1a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark1a);

            Talent bulwark1b = new Talent();
            bulwark1b.Name = "Shield Guard";
            bulwark1b.Type = TalentType.TriggeredAction;
            bulwark1b.Action = ActionType.Reaction;
            bulwark1b.DescriptionFluff = "If your allies stay close, you will oft er protection.";
            bulwark1b.Description = "Adjacent allies get ½ of the Cover Value and Toughness that your shield provides for 1 round. [2 Stamina]";
            bulwark1b.ClarifyingText = "";
            bulwark1b.StaminaCost = 2;
            bulwark1b.UpkeepCost = null;
            bulwark1b.Tier = 1;
            bulwark1b.TierBenefitDescription = "When you attack an enemy, it cannot gain the “Allies adjacent to target” bonus against you for 1 round";
            bulwark1b.Tree = TalentTree.Bulwark;
            bulwark1b.TreeName = "Bulwark";
            bulwark1b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark1b);

            Talent bulwark1c = new Talent();
            bulwark1c.Name = "Break Grapple";
            bulwark1c.Type = TalentType.Maneuver;
            bulwark1c.Action = ActionType.Combat;
            bulwark1c.DescriptionFluff = "You strategically strike at your target to separate it from your ally.";
            bulwark1c.Description = "Weapon {Melee +0/+0} [4 Stamina]";
            bulwark1c.ClarifyingText = "If the target of the attack takes damage, it is no longer considered Wrestling, and neither is anyone with which only he was Wrestling.";
            bulwark1c.StaminaCost = 4;
            bulwark1c.UpkeepCost = null;
            bulwark1c.Tier = 1;
            bulwark1c.TierBenefitDescription = "When you attack an enemy, it cannot gain the “Allies adjacent to target” bonus against you for 1 round";
            bulwark1c.Tree = TalentTree.Bulwark;
            bulwark1c.TreeName = "Bulwark";
            bulwark1c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark1c);
            #endregion
            #region T2
            Talent bulwark2a = new Talent();
            bulwark2a.Name = "Sword and Board Stance";
            bulwark2a.Type = TalentType.Stance;
            bulwark2a.Action = ActionType.Quick;
            bulwark2a.DescriptionFluff = "Your weapon and shield work in perfect concert.";
            bulwark2a.Description = "While active, gain the Focused Condition. [6/2 Stamina]";
            bulwark2a.ClarifyingText = "Focused = +1 to Melee weapon attaks, +1 to all defenses, +2 to Focus linked non-combat skill checks.";
            bulwark2a.StaminaCost = 6;
            bulwark2a.UpkeepCost = 2;
            bulwark2a.Tier = 2;
            bulwark2a.TierBenefitDescription = "The Defensive property of weapons you wield can be applied to one ally within 5'. All Dueling and Close Combat weapons are Defensive 1 in your hands. If already Defensive, they provide an additional + 1 bonus to your  Melee Defense.";
            bulwark2a.Tree = TalentTree.Bulwark;
            bulwark2a.TreeName = "Bulwark";
            bulwark2a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark2a);

            Talent bulwark2b = new Talent();
            bulwark2b.Name = "Lunge";
            bulwark2b.Type = TalentType.AttackAugment;
            bulwark2b.Action = ActionType.Quick;
            bulwark2b.DescriptionFluff = "Through quick footwork you are able to extend your area of influence while fighting.";
            bulwark2b.Description = "For the augmented attack and for 1 round aft er, your weapon gains the Reach property. [3 Stamina]";
            bulwark2b.ClarifyingText = "An Unarmed or Untrained attack against a target armed with a Reach weapon while not wielding a Reach weapon creates an opening.  Reach = Gain +2 to attack and damage with the Reaction Attack from Openings.";
            bulwark2b.StaminaCost = 3;
            bulwark2b.UpkeepCost = null;
            bulwark2b.Tier = 2;
            bulwark2b.TierBenefitDescription = "The Defensive property of weapons you wield can be applied to one ally within 5'. All Dueling and Close Combat weapons are Defensive 1 in your hands. If already Defensive, they provide an additional + 1 bonus to your  Melee Defense.";
            bulwark2b.Tree = TalentTree.Bulwark;
            bulwark2b.TreeName = "Bulwark";
            bulwark2b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark2b);

            Talent bulwark2c = new Talent();
            bulwark2c.Name = "Drive them Low";
            bulwark2c.Type = TalentType.Maneuver;
            bulwark2c.Action = ActionType.Combat;
            bulwark2c.DescriptionFluff = "You use your enemies’ size against them and drive them to the ground.";
            bulwark2c.Description = "Weapon {Melee -2/+[targets size]} [6 Stamina]";
            bulwark2c.ClarifyingText = "On a hit, the target of this attack falls Prone and becomes Dazed (until Resisted).";
            bulwark2c.StaminaCost = 6;
            bulwark2c.UpkeepCost = null;
            bulwark2c.Tier = 2;
            bulwark2c.TierBenefitDescription = "The Defensive property of weapons you wield can be applied to one ally within 5'. All Dueling and Close Combat weapons are Defensive 1 in your hands. If already Defensive, they provide an additional + 1 bonus to your  Melee Defense.";
            bulwark2c.Tree = TalentTree.Bulwark;
            bulwark2c.TreeName = "Bulwark";
            bulwark2c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark2c);
            #endregion
            #region T3
            Talent bulwark3a = new Talent();
            bulwark3a.Name = "Concussion";
            bulwark3a.Type = TalentType.Maneuver;
            bulwark3a.Action = ActionType.Combat;
            bulwark3a.DescriptionFluff = "You slam your weapon into your opponent’s head.";
            bulwark3a.Description = "Weapon {Melee +0/+0} [6 Stamina]";
            bulwark3a.ClarifyingText = "If the target is damaged by this attack, it gains the Weakened condition until the end of the encounter. If you make this attack with a shield, gain +2 to attack and damage and increase the Resistance MCR by 2.";
            bulwark3a.StaminaCost = 6;
            bulwark3a.UpkeepCost = null;
            bulwark3a.Tier = 3;
            bulwark3a.TierBenefitDescription = "Defensive weapons provide their defensive bonus to all of your Defenses";
            bulwark3a.Tree = TalentTree.Bulwark;
            bulwark3a.TreeName = "Bulwark";
            bulwark3a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark3a);

            Talent bulwark3b = new Talent();
            bulwark3b.Name = "Anticipate Attack";
            bulwark3b.Type = TalentType.TriggeredAction;
            bulwark3b.Action = ActionType.Reaction;
            bulwark3b.DescriptionFluff = "You prepare for an upcoming blow.";
            bulwark3b.Description = "(Triggered Action: you are targeted by an attack) Gain the Girded Condition until the beginning of your next turn. [5 Stamina]";
            bulwark3b.ClarifyingText = "Girded = +2 to Durability and + 1 to all Defenses";
            bulwark3b.StaminaCost = 5;
            bulwark3b.UpkeepCost = null;
            bulwark3b.Tier = 3;
            bulwark3b.TierBenefitDescription = "Defensive weapons provide their defensive bonus to all of your Defenses";
            bulwark3b.Tree = TalentTree.Bulwark;
            bulwark3b.TreeName = "Bulwark";
            bulwark3b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark3b);

            Talent bulwark3c = new Talent();
            bulwark3c.Name = "Beat Back";
            bulwark3c.Type = TalentType.Maneuver;
            bulwark3c.Action = ActionType.Combat;
            bulwark3c.DescriptionFluff = "If the augmented attack is made using a shield, it gains Knockback and a +6 to damage. [4 Stamina]";
            bulwark3c.Description = "";
            bulwark3c.ClarifyingText = "";
            bulwark3c.StaminaCost = 4;
            bulwark3c.UpkeepCost = null;
            bulwark3c.Tier = 3;
            bulwark3c.TierBenefitDescription = "Defensive weapons provide their defensive bonus to all of your Defenses";
            bulwark3c.Tree = TalentTree.Bulwark;
            bulwark3c.TreeName = "Bulwark";
            bulwark3c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark3c);
            #endregion
            #region T4
            Talent bulwark4a = new Talent();
            bulwark4a.Name = "Reach Advantage";
            bulwark4a.Type = TalentType.TriggeredAction;
            bulwark4a.Action = ActionType.Reaction;
            bulwark4a.DescriptionFluff = "Your quick steps keep the enemy at bay.";
            bulwark4a.Description = "(Triggered Action: an enemy advances adjacent to you) Move 1 MI. [5 Stamina]";
            bulwark4a.ClarifyingText = "";
            bulwark4a.StaminaCost = 5;
            bulwark4a.UpkeepCost = null;
            bulwark4a.Tier = 4;
            bulwark4a.TierBenefitDescription = "You gain a +2 to Disarm attacks with Close Combat and Dueling Weapons as well as shields";
            bulwark4a.Tree = TalentTree.Bulwark;
            bulwark4a.TreeName = "Bulwark";
            bulwark4a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark4a);

            Talent bulwark4b = new Talent();
            bulwark4b.Name = "Adrenaline Rush Stance";
            bulwark4b.Type = TalentType.Stance;
            bulwark4b.Action = ActionType.Quick;
            bulwark4b.DescriptionFluff = "Your coursing hormones give you a boost during combat.";
            bulwark4b.Description = "Gain Stamina Regen +3 for up to 3 rounds. When the Stance ends, suffer the Exhausted condition (until Resisted). [0/0 Stamina]";
            bulwark4b.ClarifyingText = "This Stance cannot be activated while the user is Exhausted.";
            bulwark4b.StaminaCost = 0;
            bulwark4b.UpkeepCost = 0;
            bulwark4b.Tier = 4;
            bulwark4b.TierBenefitDescription = "You gain a +2 to Disarm attacks with Close Combat and Dueling Weapons as well as shields";
            bulwark4b.Tree = TalentTree.Bulwark;
            bulwark4b.TreeName = "Bulwark";
            bulwark4b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark4b);

            Talent bulwark4c = new Talent();
            bulwark4c.Name = "Phalanx Stance";
            bulwark4c.Type = TalentType.Stance;
            bulwark4c.Action = ActionType.Quick;
            bulwark4c.DescriptionFluff = "You and your allies work together to avoid damage from enemy attacks.";
            bulwark4c.Description = "Allies without shields adjacent to you gain the Cover that your shield provides. As long as you have an ally within 10 feet of you that is armed with a shield, you and all such equipped allies become Girded and Focused. [10/3 Stamina]";
            bulwark4c.ClarifyingText = "Focused = +1 to Melee weapon attaks, +1 to all defenses, +2 to Focus linked non-combat skill checks. Girded = +2 to Durability and + 1 to all Defenses";
            bulwark4c.StaminaCost = 10;
            bulwark4c.UpkeepCost = 3;
            bulwark4c.Tier = 4;
            bulwark4c.TierBenefitDescription = "You gain a +2 to Disarm attacks with Close Combat and Dueling Weapons as well as shields";
            bulwark4c.Tree = TalentTree.Bulwark;
            bulwark4c.TreeName = "Bulwark";
            bulwark4c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark4c);
            #endregion
            #region T5
            Talent bulwark5a = new Talent();
            bulwark5a.Name = "Master Strike";
            bulwark5a.Type = TalentType.AttackAugment;
            bulwark5a.Action = ActionType.Quick;
            bulwark5a.DescriptionFluff = "Enemies cannot stop your skilled oft ensive.";
            bulwark5a.Description = "The augmented attack with a Dueling weapon, Close Combat weapon, or Shield automatically hits, or you win the augmented opposed Skill Check.";
            bulwark5a.ClarifyingText = "This attack augment can be applid to opposed skill checks. Do not roll to hit; Crits and Fumbles are not possible.";
            bulwark5a.StaminaCost = 6;
            bulwark5a.UpkeepCost = null;
            bulwark5a.Tier = 5;
            bulwark5a.TierBenefitDescription = "Enemies do not gain the “Allies adjacent to target” bonuses against you when you are armed with a Dueling or Close Combat weapon or a shield";
            bulwark5a.Tree = TalentTree.Bulwark;
            bulwark5a.TreeName = "Bulwark";
            bulwark5a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark5a);

            Talent bulwark5b = new Talent();
            bulwark5b.Name = "Finishing Blow";
            bulwark5b.Type = TalentType.Maneuver;
            bulwark5b.Action = ActionType.Combat;
            bulwark5b.DescriptionFluff = "No enemy can resist your onslaught.";
            bulwark5b.Description = "Weapon {Melee +0/+0)} [12 Stamina]";
            bulwark5b.ClarifyingText = "Make a Melee attack against a Vulnerable target. If the attack hits, the damage roll ignores the armor of the target.";
            bulwark5b.StaminaCost = 12;
            bulwark5b.UpkeepCost = null;
            bulwark5b.Tier = 5;
            bulwark5b.TierBenefitDescription = "Enemies do not gain the “Allies adjacent to target” bonuses against you when you are armed with a Dueling or Close Combat weapon or a shield";
            bulwark5b.Tree = TalentTree.Bulwark;
            bulwark5b.TreeName = "Bulwark";
            bulwark5b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark5b);

            Talent bulwark5c = new Talent();
            bulwark5c.Name = "Disrupting Strike";
            bulwark5c.Type = TalentType.Maneuver;
            bulwark5c.Action = ActionType.Combat;
            bulwark5c.DescriptionFluff = "Your barrage of attacks disrupts the enemy’s concentration.";
            bulwark5c.Description = "Weapon {Melee +0/+0} [10 Stamina]";
            bulwark5c.ClarifyingText = "On hit, end all Stances, Enhancements, Effects, Augmentations, and weapon or armor Properties that require activation or maintenance(those affecting the target as well as those the target created).";
            bulwark5c.StaminaCost = 10;
            bulwark5c.UpkeepCost = null;
            bulwark5c.Tier = 5;
            bulwark5c.TierBenefitDescription = "Enemies do not gain the “Allies adjacent to target” bonuses against you when you are armed with a Dueling or Close Combat weapon or a shield";
            bulwark5c.Tree = TalentTree.Bulwark;
            bulwark5c.TreeName = "Bulwark";
            bulwark5c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(bulwark5c);
            #endregion
            #endregion
            #region Bulwark (Close Combat)
            //TODO: Copy
            #endregion
            #region Grappling (Unarmed)
            #region T1
            Talent grappling1a = new Talent();
            grappling1a.Name = "Improved Grab";
            grappling1a.Type = TalentType.Benefit;
            grappling1a.Action = ActionType.None;
            grappling1a.DescriptionFluff = "Your grasp is inescapable.";
            grappling1a.Description = "You gain +2 to Grab attacks and +1 to Overpower checks.";
            grappling1a.ClarifyingText = "";
            grappling1a.StaminaCost = null;
            grappling1a.UpkeepCost = null;
            grappling1a.Tier = 1;
            grappling1a.TierBenefitDescription = "Gain +2 to Melee Defense vs. attempts to break free from you";
            grappling1a.Tree = TalentTree.Grappling;
            grappling1a.TreeName = "Grappling";
            grappling1a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling1a);

            Talent grappling1b = new Talent();
            grappling1b.Name = "Improved Disarm";
            grappling1b.Type = TalentType.Benefit;
            grappling1b.Action = ActionType.None;
            grappling1b.DescriptionFluff = "You painfully twist your opponent’s limb, freeing him of his weapon.";
            grappling1b.Description = "When you successfully perform a Disarm attack against an opponent, the target of that attack suffers a damage roll equal to your normal Unarmed damage.";
            grappling1b.ClarifyingText = "";
            grappling1b.StaminaCost = null;
            grappling1b.UpkeepCost = null;
            grappling1b.Tier = 1;
            grappling1b.TierBenefitDescription = "Gain +2 to Melee Defense vs. attempts to break free from you";
            grappling1b.Tree = TalentTree.Grappling;
            grappling1b.TreeName = "Grappling";
            grappling1b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling1b);

            Talent grappling1c = new Talent();
            grappling1c.Name = "Lock";
            grappling1c.Type = TalentType.Maneuver;
            grappling1c.Action = ActionType.Combat;
            grappling1c.DescriptionFluff = "You tangle your opponent into a compromising position.";
            grappling1c.Description = "Unarmed or Agi based Unarmed {Melee -2/Physical +0} [6 Stamina]";
            grappling1c.ClarifyingText = "On a hit the target becomes Locked. Locked opponents are Weakened (until no longer Locked). The target can free itself from a Lock in the same way it would break from Wrestling. The target must first free itself from the Lock before it can break from Wrestling.";
            grappling1c.StaminaCost = 6;
            grappling1c.UpkeepCost = null;
            grappling1c.Tier = 1;
            grappling1c.TierBenefitDescription = "Gain +2 to Melee Defense vs. attempts to break free from you";
            grappling1c.Tree = TalentTree.Grappling;
            grappling1c.TreeName = "Grappling";
            grappling1c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling1c);
            #endregion
            #region T2
            Talent grappling2a = new Talent();
            grappling2a.Name = "Weapon Reversal";
            grappling2a.Type = TalentType.Maneuver;
            grappling2a.Action = ActionType.Combat;
            grappling2a.DescriptionFluff = "You contort your opponent with a quick series of Maneuvers, forcing their own weapon to be used against them.";
            grappling2a.Description = "Overpower. If you succeed in the Overpower Check, make an Unarmed attack using the Accuracy of the weapon the target is wielding. On a hit, add the weapons damage to your normal Unarmed damage roll. [6 Stamina]";
            grappling2a.ClarifyingText = "If the target is Incapacitated before the start of its next action, you can choose to be armed with the weapon used in this attack.";
            grappling2a.StaminaCost = 6;
            grappling2a.UpkeepCost = null;
            grappling2a.Tier = 2;
            grappling2a.TierBenefitDescription = "Whenever you damage an opponent with a Grab attack or Wrestling Maneuver, the opponent is Weakened for 1 round";
            grappling2a.Tree = TalentTree.Grappling;
            grappling2a.TreeName = "Grappling";
            grappling2a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling2a);

            Talent grappling2b = new Talent();
            grappling2b.Name = "Improved Lock";
            grappling2b.Type = TalentType.Benefit;
            grappling2b.Action = ActionType.None;
            grappling2b.DescriptionFluff = "Once you tie them up, they become your playthings.";
            grappling2b.Description = "When you have an opponent Locked, it is also Vulnerable. You gain a +1 CM against opponents Locked by you.";
            grappling2b.ClarifyingText = "Vulnerable = -2 Defenses, -2 Durability, -2 Willpower checks";
            grappling2b.StaminaCost = 6;
            grappling2b.UpkeepCost = null;
            grappling2b.Tier = 2;
            grappling2b.TierBenefitDescription = "Whenever you damage an opponent with a Grab attack or Wrestling Maneuver, the opponent is Weakened for 1 round";
            grappling2b.Tree = TalentTree.Grappling;
            grappling2b.TreeName = "Grappling";
            grappling2b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling2b);

            Talent grappling2c = new Talent();
            grappling2c.Name = "Improved Slam";
            grappling2c.Type = TalentType.Benefit;
            grappling2c.Action = ActionType.None;
            grappling2c.DescriptionFluff = "You are practiced at smashing your opponents into things.";
            grappling2c.Description = "Gain +4 to Overpower checks when attempting Slam Maneuvers.";
            grappling2c.ClarifyingText = "";
            grappling2c.StaminaCost = 6;
            grappling2c.UpkeepCost = null;
            grappling2c.Tier = 2;
            grappling2c.TierBenefitDescription = "Whenever you damage an opponent with a Grab attack or Wrestling Maneuver, the opponent is Weakened for 1 round";
            grappling2c.Tree = TalentTree.Grappling;
            grappling2c.TreeName = "Grappling";
            grappling2c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling2c);
            #endregion
            #region T3
            Talent grappling3a = new Talent();
            grappling3a.Name = "Improved Throw";
            grappling3a.Type = TalentType.Benefit;
            grappling3a.Action = ActionType.None;
            grappling3a.DescriptionFluff = "Careful joint manipulation can extend the effectiveness of your throws.";
            grappling3a.Description = "Gain +4 to Overpower Checks when attempting the Throw wrestling maneuver. If you Overpower the target, the target can be placed anywhere with 5’ of you before being moved by the Throw.";
            grappling3a.ClarifyingText = "";
            grappling3a.StaminaCost = null;
            grappling3a.UpkeepCost = null;
            grappling3a.Tier = 3;
            grappling3a.TierBenefitDescription = "Gain +2 to damage rolls for Grab attacks and Wrestling Maneuvers.";
            grappling3a.Tree = TalentTree.Grappling;
            grappling3a.TreeName = "Grappling";
            grappling3a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling3a);

            Talent grappling3b = new Talent();
            grappling3b.Name = "Reactive Grab";
            grappling3b.Type = TalentType.TriggeredAction;
            grappling3b.Action = ActionType.Reaction;
            grappling3b.DescriptionFluff = "A missed melee attack leaves your foe Vulnerable to your iron grip.";
            grappling3b.Description = "Make a Grab attack against the creature that made the Triggering attack. [4 Stamina]";
            grappling3b.ClarifyingText = "Triggered Action: you are missed by a Melee attack";
            grappling3b.StaminaCost = 4;
            grappling3b.UpkeepCost = null;
            grappling3b.Tier = 3;
            grappling3b.TierBenefitDescription = "Gain +2 to damage rolls for Grab attacks and Wrestling Maneuvers.";
            grappling3b.Tree = TalentTree.Grappling;
            grappling3b.TreeName = "Grappling";
            grappling3b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling3b);

            Talent grappling3c = new Talent();
            grappling3c.Name = "Body Shield";
            grappling3c.Type = TalentType.Benefit;
            grappling3c.Action = ActionType.None;
            grappling3c.DescriptionFluff = "You hold your opponent in the line of fire.";
            grappling3c.Description = "You gain Heavy Concealment against the attack. If the attacker rolls within the Concealment Value range with the attack, the damage is rolled against the opponent you are Wrestling instead of you.  If an opponent that you were Wrestling was Incapacitated during the last round of combat, you are still considered Wrestling for the purposes of this ability.";
            grappling3c.ClarifyingText = "Triggered Action: you are attacked with a Ranged or Area attack while you are Wrestling";
            grappling3c.StaminaCost = 4;
            grappling3c.UpkeepCost = null;
            grappling3c.Tier = 3;
            grappling3c.TierBenefitDescription = "Gain +2 to damage rolls for Grab attacks and Wrestling Maneuvers.";
            grappling3c.Tree = TalentTree.Grappling;
            grappling3c.TreeName = "Grappling";
            grappling3c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling3c);
            #endregion
            #region T4
            Talent grappling4a = new Talent();
            grappling4a.Name = "Reactive Throw";
            grappling4a.Type = TalentType.Benefit;
            grappling4a.Action = ActionType.None;
            grappling4a.DescriptionFluff = "You use your opponents’ momentum against them.";
            grappling4a.Description = "If you use Throw in conjunction with Reactive Grab, you gain +4 to the Overpower Check of the Throw.";
            grappling4a.ClarifyingText = "";
            grappling4a.StaminaCost = null;
            grappling4a.UpkeepCost = null;
            grappling4a.Tier = 4;
            grappling4a.TierBenefitDescription = "You can maintain Wrestling and Locks against one opponent per round for free";
            grappling4a.Tree = TalentTree.Grappling;
            grappling4a.TreeName = "Grappling";
            grappling4a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling4a);

            Talent grappling4b = new Talent();
            grappling4b.Name = "Limb Break";
            grappling4b.Type = TalentType.Maneuver;
            grappling4b.Action = ActionType.Combat;
            grappling4b.DescriptionFluff = "With ruthless efficiency, you strain against your opponent’s limb, snapping it like a tree branch.";
            grappling4b.Description = "Overpower a Locked opponent to break a limb. On a successful Check, the target takes normal Unarmed damage +6. This Maneuver causes the target to become Locked (Persistent) even if you release the Lock. The Locked state can only be corrected by First Aid (First Aid MCR to correct the damage is equal to the damage taken from this attack). [10 Stamina]";
            grappling4b.ClarifyingText = "";
            grappling4b.StaminaCost = 10;
            grappling4b.UpkeepCost = null;
            grappling4b.Tier = 4;
            grappling4b.TierBenefitDescription = "You can maintain Wrestling and Locks against one opponent per round for free";
            grappling4b.Tree = TalentTree.Grappling;
            grappling4b.TreeName = "Grappling";
            grappling4b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling4b);

            Talent grappling4c = new Talent();
            grappling4c.Name = "Manhandler";
            grappling4c.Type = TalentType.Stance;
            grappling4c.Action = ActionType.Quick;
            grappling4c.DescriptionFluff = "You throw your opponents like rag dolls.";
            grappling4c.Description = "You gain a +5 bonus to Overpower checks to Drag, Throw, or Slam.";
            grappling4c.ClarifyingText = "";
            grappling4c.StaminaCost = 10;
            grappling4c.UpkeepCost = 0;
            grappling4c.Tier = 4;
            grappling4c.TierBenefitDescription = "You can maintain Wrestling and Locks against one opponent per round for free";
            grappling4c.Tree = TalentTree.Grappling;
            grappling4c.TreeName = "Grappling";
            grappling4c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling4c);
            #endregion
            #region T5
            Talent grappling5a = new Talent();
            grappling5a.Name = "Choke Out";
            grappling5a.Type = TalentType.Maneuver;
            grappling5a.Action = ActionType.Combat;
            grappling5a.DescriptionFluff = "You entangle your foe’s throat in a vice-like grip, causing their vision to quickly begin to blur.";
            grappling5a.Description = "On a successful Overpower Check the target takes normal damage, ignoring armor.  Additionally, the target is Dazed (until no longer Wrestling with you). If you successfully use this attack against the same opponent for 2 consecutive attacks, it is rendered Unconscious at the conclusion of your second successful attack (until Resisted). [12 Stamina]";
            grappling5a.ClarifyingText = "Dazed = Cannot spend Stamina, -2 to all non-combat Skill Checks.";
            grappling5a.StaminaCost = 12;
            grappling5a.UpkeepCost = null;
            grappling5a.Tier = 5;
            grappling5a.TierBenefitDescription = "+2 to Overpower checks vs. opponents that are at least 1 size larger than you.";
            grappling5a.Tree = TalentTree.Grappling;
            grappling5a.TreeName = "Grappling";
            grappling5a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling5a);

            Talent grappling5b = new Talent();
            grappling5b.Name = "Neck Snap";
            grappling5b.Type = TalentType.AttackAugment;
            grappling5b.Action = ActionType.Quick;
            grappling5b.DescriptionFluff = "You pounce upon your foe, turning their head around backwards with a disgusting crack.";
            grappling5b.Description = "Your next Grab attack against an Unaware or Surprised target that is used to cause damage will ignore the target’s armor and any damage rolled over Durability is doubled. If the target is rendered Unconscious by the attack, it is instantly killed. [12 Stamina]";
            grappling5b.ClarifyingText = "";
            grappling5b.StaminaCost = 12;
            grappling5b.UpkeepCost = null;
            grappling5b.Tier = 5;
            grappling5b.TierBenefitDescription = "+2 to Overpower checks vs. opponents that are at least 1 size larger than you";
            grappling5b.Tree = TalentTree.Grappling;
            grappling5b.TreeName = "Grappling";
            grappling5b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling5b);

            Talent grappling5c = new Talent();
            grappling5c.Name = "Backbreaker";
            grappling5c.Type = TalentType.Maneuver;
            grappling5c.Action = ActionType.Combat;
            grappling5c.DescriptionFluff = "With a hearty grunt, you hoist your opponent into the air, and then yank it back down over your bent knee.";
            grappling5c.Description = "Make an Overpower Check against a Grappled foe. If successful, the target is knocked Prone and you inflict your normal Unarmed damage, applying 3 x your Strength Attribute to your normal damage modifier. If the target is damaged by the attack, it becomes Slowed and Weakened (until Resisted). The target cannot stand from Prone until no longer Slowed or Weakened. If it chooses to suppress the Conditions, it will be able to stand, but it will fall Prone again if either condition affects it again at the start of its next turn. Using this Maneuver automatically ends the Grapple for the target (and the attacker if the target was the only one he was Wrestling) [12 Stamina].";
            grappling5c.ClarifyingText = "Slowed = ½ Speed cannot sprint or run, -2 to Agility linked noncombat Skill Checks.  Weakened = - 2 to Strength-linked noncombat Skill Checks and -2 to attack and damage rolls. Prone = ";
            grappling5c.StaminaCost = 12;
            grappling5c.UpkeepCost = 0;
            grappling5c.Tier = 5;
            grappling5c.TierBenefitDescription = "+2 to Overpower checks vs. opponents that are at least 1 size larger than you";
            grappling5c.Tree = TalentTree.Grappling;
            grappling5c.TreeName = "Grappling";
            grappling5c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(grappling5c);
            #endregion
            #endregion
            #region Gunfighting (Shortarms)
            #region T1
            Talent gunfighting1a = new Talent();
            gunfighting1a.Name = "Gut Plug";
            gunfighting1a.Type = TalentType.Benefit;
            gunfighting1a.Action = ActionType.None;
            gunfighting1a.DescriptionFluff = "You ram you sidearm unto the gut of your opponent before discharging the weapon.";
            gunfighting1a.Description = "While Wrestling, all damage from Shortarms is increased by 4. While Wrestling you can perform attacks with a Shortarm as a Ranged attack or as a Wrestling Maneuver (using either your Shortarms or Wrestling skill for the attack.).";
            gunfighting1a.ClarifyingText = "Gain +1 CM with Shortarms";
            gunfighting1a.StaminaCost = null;
            gunfighting1a.UpkeepCost = null;
            gunfighting1a.Tier = 1;
            gunfighting1a.TierBenefitDescription = "";
            gunfighting1a.Tree = TalentTree.Gunfighting;
            gunfighting1a.TreeName = "Gunfighting";
            gunfighting1a.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting1a);

            Talent gunfighting1b = new Talent();
            gunfighting1b.Name = "Pistol Whip";
            gunfighting1b.Type = TalentType.Maneuver;
            gunfighting1b.Action = ActionType.Combat;
            gunfighting1b.DescriptionFluff = "You smash your opponent in the face with the butt of you pistol.";
            gunfighting1b.Description = "Weapon {Melee +0/ 4+Strength (Bludgeoning)} [4 Stamina]";
            gunfighting1b.ClarifyingText = "Make a Melee attack with a Shortarm, use Shortarms Skill for attack and deal 4 + Strength damage on a hit.";
            gunfighting1b.StaminaCost = null;
            gunfighting1b.UpkeepCost = null;
            gunfighting1b.Tier = 1;
            gunfighting1b.TierBenefitDescription = "Gain +1 CM with Shortarms";
            gunfighting1b.Tree = TalentTree.Gunfighting;
            gunfighting1b.TreeName = "Gunfighting";
            gunfighting1b.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting1b);

            Talent gunfighting1c = new Talent();
            gunfighting1c.Name = "Weaver";
            gunfighting1c.Type = TalentType.Stance;
            gunfighting1c.Action = ActionType.Quick;
            gunfighting1c.DescriptionFluff = "A proper firing position enhances your marksmanship.";
            gunfighting1c.Description = "While using a Shortarm in 2 hands, gain +1 to attack and damage. [4/1 Stamina]";
            gunfighting1c.ClarifyingText = "";
            gunfighting1c.StaminaCost = 4;
            gunfighting1c.UpkeepCost = 1;
            gunfighting1c.Tier = 1;
            gunfighting1c.TierBenefitDescription = "Gain +1 CM with Shortarms";
            gunfighting1c.Tree = TalentTree.Gunfighting;
            gunfighting1c.TreeName = "Gunfighting";
            gunfighting1c.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting1c);
            #endregion
            #region T2
            Talent gunfighting2a = new Talent();
            gunfighting2a.Name = "Gun Kata";
            gunfighting2a.Type = TalentType.Stance;
            gunfighting2a.Action = ActionType.Quick;
            gunfighting2a.DescriptionFluff = "Weaving in complex forms, you pepper your enemies with bullets while expertly dodging their return fire.";
            gunfighting2a.Description = "Gain +1 to attack with Shortarms. Your rate of fire increases by 1 for Semi-Auto weapons and 5 for Full-Auto weapons. You gain a + 1 to Ranged Defense if armed only with Shortarms. [6/2 Stamina]";
            gunfighting2a.ClarifyingText = "";
            gunfighting2a.StaminaCost = 6;
            gunfighting2a.UpkeepCost = 2;
            gunfighting2a.Tier = 2;
            gunfighting2a.TierBenefitDescription = "Gain +1 Damage with Shortarms";
            gunfighting2a.Tree = TalentTree.Gunfighting;
            gunfighting2a.TreeName = "Gunfighting";
            gunfighting2a.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting2a);

            Talent gunfighting2b = new Talent();
            gunfighting2b.Name = "Double Tap";
            gunfighting2b.Type = TalentType.Benefit;
            gunfighting2b.Action = ActionType.None;
            gunfighting2b.DescriptionFluff = "Your controlled groupings decimate your foes.";
            gunfighting2b.Description = "Double the damage bonus from Semi-Auto attacks when using a Shortarm.";
            gunfighting2b.ClarifyingText = "";
            gunfighting2b.StaminaCost = null;
            gunfighting2b.UpkeepCost = null;
            gunfighting2b.Tier = 2;
            gunfighting2b.TierBenefitDescription = "Gain +1 Damage with Shortarms";
            gunfighting2b.Tree = TalentTree.Gunfighting;
            gunfighting2b.TreeName = "Gunfighting";
            gunfighting2b.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting2b);

            Talent gunfighting2c = new Talent();
            gunfighting2c.Name = "Quick Draw";
            gunfighting2c.Type = TalentType.Benefit;
            gunfighting2c.Action = ActionType.None;
            gunfighting2c.DescriptionFluff = "All that practice in the mirror has finally paid off.";
            gunfighting2c.Description = "Gain +2 to Initiative.";
            gunfighting2c.ClarifyingText = "";
            gunfighting2c.StaminaCost = null;
            gunfighting2c.UpkeepCost = null;
            gunfighting2c.Tier = 2;
            gunfighting2c.TierBenefitDescription = "Gain +1 Damage with Shortarms";
            gunfighting2c.Tree = TalentTree.Gunfighting;
            gunfighting2c.TreeName = "Gunfighting";
            gunfighting2c.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting2c);
            #endregion
            #region T3
            Talent gunfighting3a = new Talent();
            gunfighting3a.Name = "Hammer Slap";
            gunfighting3a.Type = TalentType.AttackAugment;
            gunfighting3a.Action = ActionType.Quick;
            gunfighting3a.DescriptionFluff = "Like an Old West gunfifighter, you dramatically increase the fire rate of your wheel gun.";
            gunfighting3a.Description = "Your Single Shot Shortarm gains Semi-Auto 3 fire mode when wielded by you. [4 Stamina]";
            gunfighting3a.ClarifyingText = "";
            gunfighting3a.StaminaCost = 4;
            gunfighting3a.UpkeepCost = null;
            gunfighting3a.Tier = 3;
            gunfighting3a.TierBenefitDescription = "Increase the rate of fire of all Semi-Auto Shortarms by 1";
            gunfighting3a.Tree = TalentTree.Gunfighting;
            gunfighting3a.TreeName = "Gunfighting";
            gunfighting3a.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting3a);

            Talent gunfighting3b = new Talent();
            gunfighting3b.Name = "Rapid Reload";
            gunfighting3b.Type = TalentType.Trick;
            gunfighting3b.Action = ActionType.Quick;
            gunfighting3b.DescriptionFluff = "In the blink of an eye you go from empty to full.";
            gunfighting3b.Description = "Reload a held magazine-fed Shortarm using a Readied magazine. [8 Stamina]";
            gunfighting3b.ClarifyingText = "";
            gunfighting3b.StaminaCost = 8;
            gunfighting3b.UpkeepCost = null;
            gunfighting3b.Tier = 3;
            gunfighting3b.TierBenefitDescription = "Increase the rate of fire of all Semi-Auto Shortarms by 1";
            gunfighting3b.Tree = TalentTree.Gunfighting;
            gunfighting3b.TreeName = "Gunfighting";
            gunfighting3b.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting3b);

            Talent gunfighting3c = new Talent();
            gunfighting3c.Name = "Clear Out";
            gunfighting3c.Type = TalentType.Maneuver;
            gunfighting3c.Action = ActionType.Combat;
            gunfighting3c.DescriptionFluff = "You rapid-fire your pistol into a cluster of enemies at range.";
            gunfighting3c.Description = "Weapon {Pistol(5’ radius) +0/+0} [8 Stamina]";
            gunfighting3c.ClarifyingText = "This attack expends 5 rounds of ammunition.";
            gunfighting3c.StaminaCost = 8;
            gunfighting3c.UpkeepCost = null;
            gunfighting3c.Tier = 3;
            gunfighting3c.TierBenefitDescription = "Increase the rate of fire of all Semi-Auto Shortarms by 1";
            gunfighting3c.Tree = TalentTree.Gunfighting;
            gunfighting3c.TreeName = "Gunfighting";
            gunfighting3c.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting3c);
            #endregion
            #region T4
            Talent gunfighting4a = new Talent();
            gunfighting4a.Name = "Akimbo Stance";
            gunfighting4a.Type = TalentType.Stance;
            gunfighting4a.Action = ActionType.Quick;
            gunfighting4a.DescriptionFluff = "Practice and concentration allow you to effectively aim 2 different firearms at once.";
            gunfighting4a.Description = "Reduce the Two-Weapon Fighting penalties by 1 for attack and 2 for damage when wielding Shortarms. [8/2 Stamina]";
            gunfighting4a.ClarifyingText = "";
            gunfighting4a.StaminaCost = 8;
            gunfighting4a.UpkeepCost = 2;
            gunfighting4a.Tier = 4;
            gunfighting4a.TierBenefitDescription = "On the 1st round of combat you may make a Single Shot attack using a Shortarm as a Free Action.";
            gunfighting4a.Tree = TalentTree.Gunfighting;
            gunfighting4a.TreeName = "Gunfighting";
            gunfighting4a.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting4a);

            Talent gunfighting4b = new Talent();
            gunfighting4b.Name = "Semi-Auto Spray";
            gunfighting4b.Type = TalentType.Benefit;
            gunfighting4b.Action = ActionType.None;
            gunfighting4b.DescriptionFluff = "The enemy cannot escape your bullets.";
            gunfighting4b.Description = "You can perform Full-Auto attacks while wielding a Semi-Auto Shortarm. When doing so, you can gain 1 grouping for every 2 rounds expended in Semi-Auto mode. This benefit does not allow you to perform Suppression attacks.";
            gunfighting4b.ClarifyingText = "";
            gunfighting4b.StaminaCost = null;
            gunfighting4b.UpkeepCost = null;
            gunfighting4b.Tier = 4;
            gunfighting4b.TierBenefitDescription = "On the 1st round of combat you may make a Single Shot attack using a Shortarm as a Free Action.";
            gunfighting4b.Tree = TalentTree.Gunfighting;
            gunfighting4b.TreeName = "Gunfighting";
            gunfighting4b.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting4b);

            Talent gunfighting4c = new Talent();
            gunfighting4c.Name = "Called Shot";
            gunfighting4c.Type = TalentType.Maneuver;
            gunfighting4c.Action = ActionType.Combat;
            gunfighting4c.DescriptionFluff = "You send a single bullet to do the work of many.";
            gunfighting4c.Description = "Weapon {Ranged +0/+0} [10 Stamina]";
            gunfighting4c.ClarifyingText = "Make an attack using only a single round of ammunition. Choose 1 of the following effects: • Lethal + 2 and on a Crit you choose the secondary effect. • Automatically strike any piece of equipment the target is holding or wearing. The attack causes no damage to the target. • If used against an inanimate object, you can automatically hit anything the size of a coin or larger within your range. You have the choice of performing a trick such as turning something on or off when making the shot.";
            gunfighting4c.StaminaCost = 10;
            gunfighting4c.UpkeepCost = null;
            gunfighting4c.Tier = 4;
            gunfighting4c.TierBenefitDescription = "On the 1st round of combat you may make a Single Shot attack using a Shortarm as a Free Action.";
            gunfighting4c.Tree = TalentTree.Gunfighting;
            gunfighting4c.TreeName = "Gunfighting";
            gunfighting4c.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting4c);
            #endregion
            #region T5
            Talent gunfighting5a = new Talent();
            gunfighting5a.Name = "Unload";
            gunfighting5a.Type = TalentType.Maneuver;
            gunfighting5a.Action = ActionType.Combat;
            gunfighting5a.DescriptionFluff = "You empty your weapon at your opponent.";
            gunfighting5a.Description = "Weapon {Ranged +0/+0}";
            gunfighting5a.ClarifyingText = "When making this attack, double the normal rate of fire for the weapon. This attack gains Lethal +1. [12 Stamina]";
            gunfighting5a.StaminaCost = 12;
            gunfighting5a.UpkeepCost = null;
            gunfighting5a.Tier = 5;
            gunfighting5a.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Shortarms";
            gunfighting5a.Tree = TalentTree.Gunfighting;
            gunfighting5a.TreeName = "Gunfighting";
            gunfighting5a.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting5a);

            Talent gunfighting5b = new Talent();
            gunfighting5b.Name = "Deadeye";
            gunfighting5b.Type = TalentType.AttackAugment;
            gunfighting5b.Action = ActionType.Quick;
            gunfighting5b.DescriptionFluff = "You can bullseye the target an nearly any range.";
            gunfighting5b.Description = "Your next attack with a Shortarm that uses only 1 round of ammunition uses [Rifle] range instead of [Pistol] and suffers only ½ the normal range penalties. [6 Stamina]";
            gunfighting5b.ClarifyingText = "";
            gunfighting5b.StaminaCost = 6;
            gunfighting5b.UpkeepCost = null;
            gunfighting5b.Tier = 5;
            gunfighting5b.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Shortarms";
            gunfighting5b.Tree = TalentTree.Gunfighting;
            gunfighting5b.TreeName = "Gunfighting";
            gunfighting5b.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting5b);

            Talent gunfighting5c = new Talent();
            gunfighting5c.Name = "Intercept";
            gunfighting5c.Type = TalentType.TriggeredAction;
            gunfighting5c.Action = ActionType.Reaction;
            gunfighting5c.DescriptionFluff = "She will not get away with that.";
            gunfighting5c.Description = "Weapon {Ranged +0/+0 Physical(no damage)}";
            gunfighting5c.ClarifyingText = "Triggering Action: you are attacked with a Ranged attack. Make this attack against the enemy attacking you. If successful, the target takes no damage but the Triggering attack automatically misses.";
            gunfighting5c.StaminaCost = 6;
            gunfighting5c.UpkeepCost = null;
            gunfighting5c.Tier = 5;
            gunfighting5c.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Shortarms";
            gunfighting5c.Tree = TalentTree.Gunfighting;
            gunfighting5c.TreeName = "Gunfighting";
            gunfighting5c.LinkedSkill = WeaponSkill.Shortarms;
            Talents.Add(gunfighting5c);
            #endregion
            #endregion
            #region Hunting (Longarms)
            #region T1
            Talent hunting1a = new Talent();
            hunting1a.Name = "Blowback";
            hunting1a.Type = TalentType.AttackAugment;
            hunting1a.Action = ActionType.Quick;
            hunting1a.DescriptionFluff = "The force of your shot sends your target sprawling.";
            hunting1a.Description = "Your next attack with a shotgun or Full-Auto Longarm gains Knockback. [2 Stamina]";
            hunting1a.ClarifyingText = "Knockback = (Damage without CM - Target Size * 2)/5 + 1 MI";
            hunting1a.StaminaCost = 2;
            hunting1a.UpkeepCost = null;
            hunting1a.Tier = 1;
            hunting1a.TierBenefitDescription = "Extend the Max range of all Longarms by 20%";
            hunting1a.Tree = TalentTree.Hunting;
            hunting1a.TreeName = "Hunting";
            hunting1a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting1a);

            Talent hunting1b = new Talent();
            hunting1b.Name = "Square-Off";
            hunting1b.Type = TalentType.Stance;
            hunting1b.Action = ActionType.Quick;
            hunting1b.DescriptionFluff = "You take a firm Stance that steadies your aim.";
            hunting1b.Description = "Gain a +2 to Longarm attack damage. This Stance ends if you move from the space you occupy for any reason or are knocked Prone. [4/1 Stamina]";
            hunting1b.ClarifyingText = "";
            hunting1b.StaminaCost = 4;
            hunting1b.UpkeepCost = 1;
            hunting1b.Tier = 1;
            hunting1b.TierBenefitDescription = "Extend the Max range of all Longarms by 20%";
            hunting1b.Tree = TalentTree.Hunting;
            hunting1b.TreeName = "Hunting";
            hunting1b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting1b);

            Talent hunting1c = new Talent();
            hunting1c.Name = "Hunter’s Sense";
            hunting1c.Type = TalentType.Benefit;
            hunting1c.Action = ActionType.None;
            hunting1c.DescriptionFluff = "Years of practice have sharpened your senses.";
            hunting1c.Description = "Gain +1 to Perception and Initiative.";
            hunting1c.ClarifyingText = "";
            hunting1c.StaminaCost = null;
            hunting1c.UpkeepCost = null;
            hunting1c.Tier = 1;
            hunting1c.TierBenefitDescription = "Extend the Max range of all Longarms by 20%";
            hunting1c.Tree = TalentTree.Hunting;
            hunting1c.TreeName = "Hunting";
            hunting1c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting1c);
            #endregion
            #region T2
            Talent hunting2a = new Talent();
            hunting2a.Name = "Rapid Action";
            hunting2a.Type = TalentType.AttackAugment;
            hunting2a.Action = ActionType.Quick;
            hunting2a.DescriptionFluff = "You hammer two rounds into your opponent.";
            hunting2a.Description = "Your next Single Shot attack gains +1 CM and +1 to damage. This augment expends one additional round of ammunition. [3 Stamina]";
            hunting2a.ClarifyingText = "";
            hunting2a.StaminaCost = 3;
            hunting2a.UpkeepCost = null;
            hunting2a.Tier = 2;
            hunting2a.TierBenefitDescription = "Gain +1 to attack with Longarms at close and medium range";
            hunting2a.Tree = TalentTree.Hunting;
            hunting2a.TreeName = "Hunting";
            hunting2a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting2a);

            Talent hunting2b = new Talent();
            hunting2b.Name = "Gun Club";
            hunting2b.Type = TalentType.Maneuver;
            hunting2b.Action = ActionType.Combat;
            hunting2b.DescriptionFluff = "You bash your opponent with the butt-end of your rifle.";
            hunting2b.Description = "Weapon {Melee +0/+0 Bludgeoning}";
            hunting2b.ClarifyingText = "Make an attack with your rifle as if it were a Mace, using your Longarms Skill instead of Close Combat. Treat as an Improvised attack for the purposes of Talents that aid or augment Improvised attacks.";
            hunting2b.StaminaCost = null;
            hunting2b.UpkeepCost = null;
            hunting2b.Tier = 2;
            hunting2b.TierBenefitDescription = "Gain +1 to attack with Longarms at close and medium range";
            hunting2b.Tree = TalentTree.Hunting;
            hunting2b.TreeName = "Hunting";
            hunting2b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting2b);

            Talent hunting2c = new Talent();
            hunting2c.Name = "Big Game Hunter";
            hunting2c.Type = TalentType.Benefit;
            hunting2c.Action = ActionType.None;
            hunting2c.DescriptionFluff = "The bigger they are the harder they fall.";
            hunting2c.Description = "You add (the size of the target) -3 to your damage rolls with Longarms.";
            hunting2c.ClarifyingText = "";
            hunting2c.StaminaCost = null;
            hunting2c.UpkeepCost = null;
            hunting2c.Tier = 2;
            hunting2c.TierBenefitDescription = "Gain +1 to attack with Longarms at close and medium range";
            hunting2c.Tree = TalentTree.Hunting;
            hunting2c.TreeName = "Hunting";
            hunting2c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting2c);
            #endregion
            #region T3
            Talent hunting3a = new Talent();
            hunting3a.Name = "Covering Fire";
            hunting3a.Type = TalentType.Maneuver;
            hunting3a.Action = ActionType.Combat;
            hunting3a.DescriptionFluff = "You rapid fire at your enemies, flushing them from Cover.";
            hunting3a.Description = "Weapon {Area (5’radius within weapon range) +0/+0} [8 Stamina]";
            hunting3a.ClarifyingText = "Expend 6 rounds of ammunition. All targets with Cover that are hit by the attack become Weakened for 1 round. Additionally, targets that leave Cover or end their turn without Cover create an Opening for you that you can use to make a normal Ranged attack using 1 round of ammunition.";
            hunting3a.StaminaCost = 8;
            hunting3a.UpkeepCost = null;
            hunting3a.Tier = 3;
            hunting3a.TierBenefitDescription = "Gain +1 to damage with Longarms";
            hunting3a.Tree = TalentTree.Hunting;
            hunting3a.TreeName = "Hunting";
            hunting3a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting3a);

            Talent hunting3b = new Talent();
            hunting3b.Name = "Rapid Fire";
            hunting3b.Type = TalentType.Maneuver;
            hunting3b.Action = ActionType.Combat;
            hunting3b.DescriptionFluff = "You move with surprising speed as you loose two shots at your enemies.";
            hunting3b.Description = "Weapon {Ranged +0/+0}";
            hunting3b.ClarifyingText = "Make this attack against 2 different targets, or this attack twice against 1 opponent.";
            hunting3b.StaminaCost = 8;
            hunting3b.UpkeepCost = null;
            hunting3b.Tier = 3;
            hunting3b.TierBenefitDescription = "Gain +1 to damage with Longarms";
            hunting3b.Tree = TalentTree.Hunting;
            hunting3b.TreeName = "Hunting";
            hunting3b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting3b);

            Talent hunting3c = new Talent();
            hunting3c.Name = "Assault Stance";
            hunting3c.Type = TalentType.Stance;
            hunting3c.Action = ActionType.Quick;
            hunting3c.DescriptionFluff = "Your low profile and rapid movements make you hard to pin down.";
            hunting3c.Description = "Gain +2 Speed, increase all Cover Values you receive by 1 Grade, and you do not suffer range penalties when making attacks at Medium range or closer. [8/2 Stamina]";
            hunting3c.ClarifyingText = "";
            hunting3c.StaminaCost = 8;
            hunting3c.UpkeepCost = 2;
            hunting3c.Tier = 3;
            hunting3c.TierBenefitDescription = "Gain +1 to damage with Longarms";
            hunting3c.Tree = TalentTree.Hunting;
            hunting3c.TreeName = "Hunting";
            hunting3c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting3c);
            #endregion
            #region T4
            Talent hunting4a = new Talent();
            hunting4a.Name = "Called Shot";
            hunting4a.Type = TalentType.Maneuver;
            hunting4a.Action = ActionType.Combat;
            hunting4a.DescriptionFluff = "";
            hunting4a.Description = "Weapon {Ranged +0/+0} [10 Stamina]";
            hunting4a.ClarifyingText = "Make an attack using only a single round of ammunition. Choose 1 of the following effects: • Lethal + 2 and on a Crit you choose the secondary effect. • Automatically strike any piece of equipment the target is holding or wearing. The attack causes no damage to the target. • If used against an inanimate object, you can automatically hit anything the size of a coin or larger within your range. You have the choice of performing a trick such as turning something on or off when making the shot.";
            hunting4a.StaminaCost = 10;
            hunting4a.UpkeepCost = null;
            hunting4a.Tier = 4;
            hunting4a.TierBenefitDescription = "Gain +1 CM of Longarms";
            hunting4a.Tree = TalentTree.Hunting;
            hunting4a.TreeName = "Hunting";
            hunting4a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting4a);

            Talent hunting4b = new Talent();
            hunting4b.Name = "Custom Weapon";
            hunting4b.Type = TalentType.Benefit;
            hunting4b.Action = ActionType.None;
            hunting4b.DescriptionFluff = "Your constant tinkering with your weapon has fine tuned it to perfection.";
            hunting4b.Description = "Choose 1 weapon that you own that uses the Skill linked to this Talent Tree. You gain an additional Mod Slot in that weapon.";
            hunting4b.ClarifyingText = "This Talent requires 24 hours to apply to a weapon, or to change the weapon to which it applies. This Talent can be taken multiple times, each time applying it to a different weapon in your arsenal.";
            hunting4b.StaminaCost = null;
            hunting4b.UpkeepCost = null;
            hunting4b.Tier = 4;
            hunting4b.TierBenefitDescription = "Gain +1 CM of Longarms";
            hunting4b.Tree = TalentTree.Hunting;
            hunting4b.TreeName = "Hunting";
            hunting4b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting4b);

            Talent hunting4c = new Talent();
            hunting4c.Name = "Focused Fire";
            hunting4c.Type = TalentType.Benefit;
            hunting4c.Action = ActionType.None;
            hunting4c.DescriptionFluff = "You are well-versed at picking off the wounded from the herd.";
            hunting4c.Description = "Gain +2 to damage vs. opponents that have been targeted by at least one ally’s attack since your last turn.";
            hunting4c.ClarifyingText = "";
            hunting4c.StaminaCost = null;
            hunting4c.UpkeepCost = null;
            hunting4c.Tier = 4;
            hunting4c.TierBenefitDescription = "Gain +1 CM of Longarms";
            hunting4c.Tree = TalentTree.Hunting;
            hunting4c.TreeName = "Hunting";
            hunting4c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting4c);
            #endregion
            #region T5
            Talent hunting5a = new Talent();
            hunting5a.Name = "Sure Fire";
            hunting5a.Type = TalentType.Benefit;
            hunting5a.Action = ActionType.None;
            hunting5a.DescriptionFluff = "You have a lucky streak where it comes to shooting.";
            hunting5a.Description = "You can treat die results of 2 as if they are 1s for the purpose of using Spirit.";
            hunting5a.ClarifyingText = "";
            hunting5a.StaminaCost = null;
            hunting5a.UpkeepCost = null;
            hunting5a.Tier = 5;
            hunting5a.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Longarms";
            hunting5a.Tree = TalentTree.Hunting;
            hunting5a.TreeName = "Hunting";
            hunting5a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting5a);

            Talent hunting5b = new Talent();
            hunting5b.Name = "Killshot";
            hunting5b.Type = TalentType.Maneuver;
            hunting5b.Action = ActionType.Combat;
            hunting5b.DescriptionFluff = "Sometimes, all it takes is one perfectly placed shot.";
            hunting5b.Description = "Weapon {Ranged +2/+4} [14 Stamina]";
            hunting5b.ClarifyingText = "Make a Single Shot attack with a Longarm. The attack gains Lethal +2 and a +4 CM.";
            hunting5b.StaminaCost = 14;
            hunting5b.UpkeepCost = null;
            hunting5b.Tier = 5;
            hunting5b.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Longarms";
            hunting5b.Tree = TalentTree.Hunting;
            hunting5b.TreeName = "Hunting";
            hunting5b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting5b);

            Talent hunting5c = new Talent();
            hunting5c.Name = "Killing Spree";
            hunting5c.Type = TalentType.Benefit;
            hunting5c.Action = ActionType.None;
            hunting5c.DescriptionFluff = "Your rage has built up into a rampage, and all who stand before you must die.";
            hunting5c.Description = "When you incapacitate an opponent with an attack with a Longarm, gain a +2 bonus to attack damage with Longarm attacks until the end of your next turn.";
            hunting5c.ClarifyingText = "";
            hunting5c.StaminaCost = null;
            hunting5c.UpkeepCost = null;
            hunting5c.Tier = 5;
            hunting5c.TierBenefitDescription = "Gain +1 Accuracy and Vicious with Longarms";
            hunting5c.Tree = TalentTree.Hunting;
            hunting5c.TreeName = "Hunting";
            hunting5c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(hunting5c);
            #endregion
            #endregion
            #region Hurling (Thrown)
            #region T1
            Talent hurling1a = new Talent();
            hurling1a.Name = "Reaction Throw";
            hurling1a.Type = TalentType.TriggeredAction;
            hurling1a.Action = ActionType.Reaction;
            hurling1a.DescriptionFluff = "You are not afraid.";
            hurling1a.Description = "Draw a Readied Thrown weapon and make a Thrown attack, ignoring the Point Blank range penalty.";
            hurling1a.ClarifyingText = "Triggering Action: an opponent creates an Opening for you";
            hurling1a.StaminaCost = 2;
            hurling1a.UpkeepCost = null;
            hurling1a.Tier = 1;
            hurling1a.TierBenefitDescription = "Weapons with the Thrown Property count as ½ size when Holstered";
            hurling1a.Tree = TalentTree.Hurling;
            hurling1a.TreeName = "Hurling";
            hurling1a.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling1a);

            Talent hurling1b = new Talent();
            hurling1b.Name = "Mighty Throw";
            hurling1b.Type = TalentType.Maneuver;
            hurling1b.Action = ActionType.Combat;
            hurling1b.DescriptionFluff = "You hurl your weapon with all your might, hurling your opponent";
            hurling1b.Description = "Make a Thrown attack. The attack gains Knockback.";
            hurling1b.ClarifyingText = "";
            hurling1b.StaminaCost = 4;
            hurling1b.UpkeepCost = null;
            hurling1b.Tier = 1;
            hurling1b.TierBenefitDescription = "Weapons with the Thrown Property count as ½ size when Holstered";
            hurling1b.Tree = TalentTree.Hurling;
            hurling1b.TreeName = "Hurling";
            hurling1b.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling1b);

            Talent hurling1c = new Talent();
            hurling1c.Name = "Hurling Stance";
            hurling1c.Type = TalentType.Maneuver;
            hurling1c.Action = ActionType.Combat;
            hurling1c.DescriptionFluff = "You plant your feet and grab your trusty weapons.";
            hurling1c.Description = "You are immobile, gain a +1 to attack, and can draw weapons with the Thrown property without spending MI.";
            hurling1c.ClarifyingText = "";
            hurling1c.StaminaCost = 4;
            hurling1c.UpkeepCost = 1;
            hurling1c.Tier = 1;
            hurling1c.TierBenefitDescription = "Weapons with the Thrown Property count as ½ size when Holstered";
            hurling1c.Tree = TalentTree.Hurling;
            hurling1c.TreeName = "Hurling";
            hurling1c.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling1c);
            #endregion
            #region T2
            Talent thrown2a = new Talent();
            thrown2a.Name = "Snatch";
            thrown2a.Type = TalentType.TriggeredAction;
            thrown2a.Action = ActionType.Reaction;
            thrown2a.DescriptionFluff = "You grab the incoming projectile right out of the air.";
            thrown2a.Description = "Add 1d6-2 to your (Ranged/Physical) Defense. If you are missed, you can become armed with the projectile if you choose. [3 Stamaina]";
            thrown2a.ClarifyingText = "Triggered Action: you are hit by a (Ranged/Physical) attack that does not cause Ballistic damage";
            thrown2a.StaminaCost = 3;
            thrown2a.UpkeepCost = null;
            thrown2a.Tier = 2;
            thrown2a.TierBenefitDescription = "Gain +1 damage with Thrown weapons";
            thrown2a.Tree = TalentTree.Hurling;
            thrown2a.TreeName = "Hurling";
            thrown2a.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(thrown2a);

            Talent thrown2b = new Talent();
            thrown2b.Name = "Snap Throw";
            thrown2b.Type = TalentType.Trick;
            thrown2b.Action = ActionType.Quick;
            thrown2b.DescriptionFluff = "You draw and loose the weapon in one fluid action.";
            thrown2b.Description = "Weapon {Ranged -2/-2} [6 Stamina]";
            thrown2b.ClarifyingText = "You can use this Trick once per round immediately following the drawing of the weapon.";
            thrown2b.StaminaCost = 6;
            thrown2b.UpkeepCost = null;
            thrown2b.Tier = 2;
            thrown2b.TierBenefitDescription = "Gain +1 damage with Thrown weapons";
            thrown2b.Tree = TalentTree.Hurling;
            thrown2b.TreeName = "Hurling";
            thrown2b.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(thrown2b);

            Talent thrown2c = new Talent();
            thrown2c.Name = "Boomerang Throw";
            thrown2c.Type = TalentType.AttackAugment;
            thrown2c.Action = ActionType.Quick;
            thrown2c.DescriptionFluff = "You angle your throw to cause the projectile to violently curve.";
            thrown2c.Description = "On a miss, the weapon returns to you. [2 Stamina]";
            thrown2c.ClarifyingText = "";
            thrown2c.StaminaCost = 2;
            thrown2c.UpkeepCost = null;
            thrown2c.Tier = 2;
            thrown2c.TierBenefitDescription = "Gain +1 damage with Thrown weapons";
            thrown2c.Tree = TalentTree.Hurling;
            thrown2c.TreeName = "Hurling";
            thrown2c.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(thrown2c);
            #endregion
            #region T3
            Talent hurling3a = new Talent();
            hurling3a.Name = "Double-fisting";
            hurling3a.Type = TalentType.Benefit;
            hurling3a.Action = ActionType.None;
            hurling3a.DescriptionFluff = "Your ambidexterity enhances the power of your off-hand attacks.";
            hurling3a.Description = "When making the off-hand attack granted by Two-Weapon Fighting, you can choose to apply 1 Attack Augment to the off-hand attack.";
            hurling3a.ClarifyingText = "";
            hurling3a.StaminaCost = null;
            hurling3a.UpkeepCost = null;
            hurling3a.Tier = 3;
            hurling3a.TierBenefitDescription = "You can arm yourself with Readied, Light, Thrown weapons as a Free Action or as part of any Triggered Action.";
            hurling3a.Tree = TalentTree.Hurling;
            hurling3a.TreeName = "Hurling";
            hurling3a.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling3a);

            Talent hurling3b = new Talent();
            hurling3b.Name = "Mark of the Ninja";
            hurling3b.Type = TalentType.Benefit;
            hurling3b.Action = ActionType.None;
            hurling3b.DescriptionFluff = "Your throws are subtle and quiet.";
            hurling3b.Description = "Thrown attacks with non-explosive weapons do not automatically cause you to lose the Hidden state from onlookers (though it does from the target of the attack). After the attack is resolved, make a new Stealth Check at a - 2; you and must have Cover or Concealment to remain hidden. All other creatures in the area get a new Perception Check against the new MCR.";
            hurling3b.ClarifyingText = "";
            hurling3b.StaminaCost = null;
            hurling3b.UpkeepCost = null;
            hurling3b.Tier = 3;
            hurling3b.TierBenefitDescription = "You can arm yourself with Readied, Light, Thrown weapons as a Free Action or as part of any Triggered Action.";
            hurling3b.Tree = TalentTree.Hurling;
            hurling3b.TreeName = "Hurling";
            hurling3b.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling3b);

            Talent hurling3c = new Talent();
            hurling3c.Name = "Golden Arm";
            hurling3c.Type = TalentType.AttackAugment;
            hurling3c.Action = ActionType.Quick;
            hurling3c.DescriptionFluff = "You whip a weapon at incredible velocity.";
            hurling3c.Description = "Your next attack uses SMG range instead of normal Thrown ranges. [4 Stamina]";
            hurling3c.ClarifyingText = "";
            hurling3c.StaminaCost = 4;
            hurling3c.UpkeepCost = null;
            hurling3c.Tier = 3;
            hurling3c.TierBenefitDescription = "You can arm yourself with Readied, Light, Thrown weapons as a Free Action or as part of any Triggered Action.";
            hurling3c.Tree = TalentTree.Hurling;
            hurling3c.TreeName = "Hurling";
            hurling3c.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling3c);
            #endregion
            #region T4
            Talent hurling4a = new Talent();
            hurling4a.Name = "Pinpoint";
            hurling4a.Type = TalentType.Benefit;
            hurling4a.Action = ActionType.None;
            hurling4a.DescriptionFluff = "You are startlingly accurate in the thick of battle.";
            hurling4a.Description = "Your thrown attacks do not suffer penalties to attack from intervening creatures.";
            hurling4a.ClarifyingText = "";
            hurling4a.StaminaCost = null;
            hurling4a.UpkeepCost = null;
            hurling4a.Tier = 4;
            hurling4a.TierBenefitDescription = "Gain Vicious +1 with Thrown weapons";
            hurling4a.Tree = TalentTree.Hurling;
            hurling4a.TreeName = "Hurling";
            hurling4a.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling4a);

            Talent hurling4b = new Talent();
            hurling4b.Name = "Payback";
            hurling4b.Type = TalentType.TriggeredAction;
            hurling4b.Action = ActionType.Reaction;
            hurling4b.DescriptionFluff = "That attack will not go unanswered.";
            hurling4b.Description = "Weapon {Ranged +0/+0} Make this attack targeting the creature that made the Triggering attack. [5 Stamina or 3 Stamina if following a successful Snatch]";
            hurling4b.ClarifyingText = "Triggering Action: you are missed by a Ranged attack";
            hurling4b.StaminaCost = 5;
            hurling4b.UpkeepCost = null;
            hurling4b.Tier = 4;
            hurling4b.TierBenefitDescription = "Gain Vicious +1 with Thrown weapons";
            hurling4b.Tree = TalentTree.Hurling;
            hurling4b.TreeName = "Hurling";
            hurling4b.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling4b);

            Talent hurling4c = new Talent();
            hurling4c.Name = "Double Throw";
            hurling4c.Type = TalentType.Maneuver;
            hurling4c.Action = ActionType.Combat;
            hurling4c.DescriptionFluff = "You’re more of a “quantity over quality” kinda guy.";
            hurling4c.Description = "Weapon {Ranged -2/double damage modifier} [10 Stamina]";
            hurling4c.ClarifyingText = "Make this attack twice against 1 target, or once against 2 different targets. Each attack uses 2 Light, Thrown weapons. Use the attack, damage, modifiers, and properties of either of the weapons used in each attack (choose 1).";
            hurling4c.StaminaCost = 10;
            hurling4c.UpkeepCost = null;
            hurling4c.Tier = 4;
            hurling4c.TierBenefitDescription = "Gain Vicious +1 with Thrown weapons";
            hurling4c.Tree = TalentTree.Hurling;
            hurling4c.TreeName = "Hurling";
            hurling4c.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling4c);
            #endregion
            #region T5
            Talent hurling5a = new Talent();
            hurling5a.Name = "Storm of Throws";
            hurling5a.Type = TalentType.Maneuver;
            hurling5a.Action = ActionType.Combat;
            hurling5a.DescriptionFluff = "Your hands are a blur as you chuck blades by the handful at every enemy before you.";
            hurling5a.Description = "Weapon {Area (60’ cone) +0/+0} [12 Stamina]";
            hurling5a.ClarifyingText = "You use at least 1 Thrown weapon per creature you target with this attack.";
            hurling5a.StaminaCost = 12;
            hurling5a.UpkeepCost = null;
            hurling5a.Tier = 5;
            hurling5a.TierBenefitDescription = "Gain +1 Accuracy and CM with Thrown weapons";
            hurling5a.Tree = TalentTree.Hurling;
            hurling5a.TreeName = "Hurling";
            hurling5a.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling5a);

            Talent hurling5b = new Talent();
            hurling5b.Name = "Eye Socket";
            hurling5b.Type = TalentType.AttackAugment;
            hurling5b.Action = ActionType.Quick;
            hurling5b.DescriptionFluff = "It’s time to remove the enemy from battle.";
            hurling5b.Description = "Your next Ranged attack has double your normal CM. Your next attack is treated as 1 Stage higher Crit than the attack roll indicates. [6 Stamina]";
            hurling5b.ClarifyingText = "";
            hurling5b.StaminaCost = 6;
            hurling5b.UpkeepCost = null;
            hurling5b.Tier = 5;
            hurling5b.TierBenefitDescription = "Gain +1 Accuracy and CM with Thrown weapons";
            hurling5b.Tree = TalentTree.Hurling;
            hurling5b.TreeName = "Hurling";
            hurling5b.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling5b);

            Talent hurling5c = new Talent();
            hurling5c.Name = "Combat Opportunist";
            hurling5c.Type = TalentType.Stance;
            hurling5c.Action = ActionType.Quick;
            hurling5c.DescriptionFluff = "You are quick to seize an opportunity when presented.";
            hurling5c.Description = "If an enemy within 50’ of you and within line of sight creates an Opening for an ally of yours, you can make a Reaction Attack against it using a Thrown weapon instead of a Melee weapon. [12/3 Stamina]";
            hurling5c.ClarifyingText = "";
            hurling5c.StaminaCost = 12;
            hurling5c.UpkeepCost = 3;
            hurling5c.Tier = 5;
            hurling5c.TierBenefitDescription = "Gain +1 Accuracy and CM with Thrown weapons";
            hurling5c.Tree = TalentTree.Hurling;
            hurling5c.TreeName = "Hurling";
            hurling5c.LinkedSkill = WeaponSkill.Thrown;
            Talents.Add(hurling5c);
            #endregion
            #endregion
            #region Martial Arts (Unarmed)
            #region T1
            Talent martialArts1a = new Talent();
            martialArts1a.Name = "Leg Sweep";
            martialArts1a.Type = TalentType.Maneuver;
            martialArts1a.Action = ActionType.Combat;
            martialArts1a.DescriptionFluff = "You kick your opponent’s legs out from under him.";
            martialArts1a.Description = "Weapon {Melee -2/+0} [4 Stamina]";
            martialArts1a.ClarifyingText = "On a hit, the target becomes Prone.";
            martialArts1a.StaminaCost = 4;
            martialArts1a.UpkeepCost = null;
            martialArts1a.Tier = 1;
            martialArts1a.TierBenefitDescription = "Gain +1 to Initiative and +1 to Athletics";
            martialArts1a.Tree = TalentTree.MartialArts;
            martialArts1a.TreeName = "Martial Arts";
            martialArts1a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts1a);

            Talent martialArts1b = new Talent();
            martialArts1b.Name = "Roundhouse";
            martialArts1b.Type = TalentType.Maneuver;
            martialArts1b.Action = ActionType.Combat;
            martialArts1b.DescriptionFluff = "Your spinning kick packs some serious punch.";
            martialArts1b.Description = "Weapon {Melee +0/+2} [4 Stamina]";
            martialArts1b.ClarifyingText = "Make an Unarmed Melee attack with Knockback.";
            martialArts1b.StaminaCost = 4;
            martialArts1b.UpkeepCost = null;
            martialArts1b.Tier = 1;
            martialArts1b.TierBenefitDescription = "Gain +1 to Initiative and +1 to Athletics";
            martialArts1b.Tree = TalentTree.MartialArts;
            martialArts1b.TreeName = "Martial Arts";
            martialArts1b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts1b);

            Talent martialArts1c = new Talent();
            martialArts1c.Name = "Fluid Defense";
            martialArts1c.Type = TalentType.Maneuver;
            martialArts1c.Action = ActionType.Combat;
            martialArts1c.DescriptionFluff = "You flow like the waves to avoid your enemies’ attacks.";
            martialArts1c.Description = "While active, gain a +1 to all Defenses. [4/1 Stamina]";
            martialArts1c.ClarifyingText = "";
            martialArts1c.StaminaCost = 4;
            martialArts1c.UpkeepCost = 1;
            martialArts1c.Tier = 1;
            martialArts1c.TierBenefitDescription = "Gain +1 to Initiative and +1 to Athletics";
            martialArts1c.Tree = TalentTree.MartialArts;
            martialArts1c.TreeName = "Martial Arts";
            martialArts1c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts1c);
            #endregion
            #region T2
            Talent martialArts2a = new Talent();
            martialArts2a.Name = "Aikido Training";
            martialArts2a.Type = TalentType.Benefit;
            martialArts2a.Action = ActionType.None;
            martialArts2a.DescriptionFluff = "You are trained at liberating your enemies’ weapons from their grips.";
            martialArts2a.Description = "You gain a +4 to Unarmed Disarm attacks and can become armed with the weapon if you succeed.";
            martialArts2a.ClarifyingText = "";
            martialArts2a.StaminaCost = null;
            martialArts2a.UpkeepCost = null;
            martialArts2a.Tier = 2;
            martialArts2a.TierBenefitDescription = "Gain +1 to Unarmed CM";
            martialArts2a.Tree = TalentTree.MartialArts;
            martialArts2a.TreeName = "Martial Arts";
            martialArts2a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts2a);

            Talent martialArts2b = new Talent();
            martialArts2b.Name = "Footwork";
            martialArts2b.Type = TalentType.Benefit;
            martialArts2b.Action = ActionType.None;
            martialArts2b.DescriptionFluff = "You are accustomed to regular use of your legs as weapons.";
            martialArts2b.Description = "You may make Unarmed attacks at no penalty while bound at the wrists, Wrestling, or Locked. You can make off-hand Unarmed attacks while wielding a Melee weapon in 2 hands.";
            martialArts2b.ClarifyingText = "";
            martialArts2b.StaminaCost = null;
            martialArts2b.UpkeepCost = null;
            martialArts2b.Tier = 2;
            martialArts2b.TierBenefitDescription = "Gain +1 to Unarmed CM";
            martialArts2b.Tree = TalentTree.MartialArts;
            martialArts2b.TreeName = "Martial Arts";
            martialArts2b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts2b);

            Talent martialArts2c = new Talent();
            martialArts2c.Name = "Rapid Strike";
            martialArts2c.Type = TalentType.Maneuver;
            martialArts2c.Action = ActionType.Combat;
            martialArts2c.DescriptionFluff = "Your limbs become a blur as you batter your opponents.";
            martialArts2c.Description = "Weapon {Melee +0/-2} [6 Stamina]";
            martialArts2c.ClarifyingText = "Make this attack twice against 1 opponent or once against 2 different opponents.";
            martialArts2c.StaminaCost = 6;
            martialArts2c.UpkeepCost = null;
            martialArts2c.Tier = 2;
            martialArts2c.TierBenefitDescription = "Gain +1 to Unarmed CM";
            martialArts2c.Tree = TalentTree.MartialArts;
            martialArts2c.TreeName = "Martial Arts";
            martialArts2c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts2c);
            #endregion
            #region T3
            Talent martialArts3a = new Talent();
            martialArts3a.Name = "Centered Defense";
            martialArts3a.Type = TalentType.Benefit;
            martialArts3a.Action = ActionType.None;
            martialArts3a.DescriptionFluff = "The power of your concentration shores up your Defenses.";
            martialArts3a.Description = "When you are in a Stance that grants bonuses to Defense or Durability, the bonus is increased by 1.";
            martialArts3a.ClarifyingText = "";
            martialArts3a.StaminaCost = null;
            martialArts3a.UpkeepCost = null;
            martialArts3a.Tier = 3;
            martialArts3a.TierBenefitDescription = "Gain +1 to Melee Defenses";
            martialArts3a.Tree = TalentTree.MartialArts;
            martialArts3a.TreeName = "Martial Arts";
            martialArts3a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts3a);

            Talent martialArts3b = new Talent();
            martialArts3b.Name = "Kick Training";
            martialArts3b.Type = TalentType.AttackAugment;
            martialArts3b.Action = ActionType.Quick;
            martialArts3b.DescriptionFluff = "You use your feet and knees like others use hammers and maces.";
            martialArts3b.Description = "You may take a -2 to attack to gain a +4 to damage when making Unarmed attacks. [0 Stamina]";
            martialArts3b.ClarifyingText = "";
            martialArts3b.StaminaCost = 0;
            martialArts3b.UpkeepCost = null;
            martialArts3b.Tier = 3;
            martialArts3b.TierBenefitDescription = "Gain +1 to Melee Defenses";
            martialArts3b.Tree = TalentTree.MartialArts;
            martialArts3b.TreeName = "Martial Arts";
            martialArts3b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts3b);

            Talent martialArts3c = new Talent();
            martialArts3c.Name = "Lethal Strike";
            martialArts3c.Type = TalentType.AttackAugment;
            martialArts3c.Action = ActionType.Quick;
            martialArts3c.DescriptionFluff = "You capitalize on a moment of weakness to crush your opponent.";
            martialArts3c.Description = "Your next Unarmed attack against a Vulnerable target gains Lethal 1 and CM +2. [4 Stamina]";
            martialArts3c.ClarifyingText = "";
            martialArts3c.StaminaCost = 4;
            martialArts3c.UpkeepCost = null;
            martialArts3c.Tier = 3;
            martialArts3c.TierBenefitDescription = "Gain +1 to Melee Defenses";
            martialArts3c.Tree = TalentTree.MartialArts;
            martialArts3c.TreeName = "Martial Arts";
            martialArts3c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts3c);
            #endregion
            #region T4
            Talent martialArts4a = new Talent();
            martialArts4a.Name = "Knee Kick";
            martialArts4a.Type = TalentType.Maneuver;
            martialArts4a.Action = ActionType.Combat;
            martialArts4a.DescriptionFluff = "You smash your foot into the knee of your opponent.";
            martialArts4a.Description = "Weapon {Melee +2/+2} [10 Stamina]";
            martialArts4a.ClarifyingText = "If your target is damaged by this attack, he becomes Prone and gains the Slowed and Vulnerable Conditions (until Resisted). The MCR to resist Conditions resulting from this attack are increased by the Stage of Crit that this attack has (if any).";
            martialArts4a.StaminaCost = 10;
            martialArts4a.UpkeepCost = null;
            martialArts4a.Tier = 4;
            martialArts4a.TierBenefitDescription = "Unarmed attacks gain the Penetrating Quality";
            martialArts4a.Tree = TalentTree.MartialArts;
            martialArts4a.TreeName = "Martial Arts";
            martialArts4a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts4a);

            Talent martialArts4b = new Talent();
            martialArts4b.Name = "Counterattack";
            martialArts4b.Type = TalentType.TriggeredAction;
            martialArts4b.Action = ActionType.Reaction;
            martialArts4b.DescriptionFluff = "You duck your opponents attack only to smash them back with a surprising burst of offense.";
            martialArts4b.Description = "Weapon {Melee +0/+0} [5 Stamina]";
            martialArts4b.ClarifyingText = "Triggering Action: you are missed by a Melee attack.  Make this attack against the creature that made the Triggering attack. This attack has Knockback if you choose.";
            martialArts4b.StaminaCost = null;
            martialArts4b.UpkeepCost = null;
            martialArts4b.Tier = 4;
            martialArts4b.TierBenefitDescription = "Unarmed attacks gain the Penetrating Quality";
            martialArts4b.Tree = TalentTree.MartialArts;
            martialArts4b.TreeName = "Martial Arts";
            martialArts4b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts4b);

            Talent martialArts4c = new Talent();
            martialArts4c.Name = "Pressure Point";
            martialArts4c.Type = TalentType.Maneuver;
            martialArts4c.Action = ActionType.Combat;
            martialArts4c.DescriptionFluff = "By pinching the correct nerves, you can immobilize your opponent.";
            martialArts4c.Description = "Weapon {Melee +0/+0}[10 Stamina]";
            martialArts4c.ClarifyingText = "Targets hit by this attack gain the Weakened and Slowed Conditions (until Resisted). If a Weakened or Slowed target is hit by this attack they also become Dazed (until Resisted).";
            martialArts4c.StaminaCost = 10;
            martialArts4c.UpkeepCost = null;
            martialArts4c.Tier = 4;
            martialArts4c.TierBenefitDescription = "Unarmed attacks gain the Penetrating Quality";
            martialArts4c.Tree = TalentTree.MartialArts;
            martialArts4c.TreeName = "Martial Arts";
            martialArts4c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts4c);
            #endregion
            #region T5
            Talent martialArts5a = new Talent();
            martialArts5a.Name = "Dance of Death";
            martialArts5a.Type = TalentType.Maneuver;
            martialArts5a.Action = ActionType.Combat;
            martialArts5a.DescriptionFluff = "You twirl between your opponents delivering punishment like presents.";
            martialArts5a.Description = "Weapon {Melee +0/+0} [12 Stamina]";
            martialArts5a.ClarifyingText = "Make this attack against up to 4 different opponents. You can spend your MI between these attacks.";
            martialArts5a.StaminaCost = 12;
            martialArts5a.UpkeepCost = null;
            martialArts5a.Tier = 5;
            martialArts5a.TierBenefitDescription = "Gain +1 to Melee Defenses and +1 to Accuracy of Unarmed attacks";
            martialArts5a.Tree = TalentTree.MartialArts;
            martialArts5a.TreeName = "Martial Arts";
            martialArts5a.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts5a);

            Talent martialArts5b = new Talent();
            martialArts5b.Name = "Viper Stance";
            martialArts5b.Type = TalentType.Stance;
            martialArts5b.Action = ActionType.Quick;
            martialArts5b.DescriptionFluff = "You adopt an elusive posture that holds surprising power.";
            martialArts5b.Description = "Gain a +1 to all Defenses and a +2 to attack, damage, and CM of Unarmed attacks. [12/3 Stamina]";
            martialArts5b.ClarifyingText = "";
            martialArts5b.StaminaCost = 12;
            martialArts5b.UpkeepCost = 3;
            martialArts5b.Tier = 5;
            martialArts5b.TierBenefitDescription = "Gain +1 to Melee Defenses and +1 to Accuracy of Unarmed attacks";
            martialArts5b.Tree = TalentTree.MartialArts;
            martialArts5b.TreeName = "Martial Arts";
            martialArts5b.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts5b);

            Talent martialArts5c = new Talent();
            martialArts5c.Name = "Death Strike";
            martialArts5c.Type = TalentType.Maneuver;
            martialArts5c.Action = ActionType.Combat;
            martialArts5c.DescriptionFluff = "You focus all your skill, power, experience, and concentration into a single killing blow.";
            martialArts5c.Description = "Weapon {Melee +2/+0} [12 Stamina]";
            martialArts5c.ClarifyingText = "Double the total damage of this attack (including all modifiers). This attack also gains Knockback.";
            martialArts5c.StaminaCost = null;
            martialArts5c.UpkeepCost = null;
            martialArts5c.Tier = 5;
            martialArts5c.TierBenefitDescription = "Gain +1 to Melee Defenses and +1 to Accuracy of Unarmed attacks";
            martialArts5c.Tree = TalentTree.MartialArts;
            martialArts5c.TreeName = "Martial Arts";
            martialArts5c.LinkedSkill = WeaponSkill.Unarmed;
            Talents.Add(martialArts5c);
            #endregion
            #endregion
            #region Skirmishing (Close Combat)
            #region T1
            Talent skirmishing1a = new Talent();
            skirmishing1a.Name = "Parry";
            skirmishing1a.Type = TalentType.TriggeredAction;
            skirmishing1a.Action = ActionType.Reaction;
            skirmishing1a.DescriptionFluff = "You intercept your opponent’s attack with your weapon.";
            skirmishing1a.Description = "Gain +1d6-2 to Defense vs. the Triggering attack. [2 Stamina]";
            skirmishing1a.ClarifyingText = "Triggering Action: a Melee attack targets you";
            skirmishing1a.StaminaCost = null;
            skirmishing1a.UpkeepCost = null;
            skirmishing1a.Tier = 1;
            skirmishing1a.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishing1a.Tree = TalentTree.Skirmishing;
            skirmishing1a.TreeName = "Skirmishing";
            skirmishing1a.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing1a);

            Talent skirmishing1b = new Talent();
            skirmishing1b.Name = "Feint";
            skirmishing1b.Type = TalentType.AttackAugment;
            skirmishing1b.Action = ActionType.Quick;
            skirmishing1b.DescriptionFluff = "You fake your attack to leave your opponent momentarily exposed.";
            skirmishing1b.Description = "The target of this attack becomes Vulnerable to the next attack made against it. [2 Stamina]";
            skirmishing1b.ClarifyingText = "";
            skirmishing1b.StaminaCost = null;
            skirmishing1b.UpkeepCost = null;
            skirmishing1b.Tier = 1;
            skirmishing1b.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishing1b.Tree = TalentTree.Skirmishing;
            skirmishing1b.TreeName = "Skirmishing";
            skirmishing1b.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing1b);

            Talent skirmishing1c = new Talent();
            skirmishing1c.Name = "Quick Draw";
            skirmishing1c.Type = TalentType.Benefit;
            skirmishing1c.Action = ActionType.None;
            skirmishing1c.DescriptionFluff = "All that practice in the mirror has finally paid off.";
            skirmishing1c.Description = "Gain +2 to Initiative.";
            skirmishing1c.ClarifyingText = "";
            skirmishing1c.StaminaCost = null;
            skirmishing1c.UpkeepCost = null;
            skirmishing1c.Tier = 1;
            skirmishing1c.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishing1c.Tree = TalentTree.Skirmishing;
            skirmishing1c.TreeName = "Skirmishing";
            skirmishing1c.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing1c);
            #endregion
            #region T2
            Talent skirmishing2a = new Talent();
            skirmishing2a.Name = "Double-Fisting";
            skirmishing2a.Type = TalentType.Benefit;
            skirmishing2a.Action = ActionType.None;
            skirmishing2a.DescriptionFluff = "Your ambidexterity enhances the power of your off-hand attacks.";
            skirmishing2a.Description = "You can choose to apply 1 Attack Augment to the off-hand attack granted by Two-Weapon Fighting.";
            skirmishing2a.ClarifyingText = "";
            skirmishing2a.StaminaCost = null;
            skirmishing2a.UpkeepCost = null;
            skirmishing2a.Tier = 2;
            skirmishing2a.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishing2a.Tree = TalentTree.Skirmishing;
            skirmishing2a.TreeName = "Skirmishing";
            skirmishing2a.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing2a);

            Talent skirmishing2b = new Talent();
            skirmishing2b.Name = "Follow-up Strike";
            skirmishing2b.Type = TalentType.Maneuver;
            skirmishing2b.Action = ActionType.Combat;
            skirmishing2b.DescriptionFluff = "You dart through the battle dispatching foes left and right.";
            skirmishing2b.Description = "Weapon {Melee +0/+0} [6 Stamina]";
            skirmishing2b.ClarifyingText = "Make this attack, spend up to 3 MI, and make this attack again against a different opponent.";
            skirmishing2b.StaminaCost = 6;
            skirmishing2b.UpkeepCost = null;
            skirmishing2b.Tier = 2;
            skirmishing2b.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishing2b.Tree = TalentTree.Skirmishing;
            skirmishing2b.TreeName = "Skirmishing";
            skirmishing2b.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing2b);

            Talent skirmishing2c = new Talent();
            skirmishing2c.Name = "Hack and Slash";
            skirmishing2c.Type = TalentType.Benefit;
            skirmishing2c.Action = ActionType.None;
            skirmishing2c.DescriptionFluff = "You are well-versed at overpowering opponents with the speed and volume of your attacks.";
            skirmishing2c.Description = "If you hit the same opponent with both of your weapons in the same round while Two-Weapon Fighting, that opponent loses additional HP equal to either your Strength or Agility.";
            skirmishing2c.ClarifyingText = "";
            skirmishing2c.StaminaCost = null;
            skirmishing2c.UpkeepCost = null;
            skirmishing2c.Tier = 2;
            skirmishing2c.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishing2c.Tree = TalentTree.Skirmishing;
            skirmishing2c.TreeName = "Skirmishing";
            skirmishing2c.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing2c);
            #endregion
            #region T3
            Talent skirmishing3a = new Talent();
            skirmishing3a.Name = "Quick Strikes";
            skirmishing3a.Type = TalentType.Maneuver;
            skirmishing3a.Action = ActionType.Combat;
            skirmishing3a.DescriptionFluff = "Your lightning blows are difficult to avoid.";
            skirmishing3a.Description = "Weapon {Melee +2/+0} [8 Stamina]";
            skirmishing3a.ClarifyingText = "Make this attack twice.";
            skirmishing3a.StaminaCost = 8;
            skirmishing3a.UpkeepCost = null;
            skirmishing3a.Tier = 3;
            skirmishing3a.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishing3a.Tree = TalentTree.Skirmishing;
            skirmishing3a.TreeName = "Skirmishing";
            skirmishing3a.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing3a);

            Talent skirmishing3b = new Talent();
            skirmishing3b.Name = "Florentine Fighting Stance";
            skirmishing3b.Type = TalentType.Stance;
            skirmishing3b.Action = ActionType.Quick;
            skirmishing3b.DescriptionFluff = "You adopt an ancient but highly effective dual-weapon fighting pose.";
            skirmishing3b.Description = "While active, both the main hand and off-hand attacks from Two-Weapon Fighting gain +1 to attack and + 2 to damage. [8/2 Stamina]";
            skirmishing3b.ClarifyingText = "";
            skirmishing3b.StaminaCost = 8;
            skirmishing3b.UpkeepCost = 2;
            skirmishing3b.Tier = 3;
            skirmishing3b.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishing3b.Tree = TalentTree.Skirmishing;
            skirmishing3b.TreeName = "Skirmishing";
            skirmishing3b.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing3b);

            Talent skirmishing3c = new Talent();
            skirmishing3c.Name = "Riposte";
            skirmishing3c.Type = TalentType.TriggeredAction;
            skirmishing3c.Action = ActionType.Reaction;
            skirmishing3c.DescriptionFluff = "You jab back at your enemy, drawing blood.";
            skirmishing3c.Description = "Weapon {Melee +0/+0} [4 Stamina or 2 Stamina if following a successful Parry]";
            skirmishing3c.ClarifyingText = "Triggering Action: you were missed by a Melee attack)";
            skirmishing3c.StaminaCost = 4;
            skirmishing3c.UpkeepCost = null;
            skirmishing3c.Tier = 3;
            skirmishing3c.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishing3c.Tree = TalentTree.Skirmishing;
            skirmishing3c.TreeName = "Skirmishing";
            skirmishing3c.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing3c);
            #endregion
            #region T4
            Talent skirmishing4a = new Talent();
            skirmishing4a.Name = "Custom Weapon";
            skirmishing4a.Type = TalentType.Benefit;
            skirmishing4a.Action = ActionType.None;
            skirmishing4a.DescriptionFluff = "Your constant tinkering with your weapon have fine tuned it to perfection.";
            skirmishing4a.Description = "Choose 1 weapon that you own that uses the skill linked to this Talent Tree. You gain an additional Mod Slot in that weapon. This Talent requires 24 hours to apply to a weapon, or to change the weapon to which it applies. This Talent can be taken multiple times, each time applying it to a different weapon in your arsenal.";
            skirmishing4a.ClarifyingText = "";
            skirmishing4a.StaminaCost = null;
            skirmishing4a.UpkeepCost = null;
            skirmishing4a.Tier = 4;
            skirmishing4a.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishing4a.Tree = TalentTree.Skirmishing;
            skirmishing4a.TreeName = "Skirmishing";
            skirmishing4a.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing4a);

            Talent skirmishing4b = new Talent();
            skirmishing4b.Name = "Stab and Throw";
            skirmishing4b.Type = TalentType.TriggeredAction;
            skirmishing4b.Action = ActionType.Reaction;
            skirmishing4b.DescriptionFluff = "ou throw your trusty blade at your enemy, faster than anyone can see.";
            skirmishing4b.Description = "Weapon {Ranged +0/+0} [5 Stamina]";
            skirmishing4b.ClarifyingText = "Make this attack by throwing the Triggering weapon. This attack is not considered Improvised, does not create an Opening, and ignores Intervening creatures. Triggering Action: you successfully hit with a Melee attack";
            skirmishing4b.StaminaCost = 5;
            skirmishing4b.UpkeepCost = null;
            skirmishing4b.Tier = 4;
            skirmishing4b.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishing4b.Tree = TalentTree.Skirmishing;
            skirmishing4b.TreeName = "Skirmishing";
            skirmishing4b.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing4b);

            Talent skirmishing4c = new Talent();
            skirmishing4c.Name = "Whirling Dervish";
            skirmishing4c.Type = TalentType.AttackAugment;
            skirmishing4c.Action = ActionType.Quick;
            skirmishing4c.DescriptionFluff = "You eviscerate your opponent with rapid strikes from your off-hand.";
            skirmishing4c.Description = "Your next off-hand attack from Two-Weapon Fighting is made twice against the same target with the normal penalties for Two-Weapon Fighting.";
            skirmishing4c.ClarifyingText = "";
            skirmishing4c.StaminaCost = 5;
            skirmishing4c.UpkeepCost = null;
            skirmishing4c.Tier = 4;
            skirmishing4c.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishing4c.Tree = TalentTree.Skirmishing;
            skirmishing4c.TreeName = "Skirmishing";
            skirmishing4c.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing4c);
            #endregion
            #region T5
            Talent skirmishing5a = new Talent();
            skirmishing5a.Name = "Dance of Death";
            skirmishing5a.Type = TalentType.Maneuver;
            skirmishing5a.Action = ActionType.Combat;
            skirmishing5a.DescriptionFluff = "You twirl between your opponents delivering punishment like presents.";
            skirmishing5a.Description = "Weapon {Melee +0/+0} [12 Stamina]";
            skirmishing5a.ClarifyingText = "Make this attack against up to 4 different opponents. You can spend your MI between these attacks.";
            skirmishing5a.StaminaCost = 12;
            skirmishing5a.UpkeepCost = null;
            skirmishing5a.Tier = 5;
            skirmishing5a.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishing5a.Tree = TalentTree.Skirmishing;
            skirmishing5a.TreeName = "Skirmishing";
            skirmishing5a.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing5a);

            Talent skirmishing5b = new Talent();
            skirmishing5b.Name = "Wall of Blades Stance";
            skirmishing5b.Type = TalentType.Stance;
            skirmishing5b.Action = ActionType.Quick;
            skirmishing5b.DescriptionFluff = "Your weapons protectively enshroud you while offering death to nearby enemies.";
            skirmishing5b.Description = "Gain +6 to all Defenses. You cannot take Combat Actions, but when missed by a Melee attack, the attacker creates an Opening for you. Your Triggered Actions cost 2 fewer Stamina.When missed by a Ranged attack, you may redirect the attack to another target within 20 feet of you as a Triggered Action for 2 Stamina(after the cost reduction). That attack uses the original attack roll against the new target. [12/3 Stamina]";
            skirmishing5b.ClarifyingText = "";
            skirmishing5b.StaminaCost = 12;
            skirmishing5b.UpkeepCost = 3;
            skirmishing5b.Tier = 5;
            skirmishing5b.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishing5b.Tree = TalentTree.Skirmishing;
            skirmishing5b.TreeName = "Skirmishing";
            skirmishing5b.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing5b);

            Talent skirmishing5c = new Talent();
            skirmishing5c.Name = "Whirlwind";
            skirmishing5c.Type = TalentType.Maneuver;
            skirmishing5c.Action = ActionType.Combat;
            skirmishing5c.DescriptionFluff = "Your weapon leads the charge as you turn in mighty arcs, annihilating all within your reach.";
            skirmishing5c.Description = "Weapon {Area (10’ radius) +0/+0 Physical} [12 Stamina]";
            skirmishing5c.ClarifyingText = "";
            skirmishing5c.StaminaCost = 12;
            skirmishing5c.UpkeepCost = null;
            skirmishing5c.Tier = 5;
            skirmishing5c.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishing5c.Tree = TalentTree.Skirmishing;
            skirmishing5c.TreeName = "Skirmishing";
            skirmishing5c.LinkedSkill = WeaponSkill.CloseCombat;
            Talents.Add(skirmishing5c);
            #endregion
            #endregion
            #region Skirmishing (Dueling)
            #region T1
            Talent skirmishingD1a = new Talent();
            skirmishingD1a.Name = "Parry";
            skirmishingD1a.Type = TalentType.TriggeredAction;
            skirmishingD1a.Action = ActionType.Reaction;
            skirmishingD1a.DescriptionFluff = "You intercept your opponent’s attack with your weapon.";
            skirmishingD1a.Description = "Gain +1d6-2 to Defense vs. the Triggering attack. [2 Stamina]";
            skirmishingD1a.ClarifyingText = "Triggering Action: a Melee attack targets you";
            skirmishingD1a.StaminaCost = null;
            skirmishingD1a.UpkeepCost = null;
            skirmishingD1a.Tier = 1;
            skirmishingD1a.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishingD1a.Tree = TalentTree.Skirmishing;
            skirmishingD1a.TreeName = "Skirmishing";
            skirmishingD1a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD1a);

            Talent skirmishingD1b = new Talent();
            skirmishingD1b.Name = "Feint";
            skirmishingD1b.Type = TalentType.AttackAugment;
            skirmishingD1b.Action = ActionType.Quick;
            skirmishingD1b.DescriptionFluff = "You fake your attack to leave your opponent momentarily exposed.";
            skirmishingD1b.Description = "The target of this attack becomes Vulnerable to the next attack made against it. [2 Stamina]";
            skirmishingD1b.ClarifyingText = "";
            skirmishingD1b.StaminaCost = null;
            skirmishingD1b.UpkeepCost = null;
            skirmishingD1b.Tier = 1;
            skirmishingD1b.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishingD1b.Tree = TalentTree.Skirmishing;
            skirmishingD1b.TreeName = "Skirmishing";
            skirmishingD1b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD1b);

            Talent skirmishingD1c = new Talent();
            skirmishingD1c.Name = "Quick Draw";
            skirmishingD1c.Type = TalentType.Benefit;
            skirmishingD1c.Action = ActionType.None;
            skirmishingD1c.DescriptionFluff = "All that practice in the mirror has finally paid off.";
            skirmishingD1c.Description = "Gain +2 to Initiative.";
            skirmishingD1c.ClarifyingText = "";
            skirmishingD1c.StaminaCost = null;
            skirmishingD1c.UpkeepCost = null;
            skirmishingD1c.Tier = 1;
            skirmishingD1c.TierBenefitDescription = "Reduce the penalty to damage from Two-Weapon Fighting by 1 with Close Combat and Dueling Weapons.";
            skirmishingD1c.Tree = TalentTree.Skirmishing;
            skirmishingD1c.TreeName = "Skirmishing";
            skirmishingD1c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD1c);
            #endregion
            #region T2
            Talent skirmishingD2a = new Talent();
            skirmishingD2a.Name = "Double-Fisting";
            skirmishingD2a.Type = TalentType.Benefit;
            skirmishingD2a.Action = ActionType.None;
            skirmishingD2a.DescriptionFluff = "Your ambidexterity enhances the power of your off-hand attacks.";
            skirmishingD2a.Description = "You can choose to apply 1 Attack Augment to the off-hand attack granted by Two-Weapon Fighting.";
            skirmishingD2a.ClarifyingText = "";
            skirmishingD2a.StaminaCost = null;
            skirmishingD2a.UpkeepCost = null;
            skirmishingD2a.Tier = 2;
            skirmishingD2a.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishingD2a.Tree = TalentTree.Skirmishing;
            skirmishingD2a.TreeName = "Skirmishing";
            skirmishingD2a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD2a);

            Talent skirmishingD2b = new Talent();
            skirmishingD2b.Name = "Follow-up Strike";
            skirmishingD2b.Type = TalentType.Maneuver;
            skirmishingD2b.Action = ActionType.Combat;
            skirmishingD2b.DescriptionFluff = "You dart through the battle dispatching foes left and right.";
            skirmishingD2b.Description = "Weapon {Melee +0/+0} [6 Stamina]";
            skirmishingD2b.ClarifyingText = "Make this attack, spend up to 3 MI, and make this attack again against a different opponent.";
            skirmishingD2b.StaminaCost = 6;
            skirmishingD2b.UpkeepCost = null;
            skirmishingD2b.Tier = 2;
            skirmishingD2b.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishingD2b.Tree = TalentTree.Skirmishing;
            skirmishingD2b.TreeName = "Skirmishing";
            skirmishingD2b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD2b);

            Talent skirmishingD2c = new Talent();
            skirmishingD2c.Name = "Hack and Slash";
            skirmishingD2c.Type = TalentType.Benefit;
            skirmishingD2c.Action = ActionType.None;
            skirmishingD2c.DescriptionFluff = "You are well-versed at overpowering opponents with the speed and volume of your attacks.";
            skirmishingD2c.Description = "If you hit the same opponent with both of your weapons in the same round while Two-Weapon Fighting, that opponent loses additional HP equal to either your Strength or Agility.";
            skirmishingD2c.ClarifyingText = "";
            skirmishingD2c.StaminaCost = null;
            skirmishingD2c.UpkeepCost = null;
            skirmishingD2c.Tier = 2;
            skirmishingD2c.TierBenefitDescription = "Gain +1 to Accuracy of Close Combat and Dueling weapons";
            skirmishingD2c.Tree = TalentTree.Skirmishing;
            skirmishingD2c.TreeName = "Skirmishing";
            skirmishingD2c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD2c);
            #endregion
            #region T3
            Talent skirmishingD3a = new Talent();
            skirmishingD3a.Name = "Quick Strikes";
            skirmishingD3a.Type = TalentType.Maneuver;
            skirmishingD3a.Action = ActionType.Combat;
            skirmishingD3a.DescriptionFluff = "Your lightning blows are difficult to avoid.";
            skirmishingD3a.Description = "Weapon {Melee +2/+0} [8 Stamina]";
            skirmishingD3a.ClarifyingText = "Make this attack twice.";
            skirmishingD3a.StaminaCost = 8;
            skirmishingD3a.UpkeepCost = null;
            skirmishingD3a.Tier = 3;
            skirmishingD3a.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishingD3a.Tree = TalentTree.Skirmishing;
            skirmishingD3a.TreeName = "Skirmishing";
            skirmishingD3a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD3a);

            Talent skirmishingD3b = new Talent();
            skirmishingD3b.Name = "Florentine Fighting Stance";
            skirmishingD3b.Type = TalentType.Stance;
            skirmishingD3b.Action = ActionType.Quick;
            skirmishingD3b.DescriptionFluff = "You adopt an ancient but highly effective dual-weapon fighting pose.";
            skirmishingD3b.Description = "While active, both the main hand and off-hand attacks from Two-Weapon Fighting gain +1 to attack and + 2 to damage. [8/2 Stamina]";
            skirmishingD3b.ClarifyingText = "";
            skirmishingD3b.StaminaCost = 8;
            skirmishingD3b.UpkeepCost = 2;
            skirmishingD3b.Tier = 3;
            skirmishingD3b.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishingD3b.Tree = TalentTree.Skirmishing;
            skirmishingD3b.TreeName = "Skirmishing";
            skirmishingD3b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD3b);

            Talent skirmishingD3c = new Talent();
            skirmishingD3c.Name = "Riposte";
            skirmishingD3c.Type = TalentType.TriggeredAction;
            skirmishingD3c.Action = ActionType.Reaction;
            skirmishingD3c.DescriptionFluff = "You jab back at your enemy, drawing blood.";
            skirmishingD3c.Description = "Weapon {Melee +0/+0} [4 Stamina or 2 Stamina if following a successful Parry]";
            skirmishingD3c.ClarifyingText = "Triggering Action: you were missed by a Melee attack)";
            skirmishingD3c.StaminaCost = 4;
            skirmishingD3c.UpkeepCost = null;
            skirmishingD3c.Tier = 3;
            skirmishingD3c.TierBenefitDescription = "Gain +1 to Melee Defenses";
            skirmishingD3c.Tree = TalentTree.Skirmishing;
            skirmishingD3c.TreeName = "Skirmishing";
            skirmishingD3c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD3c);
            #endregion
            #region T4
            Talent skirmishingD4a = new Talent();
            skirmishingD4a.Name = "Custom Weapon";
            skirmishingD4a.Type = TalentType.Benefit;
            skirmishingD4a.Action = ActionType.None;
            skirmishingD4a.DescriptionFluff = "Your constant tinkering with your weapon have fine tuned it to perfection.";
            skirmishingD4a.Description = "Choose 1 weapon that you own that uses the skill linked to this Talent Tree. You gain an additional Mod Slot in that weapon. This Talent requires 24 hours to apply to a weapon, or to change the weapon to which it applies. This Talent can be taken multiple times, each time applying it to a different weapon in your arsenal.";
            skirmishingD4a.ClarifyingText = "";
            skirmishingD4a.StaminaCost = null;
            skirmishingD4a.UpkeepCost = null;
            skirmishingD4a.Tier = 4;
            skirmishingD4a.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishingD4a.Tree = TalentTree.Skirmishing;
            skirmishingD4a.TreeName = "Skirmishing";
            skirmishingD4a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD4a);

            Talent skirmishingD4b = new Talent();
            skirmishingD4b.Name = "Stab and Throw";
            skirmishingD4b.Type = TalentType.TriggeredAction;
            skirmishingD4b.Action = ActionType.Reaction;
            skirmishingD4b.DescriptionFluff = "ou throw your trusty blade at your enemy, faster than anyone can see.";
            skirmishingD4b.Description = "Weapon {Ranged +0/+0} [5 Stamina]";
            skirmishingD4b.ClarifyingText = "Make this attack by throwing the Triggering weapon. This attack is not considered Improvised, does not create an Opening, and ignores Intervening creatures. Triggering Action: you successfully hit with a Melee attack";
            skirmishingD4b.StaminaCost = 5;
            skirmishingD4b.UpkeepCost = null;
            skirmishingD4b.Tier = 4;
            skirmishingD4b.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishingD4b.Tree = TalentTree.Skirmishing;
            skirmishingD4b.TreeName = "Skirmishing";
            skirmishingD4b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD4b);

            Talent skirmishingD4c = new Talent();
            skirmishingD4c.Name = "Whirling Dervish";
            skirmishingD4c.Type = TalentType.AttackAugment;
            skirmishingD4c.Action = ActionType.Quick;
            skirmishingD4c.DescriptionFluff = "You eviscerate your opponent with rapid strikes from your off-hand.";
            skirmishingD4c.Description = "Your next off-hand attack from Two-Weapon Fighting is made twice against the same target with the normal penalties for Two-Weapon Fighting.";
            skirmishingD4c.ClarifyingText = "";
            skirmishingD4c.StaminaCost = 5;
            skirmishingD4c.UpkeepCost = null;
            skirmishingD4c.Tier = 4;
            skirmishingD4c.TierBenefitDescription = "Gain a +4 to Disarm attacks with Close Combat and Dueling weapons";
            skirmishingD4c.Tree = TalentTree.Skirmishing;
            skirmishingD4c.TreeName = "Skirmishing";
            skirmishingD4c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD4c);
            #endregion
            #region T5
            Talent skirmishingD5a = new Talent();
            skirmishingD5a.Name = "Dance of Death";
            skirmishingD5a.Type = TalentType.Maneuver;
            skirmishingD5a.Action = ActionType.Combat;
            skirmishingD5a.DescriptionFluff = "You twirl between your opponents delivering punishment like presents.";
            skirmishingD5a.Description = "Weapon {Melee +0/+0} [12 Stamina]";
            skirmishingD5a.ClarifyingText = "Make this attack against up to 4 different opponents. You can spend your MI between these attacks.";
            skirmishingD5a.StaminaCost = 12;
            skirmishingD5a.UpkeepCost = null;
            skirmishingD5a.Tier = 5;
            skirmishingD5a.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishingD5a.Tree = TalentTree.Skirmishing;
            skirmishingD5a.TreeName = "Skirmishing";
            skirmishingD5a.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD5a);

            Talent skirmishingD5b = new Talent();
            skirmishingD5b.Name = "Wall of Blades Stance";
            skirmishingD5b.Type = TalentType.Stance;
            skirmishingD5b.Action = ActionType.Quick;
            skirmishingD5b.DescriptionFluff = "Your weapons protectively enshroud you while offering death to nearby enemies.";
            skirmishingD5b.Description = "Gain +6 to all Defenses. You cannot take Combat Actions, but when missed by a Melee attack, the attacker creates an Opening for you. Your Triggered Actions cost 2 fewer Stamina.When missed by a Ranged attack, you may redirect the attack to another target within 20 feet of you as a Triggered Action for 2 Stamina(after the cost reduction). That attack uses the original attack roll against the new target. [12/3 Stamina]";
            skirmishingD5b.ClarifyingText = "";
            skirmishingD5b.StaminaCost = 12;
            skirmishingD5b.UpkeepCost = 3;
            skirmishingD5b.Tier = 5;
            skirmishingD5b.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishingD5b.Tree = TalentTree.Skirmishing;
            skirmishingD5b.TreeName = "Skirmishing";
            skirmishingD5b.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD5b);

            Talent skirmishingD5c = new Talent();
            skirmishingD5c.Name = "Whirlwind";
            skirmishingD5c.Type = TalentType.Maneuver;
            skirmishingD5c.Action = ActionType.Combat;
            skirmishingD5c.DescriptionFluff = "Your weapon leads the charge as you turn in mighty arcs, annihilating all within your reach.";
            skirmishingD5c.Description = "Weapon {Area (10’ radius) +0/+0 Physical} [12 Stamina]";
            skirmishingD5c.ClarifyingText = "";
            skirmishingD5c.StaminaCost = 12;
            skirmishingD5c.UpkeepCost = null;
            skirmishingD5c.Tier = 5;
            skirmishingD5c.TierBenefitDescription = "Add Strength and Agility to determine damage with Close Combat and Dueling weapons";
            skirmishingD5c.Tree = TalentTree.Skirmishing;
            skirmishingD5c.TreeName = "Skirmishing";
            skirmishingD5c.LinkedSkill = WeaponSkill.Dueling;
            Talents.Add(skirmishingD5c);
            #endregion
            #endregion
            #region Sniping (Longarms)
            #region T1
            Talent sniping1a = new Talent();
            sniping1a.Name = "Stalking the Prey Stance";
            sniping1a.Type = TalentType.Stance;
            sniping1a.Action = ActionType.Quick;
            sniping1a.DescriptionFluff = "You silently approach your nest and prepare your shot.";
            sniping1a.Description = "Gain +1 Perception, +1 Stealth, +1 to attack, and if you already have Cover or Concealment increase its value to Heavy. This Stance can only be entered while the enemy is unaware of you. While in this Stance you are Slowed. [4/1 Stamina]";
            sniping1a.ClarifyingText = "Slowed = ½ Speed cannot sprint or run, -2 to Agility linked noncombat Skill Checks.";
            sniping1a.StaminaCost = 4;
            sniping1a.UpkeepCost = 1;
            sniping1a.Tier = 1;
            sniping1a.TierBenefitDescription = "Gain +1 to Accuracy with scope equipped rifles(see weapon attachments)";
            sniping1a.Tree = TalentTree.Sniping;
            sniping1a.TreeName = "Sniping";
            sniping1a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping1a);

            Talent sniping1b = new Talent();
            sniping1b.Name = "Sniper Team";
            sniping1b.Type = TalentType.Benefit;
            sniping1b.Action = ActionType.None;
            sniping1b.DescriptionFluff = "By locating targets and providing vital information, your ally makes you a more efficient killing machine.";
            sniping1b.Description = "An adjacent ally may spot for you as a Combat Action. While your ally is adjacent to you and spotting, you can spend their Stamina to activate abilities in this tree. The ally must be equipped with a scope or vision amplification of some kind.";
            sniping1b.ClarifyingText = "";
            sniping1b.StaminaCost = null;
            sniping1b.UpkeepCost = null;
            sniping1b.Tier = 1;
            sniping1b.TierBenefitDescription = "Gain +1 to Accuracy with scope equipped rifles(see weapon attachments)";
            sniping1b.Tree = TalentTree.Sniping;
            sniping1b.TreeName = "Sniping";
            sniping1b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping1b);

            Talent sniping1c = new Talent();
            sniping1c.Name = "Controlled Breathing Stance";
            sniping1c.Type = TalentType.Stance;
            sniping1c.Action = ActionType.Quick;
            sniping1c.DescriptionFluff = "You steady your breath to diminish your involuntary muscle movements.";
            sniping1c.Description = "Gain +2 to damage with rifles and increase the Max Range of Longarms by 50%. While active, you cannot spend MI to move. [4/1 Stamina]";
            sniping1c.ClarifyingText = "";
            sniping1c.StaminaCost = 4;
            sniping1c.UpkeepCost = 1;
            sniping1c.Tier = 1;
            sniping1c.TierBenefitDescription = "Gain +1 to Accuracy with scope equipped rifles(see weapon attachments)";
            sniping1c.Tree = TalentTree.Sniping;
            sniping1c.TreeName = "Sniping";
            sniping1c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping1c);
            #endregion
            #region T2
            Talent sniping2a = new Talent();
            sniping2a.Name = "Ambush";
            sniping2a.Type = TalentType.Benefit;
            sniping2a.Action = ActionType.None;
            sniping2a.DescriptionFluff = "They never saw you coming.";
            sniping2a.Description = "If Initiative is rolled when you are hidden from all opponents and not Surprised, you automatically go 1st in the 1st round. After the 1st round, the normal order is used (based on Initiative Checks).";
            sniping2a.ClarifyingText = "";
            sniping2a.StaminaCost = null;
            sniping2a.UpkeepCost = null;
            sniping2a.Tier = 2;
            sniping2a.TierBenefitDescription = "Gain +1 to Disguise, Stealth, Camouflage, and Concealment";
            sniping2a.Tree = TalentTree.Sniping;
            sniping2a.TreeName = "Sniping";
            sniping2a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping2a);

            Talent sniping2b = new Talent();
            sniping2b.Name = "Snipe";
            sniping2b.Type = TalentType.Maneuver;
            sniping2b.Action = ActionType.Combat;
            sniping2b.DescriptionFluff = "You patiently wait for the perfect shot. You are not disappointed by the results.";
            sniping2b.Description = "Weapon {Ranged +0/+(the amount by which the attack exceeds their Defense)} [6 Stamina]";
            sniping2b.ClarifyingText = "Make this attack against an unaware or Surprised target using a scope equipped weapon.";
            sniping2b.StaminaCost = 6;
            sniping2b.UpkeepCost = null;
            sniping2b.Tier = 2;
            sniping2b.TierBenefitDescription = "Gain +1 to Disguise, Stealth, Camouflage, and Concealment";
            sniping2b.Tree = TalentTree.Sniping;
            sniping2b.TreeName = "Sniping";
            sniping2b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping2b);

            Talent sniping2c = new Talent();
            sniping2c.Name = "Read the Wind";
            sniping2c.Type = TalentType.Trick;
            sniping2c.Action = ActionType.Quick;
            sniping2c.DescriptionFluff = "Nothing escapes your notice.";
            sniping2c.Description = "Make a Perception Check. Add your Focus to the result. [3 Stamina]";
            sniping2c.ClarifyingText = "";
            sniping2c.StaminaCost = 3;
            sniping2c.UpkeepCost = null;
            sniping2c.Tier = 2;
            sniping2c.TierBenefitDescription = "Gain +1 to Disguise, Stealth, Camouflage, and Concealment";
            sniping2c.Tree = TalentTree.Sniping;
            sniping2c.TreeName = "Sniping";
            sniping2c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping2c);
            #endregion
            #region T3
            Talent sniping3a = new Talent();
            sniping3a.Name = "You weave a ghillie suite using the surrounding materials.";
            sniping3a.Type = TalentType.Ritual;
            sniping3a.Action = ActionType.None;
            sniping3a.DescriptionFluff = "You weave a ghillie suite using the surrounding materials. [4 Fatigue]";
            sniping3a.Description = "You can make camouflage from the surrounding environment. After doing so you will have Light Concealment against any opponent at least 20’ from you as long as you move no more than 4 MI a turn and stay in the same type of environment(GM discretion).";
            sniping3a.ClarifyingText = "";
            sniping3a.StaminaCost = null;
            sniping3a.UpkeepCost = null;
            sniping3a.FatigueCost = 3;
            sniping3a.Tier = 3;
            sniping3a.TierBenefitDescription = "Gain +1 CM with longarms";
            sniping3a.Tree = TalentTree.Sniping;
            sniping3a.TreeName = "Sniping";
            sniping3a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping3a);

            Talent sniping3b = new Talent();
            sniping3b.Name = "Improved Take Aim";
            sniping3b.Type = TalentType.Benefit;
            sniping3b.Action = ActionType.None;
            sniping3b.DescriptionFluff = "You are steady.";
            sniping3b.Description = "When using the Take Aim Attack Augment, every 4 Stamina spent gives +1 to attack.";
            sniping3b.ClarifyingText = "";
            sniping3b.StaminaCost = null;
            sniping3b.UpkeepCost = null;
            sniping3b.Tier = 3;
            sniping3b.TierBenefitDescription = "Gain +1 CM with longarms";
            sniping3b.Tree = TalentTree.Sniping;
            sniping3b.TreeName = "Sniping";
            sniping3b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping3b);

            Talent sniping3c = new Talent();
            sniping3c.Name = "Rapid Acquisition";
            sniping3c.Type = TalentType.AttackAugment;
            sniping3c.Action = ActionType.Quick;
            sniping3c.DescriptionFluff = "You take a reflexive shot against a target considered too close to use a scope.";
            sniping3c.Description = "Your next attack with a scope-equipped rifle gains a +2 to attack and damage against a target at Short or Medium range. [4 Stamina]";
            sniping3c.ClarifyingText = "";
            sniping3c.StaminaCost = 4;
            sniping3c.UpkeepCost = null;
            sniping3c.Tier = 3;
            sniping3c.TierBenefitDescription = "Gain +1 CM with longarms";
            sniping3c.Tree = TalentTree.Sniping;
            sniping3c.TreeName = "Sniping";
            sniping3c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping3c);
            #endregion
            #region T4
            Talent sniping4a = new Talent();
            sniping4a.Name = "Custom Weapon";
            sniping4a.Type = TalentType.Benefit;
            sniping4a.Action = ActionType.None;
            sniping4a.DescriptionFluff = "Your constant tinkering with your weapon has fine-tuned it to perfection.";
            sniping4a.Description = "Choose 1 weapon that you own that uses the Longarms Skill. Gain an additional Mod Slot in that weapon. This Talent requires 24 hours to apply to a weapon or to change the weapon to which it applies. This Talent can be taken multiple times, each time applying it to a difierent weapon in your arsenal.";
            sniping4a.ClarifyingText = "";
            sniping4a.StaminaCost = null;
            sniping4a.UpkeepCost = null;
            sniping4a.Tier = 4;
            sniping4a.TierBenefitDescription = "Gain +4 damage vs. inanimate, Unaware, or Surprised targets";
            sniping4a.Tree = TalentTree.Sniping;
            sniping4a.TreeName = "Sniping";
            sniping4a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping4a);

            Talent sniping4b = new Talent();
            sniping4b.Name = "Sniper's Nest";
            sniping4b.Type = TalentType.Benefit;
            sniping4b.Action = ActionType.None;
            sniping4b.DescriptionFluff = "You gain the Poised condition any time you are Prone and make a Ranged attack with a Longarm.";
            sniping4b.Description = "";
            sniping4b.ClarifyingText = "Poised = +1 to Ranged weapon attacks and + 2 to Ranged damage. +2 to Presence-linked and Willpower-linked noncombat Skill Checks.";
            sniping4b.StaminaCost = null;
            sniping4b.UpkeepCost = null;
            sniping4b.Tier = 4;
            sniping4b.TierBenefitDescription = "Gain +4 damage vs. inanimate, Unaware, or Surprised targets";
            sniping4b.Tree = TalentTree.Sniping;
            sniping4b.TreeName = "Sniping";
            sniping4b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping4b);

            Talent sniping4c = new Talent();
            sniping4c.Name = "Called Shot";
            sniping4c.Type = TalentType.Maneuver;
            sniping4c.Action = ActionType.Combat;
            sniping4c.DescriptionFluff = "";
            sniping4c.Description = "Weapon {Ranged +0/+0} [10 Stamina]";
            sniping4c.ClarifyingText = "Make an attack using only a single round of ammunition. Choose 1 of the following effects: • Lethal + 2 and on a Crit you choose the secondary effect. • Automatically strike any piece of equipment the target is holding or wearing. The attack causes no damage to the target. • If used against an inanimate object, you can automatically hit anything the size of a coin or larger within your range. You have the choice of performing a trick such as turning something on or off when making the shot.";
            sniping4c.StaminaCost = 12;
            sniping4c.UpkeepCost = null;
            sniping4c.Tier = 4;
            sniping4c.TierBenefitDescription = "Gain +4 damage vs. inanimate, Unaware, or Surprised targets";
            sniping4c.Tree = TalentTree.Sniping;
            sniping4c.TreeName = "Sniping";
            sniping4c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping4c);
            #endregion
            #region T5
            Talent sniping5a = new Talent();
            sniping5a.Name = "Impossible Shot";
            sniping5a.Type = TalentType.Maneuver;
            sniping5a.Action = ActionType.Combat;
            sniping5a.DescriptionFluff = "As if by magic, you hit an opponent you cannot see.";
            sniping5a.Description = "Weapon {Ranged +0/+0} [12 Stamina]";
            sniping5a.ClarifyingText = "Make an attack against a target that was within line of sight during this encounter. This attack ignores all Concealment and Cover.";
            sniping5a.StaminaCost = 12;
            sniping5a.UpkeepCost = null;
            sniping5a.Tier = 5;
            sniping5a.TierBenefitDescription = "Gain Lethal +1 with longarm attacks at medium range or greater";
            sniping5a.Tree = TalentTree.Sniping;
            sniping5a.TreeName = "Sniping";
            sniping5a.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping5a);

            Talent sniping5b = new Talent();
            sniping5b.Name = "Unstoppable Shot";
            sniping5b.Type = TalentType.Maneuver;
            sniping5b.Action = ActionType.Combat;
            sniping5b.DescriptionFluff = "Your shot passes through creatures and armor like they were butter.";
            sniping5b.Description = "Weapon {Area (target within weapon range) +0/+0}[12 Stamina]";
            sniping5b.ClarifyingText = "Make this attack against the target as well as all Intervening enemies. This attack ignores Armor.";
            sniping5b.StaminaCost = 12;
            sniping5b.UpkeepCost = null;
            sniping5b.Tier = 5;
            sniping5b.TierBenefitDescription = "Gain Lethal +1 with longarm attacks at medium range or greater";
            sniping5b.Tree = TalentTree.Sniping;
            sniping5b.TreeName = "Sniping";
            sniping5b.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping5b);

            Talent sniping5c = new Talent();
            sniping5c.Name = "One Shot One Kill";
            sniping5c.Type = TalentType.AttackAugment;
            sniping5c.Action = ActionType.Combat;
            sniping5c.DescriptionFluff = "A properly placed shot dispatches a lesser foe with ease.";
            sniping5c.Description = "Your next longarm attack will instantly kill any Grunt that you damage with the attack. All other targets suffer a Crit 1 Stage higher than the roll would indicate. [6 Stamina]";
            sniping5c.ClarifyingText = "";
            sniping5c.StaminaCost = 6;
            sniping5c.UpkeepCost = null;
            sniping5c.Tier = 5;
            sniping5c.TierBenefitDescription = "Gain Lethal +1 with longarm attacks at medium range or greater";
            sniping5c.Tree = TalentTree.Sniping;
            sniping5c.TreeName = "Sniping";
            sniping5c.LinkedSkill = WeaponSkill.Longarms;
            Talents.Add(sniping5c);
            #endregion
            #endregion
            #region Creation (Conjuration)
            #region T1
            Talent creation1a = new Talent();
            creation1a.Name = "Repair";
            creation1a.Type = TalentType.Ritual;
            creation1a.Action = ActionType.None;
            creation1a.DescriptionFluff = "You mend a damaged item.";
            creation1a.Description = "You restore a damaged item to a functional state. This item could be broken as the result of a Fumble, deliberate destruction, or GM fiat. This Ritual cannot be used to perform Repairs with a value of more than 1,000U (GM discretion). [1 Fatigue]";
            creation1a.ClarifyingText = "10 minute cast, target adjacent item";
            creation1a.StaminaCost = null;
            creation1a.UpkeepCost = null;
            creation1a.FatigueCost = 1;
            creation1a.Tier = 1;
            creation1a.TierBenefitDescription = "Gain +2 to all Trade Skill rolls";
            creation1a.Tree = TalentTree.Creation;
            creation1a.TreeName = "Creation";
            creation1a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation1a);

            Talent creation1b = new Talent();
            creation1b.Name = "Speed Recovery";
            creation1b.Type = TalentType.Ritual;
            creation1b.Action = ActionType.None;
            creation1b.DescriptionFluff = "You weave the damaged flesh of your allies to enhance their natural recovery.";
            creation1b.Description = "You make a Conjuration Check. All allies within a 20-foot radius of you gain a bonus equal to ¼ of the result to their next Long-Term Recovery Check. [2 Fatigue]";
            creation1b.ClarifyingText = "";
            creation1b.StaminaCost = null;
            creation1b.UpkeepCost = null;
            creation1b.FatigueCost = 2;
            creation1b.Tier = 1;
            creation1b.TierBenefitDescription = "Gain +2 to all Trade Skill rolls";
            creation1b.Tree = TalentTree.Creation;
            creation1b.TreeName = "Creation";
            creation1b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation1b);

            Talent creation1c = new Talent();
            creation1c.Name = "Thorns";
            creation1c.Type = TalentType.Enhancement;
            creation1c.Action = ActionType.Quick;
            creation1c.DescriptionFluff = "You enshroud yourself in a layer of sharp thorns.";
            creation1c.Description = "A successful Melee attack against you causes the attacker to lose ½ your Presence Attribute in HP.  Double this number if the attacker uses an Unarmed attack. [4/1 Stamina]";
            creation1c.ClarifyingText = "Target self";
            creation1c.StaminaCost = 4;
            creation1c.UpkeepCost = 1;
            creation1c.Tier = 1;
            creation1c.TierBenefitDescription = "Gain +2 to all Trade Skill rolls";
            creation1c.Tree = TalentTree.Creation;
            creation1c.TreeName = "Creation";
            creation1c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation1c);
            #endregion
            #region T2
            Talent creation2a = new Talent();
            creation2a.Name = "Bolster Armor";
            creation2a.Type = TalentType.Ritual;
            creation2a.Action = ActionType.None;
            creation2a.DescriptionFluff = "You enhance a suit of armor to provide better protection.";
            creation2a.Description = "Increase the target armor’s Armor Value by 2 for 24 hours. [3 Fatigue]";
            creation2a.ClarifyingText = "";
            creation2a.StaminaCost = null;
            creation2a.UpkeepCost = null;
            creation2a.FatigueCost = 3;
            creation2a.Tier = 2;
            creation2a.TierBenefitDescription = "Reduce the cost of all items you craft by 10%";
            creation2a.Tree = TalentTree.Creation;
            creation2a.TreeName = "Creation";
            creation2a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation2a);

            Talent creation2b = new Talent();
            creation2b.Name = "Shield";
            creation2b.Type = TalentType.TriggeredAction;
            creation2b.Action = ActionType.Reaction;
            creation2b.DescriptionFluff = "You erect a short-lived barrier to intercept an incoming attack.";
            creation2b.Description = "Reduce the damage of the Triggering attack by 1/2. [2 stamina per point of damage reduced]";
            creation2b.ClarifyingText = "Triggering Action: you take damage from an attack";
            creation2b.StaminaCost = 2;
            creation2b.UpkeepCost = null;
            creation2b.Tier = 2;
            creation2b.TierBenefitDescription = "Reduce the cost of all items you craft by 10%";
            creation2b.Tree = TalentTree.Creation;
            creation2b.TreeName = "Creation";
            creation2b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation2b);

            Talent creation2c = new Talent();
            creation2c.Name = "Shape";
            creation2c.Type = TalentType.Ritual;
            creation2c.Action = ActionType.None;
            creation2c.DescriptionFluff = "You assemble and shape available materials into something useful.";
            creation2c.Description = "You reshape existing material to create new shapes or objects. When you use this Ritual, make a Conjuration Check. The result x 100U is the maximum value of the new item (maximum UEU worth of materials that can be shaped; materials discounted for any reason do not count against this). One half the result cubed is the maximum volume in cubic feet worth of material that can be shaped(GM discretion). You can create any item that you gain a 15 % or better cost reduction from having the appropriate Trade Skill (Smithing for weapons, Construction for walls), failure to have the appropriate skill results in a product that is non-functional and unstable. All necessary materials must be present to create the item (GM discretion). Items created through Shape are real and permanent. [3 Fatigue]";
            creation2c.ClarifyingText = "Ritual: 1 hour cast, target item";
            creation2c.StaminaCost = null;
            creation2c.UpkeepCost = null;
            creation2c.Tier = 2;
            creation2c.TierBenefitDescription = "Reduce the cost of all items you craft by 10%";
            creation2c.Tree = TalentTree.Creation;
            creation2c.TreeName = "Creation";
            creation2c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation2c);
            #endregion
            #region T3
            Talent creation3a = new Talent();
            creation3a.Name = "Fix";
            creation3a.Type = TalentType.Ritual;
            creation3a.Action = ActionType.Combat;
            creation3a.DescriptionFluff = "You mend a damaged item.";
            creation3a.Description = "You restore a damaged item to a functional state. This item could be broken as the result of a fumble, deliberate destruction, combat damage or GM fiat. This Ritual cannot repair an item with a value of more than 10,000U.";
            creation3a.ClarifyingText = "";
            creation3a.StaminaCost = null;
            creation3a.UpkeepCost = null;
            creation3a.FatigueCost = 4;
            creation3a.Tier = 3;
            creation3a.TierBenefitDescription = "Gain +2 to you Long-Term Recovery and at the beginning of each day all of your personal gear is restored to full functionality";
            creation3a.Tree = TalentTree.Creation;
            creation3a.TreeName = "Creation";
            creation3a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation3a);

            Talent creation3b = new Talent();
            creation3b.Name = "Create";
            creation3b.Type = TalentType.Ritual;
            creation3b.Action = ActionType.None;
            creation3b.DescriptionFluff = "You forge something useful from nothing.";
            creation3b.Description = "You form materials from nothing to create shapes or objects. When you use this Ritual, make a Conjuration Check. The result x 100U is the maximum value of the new item (maximum UEU worth of materials that can be shaped). One half the result cubed is the maximum volume in cubic feet worth of material that can be shaped (GM discretion). You can create any item that you would gain a 20 % or better cost reduction from having the appropriate Trade Skill (Smithing for weapons, Construction for walls);  Failure to have the appropriate Skill results in a product that is non-functional and unstable. No materials must be present to create the item.Items created through the Create Ritual have a subtle but obvious magical sheen to them. This sheen identifies them as being essentially worthless in the eyes of anyone considering purchasing the item.";
            creation3b.ClarifyingText = "Ritual: 1 hour cast, target item [5 Fatigue for as long as the item exists and for 8 hours after]";
            creation3b.StaminaCost = null;
            creation3b.UpkeepCost = null;
            creation3b.FatigueCost = 5;
            creation3b.Tier = 3;
            creation3b.TierBenefitDescription = "Gain +2 to you Long-Term Recovery and at the beginning of each day all of your personal gear is restored to full functionality";
            creation3b.Tree = TalentTree.Creation;
            creation3b.TreeName = "Creation";
            creation3b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation3b);

            Talent creation3c = new Talent();
            creation3c.Name = "Rapid Creation";
            creation3c.Type = TalentType.Benefit;
            creation3c.Action = ActionType.None;
            creation3c.DescriptionFluff = "You can conjure items in a jiffy.";
            creation3c.Description = "By spending double the normal Fatigue cost, you can cast Shape or Create in 1 minute (instead of the normal 1 hour).";
            creation3c.ClarifyingText = "";
            creation3c.StaminaCost = null;
            creation3c.UpkeepCost = null;
            creation3c.Tier = 3;
            creation3c.TierBenefitDescription = "Gain +2 to you Long-Term Recovery and at the beginning of each day all of your personal gear is restored to full functionality";
            creation3c.Tree = TalentTree.Creation;
            creation3c.TreeName = "Creation";
            creation3c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation3c);
            #endregion
            #region T4
            Talent creation4a = new Talent();
            creation4a.Name = "Permanence";
            creation4a.Type = TalentType.Ritual;
            creation4a.Action = ActionType.None;
            creation4a.DescriptionFluff = "Through practiced Ritual, you can make temporary items as real as any other.";
            creation4a.Description = "The target item becomes permanent and loses the sheen indicating that it is unnatural (see Create). Reagents with a value equal to 1/2 the total amount it would cost the character to craft the item are required and expended during this Ritual.";
            creation4a.ClarifyingText = "Ritual: 24-hour cast, target adjacent item crafted through the Create Ritual [6 Fatigue]";
            creation4a.StaminaCost = null;
            creation4a.UpkeepCost = null;
            creation4a.FatigueCost = 6;
            creation4a.Tier = 3;
            creation4a.TierBenefitDescription = "Gain -1 to Creation Ritual Fatigue costs (minimum cost of 1)";
            creation4a.Tree = TalentTree.Creation;
            creation4a.TreeName = "Creation";
            creation4a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation4a);

            Talent creation4b = new Talent();
            creation4b.Name = "Pocket Dimension";
            creation4b.Type = TalentType.Ritual;
            creation4b.Action = ActionType.None;
            creation4b.DescriptionFluff = "You create a small tear in the fabric of space and use it as a broom closet, to store bodies, or whatever.";
            creation4b.Description = "Make a Conjuration Check; create the result of the Check in cubic feet worth of empty space. This space must be linked to a physical portal of some kind (inside of a bag, doorway, pocket etc.) Multiple instances of the space can be applied to create one large space. Each casting counts as 1 space even if the spaces are combined.";
            creation4b.ClarifyingText = "Ritual: 1 hour cast, target adjacent empty space [1 Fatigue for as long as the space exists]";
            creation4b.StaminaCost = null;
            creation4b.UpkeepCost = null;
            creation4b.FatigueCost = 1;
            creation4b.Tier = 3;
            creation4b.TierBenefitDescription = "Gain -1 to Creation Ritual Fatigue costs (minimum cost of 1)";
            creation4b.Tree = TalentTree.Creation;
            creation4b.TreeName = "Creation";
            creation4b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation4b);

            Talent creation4c = new Talent();
            creation4c.Name = "Constantly Crafting";
            creation4c.Type = TalentType.Benefit;
            creation4c.Action = ActionType.None;
            creation4c.DescriptionFluff = "By constantly conjuring items to fill your needs your out of pocket expenses are reduced.";
            creation4c.Description = "You pay 20% less for all items you purchase for personal use.";
            creation4c.ClarifyingText = "";
            creation4c.StaminaCost = null;
            creation4c.UpkeepCost = null;
            creation4c.Tier = 3;
            creation4c.TierBenefitDescription = "Gain -1 to Creation Ritual Fatigue costs (minimum cost of 1)";
            creation4c.Tree = TalentTree.Creation;
            creation4c.TreeName = "Creation";
            creation4c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation4c);
            #endregion
            #region T5
            Talent creation5a = new Talent();
            creation5a.Name = "Mad Shaper";
            creation5a.Type = TalentType.Benefit;
            creation5a.Action = ActionType.None;
            creation5a.DescriptionFluff = "You have supernatural Conjuration abilities.";
            creation5a.Description = "Double the Conjuration Check result for when determining the maximum volume and value of items when using the Shape and Create Rituals.";
            creation5a.ClarifyingText = "";
            creation5a.StaminaCost = null;
            creation5a.UpkeepCost = null;
            creation5a.Tier = 5;
            creation5a.TierBenefitDescription = "Gain +5 to all Creation Ritual checks";
            creation5a.Tree = TalentTree.Creation;
            creation5a.TreeName = "Creation";
            creation5a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation5a);

            Talent creation5b = new Talent();
            creation5b.Name = "Creation Artistry";
            creation5b.Type = TalentType.Benefit;
            creation5b.Action = ActionType.None;
            creation5b.DescriptionFluff = "You use your skill to reduce your costs.";
            creation5b.Description = "All items created through the use of Shape or Create are of 1 Grade higher quality (at no additional cost), if applicable.";
            creation5b.ClarifyingText = "";
            creation5b.StaminaCost = null;
            creation5b.UpkeepCost = null;
            creation5b.Tier = 5;
            creation5b.TierBenefitDescription = "Gain +5 to all Creation Ritual checks";
            creation5b.Tree = TalentTree.Creation;
            creation5b.TreeName = "Creation";
            creation5b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation5b);

            Talent creation5c = new Talent();
            creation5c.Name = "Recreate";
            creation5c.Type = TalentType.Ritual;
            creation5c.Action = ActionType.None;
            creation5c.DescriptionFluff = "You conjure a lost item.";
            creation5c.Description = "You fully repair any item from at least 51% of the remnants (i.e. vehicle, equipment, building, or item). The item can be in any state of repair as long as more than half of the total materials are present.Make a Conjuration Check, the result cubed is the maximum volume in cubic feet of the item to be repaired. If this Ritual is cast on a creature, the target creature is restored to full health at the end of the Ritual.If the Ritual is interrupted before the end, the target regains a commensurate percentage of their missing HP based on the total amount of the Ritual that is cast.However, if the Ritual is interrupted, the full Fatigue cost of the Ritual is still imposed.";
            creation5c.ClarifyingText = "Ritual: 6 hour cast, target item or creature within 20’ [8 Fatigue]";
            creation5c.StaminaCost = null;
            creation5c.UpkeepCost = null;
            creation5c.FatigueCost = 8;
            creation5c.Tier = 5;
            creation5c.TierBenefitDescription = "Gain +5 to all Creation Ritual checks";
            creation5c.Tree = TalentTree.Creation;
            creation5c.TreeName = "Creation";
            creation5c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(creation5c);
            #endregion
            #endregion
            #region Earthshaping (Alteration)
            #region T1
            Talent earthshaping1a = new Talent();
            earthshaping1a.Name = "Earthstrike";
            earthshaping1a.Type = TalentType.Maneuver;
            earthshaping1a.Action = ActionType.Combat;
            earthshaping1a.DescriptionFluff = "You bid the ground around your enemy to lay him low.";
            earthshaping1a.Description = "Spell {Ranged (30’) +0/+2 Bludgeoning)} [4 Stamina]";
            earthshaping1a.ClarifyingText = "This attack has Knockback. The target gains +2 to the Resistance roll to resist the Knockback. There must be earthen or stone objects or terrain within 20’ of you or the target. If small amounts of material are available, the GM can allow the attack but reduce the damage by 2.";
            earthshaping1a.StaminaCost = 4;
            earthshaping1a.UpkeepCost = null;
            earthshaping1a.Tier = 1;
            earthshaping1a.TierBenefitDescription = "Gain +2 to Climb checks when climbing earth or stone; move over 1 MI of rough terrain for no additional cost";
            earthshaping1a.Tree = TalentTree.Earthshaping;
            earthshaping1a.TreeName = "Earthshaping";
            earthshaping1a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping1a);

            Talent earthshaping1b = new Talent();
            earthshaping1b.Name = "Stone Gauntlet";
            earthshaping1b.Type = TalentType.Enhancement;
            earthshaping1b.Action = ActionType.Quick;
            earthshaping1b.DescriptionFluff = "You engulf your hand in a weapon of pure rock.";
            earthshaping1b.Description = "Create a combat glove on one hand with a 0 Accuracy and +3 to damage. [4/1 Stamina]";
            earthshaping1b.ClarifyingText = "Enhancement: target self. The glove can have either a + 1 CM or a + 1 to damage. You can change your Unarmed damage to Slashing (claws) or Piercing (punching spikes) if you desire. You must have stone adjacent to you when the Enhancement is cast.";
            earthshaping1b.StaminaCost = 4;
            earthshaping1b.UpkeepCost = 1;
            earthshaping1b.Tier = 1;
            earthshaping1b.TierBenefitDescription = "Gain +2 to Climb checks when climbing earth or stone; move over 1 MI of rough terrain for no additional cost";
            earthshaping1b.Tree = TalentTree.Earthshaping;
            earthshaping1b.TreeName = "Earthshaping";
            earthshaping1b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping1b);

            Talent earthshaping1c = new Talent();
            earthshaping1c.Name = "Stone Armor";
            earthshaping1c.Type = TalentType.Enhancement;
            earthshaping1c.Action = ActionType.Quick;
            earthshaping1c.DescriptionFluff = "You draw earth and stone around yourself to increase your survivability.";
            earthshaping1c.Description = "Either form a suit of armor (1 Defense penalty, 4 Armor Value, 2 Speed Penalty) or increase the Speed Penalty and Armor Value of a worn suit of armor by 1. [4/1 Stamina]";
            earthshaping1c.ClarifyingText = "Enhancement: target self. If you form a suit of armor or enhance an existing one, it gains Light Fortification against Fire, Slashing, and Force damage. You must have stone adjacent to you when the Enhancement is cast.";
            earthshaping1c.StaminaCost = 4;
            earthshaping1c.UpkeepCost = 1;
            earthshaping1c.Tier = 1;
            earthshaping1c.TierBenefitDescription = "Gain +2 to Climb checks when climbing earth or stone; move over 1 MI of rough terrain for no additional cost";
            earthshaping1c.Tree = TalentTree.Earthshaping;
            earthshaping1c.TreeName = "Earthshaping";
            earthshaping1c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping1c);
            #endregion
            #region T2
            Talent earthshaping2a = new Talent();
            earthshaping2a.Name = "Stone Shape";
            earthshaping2a.Type = TalentType.Ritual;
            earthshaping2a.Action = ActionType.None;
            earthshaping2a.DescriptionFluff = "You assemble and shape available materials into something useful.";
            earthshaping2a.Description = "You reshape existing earthen material to create new shapes or objects. When you use this Ritual, make a Conjuration Check. The result x 100U is the maximum value of the new item(maximum UEU worth of materials that can be shaped; materials discounted for any reason do not count against this). One half the result cubed is the maximum volume in cubic feet worth of material that can be shaped(GM discretion).You can create any item that you gain a 15 % or better cost reduction from having the appropriate Trade Skill(Smithing for weapons, Construction for walls), failure to have the appropriate skill results in a product that is non - functional and unstable. All necessary materials must be present to create the item (GM discretion). Items created through Shape are real and permanent.";
            earthshaping2a.ClarifyingText = "Ritual: 1 hour cast, target item. [3 Fatigue]";
            earthshaping2a.StaminaCost = null;
            earthshaping2a.UpkeepCost = null;
            earthshaping2a.FatigueCost = 3;
            earthshaping2a.Tier = 2;
            earthshaping2a.TierBenefitDescription = "Gain +1 to damage with Earthshaping spells";
            earthshaping2a.Tree = TalentTree.Earthshaping;
            earthshaping2a.TreeName = "Earthshaping";
            earthshaping2a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping2a);

            Talent earthshaping2b = new Talent();
            earthshaping2b.Name = "Shield";
            earthshaping2b.Type = TalentType.TriggeredAction;
            earthshaping2b.Action = ActionType.Reaction;
            earthshaping2b.DescriptionFluff = "You erect a short-lived barrier to intercept an incoming attack.";
            earthshaping2b.Description = "Reduce the damage of the Triggering attack by 1/2. [2 stamina per point of damage reduced]";
            earthshaping2b.ClarifyingText = "Triggering Action: you take damage from an attack";
            earthshaping2b.StaminaCost = 2;
            earthshaping2b.UpkeepCost = null;
            earthshaping2b.Tier = 2;
            earthshaping2b.TierBenefitDescription = "Gain +1 to damage with Earthshaping spells";
            earthshaping2b.Tree = TalentTree.Earthshaping;
            earthshaping2b.TreeName = "Earthshaping";
            earthshaping2b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping2b);

            Talent earthshaping2c = new Talent();
            earthshaping2c.Name = "Land Lock";
            earthshaping2c.Type = TalentType.Maneuver;
            earthshaping2c.Action = ActionType.Combat;
            earthshaping2c.DescriptionFluff = "You soften the earth below their feet only to quickly solidify it again.";
            earthshaping2c.Description = "Spell {Area (10’ radius within 30’) +0/+0 Physical (no damage)} [6 Stamina]";
            earthshaping2c.ClarifyingText = "Targets hit are Vulnerable (until Resisted) and cannot spend MI to move until no longer Vulnerable as a result of this spell.";
            earthshaping2c.StaminaCost = 6;
            earthshaping2c.UpkeepCost = null;
            earthshaping2c.Tier = 2;
            earthshaping2c.TierBenefitDescription = "Gain +1 to damage with Earthshaping spells";
            earthshaping2c.Tree = TalentTree.Earthshaping;
            earthshaping2c.TreeName = "Earthshaping";
            earthshaping2c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping2c);
            #endregion
            #region T3
            Talent earthshaping3a = new Talent();
            earthshaping3a.Name = "Stone Companion";
            earthshaping3a.Type = TalentType.Ritual;
            earthshaping3a.Action = ActionType.None;
            earthshaping3a.DescriptionFluff = "You pull an earthen companion from the ground to fight by your side.";
            earthshaping3a.Description = "Create a Size 2 Solid Companion of your level to serve you. This Companion follows the normal rules for commanding Companions in combat. This Companion can know a single Tier 2 or lower Earthshaping Talent that you know. The Companion gains one of the following: Tough(2); Brawny (2) and Defender; or Tough (1), Honed(1), and { Ranged(Pistol) +0/+0 Bludgeoning} attack.";
            earthshaping3a.ClarifyingText = "Ritual: Combat Action cast, target adjacent empty space. [3 Fatigue as long as the Companion exists]";
            earthshaping3a.StaminaCost = null;
            earthshaping3a.UpkeepCost = null;
            earthshaping3a.FatigueCost = 3;
            earthshaping3a.Tier = 3;
            earthshaping3a.TierBenefitDescription = "Gain +2 to Durability vs. attacks targeting Body Defense";
            earthshaping3a.Tree = TalentTree.Earthshaping;
            earthshaping3a.TreeName = "Earthshaping";
            earthshaping3a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping3a);

            Talent earthshaping3b = new Talent();
            earthshaping3b.Name = "Stone Weapons";
            earthshaping3b.Type = TalentType.Enhancement;
            earthshaping3b.Action = ActionType.Quick;
            earthshaping3b.DescriptionFluff = "You form a weapon of earth and stone.";
            earthshaping3b.Description = "Form any Melee weapon of size 6 or smaller. The weapon can be formed with either a +1 to damage and CM; +1 to hit; or Lethal 1. If used to craft a combat glove, the weapon has a base Accuracy of 0 and damage of 4. You can change your combat glove’s weapon damage to Slashing or Piercing if you desire .You must have stone adjacent to you when the Enhancement is cast.";
            earthshaping3b.ClarifyingText = "Enhancement: target self. [8/2 stamina]";
            earthshaping3b.StaminaCost = 8;
            earthshaping3b.UpkeepCost = 2;
            earthshaping3b.Tier = 3;
            earthshaping3b.TierBenefitDescription = "Gain +2 to Durability vs. attacks targeting Body Defense";
            earthshaping3b.Tree = TalentTree.Earthshaping;
            earthshaping3b.TreeName = "Earthshaping";
            earthshaping3b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping3b);

            Talent earthshaping3c = new Talent();
            earthshaping3c.Name = "Earthen Wall";
            earthshaping3c.Type = TalentType.Trick;
            earthshaping3c.Action = ActionType.Quick;
            earthshaping3c.DescriptionFluff = "You cause a wall to surge from the ground.";
            earthshaping3c.Description = "You form a wall with maximum dimensions of 5’x5’x6” within 20’ of you. The wall can have any basic shape you would like. The wall provides Cover with a Toughness of 12. This wall lasts until the end of the encounter.";
            earthshaping3c.ClarifyingText = "";
            earthshaping3c.StaminaCost = 4;
            earthshaping3c.UpkeepCost = null;
            earthshaping3c.Tier = 3;
            earthshaping3c.TierBenefitDescription = "Gain +2 to Durability vs. attacks targeting Body Defense";
            earthshaping3c.Tree = TalentTree.Earthshaping;
            earthshaping3c.TreeName = "Earthshaping";
            earthshaping3c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping3c);
            #endregion
            #region T4
            Talent earthshaping4a = new Talent();
            earthshaping4a.Name = "Groundburst";
            earthshaping4a.Type = TalentType.Maneuver;
            earthshaping4a.Action = ActionType.Combat;
            earthshaping4a.DescriptionFluff = "You cause the ground below your opponents to explode upward.";
            earthshaping4a.Description = "Spell {Area (20’ radius within 50’) +0/+4 Force and Bludgeoning} [10 Stamina]";
            earthshaping4a.ClarifyingText = "This attack gains Knockback. When calculating Knockback from this attack, the full damage roll is used, including damage resulting from Crits. The area of this attack is rough terrain for the remainder of the encounter for everyone except the caster.";
            earthshaping4a.StaminaCost = 10;
            earthshaping4a.UpkeepCost = null;
            earthshaping4a.Tier = 4;
            earthshaping4a.TierBenefitDescription = "Gain -1 to the cost of Earthshaping spells and Rituals";
            earthshaping4a.Tree = TalentTree.Earthshaping;
            earthshaping4a.TreeName = "Earthshaping";
            earthshaping4a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping4a);

            Talent earthshaping4b = new Talent();
            earthshaping4b.Name = "Stone Shell";
            earthshaping4b.Type = TalentType.Enhancement;
            earthshaping4b.Action = ActionType.Quick;
            earthshaping4b.DescriptionFluff = "You ensconce yourself within a fortress of stone.";
            earthshaping4b.Description = "Either form a suit of armor identical to Plate Armor (Heavy, Rigid, AV:6, AP:-2, SP:-2, +2AV vs Area), or increase the armor of a worn suit of armor by 2 and its Speed Penalty by 1. Whether you form a suit of armor, or enhance an existing one, it gains Light Fortification (+2 AV) vs. all damage types. You must have stone adjacent to you when the Enhancement is cast.";
            earthshaping4b.ClarifyingText = "Enhancement: target self [10/3 Stamina]";
            earthshaping4b.StaminaCost = 10;
            earthshaping4b.UpkeepCost = 3;
            earthshaping4b.Tier = 4;
            earthshaping4b.TierBenefitDescription = "Gain -1 to the cost of Earthshaping spells and Rituals";
            earthshaping4b.Tree = TalentTree.Earthshaping;
            earthshaping4b.TreeName = "Earthshaping";
            earthshaping4b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping4b);

            Talent earthshaping4c = new Talent();
            earthshaping4c.Name = "Earth Glide";
            earthshaping4c.Type = TalentType.Benefit;
            earthshaping4c.Action = ActionType.None;
            earthshaping4c.DescriptionFluff = "The ground is your friend, moving you through obstacles and protecting you from falls.";
            earthshaping4c.Description = "You ignore rough terrain composed of natural or stone ground and gain +5 to Athletics Checks to jump from or climb across similar surfaces.You gain a + 1 to Physical Defenses while adjacent to such surfaces.You reduce all Fall damage by your Presence Attribute.";
            earthshaping4c.ClarifyingText = "";
            earthshaping4c.StaminaCost = null;
            earthshaping4c.UpkeepCost = null;
            earthshaping4c.Tier = 4;
            earthshaping4c.TierBenefitDescription = "Gain -1 to the cost of Earthshaping spells and Rituals";
            earthshaping4c.Tree = TalentTree.Earthshaping;
            earthshaping4c.TreeName = "Earthshaping";
            earthshaping4c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping4c);
            #endregion
            #region T5
            Talent earthshaping5a = new Talent();
            earthshaping5a.Name = "Earth's Avatar";
            earthshaping5a.Type = TalentType.Ritual;
            earthshaping5a.Action = ActionType.Combat;
            earthshaping5a.DescriptionFluff = "You bid your avatar cometh as it rends itself from the world, ready to serve.";
            earthshaping5a.Description = "Create a Size 5 Solid Companion of your level to serve you. This Companion follows the normal rules for commanding companions in combat. The avatar knows all Earthshaping Enhancements and Spells of Tier 3 or lower that you know. The Avatar gains Battering, Animalistic(Heavy), and Brawny(2). You can use a Combat Action to Heal the Companion [3 Stamina per point of Healing required].";
            earthshaping5a.ClarifyingText = "Ritual: Combat Action cast, target adjacent empty space [7 Fatigue for as long as the Avatar exists and for 8 hours after])";
            earthshaping5a.StaminaCost = null;
            earthshaping5a.UpkeepCost = null;
            earthshaping5a.FatigueCost = 7;
            earthshaping5a.Tier = 5;
            earthshaping5a.TierBenefitDescription = "Double the material affected by Earthshaping Rituals and widen the area of Earthshaping spells by 50%";
            earthshaping5a.Tree = TalentTree.Earthshaping;
            earthshaping5a.TreeName = "Earthshaping";
            earthshaping5a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping5a);

            Talent earthshaping5b = new Talent();
            earthshaping5b.Name = "Stone Soul";
            earthshaping5b.Type = TalentType.Benefit;
            earthshaping5b.Action = ActionType.None;
            earthshaping5b.DescriptionFluff = "You become like the stone you so often use as a weapon.";
            earthshaping5b.Description = "You gain Light Fortification against Slashing, Piercing, and Fire damage. If you have Light Fortification against one of these three damage types you instead gain Fortification against that damage type.You reduce the CM of all attacks against you by 1. Earthshaping Enhancements cast on you have a maintenance cost 1 lower, to a minimum of 1.";
            earthshaping5b.ClarifyingText = "";
            earthshaping5b.StaminaCost = null;
            earthshaping5b.UpkeepCost = null;
            earthshaping5b.Tier = 5;
            earthshaping5b.TierBenefitDescription = "Double the material affected by Earthshaping Rituals and widen the area of Earthshaping spells by 50%";
            earthshaping5b.Tree = TalentTree.Earthshaping;
            earthshaping5b.TreeName = "Earthshaping";
            earthshaping5b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping5b);

            Talent earthshaping5c = new Talent();
            earthshaping5c.Name = "Earthquake";
            earthshaping5c.Type = TalentType.Maneuver;
            earthshaping5c.Action = ActionType.Combat;
            earthshaping5c.DescriptionFluff = "You tear the world apart.";
            earthshaping5c.Description = "Spell {Area (50’ radius within Pistol range) +0/+6 Bludgeoning} [15 Stamina]";
            earthshaping5c.ClarifyingText = "All creatures that take damage are knocked Prone. Structures suffer double damage over Durability. The entire area of the spell becomes rough terrain. When you cause a Crit, that target falls into a 10’ deep space torn open by the spell. It can roll to catch itself using the usual Athletics rules. You are immune to this spell.";
            earthshaping5c.StaminaCost = 15;
            earthshaping5c.UpkeepCost = null;
            earthshaping5c.Tier = 5;
            earthshaping5c.TierBenefitDescription = "Double the material affected by Earthshaping Rituals and widen the area of Earthshaping spells by 50%";
            earthshaping5c.Tree = TalentTree.Earthshaping;
            earthshaping5c.TreeName = "Earthshaping";
            earthshaping5c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(earthshaping5c);
            #endregion
            #endregion
            #region Elementalism [Element] (Conjuration)
            #region T1
            Talent elementalism1a = new Talent();
            elementalism1a.Name = "[Element] Blast";
            elementalism1a.Type = TalentType.Maneuver;
            elementalism1a.Action = ActionType.Combat;
            elementalism1a.DescriptionFluff = "You hurl a blast of your chosen element at your enemy.";
            elementalism1a.Description = "Spell {Ranged (Pistol) +0/+2 [Element]} [4 Stamina]";
            elementalism1a.ClarifyingText = "";
            elementalism1a.StaminaCost = 4;
            elementalism1a.UpkeepCost = null;
            elementalism1a.Tier = 1;
            elementalism1a.TierBenefitDescription = "Gain +1 to damage wiht [Element] attacks";
            elementalism1a.Tree = TalentTree.Elementalism;
            elementalism1a.TreeName = "Elementalism";
            elementalism1a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism1a);

            Talent elementalism1b = new Talent();
            elementalism1b.Name = "[Element] Strike";
            elementalism1b.Type = TalentType.AttackAugment;
            elementalism1b.Action = ActionType.Quick;
            elementalism1b.DescriptionFluff = "You briefly engulf your weapon with your chosen element. [2 Stamina]";
            elementalism1b.Description = "You augment a Melee attack to gain a +2 to damage and the [Element] damage type.";
            elementalism1b.ClarifyingText = "";
            elementalism1b.StaminaCost = 2;
            elementalism1b.UpkeepCost = null;
            elementalism1b.Tier = 1;
            elementalism1b.TierBenefitDescription = "Gain +1 to damage wiht [Element] attacks";
            elementalism1b.Tree = TalentTree.Elementalism;
            elementalism1b.TreeName = "Elementalism";
            elementalism1b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism1b);

            Talent elementalism1c = new Talent();
            elementalism1c.Name = "[Element] Wave";
            elementalism1c.Type = TalentType.Maneuver;
            elementalism1c.Action = ActionType.Combat;
            elementalism1c.DescriptionFluff = "You send forth a torrent of your chosen element.";
            elementalism1c.Description = "Spell {Area (20’ cone) +0/+0 [Element]} [6 Stamina]";
            elementalism1c.ClarifyingText = "";
            elementalism1c.StaminaCost = 6;
            elementalism1c.UpkeepCost = null;
            elementalism1c.Tier = 1;
            elementalism1c.TierBenefitDescription = "Gain +1 to damage wiht [Element] attacks";
            elementalism1c.Tree = TalentTree.Elementalism;
            elementalism1c.TreeName = "Elementalism";
            elementalism1c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism1c);
            #endregion
            #region T2
            Talent elementalism2a = new Talent();
            elementalism2a.Name = "[Element] Shield";
            elementalism2a.Type = TalentType.Enhancement;
            elementalism2a.Action = ActionType.Quick;
            elementalism2a.DescriptionFluff = "You wrap yourself in a blanket of your chosen element.";
            elementalism2a.Description = "You gain Fortification vs. your [Element] and Light Fortification vs. all other Elementalism [Element] option damage types. When you are hit by a non-Reach Melee weapon, the attacker takes a damage roll with a modifier equal to your Presence Attribute.";
            elementalism2a.ClarifyingText = "Enhancement: target self [6/2 Stamina]";
            elementalism2a.StaminaCost = 6;
            elementalism2a.UpkeepCost = 2;
            elementalism2a.Tier = 2;
            elementalism2a.TierBenefitDescription = "Gain +1 to Defense against [Element] attacks";
            elementalism2a.Tree = TalentTree.Elementalism;
            elementalism2a.TreeName = "Elementalism";
            elementalism2a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism2a);

            Talent elementalism2b = new Talent();
            elementalism2b.Name = "[Element] Explosion";
            elementalism2b.Type = TalentType.Maneuver;
            elementalism2b.Action = ActionType.Combat;
            elementalism2b.DescriptionFluff = "You hurl an explosive ball of your chosen element.";
            elementalism2b.Description = "Spell {Area (10’ radius within SMG range) +0/+2 [Element]} [6 Stamina]";
            elementalism2b.ClarifyingText = "";
            elementalism2b.StaminaCost = 6;
            elementalism2b.UpkeepCost = null;
            elementalism2b.Tier = 2;
            elementalism2b.TierBenefitDescription = "Gain +1 to Defense against [Element] attacks";
            elementalism2b.Tree = TalentTree.Elementalism;
            elementalism2b.TreeName = "Elementalism";
            elementalism2b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism2b);

            Talent elementalism2c = new Talent();
            elementalism2c.Name = "[Element] Engulf";
            elementalism2c.Type = TalentType.Maneuver;
            elementalism2c.Action = ActionType.Combat;
            elementalism2c.DescriptionFluff = "You encapsulate your enemy in a tomb of your chosen element.";
            elementalism2c.Description = "Spell {(Melee) +0/+0 [Element]} [8 Stamina]";
            elementalism2c.ClarifyingText = "If hit, the target becomes Vulnerable (until Resisted) and takes another damage roll (with the same modifier as this attack) each time it fails a Resistance Check against the Vulnerability caused by this power.";
            elementalism2c.StaminaCost = 8;
            elementalism2c.UpkeepCost = null;
            elementalism2c.Tier = 2;
            elementalism2c.TierBenefitDescription = "Gain +1 to Defense against [Element] attacks";
            elementalism2c.Tree = TalentTree.Elementalism;
            elementalism2c.TreeName = "Elementalism";
            elementalism2c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism2c);
            #endregion
            #region T3
            Talent elementalism3a = new Talent();
            elementalism3a.Name = "[Element] Lash";
            elementalism3a.Type = TalentType.Enhancement;
            elementalism3a.Action = ActionType.Quick;
            elementalism3a.DescriptionFluff = "You wrap your arms in deadly tendrils of elemental fury.";
            elementalism3a.Description = "Your Unarmed attacks gain Reach, +2 [Element] damage, and +1 CM.";
            elementalism3a.ClarifyingText = "";
            elementalism3a.StaminaCost = 8;
            elementalism3a.UpkeepCost = 2;
            elementalism3a.Tier = 3;
            elementalism3a.TierBenefitDescription = "Gain +1 CM for [Element] attacks";
            elementalism3a.Tree = TalentTree.Elementalism;
            elementalism3a.TreeName = "Elementalism";
            elementalism3a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism3a);

            Talent elementalism3b = new Talent();
            elementalism3b.Name = "Counterblast";
            elementalism3b.Type = TalentType.TriggeredAction;
            elementalism3b.Action = ActionType.Reaction;
            elementalism3b.DescriptionFluff = "You strike back against your fiercest opponents.";
            elementalism3b.Description = "Spell {Ranged (automatic hit)/+2 [Element]} [8 Stamina]";
            elementalism3b.ClarifyingText = "Triggering Action: you are hit by a Ranged or Area attack [8 Stamina]";
            elementalism3b.StaminaCost = null;
            elementalism3b.UpkeepCost = null;
            elementalism3b.Tier = 3;
            elementalism3b.TierBenefitDescription = "Gain +1 CM for [Element] attacks";
            elementalism3b.Tree = TalentTree.Elementalism;
            elementalism3b.TreeName = "Elementalism";
            elementalism3b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism3b);

            Talent elementalism3c = new Talent();
            elementalism3c.Name = "[Element] Aura";
            elementalism3c.Type = TalentType.Enhancement;
            elementalism3c.Action = ActionType.Quick;
            elementalism3c.DescriptionFluff = "Waves of your Element pour out from you to consume your enemies.";
            elementalism3c.Description = "You gain Fortification vs. your [Element] and Light Fortification vs. all other Elementalism [Element] option damage types.Enemies take a damage roll with a modifier equal to your Presence Attribute any time they hit you with a Melee attack, move adjacent to you, or end their turn adjacent to you.";
            elementalism3c.ClarifyingText = "Enhancement: target self";
            elementalism3c.StaminaCost = 8;
            elementalism3c.UpkeepCost = 2;
            elementalism3c.Tier = 3;
            elementalism3c.TierBenefitDescription = "Gain +1 CM for [Element] attacks";
            elementalism3c.Tree = TalentTree.Elementalism;
            elementalism3c.TreeName = "Elementalism";
            elementalism3c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism3c);
            #endregion
            #region T4
            Talent elementalism4a = new Talent();
            elementalism4a.Name = "[Element] Wall";
            elementalism4a.Type = TalentType.Trick;
            elementalism4a.Action = ActionType.Quick;
            elementalism4a.DescriptionFluff = "You protect yourself with your elemental power.";
            elementalism4a.Description = "You form a wall of [Element] with maximum dimensions of 10’x10’x1’. The wall causes Concealment for Fire, Acid, and Electricity (Heavy if fully behind, Light if partially behind). Walls of Cold provide Cover with a Toughness of 10. Entering or ending your turn inside a wall of Fire, Electricity, or Acid causes a damage roll equal to your Presence +4 of the appropriate damage type. Cold walls cannot be moved through.";
            elementalism4a.ClarifyingText = "";
            elementalism4a.StaminaCost = 6;
            elementalism4a.UpkeepCost = 3;
            elementalism4a.Tier = 4;
            elementalism4a.TierBenefitDescription = "Gain +4 to Durability vs. [Element]";
            elementalism4a.Tree = TalentTree.Elementalism;
            elementalism4a.TreeName = "Elementalism";
            elementalism4a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism4a);

            Talent elementalism4b = new Talent();
            elementalism4b.Name = "Overcharge";
            elementalism4b.Type = TalentType.AttackAugment;
            elementalism4b.Action = ActionType.Quick;
            elementalism4b.DescriptionFluff = "You fill your body with your chosen element before releasing it violently.";
            elementalism4b.Description = "Gain +4 to the damage, +1 CM and Lethal +1 to the next [Element] attack you make. [5 Stamina]";
            elementalism4b.ClarifyingText = "";
            elementalism4b.StaminaCost = 5;
            elementalism4b.UpkeepCost = null;
            elementalism4b.Tier = 4;
            elementalism4b.TierBenefitDescription = "Gain +4 to Durability vs. [Element]";
            elementalism4b.Tree = TalentTree.Elementalism;
            elementalism4b.TreeName = "Elementalism";
            elementalism4b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism4b);

            Talent elementalism4c = new Talent();
            elementalism4c.Name = "[Element] Defense";
            elementalism4c.Type = TalentType.Benefit;
            elementalism4c.Action = ActionType.None;
            elementalism4c.DescriptionFluff = "The constant elemental flares from your body help to keep your enemies at bay.";
            elementalism4c.Description = "While maintaining an Elementalism Enhancement, you gain a +1 to all Defenses, a -1 CM to attacks against you, and attacks of [Element] cannot score Crits against you.";
            elementalism4c.ClarifyingText = "";
            elementalism4c.StaminaCost = null;
            elementalism4c.UpkeepCost = null;
            elementalism4c.Tier = 4;
            elementalism4c.TierBenefitDescription = "Gain +4 to Durability vs. [Element]";
            elementalism4c.Tree = TalentTree.Elementalism;
            elementalism4c.TreeName = "Elementalism";
            elementalism4c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism4c);
            #endregion
            #region T5
            Talent elementalism5a = new Talent();
            elementalism5a.Name = "[Element] Obliteration";
            elementalism5a.Type = TalentType.Maneuver;
            elementalism5a.Action = ActionType.Combat;
            elementalism5a.DescriptionFluff = "You lash out with the full power of your chosen element. Nothing remains of your target.";
            elementalism5a.Description = "Spell {Area (15’ radius) +2/+10 [Element]} [14 Stamina]";
            elementalism5a.ClarifyingText = "This attack gains Lethal +1.";
            elementalism5a.StaminaCost = 14;
            elementalism5a.UpkeepCost = null;
            elementalism5a.Tier = 5;
            elementalism5a.TierBenefitDescription = "Gain +2 to MCR of Resistance Checks for [Element] attacks";
            elementalism5a.Tree = TalentTree.Elementalism;
            elementalism5a.TreeName = "Elementalism";
            elementalism5a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism5a);

            Talent elementalism5b = new Talent();
            elementalism5b.Name = "[Element] Avatar";
            elementalism5b.Type = TalentType.Ritual;
            elementalism5b.Action = ActionType.Combat;
            elementalism5b.DescriptionFluff = "You lash together a walking embodiment of your chosen element to serve you.";
            elementalism5b.Description = "Create a Size 5 Energy (Element) Companion of your level to serve you. This Companion follows the normal rules for commanding companions in combat. The avatar knows all Elementalism Enhancements and Spells of Tier 3 or lower that you know. The Avatar gains Graceful(3), {Ranged (Pistol) +0/+0 [Element]} attack, Animalistic(light), and Slight (2).You can use a Combat Action to Heal the Companion [3 Stamina per point of Healing].";
            elementalism5b.ClarifyingText = "Ritual: Combat Action cast, target adjacent empty space [7 Fatugue as log as the avatar exists and for 8 hours after]";
            elementalism5b.StaminaCost = null;
            elementalism5b.UpkeepCost = null;
            elementalism5b.FatigueCost = 7;
            elementalism5b.Tier = 5;
            elementalism5b.TierBenefitDescription = "Gain +2 to MCR of Resistance Checks for [Element] attacks";
            elementalism5b.Tree = TalentTree.Elementalism;
            elementalism5b.TreeName = "Elementalism";
            elementalism5b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism5b);

            Talent elementalism5c = new Talent();
            elementalism5c.Name = "[Element] Destruction";
            elementalism5c.Type = TalentType.AttackAugment;
            elementalism5c.Action = ActionType.Quick;
            elementalism5c.DescriptionFluff = "You unleash a floodgate of elemental power.";
            elementalism5c.Description = "Your next Ranged [Element] attack gains Full-Auto (20).";
            elementalism5c.ClarifyingText = "Becomes Area [Element] and 4 groupings";
            elementalism5c.StaminaCost = 6;
            elementalism5c.UpkeepCost = null;
            elementalism5c.Tier = 5;
            elementalism5c.TierBenefitDescription = "Gain +2 to MCR of Resistance Checks for [Element] attacks";
            elementalism5c.Tree = TalentTree.Elementalism;
            elementalism5c.TreeName = "Elementalism";
            elementalism5c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(elementalism5c);
            #endregion
            #endregion
            #region Illusion (Conjuration)
            #region T1
            Talent illusion1a = new Talent();
            illusion1a.Name = "Sleight of Hand";
            illusion1a.Type = TalentType.Enhancement;
            illusion1a.Action = ActionType.Quick;
            illusion1a.DescriptionFluff = "You cloak an object from view.";
            illusion1a.Description = "The target object becomes invisible to observation. You make a Conjuration Check with a penalty equal to ½ the size of the item; the result becomes the MCR to notice the item’s Presence. [4/1 Stamina]";
            illusion1a.ClarifyingText = "If the invisible item is used to make an attack against a target that has not noticed the item, the target is considered Vulnerable to the attack and cannot take Triggered Actions that would result from the attack.";
            illusion1a.StaminaCost = 4;
            illusion1a.UpkeepCost = 1;
            illusion1a.Tier = 1;
            illusion1a.TierBenefitDescription = "Gain +1 to Perception Attribute";
            illusion1a.Tree = TalentTree.Illusion;
            illusion1a.TreeName = "Illusion";
            illusion1a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion1a);

            Talent illusion1b = new Talent();
            illusion1b.Name = "Ghost Sound";
            illusion1b.Type = TalentType.Trick;
            illusion1b.Action = ActionType.Quick;
            illusion1b.DescriptionFluff = "You create a phantom sound.";
            illusion1b.Description = "Creates a series of sounds up to 5 seconds long. The sound can come from any point within range ignoring line of sight. [4 Stamina]";
            illusion1b.ClarifyingText = "Make a Conjuration Check; the result becomes the MCR of the Perception Check to notice that the sounds are false.";
            illusion1b.StaminaCost = 4;
            illusion1b.UpkeepCost = null;
            illusion1b.Tier = 1;
            illusion1b.TierBenefitDescription = "Gain +1 to Perception Attribute";
            illusion1b.Tree = TalentTree.Illusion;
            illusion1b.TreeName = "Illusion";
            illusion1b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion1b);

            Talent illusion1c = new Talent();
            illusion1c.Name = "Cloak";
            illusion1c.Type = TalentType.Enhancement;
            illusion1c.Action = ActionType.Quick;
            illusion1c.DescriptionFluff = "You make yourself partially invisible.";
            illusion1c.Description = "Target becomes Cloaked. A Cloaked target gains a +2 to Stealth and Light Concealment if it moved 10’ or less since the beginning of its last turn. If the target moved further than 10’ it gains only a +1 to Stealth.";
            illusion1c.ClarifyingText = "Enhancement: target self or ally within 20’ [4/1 Stamina]";
            illusion1c.StaminaCost = 4;
            illusion1c.UpkeepCost = 1;
            illusion1c.Tier = 1;
            illusion1c.TierBenefitDescription = "Gain +1 to Perception Attribute";
            illusion1c.Tree = TalentTree.Illusion;
            illusion1c.TreeName = "Illusion";
            illusion1c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion1c);
            #endregion
            #region T2
            Talent illusion2a = new Talent();
            illusion2a.Name = "Projection";
            illusion2a.Type = TalentType.Ritual;
            illusion2a.Action = ActionType.Combat;
            illusion2a.DescriptionFluff = "You craft an illusionary event.";
            illusion2a.Description = "Create an image in a 10x10x10 space that contains any image you like. Make a Conjuration Check with a penalty of 0 to 4, depending on complexity of image, to determine the MCR of the Perception Check to notice that the Projection is an illusion. [6/2 Stamina]";
            illusion2a.ClarifyingText = "This spell requires a Combat Action each round to change anything in the image.";
            illusion2a.StaminaCost = 6;
            illusion2a.UpkeepCost = 2;
            illusion2a.Tier = 2;
            illusion2a.TierBenefitDescription = "+1 to Stealth and Disguise";
            illusion2a.Tree = TalentTree.Illusion;
            illusion2a.TreeName = "Illusion";
            illusion2a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion2a);

            Talent illusion2b = new Talent();
            illusion2b.Name = "Confusion";
            illusion2b.Type = TalentType.Maneuver;
            illusion2b.Action = ActionType.Combat;
            illusion2b.DescriptionFluff = "You bombard your opponent with a kaleidescope of confusing images.";
            illusion2b.Description = "Spell {Ranged (Pistol) +2/+0 Resolve (no damage)} [6 Stamina]";
            illusion2b.ClarifyingText = "On a hit, the target becomes Weakened (until Resisted).  Weakened = -2 to Strength-linked noncombat Skill Checks, -2 to attack rols and -2 to damage rolls.";
            illusion2b.StaminaCost = 6;
            illusion2b.UpkeepCost = null;
            illusion2b.Tier = 2;
            illusion2b.TierBenefitDescription = "+1 to Stealth and Disguise";
            illusion2b.Tree = TalentTree.Illusion;
            illusion2b.TreeName = "Illusion";
            illusion2b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion2b);

            Talent illusion2c = new Talent();
            illusion2c.Name = "Shadow Form";
            illusion2c.Type = TalentType.Enhancement;
            illusion2c.Action = ActionType.Quick;
            illusion2c.DescriptionFluff = "You engulf yourself in an aura of shadow.";
            illusion2c.Description = "You gain Light Concealment and a +2 to Intimidation Checks. [6/2 Stamina]";
            illusion2c.ClarifyingText = "Enhancement: Target self";
            illusion2c.StaminaCost = 6;
            illusion2c.UpkeepCost = 2;
            illusion2c.Tier = 2;
            illusion2c.TierBenefitDescription = "+1 to Stealth and Disguise";
            illusion2c.Tree = TalentTree.Illusion;
            illusion2c.TreeName = "Illusion";
            illusion2c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion2c);
            #endregion
            #region T3
            Talent illusion3a = new Talent();
            illusion3a.Name = "Invisibility";
            illusion3a.Type = TalentType.Enhancement;
            illusion3a.Action = ActionType.Quick;
            illusion3a.DescriptionFluff = "You remove yourself from sight.";
            illusion3a.Description = "You become invisible as long as you do not run or sprint. Invisible characters have Total Concealment. If you run, sprint, or make an attack, your Concealment drops to Light for 1 round.";
            illusion3a.ClarifyingText = "";
            illusion3a.StaminaCost = 8;
            illusion3a.UpkeepCost = 2;
            illusion3a.Tier = 3;
            illusion3a.TierBenefitDescription = "Gain +1 to Ranged Defense";
            illusion3a.Tree = TalentTree.Illusion;
            illusion3a.TreeName = "Illusion";
            illusion3a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion3a);

            Talent illusion3b = new Talent();
            illusion3b.Name = "Silence";
            illusion3b.Type = TalentType.Trick;
            illusion3b.Action = ActionType.Quick;
            illusion3b.DescriptionFluff = "You strip an area of all sound.";
            illusion3b.Description = "An area with a radius of 10’ within 50’ of you becomes completely silent. It is impossible to hear anything while inside the zone, and nothing within the zone creates any noise.Creatures with line of sight to the a ected area can still make Perception checks to notice activity in the area at a -2. Creatures inside the zone su er a -2 to Initiative and a -1 to Defenses.";
            illusion3b.ClarifyingText = "";
            illusion3b.StaminaCost = 8;
            illusion3b.UpkeepCost = 3;
            illusion3b.Tier = 3;
            illusion3b.TierBenefitDescription = "Gain +1 to Ranged Defense";
            illusion3b.Tree = TalentTree.Illusion;
            illusion3b.TreeName = "Illusion";
            illusion3b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion3b);

            Talent illusion3c = new Talent();
            illusion3c.Name = "Clone";
            illusion3c.Type = TalentType.Enhancement;
            illusion3c.Action = ActionType.Quick;
            illusion3c.DescriptionFluff = "You create an illusionary double of yourself.";
            illusion3c.Description = "Create an illusionary duplicate of yourself. Make a Conjuration Check every round, the result is the MCR of the Perception Check to notice that the clone is an illusion. The illusion has the same defenses as you, and every time it is hit by an attack you must spend 3 Stamina as a Triggered Action to make it react appropriately.Failure to do so makes the attacker and all within 30’ of the clone aware that it is an illusion.Use the normal rules for commanding companions to create fake actions for the clone each round.";
            illusion3c.ClarifyingText = "";
            illusion3c.StaminaCost = 8;
            illusion3c.UpkeepCost = 2;
            illusion3c.Tier = 3;
            illusion3c.TierBenefitDescription = "Gain +1 to Ranged Defense";
            illusion3c.Tree = TalentTree.Illusion;
            illusion3c.TreeName = "Illusion";
            illusion3c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion3c);
            #endregion
            #region T4
            Talent illusion4a = new Talent();
            illusion4a.Name = "True Sight";
            illusion4a.Type = TalentType.Enhancement;
            illusion4a.Action = ActionType.Quick;
            illusion4a.DescriptionFluff = "You pull back the veil and see things as the truly are.";
            illusion4a.Description = "You gain a +5 to Perception. Gear, Enhancements, and Stances that provide your enemies bonuses to Disguise or Stealth, or which create Concealment, are ignored. [5/2 Stamina]";
            illusion4a.ClarifyingText = "";
            illusion4a.StaminaCost = 5;
            illusion4a.UpkeepCost = 2;
            illusion4a.Tier = 4;
            illusion4a.TierBenefitDescription = "Gain Light Concealment vs. Ranged attacks";
            illusion4a.Tree = TalentTree.Illusion;
            illusion4a.TreeName = "Illusion";
            illusion4a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion4a);

            Talent illusion4b = new Talent();
            illusion4b.Name = "Illusionary Scene";
            illusion4b.Type = TalentType.Ritual;
            illusion4b.Action = ActionType.Combat;
            illusion4b.DescriptionFluff = "You create an illusionary production.";
            illusion4b.Description = "Target an area up to 30’ x 30’ x 30’ and within 100’; craft a scene in that area that contains any images or sounds you like. [10 Fatigue]";
            illusion4b.ClarifyingText = "Make a Conjuration Check to determine the MCR of the Perception Check to notice something strange about the image.Note that this spell requires a Combat Action every round to change anything in the image.";
            illusion4b.StaminaCost = null;
            illusion4b.UpkeepCost = null;
            illusion4b.FatigueCost = 10;
            illusion4b.Tier = 4;
            illusion4b.TierBenefitDescription = "Gain Light Concealment vs. Ranged attacks";
            illusion4b.Tree = TalentTree.Illusion;
            illusion4b.TreeName = "Illusion";
            illusion4b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion4b);

            Talent illusion4c = new Talent();
            illusion4c.Name = "Misdirect";
            illusion4c.Type = TalentType.TriggeredAction;
            illusion4c.Action = ActionType.Reaction;
            illusion4c.DescriptionFluff = "You confound your enemy, obscuring your true location.";
            illusion4c.Description = "You can cause the Triggering attack to automatically miss. [8 Stamina]";
            illusion4c.ClarifyingText = "Triggering Action: you are targeted by a Melee or Ranged attack";
            illusion4c.StaminaCost = 8;
            illusion4c.UpkeepCost = null;
            illusion4c.Tier = 4;
            illusion4c.TierBenefitDescription = "Gain Light Concealment vs. Ranged attacks";
            illusion4c.Tree = TalentTree.Illusion;
            illusion4c.TreeName = "Illusion";
            illusion4c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion4c);
            #endregion
            #region T5
            Talent illusion5a = new Talent();
            illusion5a.Name = "Clone Army";
            illusion5a.Type = TalentType.Maneuver;
            illusion5a.Action = ActionType.Combat;
            illusion5a.DescriptionFluff = "You flood the battlefield with duplicates of yourself.";
            illusion5a.Description = "As the Clone spell, except you can create a number of clones up to your Willpower. All clones can be commanded as if they were a single companion, otherwise the spell works the same. [12/3 Stamina]";
            illusion5a.ClarifyingText = "";
            illusion5a.StaminaCost = 12;
            illusion5a.UpkeepCost = 3;
            illusion5a.Tier = 5;
            illusion5a.TierBenefitDescription = "Gain +2 to all Social Skill Checks";
            illusion5a.Tree = TalentTree.Illusion;
            illusion5a.TreeName = "Illusion";
            illusion5a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion5a);

            Talent illusion5b = new Talent();
            illusion5b.Name = "False Death";
            illusion5b.Type = TalentType.TriggeredAction;
            illusion5b.Action = ActionType.Reaction;
            illusion5b.DescriptionFluff = "You fake your own death, only to fight another day.";
            illusion5b.Description = "You craft an illusion of being hit by an attack and the Invisibility Enhancement is cast on you as part of this Triggered Action (maintain as usual). The illusion of you appears to die from the Triggering attack.Make a Conjuration Check, the result becomes the MCR for people to detect the illusion with Perception checks. [8 Stamina]";
            illusion5b.ClarifyingText = "Triggering Action: you are missed by an attack";
            illusion5b.StaminaCost = 8;
            illusion5b.UpkeepCost = null;
            illusion5b.Tier = 5;
            illusion5b.TierBenefitDescription = "Gain +2 to all Social Skill Checks";
            illusion5b.Tree = TalentTree.Illusion;
            illusion5b.TreeName = "Illusion";
            illusion5b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion5b);

            Talent illusion5c = new Talent();
            illusion5c.Name = "Veiled Appearance";
            illusion5c.Type = TalentType.Benefit;
            illusion5c.Action = ActionType.None;
            illusion5c.DescriptionFluff = "You hardly remember what you actually look like anymore.";
            illusion5c.Description = "Gain a +5 to Deception checks to disguise. You can use Conjuration in place of Deception when making Disguise checks.You can make a Disguise Check as a Combat Action.";
            illusion5c.ClarifyingText = "";
            illusion5c.StaminaCost = null;
            illusion5c.UpkeepCost = null;
            illusion5c.Tier = 5;
            illusion5c.TierBenefitDescription = "Gain +2 to all Social Skill Checks";
            illusion5c.Tree = TalentTree.Illusion;
            illusion5c.TreeName = "Illusion";
            illusion5c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(illusion5c);
            #endregion
            #endregion
            #region Incantation (Alteration)
            #region T1
            Talent incantation1a = new Talent();
            incantation1a.Name = "Enchanter";
            incantation1a.Type = TalentType.Benefit;
            incantation1a.Action = ActionType.None;
            incantation1a.DescriptionFluff = "You are gifted with the application of arcane modi cations.";
            incantation1a.Description = "Reduce the cost of arcane Mods you craft by 10%.";
            incantation1a.ClarifyingText = "";
            incantation1a.StaminaCost = null;
            incantation1a.UpkeepCost = null;
            incantation1a.Tier = 1;
            incantation1a.TierBenefitDescription = "You can consume Potions, Elixirs and Reagents as a Free Action";
            incantation1a.Tree = TalentTree.Incantation;
            incantation1a.TreeName = "Incantation";
            incantation1a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation1a);

            Talent incantation1b = new Talent();
            incantation1b.Name = "Seeking Weapon";
            incantation1b.Type = TalentType.Enhancement;
            incantation1b.Action = ActionType.Quick;
            incantation1b.DescriptionFluff = "You enhance a weapon to steer towards the intended target.";
            incantation1b.Description = "Weapon gains +1 to Accuracy.";
            incantation1b.ClarifyingText = "Enhancement: target touched weapon [Stamina 4/1]";
            incantation1b.StaminaCost = 4;
            incantation1b.UpkeepCost = 1;
            incantation1b.Tier = 1;
            incantation1b.TierBenefitDescription = "You can consume Potions, Elixirs and Reagents as a Free Action";
            incantation1b.Tree = TalentTree.Incantation;
            incantation1b.TreeName = "Incantation";
            incantation1b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation1b);

            Talent incantation1c = new Talent();
            incantation1c.Name = "Mana Blast";
            incantation1c.Type = TalentType.Maneuver;
            incantation1c.Action = ActionType.Combat;
            incantation1c.DescriptionFluff = "You blast your foe with pure magic.";
            incantation1c.Description = "Spell {Ranged (Pistol) +0/+2 Force} [4 Stamina]";
            incantation1c.ClarifyingText = "";
            incantation1c.StaminaCost = 4;
            incantation1c.UpkeepCost = null;
            incantation1c.Tier = 1;
            incantation1c.TierBenefitDescription = "You can consume Potions, Elixirs and Reagents as a Free Action";
            incantation1c.Tree = TalentTree.Incantation;
            incantation1c.TreeName = "Incantation";
            incantation1c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation1c);
            #endregion
            #region T2
            Talent incantation2a = new Talent();
            incantation2a.Name = "Mana Armor";
            incantation2a.Type = TalentType.Enhancement;
            incantation2a.Action = ActionType.Quick;
            incantation2a.DescriptionFluff = "You blanket yourself in a shell of magic.";
            incantation2a.Description = "Increase your Armor Value and Ranged Defenses by 1.";
            incantation2a.ClarifyingText = "Enhancement: target self [6/2 Stamina]";
            incantation2a.StaminaCost = 6;
            incantation2a.UpkeepCost = 2;
            incantation2a.Tier = 2;
            incantation2a.TierBenefitDescription = "You can equip 2 Talismans at the same time.";
            incantation2a.Tree = TalentTree.Incantation;
            incantation2a.TreeName = "Incantation";
            incantation2a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation2a);

            Talent incantation2b = new Talent();
            incantation2b.Name = "Arcane Seal";
            incantation2b.Type = TalentType.Ritual;
            incantation2b.Action = ActionType.Combat;
            incantation2b.DescriptionFluff = "You seal a doorway with a layer of hardened magic.";
            incantation2b.Description = "Lock a portal to prevent it from being opened. Result of Alteration Check is MCR of either Alteration or thievery Check to unlock. A Locked portal gains a + 5 bonus to its armor. [1 Fatigue]";
            incantation2b.ClarifyingText = "";
            incantation2b.StaminaCost = null;
            incantation2b.UpkeepCost = null;
            incantation2b.FatigueCost = 1;
            incantation2b.Tier = 2;
            incantation2b.TierBenefitDescription = "You can equip 2 Talismans at the same time.";
            incantation2b.Tree = TalentTree.Incantation;
            incantation2b.TreeName = "Incantation";
            incantation2b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation2b);

            Talent incantation2c = new Talent();
            incantation2c.Name = "Trinket Maker";
            incantation2c.Type = TalentType.Benefit;
            incantation2c.Action = ActionType.None;
            incantation2c.DescriptionFluff = "You experiment often with expendable forms of magical enchanting.";
            incantation2c.Description = "Reduce the cost to craft Elixirs, Reagents, and Potions by 10%.";
            incantation2c.ClarifyingText = "";
            incantation2c.StaminaCost = null;
            incantation2c.UpkeepCost = null;
            incantation2c.Tier = 2;
            incantation2c.TierBenefitDescription = "You can equip 2 Talismans at the same time.";
            incantation2c.Tree = TalentTree.Incantation;
            incantation2c.TreeName = "Incantation";
            incantation2c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation2c);
            #endregion
            #region T3
            Talent incantation3a = new Talent();
            incantation3a.Name = "Mana Shield";
            incantation3a.Type = TalentType.TriggeredAction;
            incantation3a.Action = ActionType.Reaction;
            incantation3a.DescriptionFluff = "You erect a short lived barrier of magic to protect you.";
            incantation3a.Description = "Reduce the Crit Stage of the Triggering attack by 1 and the damage of the Triggering attack by 6.";
            incantation3a.ClarifyingText = "Triggering Action: you are hit by attack [8 Stamina]";
            incantation3a.StaminaCost = 8;
            incantation3a.UpkeepCost = null;
            incantation3a.Tier = 3;
            incantation3a.TierBenefitDescription = "You gain a 10% reduction to the cost of all Magic Items, Amps, and Arcane Mods";
            incantation3a.Tree = TalentTree.Incantation;
            incantation3a.TreeName = "Incantation";
            incantation3a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation3a);

            Talent incantation3b = new Talent();
            incantation3b.Name = "Improved Seeking Weapon";
            incantation3b.Type = TalentType.Enhancement;
            incantation3b.Action = ActionType.Quick;
            incantation3b.DescriptionFluff = "You enchant a weapon to possess a homing - like quality.";
            incantation3b.Description = "Targeted weapon gains +1 Accuracy, +1 damage, and +1 CM. Does not stack with Seeking Weapon. [8/2 Stamina]";
            incantation3b.ClarifyingText = "";
            incantation3b.StaminaCost = 8;
            incantation3b.UpkeepCost = 2;
            incantation3b.Tier = 3;
            incantation3b.TierBenefitDescription = "You gain a 10% reduction to the cost of all Magic Items, Amps, and Arcane Mods";
            incantation3b.Tree = TalentTree.Incantation;
            incantation3b.TreeName = "Incantation";
            incantation3b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation3b);

            Talent incantation3c = new Talent();
            incantation3c.Name = "Relic Crafter";
            incantation3c.Type = TalentType.Benefit;
            incantation3c.Action = ActionType.None;
            incantation3c.DescriptionFluff = "You are practiced in creating permanent magical items.";
            incantation3c.Description = "Reduce the cost of Magic Items that you craft by 10%.";
            incantation3c.ClarifyingText = "";
            incantation3c.StaminaCost = null;
            incantation3c.UpkeepCost = null;
            incantation3c.Tier = 3;
            incantation3c.TierBenefitDescription = "You gain a 10% reduction to the cost of all Magic Items, Amps, and Arcane Mods";
            incantation3c.Tree = TalentTree.Incantation;
            incantation3c.TreeName = "Incantation";
            incantation3c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation3c);
            #endregion
            #region T4
            Talent incantation4a = new Talent();
            incantation4a.Name = "Mana Sphere";
            incantation4a.Type = TalentType.Enhancement;
            incantation4a.Action = ActionType.Quick;
            incantation4a.DescriptionFluff = "You erect a dome of pure magic.";
            incantation4a.Description = "Create a dome with a 10’ radius around you. The Sphere provides Cover with a Toughness of 10, but does not affect movement through it.";
            incantation4a.ClarifyingText = "Enhancement: target self [10/3 Stamina]";
            incantation4a.StaminaCost = 10;
            incantation4a.UpkeepCost = 3;
            incantation4a.Tier = 4;
            incantation4a.TierBenefitDescription = "At the start of every day you can select (your total Alteration skill x 1,000U) worth of expendable items that can be crafted using the Enchantment skill. These items cannot be saved past the beginning of the following day. They cannot be sold, salvaged or otherwise mined for resources. These items can only be used by you.";
            incantation4a.Tree = TalentTree.Incantation;
            incantation4a.TreeName = "Incantation";
            incantation4a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation4a);

            Talent incantation4b = new Talent();
            incantation4b.Name = "Arcane Ward";
            incantation4b.Type = TalentType.Ritual;
            incantation4b.Action = ActionType.None;
            incantation4b.DescriptionFluff = "You secure an area with a magical field.";
            incantation4b.Description = "You seal an area against entry. When you perform this Ritual, make an Alteration Check. To pass through the seal, a creature must pass a Presence +Willpower Check or a Strength +Fortitude vs an MCR equal to the result of the Alteration Check.Teleportation into the area of the ward is impossible. The seal can be damaged; it has armor equal to the original Alteration Check and the same amount of HP.It regenerates HP equal to your Presence at the beginning of each of your turns. You can spend a Combat Action to regenerate the ward faster [1 Stamina per point of regeneration]. You and anyone you designate can enter the ward without restriction. This ward is visible and obvious. [6 Fatigue as long as the ward exists]";
            incantation4b.ClarifyingText = "Ritual: 10-minute cast, target area with 20’ radius centered on you";
            incantation4b.StaminaCost = null;
            incantation4b.UpkeepCost = null;
            incantation4b.FatigueCost = 6;
            incantation4b.Tier = 4;
            incantation4b.TierBenefitDescription = "At the start of every day you can select (your total Alteration skill x 1,000U) worth of expendable items that can be crafted using the Enchantment skill. These items cannot be saved past the beginning of the following day. They cannot be sold, salvaged or otherwise mined for resources. These items can only be used by you.";
            incantation4b.Tree = TalentTree.Incantation;
            incantation4b.TreeName = "Incantation";
            incantation4b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation4b);

            Talent incantation4c = new Talent();
            incantation4c.Name = "Emergency Enchantment";
            incantation4c.Type = TalentType.Enhancement;
            incantation4c.Action = ActionType.Quick;
            incantation4c.DescriptionFluff = "You enhance an item through shear force of will.";
            incantation4c.Description = "Apply a temporary arcane Mod to an item without using any Mod slots or needing to increase the Quality of the item.";
            incantation4c.ClarifyingText = "[4 per Grade (Basic, Superior, or Advanced)/4 Stamina]";
            incantation4c.StaminaCost = 12;
            incantation4c.UpkeepCost = 4;
            incantation4c.Tier = 4;
            incantation4c.TierBenefitDescription = "At the start of every day you can select (your total Alteration skill x 1,000U) worth of expendable items that can be crafted using the Enchantment skill. These items cannot be saved past the beginning of the following day. They cannot be sold, salvaged or otherwise mined for resources. These items can only be used by you.";
            incantation4c.Tree = TalentTree.Incantation;
            incantation4c.TreeName = "Incantation";
            incantation4c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation4c);
            #endregion
            #region T5
            Talent incantation5a = new Talent();
            incantation5a.Name = "Charged Seal";
            incantation5a.Type = TalentType.Benefit;
            incantation5a.Action = ActionType.None;
            incantation5a.DescriptionFluff = "Your wards pack a punch.";
            incantation5a.Description = "Your Arcane Seals and Arcane Wards cause damage to anyone who fails to pass through or damage one of them.Such creatures take a damage roll as if you had hit them with a Mana Blast spell.";
            incantation5a.ClarifyingText = "";
            incantation5a.StaminaCost = null;
            incantation5a.UpkeepCost = null;
            incantation5a.Tier = 5;
            incantation5a.TierBenefitDescription = "All Magic Items you wear have their Spirit cost reduced by 1 (to a minimum of 1 Spirit cost)";
            incantation5a.Tree = TalentTree.Incantation;
            incantation5a.TreeName = "Incantation";
            incantation5a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation5a);

            Talent incantation5b = new Talent();
            incantation5b.Name = "Magic Compatibility";
            incantation5b.Type = TalentType.Benefit;
            incantation5b.Action = ActionType.None;
            incantation5b.DescriptionFluff = "Your body is inured to the rigors of wearing magical items.";
            incantation5b.Description = "Your Spirit Attribute increases by 2.";
            incantation5b.ClarifyingText = "";
            incantation5b.StaminaCost = null;
            incantation5b.UpkeepCost = null;
            incantation5b.Tier = 5;
            incantation5b.TierBenefitDescription = "All Magic Items you wear have their Spirit cost reduced by 1 (to a minimum of 1 Spirit cost)";
            incantation5b.Tree = TalentTree.Incantation;
            incantation5b.TreeName = "Incantation";
            incantation5b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation5b);

            Talent incantation5c = new Talent();
            incantation5c.Name = "Disenchantment";
            incantation5c.Type = TalentType.Benefit;
            incantation5c.Action = ActionType.None;
            incantation5c.DescriptionFluff = "";
            incantation5c.Description = "You have learned to extract the pure magical essence of enchanted items. With 1 hour work, you can extract ½ the value of any item or Mod that was made using the Enchantment Skill as “raw materials.” These materials have little weight and take up little space (size 1 and 2 pounds per 10, 000U in value). Raw materials obtained in this way can only be used to craftor Mod items while using the Enchantment Skill.";
            incantation5c.ClarifyingText = "";
            incantation5c.StaminaCost = null;
            incantation5c.UpkeepCost = null;
            incantation5c.Tier = 5;
            incantation5c.TierBenefitDescription = "All Magic Items you wear have their Spirit cost reduced by 1 (to a minimum of 1 Spirit cost)";
            incantation5c.Tree = TalentTree.Incantation;
            incantation5c.TreeName = "Incantation";
            incantation5c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(incantation5c);
            #endregion
            #endregion
            #region Infernalism (Invocation)
            #region T1
            Talent infernalism1a = new Talent();
            infernalism1a.Name = "Dark Blast";
            infernalism1a.Type = TalentType.Maneuver;
            infernalism1a.Action = ActionType.Combat;
            infernalism1a.DescriptionFluff = "";
            infernalism1a.Description = "Spell {Ranged (Pistol) +0/+2 Unholy)} [4 Stamina]";
            infernalism1a.ClarifyingText = "";
            infernalism1a.StaminaCost = 4;
            infernalism1a.UpkeepCost = null;
            infernalism1a.Tier = 1;
            infernalism1a.TierBenefitDescription = "Gain +1 to Intimidation and +1 HP per Track";
            infernalism1a.Tree = TalentTree.Infernalism;
            infernalism1a.TreeName = "Infernalism";
            infernalism1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism1a);

            Talent infernalism1b = new Talent();
            infernalism1b.Name = "Reaving";
            infernalism1b.Type = TalentType.Benefit;
            infernalism1b.Action = ActionType.None;
            infernalism1b.DescriptionFluff = "";
            infernalism1b.Description = "Regain 1 HP in 1st Track whenever a creature is rendered Unconscious within 10’ of you.";
            infernalism1b.ClarifyingText = "";
            infernalism1b.StaminaCost = null;
            infernalism1b.UpkeepCost = null;
            infernalism1b.Tier = 1;
            infernalism1b.TierBenefitDescription = "Gain +1 to Intimidation and +1 HP per Track";
            infernalism1b.Tree = TalentTree.Infernalism;
            infernalism1b.TreeName = "Infernalism";
            infernalism1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism1b);

            Talent infernalism1c = new Talent();
            infernalism1c.Name = "Infernal Familiar";
            infernalism1c.Type = TalentType.Ritual;
            infernalism1c.Action = ActionType.None;
            infernalism1c.DescriptionFluff = "You are gifted the power to summon servants by your master.";
            infernalism1c.Description = "You summon a size 2 Natural Companion of your level -2 with the Infernal quality. The use of this Ritual requires the sacrifice of a living creature of your level -2. There are 3 di erent Companions to choose from each with their own set of abilities: • Imps gain Flight(2), Skilled(2), and Camouftage(Light). • Hellcats gain Animalistic(Medium), Feral(1), and Energy Infused(Fire). • Hellions gain Venomous, Honed(1), and Ranged attack(Pistol / Fire and Poison).";
            infernalism1c.ClarifyingText = "Ritual: 1 hour cast, target adjacent space. [2 Fatigue for as long as the Familiar exists and for 8 hours after]";
            infernalism1c.StaminaCost = null;
            infernalism1c.UpkeepCost = null;
            infernalism1c.FatigueCost = 2;
            infernalism1c.Tier = 1;
            infernalism1c.TierBenefitDescription = "Gain +1 to Intimidation and +1 HP per Track";
            infernalism1c.Tree = TalentTree.Infernalism;
            infernalism1c.TreeName = "Infernalism";
            infernalism1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism1c);
            #endregion
            #region T2
            Talent infernalism2a = new Talent();
            infernalism2a.Name = "Unnatural Warp";
            infernalism2a.Type = TalentType.Benefit;
            infernalism2a.Action = ActionType.None;
            infernalism2a.DescriptionFluff = "";
            infernalism2a.Description = "Choose 1 of the following warps: Horns (+1 Unarmed CM); Claws (+1 Unarmed damage); Satyr Legs(+1 Speed); or Jaws (+2 Wrestling damage). This Talent can be selected multiple times, each time a di erent warp must be selected.";
            infernalism2a.ClarifyingText = "";
            infernalism2a.StaminaCost = null;
            infernalism2a.UpkeepCost = null;
            infernalism2a.Tier = 2;
            infernalism2a.TierBenefitDescription = "Gain +1 to Unholy damage";
            infernalism2a.Tree = TalentTree.Infernalism;
            infernalism2a.TreeName = "Infernalism";
            infernalism2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism2a);

            Talent infernalism2b = new Talent();
            infernalism2b.Name = "Unholy Fire";
            infernalism2b.Type = TalentType.Enhancement;
            infernalism2b.Action = ActionType.Quick;
            infernalism2b.DescriptionFluff = "";
            infernalism2b.Description = "Add 2 Unholy and Fire damage to a single weapon you wield. [6/2 Stamina]";
            infernalism2b.ClarifyingText = "Enhancement: target held weapon";
            infernalism2b.StaminaCost = 6;
            infernalism2b.UpkeepCost = 2;
            infernalism2b.Tier = 2;
            infernalism2b.TierBenefitDescription = "Gain +1 to Unholy damage";
            infernalism2b.Tree = TalentTree.Infernalism;
            infernalism2b.TreeName = "Infernalism";
            infernalism2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism2b);

            Talent infernalism2c = new Talent();
            infernalism2c.Name = "Seething";
            infernalism2c.Type = TalentType.Trick;
            infernalism2c.Action = ActionType.Quick;
            infernalism2c.DescriptionFluff = "";
            infernalism2c.Description = "You can lose up to your Willpower in HP. For every HP you lose, you can regain 2 spent Stamina.";
            infernalism2c.ClarifyingText = "";
            infernalism2c.StaminaCost = 0;
            infernalism2c.UpkeepCost = null;
            infernalism2c.Tier = 2;
            infernalism2c.TierBenefitDescription = "Gain +1 to Unholy damage";
            infernalism2c.Tree = TalentTree.Infernalism;
            infernalism2c.TreeName = "Infernalism";
            infernalism2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism2c);
            #endregion
            #region T3
            Talent infernalism3a = new Talent();
            infernalism3a.Name = "Sulfuric Blast";
            infernalism3a.Type = TalentType.Maneuver;
            infernalism3a.Action = ActionType.Combat;
            infernalism3a.DescriptionFluff = "";
            infernalism3a.Description = "Spell {Area (20’ cone)/+6 Poison and Force} [8 Stamina]";
            infernalism3a.ClarifyingText = "";
            infernalism3a.StaminaCost = 8;
            infernalism3a.UpkeepCost = null;
            infernalism3a.Tier = 3;
            infernalism3a.TierBenefitDescription = "You are considered an Unholy target and reduce the cost to activate Infernalism Spells by 1";
            infernalism3a.Tree = TalentTree.Infernalism;
            infernalism3a.TreeName = "Infernalism";
            infernalism3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism3a);

            Talent infernalism3b = new Talent();
            infernalism3b.Name = "Infernal Aura";
            infernalism3b.Type = TalentType.Enhancement;
            infernalism3b.Action = ActionType.Quick;
            infernalism3b.DescriptionFluff = "";
            infernalism3b.Description = "Your Unarmed attacks gain +2 Unholy damage and you gain Light Fortification vs. all Resolve and Body attacks. Holy attackers using Close Combat or Unarmed attacks against you su er an Unholy damage roll with a modifier equal to your Presence Attribute.";
            infernalism3b.ClarifyingText = "Enhancement: target self [8/2 Stamina]";
            infernalism3b.StaminaCost = null;
            infernalism3b.UpkeepCost = null;
            infernalism3b.Tier = 3;
            infernalism3b.TierBenefitDescription = "You are considered an Unholy target and reduce the cost to activate Infernalism Spells by 1";
            infernalism3b.Tree = TalentTree.Infernalism;
            infernalism3b.TreeName = "Infernalism";
            infernalism3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism3b);

            Talent infernalism3c = new Talent();
            infernalism3c.Name = "Empower Familiar";
            infernalism3c.Type = TalentType.Benefit;
            infernalism3c.Action = ActionType.None;
            infernalism3c.DescriptionFluff = "";
            infernalism3c.Description = "When you cast Infernal Familiar, you can choose to empower the familiar. If you do, the casting time is doubled and the sacrifice must be two levels higher. Empowered familiars are of the same level as you and gain the following abilities. This benefit can be taken multiple times. You can only have one Empowered Familiar for each time you have taken this benefit. • Imps gain Flight(1), Skilled(3), Shapechanger, Natural Weapons(Light) and Camouflage(Light). • Hellcats Become Size 3 and Gain Animalistic(Medium), Feral(1), Energy Infused(Fire), Area attack(20’ cone / Fire) and Tough(2). • Hellions Gain Venomous, Natural Weapons(Light), Honed(2) Ranged attack(Pistol / Fire and Poison) and Area attack(10’radius at Pistol range / Fire and Poison).";
            infernalism3c.ClarifyingText = "";
            infernalism3c.StaminaCost = null;
            infernalism3c.UpkeepCost = null;
            infernalism3c.Tier = 3;
            infernalism3c.TierBenefitDescription = "You are considered an Unholy target and reduce the cost to activate Infernalism Spells by 1";
            infernalism3c.Tree = TalentTree.Infernalism;
            infernalism3c.TreeName = "Infernalism";
            infernalism3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism3c);
            #endregion
            #region T4
            Talent infernalism4a = new Talent();
            infernalism4a.Name = "Summon Deamon";
            infernalism4a.Type = TalentType.Ritual;
            infernalism4a.Action = ActionType.None;
            infernalism4a.DescriptionFluff = "";
            infernalism4a.Description = "You summon a Size 3 or 4 (your choice) Natural Companion of your level with the Infernal quality. The demon gains 4 of the following possible abilities.Where applicable, an ability can be taken multiple times. Animalistic(Light, Medium or Heavy), Feral(2), Brawny, Slight, Honed(2), any non - Smiting, non - Prayer Talent Ability of Tier 3 or lower, Area attack(30’ cone or 15’ radius at     Pistol range / any combination of Fire, Poison, or Unholy), Ranged attack (Pistol, SMG or Shotgun/       any combination of Fire, Poison, or Unholy), Energy Infused (Fire, Poison or Unholy), Flight(1 if      Size 3, 2 if Size 4), Tough(2), Venomous, Natural Weapons (any), Natural Armor(any).";
            infernalism4a.ClarifyingText = "Ritual: 24-hour cast, target adjacent space [5 Stamina for as long as the deamon exists and for 8 hours after]";
            infernalism4a.StaminaCost = null;
            infernalism4a.UpkeepCost = null;
            infernalism4a.Tier = 4;
            infernalism4a.TierBenefitDescription = "Gain +1 to Durability";
            infernalism4a.Tree = TalentTree.Infernalism;
            infernalism4a.TreeName = "Infernalism";
            infernalism4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism4a);

            Talent infernalism4b = new Talent();
            infernalism4b.Name = "Unholy Strength";
            infernalism4b.Type = TalentType.Benefit;
            infernalism4b.Action = ActionType.None;
            infernalism4b.DescriptionFluff = "";
            infernalism4b.Description = "Gain +4 to Stamina and +1 to Stamina Regen.";
            infernalism4b.ClarifyingText = "";
            infernalism4b.StaminaCost = null;
            infernalism4b.UpkeepCost = null;
            infernalism4b.Tier = 4;
            infernalism4b.TierBenefitDescription = "Gain +1 to Durability";
            infernalism4b.Tree = TalentTree.Infernalism;
            infernalism4b.TreeName = "Infernalism";
            infernalism4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism4b);

            Talent infernalism4c = new Talent();
            infernalism4c.Name = "Condemnation";
            infernalism4c.Type = TalentType.Maneuver;
            infernalism4c.Action = ActionType.Combat;
            infernalism4c.DescriptionFluff = "";
            infernalism4c.Description = "Spell {Area (10’ radius) +0/+0 Unholy} [10 Stamina]";
            infernalism4c.ClarifyingText = "Anyone damaged by the attack is Dazed, Slowed, and Weakened (until Resisted).";
            infernalism4c.StaminaCost = 10;
            infernalism4c.UpkeepCost = null;
            infernalism4c.Tier = 4;
            infernalism4c.TierBenefitDescription = "Gain +1 to Durability";
            infernalism4c.Tree = TalentTree.Infernalism;
            infernalism4c.TreeName = "Infernalism";
            infernalism4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism4c);
            #endregion
            #region T5
            Talent infernalism5a = new Talent();
            infernalism5a.Name = "Brimstone";
            infernalism5a.Type = TalentType.Maneuver;
            infernalism5a.Action = ActionType.Combat;
            infernalism5a.DescriptionFluff = "";
            infernalism5a.Description = "Spell {Area (15’ radius within Rifle range) +0/+6 Poison and Fire) [12 Stamina]";
            infernalism5a.ClarifyingText = "The area becomes a Hazard that causes 1/2 the original damage modifier of Poison and fire damage for the rest of the encounter.";
            infernalism5a.StaminaCost = 12;
            infernalism5a.UpkeepCost = null;
            infernalism5a.Tier = 5;
            infernalism5a.TierBenefitDescription = "Gain Fortified vs. Fire and Unholy damage";
            infernalism5a.Tree = TalentTree.Infernalism;
            infernalism5a.TreeName = "Infernalism";
            infernalism5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism5a);

            Talent infernalism5b = new Talent();
            infernalism5b.Name = "Possession";
            infernalism5b.Type = TalentType.Enhancement;
            infernalism5b.Action = ActionType.Quick;
            infernalism5b.DescriptionFluff = "";
            infernalism5b.Description = "Add your Presence Attribute to the damage of all Fire, Unholy, or Poison attacks that you make. You become immune to Fire and Poison damage. Your Fire, Unholy, and Poison attacks gain CM +1 and Lethal +1.At the beginning of each turn that this power is in e ect you regain 1 HP in your 1st or 2nd Track. Every round that this Enhancement is in e ect you must use your Combat Action to make an attack against an enemy. If you do not, the GM selects that Combat Action for you, using any powers and as much Stamina as possible to create as much destruction as possible to friends and foes alike.After that action is resolved, you can choose to end this enchantment.";
            infernalism5b.ClarifyingText = "Enhancement: target self [12/3 Stamina]";
            infernalism5b.StaminaCost = 12;
            infernalism5b.UpkeepCost = 3;
            infernalism5b.Tier = 5;
            infernalism5b.TierBenefitDescription = "Gain Fortified vs. Fire and Unholy damage";
            infernalism5b.Tree = TalentTree.Infernalism;
            infernalism5b.TreeName = "Infernalism";
            infernalism5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism5b);

            Talent infernalism5c = new Talent();
            infernalism5c.Name = "Soul Rend";
            infernalism5c.Type = TalentType.Benefit;
            infernalism5c.Action = ActionType.None;
            infernalism5c.DescriptionFluff = "";
            infernalism5c.Description = "When a living creature is rendered Unconscious within 10’ of you, regain 4 Stamina and 1 HP in either the 1st or 2nd Track.";
            infernalism5c.ClarifyingText = "";
            infernalism5c.StaminaCost = null;
            infernalism5c.UpkeepCost = null;
            infernalism5c.Tier = 5;
            infernalism5c.TierBenefitDescription = "Gain Fortified vs. Fire and Unholy damage";
            infernalism5c.Tree = TalentTree.Infernalism;
            infernalism5c.TreeName = "Infernalism";
            infernalism5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(infernalism5c);
            #endregion
            #endregion
            #region Kinesis (Alteration)
            #region T1
            Talent kinesis1a = new Talent();
            kinesis1a.Name = "Kinetic Leap";
            kinesis1a.Type = TalentType.Enhancement;
            kinesis1a.Action = ActionType.Quick;
            kinesis1a.DescriptionFluff = "";
            kinesis1a.Description = "You gain a bonus to jump checks equal to double your Alteration Skill.";
            kinesis1a.ClarifyingText = "Enhancement: target self [4/1 Stamina]";
            kinesis1a.StaminaCost = 4;
            kinesis1a.UpkeepCost = 1;
            kinesis1a.Tier = 1;
            kinesis1a.TierBenefitDescription = "Gain +1 to Durability vs. Ranged attacks";
            kinesis1a.Tree = TalentTree.Kinesis;
            kinesis1a.TreeName = "Kinesis";
            kinesis1a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis1a);

            Talent kinesis1b = new Talent();
            kinesis1b.Name = "Kinetic Blast";
            kinesis1b.Type = TalentType.Maneuver;
            kinesis1b.Action = ActionType.Combat;
            kinesis1b.DescriptionFluff = "";
            kinesis1b.Description = "Spell {Ranged (Pistol) +0/+2 Force) [4 Stamina]";
            kinesis1b.ClarifyingText = "";
            kinesis1b.StaminaCost = 4;
            kinesis1b.UpkeepCost = null;
            kinesis1b.Tier = 1;
            kinesis1b.TierBenefitDescription = "Gain +1 to Durability vs. Ranged attacks";
            kinesis1b.Tree = TalentTree.Kinesis;
            kinesis1b.TreeName = "Kinesis";
            kinesis1b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis1b);

            Talent kinesis1c = new Talent();
            kinesis1c.Name = "Manipulate";
            kinesis1c.Type = TalentType.Maneuver;
            kinesis1c.Action = ActionType.Combat;
            kinesis1c.DescriptionFluff = "";
            kinesis1c.Description = "Choose 1 of the e ects below. • (Trick)[1 Stamina per 10’ between you and the object] Retrieve an unattended object of size 6 or smaller within 30’. You must have a free hand. You become armed with the item. • (Spell)[4 / 1 Stamina] Perform acts of manually dexterity at Pistol range. Appropriate Skill Checks are still required (Smithing, Repair, etc.). • (Trick)[1 / 1 Stamina per e ect] You can make candle light, or can manipulate fire to make it dance. (No combat e ect.)";
            kinesis1c.ClarifyingText = "";
            kinesis1c.StaminaCost = 0;
            kinesis1c.UpkeepCost = null;
            kinesis1c.Tier = 1;
            kinesis1c.TierBenefitDescription = "Gain +1 to Durability vs. Ranged attacks";
            kinesis1c.Tree = TalentTree.Kinesis;
            kinesis1c.TreeName = "Kinesis";
            kinesis1c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis1c);
            #endregion
            #region T2
            Talent kinesis2a = new Talent();
            kinesis2a.Name = "Shield";
            kinesis2a.Type = TalentType.TriggeredAction;
            kinesis2a.Action = ActionType.Reaction;
            kinesis2a.DescriptionFluff = "";
            kinesis2a.Description = "Reduce the damage of the Triggering attack by one half. [2 Stamina per point of damage reduced]";
            kinesis2a.ClarifyingText = "";
            kinesis2a.StaminaCost = 0;
            kinesis2a.UpkeepCost = null;
            kinesis2a.Tier = 2;
            kinesis2a.TierBenefitDescription = "Gain +1 damage with Kinesis spells";
            kinesis2a.Tree = TalentTree.Kinesis;
            kinesis2a.TreeName = "Kinesis";
            kinesis2a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis2a);

            Talent kinesis2b = new Talent();
            kinesis2b.Name = "Slow Fall";
            kinesis2b.Type = TalentType.Maneuver;
            kinesis2b.Action = ActionType.Combat;
            kinesis2b.DescriptionFluff = "";
            kinesis2b.Description = "Treat the fall as if you fell 10 feet less per point of Stamina spent.";
            kinesis2b.ClarifyingText = "Triggering Action: you fall [1 Stamina per 10 feet reduced]";
            kinesis2b.StaminaCost = 0;
            kinesis2b.UpkeepCost = null;
            kinesis2b.Tier = 2;
            kinesis2b.TierBenefitDescription = "Gain +1 damage with Kinesis spells";
            kinesis2b.Tree = TalentTree.Kinesis;
            kinesis2b.TreeName = "Kinesis";
            kinesis2b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis2b);

            Talent kinesis2c = new Talent();
            kinesis2c.Name = "Telekinesis";
            kinesis2c.Type = TalentType.Maneuver;
            kinesis2c.Action = ActionType.Combat;
            kinesis2c.DescriptionFluff = "";
            kinesis2c.Description = "Choose 1 of the e ects below. • Move Objects: Spell{ no attack}             When manipulating unattended objects use Presence in place of Strength for max lifting capacity.If you spend additional Stamina, you can treat your e ective Strength as 1 point higher per 4 points of Stamina you spend(to a maximum e ective Strength increase of your Presence). This e ect lasts for one round. You spend your MI from your Speed to move items that you can lift. If you are spending additional Stamina to increase your lifting capacity each MI costs one additional Stamina. • Perform Wrestling Maneuvers: Spell { Melee(within 50’) / }             When making checks, roll your alteration skill for the Overpower checks.Attempts to break from your Wrestle target your Area / Resolve Defense. • Hurl: Spell { Ranged(Pistol) / (type depends on object hurled)}             Use this attack to hurl an unattended item within 10’ of you with a size up to your Presence Attribute at the target.If using an Improvised weapon, use the Improvised Attacks guidelines to determine the Accuracy and damage of the item. If using a standard weapon, use its Accuracy and Damage.When determining the total attack and damage of this attack, the Accuracy and Damage of the item Hurled is added directly to the normal Accuracy and Damage modifiers of this Kinesis spell. Use of this option causes a maintained Telekinesis to end.To use it again in a future round, the initial cost must be spent again.If no Telekinesis is currently maintained when used, spend initial cost only.";
            kinesis2c.ClarifyingText = "[6 Stamina on initial use / 2 Stamina on subsequent rounds when continuing Wrestling]";
            kinesis2c.StaminaCost = 6;
            kinesis2c.UpkeepCost = 2;
            kinesis2c.Tier = 2;
            kinesis2c.TierBenefitDescription = "Gain +1 damage with Kinesis spells";
            kinesis2c.Tree = TalentTree.Kinesis;
            kinesis2c.TreeName = "Kinesis";
            kinesis2c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis2c);
            #endregion
            #region T3
            Talent kinesis3a = new Talent();
            kinesis3a.Name = "Crush";
            kinesis3a.Type = TalentType.Benefit;
            kinesis3a.Action = ActionType.None;
            kinesis3a.DescriptionFluff = "";
            kinesis3a.Description = "You gain a +4 to damage rolls with Kinesis powers against opponents Wrestled by you through Telekinesis.";
            kinesis3a.ClarifyingText = "";
            kinesis3a.StaminaCost = null;
            kinesis3a.UpkeepCost = null;
            kinesis3a.Tier = 3;
            kinesis3a.TierBenefitDescription = "Gain +1 to Ranged Defense";
            kinesis3a.Tree = TalentTree.Kinesis;
            kinesis3a.TreeName = "Kinesis";
            kinesis3a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis3a);

            Talent kinesis3b = new Talent();
            kinesis3b.Name = "Overland Flight";
            kinesis3b.Type = TalentType.Maneuver;
            kinesis3b.Action = ActionType.Combat;
            kinesis3b.DescriptionFluff = "";
            kinesis3b.Description = "Gain a max fly Speed of your total Alteration skill x your Presence Attribute in MPH. While flying you cannot take Combat Actions. The Pilot (Air)Skill is used if Piloting Checks need to be made. This Ritual ends if you make an attack of take damage.";
            kinesis3b.ClarifyingText = "Ritual: Target self, 1-minute cast time [2 Fatigue +1 per hour of flight]";
            kinesis3b.StaminaCost = null;
            kinesis3b.UpkeepCost = null;
            kinesis3b.FatigueCost = 2;
            kinesis3b.Tier = 3;
            kinesis3b.TierBenefitDescription = "Gain +1 to Ranged Defense";
            kinesis3b.Tree = TalentTree.Kinesis;
            kinesis3b.TreeName = "Kinesis";
            kinesis3b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis3b);

            Talent kinesis3c = new Talent();
            kinesis3c.Name = "Levitate";
            kinesis3c.Type = TalentType.Enhancement;
            kinesis3c.Action = ActionType.Quick;
            kinesis3c.DescriptionFluff = "";
            kinesis3c.Description = "You can float up or down at a rate of 20’ per round. When moving along the ground, you can ignore the penalty for moving over rough terrain. You can walk on water as if it were solid ground.";
            kinesis3c.ClarifyingText = "Enhancement: target self only [6/2 Stamina]";
            kinesis3c.StaminaCost = 6;
            kinesis3c.UpkeepCost = 2;
            kinesis3c.Tier = 3;
            kinesis3c.TierBenefitDescription = "Gain +1 to Ranged Defense";
            kinesis3c.Tree = TalentTree.Kinesis;
            kinesis3c.TreeName = "Kinesis";
            kinesis3c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis3c);
            #endregion
            #region T4
            Talent kinesis4a = new Talent();
            kinesis4a.Name = "Fly";
            kinesis4a.Type = TalentType.Enhancement;
            kinesis4a.Action = ActionType.Quick;
            kinesis4a.DescriptionFluff = "";
            kinesis4a.Description = "You gain Flight[2]. When you maintain this Enhancement, you can pay 4 extra Stamina to double your Flight Speed.";
            kinesis4a.ClarifyingText = "Enhancement: target self [6/2 Stamina]";
            kinesis4a.StaminaCost = 6;
            kinesis4a.UpkeepCost = 2;
            kinesis4a.Tier = 4;
            kinesis4a.TierBenefitDescription = "Gain -1 to the maintenance cost of all Kinesis spells and Enhancements";
            kinesis4a.Tree = TalentTree.Kinesis;
            kinesis4a.TreeName = "Kinesis";
            kinesis4a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis4a);

            Talent kinesis4b = new Talent();
            kinesis4b.Name = "Improved Telekinesis";
            kinesis4b.Type = TalentType.Benefit;
            kinesis4b.Action = ActionType.None;
            kinesis4b.DescriptionFluff = "";
            kinesis4b.Description = "When using Telekinesis you treat your Presence as 2 points higher when determining maximum Lifting power, you gain + 2 to all attacks and overpower checks. You also gain a +2 to damage and you can choose to make the Hurl option an Area attack with a 10’ radius within the same range.";
            kinesis4b.ClarifyingText = "";
            kinesis4b.StaminaCost = null;
            kinesis4b.UpkeepCost = null;
            kinesis4b.Tier = 4;
            kinesis4b.TierBenefitDescription = "Gain -1 to the maintenance cost of all Kinesis spells and Enhancements";
            kinesis4b.Tree = TalentTree.Kinesis;
            kinesis4b.TreeName = "Kinesis";
            kinesis4b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis4b);

            Talent kinesis4c = new Talent();
            kinesis4c.Name = "Repellent Aura";
            kinesis4c.Type = TalentType.Enhancement;
            kinesis4c.Action = ActionType.Quick;
            kinesis4c.DescriptionFluff = "";
            kinesis4c.Description = "You gain the Girded Condition as well as a +2 to Durability vs. Physical attacks. While within 20’ of you enemies must pay 2 additional MI per MI toward you.";
            kinesis4c.ClarifyingText = "Enhancement: target self [10/2 Stamina]";
            kinesis4c.StaminaCost = 10;
            kinesis4c.UpkeepCost = 2;
            kinesis4c.Tier = 4;
            kinesis4c.TierBenefitDescription = "Gain -1 to the maintenance cost of all Kinesis spells and Enhancements";
            kinesis4c.Tree = TalentTree.Kinesis;
            kinesis4c.TreeName = "Kinesis";
            kinesis4c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis4c);
            #endregion
            #region T5
            Talent kinesis5a = new Talent();
            kinesis5a.Name = "Shockwave";
            kinesis5a.Type = TalentType.Maneuver;
            kinesis5a.Action = ActionType.Combat;
            kinesis5a.DescriptionFluff = "";
            kinesis5a.Description = "Spell {Area (20’ radius)/ +6 Force}[12 Stamina]";
            kinesis5a.ClarifyingText = "This attack gain Knockback and only targets enemies. Objects hit by this attack su er double damage over Durability.";
            kinesis5a.StaminaCost = 12;
            kinesis5a.UpkeepCost = null;
            kinesis5a.Tier = 5;
            kinesis5a.TierBenefitDescription = "Gain +1 to Area Defenses and +1 to Durability vs. Area attacks";
            kinesis5a.Tree = TalentTree.Kinesis;
            kinesis5a.TreeName = "Kinesis";
            kinesis5a.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis5a);

            Talent kinesis5b = new Talent();
            kinesis5b.Name = "Force Barrier";
            kinesis5b.Type = TalentType.Enhancement;
            kinesis5b.Action = ActionType.Quick;
            kinesis5b.DescriptionFluff = "";
            kinesis5b.Description = "Create a wall of force up to 10 ft. x 100 ft. x 1ft. It is visible but transparent. The barrier blocks movement and provides Cover with a Toughness of 20.";
            kinesis5b.ClarifyingText = "Enhancement: Target area up to 50’ away";
            kinesis5b.StaminaCost = 12;
            kinesis5b.UpkeepCost = 3;
            kinesis5b.Tier = 5;
            kinesis5b.TierBenefitDescription = "Gain +1 to Area Defenses and +1 to Durability vs. Area attacks";
            kinesis5b.Tree = TalentTree.Kinesis;
            kinesis5b.TreeName = "Kinesis";
            kinesis5b.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis5b);

            Talent kinesis5c = new Talent();
            kinesis5c.Name = "Kinetic Blender";
            kinesis5c.Type = TalentType.Maneuver;
            kinesis5c.Action = ActionType.Combat;
            kinesis5c.DescriptionFluff = "";
            kinesis5c.Description = "Spell {Area (Shotgun) +0/+4 Slashing}This attack is Full-Auto(25).";
            kinesis5c.ClarifyingText = "";
            kinesis5c.StaminaCost = 12;
            kinesis5c.UpkeepCost = null;
            kinesis5c.Tier = 5;
            kinesis5c.TierBenefitDescription = "Gain +1 to Area Defenses and +1 to Durability vs. Area attacks";
            kinesis5c.Tree = TalentTree.Kinesis;
            kinesis5c.TreeName = "Kinesis";
            kinesis5c.LinkedSkill = WeaponSkill.Alteration;
            Talents.Add(kinesis5c);
            #endregion
            #endregion
            #region Nature (Invocation)
            #region T1
            Talent nature1a = new Talent();
            nature1a.Name = "Pass Without a Trace";
            nature1a.Type = TalentType.Enhancement;
            nature1a.Action = ActionType.Quick;
            nature1a.DescriptionFluff = "";
            nature1a.Description = "You gain +2 to Stealth while in a natural setting and cannot be tracked.";
            nature1a.ClarifyingText = "Enhancement: target self [4/1 Stamina]";
            nature1a.StaminaCost = null;
            nature1a.UpkeepCost = null;
            nature1a.Tier = 1;
            nature1a.TierBenefitDescription = "Gain +1 to Survival";
            nature1a.Tree = TalentTree.Nature;
            nature1a.TreeName = "Nature";
            nature1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature1a);

            Talent nature1b = new Talent();
            nature1b.Name = "Plant Strike";
            nature1b.Type = TalentType.Maneuver;
            nature1b.Action = ActionType.Combat;
            nature1b.DescriptionFluff = "";
            nature1b.Description = "Spell {Ranged (Pistol) +0/+2 [Slashing, Bludgeoning, or Piercing]} [4 Stamina]";
            nature1b.ClarifyingText = "A living plant must be within 20’ of you or the target.";
            nature1b.StaminaCost = null;
            nature1b.UpkeepCost = null;
            nature1b.Tier = 1;
            nature1b.TierBenefitDescription = "Gain +1 to Survival";
            nature1b.Tree = TalentTree.Nature;
            nature1b.TreeName = "Nature";
            nature1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature1b);

            Talent nature1c = new Talent();
            nature1c.Name = "Thorns";
            nature1c.Type = TalentType.Enhancement;
            nature1c.Action = ActionType.Quick;
            nature1c.DescriptionFluff = "";
            nature1c.Description = "A successful Melee attack against you causes the attacker to lose 1/2 your Presence Attribute in HP. Double this number if the attacker uses an Unarmed attack.";
            nature1c.ClarifyingText = "Enhancement: target self [4/1 Stamina]";
            nature1c.StaminaCost = null;
            nature1c.UpkeepCost = null;
            nature1c.Tier = 1;
            nature1c.TierBenefitDescription = "Gain +1 to Survival";
            nature1c.Tree = TalentTree.Nature;
            nature1c.TreeName = "Nature";
            nature1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature1c);
            #endregion
            #region T2
            Talent nature2a = new Talent();
            nature2a.Name = "Mend Wound";
            nature2a.Type = TalentType.Ritual;
            nature2a.Action = ActionType.Combat;
            nature2a.DescriptionFluff = "";
            nature2a.Description = "You can perform any use of the Healing skill that normally takes 1-minute or less as part of this Ritual .When doing so you are always treated as though you are using a Med Unit.";
            nature2a.ClarifyingText = "Ritual: Combat Action cast, target adjacent creature [2 Fatigue]";
            nature2a.StaminaCost = null;
            nature2a.UpkeepCost = null;
            nature2a.FatigueCost = 2;
            nature2a.Tier = 2;
            nature2a.TierBenefitDescription = "You can use your Invocation Skill in place of the Healing Skill";
            nature2a.Tree = TalentTree.Nature;
            nature2a.TreeName = "Nature";
            nature2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature2a);

            Talent nature2b = new Talent();
            nature2b.Name = "Roots";
            nature2b.Type = TalentType.Maneuver;
            nature2b.Action = ActionType.Combat;
            nature2b.DescriptionFluff = "";
            nature2b.Description = "Spell {Area (30’ radius) +0/-2 Bludgeoning)} [6 Stamina]";
            nature2b.ClarifyingText = "On a hit, the targets become Slowed. On a Stage 2 or higher Crit the target cannot spend MI to move for as long as the secondary e ect lasts. The area of the spell becomes rough terrain for all enemies until the end of the encounter.";
            nature2b.StaminaCost = 6;
            nature2b.UpkeepCost = null;
            nature2b.Tier = 2;
            nature2b.TierBenefitDescription = "You can use your Invocation Skill in place of the Healing Skill";
            nature2b.Tree = TalentTree.Nature;
            nature2b.TreeName = "Nature";
            nature2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature2b);

            Talent nature2c = new Talent();
            nature2c.Name = "Barkskin";
            nature2c.Type = TalentType.Enhancement;
            nature2c.Action = ActionType.Quick;
            nature2c.DescriptionFluff = "";
            nature2c.Description = "You gain Light Fortification versus Slashing, Piercing, and Bludgeoning.";
            nature2c.ClarifyingText = "Enhancement: target self [6/2 Stamina]";
            nature2c.StaminaCost = 6;
            nature2c.UpkeepCost = 2;
            nature2c.Tier = 2;
            nature2c.TierBenefitDescription = "You can use your Invocation Skill in place of the Healing Skill";
            nature2c.Tree = TalentTree.Nature;
            nature2c.TreeName = "Nature";
            nature2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature2c);
            #endregion
            #region T3
            Talent nature3a = new Talent();
            nature3a.Name = "Cure Wound";
            nature3a.Type = TalentType.Ritual;
            nature3a.Action = ActionType.Combat;
            nature3a.DescriptionFluff = "";
            nature3a.Description = "Target creature regains your Willpower + Presence in HP. HP regained in the 3rd Track costs double. Remove the Poisoned state (but not damage already caused by it). This spell will automatically stabilize targets in the Bleed Out Track and Healing spent in that Track heals normally.";
            nature3a.ClarifyingText = "Ritual: Combat Action cast, target adjacent creature [5 Fatigue]";
            nature3a.StaminaCost = null;
            nature3a.UpkeepCost = null;
            nature3a.FatigueCost = 5;
            nature3a.Tier = 3;
            nature3a.TierBenefitDescription = "Gain +2 to all Presence based Skill Checks against natural Animalistic creatures. You no longer suffer the effects of rough terrain caused by plant life.";
            nature3a.Tree = TalentTree.Nature;
            nature3a.TreeName = "Nature";
            nature3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature3a);

            Talent nature3b = new Talent();
            nature3b.Name = "Plant Shape";
            nature3b.Type = TalentType.Ritual;
            nature3b.Action = ActionType.None;
            nature3b.DescriptionFluff = "";
            nature3b.Description = "You reshape existing material to create new shapes or objects. When you use this Ritual, make a Conjuration Check. The result x 100U is the maximum value of the new item(maximum UEU worth of materials that can be shaped; materials discounted for any reason do not count against this). One half the result cubed is the maximum volume in cubic feet worth of material that can be shaped(GM discretion).You can create any item that you gain a 15 % or better cost reduction from having the appropriate Trade Skill(Smithing for weapons, Construction for walls), failure to have the appropriate skill results in a product that is non - functional and unstable. All necessary materials  must be present to create the item (GM discretion). Items created through Shape are real and permanent. This Ritual can only be used to shape living plant materials.";
            nature3b.ClarifyingText = "Ritual: 1 hour cast, target item [3 Fatigue]";
            nature3b.StaminaCost = null;
            nature3b.UpkeepCost = null;
            nature3b.Tier = 3;
            nature3b.TierBenefitDescription = "Gain +2 to all Presence based Skill Checks against natural Animalistic creatures. You no longer suffer the effects of rough terrain caused by plant life.";
            nature3b.Tree = TalentTree.Nature;
            nature3b.TreeName = "Nature";
            nature3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature3b);

            Talent nature3c = new Talent();
            nature3c.Name = "Animal Companion";
            nature3c.Type = TalentType.Benefit;
            nature3c.Action = ActionType.None;
            nature3c.DescriptionFluff = "";
            nature3c.Description = "You gain a Size 2 Natural Companion of your level minus 1. Use the rules for commanding companions in combat; it takes 30 days to replace a dead Animal Companion. This Talent can be selected multiple times. The Animal Companion gains the Animalistic (any)property as well as one of the following sets: • The Hunter gains Camouflaged(light), Graceful(3), and Skilled(2). • The Bird of Prey gains Flight(1), Slight, and Nightsight. • The Beast gains Brawny, Tough(2), and Powerful(2 / any 1 Melee).";
            nature3c.ClarifyingText = "";
            nature3c.StaminaCost = null;
            nature3c.UpkeepCost = null;
            nature3c.Tier = 3;
            nature3c.TierBenefitDescription = "Gain +2 to all Presence based Skill Checks against natural Animalistic creatures. You no longer suffer the effects of rough terrain caused by plant life.";
            nature3c.Tree = TalentTree.Nature;
            nature3c.TreeName = "Nature";
            nature3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature3c);
            #endregion
            #region T4
            Talent nature4a = new Talent();
            nature4a.Name = "Dominate Animal";
            nature4a.Type = TalentType.Maneuver;
            nature4a.Action = ActionType.Combat;
            nature4a.DescriptionFluff = "";
            nature4a.Description = "Spell {Ranged (Pistol) +0/+4 Psychic} [10 Stamina]";
            nature4a.ClarifyingText = "Attack a Natural target. If damaged, the target becomes Controlled. Controlled creatures are commanded like companions and cannot pick any of their own actions.Any available action not commanded by you is lost with no benefit to the Controlled creatures. The target becomes Dazed and Exhausted (until Resisted). The target is Controlled until it is no longer su ering from Conditions caused by this attack.";
            nature4a.StaminaCost = 10;
            nature4a.UpkeepCost = null;
            nature4a.Tier = 4;
            nature4a.TierBenefitDescription = "You can use your Invocation skill in place of your Survival skill when making checks and in place of your Construction skill when crafting structures of natural materials.";
            nature4a.Tree = TalentTree.Nature;
            nature4a.TreeName = "Nature";
            nature4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature4a);

            Talent nature4b = new Talent();
            nature4b.Name = "Create";
            nature4b.Type = TalentType.Ritual;
            nature4b.Action = ActionType.None;
            nature4b.DescriptionFluff = "";
            nature4b.Description = "You form materials from nothing to create shapes or objects. When you use this Ritual make a Conjuration Check. The result x 100U is the maximum value of the new item(maximum UEU worth of materials that can be shaped, materials discounted for any reason do not count against this). One half the result cubed is the maximum volume of material that can be shaped (GM discretion). You can create any item that you would gain a 20 % or better cost reduction from having the appropriate Trade Skill(Smithing for weapons, Construction for walls), failure to have the appropriate skill results in a product that is non - functional and unstable. No materials must be present to create the item.Items created through the Create Ritual have a subtle but obvious magical sheen to them. This sheen identifies them as being essentially worthless in the eyes of anyone considering purchasing the item. This version of the Create spell can only be used to make items made of living plant material (usually structures).";
            nature4b.ClarifyingText = "Ritual: 1 hour cast, target item [5 Fatigue for as long as the item exists and for 8 hours after]";
            nature4b.StaminaCost = null;
            nature4b.UpkeepCost = null;
            nature4b.FatigueCost = 5;
            nature4b.Tier = 4;
            nature4b.TierBenefitDescription = "You can use your Invocation skill in place of your Survival skill when making checks and in place of your Construction skill when crafting structures of natural materials.";
            nature4b.Tree = TalentTree.Nature;
            nature4b.TreeName = "Nature";
            nature4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature4b);

            Talent nature4c = new Talent();
            nature4c.Name = "Hunting Partner";
            nature4c.Type = TalentType.Benefit;
            nature4c.Action = ActionType.None;
            nature4c.DescriptionFluff = "";
            nature4c.Description = "The size and level of your Companion increase by 1. This Talent can be selected multiple times, each time it must be applied to a di erent animal Companion.You may select an additional ability from the following list: Feral(2), Speedy(2), Quick(4), Powerful(2 / any 1 attack), Unerring(1 / any 1 attack), Mount, or Tough (2, 4 if selected for a Beast Companion.) ";
            nature4c.ClarifyingText = "";
            nature4c.StaminaCost = null;
            nature4c.UpkeepCost = null;
            nature4c.Tier = 4;
            nature4c.TierBenefitDescription = "You can use your Invocation skill in place of your Survival skill when making checks and in place of your Construction skill when crafting structures of natural materials.";
            nature4c.Tree = TalentTree.Nature;
            nature4c.TreeName = "Nature";
            nature4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature4c);
            #endregion
            #region T5
            Talent nature5a = new Talent();
            nature5a.Name = "Nature's Avatar";
            nature5a.Type = TalentType.Ritual;
            nature5a.Action = ActionType.None;
            nature5a.DescriptionFluff = "";
            nature5a.Description = "Create a Size 5 Plant Companion of your level to serve you. This Companion follows the normal rules for commanding companions in combat. The avatar knows all Nature Enhancements and Spells of Tier 3 or lower that you know. The Avatar gains Threatening(15’) and Animalistic (Medium).You can use a Combat Action to Heal the Companion[3 Stamina per point of Healing required].";
            nature5a.ClarifyingText = "Ritual: 10-minute cast, target adjacent space [7 Fatigue for as long as the avatar exists and for 8 hours after]";
            nature5a.StaminaCost = null;
            nature5a.UpkeepCost = null;
            nature5a.FatigueCost = 7;
            nature5a.Tier = 5;
            nature5a.TierBenefitDescription = "Gain +10 to Long-Term Recovery when in a natural setting";
            nature5a.Tree = TalentTree.Nature;
            nature5a.TreeName = "Nature";
            nature5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature5a);

            Talent nature5b = new Talent();
            nature5b.Name = "Alpha Hunter";
            nature5b.Type = TalentType.Benefit;
            nature5b.Action = ActionType.None;
            nature5b.DescriptionFluff = "";
            nature5b.Description = "The size and level of your Companion a ected by Hunting Partner increase by 1. You may select an additional ability from the following list: Feral(2), Speedy(2), Quick(4), Powerful(2 / any 1 attack), Unerring(1 / any 1 attack), Mount, or Tough (2, 4 if selected for a Beast Companion.) This Talent can be selected multiple times; each time it must be applied to a di erent Animal Companion that already has the benefit of Hunting Partner.";
            nature5b.ClarifyingText = "";
            nature5b.StaminaCost = null;
            nature5b.UpkeepCost = null;
            nature5b.Tier = 5;
            nature5b.TierBenefitDescription = "Gain +10 to Long-Term Recovery when in a natural setting";
            nature5b.Tree = TalentTree.Nature;
            nature5b.TreeName = "Nature";
            nature5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature5b);

            Talent nature5c = new Talent();
            nature5c.Name = "One With Nature";
            nature5c.Type = TalentType.Enhancement;
            nature5c.Action = ActionType.Quick;
            nature5c.DescriptionFluff = "";
            nature5c.Description = "While in a natural setting you become Invisible(total cover) and you can pick any point within 30’ of you to become the point of origin for your Nature spells.";
            nature5c.ClarifyingText = "Enhancement: target self [12/3 Stamina]";
            nature5c.StaminaCost = 12;
            nature5c.UpkeepCost = 3;
            nature5c.Tier = 5;
            nature5c.TierBenefitDescription = "Gain +10 to Long-Term Recovery when in a natural setting";
            nature5c.Tree = TalentTree.Nature;
            nature5c.TreeName = "Nature";
            nature5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(nature5c);
            #endregion
            #endregion
            #region Necromancy (Invocation)
            #region T1
            Talent necromancy1a = new Talent();
            necromancy1a.Name = "Rot Flesh";
            necromancy1a.Type = TalentType.Maneuver;
            necromancy1a.Action = ActionType.Combat;
            necromancy1a.DescriptionFluff = "";
            necromancy1a.Description = "Spell {Melee +0/+2 Necrotic} attack against a living target. [4 Stamina]";
            necromancy1a.ClarifyingText = "";
            necromancy1a.StaminaCost = 4;
            necromancy1a.UpkeepCost = null;
            necromancy1a.Tier = 1;
            necromancy1a.TierBenefitDescription = "Gain +1 Body Defense";
            necromancy1a.Tree = TalentTree.Necromancy;
            necromancy1a.TreeName = "Necromancy";
            necromancy1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy1a);

            Talent necromancy1b = new Talent();
            necromancy1b.Name = "Haunt";
            necromancy1b.Type = TalentType.Maneuver;
            necromancy1b.Action = ActionType.Combat;
            necromancy1b.DescriptionFluff = "";
            necromancy1b.Description = "Spell {Ranged (20’) +0/+0 Psychic} [4 Stamina]";
            necromancy1b.ClarifyingText = "If the target takes damage, it becomes Vulnerable (until Resisted).";
            necromancy1b.StaminaCost = 4;
            necromancy1b.UpkeepCost = null;
            necromancy1b.Tier = 1;
            necromancy1b.TierBenefitDescription = "Gain +1 Body Defense";
            necromancy1b.Tree = TalentTree.Necromancy;
            necromancy1b.TreeName = "Necromancy";
            necromancy1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy1b);

            Talent necromancy1c = new Talent();
            necromancy1c.Name = "Dark Veil";
            necromancy1c.Type = TalentType.Enhancement;
            necromancy1c.Action = ActionType.Quick;
            necromancy1c.DescriptionFluff = "";
            necromancy1c.Description = "Gain Light Concealment while within poorly lit environments and enemies adjacent to you have their Resolve and Body Defenses reduced by 1.";
            necromancy1c.ClarifyingText = "Enhancement: target self [4/1 Stamina]";
            necromancy1c.StaminaCost = 4;
            necromancy1c.UpkeepCost = 1;
            necromancy1c.Tier = 1;
            necromancy1c.TierBenefitDescription = "Gain +1 Body Defense";
            necromancy1c.Tree = TalentTree.Necromancy;
            necromancy1c.TreeName = "Necromancy";
            necromancy1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy1c);
            #endregion
            #region T2
            Talent necromancy2a = new Talent();
            necromancy2a.Name = "Fear";
            necromancy2a.Type = TalentType.Maneuver;
            necromancy2a.Action = ActionType.Combat;
            necromancy2a.DescriptionFluff = "";
            necromancy2a.Description = "Spell {Area (30’radius) +0/+2 Psychic} [6 Stamina]";
            necromancy2a.ClarifyingText = "Targets damaged cannot move closer to you. If they are not at least 10’ away from you they must use their next available action to move at least that distance from you.";
            necromancy2a.StaminaCost = 6;
            necromancy2a.UpkeepCost = null;
            necromancy2a.Tier = 2;
            necromancy2a.TierBenefitDescription = "Gain +1 to Intimidate and Deception";
            necromancy2a.Tree = TalentTree.Necromancy;
            necromancy2a.TreeName = "Necromancy";
            necromancy2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy2a);

            Talent necromancy2b = new Talent();
            necromancy2b.Name = "Seance";
            necromancy2b.Type = TalentType.Ritual;
            necromancy2b.Action = ActionType.None;
            necromancy2b.DescriptionFluff = "";
            necromancy2b.Description = "Drawing upon dark energy, you are able to commune with the corpse. You need not share a common language.Using your Invocation Skill in place of your social Skill, during the Ritual you gain a number of “charges” equal to your Invocation Skill.Charges can be used to ask a question or add your Presence to your Invocation Skill when asking a question (thus using 2 charges). You can use a charge to a question again, re - rolling the Skill Check.";
            necromancy2b.ClarifyingText = "Ritual: 10-minute cast, target adjacent corpse [3 Fatigue]";
            necromancy2b.StaminaCost = null;
            necromancy2b.UpkeepCost = null;
            necromancy2b.FatigueCost = 3;
            necromancy2b.Tier = 2;
            necromancy2b.TierBenefitDescription = "Gain +1 to Intimidate and Deception";
            necromancy2b.Tree = TalentTree.Necromancy;
            necromancy2b.TreeName = "Necromancy";
            necromancy2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy2b);

            Talent necromancy2c = new Talent();
            necromancy2c.Name = "Wither";
            necromancy2c.Type = TalentType.Maneuver;
            necromancy2c.Action = ActionType.Combat;
            necromancy2c.DescriptionFluff = "";
            necromancy2c.Description = "Spell {Area (20’ radius) +0/+2 Necrotic} [6 Stamina]";
            necromancy2c.ClarifyingText = "This spell only targets enemies within the area.";
            necromancy2c.StaminaCost = 6;
            necromancy2c.UpkeepCost = null;
            necromancy2c.Tier = 2;
            necromancy2c.TierBenefitDescription = "Gain +1 to Intimidate and Deception";
            necromancy2c.Tree = TalentTree.Necromancy;
            necromancy2c.TreeName = "Necromancy";
            necromancy2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy2c);
            #endregion
            #region T3
            Talent necromancy3a = new Talent();
            necromancy3a.Name = "Power of Blood";
            necromancy3a.Type = TalentType.Trick;
            necromancy3a.Action = ActionType.Quick;
            necromancy3a.DescriptionFluff = "";
            necromancy3a.Description = "You cause yourself to lose HP up to your Willpower in HP. Each point of HP lost in the 2nd Track gains 2 Stamina, each point lost in the 3rd Track gains 3 Stamina.";
            necromancy3a.ClarifyingText = "";
            necromancy3a.StaminaCost = null;
            necromancy3a.UpkeepCost = null;
            necromancy3a.Tier = 3;
            necromancy3a.TierBenefitDescription = "Gain +1 to MCR to resist your Necromancy Spells";
            necromancy3a.Tree = TalentTree.Necromancy;
            necromancy3a.TreeName = "Necromancy";
            necromancy3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy3a);

            Talent necromancy3b = new Talent();
            necromancy3b.Name = "Crippling Grasp";
            necromancy3b.Type = TalentType.Maneuver;
            necromancy3b.Action = ActionType.Combat;
            necromancy3b.DescriptionFluff = "";
            necromancy3b.Description = "Spell {Ranged (Pistol) +0/-2 Resolve} [8 Stamina]";
            necromancy3b.ClarifyingText = "On a hit, target loses an amount of Stamina equal to the amount of damage the attack would have caused and becomes Exhausted. Targets that do not have a Stamina pool or are out of Stamina cannot use any special abilities on the following round.";
            necromancy3b.StaminaCost = 8;
            necromancy3b.UpkeepCost = null;
            necromancy3b.Tier = 3;
            necromancy3b.TierBenefitDescription = "Gain +1 to MCR to resist your Necromancy Spells";
            necromancy3b.Tree = TalentTree.Necromancy;
            necromancy3b.TreeName = "Necromancy";
            necromancy3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy3b);

            Talent necromancy3c = new Talent();
            necromancy3c.Name = "Create Thrall";
            necromancy3c.Type = TalentType.Maneuver;
            necromancy3c.Action = ActionType.Combat;
            necromancy3c.DescriptionFluff = "";
            necromancy3c.Description = "Use a corpse to create either a skeleton or zombie depending on the nature of the corpse used. The Thrall is a Companion of the same level of the corpse used - 1(max of your level - 1) and gains the  Undead Quality.If the corpse has Animalistic, Multi - Legged, Flight, Natural weapons or Natural Armor the Thrall also gains those Qualities.Skeletons are Solid and gain Speedy (2), Evasive (Physical / 2).Zombies are Flesh and gain Natural Weapons(light)(if the corpse did not already have a heavier Natural Weapon) and Feral (1) This Ritual costs UEU equal to the size of the Thrall x100.";
            necromancy3c.ClarifyingText = "Ritual: 10-minute cast, target adjacent corpse [(Size of Thrall) Fatigue for as long as the Thrall exists and for 8 hours after]";
            necromancy3c.StaminaCost = null;
            necromancy3c.UpkeepCost = null;
            necromancy3c.FatigueCost = 3;
            necromancy3c.Tier = 3;
            necromancy3c.TierBenefitDescription = "Gain +1 to MCR to resist your Necromancy Spells";
            necromancy3c.Tree = TalentTree.Necromancy;
            necromancy3c.TreeName = "Necromancy";
            necromancy3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy3c);
            #endregion
            #region T4
            Talent necromancy4a = new Talent();
            necromancy4a.Name = "Puppet Master";
            necromancy4a.Type = TalentType.Benefit;
            necromancy4a.Action = ActionType.None;
            necromancy4a.DescriptionFluff = "";
            necromancy4a.Description = "Reduce the Fatigue cost of ‘create thrall’ and ‘create Undead champion’ Rituals by 1 (minimum 1). You can treat your Undead companions within 50’ of you as the point of origin for any necromancy spells you cast, for the purposes of LOS, range, Concealment and Cover.Spells cast through your companion cost 2 extra Stamina.";
            necromancy4a.ClarifyingText = "";
            necromancy4a.StaminaCost = null;
            necromancy4a.UpkeepCost = null;
            necromancy4a.Tier = 4;
            necromancy4a.TierBenefitDescription = "Gain +1 CM with Necromancy Spells";
            necromancy4a.Tree = TalentTree.Necromancy;
            necromancy4a.TreeName = "Necromancy";
            necromancy4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy4a);

            Talent necromancy4b = new Talent();
            necromancy4b.Name = "Gory Sacrifice";
            necromancy4b.Type = TalentType.TriggeredAction;
            necromancy4b.Action = ActionType.Reaction;
            necromancy4b.DescriptionFluff = "";
            necromancy4b.Description = "Spell {Area (10’ radius) +0/+0 Necrotic} [5 Stamina]";
            necromancy4b.ClarifyingText = "Triggering Action: you kill a grunt with a Necromancy spell";
            necromancy4b.StaminaCost = 5;
            necromancy4b.UpkeepCost = null;
            necromancy4b.Tier = 4;
            necromancy4b.TierBenefitDescription = "Gain +1 CM with Necromancy Spells";
            necromancy4b.Tree = TalentTree.Necromancy;
            necromancy4b.TreeName = "Necromancy";
            necromancy4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy4b);

            Talent necromancy4c = new Talent();
            necromancy4c.Name = "Desecrate";
            necromancy4c.Type = TalentType.Ritual;
            necromancy4c.Action = ActionType.None;
            necromancy4c.DescriptionFluff = "";
            necromancy4c.Description = "For 24 hours, the space is desecrated. While in a desecrated space, Unholy and Undead creatures become Focused and Empowered; Holy creatures are gain Weakened.While in a desecrated space, Undead creatures Heal 1 HP per Track per hour.";
            necromancy4c.ClarifyingText = "Ritual: 1 hour cast, target a space with a 50’radius [5 Fatigue]";
            necromancy4c.StaminaCost = null;
            necromancy4c.UpkeepCost = null;
            necromancy4c.FatigueCost = 5;
            necromancy4c.Tier = 4;
            necromancy4c.TierBenefitDescription = "Gain +1 CM with Necromancy Spells";
            necromancy4c.Tree = TalentTree.Necromancy;
            necromancy4c.TreeName = "Necromancy";
            necromancy4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy4c);
            #endregion
            #region T5
            Talent necromancy5a = new Talent();
            necromancy5a.Name = "Life Siphon";
            necromancy5a.Type = TalentType.Maneuver;
            necromancy5a.Action = ActionType.Combat;
            necromancy5a.DescriptionFluff = "";
            necromancy5a.Description = "Spell {Melee +0/+8 Necrotic} [12 Stamina plus 1 Fatigue for every 3 HP healed in the 2nd Track]";
            necromancy5a.ClarifyingText = "You gain ¼ the damage caused as Healing. These points can be spent point for point to Heal damage in the 1st or 2nd Track.";
            necromancy5a.StaminaCost = 12;
            necromancy5a.UpkeepCost = null;
            necromancy5a.Tier = 5;
            necromancy5a.TierBenefitDescription = "Gain -1 Fatigue of Necromancy Rituals and +1 Resolve Defense. You count as an Undead creature and target.";
            necromancy5a.Tree = TalentTree.Necromancy;
            necromancy5a.TreeName = "Necromancy";
            necromancy5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy5a);

            Talent necromancy5b = new Talent();
            necromancy5b.Name = "Create Undead Champion";
            necromancy5b.Type = TalentType.Ritual;
            necromancy5b.Action = ActionType.None;
            necromancy5b.DescriptionFluff = "";
            necromancy5b.Description = "Turn a thrall into an Undead Champion. The Champions level increases to your level and it gains Natural Armor (any), Natural Weapons(any), Hardened(1), Tough(2), Evasive(Resolve / 2), Quick (2), and Resistant(2). This Ritual costs UEU equal to the size of the Undead Champion x 250.";
            necromancy5b.ClarifyingText = "Ritual: 1-hour cast, target adjacent Thrall [Double the Thrall Fatigue]";
            necromancy5b.StaminaCost = null;
            necromancy5b.UpkeepCost = null;
            necromancy5b.Tier = 5;
            necromancy5b.TierBenefitDescription = "Gain -1 Fatigue of Necromancy Rituals and +1 Resolve Defense. You count as an Undead creature and target.";
            necromancy5b.Tree = TalentTree.Necromancy;
            necromancy5b.TreeName = "Necromancy";
            necromancy5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy5b);

            Talent necromancy5c = new Talent();
            necromancy5c.Name = "Blood Rend";
            necromancy5c.Type = TalentType.Maneuver;
            necromancy5c.Action = ActionType.Combat;
            necromancy5c.DescriptionFluff = "";
            necromancy5c.Description = "Spell {Ranged (Pistol) +0/+6 Necrotic} [12/5 Stamina]";
            necromancy5c.ClarifyingText = "This attack ignores Armor. If a Natural target is incapacitated by the attack, the target is instantly killed and a Blood Thrall is spawned adjacent to the body. The Blood Thrall is a Liquid Companion of the same Size and Level as the target. The Blood Thrall gains Slight, { Ranged(Pistol) / Necrotic}             attack, and Energy Infused(Necrotic). The Blood Thrall is under your control and can be controlled as per the usual rules for controlling Companions.";
            necromancy5c.StaminaCost = 12;
            necromancy5c.UpkeepCost = 5;
            necromancy5c.Tier = 5;
            necromancy5c.TierBenefitDescription = "Gain -1 Fatigue of Necromancy Rituals and +1 Resolve Defense. You count as an Undead creature and target.";
            necromancy5c.Tree = TalentTree.Necromancy;
            necromancy5c.TreeName = "Necromancy";
            necromancy5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(necromancy5c);
            #endregion
            #endregion
            #region Prayer (Invocation)
            #region T1
            Talent prayer1a = new Talent();
            prayer1a.Name = "Speed Recovery";
            prayer1a.Type = TalentType.Ritual;
            prayer1a.Action = ActionType.None;
            prayer1a.DescriptionFluff = "";
            prayer1a.Description = "You make a Conjuration Check. All allies within a 20-foot radius of you gain a bonus equal to ¼ of the result to their next Long-Term Recovery Check.";
            prayer1a.ClarifyingText = "Ritual: 1 hour cast, target self [2 Fatigue]";
            prayer1a.StaminaCost = null;
            prayer1a.UpkeepCost = null;
            prayer1a.FatigueCost = 2;
            prayer1a.Tier = 1;
            prayer1a.TierBenefitDescription = "Gain +1 to all Knowledge checks";
            prayer1a.Tree = TalentTree.Prayer;
            prayer1a.TreeName = "Prayer";
            prayer1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer1a);

            Talent prayer1b = new Talent();
            prayer1b.Name = "Holy Lash";
            prayer1b.Type = TalentType.Maneuver;
            prayer1b.Action = ActionType.Combat;
            prayer1b.DescriptionFluff = "";
            prayer1b.Description = "Spell{Ranged (Pistol) +0/+2 Holy} [4 Stamina]";
            prayer1b.ClarifyingText = "";
            prayer1b.StaminaCost = 4;
            prayer1b.UpkeepCost = null;
            prayer1b.Tier = 1;
            prayer1b.TierBenefitDescription = "Gain +1 to all Knowledge checks";
            prayer1b.Tree = TalentTree.Prayer;
            prayer1b.TreeName = "Prayer";
            prayer1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer1b);

            Talent prayer1c = new Talent();
            prayer1c.Name = "Ward";
            prayer1c.Type = TalentType.Maneuver;
            prayer1c.Action = ActionType.Combat;
            prayer1c.DescriptionFluff = "";
            prayer1c.Description = "All allies within 10’ of you gains Light Fortification against all damage. This Enhancement does not a ect you.";
            prayer1c.ClarifyingText = "Enhancement: target self [4/1 Stamina]";
            prayer1c.StaminaCost = 4;
            prayer1c.UpkeepCost = 1;
            prayer1c.Tier = 1;
            prayer1c.TierBenefitDescription = "Gain +1 to all Knowledge checks";
            prayer1c.Tree = TalentTree.Prayer;
            prayer1c.TreeName = "Prayer";
            prayer1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer1c);
            #endregion
            #region T2
            Talent prayer2a = new Talent();
            prayer2a.Name = "Mend Wound";
            prayer2a.Type = TalentType.Ritual;
            prayer2a.Action = ActionType.Combat;
            prayer2a.DescriptionFluff = "";
            prayer2a.Description = "You can perform any use of the Healing skill that normally takes 1-minute or less as part of this Ritual. When doing so you are always treated as though you are using a Med Unit.";
            prayer2a.ClarifyingText = "Ritual: Combat Action cast [2 Fatigue]";
            prayer2a.StaminaCost = null;
            prayer2a.UpkeepCost = null;
            prayer2a.FatigueCost = 2;
            prayer2a.Tier = 2;
            prayer2a.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            prayer2a.Tree = TalentTree.Prayer;
            prayer2a.TreeName = "Prayer";
            prayer2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer2a);

            Talent prayer2b = new Talent();
            prayer2b.Name = "Insight";
            prayer2b.Type = TalentType.Enhancement;
            prayer2b.Action = ActionType.Quick;
            prayer2b.DescriptionFluff = "";
            prayer2b.Description = "All allies within 20’ of you gain a +1 to attacks and a +2 to damage rolls. This Enhancement does not a ect you.";
            prayer2b.ClarifyingText = "";
            prayer2b.StaminaCost = 6;
            prayer2b.UpkeepCost = 2;
            prayer2b.Tier = 2;
            prayer2b.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            prayer2b.Tree = TalentTree.Prayer;
            prayer2b.TreeName = "Prayer";
            prayer2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer2b);

            Talent prayer2c = new Talent();
            prayer2c.Name = "Spiritual Guide";
            prayer2c.Type = TalentType.Ritual;
            prayer2c.Action = ActionType.None;
            prayer2c.DescriptionFluff = "";
            prayer2c.Description = "The target gains a +1 to Initiative, Perception, and all Defenses for 24 hours. This e ect ends if the target enters the Bleed Out Track.";
            prayer2c.ClarifyingText = "Ritual: 10-minute cast; target adjacent creature [3 Fatigue]";
            prayer2c.StaminaCost = null;
            prayer2c.UpkeepCost = null;
            prayer2c.FatigueCost = 3;
            prayer2c.Tier = 2;
            prayer2c.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            prayer2c.Tree = TalentTree.Prayer;
            prayer2c.TreeName = "Prayer";
            prayer2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer2c);
            #endregion
            #region T3
            Talent prayer3a = new Talent();
            prayer3a.Name = "Shield of Faith";
            prayer3a.Type = TalentType.Enhancement;
            prayer3a.Action = ActionType.Quick;
            prayer3a.DescriptionFluff = "";
            prayer3a.Description = "The target increases its current and max HP in its 1st Damage Track by your Presence Attribute.";
            prayer3a.ClarifyingText = "Enhancement, target ally within 30 feet [8/2 Stamina]";
            prayer3a.StaminaCost = 8;
            prayer3a.UpkeepCost = 2;
            prayer3a.Tier = 3;
            prayer3a.TierBenefitDescription = "All Healing effects from Prayer spells add 2 to the amount healed in the 1st or 2nd Tracks";
            prayer3a.Tree = TalentTree.Prayer;
            prayer3a.TreeName = "Prayer";
            prayer3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer3a);

            Talent prayer3b = new Talent();
            prayer3b.Name = "Sanctify";
            prayer3b.Type = TalentType.Ritual;
            prayer3b.Action = ActionType.None;
            prayer3b.DescriptionFluff = "";
            prayer3b.Description = "Make an Invocation Check. Targets gain a bonus equal to ¼ of the result to their next Long-Term Recovery, and for 24 hours the 20’ radius area is Sanctified.While in a Sanctified space, Holy creatures become Focused and Empowered, and Unholy creatures are Weakened.";
            prayer3b.ClarifyingText = "Ritual: 1 hour cast, targets allies within a 20-foot radius [4 Fatigue]";
            prayer3b.StaminaCost = null;
            prayer3b.UpkeepCost = null;
            prayer3b.FatigueCost = 4;
            prayer3b.Tier = 3;
            prayer3b.TierBenefitDescription = "All Healing effects from Prayer spells add 2 to the amount healed in the 1st or 2nd Tracks";
            prayer3b.Tree = TalentTree.Prayer;
            prayer3b.TreeName = "Prayer";
            prayer3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer3b);

            Talent prayer3c = new Talent();
            prayer3c.Name = "Enliven";
            prayer3c.Type = TalentType.TriggeredAction;
            prayer3c.Action = ActionType.Reaction;
            prayer3c.DescriptionFluff = "";
            prayer3c.Description = "You suppress the condition and the creature will automatically pass its next Resistance Check against the Triggering condition.";
            prayer3c.ClarifyingText = "Triggering Action: you or an ally within 30’ gains a negative condition [4 Stamina]";
            prayer3c.StaminaCost = 4;
            prayer3c.UpkeepCost = null;
            prayer3c.Tier = 3;
            prayer3c.TierBenefitDescription = "All Healing effects from Prayer spells add 2 to the amount healed in the 1st or 2nd Tracks";
            prayer3c.Tree = TalentTree.Prayer;
            prayer3c.TreeName = "Prayer";
            prayer3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer3c);
            #endregion
            #region T4
            Talent prayer4a = new Talent();
            prayer4a.Name = "Cure Wound";
            prayer4a.Type = TalentType.Ritual;
            prayer4a.Action = ActionType.Combat;
            prayer4a.DescriptionFluff = "";
            prayer4a.Description = "Target creature regains your Willpower + Presence in HP. HP regained in the 3rd Track costs double. Remove the Poisoned state (but not damage already caused by it). This spell will automatically stabilize targets in the Bleed Out Track and Healing spent in that track heals normally.";
            prayer4a.ClarifyingText = "Ritual: Combat Action cast. Target adjacent creature [5 Fatigue]";
            prayer4a.StaminaCost = null;
            prayer4a.UpkeepCost = null;
            prayer4a.FatigueCost = 5;
            prayer4a.Tier = 4;
            prayer4a.TierBenefitDescription = "Gain +1 Resolve Defense";
            prayer4a.Tree = TalentTree.Prayer;
            prayer4a.TreeName = "Prayer";
            prayer4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer4a);

            Talent prayer4b = new Talent();
            prayer4b.Name = "Divine Favor";
            prayer4b.Type = TalentType.Enhancement;
            prayer4b.Action = ActionType.Quick;
            prayer4b.DescriptionFluff = "";
            prayer4b.Description = "While within 30’ of you all allies can re-roll Skill Checks, attacks and damage rolls. The new result must be taken, and the entire roll must be re - rolled(all dice).Re - rolls from this spell must be done before any other re-rolls the character can take (i.e.Vicious or Spirit). This Enhancement does not a ect you.";
            prayer4b.ClarifyingText = "Enhancement: target self [10/3 Stamina]";
            prayer4b.StaminaCost = 10;
            prayer4b.UpkeepCost = 3;
            prayer4b.Tier = 4;
            prayer4b.TierBenefitDescription = "Gain +1 Resolve Defense";
            prayer4b.Tree = TalentTree.Prayer;
            prayer4b.TreeName = "Prayer";
            prayer4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer4b);

            Talent prayer4c = new Talent();
            prayer4c.Name = "Martyr";
            prayer4c.Type = TalentType.TriggeredAction;
            prayer4c.Action = ActionType.Reaction;
            prayer4c.DescriptionFluff = "";
            prayer4c.Description = "You are treated as the target of the attack. You are automatically hit (su ering the same stage of Crit as the Triggering attack). The damage of the attack is rolled again.";
            prayer4c.ClarifyingText = "Triggering Action: an ally within 30’ takes damage from an attack that would cause them to become unconscious [5 Stamina]";
            prayer4c.StaminaCost = 5;
            prayer4c.UpkeepCost = null;
            prayer4c.Tier = 4;
            prayer4c.TierBenefitDescription = "Gain +1 Resolve Defense";
            prayer4c.Tree = TalentTree.Prayer;
            prayer4c.TreeName = "Prayer";
            prayer4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer4c);
            #endregion
            #region T5
            Talent prayer5a = new Talent();
            prayer5a.Name = "Divine Intervention";
            prayer5a.Type = TalentType.Ritual;
            prayer5a.Action = ActionType.None;
            prayer5a.DescriptionFluff = "";
            prayer5a.Description = "The target regains all HP in the Bleed Out Track plus 1 HP in its 3rd Track. The target is Weakened until fully Healed and for 8 hours after. The target su ers [20 Fatigue]. This Ritual cost UEU equal to (the level of the target squared) x 1, 000 to cast.";
            prayer5a.ClarifyingText = "Ritual: 1 hour cast, target adjacent creature that has died within the last 24 hours and whose body is generally intact [6 Fatigue]";
            prayer5a.StaminaCost = null;
            prayer5a.UpkeepCost = null;
            prayer5a.FatigueCost = 6;
            prayer5a.Tier = 5;
            prayer5a.TierBenefitDescription = "You and all adjacent allies automatically regain 1 HP in their 1st track at the beginning of their turn";
            prayer5a.Tree = TalentTree.Prayer;
            prayer5a.TreeName = "Prayer";
            prayer5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer5a);

            Talent prayer5b = new Talent();
            prayer5b.Name = "Healing Aura";
            prayer5b.Type = TalentType.Enhancement;
            prayer5b.Action = ActionType.Quick;
            prayer5b.DescriptionFluff = "";
            prayer5b.Description = "While within 30’ of you all allies regain HP equal to 1/2 your presence Attribute in their 1st and 2nd Tracks at the beginning of their turn.";
            prayer5b.ClarifyingText = "Enhancement: target self [12/3 Stamina]";
            prayer5b.StaminaCost = 12;
            prayer5b.UpkeepCost = 3;
            prayer5b.Tier = 5;
            prayer5b.TierBenefitDescription = "You and all adjacent allies automatically regain 1 HP in their 1st track at the beginning of their turn";
            prayer5b.Tree = TalentTree.Prayer;
            prayer5b.TreeName = "Prayer";
            prayer5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer5b);

            Talent prayer5c = new Talent();
            prayer5c.Name = "Foresight";
            prayer5c.Type = TalentType.Benefit;
            prayer5c.Action = ActionType.None;
            prayer5c.DescriptionFluff = "";
            prayer5c.Description = "You can spend one point of Spirit to gain a +5 to any roll or to your Defense against 1 attack. This option can be selected after the roll is made.";
            prayer5c.ClarifyingText = "";
            prayer5c.StaminaCost = null;
            prayer5c.UpkeepCost = null;
            prayer5c.Tier = 5;
            prayer5c.TierBenefitDescription = "You and all adjacent allies automatically regain 1 HP in their 1st track at the beginning of their turn";
            prayer5c.Tree = TalentTree.Prayer;
            prayer5c.TreeName = "Prayer";
            prayer5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(prayer5c);
            #endregion
            #endregion
            #region Seeing (Invocation)
            #region T1
            Talent seeing1a = new Talent();
            seeing1a.Name = "Scan";
            seeing1a.Type = TalentType.Maneuver;
            seeing1a.Action = ActionType.Combat;
            seeing1a.DescriptionFluff = "";
            seeing1a.Description = "Spell {Ranged (Pistol) +0/+0 Psychic} [4 Stamina]";
            seeing1a.ClarifyingText = "If you damage the target with this attack, you glean their current HP total (after the damage of this attack), Durability, and the value of 1 Defense of your choice.";
            seeing1a.StaminaCost = 4;
            seeing1a.UpkeepCost = null;
            seeing1a.Tier = 1;
            seeing1a.TierBenefitDescription = "You can re-roll one Spirit roll per encounter";
            seeing1a.Tree = TalentTree.Seeing;
            seeing1a.TreeName = "Seeing";
            seeing1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing1a);

            Talent seeing1b = new Talent();
            seeing1b.Name = "Precognition";
            seeing1b.Type = TalentType.Ritual;
            seeing1b.Action = ActionType.None;
            seeing1b.DescriptionFluff = "";
            seeing1b.Description = "You gain a +6 bonus on your next attack roll or skill Check.";
            seeing1b.ClarifyingText = "Ritual: Combat Action cast, target self [2 Fatigue]";
            seeing1b.StaminaCost = null;
            seeing1b.UpkeepCost = null;
            seeing1b.FatigueCost = 2;
            seeing1b.Tier = 1;
            seeing1b.TierBenefitDescription = "You can re-roll one Spirit roll per encounter";
            seeing1b.Tree = TalentTree.Seeing;
            seeing1b.TreeName = "Seeing";
            seeing1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing1b);

            Talent seeing1c = new Talent();
            seeing1c.Name = "Read Object";
            seeing1c.Type = TalentType.Ritual;
            seeing1c.Action = ActionType.Combat;
            seeing1c.DescriptionFluff = "";
            seeing1c.Description = "“Read” the history of an item. At the end of the casting time, make an Invocation Check. • MCR 15 – Minor impressions including the general physical appearance of the last people to handle the item and the emotions of the people when they last handled it. • MCR 20 – The above information as well as the name of the person to last handle the item, minor details concerning his or her appearance, and the last thing he or she did with the item. • MCR 25 – The above information as well as the length of time the person owned the item and every significant event that occurred with the item while it was in that person’s possession.";
            seeing1c.ClarifyingText = "Ritual: 10-minute cast, target held or adjacent item [2 Fatigue]";
            seeing1c.StaminaCost = null;
            seeing1c.UpkeepCost = null;
            seeing1c.FatigueCost = 2;
            seeing1c.Tier = 1;
            seeing1c.TierBenefitDescription = "You can re-roll one Spirit roll per encounter";
            seeing1c.Tree = TalentTree.Seeing;
            seeing1c.TreeName = "Seeing";
            seeing1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing1c);
            #endregion
            #region T2
            Talent seeing2a = new Talent();
            seeing2a.Name = "Sight of Unsight";
            seeing2a.Type = TalentType.Maneuver;
            seeing2a.Action = ActionType.Combat;
            seeing2a.DescriptionFluff = "";
            seeing2a.Description = "You gain 1 additional vision mode to your available modes (night vision, thermal, or sonar)";
            seeing2a.ClarifyingText = "Enhancement: target self [6/2 Stamina]";
            seeing2a.StaminaCost = 6;
            seeing2a.UpkeepCost = 2;
            seeing2a.Tier = 2;
            seeing2a.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            seeing2a.Tree = TalentTree.Seeing;
            seeing2a.TreeName = "Seeing";
            seeing2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing2a);

            Talent seeing2b = new Talent();
            seeing2b.Name = "Reading the Signs";
            seeing2b.Type = TalentType.Benefit;
            seeing2b.Action = ActionType.None;
            seeing2b.DescriptionFluff = "";
            seeing2b.Description = "You gain +2 to Connection rolls.";
            seeing2b.ClarifyingText = "";
            seeing2b.StaminaCost = null;
            seeing2b.UpkeepCost = null;
            seeing2b.Tier = 2;
            seeing2b.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            seeing2b.Tree = TalentTree.Seeing;
            seeing2b.TreeName = "Seeing";
            seeing2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing2b);

            Talent seeing2c = new Talent();
            seeing2c.Name = "Lending Your Gift";
            seeing2c.Type = TalentType.Trick;
            seeing2c.Action = ActionType.Quick;
            seeing2c.DescriptionFluff = "";
            seeing2c.Description = "You can temporarily transfer 1 point of Spirit to an ally within 50’ of you; subtract 1 point from your current total and add 1 point to the recipient’s total. You must have an unused point available to invoke this power.At the end of the encounter, the recipient loses any unused Spirit that was lent to them through the use of this power.Spirit lost from this Talent is regained as usual.";
            seeing2c.ClarifyingText = "";
            seeing2c.StaminaCost = 0;
            seeing2c.UpkeepCost = null;
            seeing2c.Tier = 2;
            seeing2c.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            seeing2c.Tree = TalentTree.Seeing;
            seeing2c.TreeName = "Seeing";
            seeing2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing2c);
            #endregion
            #region T3
            Talent seeing3a = new Talent();
            seeing3a.Name = "Future Sight";
            seeing3a.Type = TalentType.TriggeredAction;
            seeing3a.Action = ActionType.Reaction;
            seeing3a.DescriptionFluff = "";
            seeing3a.Description = "You cause the attacker to re-roll the attack and use the new roll. This ability can only be used once on a given attack.";
            seeing3a.ClarifyingText = "Triggering Action: you are hit by a Melee or Ranged attack";
            seeing3a.StaminaCost = 4;
            seeing3a.UpkeepCost = null;
            seeing3a.Tier = 3;
            seeing3a.TierBenefitDescription = "You can re-roll all 1s on Perception checks";
            seeing3a.Tree = TalentTree.Seeing;
            seeing3a.TreeName = "Seeing";
            seeing3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing3a);

            Talent seeing3b = new Talent();
            seeing3b.Name = "Curse of Misfortune";
            seeing3b.Type = TalentType.Maneuver;
            seeing3b.Action = ActionType.Combat;
            seeing3b.DescriptionFluff = "";
            seeing3b.Description = "Spell {Ranged (30’) +0/+4 Psychic} [8 Stamina]";
            seeing3b.ClarifyingText = "On a successful hit, the target gains Weakened (until Resisted).";
            seeing3b.StaminaCost = 8;
            seeing3b.UpkeepCost = null;
            seeing3b.Tier = 3;
            seeing3b.TierBenefitDescription = "You can re-roll all 1s on Perception checks";
            seeing3b.Tree = TalentTree.Seeing;
            seeing3b.TreeName = "Seeing";
            seeing3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing3b);

            Talent seeing3c = new Talent();
            seeing3c.Name = "Augury";
            seeing3c.Type = TalentType.Ritual;
            seeing3c.Action = ActionType.None;
            seeing3c.DescriptionFluff = "";
            seeing3c.Description = "During this Ritual you can ask one yes or no question about what will transpire within the area in the next 24 hours. The answer to the question must not be dependent on actions from the caster or his allies. Acceptable questions are “will the guards be at their posts over night ?”, Will Manfred return to the area within the next 6 hours ?”, “Will it rain in the next 12 hours ?” or any similar question.";
            seeing3c.ClarifyingText = "Ritual: 1 hour cast, target an area with a 50’ radius you have viewed in the last 24 hours [4 Fatigue]";
            seeing3c.StaminaCost = null;
            seeing3c.UpkeepCost = null;
            seeing3c.FatigueCost = 4;
            seeing3c.Tier = 3;
            seeing3c.TierBenefitDescription = "You can re-roll all 1s on Perception checks";
            seeing3c.Tree = TalentTree.Seeing;
            seeing3c.TreeName = "Seeing";
            seeing3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing3c);
            #endregion
            #region T4
            Talent seeing4a = new Talent();
            seeing4a.Name = "True Sight";
            seeing4a.Type = TalentType.Enhancement;
            seeing4a.Action = ActionType.Quick;
            seeing4a.DescriptionFluff = "";
            seeing4a.Description = "You gain a +5 to Perception. Gear, Enhancements, and Stances that provide your enemies bonuses to Disguise or Stealth, or which create Concealment, are ignored.";
            seeing4a.ClarifyingText = "Enhancement: target self [5/2 Stamina]";
            seeing4a.StaminaCost = 5;
            seeing4a.UpkeepCost = 2;
            seeing4a.Tier = 4;
            seeing4a.TierBenefitDescription = "You treat enemy Concealment as one Grade lower (Total as Heavy, Heavy as Light, Light as none)";
            seeing4a.Tree = TalentTree.Seeing;
            seeing4a.TreeName = "Seeing";
            seeing4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing4a);

            Talent seeing4b = new Talent();
            seeing4b.Name = "Luck Has Nothing To Do With It";
            seeing4b.Type = TalentType.Benefit;
            seeing4b.Action = ActionType.None;
            seeing4b.DescriptionFluff = "";
            seeing4b.Description = "When making Spirit re-rolls, you can re-roll 1s and 2s.";
            seeing4b.ClarifyingText = "";
            seeing4b.StaminaCost = null;
            seeing4b.UpkeepCost = null;
            seeing4b.Tier = 4;
            seeing4b.TierBenefitDescription = "You treat enemy Concealment as one Grade lower (Total as Heavy, Heavy as Light, Light as none)";
            seeing4b.Tree = TalentTree.Seeing;
            seeing4b.TreeName = "Seeing";
            seeing4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing4b);

            Talent seeing4c = new Talent();
            seeing4c.Name = "Rewriting the Future";
            seeing4c.Type = TalentType.TriggeredAction;
            seeing4c.Action = ActionType.Reaction;
            seeing4c.DescriptionFluff = "";
            seeing4c.Description = "You can spend 1 Spirit and cause the opponent to lose the point of Spirit he or she was attempting to use. The target is forced to use the result of the original roll (or spend another point of Spirit).";
            seeing4c.ClarifyingText = "Triggered Action: an opponent within line of sight makes a Spirit re-roll [5 Stamina]";
            seeing4c.StaminaCost = null;
            seeing4c.UpkeepCost = null;
            seeing4c.Tier = 4;
            seeing4c.TierBenefitDescription = "You treat enemy Concealment as one Grade lower (Total as Heavy, Heavy as Light, Light as none)";
            seeing4c.Tree = TalentTree.Seeing;
            seeing4c.TreeName = "Seeing";
            seeing4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing4c);
            #endregion
            #region T5
            Talent seeing5a = new Talent();
            seeing5a.Name = "Scry";
            seeing5a.Type = TalentType.Ritual;
            seeing5a.Action = ActionType.None;
            seeing5a.DescriptionFluff = "";
            seeing5a.Description = "You create a remote viewing device. The device is a small magical disturbance through which you can see and hear the world. The device is limited to your available vision modes.A MCR 25 Perception Check is required to notice the presence of the device. The device can fly at a Speed up to 50 MPH.You can double the Speed of the device for double the Fatigue cost for that hour. The device can pass through walls but not through wards and other barriers that cause damage or prevent teleportation.You can deliver audio messages through the device.";
            seeing5a.ClarifyingText = "Ritual, 1-minute cast, target adjacent space [2 + 1 Fatigue per hour used]";
            seeing5a.StaminaCost = null;
            seeing5a.UpkeepCost = null;
            seeing5a.FatigueCost = 2;
            seeing5a.Tier = 5;
            seeing5a.TierBenefitDescription = "Gain +1 to all Defenses";
            seeing5a.Tree = TalentTree.Seeing;
            seeing5a.TreeName = "Seeing";
            seeing5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing5a);

            Talent seeing5b = new Talent();
            seeing5b.Name = "Foresight";
            seeing5b.Type = TalentType.Benefit;
            seeing5b.Action = ActionType.None;
            seeing5b.DescriptionFluff = "";
            seeing5b.Description = "You can spend one point of Spirit to gain a +5 to any roll or to your Defense against 1 attack. This option can be selected after the roll is made.";
            seeing5b.ClarifyingText = "";
            seeing5b.StaminaCost = null;
            seeing5b.UpkeepCost = null;
            seeing5b.Tier = 5;
            seeing5b.TierBenefitDescription = "Gain +1 to all Defenses";
            seeing5b.Tree = TalentTree.Seeing;
            seeing5b.TreeName = "Seeing";
            seeing5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing5b);

            Talent seeing5c = new Talent();
            seeing5c.Name = "From Beyond the Veil";
            seeing5c.Type = TalentType.Benefit;
            seeing5c.Action = ActionType.None;
            seeing5c.DescriptionFluff = "";
            seeing5c.Description = "Increase your Spirit Attribute by 2.";
            seeing5c.ClarifyingText = "";
            seeing5c.StaminaCost = null;
            seeing5c.UpkeepCost = null;
            seeing5c.Tier = 5;
            seeing5c.TierBenefitDescription = "Gain +1 to all Defenses";
            seeing5c.Tree = TalentTree.Seeing;
            seeing5c.TreeName = "Seeing";
            seeing5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(seeing5c);
            #endregion
            #endregion
            #region Smiting (Conjuration)
            #region T1
            Talent smiting1a = new Talent();
            smiting1a.Name = "Smite";
            smiting1a.Type = TalentType.AttackAugment;
            smiting1a.Action = ActionType.Quick;
            smiting1a.DescriptionFluff = "";
            smiting1a.Description = "Add your Presence Attribute in Holy damage to your next Melee attack. [3 Stamina]";
            smiting1a.ClarifyingText = "";
            smiting1a.StaminaCost = 3;
            smiting1a.UpkeepCost = null;
            smiting1a.Tier = 1;
            smiting1a.TierBenefitDescription = "Gain +1 to Resistance and Durability vs. Necrotic and Unholy attacks";
            smiting1a.Tree = TalentTree.Smiting;
            smiting1a.TreeName = "Smiting";
            smiting1a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting1a);

            Talent smiting1b = new Talent();
            smiting1b.Name = "Blessed Weapon";
            smiting1b.Type = TalentType.Ritual;
            smiting1b.Action = ActionType.None;
            smiting1b.DescriptionFluff = "";
            smiting1b.Description = "Target gains the Holy damage type as well as +1 CM and Vicious vs. Unholy and Undead targets.";
            smiting1b.ClarifyingText = "Ritual: 1 hour cast, target touched weapon [2 Fatigue]";
            smiting1b.StaminaCost = null;
            smiting1b.UpkeepCost = null;
            smiting1b.FatigueCost = 2;
            smiting1b.Tier = 1;
            smiting1b.TierBenefitDescription = "Gain +1 to Resistance and Durability vs. Necrotic and Unholy attacks";
            smiting1b.Tree = TalentTree.Smiting;
            smiting1b.TreeName = "Smiting";
            smiting1b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting1b);

            Talent smiting1c = new Talent();
            smiting1c.Name = "Retribution";
            smiting1c.Type = TalentType.TriggeredAction;
            smiting1c.Action = ActionType.Reaction;
            smiting1c.DescriptionFluff = "";
            smiting1c.Description = "You become Enraged until the end of your next turn. [2 Stamina]";
            smiting1c.ClarifyingText = "Triggering Action: An ally within 20’ of you takes damage from an enemy attack.  Enraged = +2 melee damage, +2 durability, +2 Str non-combat skill checks.";
            smiting1c.StaminaCost = 2;
            smiting1c.UpkeepCost = null;
            smiting1c.Tier = 1;
            smiting1c.TierBenefitDescription = "Gain +1 to Resistance and Durability vs. Necrotic and Unholy attacks";
            smiting1c.Tree = TalentTree.Smiting;
            smiting1c.TreeName = "Smiting";
            smiting1c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting1c);
            #endregion
            #region T2
            Talent smiting2a = new Talent();
            smiting2a.Name = "Fervor";
            smiting2a.Type = TalentType.Stance;
            smiting2a.Action = ActionType.Quick;
            smiting2a.DescriptionFluff = "";
            smiting2a.Description = "You gain the Focused condition.";
            smiting2a.ClarifyingText = "Focused = +1 to Melee weapon attaks, +1 to all defenses, +2 to Focus linked non-combat skill checks.";
            smiting2a.StaminaCost = 6;
            smiting2a.UpkeepCost = 2;
            smiting2a.Tier = 2;
            smiting2a.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            smiting2a.Tree = TalentTree.Smiting;
            smiting2a.TreeName = "Smiting";
            smiting2a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting2a);

            Talent smiting2b = new Talent();
            smiting2b.Name = "Zeal";
            smiting2b.Type = TalentType.Enhancement;
            smiting2b.Action = ActionType.Quick;
            smiting2b.DescriptionFluff = "";
            smiting2b.Description = "You gain Girded vs. attacks from Unholy or Undead creatures.";
            smiting2b.ClarifyingText = "Girded = +2 to Durability and +1 to all Defenses";
            smiting2b.StaminaCost = 6;
            smiting2b.UpkeepCost = 2;
            smiting2b.Tier = 2;
            smiting2b.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            smiting2b.Tree = TalentTree.Smiting;
            smiting2b.TreeName = "Smiting";
            smiting2b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting2b);

            Talent smiting2c = new Talent();
            smiting2c.Name = "Purge";
            smiting2c.Type = TalentType.Trick;
            smiting2c.Action = ActionType.Quick;
            smiting2c.DescriptionFluff = "";
            smiting2c.Description = "Remove one non-persistent condition or the Poisoned state from yourself or an adjacent ally.";
            smiting2c.ClarifyingText = "";
            smiting2c.StaminaCost = 6;
            smiting2c.UpkeepCost = null;
            smiting2c.Tier = 2;
            smiting2c.TierBenefitDescription = "Gain +2 to Long-Term Recovery";
            smiting2c.Tree = TalentTree.Smiting;
            smiting2c.TreeName = "Smiting";
            smiting2c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting2c);
            #endregion
            #region T3
            Talent smiting3a = new Talent();
            smiting3a.Name = "Holy Strike";
            smiting3a.Type = TalentType.AttackAugment;
            smiting3a.Action = ActionType.Quick;
            smiting3a.DescriptionFluff = "";
            smiting3a.Description = "Your next Melee or Holy attack gains your Presence in Holy damage and can re-roll all 1s on the damage roll.";
            smiting3a.ClarifyingText = "Vicious weapons use that property to re-roll 2s for this attack.";
            smiting3a.StaminaCost = 6;
            smiting3a.UpkeepCost = null;
            smiting3a.Tier = 3;
            smiting3a.TierBenefitDescription = "Gain +2 to Holy damage. You are considered a Holy target when taking damage.";
            smiting3a.Tree = TalentTree.Smiting;
            smiting3a.TreeName = "Smiting";
            smiting3a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting3a);

            Talent smiting3b = new Talent();
            smiting3b.Name = "Retaliate";
            smiting3b.Type = TalentType.TriggeredAction;
            smiting3b.Action = ActionType.Reaction;
            smiting3b.DescriptionFluff = "";
            smiting3b.Description = "The attacker su ers a Holy damage roll with a bonus equal to your Presence Attribute. If you are armed with an Amplifier you can expend a charge to add the Amps damage modifier to this roll.";
            smiting3b.ClarifyingText = "Triggering Action: You are hit by a Melee attack.";
            smiting3b.StaminaCost = 4;
            smiting3b.UpkeepCost = null;
            smiting3b.Tier = 3;
            smiting3b.TierBenefitDescription = "Gain +2 to Holy damage. You are considered a Holy target when taking damage.";
            smiting3b.Tree = TalentTree.Smiting;
            smiting3b.TreeName = "Smiting";
            smiting3b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting3b);

            Talent smiting3c = new Talent();
            smiting3c.Name = "Incite the Wicked";
            smiting3c.Type = TalentType.Maneuver;
            smiting3c.Action = ActionType.Combat;
            smiting3c.DescriptionFluff = "";
            smiting3c.Description = "Spell (Area (20’ radius) +0/+0 Holy} [8 Stamina]";
            smiting3c.ClarifyingText = "Targets hit must use their Combat Action to make an attack against you during their next turn. You gain Fortification vs. all damage for 1 round.";
            smiting3c.StaminaCost = 8;
            smiting3c.UpkeepCost = null;
            smiting3c.Tier = 3;
            smiting3c.TierBenefitDescription = "Gain +2 to Holy damage. You are considered a Holy target when taking damage.";
            smiting3c.Tree = TalentTree.Smiting;
            smiting3c.TreeName = "Smiting";
            smiting3c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting3c);
            #endregion
            #region T4
            Talent smiting4a = new Talent();
            smiting4a.Name = "Rebuke";
            smiting4a.Type = TalentType.TriggeredAction;
            smiting4a.Action = ActionType.Reaction;
            smiting4a.DescriptionFluff = "";
            smiting4a.Description = "The creature that made the Triggering attack becomes a target of the attack as well. If the creature is hit add your Presence Attribute to any damage they su er.";
            smiting4a.ClarifyingText = "Triggering Action: You are hit by a Ranged or Area spell attack.";
            smiting4a.StaminaCost = 5;
            smiting4a.UpkeepCost = null;
            smiting4a.Tier = 4;
            smiting4a.TierBenefitDescription = "+2 to CM vs. Unholy and Undead targets";
            smiting4a.Tree = TalentTree.Smiting;
            smiting4a.TreeName = "Smiting";
            smiting4a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting4a);

            Talent smiting4b = new Talent();
            smiting4b.Name = "Reighteous Strike";
            smiting4b.Type = TalentType.AttackAugment;
            smiting4b.Action = ActionType.Quick;
            smiting4b.DescriptionFluff = "";
            smiting4b.Description = "The Augmented attack removes Enhancements, Stances, and e ects that require activation from the target for 1 round if it damages the target.  E ects that are removed by this attack cannot be reactivated for 1 round.";
            smiting4b.ClarifyingText = "";
            smiting4b.StaminaCost = 5;
            smiting4b.UpkeepCost = null;
            smiting4b.Tier = 4;
            smiting4b.TierBenefitDescription = "+2 to CM vs. Unholy and Undead targets";
            smiting4b.Tree = TalentTree.Smiting;
            smiting4b.TreeName = "Smiting";
            smiting4b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting4b);

            Talent smiting4c = new Talent();
            smiting4c.Name = "Righteous Armor";
            smiting4c.Type = TalentType.Enhancement;
            smiting4c.Action = ActionType.Quick;
            smiting4c.DescriptionFluff = "";
            smiting4c.Description = "Unholy and Undead creatures that damage you in Melee lose HP equal to your Presence. You gain Light Fortification vs. all damage and Fortification against Unholy and Necrotic damage.";
            smiting4c.ClarifyingText = "";
            smiting4c.StaminaCost = 10;
            smiting4c.UpkeepCost = 3;
            smiting4c.Tier = 4;
            smiting4c.TierBenefitDescription = "+2 to CM vs. Unholy and Undead targets";
            smiting4c.Tree = TalentTree.Smiting;
            smiting4c.TreeName = "Smiting";
            smiting4c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting4c);
            #endregion
            #region T5
            Talent smiting5a = new Talent();
            smiting5a.Name = "Hammer of God";
            smiting5a.Type = TalentType.Maneuver;
            smiting5a.Action = ActionType.Combat;
            smiting5a.DescriptionFluff = "";
            smiting5a.Description = "Spell {Area (30’ radius within Pistol range) +0/+8 Holy} [12 Stamina]";
            smiting5a.ClarifyingText = "Undead and Unholy targets damaged by this attack become Dazed and Weakened until the end of the encounter. Holy targets in the area su er no damage and become Girded until the end of the encounter.";
            smiting5a.StaminaCost = 12;
            smiting5a.UpkeepCost = null;
            smiting5a.Tier = 5;
            smiting5a.TierBenefitDescription = "Gain Lethal +1 vs. Unholy and Undead targets";
            smiting5a.Tree = TalentTree.Smiting;
            smiting5a.TreeName = "Smiting";
            smiting5a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting5a);

            Talent smiting5b = new Talent();
            smiting5b.Name = "Divinity Incarnate";
            smiting5b.Type = TalentType.Enhancement;
            smiting5b.Action = ActionType.Quick;
            smiting5b.DescriptionFluff = "";
            smiting5b.Description = "You grow wings and gain the Flight[1] Property. Add your Presence in Holy damage to all attacks and ½ your willpower to all attacks and Skill Checks.";
            smiting5b.ClarifyingText = "";
            smiting5b.StaminaCost = 12;
            smiting5b.UpkeepCost = 3;
            smiting5b.Tier = 5;
            smiting5b.TierBenefitDescription = "Gain Lethal +1 vs. Unholy and Undead targets";
            smiting5b.Tree = TalentTree.Smiting;
            smiting5b.TreeName = "Smiting";
            smiting5b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting5b);

            Talent smiting5c = new Talent();
            smiting5c.Name = "Angelic Guardian";
            smiting5c.Type = TalentType.Maneuver;
            smiting5c.Action = ActionType.Combat;
            smiting5c.DescriptionFluff = "";
            smiting5c.Description = "Summon a Size 3 Natural Companion Of your level to fight at your side. All Companions are armed with a weapon or amp of your choice that grants a + 1 to Lethal and attacks, and a + 2 to damage. Use the normal rules for commanding companions in combat. The Companion knows all Smiting Talents of Tier 3 or lower that you know. The Companion gains Heavenly, Natural Armor(any), and Flight(1) as well as one of the following sets of abilities: • The Knight gains Brawny, Tough(2) and Feral(2). • The Mage gains Forceful(2), Fast Recovery(4), and Area attack(10 foot radius in Pistol range / Fire, Cold, or Electricity). • The Healer gains Fast Recovery(4), Leader(Holy), and Talent ability(Mend Wounds).";
            smiting5c.ClarifyingText = "";
            smiting5c.StaminaCost = null;
            smiting5c.UpkeepCost = null;
            smiting5c.FatigueCost = 8;
            smiting5c.Tier = 5;
            smiting5c.TierBenefitDescription = "Gain Lethal +1 vs. Unholy and Undead targets";
            smiting5c.Tree = TalentTree.Smiting;
            smiting5c.TreeName = "Smiting";
            smiting5c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(smiting5c);
            #endregion
            #endregion
            #region Telepathy (Invocation)
            #region T1
            Talent telepathy1a = new Talent();
            telepathy1a.Name = "Charmer";
            telepathy1a.Type = TalentType.Benefit;
            telepathy1a.Action = ActionType.None;
            telepathy1a.DescriptionFluff = "";
            telepathy1a.Description = "Gain +2 to Persuasion checks.";
            telepathy1a.ClarifyingText = "";
            telepathy1a.StaminaCost = null;
            telepathy1a.UpkeepCost = null;
            telepathy1a.Tier = 1;
            telepathy1a.TierBenefitDescription = "Gain +1 damage with attacks that deal Psychic damage.";
            telepathy1a.Tree = TalentTree.Telepathy;
            telepathy1a.TreeName = "Telepathy";
            telepathy1a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy1a);

            Talent telepathy1b = new Talent();
            telepathy1b.Name = "Mind Link";
            telepathy1b.Type = TalentType.Benefit;
            telepathy1b.Action = ActionType.None;
            telepathy1b.DescriptionFluff = "";
            telepathy1b.Description = "You can engage in two-way communication with people within 20’ of you without speaking. You can connect with a number of people equal to your highest Telepathy Tier. Creatures connected to you are also connected to each otherif you choose.";
            telepathy1b.ClarifyingText = "";
            telepathy1b.StaminaCost = null;
            telepathy1b.UpkeepCost = null;
            telepathy1b.Tier = 1;
            telepathy1b.TierBenefitDescription = "Gain +1 damage with attacks that deal Psychic damage.";
            telepathy1b.Tree = TalentTree.Telepathy;
            telepathy1b.TreeName = "Telepathy";
            telepathy1b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy1b);

            Talent telepathy1c = new Talent();
            telepathy1c.Name = "Confound";
            telepathy1c.Type = TalentType.Maneuver;
            telepathy1c.Action = ActionType.Combat;
            telepathy1c.DescriptionFluff = "";
            telepathy1c.Description = "Spell {Ranged (Pistol) +0/+0 Psychic} [4 Stamina]";
            telepathy1c.ClarifyingText = "On a successful hit, the target is Dazed (until Resisted).  Dazed = Cannot spend Stamina, -2 to all non-combat Skill Checks.";
            telepathy1c.StaminaCost = 4;
            telepathy1c.UpkeepCost = null;
            telepathy1c.Tier = 1;
            telepathy1c.TierBenefitDescription = "Gain +1 damage with attacks that deal Psychic damage.";
            telepathy1c.Tree = TalentTree.Telepathy;
            telepathy1c.TreeName = "Telepathy";
            telepathy1c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy1c);
            #endregion
            #region T2
            Talent telepathy2a = new Talent();
            telepathy2a.Name = "Read Thoughts";
            telepathy2a.Type = TalentType.Maneuver;
            telepathy2a.Action = ActionType.Combat;
            telepathy2a.DescriptionFluff = "";
            telepathy2a.Description = "Spell {Ranged (Pistol) +0/-inf Resolve (no damage)} [6 Stamina]";
            telepathy2a.ClarifyingText = "Gain 1-2 sentences worth of surface thoughts as determined by the GM. If the attack exceeds the target’s defense by 3 or more, the target is unaware of the intrusion.If you successfully Read Thoughts without being detected during a social interaction, gain +2 to attack rolls made to Interrogate or Persuade.";
            telepathy2a.StaminaCost = 6;
            telepathy2a.UpkeepCost = null;
            telepathy2a.Tier = 2;
            telepathy2a.TierBenefitDescription = "Gain +2 to Durability vs. Psychic damage";
            telepathy2a.Tree = TalentTree.Telepathy;
            telepathy2a.TreeName = "Telepathy";
            telepathy2a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy2a);

            Talent telepathy2b = new Talent();
            telepathy2b.Name = "Psycic Assault";
            telepathy2b.Type = TalentType.Maneuver;
            telepathy2b.Action = ActionType.Combat;
            telepathy2b.DescriptionFluff = "";
            telepathy2b.Description = "Spell {Area (10’ radius within Pistol range)/Psychic} [6 Stamina]";
            telepathy2b.ClarifyingText = "";
            telepathy2b.StaminaCost = 6;
            telepathy2b.UpkeepCost = null;
            telepathy2b.Tier = 2;
            telepathy2b.TierBenefitDescription = "Gain +2 to Durability vs. Psychic damage";
            telepathy2b.Tree = TalentTree.Telepathy;
            telepathy2b.TreeName = "Telepathy";
            telepathy2b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy2b);

            Talent telepathy2c = new Talent();
            telepathy2c.Name = "Pulling the Wool over Their Eyes (PWTE)";
            telepathy2c.Type = TalentType.Enhancement;
            telepathy2c.Action = ActionType.Quick;
            telepathy2c.DescriptionFluff = "";
            telepathy2c.Description = "Create a Psychic disguise over yourself, masking your true identity from all observers. Use your Invocation Check in place of Disguise checks. This does not change your appearance from cameras and monitors, only the natural vision modes of creatures.";
            telepathy2c.ClarifyingText = "";
            telepathy2c.StaminaCost = 6;
            telepathy2c.UpkeepCost = 2;
            telepathy2c.Tier = 2;
            telepathy2c.TierBenefitDescription = "Gain +2 to Durability vs. Psychic damage";
            telepathy2c.Tree = TalentTree.Telepathy;
            telepathy2c.TreeName = "Telepathy";
            telepathy2c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy2c);
            #endregion
            #region T3
            Talent telepathy3a = new Talent();
            telepathy3a.Name = "Mental Beacon";
            telepathy3a.Type = TalentType.Maneuver;
            telepathy3a.Action = ActionType.Combat;
            telepathy3a.DescriptionFluff = "";
            telepathy3a.Description = "Spell {Area (20’ radius) +0/-inf Resolve (no damage)} [8 Stamina]";
            telepathy3a.ClarifyingText = "Implant a psychic beacon in the minds of all creatures in the area. You are aware of the location of all beaconed creatures.Creatures with beacons cannot benefit from Concealment from you and are Vulnerable to your attacks. This e ect last for 10 minutes.";
            telepathy3a.StaminaCost = 8;
            telepathy3a.UpkeepCost = null;
            telepathy3a.Tier = 3;
            telepathy3a.TierBenefitDescription = "Gain +2 to Defense vs. Deception (which happens to be Area/Resolve).";
            telepathy3a.Tree = TalentTree.Telepathy;
            telepathy3a.TreeName = "Telepathy";
            telepathy3a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy3a);

            Talent telepathy3b = new Talent();
            telepathy3b.Name = "Dominate";
            telepathy3b.Type = TalentType.Maneuver;
            telepathy3b.Action = ActionType.Combat;
            telepathy3b.DescriptionFluff = "";
            telepathy3b.Description = "Spell {Ranged (Pistol) +0/+4 Psychic} [10 Stamina]";
            telepathy3b.ClarifyingText = "Attack a Natural target; damaged targets become Controlled. Controlled targets are commanded like Companions and cannot choose any of their own actions. Any available action not commanded by you is lost with no benefit to the Controlled creatures. The target becomes Dazed and Exhausted (until Resisted). The target is Controlled until it is no longer su ering from Conditions caused by this attack.";
            telepathy3b.StaminaCost = 10;
            telepathy3b.UpkeepCost = null;
            telepathy3b.Tier = 3;
            telepathy3b.TierBenefitDescription = "Gain +2 to Defense vs. Deception (which happens to be Area/Resolve).";
            telepathy3b.Tree = TalentTree.Telepathy;
            telepathy3b.TreeName = "Telepathy";
            telepathy3b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy3b);

            Talent telepathy3c = new Talent();
            telepathy3c.Name = "A Glimmer of Intentions";
            telepathy3c.Type = TalentType.TriggeredAction;
            telepathy3c.Action = ActionType.Reaction;
            telepathy3c.DescriptionFluff = "";
            telepathy3c.Description = "Gain a +4 to all Defenses vs. the attack.";
            telepathy3c.ClarifyingText = "Triggering Action: you are attacked by a target within 20’ of you";
            telepathy3c.StaminaCost = 4;
            telepathy3c.UpkeepCost = null;
            telepathy3c.Tier = 3;
            telepathy3c.TierBenefitDescription = "Gain +2 to Defense vs. Deception (which happens to be Area/Resolve).";
            telepathy3c.Tree = TalentTree.Telepathy;
            telepathy3c.TreeName = "Telepathy";
            telepathy3c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy3c);
            #endregion
            #region T4
            Talent telepathy4a = new Talent();
            telepathy4a.Name = "Hive Mind";
            telepathy4a.Type = TalentType.Ritual;
            telepathy4a.Action = ActionType.Combat;
            telepathy4a.DescriptionFluff = "";
            telepathy4a.Description = "The minds of all allies within 50’ of you become linked to yours as well as each others. While linked, the a ected participants gain the following benefits: • Everyone uses the highest Perception score among the group with a + 2 • Everyone can use the highest Knowledge Skill among the group with a +2 • All participants can communicate telepathically as through Mind Link • Everyone shares the vision of the group.If any one participant is aware of an enemy’s location, all participates are immediately aware of an enemy’s location.";
            telepathy4a.ClarifyingText = "";
            telepathy4a.StaminaCost = null;
            telepathy4a.UpkeepCost = null;
            telepathy4a.FatigueCost = 5;
            telepathy4a.Tier = 4;
            telepathy4a.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            telepathy4a.Tree = TalentTree.Telepathy;
            telepathy4a.TreeName = "Telepathy";
            telepathy4a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy4a);

            Talent telepathy4b = new Talent();
            telepathy4b.Name = "Psycic Blast";
            telepathy4b.Type = TalentType.Maneuver;
            telepathy4b.Action = ActionType.Combat;
            telepathy4b.DescriptionFluff = "";
            telepathy4b.Description = "Spell {Area (60’ cone +0/+0 Psychic} [10 Stamina]";
            telepathy4b.ClarifyingText = "On a Stage 1 Crit, all targets hit are Weakened; Stage 2 Vulnerable and Weakened; Stage 3 Dazed, Vulnerable, and Weakened.";
            telepathy4b.StaminaCost = 10;
            telepathy4b.UpkeepCost = null;
            telepathy4b.Tier = 4;
            telepathy4b.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            telepathy4b.Tree = TalentTree.Telepathy;
            telepathy4b.TreeName = "Telepathy";
            telepathy4b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy4b);

            Talent telepathy4c = new Talent();
            telepathy4c.Name = "Off-Switch";
            telepathy4c.Type = TalentType.Maneuver;
            telepathy4c.Action = ActionType.Combat;
            telepathy4c.DescriptionFluff = "";
            telepathy4c.Description = "Spell {Ranged (Pistol) +0/-inf Resolve (no damage))} [10 Stamina]";
            telepathy4c.ClarifyingText = "On a successful hit, target is Slowed, Weakened, and Vulnerable. On a Crit, the target is also Dazed. On a Stage 2 Crit or higher the target becomes Unconscious.All conditions are until Resisted. Unconscious targets regain conciousness if they take damage.";
            telepathy4c.StaminaCost = 10;
            telepathy4c.UpkeepCost = null;
            telepathy4c.Tier = 4;
            telepathy4c.TierBenefitDescription = "Gain +1 to Resolve Defenses";
            telepathy4c.Tree = TalentTree.Telepathy;
            telepathy4c.TreeName = "Telepathy";
            telepathy4c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy4c);
            #endregion
            #region T5
            Talent telepathy5a = new Talent();
            telepathy5a.Name = "Puppeteer";
            telepathy5a.Type = TalentType.Benefit;
            telepathy5a.Action = ActionType.None;
            telepathy5a.DescriptionFluff = "";
            telepathy5a.Description = "Creatures Controlled by you do not su er the e ects of negative Conditions and gain the Focused and Empowered Conditions. They also su er a -2 to Resistance Checks.";
            telepathy5a.ClarifyingText = "";
            telepathy5a.StaminaCost = null;
            telepathy5a.UpkeepCost = null;
            telepathy5a.Tier = 5;
            telepathy5a.TierBenefitDescription = "Gain +2 to the MCR of all Resistance Checks made against your Psychic effects";
            telepathy5a.Tree = TalentTree.Telepathy;
            telepathy5a.TreeName = "Telepathy";
            telepathy5a.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy5a);

            Talent telepathy5b = new Talent();
            telepathy5b.Name = "Mind Spike";
            telepathy5b.Type = TalentType.Maneuver;
            telepathy5b.Action = ActionType.Combat;
            telepathy5b.DescriptionFluff = "";
            telepathy5b.Description = "Spell {Ranged (30’) +0/+12 Psychic} [12 Stamina]";
            telepathy5b.ClarifyingText = "The attack gains Lethal 1 and ignores Armor.";
            telepathy5b.StaminaCost = 12;
            telepathy5b.UpkeepCost = null;
            telepathy5b.Tier = 5;
            telepathy5b.TierBenefitDescription = "Gain +2 to the MCR of all Resistance Checks made against your Psychic effects";
            telepathy5b.Tree = TalentTree.Telepathy;
            telepathy5b.TreeName = "Telepathy";
            telepathy5b.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy5b);

            Talent telepathy5c = new Talent();
            telepathy5c.Name = "Schism";
            telepathy5c.Type = TalentType.Trick;
            telepathy5c.Action = ActionType.Quick;
            telepathy5c.DescriptionFluff = "";
            telepathy5c.Description = "Activate one Telepathy power of Tier 3 or lower as part of this action. Pay the full Stamina cost of both this Trick and the other power.Schism can only be used once per turn.";
            telepathy5c.ClarifyingText = "";
            telepathy5c.StaminaCost = 6;
            telepathy5c.UpkeepCost = null;
            telepathy5c.Tier = 5;
            telepathy5c.TierBenefitDescription = "Gain +2 to the MCR of all Resistance Checks made against your Psychic effects";
            telepathy5c.Tree = TalentTree.Telepathy;
            telepathy5c.TreeName = "Telepathy";
            telepathy5c.LinkedSkill = WeaponSkill.Invocation;
            Talents.Add(telepathy5c);
            #endregion
            #endregion
            #region Teleportation (Conjuration)
            #region T1
            Talent tele1a = new Talent();
            tele1a.Name = "Phase";
            tele1a.Type = TalentType.TriggeredAction;
            tele1a.Action = ActionType.Reaction;
            tele1a.DescriptionFluff = "";
            tele1a.Description = "You take half damage from the attack. You su er the Vulnerable condition until the start of your next turn.";
            tele1a.ClarifyingText = "Triggering Action: you are hit by an attack";
            tele1a.StaminaCost = 4;
            tele1a.UpkeepCost = null;
            tele1a.Tier = 1;
            tele1a.TierBenefitDescription = "Gain +1 Initiative";
            tele1a.Tree = TalentTree.Teleportation;
            tele1a.TreeName = "Teleportation";
            tele1a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele1a);

            Talent tele1b = new Talent();
            tele1b.Name = "Instant Draw";
            tele1b.Type = TalentType.Trick;
            tele1b.Action = ActionType.Quick;
            tele1b.DescriptionFluff = "";
            tele1b.Description = "You teleport an item on your person (be it stored, concealed, or Readied) into your hand. You are armed with the item (if appropriate).";
            tele1b.ClarifyingText = "";
            tele1b.StaminaCost = 2;
            tele1b.UpkeepCost = null;
            tele1b.Tier = 1;
            tele1b.TierBenefitDescription = "Gain +1 Initiative";
            tele1b.Tree = TalentTree.Teleportation;
            tele1b.TreeName = "Teleportation";
            tele1b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele1b);

            Talent tele1c = new Talent();
            tele1c.Name = "Teleport Item";
            tele1c.Type = TalentType.Ritual;
            tele1c.Action = ActionType.None;
            tele1c.DescriptionFluff = "";
            tele1c.Description = "The target item becomes Prepared. A Prepared item can be summoned to your hand as a Combat Action from anywhere on the Planet. Wards and other e ects that stop teleportation will inhibit you from calling the item.";
            tele1c.ClarifyingText = "Ritual, 1-hour cast, target one adjacent item up to your Presence in size.";
            tele1c.StaminaCost = null;
            tele1c.UpkeepCost = null;
            tele1c.FatigueCost = 1;
            tele1c.Tier = 1;
            tele1c.TierBenefitDescription = "Gain +1 Initiative";
            tele1c.Tree = TalentTree.Teleportation;
            tele1c.TreeName = "Teleportation";
            tele1c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele1c);
            #endregion
            #region T2
            Talent tele2a = new Talent();
            tele2a.Name = "Teleport Disarm";
            tele2a.Type = TalentType.Maneuver;
            tele2a.Action = ActionType.Combat;
            tele2a.DescriptionFluff = "";
            tele2a.Description = "Spell {Ranged (20’) +0/-inf Resolve (no damage)}[6 Stamina]";
            tele2a.ClarifyingText = "On a hit you can teleport one item of your Presence in size or smaller o of the target’s body and deliver it either to your hand, or to another location within 10’ of the target and within line of sight. The item can be in any state on the target’s body (Armed, Stored etc.) but must be visible to you.";
            tele2a.StaminaCost = 6;
            tele2a.UpkeepCost = null;
            tele2a.Tier = 2;
            tele2a.TierBenefitDescription = "Gain +1 Durability vs. attacks that target Physical defenses";
            tele2a.Tree = TalentTree.Teleportation;
            tele2a.TreeName = "Teleportation";
            tele2a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele2a);

            Talent tele2b = new Talent();
            tele2b.Name = "Leap";
            tele2b.Type = TalentType.Trick;
            tele2b.Action = ActionType.Quick;
            tele2b.DescriptionFluff = "";
            tele2b.Description = "You can teleport in any direction a number of MI up to your Presence Attribute. You are Vulnerable until the start of your next turn. You must teleport somewhere within line of sight.";
            tele2b.ClarifyingText = "";
            tele2b.StaminaCost = 6;
            tele2b.UpkeepCost = null;
            tele2b.Tier = 2;
            tele2b.TierBenefitDescription = "Gain +1 Durability vs. attacks that target Physical defenses";
            tele2b.Tree = TalentTree.Teleportation;
            tele2b.TreeName = "Teleportation";
            tele2b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele2b);

            Talent tele2c = new Talent();
            tele2c.Name = "Redirect Melee Attack";
            tele2c.Type = TalentType.TriggeredAction;
            tele2c.Action = ActionType.Reaction;
            tele2c.DescriptionFluff = "";
            tele2c.Description = "You force the creature that made the Triggering attack to re-roll the attack against itself. This attack uses the same modifiers as the Triggering attack.";
            tele2c.ClarifyingText = "Triggering Action: you are missed by a Melee attack.";
            tele2c.StaminaCost = 4;
            tele2c.UpkeepCost = null;
            tele2c.Tier = 2;
            tele2c.TierBenefitDescription = "Gain +1 Durability vs. attacks that target Physical defenses";
            tele2c.Tree = TalentTree.Teleportation;
            tele2c.TreeName = "Teleportation";
            tele2c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele2c);
            #endregion
            #region T3
            Talent tele3a = new Talent();
            tele3a.Name = "Warp";
            tele3a.Type = TalentType.Maneuver;
            tele3a.Action = ActionType.Combat;
            tele3a.DescriptionFluff = "";
            tele3a.Description = "Spell {Ranged (SMG) +0/+4 Force} [8 Stamina]";
            tele3a.ClarifyingText = "";
            tele3a.StaminaCost = null;
            tele3a.UpkeepCost = null;
            tele3a.Tier = 3;
            tele3a.TierBenefitDescription = "Your Presence Attribute is considered 2 points higher when determining the distance you can move with Teleportation powers.";
            tele3a.Tree = TalentTree.Teleportation;
            tele3a.TreeName = "Teleportation";
            tele3a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele3a);

            Talent tele3b = new Talent();
            tele3b.Name = "Teleport Self";
            tele3b.Type = TalentType.Ritual;
            tele3b.Action = ActionType.None;
            tele3b.DescriptionFluff = "";
            tele3b.Description = "At the end of this Ritual’s casting time, you teleport yourself in a direction of your choosing. The maximum distance you can travel is determined by making a Conjuration Check and multiplying the result by a distance determined by the casting time you selected for this ritual.With this Ritual you pick a direction and a distance.To hit a specific target space you will need to make a Knowledge Geography Check with an MCR determined by your GM based on distance and the size of the target space.After teleporting any distance with this ritual you are Vulnerable until the beginning of your next turn. • 10 minute casting time: teleport 500 ft. times the result of Conjuration Check. • 1 hour casting time: teleport 1 mile times the result of Conjuration Check.";
            tele3b.ClarifyingText = "";
            tele3b.StaminaCost = null;
            tele3b.UpkeepCost = null;
            tele3b.FatigueCost = 4;
            tele3b.Tier = 3;
            tele3b.TierBenefitDescription = "Your Presence Attribute is considered 2 points higher when determining the distance you can move with Teleportation powers.";
            tele3b.Tree = TalentTree.Teleportation;
            tele3b.TreeName = "Teleportation";
            tele3b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele3b);

            Talent tele3c = new Talent();
            tele3c.Name = "Teleport Other";
            tele3c.Type = TalentType.Maneuver;
            tele3c.Action = ActionType.Combat;
            tele3c.DescriptionFluff = "";
            tele3c.Description = "Spell {Ranged (50’) +0/-inf Resolve (no damage)} [8 Stamina]";
            tele3c.ClarifyingText = "You teleport the target a number of MI equal to your Presence + the base damage of the Amplifier used.Allies that are targeted by this attack can chose to be automatically hit by it.Anyone hit by this attack gains Vulnerable until the start of your next turn.";
            tele3c.StaminaCost = 8;
            tele3c.UpkeepCost = null;
            tele3c.Tier = 3;
            tele3c.TierBenefitDescription = "Your Presence Attribute is considered 2 points higher when determining the distance you can move with Teleportation powers.";
            tele3c.Tree = TalentTree.Teleportation;
            tele3c.TreeName = "Teleportation";
            tele3c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele3c);
            #endregion
            #region T4
            Talent tele4a = new Talent();
            tele4a.Name = "Redirect";
            tele4a.Type = TalentType.TriggeredAction;
            tele4a.Action = ActionType.Reaction;
            tele4a.DescriptionFluff = "";
            tele4a.Description = "You gain a +4 to defenses against the Triggering attack. If you are missed by the attack, you can chose a new target for the attack that is within sight.In the case of an Area attack you chose a new location for the area. You force the creature that made the Triggering attack to re - roll the attack against the new target(s). This attack uses the same modifiers as the Triggering attack.";
            tele4a.ClarifyingText = "Triggering Action: you are hit by Ranged or Area attack.";
            tele4a.StaminaCost = 8;
            tele4a.UpkeepCost = null;
            tele4a.Tier = 4;
            tele4a.TierBenefitDescription = "You are no longer Vulnerable after teleporting any distance";
            tele4a.Tree = TalentTree.Teleportation;
            tele4a.TreeName = "Teleportation";
            tele4a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele4a);

            Talent tele4b = new Talent();
            tele4b.Name = "Teleport Group";
            tele4b.Type = TalentType.Ritual;
            tele4b.Action = ActionType.None;
            tele4b.DescriptionFluff = "";
            tele4b.Description = "At the end of this Ritual’s casting time, you teleport yourself and a number of adjacent willing creatures equal to your Presence Attribute in a direction of your choosing. The maximum distance you can travel is determined by making a Conjuration Check and multiplying the result by a distance determined by the casting time you selected for this ritual.With this Ritual you pick a direction and a distance, to hit a specific target space you will need to make a Knowledge Geography Check with an MCR determined by your GM based on distance and the size of the target space. After teleporting any distance with this ritual you are Vulnerable until the beginning of your next turn. • 10 minute casting time: teleport 500 ft. times the result of Conjuration Check. • 1 hour casting time: teleport 1 mile times the result of Conjuration Check.";
            tele4b.ClarifyingText = "Ritual: 10 minutes or 1 hour to cast";
            tele4b.StaminaCost = null;
            tele4b.UpkeepCost = null;
            tele4b.FatigueCost = 4;
            tele4b.Tier = 4;
            tele4b.TierBenefitDescription = "You are no longer Vulnerable after teleporting any distance";
            tele4b.Tree = TalentTree.Teleportation;
            tele4b.TreeName = "Teleportation";
            tele4b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele4b);

            Talent tele4c = new Talent();
            tele4c.Name = "Hop";
            tele4c.Type = TalentType.Trick;
            tele4c.Action = ActionType.Quick;
            tele4c.DescriptionFluff = "";
            tele4c.Description = "Teleport a number of MI equal to your Presence + your total Conjuration Skill. You must teleport somewhere within line of sight.";
            tele4c.ClarifyingText = "";
            tele4c.StaminaCost = 10;
            tele4c.UpkeepCost = null;
            tele4c.Tier = 4;
            tele4c.TierBenefitDescription = "You are no longer Vulnerable after teleporting any distance";
            tele4c.Tree = TalentTree.Teleportation;
            tele4c.TreeName = "Teleportation";
            tele4c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele4c);
            #endregion
            #region T5
            Talent tele5a = new Talent();
            tele5a.Name = "Disintegration";
            tele5a.Type = TalentType.Maneuver;
            tele5a.Action = ActionType.Combat;
            tele5a.DescriptionFluff = "";
            tele5a.Description = "Spell {Area (5’ radius within 50’ +0/+10 Force} [12 Stamina]";
            tele5a.ClarifyingText = "Unattended objects and structures within the area su er triple damage over Durability from this attack.";
            tele5a.StaminaCost = 12;
            tele5a.UpkeepCost = null;
            tele5a.Tier = 5;
            tele5a.TierBenefitDescription = "Gain -1 to the CM of attacks against you";
            tele5a.Tree = TalentTree.Teleportation;
            tele5a.TreeName = "Teleportation";
            tele5a.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele5a);

            Talent tele5b = new Talent();
            tele5b.Name = "Portal";
            tele5b.Type = TalentType.Ritual;
            tele5b.Action = ActionType.None;
            tele5b.DescriptionFluff = "";
            tele5b.Description = "You open a portal with a maximum radius equal to your Presence Attribute. This portal connects a space within 10’ of you to a space within 100 miles that you have been before. The portal can be kept open during each of your turns by spending 2 Stamina as a Quick Action. Failure to do so will result in the portal closing and this Ritual ending at the end of your next turn.Creatures can move freely between the sides of the portal without spending additional MI and they are not Vulnerable afterwards. The portal and what lies on the other side of it are both plainly visible to casual observation.";
            tele5b.ClarifyingText = "";
            tele5b.StaminaCost = null;
            tele5b.UpkeepCost = null;
            tele5b.FatigueCost = 6;
            tele5b.Tier = 5;
            tele5b.TierBenefitDescription = "Gain -1 to the CM of attacks against you";
            tele5b.Tree = TalentTree.Teleportation;
            tele5b.TreeName = "Teleportation";
            tele5b.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele5b);

            Talent tele5c = new Talent();
            tele5c.Name = "Wave of Distortion";
            tele5c.Type = TalentType.Maneuver;
            tele5c.Action = ActionType.Combat;
            tele5c.DescriptionFluff = "";
            tele5c.Description = "Spell {Area (60’cone) +0/+4 Force} [12 Stamina]";
            tele5c.ClarifyingText = "Creatures su ering Crits from this attack su er one additional Secondary e ect from the Crit and all Resistance Checks made against these e ects are made at a - 2.";
            tele5c.StaminaCost = 12;
            tele5c.UpkeepCost = null;
            tele5c.Tier = 5;
            tele5c.TierBenefitDescription = "Gain -1 to the CM of attacks against you";
            tele5c.Tree = TalentTree.Teleportation;
            tele5c.TreeName = "Teleportation";
            tele5c.LinkedSkill = WeaponSkill.Conjuration;
            Talents.Add(tele5c);
            #endregion
            #endregion
            //TODO: Accept stats as Linked Skills
            #region Armored Fighting (Strength)
            #region T1
            Talent armor1a = new Talent();
            armor1a.Name = "Endurance Training";
            armor1a.Type = TalentType.Benefit;
            armor1a.Action = ActionType.None;
            armor1a.DescriptionFluff = "";
            armor1a.Description = "Reduce the Stamina penalty for worn armor to 0.";
            armor1a.ClarifyingText = "";
            armor1a.StaminaCost = null;
            armor1a.UpkeepCost = null;
            armor1a.Tier = 1;
            armor1a.TierBenefitDescription = "Add the Armor Value of the armor you are wearing to your 2nd Damage Track";
            armor1a.Tree = TalentTree.ArmoredFighting;
            armor1a.TreeName = "Armored Fighting";
            armor1a.LinkedSkill = null;
            Talents.Add(armor1a);

            Talent armor1b = new Talent();
            armor1b.Name = "Web Harness";
            armor1b.Type = TalentType.Benefit;
            armor1b.Action = ActionType.None;
            armor1b.DescriptionFluff = "";
            armor1b.Description = "Readied ammunition does not take up Holster space as long as you are wearing armor.";
            armor1b.ClarifyingText = "";
            armor1b.StaminaCost = null;
            armor1b.UpkeepCost = null;
            armor1b.Tier = 1;
            armor1b.TierBenefitDescription = "Add the Armor Value of the armor you are wearing to your 2nd Damage Track";
            armor1b.Tree = TalentTree.ArmoredFighting;
            armor1b.TreeName = "Armored Fighting";
            armor1b.LinkedSkill = null;
            Talents.Add(armor1b);

            Talent armor1c = new Talent();
            armor1c.Name = "Weight Distribution";
            armor1c.Type = TalentType.Benefit;
            armor1c.Action = ActionType.None;
            armor1c.DescriptionFluff = "";
            armor1c.Description = "Increase your carrying capacity by 10.";
            armor1c.ClarifyingText = "";
            armor1c.StaminaCost = null;
            armor1c.UpkeepCost = null;
            armor1c.Tier = 1;
            armor1c.TierBenefitDescription = "Add the Armor Value of the armor you are wearing to your 2nd Damage Track";
            armor1c.Tree = TalentTree.ArmoredFighting;
            armor1c.TreeName = "Armored Fighting";
            armor1c.LinkedSkill = null;
            Talents.Add(armor1c);
            #endregion
            #region T2
            Talent armor2a = new Talent();
            armor2a.Name = "Square On";
            armor2a.Type = TalentType.Stance;
            armor2a.Action = ActionType.Quick;
            armor2a.DescriptionFluff = "";
            armor2a.Description = "Gain +2 to Armor Value.";
            armor2a.ClarifyingText = "";
            armor2a.StaminaCost = 6;
            armor2a.UpkeepCost = 2;
            armor2a.Tier = 2;
            armor2a.TierBenefitDescription = "You gain Heavy Armor Proficiency";
            armor2a.Tree = TalentTree.ArmoredFighting;
            armor2a.TreeName = "Armored Fighting";
            armor2a.LinkedSkill = null;
            Talents.Add(armor2a);

            Talent armor2b = new Talent();
            armor2b.Name = "Vitals Padding";
            armor2b.Type = TalentType.Benefit;
            armor2b.Action = ActionType.None;
            armor2b.DescriptionFluff = "";
            armor2b.Description = "While wearing armor, receive a -1 to the CM of attacks against you. You must spend 1 hour and 100UEU per point of Armor Value of the armor to receive this Benefit.";
            armor2b.ClarifyingText = "";
            armor2b.StaminaCost = null;
            armor2b.UpkeepCost = null;
            armor2b.Tier = 2;
            armor2b.TierBenefitDescription = "You gain Heavy Armor Proficiency";
            armor2b.Tree = TalentTree.ArmoredFighting;
            armor2b.TreeName = "Armored Fighting";
            armor2b.LinkedSkill = null;
            Talents.Add(armor2b);

            Talent armor2c = new Talent();
            armor2c.Name = "Double Time";
            armor2c.Type = TalentType.Enhancement;
            armor2c.Action = ActionType.Quick;
            armor2c.DescriptionFluff = "";
            armor2c.Description = "While wearing armor, ignore Speed penalties of armor and increase the cap on additional MI that can be purchased with Stamina by 2.";
            armor2c.ClarifyingText = "";
            armor2c.StaminaCost = 6;
            armor2c.UpkeepCost = 2;
            armor2c.Tier = 2;
            armor2c.TierBenefitDescription = "You gain Heavy Armor Proficiency";
            armor2c.Tree = TalentTree.ArmoredFighting;
            armor2c.TreeName = "Armored Fighting";
            armor2c.LinkedSkill = null;
            Talents.Add(armor2c);
            #endregion
            #region T3
            Talent armor3a = new Talent();
            armor3a.Name = "Servo Utilization";
            armor3a.Type = TalentType.Benefit;
            armor3a.Action = ActionType.None;
            armor3a.DescriptionFluff = "";
            armor3a.Description = "Gain +1 to your Strength Attribute when wearing armor with Powered Servos.";
            armor3a.ClarifyingText = "";
            armor3a.StaminaCost = null;
            armor3a.UpkeepCost = null;
            armor3a.Tier = 3;
            armor3a.TierBenefitDescription = "Gain +1 to the Armor Value of armor you wear";
            armor3a.Tree = TalentTree.ArmoredFighting;
            armor3a.TreeName = "Armored Fighting";
            armor3a.LinkedSkill = null;
            Talents.Add(armor3a);

            Talent armor3b = new Talent();
            armor3b.Name = "Shield Expertise";
            armor3b.Type = TalentType.Maneuver;
            armor3b.Action = ActionType.Combat;
            armor3b.DescriptionFluff = "";
            armor3b.Description = "Increase the Cover Toughness of all shields you wield by 1.";
            armor3b.ClarifyingText = "";
            armor3b.StaminaCost = null;
            armor3b.UpkeepCost = null;
            armor3b.Tier = 3;
            armor3b.TierBenefitDescription = "Gain +1 to the Armor Value of armor you wear";
            armor3b.Tree = TalentTree.ArmoredFighting;
            armor3b.TreeName = "Armored Fighting";
            armor3b.LinkedSkill = null;
            Talents.Add(armor3b);

            Talent armor3c = new Talent();
            armor3c.Name = "";
            armor3c.Type = TalentType.Maneuver;
            armor3c.Action = ActionType.Combat;
            armor3c.DescriptionFluff = "Encumbrance Management";
            armor3c.Description = "Can sling 2 additional items in a Readied state.";
            armor3c.ClarifyingText = "";
            armor3c.StaminaCost = null;
            armor3c.UpkeepCost = null;
            armor3c.Tier = 3;
            armor3c.TierBenefitDescription = "Gain +1 to the Armor Value of armor you wear";
            armor3c.Tree = TalentTree.ArmoredFighting;
            armor3c.TreeName = "Armored Fighting";
            armor3c.LinkedSkill = null;
            Talents.Add(armor3c);
            #endregion
            #region T4
            Talent armor4a = new Talent();
            armor4a.Name = "Let the Suit Do the Work (LSDW)";
            armor4a.Type = TalentType.Maneuver;
            armor4a.Action = ActionType.Combat;
            armor4a.DescriptionFluff = "";
            armor4a.Description = "+2 to Stamina while wearing armor with Powered Servos.";
            armor4a.ClarifyingText = "";
            armor4a.StaminaCost = null;
            armor4a.UpkeepCost = null;
            armor4a.Tier = 4;
            armor4a.TierBenefitDescription = "You gain Advanced Armor Proficiency";
            armor4a.Tree = TalentTree.ArmoredFighting;
            armor4a.TreeName = "Armored Fighting";
            armor4a.LinkedSkill = null;
            Talents.Add(armor4a);

            Talent armor4b = new Talent();
            armor4b.Name = "Breakdown Training";
            armor4b.Type = TalentType.Maneuver;
            armor4b.Action = ActionType.Combat;
            armor4b.DescriptionFluff = "";
            armor4b.Description = "You can don or remove your armor and are Girded until the end of your next turn.";
            armor4b.ClarifyingText = "";
            armor4b.StaminaCost = 8;
            armor4b.UpkeepCost = null;
            armor4b.Tier = 4;
            armor4b.TierBenefitDescription = "You gain Advanced Armor Proficiency";
            armor4b.Tree = TalentTree.ArmoredFighting;
            armor4b.TreeName = "Armored Fighting";
            armor4b.LinkedSkill = null;
            Talents.Add(armor4b);

            Talent armor4c = new Talent();
            armor4c.Name = "Momentum";
            armor4c.Type = TalentType.Benefit;
            armor4c.Action = ActionType.None;
            armor4c.DescriptionFluff = "";
            armor4c.Description = "Add ½ the Armor Value of the armor you are wearing to your damage rolls on Knockback attacks and to Knockdown attacks. You can Push Through an opponent’s space with 1 additional MI.";
            armor4c.ClarifyingText = "";
            armor4c.StaminaCost = null;
            armor4c.UpkeepCost = null;
            armor4c.Tier = 4;
            armor4c.TierBenefitDescription = "You gain Advanced Armor Proficiency";
            armor4c.Tree = TalentTree.ArmoredFighting;
            armor4c.TreeName = "Armored Fighting";
            armor4c.LinkedSkill = null;
            Talents.Add(armor4c);
            #endregion
            #region T5
            Talent armor5a = new Talent();
            armor5a.Name = "Second Skin";
            armor5a.Type = TalentType.Benefit;
            armor5a.Action = ActionType.None;
            armor5a.DescriptionFluff = "";
            armor5a.Description = "Increase Armor Value of any worn armor by 2.";
            armor5a.ClarifyingText = "";
            armor5a.StaminaCost = null;
            armor5a.UpkeepCost = null;
            armor5a.Tier = 5;
            armor5a.TierBenefitDescription = "Gain -1 to the Armor Penalty of all armor you wear";
            armor5a.Tree = TalentTree.ArmoredFighting;
            armor5a.TreeName = "Armored Fighting";
            armor5a.LinkedSkill = null;
            Talents.Add(armor5a);

            Talent armor5b = new Talent();
            armor5b.Name = "Customized Suit";
            armor5b.Type = TalentType.Benefit;
            armor5b.Action = ActionType.None;
            armor5b.DescriptionFluff = "";
            armor5b.Description = "Add 1 additional Mod Slot and reduce the Armor Penalty to Skill Checks by 1. You must spend 1 day’s work and 1000x the Armor Value of the armor to gain this Benefit with a particular suit of armor or to change the suit of armor this Talent applies to.";
            armor5b.ClarifyingText = "";
            armor5b.StaminaCost = null;
            armor5b.UpkeepCost = null;
            armor5b.Tier = 5;
            armor5b.TierBenefitDescription = "Gain -1 to the Armor Penalty of all armor you wear";
            armor5b.Tree = TalentTree.ArmoredFighting;
            armor5b.TreeName = "Armored Fighting";
            armor5b.LinkedSkill = null;
            Talents.Add(armor5b);

            Talent armor5c = new Talent();
            armor5c.Name = "Lucky Bastard";
            armor5c.Type = TalentType.TriggeredAction;
            armor5c.Action = ActionType.Reaction;
            armor5c.DescriptionFluff = "";
            armor5c.Description = "The attack causes no damage. Instead, the value of your armor is reduced by 2 until repaired (1 hour of work).";
            armor5c.ClarifyingText = "Triggering Action: a Crit is scored against you";
            armor5c.StaminaCost = 10;
            armor5c.UpkeepCost = null;
            armor5c.Tier = 5;
            armor5c.TierBenefitDescription = "Gain -1 to the Armor Penalty of all armor you wear";
            armor5c.Tree = TalentTree.ArmoredFighting;
            armor5c.TreeName = "Armored Fighting";
            armor5c.LinkedSkill = null;
            Talents.Add(armor5c);
            #endregion
            #endregion
            #region Leadership (Presence)
            #region T1
            Talent leader1a = new Talent();
            leader1a.Name = "Taunt";
            leader1a.Type = TalentType.Maneuver;
            leader1a.Action = ActionType.Combat;
            leader1a.DescriptionFluff = "";
            leader1a.Description = "Roll double your Presence Attribute against the Area/Resolve Defense of a target within 30’ of you. Creatures hit must spend their Combat Action attacking you, unless they are already in Melee with another creature.";
            leader1a.ClarifyingText = "";
            leader1a.StaminaCost = 4;
            leader1a.UpkeepCost = null;
            leader1a.Tier = 1;
            leader1a.TierBenefitDescription = "Gain +1 to Negotiation and Persuasion checks";
            leader1a.Tree = TalentTree.Leadership;
            leader1a.TreeName = "Leadership";
            leader1a.LinkedSkill = null;
            Talents.Add(leader1a);

            Talent leader1b = new Talent();
            leader1b.Name = "Influence";
            leader1b.Type = TalentType.Benefit;
            leader1b.Action = ActionType.None;
            leader1b.DescriptionFluff = "";
            leader1b.Description = "You can increase your Lifestyle by one level at no cost.";
            leader1b.ClarifyingText = "";
            leader1b.StaminaCost = null;
            leader1b.UpkeepCost = null;
            leader1b.Tier = 1;
            leader1b.TierBenefitDescription = "Gain +1 to Negotiation and Persuasion checks";
            leader1b.Tree = TalentTree.Leadership;
            leader1b.TreeName = "Leadership";
            leader1b.LinkedSkill = null;
            Talents.Add(leader1b);

            Talent leader1c = new Talent();
            leader1c.Name = "Powerful Voice";
            leader1c.Type = TalentType.TriggeredAction;
            leader1c.Action = ActionType.Reaction;
            leader1c.DescriptionFluff = "";
            leader1c.Description = "Roll double your Presence Attribute against the Area/Resolve Defense of the creature making the Triggering attack. If you hit, the attacker is Weakened for the Triggering attack, and the Stage of the Crit(if any) for the attack is reduced by 1.If you miss, you are Vulnerable to the Triggering attack.";
            leader1c.ClarifyingText = "Triggering Action: you are attacked in Melee";
            leader1c.StaminaCost = 4;
            leader1c.UpkeepCost = null;
            leader1c.Tier = 1;
            leader1c.TierBenefitDescription = "Gain +1 to Negotiation and Persuasion checks";
            leader1c.Tree = TalentTree.Leadership;
            leader1c.TreeName = "Leadership";
            leader1c.LinkedSkill = null;
            Talents.Add(leader1c);
            #endregion
            #region T2
            Talent leader2a = new Talent();
            leader2a.Name = "Inspire";
            leader2a.Type = TalentType.Enhancement;
            leader2a.Action = ActionType.Quick;
            leader2a.DescriptionFluff = "";
            leader2a.Description = "A single creature other than yourself within 20’ gains Focused. The target must be within range for you to maintain this Enhancement.";
            leader2a.ClarifyingText = "Enhancement: target ally within 20’.  Focused = +1 to Melee weapon attaks, +1 to all defenses, +2 to Focus linked non-combat skill checks.";
            leader2a.StaminaCost = 3;
            leader2a.UpkeepCost = 0;
            leader2a.Tier = 2;
            leader2a.TierBenefitDescription = "Gain any 2 Contacts";
            leader2a.Tree = TalentTree.Leadership;
            leader2a.TreeName = "Leadership";
            leader2a.LinkedSkill = null;
            Talents.Add(leader2a);

            Talent leader2b = new Talent();
            leader2b.Name = "Lackey";
            leader2b.Type = TalentType.Benefit;
            leader2b.Action = ActionType.None;
            leader2b.DescriptionFluff = "";
            leader2b.Description = "You gain a Size 3 Natural Companion of your level -2. A Lackey starts with normal equipment for a Companion of its level. When you level(and by proxy your Lackey levels) the Lackey gains the di erence in the equipment value of his last level and his new one in equipment and upgrades.A Lackey does not store money and its equipment cannot be taken, sold, or used in any way except for equipping the Lackey.A Lackey has 1 Talent from the Talent Ability(see Chapter 9: Opponents).A Lackey cannot have Talents from the Leadership Tree. The Lackey also gains a Lackey sub - type and associated Quality from the list below.When a Lackey moves up in level it can replace the Talents it knows with new ones as long as it qualifies for the new Talent. The Lackey Talent can be taken multiple times. • Brawlers gain Brawny(1). • Casters gain Forcful(1) or Honed(1). • Gunners gain Slight(1). • Sneaks gain Skilled(2).";
            leader2b.ClarifyingText = "";
            leader2b.StaminaCost = null;
            leader2b.UpkeepCost = null;
            leader2b.Tier = 2;
            leader2b.TierBenefitDescription = "Gain any 2 Contacts";
            leader2b.Tree = TalentTree.Leadership;
            leader2b.TreeName = "Leadership";
            leader2b.LinkedSkill = null;
            Talents.Add(leader2b);

            Talent leader2c = new Talent();
            leader2c.Name = "Voice of Reason";
            leader2c.Type = TalentType.Ritual;
            leader2c.Action = ActionType.Combat;
            leader2c.DescriptionFluff = "";
            leader2c.Description = "You gain a +2 to Resolve Defenses and gain a +2 to Negotiation and Persuasion Checks. The Ritual lasts for the remainder of the encounter as long as you don’t take a Combat Action.";
            leader2c.ClarifyingText = "Ritual: Combat Action cast";
            leader2c.StaminaCost = null;
            leader2c.UpkeepCost = null;
            leader2c.FatigueCost = 3;
            leader2c.Tier = 2;
            leader2c.TierBenefitDescription = "Gain any 2 Contacts";
            leader2c.Tree = TalentTree.Leadership;
            leader2c.TreeName = "Leadership";
            leader2c.LinkedSkill = null;
            Talents.Add(leader2c);
            #endregion
            #region T3
            Talent leader3a = new Talent();
            leader3a.Name = "Rally";
            leader3a.Type = TalentType.Ritual;
            leader3a.Action = ActionType.Combat;
            leader3a.DescriptionFluff = "";
            leader3a.Description = "All allies within 50’ of you regain 5 HP in the 1st and 2nd Tracks, and all Conditions a ecting them are Suppressed until the end of your next turn.";
            leader3a.ClarifyingText = "";
            leader3a.StaminaCost = null;
            leader3a.UpkeepCost = null;
            leader3a.FatigueCost = 5;
            leader3a.Tier = 3;
            leader3a.TierBenefitDescription = "Your Companions require only 1 MI to command them in combat";
            leader3a.Tree = TalentTree.Leadership;
            leader3a.TreeName = "Leadership";
            leader3a.LinkedSkill = null;
            Talents.Add(leader3a);

            Talent leader3b = new Talent();
            leader3b.Name = "Follower";
            leader3b.Type = TalentType.Benefit;
            leader3b.Action = ActionType.None;
            leader3b.DescriptionFluff = "";
            leader3b.Description = "One of your Lackeys becomes a Follower and has its level increased by 1 (to a maximum of your level - 1). This Follower gains all associated bonuses from this increase in level as well as a second Talent from the Talent Ability(see Chapter 9: Opponents).It also gains additional Qualities based on its Lackey sub - type(see below). This Talent can be taken multiple times, each time a ecting a di erent Lackey. • Brawlers gain Brawny(1), Feral(2), and Tough (2). • Casters gain Honed(1), Evasive(Resolve / 2), and Forceful(2). • Gunners gain Slight(1), Evasive(Ranged / 2), and Honed(2). • Sneaks gain Skilled(2), Unerring(1 / any one attack), and Powerful(1 / any one attack).";
            leader3b.ClarifyingText = "";
            leader3b.StaminaCost = null;
            leader3b.UpkeepCost = null;
            leader3b.Tier = 3;
            leader3b.TierBenefitDescription = "Your Companions require only 1 MI to command them in combat";
            leader3b.Tree = TalentTree.Leadership;
            leader3b.TreeName = "Leadership";
            leader3b.LinkedSkill = null;
            Talents.Add(leader3b);

            Talent leader3c = new Talent();
            leader3c.Name = "Authority Figure";
            leader3c.Type = TalentType.Benefit;
            leader3c.Action = ActionType.None;
            leader3c.DescriptionFluff = "";
            leader3c.Description = "You gain a +1 to Resolve Defenses, Resistance, and can re-roll 1s when making Social Skill Checks.";
            leader3c.ClarifyingText = "";
            leader3c.StaminaCost = null;
            leader3c.UpkeepCost = null;
            leader3c.Tier = 3;
            leader3c.TierBenefitDescription = "Your Companions require only 1 MI to command them in combat";
            leader3c.Tree = TalentTree.Leadership;
            leader3c.TreeName = "Leadership";
            leader3c.LinkedSkill = null;
            Talents.Add(leader3c);
            #endregion
            #region T4
            Talent leader4a = new Talent();
            leader4a.Name = "Lead By Example";
            leader4a.Type = TalentType.Stance;
            leader4a.Action = ActionType.Quick;
            leader4a.DescriptionFluff = "";
            leader4a.Description = "Your Companions gain a +1 to attack and +2 to damage for 1 round whenever you cause damage to an enemy.";
            leader4a.ClarifyingText = "";
            leader4a.StaminaCost = 10;
            leader4a.UpkeepCost = 3;
            leader4a.Tier = 4;
            leader4a.TierBenefitDescription = "You may have up to your Presence +1 in Companions at any time. Double your Presence to determine the maximum number of Companions you can have.";
            leader4a.Tree = TalentTree.Leadership;
            leader4a.TreeName = "Leadership";
            leader4a.LinkedSkill = null;
            Talents.Add(leader4a);

            Talent leader4b = new Talent();
            leader4b.Name = "Devotee";
            leader4b.Type = TalentType.Benefit;
            leader4b.Action = ActionType.None;
            leader4b.DescriptionFluff = "";
            leader4b.Description = "One of your Followers becomes a Devotee and has its level increased by 1 (to a maximum of your level). This Devotee gains all associated bonuses from this increase in level as well as a third Talent from the Talent Ability(see Chapter 9: Opponents).It also gains a second Lackey subtype (including all associated Qualities, see below). This Talent can be taken multiple times, each a ecting a di erent Follower. • Brawlers gain Brawny(2), Feral(2), and Tough (2). • Casters gain Honed(2), Evasive(Resolve / 2), and Forceful(2). • Gunners gain Slight(2), Evasive(Ranged / 2), and Honed(2). • Sneaks gain Skilled(2), Unerring(2 / any one attack), and Powerful(2 / any one attack).";
            leader4b.ClarifyingText = "";
            leader4b.StaminaCost = null;
            leader4b.UpkeepCost = null;
            leader4b.Tier = 4;
            leader4b.TierBenefitDescription = "You may have up to your Presence +1 in Companions at any time. Double your Presence to determine the maximum number of Companions you can have.";
            leader4b.Tree = TalentTree.Leadership;
            leader4b.TreeName = "Leadership";
            leader4b.LinkedSkill = null;
            Talents.Add(leader4b);

            Talent leader4c = new Talent();
            leader4c.Name = "Advanced Training";
            leader4c.Type = TalentType.Benefit;
            leader4c.Action = ActionType.None;
            leader4c.DescriptionFluff = "";
            leader4c.Description = "Each of your Companions gains an additional Talent of your max Tier -1 from any Talent Tree except Leadership.";
            leader4c.ClarifyingText = "";
            leader4c.StaminaCost = null;
            leader4c.UpkeepCost = null;
            leader4c.Tier = 4;
            leader4c.TierBenefitDescription = "You may have up to your Presence +1 in Companions at any time. Double your Presence to determine the maximum number of Companions you can have.";
            leader4c.Tree = TalentTree.Leadership;
            leader4c.TreeName = "Leadership";
            leader4c.LinkedSkill = null;
            Talents.Add(leader4c);
            #endregion
            #region T5
            Talent leader5a = new Talent();
            leader5a.Name = "Born To Lead";
            leader5a.Type = TalentType.Benefit;
            leader5a.Action = ActionType.None;
            leader5a.DescriptionFluff = "";
            leader5a.Description = "For every time you have taken the Lackey Talent you gain 2 Lackeys instead of 1. For each additional time you take the Lackey Talent you will also gain 2 Lackeys instead of 1. These Lackeys follow all of the usual rules that pertain to Lackeys.";
            leader5a.ClarifyingText = "";
            leader5a.StaminaCost = null;
            leader5a.UpkeepCost = null;
            leader5a.Tier = 5;
            leader5a.TierBenefitDescription = "Pay 20% less for all purchases";
            leader5a.Tree = TalentTree.Leadership;
            leader5a.TreeName = "Leadership";
            leader5a.LinkedSkill = null;
            Talents.Add(leader5a);

            Talent leader5b = new Talent();
            leader5b.Name = "Quest";
            leader5b.Type = TalentType.Maneuver;
            leader5b.Action = ActionType.Combat;
            leader5b.DescriptionFluff = "";
            leader5b.Description = "While being commanded by you, your Companions gain a +1 to attacks, Defenses, damage, and Durability.";
            leader5b.ClarifyingText = "";
            leader5b.StaminaCost = null;
            leader5b.UpkeepCost = null;
            leader5b.Tier = 5;
            leader5b.TierBenefitDescription = "Pay 20% less for all purchases";
            leader5b.Tree = TalentTree.Leadership;
            leader5b.TreeName = "Leadership";
            leader5b.LinkedSkill = null;
            Talents.Add(leader5b);

            Talent leader5c = new Talent();
            leader5c.Name = "Fanatics";
            leader5c.Type = TalentType.TriggeredAction;
            leader5c.Action = ActionType.Reaction;
            leader5c.DescriptionFluff = "";
            leader5c.Description = "The Companion is hit instead and su ers all e ects of the attack instead of you.";
            leader5c.ClarifyingText = "Triggering Action: you are hit by an attack while adjacent to one of your Companions.";
            leader5c.StaminaCost = 7;
            leader5c.UpkeepCost = null;
            leader5c.Tier = 5;
            leader5c.TierBenefitDescription = "Pay 20% less for all purchases";
            leader5c.Tree = TalentTree.Leadership;
            leader5c.TreeName = "Leadership";
            leader5c.LinkedSkill = null;
            Talents.Add(leader5c);
            #endregion
            #endregion
            #region Metamagic (Willpower)
            #region T1
            Talent meta1a = new Talent();
            meta1a.Name = "Extend Range";
            meta1a.Type = TalentType.AttackAugment;
            meta1a.Action = ActionType.Quick;
            meta1a.DescriptionFluff = "";
            meta1a.Description = "Increase the range of the Augmented spell by one category. Your Melee spells are now Ranged (30’). Any spell with a range listed in feet is increased by 30’.";
            meta1a.ClarifyingText = "";
            meta1a.StaminaCost = 2;
            meta1a.UpkeepCost = null;
            meta1a.Tier = 1;
            meta1a.TierBenefitDescription = "Reduce the casting cost of spells by 1 (minimum cost of 1)";
            meta1a.Tree = TalentTree.Metamagic;
            meta1a.TreeName = "Metamagic";
            meta1a.LinkedSkill = null;
            Talents.Add(meta1a);

            Talent meta1b = new Talent();
            meta1b.Name = "Enlarge Area";
            meta1b.Type = TalentType.Maneuver;
            meta1b.Action = ActionType.Combat;
            meta1b.DescriptionFluff = "";
            meta1b.Description = "Increase the Area of the Augmented spell by 10’ radius or 50% (if the area is not radius).";
            meta1b.ClarifyingText = "";
            meta1b.StaminaCost = 2;
            meta1b.UpkeepCost = null;
            meta1b.Tier = 1;
            meta1b.TierBenefitDescription = "Reduce the casting cost of spells by 1 (minimum cost of 1)";
            meta1b.Tree = TalentTree.Metamagic;
            meta1b.TreeName = "Metamagic";
            meta1b.LinkedSkill = null;
            Talents.Add(meta1b);

            Talent meta1c = new Talent();
            meta1c.Name = "Conceal";
            meta1c.Type = TalentType.Benefit;
            meta1c.Action = ActionType.None;
            meta1c.DescriptionFluff = "";
            meta1c.Description = "The MCR to notice and identify your spells is increased by 6.";
            meta1c.ClarifyingText = "";
            meta1c.StaminaCost = null;
            meta1c.UpkeepCost = null;
            meta1c.Tier = 1;
            meta1c.TierBenefitDescription = "Reduce the casting cost of spells by 1 (minimum cost of 1)";
            meta1c.Tree = TalentTree.Metamagic;
            meta1c.TreeName = "Metamagic";
            meta1c.LinkedSkill = null;
            Talents.Add(meta1c);
            #endregion
            #region T2
            Talent meta2a = new Talent();
            meta2a.Name = "Bolster Spell";
            meta2a.Type = TalentType.Trick;
            meta2a.Action = ActionType.Quick;
            meta2a.DescriptionFluff = "";
            meta2a.Description = "Increase the numerical benefit of the next Enhancement you cast by 1.";
            meta2a.ClarifyingText = "";
            meta2a.StaminaCost = 3;
            meta2a.UpkeepCost = 1;
            meta2a.Tier = 2;
            meta2a.TierBenefitDescription = "Gain +1 to damage with all spells";
            meta2a.Tree = TalentTree.Metamagic;
            meta2a.TreeName = "Metamagic";
            meta2a.LinkedSkill = null;
            Talents.Add(meta2a);

            Talent meta2b = new Talent();
            meta2b.Name = "Deadly Spell";
            meta2b.Type = TalentType.AttackAugment;
            meta2b.Action = ActionType.Quick;
            meta2b.DescriptionFluff = "";
            meta2b.Description = "The Augmented spell gains the Armor Piercing quality.";
            meta2b.ClarifyingText = "Armor Piercing (AP): Reduce worn/natural Armor Value of targets by ½.";
            meta2b.StaminaCost = 3;
            meta2b.UpkeepCost = null;
            meta2b.Tier = 2;
            meta2b.TierBenefitDescription = "Gain +1 to damage with all spells";
            meta2b.Tree = TalentTree.Metamagic;
            meta2b.TreeName = "Metamagic";
            meta2b.LinkedSkill = null;
            Talents.Add(meta2b);

            Talent meta2c = new Talent();
            meta2c.Name = "Invigorate";
            meta2c.Type = TalentType.Benefit;
            meta2c.Action = ActionType.None;
            meta2c.DescriptionFluff = "";
            meta2c.Description = "Regain 1 additional Stamina per turn while maintaining more than one Enhancement.";
            meta2c.ClarifyingText = "";
            meta2c.StaminaCost = null;
            meta2c.UpkeepCost = null;
            meta2c.Tier = 2;
            meta2c.TierBenefitDescription = "Gain +1 to damage with all spells";
            meta2c.Tree = TalentTree.Metamagic;
            meta2c.TreeName = "Metamagic";
            meta2c.LinkedSkill = null;
            Talents.Add(meta2c);
            #endregion
            #region T3
            Talent meta3a = new Talent();
            meta3a.Name = "Selective";
            meta3a.Type = TalentType.AttackAugment;
            meta3a.Action = ActionType.Quick;
            meta3a.DescriptionFluff = "";
            meta3a.Description = "You can exclude creatures within an area of e ect from being targeted by the Augmented spell. [2 Stamina per creature excluded]";
            meta3a.ClarifyingText = "";
            meta3a.StaminaCost = 2;
            meta3a.UpkeepCost = null;
            meta3a.Tier = 2;
            meta3a.TierBenefitDescription = "Increase your Stamina Pool by your Willpower";
            meta3a.Tree = TalentTree.Metamagic;
            meta3a.TreeName = "Metamagic";
            meta3a.LinkedSkill = null;
            Talents.Add(meta3a);

            Talent meta3b = new Talent();
            meta3b.Name = "Power Swell";
            meta3b.Type = TalentType.Enhancement;
            meta3b.Action = ActionType.Quick;
            meta3b.DescriptionFluff = "";
            meta3b.Description = "While active, you gain the Empowered condition.";
            meta3b.ClarifyingText = "Empowered = +1 atk and +2 dmg with spells, +2 to Presence and Strength linked non-combat skill checks.";
            meta3b.StaminaCost = 8;
            meta3b.UpkeepCost = 2;
            meta3b.Tier = 2;
            meta3b.TierBenefitDescription = "Increase your Stamina Pool by your Willpower";
            meta3b.Tree = TalentTree.Metamagic;
            meta3b.TreeName = "Metamagic";
            meta3b.LinkedSkill = null;
            Talents.Add(meta3b);

            Talent meta3c = new Talent();
            meta3c.Name = "Amplify";
            meta3c.Type = TalentType.AttackAugment;
            meta3c.Action = ActionType.Quick;
            meta3c.DescriptionFluff = "";
            meta3c.Description = "The Augmented spell is treated as rolling a Crit 1 Stage higher than your attack roll would indicate, thus negating a Stage 1 fumble and turning a Stage 2 fumble to a Stage 1fumble, and so forth.";
            meta3c.ClarifyingText = "";
            meta3c.StaminaCost = null;
            meta3c.UpkeepCost = null;
            meta3c.Tier = 2;
            meta3c.TierBenefitDescription = "Increase your Stamina Pool by your Willpower";
            meta3c.Tree = TalentTree.Metamagic;
            meta3c.TreeName = "Metamagic";
            meta3c.LinkedSkill = null;
            Talents.Add(meta3c);
            #endregion
            #region T4
            Talent meta4a = new Talent();
            meta4a.Name = "Seeking Spell";
            meta4a.Type = TalentType.AttackAugment;
            meta4a.Action = ActionType.Quick;
            meta4a.DescriptionFluff = "";
            meta4a.Description = "The Augmented spell gains a +2 to attack.";
            meta4a.ClarifyingText = "";
            meta4a.StaminaCost = 5;
            meta4a.UpkeepCost = null;
            meta4a.Tier = 4;
            meta4a.TierBenefitDescription = "Reduce the cost to maintain spells by 1 (minimum cost of 1)";
            meta4a.Tree = TalentTree.Metamagic;
            meta4a.TreeName = "Metamagic";
            meta4a.LinkedSkill = null;
            Talents.Add(meta4a);

            Talent meta4b = new Talent();
            meta4b.Name = "Explosive Spell";
            meta4b.Type = TalentType.AttackAugment;
            meta4b.Action = ActionType.Quick;
            meta4b.DescriptionFluff = "";
            meta4b.Description = "Turn Ranged or Melee spell into an Area spell. If originally a Melee spell, the Area is centered on you and you can choose to be a ected by it.If originally a Ranged spell, the spell retains its original range. The Area of the spell becomes a 10’ radius.";
            meta4b.ClarifyingText = "";
            meta4b.StaminaCost = 5;
            meta4b.UpkeepCost = null;
            meta4b.Tier = 4;
            meta4b.TierBenefitDescription = "Reduce the cost to maintain spells by 1 (minimum cost of 1)";
            meta4b.Tree = TalentTree.Metamagic;
            meta4b.TreeName = "Metamagic";
            meta4b.LinkedSkill = null;
            Talents.Add(meta4b);

            Talent meta4c = new Talent();
            meta4c.Name = "Trigger";
            meta4c.Type = TalentType.AttackAugment;
            meta4c.Action = ActionType.Quick;
            meta4c.DescriptionFluff = "";
            meta4c.Description = "You cast the Augmented spell but the e ects are delayed. You set a trigger that will activate the e ects as a Triggered Action. The point of origin for the spell is where you were when this Augment was used.If the Triggering event is not met within 10 minutes the Augmented spell fades away with no e ect.";
            meta4c.ClarifyingText = "";
            meta4c.StaminaCost = 0;
            meta4c.UpkeepCost = null;
            meta4c.Tier = 4;
            meta4c.TierBenefitDescription = "Reduce the cost to maintain spells by 1 (minimum cost of 1)";
            meta4c.Tree = TalentTree.Metamagic;
            meta4c.TreeName = "Metamagic";
            meta4c.LinkedSkill = null;
            Talents.Add(meta4c);
            #endregion
            #region T5
            Talent meta5a = new Talent();
            meta5a.Name = "Homing Spell";
            meta5a.Type = TalentType.AttackAugment;
            meta5a.Action = ActionType.Quick;
            meta5a.DescriptionFluff = "";
            meta5a.Description = "Do not roll for attack. The attack hits all targets (but no Crit or Fumble is possible).";
            meta5a.ClarifyingText = "";
            meta5a.StaminaCost = null;
            meta5a.UpkeepCost = null;
            meta5a.Tier = 5;
            meta5a.TierBenefitDescription = "Reduce the cost of Spell Augments and Enhancements by 1 (minimum cost of 1)";
            meta5a.Tree = TalentTree.Metamagic;
            meta5a.TreeName = "Metamagic";
            meta5a.LinkedSkill = null;
            Talents.Add(meta5a);

            Talent meta5b = new Talent();
            meta5b.Name = "Maximize";
            meta5b.Type = TalentType.AttackAugment;
            meta5b.Action = ActionType.Quick;
            meta5b.DescriptionFluff = "";
            meta5b.Description = "Do not roll for damage. The damage roll is equal to 18.";
            meta5b.ClarifyingText = "";
            meta5b.StaminaCost = 6;
            meta5b.UpkeepCost = null;
            meta5b.Tier = 5;
            meta5b.TierBenefitDescription = "Reduce the cost of Spell Augments and Enhancements by 1 (minimum cost of 1)";
            meta5b.Tree = TalentTree.Metamagic;
            meta5b.TreeName = "Metamagic";
            meta5b.LinkedSkill = null;
            Talents.Add(meta5b);

            Talent meta5c = new Talent();
            meta5c.Name = "Quicken";
            meta5c.Type = TalentType.Trick;
            meta5c.Action = ActionType.Quick;
            meta5c.DescriptionFluff = "";
            meta5c.Description = "You cast a spell as part of this Trick. You pay the full cost of this Trick and the Spell cast. Quicken can only be used once a turn.";
            meta5c.ClarifyingText = "";
            meta5c.StaminaCost = 8;
            meta5c.UpkeepCost = null;
            meta5c.Tier = 5;
            meta5c.TierBenefitDescription = "Reduce the cost of Spell Augments and Enhancements by 1 (minimum cost of 1)";
            meta5c.Tree = TalentTree.Metamagic;
            meta5c.TreeName = "Metamagic";
            meta5c.LinkedSkill = null;
            Talents.Add(meta5c);
            #endregion
            #endregion
            #region Quickness (Agility)
            #region T1
            Talent quick1a = new Talent();
            quick1a.Name = "Elusiveness";
            quick1a.Type = TalentType.Benefit;
            quick1a.Action = ActionType.None;
            quick1a.DescriptionFluff = "";
            quick1a.Description = "+1 to Ranged and Area Defenses while in Melee.";
            quick1a.ClarifyingText = "";
            quick1a.StaminaCost = null;
            quick1a.UpkeepCost = null;
            quick1a.Tier = 1;
            quick1a.TierBenefitDescription = "Gain +1 to Speed";
            quick1a.Tree = TalentTree.Quickness;
            quick1a.TreeName = "Quickness";
            quick1a.LinkedSkill = null;
            Talents.Add(quick1a);

            Talent quick1b = new Talent();
            quick1b.Name = "Improved Reflexes";
            quick1b.Type = TalentType.TriggeredAction;
            quick1b.Action = ActionType.Reaction;
            quick1b.DescriptionFluff = "";
            quick1b.Description = "You gain a +2 to the Initiative roll.";
            quick1b.ClarifyingText = "";
            quick1b.StaminaCost = 2;
            quick1b.UpkeepCost = null;
            quick1b.Tier = 1;
            quick1b.TierBenefitDescription = "Gain +1 to Speed";
            quick1b.Tree = TalentTree.Quickness;
            quick1b.TreeName = "Quickness";
            quick1b.LinkedSkill = null;
            Talents.Add(quick1b);

            Talent quick1c = new Talent();
            quick1c.Name = "Flash";
            quick1c.Type = TalentType.Benefit;
            quick1c.Action = ActionType.None;
            quick1c.DescriptionFluff = "";
            quick1c.Description = "Once per round while Sprinting, you can purchase 1 additional MI for 1 Stamina. This MI can exceed the normal maximum.";
            quick1c.ClarifyingText = "";
            quick1c.StaminaCost = null;
            quick1c.UpkeepCost = null;
            quick1c.Tier = 1;
            quick1c.TierBenefitDescription = "Gain +1 to Speed";
            quick1c.Tree = TalentTree.Quickness;
            quick1c.TreeName = "Quickness";
            quick1c.LinkedSkill = null;
            Talents.Add(quick1c);
            #endregion
            #region T2
            Talent quick2a = new Talent();
            quick2a.Name = "Fleet";
            quick2a.Type = TalentType.Stance;
            quick2a.Action = ActionType.Quick;
            quick2a.DescriptionFluff = "";
            quick2a.Description = "You become Hastened.";
            quick2a.ClarifyingText = "";
            quick2a.StaminaCost = 4;
            quick2a.UpkeepCost = 1;
            quick2a.Tier = 2;
            quick2a.TierBenefitDescription = "Reduce the cost of one Triggered Action a turn by 2 Stamina";
            quick2a.Tree = TalentTree.Quickness;
            quick2a.TreeName = "Quickness";
            quick2a.LinkedSkill = null;
            Talents.Add(quick2a);

            Talent quick2b = new Talent();
            quick2b.Name = "Hurdler";
            quick2b.Type = TalentType.Benefit;
            quick2b.Action = ActionType.None;
            quick2b.DescriptionFluff = "";
            quick2b.Description = "Gain +2 to Athletics checks to jump, to overcome rough terrain, and to Scramble. You can stand from Prone for 3 MI.";
            quick2b.ClarifyingText = "";
            quick2b.StaminaCost = null;
            quick2b.UpkeepCost = null;
            quick2b.Tier = 2;
            quick2b.TierBenefitDescription = "Reduce the cost of one Triggered Action a turn by 2 Stamina";
            quick2b.Tree = TalentTree.Quickness;
            quick2b.TreeName = "Quickness";
            quick2b.LinkedSkill = null;
            Talents.Add(quick2b);

            Talent quick2c = new Talent();
            quick2c.Name = "Defensive Flip";
            quick2c.Type = TalentType.TriggeredAction;
            quick2c.Action = ActionType.Reaction;
            quick2c.DescriptionFluff = "";
            quick2c.Description = "You gain a 1d6 -2 to Defense against the Triggering attack.";
            quick2c.ClarifyingText = "";
            quick2c.StaminaCost = 6;
            quick2c.UpkeepCost = null;
            quick2c.Tier = 2;
            quick2c.TierBenefitDescription = "Reduce the cost of one Triggered Action a turn by 2 Stamina";
            quick2c.Tree = TalentTree.Quickness;
            quick2c.TreeName = "Quickness";
            quick2c.LinkedSkill = null;
            Talents.Add(quick2c);
            #endregion
            #region T3
            Talent quick3a = new Talent();
            quick3a.Name = "Improved Sprint";
            quick3a.Type = TalentType.Benefit;
            quick3a.Action = ActionType.None;
            quick3a.DescriptionFluff = "";
            quick3a.Description = "You pay only 1 Stamina per MI while Sprinting.";
            quick3a.ClarifyingText = "";
            quick3a.StaminaCost = null;
            quick3a.UpkeepCost = null;
            quick3a.Tier = 3;
            quick3a.TierBenefitDescription = "Gain +2 to limit of additional MI that can be purchased while Sprinting";
            quick3a.Tree = TalentTree.Quickness;
            quick3a.TreeName = "Quickness";
            quick3a.LinkedSkill = null;
            Talents.Add(quick3a);

            Talent quick3b = new Talent();
            quick3b.Name = "Quicken";
            quick3b.Type = TalentType.Stance;
            quick3b.Action = ActionType.Quick;
            quick3b.DescriptionFluff = "";
            quick3b.Description = "Gain the Hastened and Focused Conditions.";
            quick3b.ClarifyingText = "";
            quick3b.StaminaCost = 8;
            quick3b.UpkeepCost = 2;
            quick3b.Tier = 3;
            quick3b.TierBenefitDescription = "Gain +2 to limit of additional MI that can be purchased while Sprinting";
            quick3b.Tree = TalentTree.Quickness;
            quick3b.TreeName = "Quickness";
            quick3b.LinkedSkill = null;
            Talents.Add(quick3b);

            Talent quick3c = new Talent();
            quick3c.Name = "Battle Dancer";
            quick3c.Type = TalentType.Benefit;
            quick3c.Action = ActionType.None;
            quick3c.DescriptionFluff = "";
            quick3c.Description = "When you render an opponent Unconscious with an attack, you can move 2 MI for free.";
            quick3c.ClarifyingText = "";
            quick3c.StaminaCost = null;
            quick3c.UpkeepCost = null;
            quick3c.Tier = 3;
            quick3c.TierBenefitDescription = "Gain +2 to limit of additional MI that can be purchased while Sprinting";
            quick3c.Tree = TalentTree.Quickness;
            quick3c.TreeName = "Quickness";
            quick3c.LinkedSkill = null;
            Talents.Add(quick3c);
            #endregion
            #region T4
            Talent quick4a = new Talent();
            quick4a.Name = "Blur";
            quick4a.Type = TalentType.Maneuver;
            quick4a.Action = ActionType.Combat;
            quick4a.DescriptionFluff = "";
            quick4a.Description = "You move 3x your Speed in MI and gain Heavy Concealment until the beginning of your next turn.";
            quick4a.ClarifyingText = "";
            quick4a.StaminaCost = 8;
            quick4a.UpkeepCost = null;
            quick4a.Tier = 4;
            quick4a.TierBenefitDescription = "Can use 1 additional Quick Action each turn";
            quick4a.Tree = TalentTree.Quickness;
            quick4a.TreeName = "Quickness";
            quick4a.LinkedSkill = null;
            Talents.Add(quick4a);

            Talent quick4b = new Talent();
            quick4b.Name = "Master Hudler";
            quick4b.Type = TalentType.Benefit;
            quick4b.Action = ActionType.None;
            quick4b.DescriptionFluff = "";
            quick4b.Description = "The Benefit of Hurdler increases to +4. You can stand from Prone for 2 MI. The first Opening you create every round is ignored.";
            quick4b.ClarifyingText = "";
            quick4b.StaminaCost = null;
            quick4b.UpkeepCost = null;
            quick4b.Tier = 4;
            quick4b.TierBenefitDescription = "Can use 1 additional Quick Action each turn";
            quick4b.Tree = TalentTree.Quickness;
            quick4b.TreeName = "Quickness";
            quick4b.LinkedSkill = null;
            Talents.Add(quick4b);

            Talent quick4c = new Talent();
            quick4c.Name = "Tiptoe through the Tulips (TTTT)";
            quick4c.Type = TalentType.Benefit;
            quick4c.Action = ActionType.None;
            quick4c.DescriptionFluff = "";
            quick4c.Description = "Pushing Through no longer costs extra MI. You no longer create an Opening for the first enemy you Push Through in a round.";
            quick4c.ClarifyingText = "";
            quick4c.StaminaCost = null;
            quick4c.UpkeepCost = null;
            quick4c.Tier = 4;
            quick4c.TierBenefitDescription = "Can use 1 additional Quick Action each turn";
            quick4c.Tree = TalentTree.Quickness;
            quick4c.TreeName = "Quickness";
            quick4c.LinkedSkill = null;
            Talents.Add(quick4c);
            #endregion
            #region T5
            Talent quick5a = new Talent();
            quick5a.Name = "Haste";
            quick5a.Type = TalentType.Stance;
            quick5a.Action = ActionType.Quick;
            quick5a.DescriptionFluff = "";
            quick5a.Description = "Gain Hastened and Focused Conditions. You can make one unaugmented basic attack per round as a Quick Action.";
            quick5a.ClarifyingText = "";
            quick5a.StaminaCost = 12;
            quick5a.UpkeepCost = 3;
            quick5a.Tier = 5;
            quick5a.TierBenefitDescription = "Gain +1 to all Defenses if not wearing Heavy or Advanced Armor";
            quick5a.Tree = TalentTree.Quickness;
            quick5a.TreeName = "Quickness";
            quick5a.LinkedSkill = null;
            Talents.Add(quick5a);

            Talent quick5b = new Talent();
            quick5b.Name = "After Image";
            quick5b.Type = TalentType.TriggeredAction;
            quick5b.Action = ActionType.Reaction;
            quick5b.DescriptionFluff = "";
            quick5b.Description = "The attacker must re-roll the attack at a -4. After the attack is resolved, you may move up to 6 MI for free.Movement from this Talent does not create Openings.";
            quick5b.ClarifyingText = "Triggering Action: you are hit by a Melee or Ranged attack";
            quick5b.StaminaCost = 12;
            quick5b.UpkeepCost = null;
            quick5b.Tier = 5;
            quick5b.TierBenefitDescription = "Gain +1 to all Defenses if not wearing Heavy or Advanced Armor";
            quick5b.Tree = TalentTree.Quickness;
            quick5b.TreeName = "Quickness";
            quick5b.LinkedSkill = null;
            Talents.Add(quick5b);

            Talent quick5c = new Talent();
            quick5c.Name = "Lightning Reflexes";
            quick5c.Type = TalentType.TriggeredAction;
            quick5c.Action = ActionType.Reaction;
            quick5c.DescriptionFluff = "";
            quick5c.Description = "You automatically go first.";
            quick5c.ClarifyingText = "Triggering Action: Initiative is rolled";
            quick5c.StaminaCost = 6;
            quick5c.UpkeepCost = null;
            quick5c.Tier = 5;
            quick5c.TierBenefitDescription = "Gain +1 to all Defenses if not wearing Heavy or Advanced Armor";
            quick5c.Tree = TalentTree.Quickness;
            quick5c.TreeName = "Quickness";
            quick5c.LinkedSkill = null;
            Talents.Add(quick5c);
            #endregion
            #endregion
            #region Science! (Focus)
            #region T1
            Talent sci1a = new Talent();
            sci1a.Name = "Salvage";
            sci1a.Type = TalentType.Benefit;
            sci1a.Action = ActionType.None;
            sci1a.DescriptionFluff = "";
            sci1a.Description = "With 1-minute of work, you can salvage 1/10 of an item’s value in materials that can be used for crafting and Mod’ing with Smithing, Electronics, or Mechanics at a later date. These salvaged materials can only be used toward items for which you receive a crafting discount. These materials weigh 1 pound per 1, 000U value.";
            sci1a.ClarifyingText = "";
            sci1a.StaminaCost = null;
            sci1a.UpkeepCost = null;
            sci1a.Tier = 1;
            sci1a.TierBenefitDescription = "Reduce the penalty for not possessing the correct tools when making Trade or Healing Skill Checks to -1.";
            sci1a.Tree = TalentTree.Science;
            sci1a.TreeName = "Science";
            sci1a.LinkedSkill = null;
            Talents.Add(sci1a);

            Talent sci1b = new Talent();
            sci1b.Name = "Jerry-Rig";
            sci1b.Type = TalentType.Maneuver;
            sci1b.Action = ActionType.Combat;
            sci1b.DescriptionFluff = "";
            sci1b.Description = "You cause an inoperable piece of equipment to start working again. It continues to work until the end of the encounter or until it is rendered inoperable again.";
            sci1b.ClarifyingText = "";
            sci1b.StaminaCost = 4;
            sci1b.UpkeepCost = null;
            sci1b.Tier = 1;
            sci1b.TierBenefitDescription = "Reduce the penalty for not possessing the correct tools when making Trade or Healing Skill Checks to -1.";
            sci1b.Tree = TalentTree.Science;
            sci1b.TreeName = "Science";
            sci1b.LinkedSkill = null;
            Talents.Add(sci1b);

            Talent sci1c = new Talent();
            sci1c.Name = "Heavy Bandage";
            sci1c.Type = TalentType.Trick;
            sci1c.Action = ActionType.Quick;
            sci1c.DescriptionFluff = "";
            sci1c.Description = "Use a Med Unit to Heal damage equal to the Grade of the Med Unit to an adjacent creature (1/2 e ect when Healing in the 3rd Track). All Healing must be applied to a single Track.Can only be used to Heal the 2nd and 3rd Tracks once per 24 hours, per target.";
            sci1c.ClarifyingText = "";
            sci1c.StaminaCost = 4;
            sci1c.UpkeepCost = null;
            sci1c.Tier = 1;
            sci1c.TierBenefitDescription = "Reduce the penalty for not possessing the correct tools when making Trade or Healing Skill Checks to -1.";
            sci1c.Tree = TalentTree.Science;
            sci1c.TreeName = "Science";
            sci1c.LinkedSkill = null;
            Talents.Add(sci1c);
            #endregion
            #region T2
            Talent sci2a = new Talent();
            sci2a.Name = "Trauma Experience";
            sci2a.Type = TalentType.Benefit;
            sci2a.Action = ActionType.None;
            sci2a.DescriptionFluff = "";
            sci2a.Description = "Any time you make a Heal Check, do so as if you possess a Grade 1 Med Unit. If you are using a Med Unit of Grade 1 or higher, treat the Med Unit as 1 Grade higher.";
            sci2a.ClarifyingText = "";
            sci2a.StaminaCost = null;
            sci2a.UpkeepCost = null;
            sci2a.Tier = 2;
            sci2a.TierBenefitDescription = "Gain -10% to the cost of Cybernetic Augmentations installed by you or installed in your body.";
            sci2a.Tree = TalentTree.Science;
            sci2a.TreeName = "Science";
            sci2a.LinkedSkill = null;
            Talents.Add(sci2a);

            Talent sci2b = new Talent();
            sci2b.Name = "Scratch Build";
            sci2b.Type = TalentType.Ritual;
            sci2b.Action = ActionType.None;
            sci2b.DescriptionFluff = "";
            sci2b.Description = "Create a piece of Ordnance using the available resources. Make a Demolitions Check. The result x 50 is the value of the Ordnance created.Any Ordnance created with this Ritual is unstable and will become inert (and worthless) if not used within 1 hour. The GM applies a modifier to the demolitions roll from -5 to 0 depending on the resources available(pharmacy or lab: -0, park or forest: -5). The GM has discretion on what options are available for Ordnance created with this Ritual.";
            sci2b.ClarifyingText = "";
            sci2b.StaminaCost = null;
            sci2b.UpkeepCost = null;
            sci2b.FatigueCost = 3;
            sci2b.Tier = 2;
            sci2b.TierBenefitDescription = "Gain -10% to the cost of Cybernetic Augmentations installed by you or installed in your body.";
            sci2b.Tree = TalentTree.Science;
            sci2b.TreeName = "Science";
            sci2b.LinkedSkill = null;
            Talents.Add(sci2b);

            Talent sci2c = new Talent();
            sci2c.Name = "Hacker";
            sci2c.Type = TalentType.Benefit;
            sci2c.Action = ActionType.None;
            sci2c.DescriptionFluff = "";
            sci2c.Description = "You can use the Electronics skill to gather information on targets that is restricted or confidential (financials, military records, alias information).";
            sci2c.ClarifyingText = "";
            sci2c.StaminaCost = null;
            sci2c.UpkeepCost = null;
            sci2c.Tier = 2;
            sci2c.TierBenefitDescription = "Gain -10% to the cost of Cybernetic Augmentations installed by you or installed in your body.";
            sci2c.Tree = TalentTree.Science;
            sci2c.TreeName = "Science";
            sci2c.LinkedSkill = null;
            Talents.Add(sci2c);
            #endregion
            #region T3
            Talent sci3a = new Talent();
            sci3a.Name = "Fine Tune";
            sci3a.Type = TalentType.Ritual;
            sci3a.Action = ActionType.None;
            sci3a.DescriptionFluff = "";
            sci3a.Description = "1 piece of equipment gains either a +2 to damage, a +1 to Accuracy, +1 CM, +1 Armor, +1 Handling or - 1 to Armor Penalty for 24 hours.";
            sci3a.ClarifyingText = "";
            sci3a.StaminaCost = null;
            sci3a.UpkeepCost = null;
            sci3a.FatigueCost = 4;
            sci3a.Tier = 3;
            sci3a.TierBenefitDescription = "Re-roll all 1s when making checks with a single Trade Skill or the Healing skill (chose one).";
            sci3a.Tree = TalentTree.Science;
            sci3a.TreeName = "Science";
            sci3a.LinkedSkill = null;
            Talents.Add(sci3a);

            Talent sci3b = new Talent();
            sci3b.Name = "Combat Cocktail";
            sci3b.Type = TalentType.Maneuver;
            sci3b.Action = ActionType.Combat;
            sci3b.DescriptionFluff = "";
            sci3b.Description = "Use 1 Med Unit of Grade 3 or better to grant an adjacent ally a Positive Condition for 10 minutes.";
            sci3b.ClarifyingText = "";
            sci3b.StaminaCost = 8;
            sci3b.UpkeepCost = null;
            sci3b.Tier = 3;
            sci3b.TierBenefitDescription = "Re-roll all 1s when making checks with a single Trade Skill or the Healing skill (chose one).";
            sci3b.Tree = TalentTree.Science;
            sci3b.TreeName = "Science";
            sci3b.LinkedSkill = null;
            Talents.Add(sci3b);

            Talent sci3c = new Talent();
            sci3c.Name = "Mechanical Servant";
            sci3c.Type = TalentType.Benefit;
            sci3c.Action = ActionType.None;
            sci3c.DescriptionFluff = "";
            sci3c.Description = "You gain a Size 2 Machine Companion of your level minus 1. Use the rules for commanding companions in combat; this Talent can be selected multiple times. The Mechanical Servant gains the Natural Armor(any) and the Mounted Weapon properties as well as one of the following sets: • The Drone gains Flight (1), Night Sight, and Slight(1). • The Hunter - Killer gains Speedy(2), Feral(2), and Lethal(1). • The Brawler gains Brawny(2), Tough(2), and Defender.";
            sci3c.ClarifyingText = "";
            sci3c.StaminaCost = null;
            sci3c.UpkeepCost = null;
            sci3c.Tier = 3;
            sci3c.TierBenefitDescription = "Re-roll all 1s when making checks with a single Trade Skill or the Healing skill (chose one).";
            sci3c.Tree = TalentTree.Science;
            sci3c.TreeName = "Science";
            sci3c.LinkedSkill = null;
            Talents.Add(sci3c);
            #endregion
            #region T4
            Talent sci4a = new Talent();
            sci4a.Name = "System Intrusion";
            sci4a.Type = TalentType.Ritual;
            sci4a.Action = ActionType.None;
            sci4a.DescriptionFluff = "";
            sci4a.Description = "Make an Electronics Check against an accessible system (one you are directly linked to, or is accessible through the Internet). The MCR of the Check is based on the capabilities of the system and modified by the di culty of the task being attempted. If the Check is successful, the system performs the desired task. Only tasks deemed possible by the GM can be performed. The base MCR for systems are as follows: simple system(email server, voice mail server): MCR 15; moderate system (e - commerce site, active blue tooth): MCR 20; secure system (factory or high - rise security system): MCR 25, Impenetrable system (military or bank): MCR 30. The di culty modifiers for various tasks are as follows: simple task(view unrestricted data): -2; moderate task (view restricted data or insert small amounts of data to unrestricted sections): 0; di cult task(view and add information to restricted sections, issue commands to specific system controlled devices like elevators, cameras and doors): +2; extreme task (view alter or delete any information in the system, change passwords, add or remove accounts, shut down system, issue commands to all system controlled devices like elevators, cameras and doors): +4.";
            sci4a.ClarifyingText = "";
            sci4a.StaminaCost = null;
            sci4a.UpkeepCost = null;
            sci4a.FatigueCost = 4;
            sci4a.Tier = 4;
            sci4a.TierBenefitDescription = "Choose 1: Receive an additional 10% discount on items crafted by you (items you received a Trade Skill discount on already); or Add 1 to the regained HP for any Healing effect you create.";
            sci4a.Tree = TalentTree.Science;
            sci4a.TreeName = "Science";
            sci4a.LinkedSkill = null;
            Talents.Add(sci4a);

            Talent sci4b = new Talent();
            sci4b.Name = "Master Crafter";
            sci4b.Type = TalentType.Benefit;
            sci4b.Action = ActionType.None;
            sci4b.DescriptionFluff = "";
            sci4b.Description = "Gain an additional Mod Slot on each item you receive at least a 25% discount on for possessing the appropriate Trade Skill.";
            sci4b.ClarifyingText = "";
            sci4b.StaminaCost = null;
            sci4b.UpkeepCost = null;
            sci4b.Tier = 4;
            sci4b.TierBenefitDescription = "Choose 1: Receive an additional 10% discount on items crafted by you (items you received a Trade Skill discount on already); or Add 1 to the regained HP for any Healing effect you create.";
            sci4b.Tree = TalentTree.Science;
            sci4b.TreeName = "Science";
            sci4b.LinkedSkill = null;
            Talents.Add(sci4b);

            Talent sci4c = new Talent();
            sci4c.Name = "Resucitate";
            sci4c.Type = TalentType.Maneuver;
            sci4c.Action = ActionType.Combat;
            sci4c.DescriptionFluff = "";
            sci4c.Description = "remove all damage from the Bleed Out Track of an adjacent target. The target also regains 1 HP in each track. At the end of the encounter, the target su ers a damage roll equal to the total amount of damage this Maneuver removed. This damage roll ignores armor.";
            sci4c.ClarifyingText = "";
            sci4c.StaminaCost = null;
            sci4c.UpkeepCost = null;
            sci4c.Tier = 4;
            sci4c.TierBenefitDescription = "Choose 1: Receive an additional 10% discount on items crafted by you (items you received a Trade Skill discount on already); or Add 1 to the regained HP for any Healing effect you create.";
            sci4c.Tree = TalentTree.Science;
            sci4c.TreeName = "Science";
            sci4c.LinkedSkill = null;
            Talents.Add(sci4c);
            #endregion
            #region T5
            Talent sci5a = new Talent();
            sci5a.Name = "Custom Item";
            sci5a.Type = TalentType.Benefit;
            sci5a.Action = ActionType.None;
            sci5a.DescriptionFluff = "";
            sci5a.Description = "One item you possess gains 2 additional Mod Slots. These Mod Slots are in addition to whatever slots it had before taking this Talent.Only you gain benefit from these slots. If another creature is using the item, the extra Mods become inert. This Talent can be made to a ect a di erent item with 48 hours’ notice(it would no longer a ect the original item). This Talent can be taken multiple times";
            sci5a.ClarifyingText = "";
            sci5a.StaminaCost = null;
            sci5a.UpkeepCost = null;
            sci5a.Tier = 5;
            sci5a.TierBenefitDescription = "Re-roll 1s when making all Trade and Healing Skill Checks.";
            sci5a.Tree = TalentTree.Science;
            sci5a.TreeName = "Science";
            sci5a.LinkedSkill = null;
            Talents.Add(sci5a);

            Talent sci5b = new Talent();
            sci5b.Name = "Medical Miracle";
            sci5b.Type = TalentType.Ritual;
            sci5b.Action = ActionType.Combat;
            sci5b.DescriptionFluff = "";
            sci5b.Description = "Adjacent target regains HP equal to your total Healing skill (Skill+Attribute). If you decide to expend a Med Unit as part of this Maneuver, the target also regains HP equal to its Grade. This Maneuver can restore HP in the 3rd Track as well as the Bleed Out Track.If this Maneuver is used on a target that died up to 5 minutes ago, that target still regains the listed amount of HP (starting in the Bleed Out Track).";
            sci5b.ClarifyingText = "";
            sci5b.StaminaCost = null;
            sci5b.UpkeepCost = null;
            sci5b.FatigueCost = 10;
            sci5b.Tier = 5;
            sci5b.TierBenefitDescription = "Re-roll 1s when making all Trade and Healing Skill Checks.";
            sci5b.Tree = TalentTree.Science;
            sci5b.TreeName = "Science";
            sci5b.LinkedSkill = null;
            Talents.Add(sci5b);

            Talent sci5c = new Talent();
            sci5c.Name = "Mechanical Ally";
            sci5c.Type = TalentType.Benefit;
            sci5c.Action = ActionType.None;
            sci5c.DescriptionFluff = "";
            sci5c.Description = "You gain a Size 3 or 4 (your choice) Machine Companion of your level. Use the rules for commanding companions in combat; This Talent can be selected multiple times. The Mechanical Servants gains the Natural Armor(any) and the Mounted Weapon properties as well as one of the following sets: • The Drone gains Concealment (Light), Flight(1), Night Sight, and Slight(1). • The Hunter - Killer gains Speedy(2), Quick(2), Feral(2), and Lethal(1). • The Brawler gains Brawny(2), Fortified(any 2), Tough(2), and Defender.";
            sci5c.ClarifyingText = "";
            sci5c.StaminaCost = null;
            sci5c.UpkeepCost = null;
            sci5c.Tier = 5;
            sci5c.TierBenefitDescription = "Re-roll 1s when making all Trade and Healing Skill Checks.";
            sci5c.Tree = TalentTree.Science;
            sci5c.TreeName = "Science";
            sci5c.LinkedSkill = null;
            Talents.Add(sci5c);
            #endregion
            #endregion
            #region Toughness (Fortitude)
            #region T1
            Talent tough1a = new Talent();
            tough1a.Name = "Indomitable";
            tough1a.Type = TalentType.Benefit;
            tough1a.Action = ActionType.None;
            tough1a.DescriptionFluff = "";
            tough1a.Description = "Reduce the Wound penalties to your 2nd Track by 1.";
            tough1a.ClarifyingText = "";
            tough1a.StaminaCost = null;
            tough1a.UpkeepCost = null;
            tough1a.Tier = 1;
            tough1a.TierBenefitDescription = "Add your Fortitude to your 1st Damage Track.";
            tough1a.Tree = TalentTree.Toughness;
            tough1a.TreeName = "Toughness";
            tough1a.LinkedSkill = null;
            Talents.Add(tough1a);

            Talent tough1b = new Talent();
            tough1b.Name = "Iron Heart";
            tough1b.Type = TalentType.Benefit;
            tough1b.Action = ActionType.None;
            tough1b.DescriptionFluff = "";
            tough1b.Description = "Gain +1 to Body Defense and +1 Durability vs. attacks targeting Body defenses.";
            tough1b.ClarifyingText = "";
            tough1b.StaminaCost = null;
            tough1b.UpkeepCost = null;
            tough1b.Tier = 1;
            tough1b.TierBenefitDescription = "Add your Fortitude to your 1st Damage Track.";
            tough1b.Tree = TalentTree.Toughness;
            tough1b.TreeName = "Toughness";
            tough1b.LinkedSkill = null;
            Talents.Add(tough1b);

            Talent tough1c = new Talent();
            tough1c.Name = "Tough as Nails";
            tough1c.Type = TalentType.Benefit;
            tough1c.Action = ActionType.None;
            tough1c.DescriptionFluff = "";
            tough1c.Description = "Gain +1 to Resistance and regain 1 HP in your 1st Track at the end of each of your turns.";
            tough1c.ClarifyingText = "";
            tough1c.StaminaCost = null;
            tough1c.UpkeepCost = null;
            tough1c.Tier = 1;
            tough1c.TierBenefitDescription = "Add your Fortitude to your 1st Damage Track.";
            tough1c.Tree = TalentTree.Toughness;
            tough1c.TreeName = "Toughness";
            tough1c.LinkedSkill = null;
            Talents.Add(tough1c);
            #endregion
            #region T2
            Talent tough2a = new Talent();
            tough2a.Name = "Second Wind";
            tough2a.Type = TalentType.Trick;
            tough2a.Action = ActionType.Quick;
            tough2a.DescriptionFluff = "";
            tough2a.Description = "Regain up to your Fortitude worth of lost HP in the 1st Track. [2 Stamina per HP regained]";
            tough2a.ClarifyingText = "";
            tough2a.StaminaCost = 2;
            tough2a.UpkeepCost = null;
            tough2a.Tier = 2;
            tough2a.TierBenefitDescription = "Gain +1 to Durability.";
            tough2a.Tree = TalentTree.Toughness;
            tough2a.TreeName = "Toughness";
            tough2a.LinkedSkill = null;
            Talents.Add(tough2a);

            Talent tough2b = new Talent();
            tough2b.Name = "Mountain Defense";
            tough2b.Type = TalentType.Stance;
            tough2b.Action = ActionType.Quick;
            tough2b.DescriptionFluff = "";
            tough2b.Description = "While active, the user gains a +2 to Durability and cannot be Knocked Back or Knocked Down.";
            tough2b.ClarifyingText = "";
            tough2b.StaminaCost = 6;
            tough2b.UpkeepCost = 2;
            tough2b.Tier = 2;
            tough2b.TierBenefitDescription = "Gain +1 to Durability.";
            tough2b.Tree = TalentTree.Toughness;
            tough2b.TreeName = "Toughness";
            tough2b.LinkedSkill = null;
            Talents.Add(tough2b);

            Talent tough2c = new Talent();
            tough2c.Name = "Brace for Impact";
            tough2c.Type = TalentType.TriggeredAction;
            tough2c.Action = ActionType.Reaction;
            tough2c.DescriptionFluff = "";
            tough2c.Description = "Roll 1d6, and reduce the damage taken from the Triggering attack by the result.";
            tough2c.ClarifyingText = "Triggering Action: you take damage from an attack";
            tough2c.StaminaCost = 3;
            tough2c.UpkeepCost = null;
            tough2c.Tier = 2;
            tough2c.TierBenefitDescription = "Gain +1 to Durability.";
            tough2c.Tree = TalentTree.Toughness;
            tough2c.TreeName = "Toughness";
            tough2c.LinkedSkill = null;
            Talents.Add(tough2c);
            #endregion
            #region T3
            Talent fort3a = new Talent();
            fort3a.Name = "Unstoppable";
            fort3a.Type = TalentType.Stance;
            fort3a.Action = ActionType.Quick;
            fort3a.DescriptionFluff = "";
            fort3a.Description = "While active, you ignore negative Conditions (except Unconscious).";
            fort3a.ClarifyingText = "";
            fort3a.StaminaCost = 8;
            fort3a.UpkeepCost = 2;
            fort3a.Tier = 3;
            fort3a.TierBenefitDescription = "Gain +2 to Long-Term Recovery.";
            fort3a.Tree = TalentTree.Toughness;
            fort3a.TreeName = "Toughness";
            fort3a.LinkedSkill = null;
            Talents.Add(fort3a);

            Talent fort3b = new Talent();
            fort3b.Name = "Improved Indomitability";
            fort3b.Type = TalentType.TriggeredAction;
            fort3b.Action = ActionType.Reaction;
            fort3b.DescriptionFluff = "";
            fort3b.Description = "Reduce the Stage of the Triggering Crit by 1.";
            fort3b.ClarifyingText = "Triggering Action: a Crit is scored against you.";
            fort3b.StaminaCost = 4;
            fort3b.UpkeepCost = null;
            fort3b.Tier = 3;
            fort3b.TierBenefitDescription = "Gain +2 to Long-Term Recovery.";
            fort3b.Tree = TalentTree.Toughness;
            fort3b.TreeName = "Toughness";
            fort3b.LinkedSkill = null;
            Talents.Add(fort3b);

            Talent fort3c = new Talent();
            fort3c.Name = "Tough as Iron";
            fort3c.Type = TalentType.Benefit;
            fort3c.Action = ActionType.None;
            fort3c.DescriptionFluff = "";
            fort3c.Description = "-1 to CM of attacks against you.";
            fort3c.ClarifyingText = "";
            fort3c.StaminaCost = null;
            fort3c.UpkeepCost = null;
            fort3c.Tier = 3;
            fort3c.TierBenefitDescription = "Gain +2 to Long-Term Recovery.";
            fort3c.Tree = TalentTree.Toughness;
            fort3c.TreeName = "Toughness";
            fort3c.LinkedSkill = null;
            Talents.Add(fort3c);
            #endregion
            #region T4
            Talent tough4a = new Talent();
            tough4a.Name = "Steelheart";
            tough4a.Type = TalentType.TriggeredAction;
            tough4a.Action = ActionType.Reaction;
            tough4a.DescriptionFluff = "";
            tough4a.Description = "Increase your Body Defense by 1d6+1 versus the Triggering attack.";
            tough4a.ClarifyingText = "Triggering Action: an attack hits your Body Defense.";
            tough4a.StaminaCost = 5;
            tough4a.UpkeepCost = null;
            tough4a.Tier = 4;
            tough4a.TierBenefitDescription = "Gain +2 to Resistance.";
            tough4a.Tree = TalentTree.Toughness;
            tough4a.TreeName = "Toughness";
            tough4a.LinkedSkill = null;
            Talents.Add(tough4a);

            Talent tough4b = new Talent();
            tough4b.Name = "Dig Deep";
            tough4b.Type = TalentType.Maneuver;
            tough4b.Action = ActionType.Combat;
            tough4b.DescriptionFluff = "";
            tough4b.Description = "Your Stamina pool is filled to current maximum. You lose 1d6 HP in the 2nd Damage Track.";
            tough4b.ClarifyingText = "";
            tough4b.StaminaCost = 0;
            tough4b.UpkeepCost = null;
            tough4b.Tier = 4;
            tough4b.TierBenefitDescription = "Gain +2 to Resistance.";
            tough4b.Tree = TalentTree.Toughness;
            tough4b.TreeName = "Toughness";
            tough4b.LinkedSkill = null;
            Talents.Add(tough4b);

            Talent tough4c = new Talent();
            tough4c.Name = "Unrelenting";
            tough4c.Type = TalentType.Stance;
            tough4c.Action = ActionType.Quick;
            tough4c.DescriptionFluff = "";
            tough4c.Description = "You gain the Girded Condition, ignore the e ects of Negative Conditions (except Unconscious), and gain a +2 to Resistance Checks.";
            tough4c.ClarifyingText = "";
            tough4c.StaminaCost = 10;
            tough4c.UpkeepCost = 3;
            tough4c.Tier = 4;
            tough4c.TierBenefitDescription = "Gain +2 to Resistance.";
            tough4c.Tree = TalentTree.Toughness;
            tough4c.TreeName = "Toughness";
            tough4c.LinkedSkill = null;
            Talents.Add(tough4c);
            #endregion
            #region T5
            Talent tough5a = new Talent();
            tough5a.Name = "Diamond Defense";
            tough5a.Type = TalentType.Trick;
            tough5a.Action = ActionType.Quick;
            tough5a.DescriptionFluff = "";
            tough5a.Description = "You gain the Girded Condition and Fortification against all damage for 1 round.";
            tough5a.ClarifyingText = "";
            tough5a.StaminaCost = 6;
            tough5a.UpkeepCost = null;
            tough5a.Tier = 5;
            tough5a.TierBenefitDescription = "Reduce wound penalties by 2. If you have the Indomitalbe Talent, gain an additional +1 to danage and Durability when you would otherwise be suffering wound penalties.";
            tough5a.Tree = TalentTree.Toughness;
            tough5a.TreeName = "Toughness";
            tough5a.LinkedSkill = null;
            Talents.Add(tough5a);

            Talent tough5b = new Talent();
            tough5b.Name = "Tough as Steel";
            tough5b.Type = TalentType.Benefit;
            tough5b.Action = ActionType.None;
            tough5b.DescriptionFluff = "";
            tough5b.Description = "+2 to Durability.";
            tough5b.ClarifyingText = "";
            tough5b.StaminaCost = null;
            tough5b.UpkeepCost = null;
            tough5b.Tier = 5;
            tough5b.TierBenefitDescription = "Reduce wound penalties by 2. If you have the Indomitalbe Talent, gain an additional +1 to danage and Durability when you would otherwise be suffering wound penalties.";
            tough5b.Tree = TalentTree.Toughness;
            tough5b.TreeName = "Toughness";
            tough5b.LinkedSkill = null;
            Talents.Add(tough5b);

            Talent tough5c = new Talent();
            tough5c.Name = "Undying";
            tough5c.Type = TalentType.Stance;
            tough5c.Action = ActionType.Quick;
            tough5c.DescriptionFluff = "";
            tough5c.Description = "You become immune to the e ects of the Unconscious Condition. Whenever you would be Unconscious you are instead Weakened and Vulnerable.You lose 2 HP at the end of any turn that you would have been Unconscious.If you die while this Stance is active, you are still dead.";
            tough5c.ClarifyingText = "";
            tough5c.StaminaCost = 12;
            tough5c.UpkeepCost = 3;
            tough5c.Tier = 5;
            tough5c.TierBenefitDescription = "Reduce wound penalties by 2. If you have the Indomitalbe Talent, gain an additional +1 to danage and Durability when you would otherwise be suffering wound penalties.";
            tough5c.Tree = TalentTree.Toughness;
            tough5c.TreeName = "Toughness";
            tough5c.LinkedSkill = null;
            Talents.Add(tough5c);
            #endregion
            #endregion
        }

        public void GenerateRandomEnemy()
        {
            NPCQuickReferenceVM enemy = new NPCQuickReferenceVM();
            //Archetype
            Archetype ra = (Archetype)r.Next(0, 6);

            //Level
            int l1 = r.Next(1, 11);
            int l2 = r.Next(1, 11);
            List<int> level = new List<int>();
            level.Add(l1);
            level.Add(l2);
            NpcClass rc = (NpcClass)r.Next(1, 4); //Exclude companions (0 enum value)
            NpcType rt = (NpcType)r.Next(0, 8);
            enemy.model = new NonPlayerCharacter(ra, level.Min(), rc, GetRandomSize(), rt);
            enemy.model.Qualities = new List<NpcQuality>();

            AddArchetypeToEnemy(enemy, ra);

            //Add type qualities
            #region
            switch (rt)
            {
                case NpcType.Flesh_aka_Unliving:
                    {
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);

                        NpcQuality susceptibleFire = new NpcQuality();
                        susceptibleFire.Name = "Susceptible (Fire)";
                        susceptibleFire.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleFire);
                        NpcQuality susceptibleAcid = new NpcQuality();
                        susceptibleAcid.Name = "Susceptible (Acid)";
                        susceptibleAcid.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleAcid);

                        AttributeModifier primeMeleeBody = new AttributeModifier();
                        primeMeleeBody.AttributeName = "MeleeBody";
                        primeMeleeBody.ModifierValue = 2;
                        primeMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                        AttributeModifier primeAreaBody = new AttributeModifier();
                        primeAreaBody.AttributeName = "AreaBody";
                        primeAreaBody.ModifierValue = 2;
                        primeAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                        AttributeModifier primeRangedBody = new AttributeModifier();
                        primeRangedBody.AttributeName = "RangedBody";
                        primeRangedBody.ModifierValue = 2;
                        primeRangedBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);

                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserRangedResolve = new AttributeModifier();
                        lesserRangedResolve.AttributeName = "RangedResolve";
                        lesserRangedResolve.ModifierValue = -2;
                        lesserRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Secondary Crit Effects)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        AttributeModifier hp2 = new AttributeModifier();
                        hp2.AttributeName = "HealthPoints";
                        hp2.ModifierValue = 2;
                        hp2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(hp2);
                        break;
                    }
                case NpcType.Plant:
                    {
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedPoison = new NpcQuality();
                        fortifiedPoison.Name = "Fortified (Poison)";
                        fortifiedPoison.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPoison);

                        NpcQuality susceptibleFire = new NpcQuality();
                        susceptibleFire.Name = "Susceptible (Fire)";
                        susceptibleFire.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleFire);
                        NpcQuality susceptibleSlashing = new NpcQuality();
                        susceptibleSlashing.Name = "Susceptible (Slashing)";
                        susceptibleSlashing.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleSlashing);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserMeleeBody = new AttributeModifier();
                        lesserMeleeBody.AttributeName = "MeleeBody";
                        lesserMeleeBody.ModifierValue = -2;
                        lesserMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);

                        enemy.model.Attributes.Special = "-1 CM against Ranged Attacks";
                        break;
                    }
                case NpcType.Fluid:
                    {
                        NpcQuality fortifiedPhysical = new NpcQuality();
                        fortifiedPhysical.Name = "Fortified (Physical)";
                        fortifiedPhysical.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPhysical);

                        NpcQuality susceptibleCold = new NpcQuality();
                        susceptibleCold.Name = "Susceptible (Cold)";
                        susceptibleCold.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleCold);

                        AttributeModifier primeMeleePhysical = new AttributeModifier();
                        primeMeleePhysical.AttributeName = "MeleePhysical";
                        primeMeleePhysical.ModifierValue = 2;
                        primeMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                        AttributeModifier primeAreaPhysical = new AttributeModifier();
                        primeAreaPhysical.AttributeName = "AreaPhysical";
                        primeAreaPhysical.ModifierValue = 2;
                        primeAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                        AttributeModifier primeRangedPhysical = new AttributeModifier();
                        primeRangedPhysical.AttributeName = "RangedPhysical";
                        primeRangedPhysical.ModifierValue = 2;
                        primeRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);

                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserAreaBody = new AttributeModifier();
                        lesserAreaBody.AttributeName = "AreaBody";
                        lesserAreaBody.ModifierValue = -2;
                        lesserAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);

                        NpcQuality immunePiercing = new NpcQuality();
                        immunePiercing.Name = "Immune (Piercing)";
                        immunePiercing.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immunePiercing);

                        enemy.model.Attributes.Special = "+5 to Athletics checks to escape";
                        break;
                    }
                case NpcType.Swarm:
                    {
                        NpcQuality fortifiedMelee = new NpcQuality();
                        fortifiedMelee.Name = "Fortified (Melee)";
                        fortifiedMelee.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedMelee);
                        NpcQuality fortifiedRanged = new NpcQuality();
                        fortifiedRanged.Name = "Fortified (Ranged)";
                        fortifiedRanged.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedRanged);

                        NpcQuality susceptibleArea = new NpcQuality();
                        susceptibleArea.Name = "Susceptible (Area)";
                        susceptibleArea.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleArea);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Mind Control)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "+5 to defense on incoming Social Skill Attacks";
                        break;
                    }
                case NpcType.Machine:
                    {
                        NpcQuality fortifiedFire = new NpcQuality();
                        fortifiedFire.Name = "Fortified (Fire)";
                        fortifiedFire.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedFire);
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedForce = new NpcQuality();
                        fortifiedForce.Name = "Fortified (Force)";
                        fortifiedForce.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedForce);

                        NpcQuality susceptibleAcid = new NpcQuality();
                        susceptibleAcid.Name = "Susceptible (Acid)";
                        susceptibleAcid.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleAcid);
                        NpcQuality susceptibleElectricity = new NpcQuality();
                        susceptibleElectricity.Name = "Susceptible (Electricity)";
                        susceptibleElectricity.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleElectricity);

                        AttributeModifier primeMeleeResolve = new AttributeModifier();
                        primeMeleeResolve.AttributeName = "MeleeResolve";
                        primeMeleeResolve.ModifierValue = 2;
                        primeMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                        AttributeModifier primeAreaResolve = new AttributeModifier();
                        primeAreaResolve.AttributeName = "AreaResolve";
                        primeAreaResolve.ModifierValue = 2;
                        primeAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                        AttributeModifier primeRangedResolve = new AttributeModifier();
                        primeRangedResolve.AttributeName = "RangedResolve";
                        primeRangedResolve.ModifierValue = 2;
                        primeRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Mind Control)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);
                        NpcQuality immune2 = new NpcQuality();
                        immune2.Name = "Immune (Social Skill Attacks)";
                        immune2.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune2);

                        enemy.model.Attributes.Special = "-1 CM to incoming reaction attacks";
                        break;
                    }
                case NpcType.Energy:
                    {
                        NpcQuality fortifiedReactionAttacks = new NpcQuality();
                        fortifiedReactionAttacks.Name = "Fortified (Reaction Attacks)";
                        fortifiedReactionAttacks.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedReactionAttacks);

                        if (r.NextDouble() < .5)
                        {
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);

                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }
                        else
                        {
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);

                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);

                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        }

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Same Energy Type)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "All attacks gain damage type matching energy type";
                        break;
                    }
                case NpcType.Solid:
                    {
                        NpcQuality fortifiedPiercing = new NpcQuality();
                        fortifiedPiercing.Name = "Fortified (Piercing)";
                        fortifiedPiercing.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPiercing);
                        NpcQuality fortifiedBallistic = new NpcQuality();
                        fortifiedBallistic.Name = "Fortified (Ballistic)";
                        fortifiedBallistic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedBallistic);
                        NpcQuality fortifiedSlashing = new NpcQuality();
                        fortifiedSlashing.Name = "Fortified (Slashing)";
                        fortifiedSlashing.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedSlashing);
                        NpcQuality fortifiedPsycic = new NpcQuality();
                        fortifiedPsycic.Name = "Fortified (Psycic)";
                        fortifiedPsycic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPsycic);
                        NpcQuality fortifiedUnholy = new NpcQuality();
                        fortifiedUnholy.Name = "Fortified (Unholy)";
                        fortifiedUnholy.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedUnholy);
                        NpcQuality fortifiedHoly = new NpcQuality();
                        fortifiedHoly.Name = "Fortified (Holy)";
                        fortifiedHoly.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedHoly);
                        NpcQuality fortifiedFire = new NpcQuality();
                        fortifiedFire.Name = "Fortified (Fire)";
                        fortifiedFire.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedFire);
                        NpcQuality fortifiedAcid = new NpcQuality();
                        fortifiedAcid.Name = "Fortified (Acid)";
                        fortifiedAcid.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedAcid);
                        NpcQuality fortifiedElectricity = new NpcQuality();
                        fortifiedElectricity.Name = "Fortified (Electricity)";
                        fortifiedElectricity.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedElectricity);
                        NpcQuality fortifiedCold = new NpcQuality();
                        fortifiedCold.Name = "Fortified (Cold)";
                        fortifiedCold.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedCold);
                        NpcQuality fortifiedNecrotic = new NpcQuality();
                        fortifiedNecrotic.Name = "Fortified (Necrotic)";
                        fortifiedNecrotic.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedNecrotic);
                        NpcQuality fortifiedPoison = new NpcQuality();
                        fortifiedPoison.Name = "Fortified (Poison)";
                        fortifiedPoison.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(fortifiedPoison);

                        AttributeModifier lesserMeleePhysical = new AttributeModifier();
                        lesserMeleePhysical.AttributeName = "MeleePhysical";
                        lesserMeleePhysical.ModifierValue = -2;
                        lesserMeleePhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                        AttributeModifier lesserMeleeResolve = new AttributeModifier();
                        lesserMeleeResolve.AttributeName = "MeleeResolve";
                        lesserMeleeResolve.ModifierValue = -2;
                        lesserMeleeResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                        AttributeModifier lesserMeleeBody = new AttributeModifier();
                        lesserMeleeBody.AttributeName = "MeleeBody";
                        lesserMeleeBody.ModifierValue = -2;
                        lesserMeleeBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                        AttributeModifier lesserAreaPhysical = new AttributeModifier();
                        lesserAreaPhysical.AttributeName = "AreaPhysical";
                        lesserAreaPhysical.ModifierValue = -2;
                        lesserAreaPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                        AttributeModifier lesserAreaResolve = new AttributeModifier();
                        lesserAreaResolve.AttributeName = "AreaResolve";
                        lesserAreaResolve.ModifierValue = -2;
                        lesserAreaResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                        AttributeModifier lesserAreaBody = new AttributeModifier();
                        lesserAreaBody.AttributeName = "AreaBody";
                        lesserAreaBody.ModifierValue = -2;
                        lesserAreaBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                        AttributeModifier lesserRangedPhysical = new AttributeModifier();
                        lesserRangedPhysical.AttributeName = "RangedPhysical";
                        lesserRangedPhysical.ModifierValue = -2;
                        lesserRangedPhysical.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                        AttributeModifier lesserRangedResolve = new AttributeModifier();
                        lesserRangedResolve.AttributeName = "RangedResolve";
                        lesserRangedResolve.ModifierValue = -2;
                        lesserRangedResolve.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        AttributeModifier lesserRangedBody = new AttributeModifier();
                        lesserRangedBody.AttributeName = "RangedBody";
                        lesserRangedBody.ModifierValue = -2;
                        lesserRangedBody.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);

                        NpcQuality susceptibleBludgeoning = new NpcQuality();
                        susceptibleBludgeoning.Name = "Susceptible (Bludgeoning)";
                        susceptibleBludgeoning.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleBludgeoning);
                        NpcQuality susceptibleForce = new NpcQuality();
                        susceptibleForce.Name = "Susceptible (Force)";
                        susceptibleForce.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                        enemy.model.Qualities.Add(susceptibleForce);

                        NpcQuality immune = new NpcQuality();
                        immune.Name = "Immune (Secondary Crit Effects)";
                        immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                        enemy.model.Qualities.Add(immune);

                        enemy.model.Attributes.Special = "-1 CM on incoming attacks";

                        AttributeModifier durability2 = new AttributeModifier();
                        durability2.AttributeName = "Durability";
                        durability2.ModifierValue = 2;
                        durability2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(durability2);

                        AttributeModifier speed2 = new AttributeModifier();
                        speed2.AttributeName = "Speed";
                        speed2.ModifierValue = -2;
                        speed2.Type = ModifierType.Additive;
                        enemy.model.Attributes.AttributeAdjustments.Add(speed2);
                        break;
                    }
                case NpcType.Natural:
                    {
                        int randPrimeDef = r.Next(0, 6);
                        if (randPrimeDef == 0)
                        {
                            // Melee
                            AttributeModifier primeMeleePhysical = new AttributeModifier();
                            primeMeleePhysical.AttributeName = "MeleePhysical";
                            primeMeleePhysical.ModifierValue = 2;
                            primeMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                        }
                        else if (randPrimeDef == 1)
                        {
                            // Area
                            AttributeModifier primeAreaPhysical = new AttributeModifier();
                            primeAreaPhysical.AttributeName = "AreaPhysical";
                            primeAreaPhysical.ModifierValue = 2;
                            primeAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                        }
                        else if (randPrimeDef == 2)
                        {
                            //Ranged
                            AttributeModifier primeRangedPhysical = new AttributeModifier();
                            primeRangedPhysical.AttributeName = "RangedPhysical";
                            primeRangedPhysical.ModifierValue = 2;
                            primeRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);
                        }
                        else if (randPrimeDef == 3)
                        {
                            //Physical
                            AttributeModifier primeMeleePhysical = new AttributeModifier();
                            primeMeleePhysical.AttributeName = "MeleePhysical";
                            primeMeleePhysical.ModifierValue = 2;
                            primeMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleePhysical);
                            AttributeModifier primeAreaPhysical = new AttributeModifier();
                            primeAreaPhysical.AttributeName = "AreaPhysical";
                            primeAreaPhysical.ModifierValue = 2;
                            primeAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaPhysical);
                            AttributeModifier primeRangedPhysical = new AttributeModifier();
                            primeRangedPhysical.AttributeName = "RangedPhysical";
                            primeRangedPhysical.ModifierValue = 2;
                            primeRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedPhysical);
                        }
                        else if (randPrimeDef == 4)
                        {
                            //Resolve
                            AttributeModifier primeMeleeResolve = new AttributeModifier();
                            primeMeleeResolve.AttributeName = "MeleeResolve";
                            primeMeleeResolve.ModifierValue = 2;
                            primeMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeResolve);
                            AttributeModifier primeAreaResolve = new AttributeModifier();
                            primeAreaResolve.AttributeName = "AreaResolve";
                            primeAreaResolve.ModifierValue = 2;
                            primeAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaResolve);
                            AttributeModifier primeRangedResolve = new AttributeModifier();
                            primeRangedResolve.AttributeName = "RangedResolve";
                            primeRangedResolve.ModifierValue = 2;
                            primeRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedResolve);
                        }
                        else
                        {
                            //Body
                            AttributeModifier primeMeleeBody = new AttributeModifier();
                            primeMeleeBody.AttributeName = "MeleeBody";
                            primeMeleeBody.ModifierValue = 2;
                            primeMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeMeleeBody);
                            AttributeModifier primeAreaBody = new AttributeModifier();
                            primeAreaBody.AttributeName = "AreaBody";
                            primeAreaBody.ModifierValue = 2;
                            primeAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeAreaBody);
                            AttributeModifier primeRangedBody = new AttributeModifier();
                            primeRangedBody.AttributeName = "RangedBody";
                            primeRangedBody.ModifierValue = 2;
                            primeRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(primeRangedBody);
                        }

                        int randLesserDef = r.Next(0, 6);
                        if (randLesserDef == 0)
                        {
                            // Melee
                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                        }
                        else if (randLesserDef == 1)
                        {
                            // Area
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                        }
                        else if (randLesserDef == 2)
                        {
                            //Ranged
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }
                        else if (randLesserDef == 3)
                        {
                            //Physical
                            AttributeModifier lesserMeleePhysical = new AttributeModifier();
                            lesserMeleePhysical.AttributeName = "MeleePhysical";
                            lesserMeleePhysical.ModifierValue = -2;
                            lesserMeleePhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleePhysical);
                            AttributeModifier lesserAreaPhysical = new AttributeModifier();
                            lesserAreaPhysical.AttributeName = "AreaPhysical";
                            lesserAreaPhysical.ModifierValue = -2;
                            lesserAreaPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaPhysical);
                            AttributeModifier lesserRangedPhysical = new AttributeModifier();
                            lesserRangedPhysical.AttributeName = "RangedPhysical";
                            lesserRangedPhysical.ModifierValue = -2;
                            lesserRangedPhysical.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedPhysical);
                        }
                        else if (randLesserDef == 4)
                        {
                            //Resolve
                            AttributeModifier lesserMeleeResolve = new AttributeModifier();
                            lesserMeleeResolve.AttributeName = "MeleeResolve";
                            lesserMeleeResolve.ModifierValue = -2;
                            lesserMeleeResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeResolve);
                            AttributeModifier lesserAreaResolve = new AttributeModifier();
                            lesserAreaResolve.AttributeName = "AreaResolve";
                            lesserAreaResolve.ModifierValue = -2;
                            lesserAreaResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaResolve);
                            AttributeModifier lesserRangedResolve = new AttributeModifier();
                            lesserRangedResolve.AttributeName = "RangedResolve";
                            lesserRangedResolve.ModifierValue = -2;
                            lesserRangedResolve.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedResolve);
                        }
                        else
                        {
                            //Body
                            AttributeModifier lesserMeleeBody = new AttributeModifier();
                            lesserMeleeBody.AttributeName = "MeleeBody";
                            lesserMeleeBody.ModifierValue = -2;
                            lesserMeleeBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserMeleeBody);
                            AttributeModifier lesserAreaBody = new AttributeModifier();
                            lesserAreaBody.AttributeName = "AreaBody";
                            lesserAreaBody.ModifierValue = -2;
                            lesserAreaBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserAreaBody);
                            AttributeModifier lesserRangedBody = new AttributeModifier();
                            lesserRangedBody.AttributeName = "RangedBody";
                            lesserRangedBody.ModifierValue = -2;
                            lesserRangedBody.Type = ModifierType.Additive;
                            enemy.model.Attributes.AttributeAdjustments.Add(lesserRangedBody);
                        }

                        AttributeModifier skill1 = new AttributeModifier();
                        skill1.AttributeName = "PrimaryAttack";
                        skill1.Type = ModifierType.Additive;
                        skill1.ModifierValue = 1;
                        enemy.model.Attributes.AttributeAdjustments.Add(skill1);

                        AttributeModifier skill2 = new AttributeModifier();
                        skill2.AttributeName = "SecondaryAttack";
                        skill2.Type = ModifierType.Additive;
                        skill2.ModifierValue = 1;
                        enemy.model.Attributes.AttributeAdjustments.Add(skill2);

                        enemy.model.Attributes.Special = "+1 to all skills";

                        AddRandomQuality(enemy);
                        break;
                    }
            }
            #endregion

            int numberOfRandomAbilities = 0;
            int numberOfRandomQualities = 0;
#pragma warning disable CS0219 // The variable 'numberOfRandomTalents' is assigned but its value is never used
            int numberOfRandomTalents = 0;
#pragma warning restore CS0219 // The variable 'numberOfRandomTalents' is assigned but its value is never used

            switch (rc)
            {
                case NpcClass.Foe:
                    numberOfRandomAbilities = 1;
                    numberOfRandomQualities = 1;
                    numberOfRandomTalents = 1;
                    break;
                case NpcClass.Grunt:
                    if (r.Next(0, 2) == 0)
                    {
                        numberOfRandomAbilities = 2;
                    }
                    else
                    {
                        numberOfRandomAbilities = 1;
                        numberOfRandomQualities = 1;
                    }
                    numberOfRandomTalents = 1;
                    break;
                case NpcClass.Antagonist:
                    if (r.Next(0, 2) == 0)
                    {
                        numberOfRandomAbilities = 2;
                        numberOfRandomQualities = 1;
                    }
                    else
                    {
                        numberOfRandomAbilities = 1;
                        numberOfRandomQualities = 3;
                    }
                    numberOfRandomTalents = 2;
                    break;
            }

            //Add random qualities
            for (int i = 0; i < numberOfRandomQualities; i++)
            {
                AddRandomQuality(enemy);
            }
            //Add random abilities
            for (int i = 0; i < numberOfRandomAbilities; i++)
            {
                AddRandomAbility(enemy);
            }
            //Add random talents
            for (int i = 0; i < numberOfRandomAbilities; i++)
            {
                AddRandomTalent(enemy);
            }

            Enemies.Add(enemy);
        }

        public void AddArchetypeToEnemy(NPCQuickReferenceVM enemy, Archetype ra)
        {
            if (!enemy.model.Archetype.HasFlag(ra))
            {
                //Archetype freebies
                switch (ra)
                {
                    case Archetype.Beast:
                        AddQualityByName(enemy, "Animalistic");
                        enemy.model.SpecializedSkills.Add(new KeyValuePair<SpecializedSkill, int>(SpecializedSkill.Stealth, enemy.model.Level + 5));
                        break;
                    case Archetype.Risen:
                        AddQualityByName(enemy, "Undead");
                        enemy.model.SpecializedSkills.Add(new KeyValuePair<SpecializedSkill, int>(SpecializedSkill.Intimidation, enemy.model.Level + 5));
                        break;
                    case Archetype.Demonic:
                        AddQualityByName(enemy, "Infernal");
                        enemy.model.SpecializedSkills.Add(new KeyValuePair<SpecializedSkill, int>(SpecializedSkill.Deception, enemy.model.Level + 5));
                        break;
                    case Archetype.Flying_aka_Dragonkin:
                        NpcQuality flight = new NpcQuality();
                        flight.Name = "Flight (1 or 1/2)";
                        flight.Description = "Gains flight as a movement mode.  The listed number is the quantity of MI needed to purchase a single MI of flight.";
                        enemy.model.Qualities.Add(flight);
                        enemy.model.SpecializedSkills.Add(new KeyValuePair<SpecializedSkill, int>(SpecializedSkill.Survival, enemy.model.Level + 5));
                        break;
                    case Archetype.Elemental:
                        AddQualityByName(enemy, "Blindsight");
                        enemy.model.SpecializedSkills.Add(new KeyValuePair<SpecializedSkill, int>(SpecializedSkill.Athletics, enemy.model.Level + 5));
                        break;
                    case Archetype.Humanoid:
                        AddRandomQuality(enemy);
                        break;
                }
            }
        }

        private void AddRandomAbility(NPCQuickReferenceVM enemy)
        {
            int index = r.Next(0, CreatureAbilities.Count);
            AddAbilityByIndex(enemy, index);
        }

        private void AddAbilityByIndex(NPCQuickReferenceVM enemy, int index)
        {
            var ability = CreatureAbilities[index];
            enemy.model.Abilities.Add(ability);
            if (ability.Name == "Talent Ability")
            {
                AddRandomTalent(enemy);
            }
            else if (ability.GrantedAttack != null)
            {
                enemy.model.Attacks.Add(ability.GrantedAttack);
                RandomizeGrantedAttack(ability);
            }
            enemy.NotifyAll();
        }

        private void RandomizeGrantedAttack(NpcAbility ability)
        {
            if (ability.GrantedAttack is NpcWeaponAttack && ability.Name.Contains("Range"))
            {
                //Swap out the granted attack
                NpcWeaponAttack grantedAttack2 = new NpcWeaponAttack();
                grantedAttack2.Weapon = new NaturalWeapon(NaturalWeaponClass.Ranged);
                grantedAttack2.Weapon.Type = (DamageType)Math.Pow(2, r.Next(0, 14));
                switch (r.Next(0, 7))
                {
                    case 0:
                        grantedAttack2.Weapon.Range = Range.Pistol;
                        break;
                    case 1:
                        grantedAttack2.Weapon.Range = Range.Rifle;
                        break;
                    case 2:
                        grantedAttack2.Weapon.Range = Range.Shotgun;
                        break;
                    case 3:
                        grantedAttack2.Weapon.Range = Range.SMG;
                        break;
                    case 4:
                        grantedAttack2.Weapon.Range = Range.HeavyRifle;
                        break;
                    case 5:
                        grantedAttack2.Weapon.Range = Range.Thrown;
                        break;
                    case 6:
                        grantedAttack2.Weapon.Range = Range.Bows;
                        break;
                }
                grantedAttack2.Name = "Natural Ranged Attack Ability";
                ability.GrantedAttack = grantedAttack2;
            }
        }

        public void AddAbilityByName(NPCQuickReferenceVM enemy, string abilityName)
        {
            int index = CreatureAbilities.IndexOf(CreatureAbilities.First(q => q.Name == abilityName));
            AddAbilityByIndex(enemy, index);
        }

        public void AddRandomTalent(NPCQuickReferenceVM enemy)
        {
            try
            {
                int index = r.Next(0, Talents.Count);
                AddTalentByIndex(enemy, index);
            }
            catch (ArgumentOutOfRangeException)
            {
                AddRandomTalent(enemy);
            }
        }

        public void AddTalentByIndex(NPCQuickReferenceVM enemy, int index)
        {
            Talent t = Talents[index];
            int maxTier = 0;
            switch (enemy.model.NpcClass)
            {
                case NpcClass.Foe:
                    maxTier = 2 + decimal.ToInt32(Math.Floor(enemy.model.Level / 3M));
                    break;
                case NpcClass.Grunt:
                    maxTier = 1 + decimal.ToInt32(Math.Floor(enemy.model.Level / 3M));
                    break;
                case NpcClass.Antagonist:
                    maxTier = 3 + decimal.ToInt32(Math.Floor(enemy.model.Level / 3M));
                    break;
            }
            //Check legality
            if (t.Tier > maxTier)
            {
                throw new ArgumentOutOfRangeException("Talent Tier Too High.");
            }
            else
            {
                enemy.model.Talents.Add(t);
            }
        }

        private void AddTalentByTierAndTree(NPCQuickReferenceVM enemy, int v, TalentTree tree)
        {

        }

        public void Load()
        {
            try
            {
                enemies.Clear();
                List<string> fileNames = Directory.EnumerateFiles(".", "*.dat").ToList();
                foreach (string file in fileNames)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(file, FileMode.Open))
                        {
                            List<Type> extraTypes = GetExtraTypes();
                            XmlSerializer xs = new XmlSerializer(typeof(NPCQuickReferenceVM), extraTypes.ToArray());
                            NPCQuickReferenceVM e = xs.Deserialize(fs) as NPCQuickReferenceVM;
                            if (enemies == null)
                            {
                                enemies = new ObservableCollection<ViewModel.NPCQuickReferenceVM>();
                            }
                            enemies.Add(e);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public void Save()
        {
            //Save each enemy as its own file.
            foreach (var e in enemies)
            {
                using (FileStream fs = new FileStream(string.Format("Enemy-{0}.dat", e.Name), FileMode.Create))
                {
                    List<Type> extraTypes = GetExtraTypes();
                    XmlSerializer xs = new XmlSerializer(typeof(NPCQuickReferenceVM), extraTypes.ToArray());
                    xs.Serialize(fs, e);
                    fs.Flush();
                }
            }
        }

        private static List<Type> GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(NpcAttack));
            extraTypes.Add(typeof(NpcWeaponAttack));
            extraTypes.Add(typeof(NpcAmpAttack));
            extraTypes.Add(typeof(NaturalWeapon));
            extraTypes.Add(typeof(NaturalArmor));

            return extraTypes;
        }

        public void AddRandomQuality(NPCQuickReferenceVM enemy)
        {
            int index = r.Next(0, CreatureQualities.Count);
            AddQualityByIndex(enemy, index);
        }

        private void AddQualityByIndex(NPCQuickReferenceVM enemy, int index)
        {
            enemy.model.Qualities.Add(CreatureQualities[index]);
            if (CreatureQualities[index].Modifiers.Count > 0)
            {
                enemy.model.Attributes.AttributeAdjustments.AddRange(CreatureQualities[index].Modifiers);
            }
            if (CreatureQualities[index].GrantedWeapons.Count > 0)
            {
                foreach (var w in CreatureQualities[index].GrantedWeapons)
                {
                    NpcWeaponAttack ma = new NpcWeaponAttack();
                    ma.Name = w.Name;
                    ma.Weapon = w;
                    enemy.model.Attacks.Add(ma);
                }
            }
            if (CreatureQualities[index].GrantedArmors.Count > 0)
            {
                foreach (var a in CreatureQualities[index].GrantedArmors)
                {
                    enemy.model.Armor = a;//last armor wins? i guess
                }
            }
            enemy.NotifyAll();
        }

        public void AddQualityByName(NPCQuickReferenceVM enemy, string QualityName)
        {
            int index = CreatureQualities.IndexOf(CreatureQualities.First(q => q.Name == QualityName));
            AddQualityByIndex(enemy, index);
        }

        private int GetRandomSize()
        {
            //Should be heavily weighted to 3 in range of 1-10.
            int size = 3;
            while (true)
            {
                if (size == 1 || size == 10)
                {
                    return size;
                }
                if (r.NextDouble() < .8)
                {
                    return size;
                }
                else
                {
                    if (r.NextDouble() < .7)
                    {
                        size++;
                    }
                    else
                    {
                        size--;
                    }
                }
            }
        }

        private void InitializeCreatureQualities()
        {
            #region Animalistic
            {
                foreach (NaturalArmorClass na in Enum.GetValues(typeof(NaturalArmorClass)))
                {
                    foreach (NaturalWeaponClass nw in Enum.GetValues(typeof(NaturalWeaponClass)))
                    {
                        NpcQuality animalistic = new NpcQuality();
                        animalistic.Name = "Animalistic (" + nw.ToString() + "/" + na.ToString() + ")";
                        animalistic.Description = "Gains a Natural Weapon(Any) and Natural Armor(Any), +1 to CM of Melee attacks, and + 1 to Defense, Attack, Damage, and Durability.  Companions with the Animalistic Quality start with no UEU, Equipment, Gear, or Augmentations.";

                        NaturalWeaponVM weapon1 = new ViewModel.NaturalWeaponVM(nw);
                        weapon1.Name = "Natural Weapon (" + nw.ToString() + ")";
                        weapon1.Type = GetRandomPhysicalDamageType();
                        weapon1.Range = Range.Melee;
                        weapon1.Properties = WeaponProperty.None;
                        weapon1.CM = 3;
                        animalistic.GrantedWeapons.Add(weapon1.model);

                        NaturalArmorVM armor1 = new NaturalArmorVM(na);
                        animalistic.GrantedArmors.Add(armor1.model);

                        AttributeModifier animalisticMod0 = new AttributeModifier();
                        animalisticMod0.AttributeName = "CM";
                        animalisticMod0.Type = ModifierType.Additive;
                        animalisticMod0.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod0);

                        ModifyDefenses(animalistic, 1);

                        AttributeModifier animalisticMod10 = new AttributeModifier();
                        animalisticMod10.AttributeName = "PrimaryAttack";
                        animalisticMod10.Type = ModifierType.Additive;
                        animalisticMod10.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod10);

                        AttributeModifier animalisticMod11 = new AttributeModifier();
                        animalisticMod11.AttributeName = "SecondaryAttack";
                        animalisticMod11.Type = ModifierType.Additive;
                        animalisticMod11.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod11);

                        AttributeModifier animalisticMod12 = new AttributeModifier();
                        animalisticMod12.AttributeName = "PrimaryAttackDamage";
                        animalisticMod12.Type = ModifierType.Additive;
                        animalisticMod12.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod12);

                        AttributeModifier animalisticMod13 = new AttributeModifier();
                        animalisticMod13.AttributeName = "SecondaryAttackDamage";
                        animalisticMod13.Type = ModifierType.Additive;
                        animalisticMod13.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod13);

                        AttributeModifier animalisticMod14 = new AttributeModifier();
                        animalisticMod14.AttributeName = "Durability";
                        animalisticMod14.Type = ModifierType.Additive;
                        animalisticMod14.ModifierValue = 1;
                        animalistic.Modifiers.Add(animalisticMod14);

                        CreatureQualities.Add(animalistic);
                    }
                }
            }
            #endregion
            #region Battering
            {
                NpcQuality battering = new NpcQuality();
                battering.Name = "Battering (Primary)";
                battering.Description = "All Crits hits from the listed attack have Knockback. (Can take this Quality more than once applying it to a different attack.)";
                battering.xVariable = 0; //NOTE: 0 = primary attack, 1 = secondsary, 2 = tertiary... etc.
                CreatureQualities.Add(battering);
            }
            #endregion
            #region Blindsight
            {
                NpcQuality blindsight = new NpcQuality();
                blindsight.Name = "Blindsight";
                blindsight.Description = "The creature does not suffer penalties from Concealment.";
                CreatureQualities.Add(blindsight);
            }
            #endregion
            #region Brawny (2)
            {
                NpcQuality brawny2 = new NpcQuality();
                brawny2.Name = "Brawny (2)";
                brawny2.Description = "Reduce the creature’s attacks and defenses by the listed amount, increase its Durability and damage by double the listed amount.";
                brawny2.xVariable = 2;

                AttributeModifier brawny2Mod0 = new AttributeModifier();
                brawny2Mod0.AttributeName = "PrimaryAttack";
                brawny2Mod0.Type = ModifierType.Additive;
                brawny2Mod0.ModifierValue = -brawny2.xVariable;
                brawny2.Modifiers.Add(brawny2Mod0);

                AttributeModifier brawny2Mod1 = new AttributeModifier();
                brawny2Mod1.AttributeName = "SecondaryAttack";
                brawny2Mod1.Type = ModifierType.Additive;
                brawny2Mod1.ModifierValue = -brawny2.xVariable;
                brawny2.Modifiers.Add(brawny2Mod1);

                ModifyDefenses(brawny2, -brawny2.xVariable);

                AttributeModifier brawny2Mod2 = new AttributeModifier();
                brawny2Mod2.AttributeName = "Durability";
                brawny2Mod2.Type = ModifierType.Additive;
                brawny2Mod2.ModifierValue = 2 * brawny2.xVariable;
                brawny2.Modifiers.Add(brawny2Mod2);

                AttributeModifier brawny2Mod3 = new AttributeModifier();
                brawny2Mod3.AttributeName = "PrimaryAttackDamage";
                brawny2Mod3.Type = ModifierType.Additive;
                brawny2Mod3.ModifierValue = 2 * brawny2.xVariable;
                brawny2.Modifiers.Add(brawny2Mod3);

                AttributeModifier brawny2Mod4 = new AttributeModifier();
                brawny2Mod4.AttributeName = "SecondaryAttackDamage";
                brawny2Mod4.Type = ModifierType.Additive;
                brawny2Mod4.ModifierValue = 2 * brawny2.xVariable;
                brawny2.Modifiers.Add(brawny2Mod4);
                CreatureQualities.Add(brawny2);
            }
            #endregion
            #region Camouflaged (2)
            {
                NpcQuality camouflaged2 = new NpcQuality();
                camouflaged2.Name = "Camouflaged (2)";
                camouflaged2.Description = "The creature has the listed amount of Concealment at all times. If an exception exists it is listed in the property description.";
                camouflaged2.xVariable = 2;
                CreatureQualities.Add(camouflaged2);
            }
            #endregion
            #region Energy Infused
            {
                NpcQuality energyInfused = new NpcQuality();
                energyInfused.Name = "Energy Infused";
                energyInfused.Description = "Gains an Energy subtype (listed in the Energy Type).  Gains immunity to all damage of the listed type, and all Melee attacks gain the damage type of the subtype.";
                CreatureQualities.Add(energyInfused);
            }
            #endregion
            #region Evasive (M/2)
            {
                NpcQuality evasiveM2 = new NpcQuality();
                evasiveM2.Name = "Evasive (M/2)";
                evasiveM2.Description = "Increase the creature’s Melee Defense by the listed amount. (Can take this Quality more than once applying it to a different Defense each time.)";
                evasiveM2.xVariable = 2;

                AttributeModifier evasiveMod0 = new AttributeModifier();
                evasiveMod0.AttributeName = "MeleePhysical";
                evasiveMod0.Type = ModifierType.Additive;
                evasiveMod0.ModifierValue = evasiveM2.xVariable;
                evasiveM2.Modifiers.Add(evasiveMod0);

                AttributeModifier evasiveMod1 = new AttributeModifier();
                evasiveMod1.AttributeName = "MeleeResolve";
                evasiveMod1.Type = ModifierType.Additive;
                evasiveMod1.ModifierValue = evasiveM2.xVariable;
                evasiveM2.Modifiers.Add(evasiveMod1);

                AttributeModifier evasiveMod2 = new AttributeModifier();
                evasiveMod2.AttributeName = "MeleeBody";
                evasiveMod2.Type = ModifierType.Additive;
                evasiveMod2.ModifierValue = evasiveM2.xVariable;
                evasiveM2.Modifiers.Add(evasiveMod2);
                CreatureQualities.Add(evasiveM2);
            }
            #endregion
            #region Evasive (A/2)
            {
                NpcQuality evasiveA2 = new NpcQuality();
                evasiveA2.Name = "Evasive (A/2)";
                evasiveA2.Description = "Increase the creature’s Area Defense by the listed amount. (Can take this Quality more than once applying it to a different Defense each time.)";
                evasiveA2.xVariable = 2;

                AttributeModifier evasiveMod0 = new AttributeModifier();
                evasiveMod0.AttributeName = "AreaPhysical";
                evasiveMod0.Type = ModifierType.Additive;
                evasiveMod0.ModifierValue = evasiveA2.xVariable;
                evasiveA2.Modifiers.Add(evasiveMod0);

                AttributeModifier evasiveMod1 = new AttributeModifier();
                evasiveMod1.AttributeName = "AreaResolve";
                evasiveMod1.Type = ModifierType.Additive;
                evasiveMod1.ModifierValue = evasiveA2.xVariable;
                evasiveA2.Modifiers.Add(evasiveMod1);

                AttributeModifier evasiveMod2 = new AttributeModifier();
                evasiveMod2.AttributeName = "AreaBody";
                evasiveMod2.Type = ModifierType.Additive;
                evasiveMod2.ModifierValue = evasiveA2.xVariable;
                evasiveA2.Modifiers.Add(evasiveMod2);
                CreatureQualities.Add(evasiveA2);
            }
            #endregion
            #region Evasive (R/2)
            {
                NpcQuality evasiveR2 = new NpcQuality();
                evasiveR2.Name = "Evasive (R/2)";
                evasiveR2.Description = "Increase the creature’s Ranged Defense by the listed amount. (Can take this Quality more than once applying it to a different Defense each time.)";
                evasiveR2.xVariable = 2;

                AttributeModifier evasiveMod0 = new AttributeModifier();
                evasiveMod0.AttributeName = "RangedPhysical";
                evasiveMod0.Type = ModifierType.Additive;
                evasiveMod0.ModifierValue = evasiveR2.xVariable;
                evasiveR2.Modifiers.Add(evasiveMod0);

                AttributeModifier evasiveMod1 = new AttributeModifier();
                evasiveMod1.AttributeName = "RangedResolve";
                evasiveMod1.Type = ModifierType.Additive;
                evasiveMod1.ModifierValue = evasiveR2.xVariable;
                evasiveR2.Modifiers.Add(evasiveMod1);

                AttributeModifier evasiveMod2 = new AttributeModifier();
                evasiveMod2.AttributeName = "RangedBody";
                evasiveMod2.Type = ModifierType.Additive;
                evasiveMod2.ModifierValue = evasiveR2.xVariable;
                evasiveR2.Modifiers.Add(evasiveMod2);
                CreatureQualities.Add(evasiveR2);
            }
            #endregion
            #region FastRecovery(2)
            {
                NpcQuality fastRecovery2 = new NpcQuality();
                fastRecovery2.Name = "Fast Recovery (2)";
                fastRecovery2.Description = "Increase the creature’s Stamina Regen by the listed amount.";
                fastRecovery2.xVariable = 2;

                AttributeModifier fastRecovery2Mod0 = new AttributeModifier();
                fastRecovery2Mod0.AttributeName = "StaminaRegen";
                fastRecovery2Mod0.Type = ModifierType.Additive;
                fastRecovery2Mod0.ModifierValue = fastRecovery2.xVariable;
                fastRecovery2.Modifiers.Add(fastRecovery2Mod0);
                CreatureQualities.Add(fastRecovery2);
            }
            #endregion
            #region Feral (2)
            {
                NpcQuality feral2 = new NpcQuality();
                feral2.Name = "Feral (2)";
                feral2.Description = "Increase the creature’s Melee CM by the listed amount.";
                feral2.xVariable = 2;

                AttributeModifier feral2Mod0 = new AttributeModifier();
                feral2Mod0.AttributeName = "CM";
                feral2Mod0.Type = ModifierType.Additive;
                feral2Mod0.ModifierValue = feral2.xVariable;
                feral2.Modifiers.Add(feral2Mod0);
                CreatureQualities.Add(feral2);
            }
            #endregion
            #region Flight (2)
            {
                NpcQuality flight2 = new NpcQuality();
                flight2.Name = "Flight (2)";
                flight2.Description = "Gains flight as a movement mode.  The listed number is the quantity of MI needed to purchase a single MI of flight.  Assign this variable with caution; should be tied to creature Size.";
                flight2.xVariable = 2;
                CreatureQualities.Add(flight2);
            }
            #endregion
            #region Forceful (2)
            {
                NpcQuality forceful2 = new NpcQuality();
                forceful2.Name = "Forceful (2)";
                forceful2.Description = "Increase the creature’s Area CM by the listed amount.";
                forceful2.xVariable = 2;

                AttributeModifier forceful2Mod0 = new AttributeModifier();
                forceful2Mod0.AttributeName = "CM";
                forceful2Mod0.Type = ModifierType.Additive;
                forceful2Mod0.ModifierValue = forceful2.xVariable;
                forceful2.Modifiers.Add(forceful2Mod0);
                CreatureQualities.Add(forceful2);
            }
            #endregion
            #region Fortified (DamageType)
            {
                NpcQuality fortified = new NpcQuality();
                fortified.Name = "Fortified (DamageType)";
                fortified.Description = "Gains a +4 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                CreatureQualities.Add(fortified);
            }
            #endregion
            #region Graceful (2)
            {
                NpcQuality graceful2 = new NpcQuality();
                graceful2.Name = "Graceful (2)";
                graceful2.Description = "The creature can treat the listed number of MI of rough terrain as it was normal terrain.";
                CreatureQualities.Add(graceful2);
            }
            #endregion
            #region Hardened (2)
            {
                NpcQuality hardened2 = new NpcQuality();
                hardened2.Name = "Hardened (2)";
                hardened2.Description = "The creature reduces the CM of incoming attacks by the listed amount.";
                CreatureQualities.Add(hardened2);
            }
            #endregion
            #region Heavenly
            {
                NpcQuality heavenly = new NpcQuality();
                heavenly.Name = "Heavenly";
                heavenly.Description = "Gains the Holy property (Holy damage does -4 damage to and has - 1 CM vs. Holy targets, Unholy damange has +2 CM vs. Holy targets). Fortified vs. Force and Lightning.";
                CreatureQualities.Add(heavenly);
            }
            #endregion
            #region Heavy Fortified (DamageType)
            {
                NpcQuality heavyFortified = new NpcQuality();
                heavyFortified.Name = "Heavy Fortified (DamageType)";
                heavyFortified.Description = "Gains a +8 to Durability against the listed damage type. (Can take this Quality more than once applying it to a different damage type each time.)";
                CreatureQualities.Add(heavyFortified);
            }
            #endregion
            #region Highly Susceptible (DamageType)
            {
                NpcQuality highlySusceptible = new NpcQuality();
                highlySusceptible.Name = "Highly Susceptible (DamageType)";
                highlySusceptible.Description = "Suffers a +4 damage and + 2 CM from damage of the listed type.  (Can take this Quality more than once  applying it to a different damage type each  time.) Taking this Quality gives the creature 1 free selection of the Heavy Fortified Quality.";
                CreatureQualities.Add(highlySusceptible);
            }
            #endregion
            #region Honed (2)
            {
                NpcQuality honed2 = new NpcQuality();
                honed2.Name = "Honed (2)";
                honed2.Description = "Increase the creature’s Ranged CM by the listed amount.";
                honed2.xVariable = 2;

                AttributeModifier honed2Mod0 = new AttributeModifier();
                honed2Mod0.AttributeName = "CM";
                honed2Mod0.Type = ModifierType.Additive;
                honed2Mod0.ModifierValue = honed2.xVariable;
                honed2.Modifiers.Add(honed2Mod0);
                CreatureQualities.Add(honed2);
            }
            #endregion
            #region Immune (DamageType)
            {
                NpcQuality immune = new NpcQuality();
                immune.Name = "Immune (DamageType)";
                immune.Description = "The creature is immune to the listed damage type. If an attack is of multiple damage types the final damage is divided by the total number of damage types and then multiplied by the number of damage types the creature is not immune to. The result is applied as normal health loss. (Can take this Quality more than once applying it to a different damage type each time.)";
                CreatureQualities.Add(immune);
            }
            #endregion
            #region Incorporeal [Energy Type only]
            {
                NpcQuality incorporeal = new NpcQuality();
                incorporeal.Name = "Incorporeal [Energy Type only]";
                incorporeal.Description = "Cannot use Weapons, Armor, Gear or Augmentations. Immune to Piercing, Slashing, Bludgeoning, and Ballistic damage, and Susceptible to all damage that does not match its Energy Type.";
                CreatureQualities.Add(incorporeal);
            }
            #endregion
            #region Infernal
            {
                NpcQuality infernal = new NpcQuality();
                infernal.Name = "Infernal";
                infernal.Description = "Gains the Unholy property (Unholy damage does -4 damage to and -1 CM vs. Unholy targets, +2 CM vs. Holy targets.), Fortified vs. Poison and Fire.";
                CreatureQualities.Add(infernal);
            }
            #endregion
            #region Leader (Creature Type)
            {
                NpcQuality leader = new NpcQuality();
                leader.Name = "Leader (Creature Type)";
                leader.Description = "Creatures of the listed Type gain a +1 to attack and a +2 to damage while within 5’ per Level of the creature with this ability. (Can take this Quality more than once applying it to a different Type.)";
                CreatureQualities.Add(leader);
            }
            #endregion
            #region Lethal (1)
            {
                NpcQuality lethal = new NpcQuality();
                lethal.Name = "Lethal (1)";
                lethal.Description = "The primary attack gains Lethal at the listed amount. (Can take this Quality more than once, applying it to a different attack each time.)";
                CreatureQualities.Add(lethal);
            }
            #endregion
            #region Mounted Weapon
            {
                NpcQuality mountedWeapon = new NpcQuality();
                mountedWeapon.Name = "Mounted Weapon";
                mountedWeapon.Description = "The creature has a weapon permanently attached to its body. The weapon has a maximum size equal to the creatures x 2 and is always considered armed. In the case of weapons that require ammunition, double the base capacity of the weapon.";
                CreatureQualities.Add(mountedWeapon);
            }
            #endregion
            #region Multi-Legged
            {
                NpcQuality multiLegged = new NpcQuality();
                multiLegged.Name = "Multi-Legged";
                multiLegged.Description = "Gains +2 to Speed, +4 to resist Knockback, and + 4 to Athletics checks.";

                AttributeModifier multiLeggedMod0 = new AttributeModifier();
                multiLeggedMod0.AttributeName = "Speed";
                multiLeggedMod0.Type = ModifierType.Additive;
                multiLeggedMod0.ModifierValue = 2;
                multiLegged.Modifiers.Add(multiLeggedMod0);
                CreatureQualities.Add(multiLegged);
            }
            #endregion
            #region Multi-Limbed (2)
            {
                NpcQuality multiLimbed = new NpcQuality();
                multiLimbed.Name = "Multi-Limbed (2)";
                multiLimbed.Description = "The Multi-Limbed creature can be armed with (2) additional weapons (all of which must be either Light or Medium). When the Multi-Limbed creature uses the Two Weapon fighting Attack Augment it can make a Free Action attack with all secondary weapons that it is armed with at normal penalties. All armed weapons must be of the same base type to benefit from the matched weapon penalty reduction.";
                CreatureQualities.Add(multiLimbed);
            }
            #endregion
            #region Natural Armor
            {
                foreach (NaturalArmorClass na in Enum.GetValues(typeof(NaturalArmorClass)))
                {
                    NpcQuality naturalArmor = new NpcQuality();
                    naturalArmor.Name = "Natural Armor (" + na.ToString() + ")";
                    naturalArmor.Description = "The creature gains a Natural Armor of the listed type.";

                    NaturalArmorVM naturalArmor1 = new NaturalArmorVM(na);
                    naturalArmor.GrantedArmors.Add(naturalArmor1.model);
                    CreatureQualities.Add(naturalArmor);
                }
            }
            #endregion
            #region Natural Weapon
            {
                foreach (NaturalWeaponClass nw in Enum.GetValues(typeof(NaturalWeaponClass)))
                {
                    NpcQuality naturalWeapon = new NpcQuality();
                    naturalWeapon.Name = "Natural Weapon (" + nw.ToString() + ")";
                    naturalWeapon.Description = "The creature gains Natural Weapon of the listed type.";

                    NaturalWeaponVM naturalWeapon1 = new ViewModel.NaturalWeaponVM(nw);
                    naturalWeapon1.Name = "Natural Weapon (" + nw.ToString() + ")";
                    naturalWeapon1.CM = 3;
                    naturalWeapon1.Type = GetRandomPhysicalDamageType();
                    naturalWeapon1.Skill = Model.WeaponSkill.Unarmed;
                    naturalWeapon.GrantedWeapons.Add(naturalWeapon1.model);
                    CreatureQualities.Add(naturalWeapon);
                }
            }
            #endregion
            #region Night Sight
            {
                NpcQuality nightSight = new NpcQuality();
                nightSight.Name = "Night Sight";
                nightSight.Description = "Nightsight: The creature does not suffer penalties light or heavy Concealment from darkness.";
                CreatureQualities.Add(nightSight);
            }
            #endregion
            #region Power Reserve (2)
            {
                NpcQuality powerReserve = new NpcQuality();
                powerReserve.Name = "Power Reserve (2)";
                powerReserve.Description = "Increase the creature’s total Stamina by 4x the listed amount.";
                powerReserve.xVariable = 2;

                AttributeModifier powerReserveMod0 = new AttributeModifier();
                powerReserveMod0.AttributeName = "StaminaPool";
                powerReserveMod0.Type = ModifierType.Additive;
                powerReserveMod0.ModifierValue = 4 * powerReserve.xVariable;
                powerReserve.Modifiers.Add(powerReserveMod0);
                CreatureQualities.Add(powerReserve);
            }
            #endregion
            #region Powerful (2)
            {
                NpcQuality powerful2 = new NpcQuality();
                powerful2.Name = "Powerful (2)";
                powerful2.Description = "Increase the creature’s primary attack’s damage by 2x the listed amount. (Can take this Quality more than once applying it to a different attack each time.)";
                powerful2.xVariable = 2;

                AttributeModifier powerful2Mod0 = new AttributeModifier();
                powerful2Mod0.AttributeName = "PrimaryAttackDamage";
                powerful2Mod0.Type = ModifierType.Additive;
                powerful2Mod0.ModifierValue = 2 * powerful2.xVariable;
                powerful2.Modifiers.Add(powerful2Mod0);
                CreatureQualities.Add(powerful2);
            }
            #endregion
            #region Pushy (2)
            {
                NpcQuality pushy2 = new NpcQuality();
                pushy2.Name = "Pushy (2)";
                pushy2.Description = "The creature adds 5x the listed amount to any Knockback effect for the purposes of determining if the target creature is knocked back, and if so, how far.";
                pushy2.xVariable = 2;
                CreatureQualities.Add(pushy2);
            }
            #endregion
            #region Quick (2)
            {
                NpcQuality quick2 = new NpcQuality();
                quick2.Name = "Quick (2)";
                quick2.Description = "Increase the creature’s Initiative by 2x the listed amount.";
                quick2.xVariable = 2;

                AttributeModifier quick2Mod0 = new AttributeModifier();
                quick2Mod0.AttributeName = "Initiative";
                quick2Mod0.Type = ModifierType.Additive;
                quick2Mod0.ModifierValue = 2 * quick2.xVariable;
                quick2.Modifiers.Add(quick2Mod0);
                CreatureQualities.Add(quick2);
            }
            #endregion
            #region Regeneration (2)
            {
                NpcQuality regeneration2 = new NpcQuality();
                regeneration2.Name = "Regeneration (2)";
                regeneration2.Description = "The number listed is the amount of Health that is restored in the following tracks and timeframes. Per Turn: 1st Track, Per Minute: 2nd Track, and Per Hour: 3rd Track. Regeneration restores health in the first track until full, then it restores health in the second until full, and finally it restores health in the third track until full. Regeneration cannot restore more health in a given track than the creature’s normal maximum.";
                regeneration2.xVariable = 2;
                CreatureQualities.Add(regeneration2);
            }
            #endregion
            #region Resilient (2)
            {
                NpcQuality resilient2 = new NpcQuality();
                resilient2.Name = "Resilient (2)";
                resilient2.Description = "Increase the creature’s Resistance by the listed amount.";
                resilient2.xVariable = 2;

                AttributeModifier resilient2Mod0 = new AttributeModifier();
                resilient2Mod0.AttributeName = "Resistance";
                resilient2Mod0.Type = ModifierType.Additive;
                resilient2Mod0.ModifierValue = resilient2.xVariable;
                resilient2.Modifiers.Add(resilient2Mod0);
                CreatureQualities.Add(resilient2);
            }
            #endregion
            #region Skilled (2)
            {
                NpcQuality skilled = new NpcQuality();
                skilled.Name = "Skilled (2)";
                skilled.Description = "The creature adds the listed amount to all non-attack skill checks that it makes.";
                skilled.xVariable = 2;
                CreatureQualities.Add(skilled);
            }
            #endregion
            #region Slight (2)
            {
                NpcQuality slight2 = new NpcQuality();
                slight2.Name = "Slight (2)";
                slight2.Description = "Increase the creature’s attack bonus and defenses by listed amount, decrease damage and Durability by double the listed amount.";
                slight2.xVariable = 2;

                AttributeModifier slight2Mod0 = new AttributeModifier();
                slight2Mod0.AttributeName = "PrimaryAttack";
                slight2Mod0.Type = ModifierType.Additive;
                slight2Mod0.ModifierValue = slight2.xVariable;
                slight2.Modifiers.Add(slight2Mod0);

                AttributeModifier slight2Mod1 = new AttributeModifier();
                slight2Mod1.AttributeName = "SecondaryAttack";
                slight2Mod1.Type = ModifierType.Additive;
                slight2Mod1.ModifierValue = slight2.xVariable;
                slight2.Modifiers.Add(slight2Mod1);

                ModifyDefenses(slight2, slight2.xVariable);

                AttributeModifier slight2Mod2 = new AttributeModifier();
                slight2Mod2.AttributeName = "Durability";
                slight2Mod2.Type = ModifierType.Additive;
                slight2Mod2.ModifierValue = -2 * slight2.xVariable;
                slight2.Modifiers.Add(slight2Mod2);

                AttributeModifier slight2Mod3 = new AttributeModifier();
                slight2Mod3.AttributeName = "PrimaryAttackDamage";
                slight2Mod3.Type = ModifierType.Additive;
                slight2Mod3.ModifierValue = -2 * slight2.xVariable;
                slight2.Modifiers.Add(slight2Mod3);

                AttributeModifier slight2Mod4 = new AttributeModifier();
                slight2Mod4.AttributeName = "SecondaryAttackDamage";
                slight2Mod4.Type = ModifierType.Additive;
                slight2Mod4.ModifierValue = -2 * slight2.xVariable;
                slight2.Modifiers.Add(slight2Mod4);
                CreatureQualities.Add(slight2);
            }
            #endregion
            #region Speedy (2)
            {
                NpcQuality speedy2 = new NpcQuality();
                speedy2.Name = "Speedy (2)";
                speedy2.Description = "Increase the creature’s Speed by the listed amount.";
                speedy2.xVariable = 2;

                AttributeModifier speedy2Mod0 = new AttributeModifier();
                speedy2Mod0.AttributeName = "Speed";
                speedy2Mod0.Type = ModifierType.Additive;
                speedy2Mod0.ModifierValue = speedy2.xVariable;
                speedy2.Modifiers.Add(speedy2Mod0);
                CreatureQualities.Add(speedy2);
            }
            #endregion
            #region Stalwart (2)
            {
                NpcQuality stalwart2 = new NpcQuality();
                stalwart2.Name = "Stalwart (2)";
                stalwart2.Description = "Increase the creature’s Health per track by 2x the listed amount.";
                stalwart2.xVariable = 2;

                AttributeModifier stalwart2Mod0 = new AttributeModifier();
                stalwart2Mod0.AttributeName = "HealthPoints";
                stalwart2Mod0.Type = ModifierType.Additive;
                stalwart2Mod0.ModifierValue = 2 * stalwart2.xVariable;
                stalwart2.Modifiers.Add(stalwart2Mod0);
                CreatureQualities.Add(stalwart2);
            }
            #endregion
            #region Susceptible (DamageType)
            {
                NpcQuality susceptible = new NpcQuality();
                susceptible.Name = "Susceptible (DamageType)";
                susceptible.Description = "Suffers a +4 damage from damage of the listed type. (Can take this Quality more than once applying it to a different damage type each time.) Taking this Quality gives the creature 1 free selection of the Fortified Quality.";
                CreatureQualities.Add(susceptible);
            }
            #endregion
            #region Threatening (2)
            {
                NpcQuality threatening2 = new NpcQuality();
                threatening2.Name = "Threatening (2)";
                threatening2.Description = "The creature adds 5x the listed amount in feet to the area around itself that it considers Adjacent.";
                threatening2.xVariable = 2;
                CreatureQualities.Add(threatening2);
            }
            #endregion
            #region Tough (2)
            {
                NpcQuality tough2 = new NpcQuality();
                tough2.Name = "Tough (2)";
                tough2.Description = "Increase the creature’s Durability by the listed amount.";
                tough2.xVariable = 2;

                AttributeModifier tough2Mod0 = new AttributeModifier();
                tough2Mod0.AttributeName = "Durability";
                tough2Mod0.Type = ModifierType.Additive;
                tough2Mod0.ModifierValue = tough2.xVariable;
                tough2.Modifiers.Add(tough2Mod0);
                CreatureQualities.Add(tough2);
            }
            #endregion
            #region Undead
            {
                NpcQuality undead = new NpcQuality();
                undead.Name = "Undead";
                undead.Description = "The creature is not considered Living, gains a +4 to Damage with Necrotic attacks, Susceptible to Holy damage (Takes +4 damage from holy [in addition to the +2 to CM holy damage already does to undead].";
                CreatureQualities.Add(undead);
            }
            #endregion
            #region Unerring
            {
                NpcQuality unerring2 = new NpcQuality();
                unerring2.Name = "Unerring (2)";
                unerring2.Description = "Increase the creature’s primary attack’s attack bonus by the listed amount. (Can take this Quality more than once applying it to a different attack each time.)";
                unerring2.xVariable = 2;

                AttributeModifier unerring2Mod0 = new AttributeModifier();
                unerring2Mod0.AttributeName = "PrimaryAttack";
                unerring2Mod0.Type = ModifierType.Additive;
                unerring2Mod0.ModifierValue = unerring2.xVariable;
                unerring2.Modifiers.Add(unerring2Mod0);
                CreatureQualities.Add(unerring2);
            }
            #endregion
            #region Venemous
            {
                NpcQuality venemous = new NpcQuality();
                venemous.Name = "Venemous";
                venemous.Description = "The creature's primary attack gains the Poison damage type in addition to its regular damage type. (Can take this Quality more than once applying it to a different attack.)";
                CreatureQualities.Add(venemous);
            }
            #endregion
        }

        private NaturalArmorClass GetRandomNaturalArmorClass()
        {
            switch (r.Next(1, 5))
            {
                case 0:
                    return NaturalArmorClass.None;
                case 1:
                    return NaturalArmorClass.Light;
                case 2:
                    return NaturalArmorClass.Medium;
                case 3:
                    return NaturalArmorClass.Heavy;
                case 4:
                default:
                    return NaturalArmorClass.Extreme;
            }
        }

        private NaturalWeaponClass GetRandomNaturalWeaponClass()
        {
            switch (r.Next(0, 4))
            {
                case 0:
                    return NaturalWeaponClass.Light;
                case 1:
                    return NaturalWeaponClass.Medium;
                case 2:
                    return NaturalWeaponClass.Heavy;
                case 3:
                default:
                    return NaturalWeaponClass.Extreme;
            }
        }

        private DamageType GetRandomPhysicalDamageType()
        {
            switch (r.Next(0, 3))
            {
                case 0:
                    return DamageType.Piercing;
                case 1:
                    return DamageType.Slashing;
                case 2:
                    return DamageType.Bludgeoning;
                default:
                    return DamageType.Ballistic;
            }
        }

        private static void ModifyDefenses(NpcQuality quality, int defenseChange)
        {
            AttributeModifier MeleePhysical = new AttributeModifier();
            MeleePhysical.AttributeName = "MeleePhysical";
            MeleePhysical.Type = ModifierType.Additive;
            MeleePhysical.ModifierValue = 1;
            quality.Modifiers.Add(MeleePhysical);
            AttributeModifier AreaPhysical = new AttributeModifier();
            AreaPhysical.AttributeName = "AreaPhysical";
            AreaPhysical.Type = ModifierType.Additive;
            AreaPhysical.ModifierValue = 1;
            quality.Modifiers.Add(AreaPhysical);
            AttributeModifier RangedPhysical = new AttributeModifier();
            RangedPhysical.AttributeName = "RangedPhysical";
            RangedPhysical.Type = ModifierType.Additive;
            RangedPhysical.ModifierValue = 1;
            quality.Modifiers.Add(RangedPhysical);
            AttributeModifier MeleeResolve = new AttributeModifier();
            MeleeResolve.AttributeName = "MeleeResolve";
            MeleeResolve.Type = ModifierType.Additive;
            MeleeResolve.ModifierValue = 1;
            quality.Modifiers.Add(MeleeResolve);
            AttributeModifier AreaResolve = new AttributeModifier();
            AreaResolve.AttributeName = "AreaResolve";
            AreaResolve.Type = ModifierType.Additive;
            AreaResolve.ModifierValue = 1;
            quality.Modifiers.Add(AreaResolve);
            AttributeModifier RangedResolve = new AttributeModifier();
            RangedResolve.AttributeName = "RangedResolve";
            RangedResolve.Type = ModifierType.Additive;
            RangedResolve.ModifierValue = 1;
            quality.Modifiers.Add(RangedResolve);
            AttributeModifier MeleeBody = new AttributeModifier();
            MeleeBody.AttributeName = "MeleeBody";
            MeleeBody.Type = ModifierType.Additive;
            MeleeBody.ModifierValue = 1;
            quality.Modifiers.Add(MeleeBody);
            AttributeModifier AreaBody = new AttributeModifier();
            AreaBody.AttributeName = "AreaBody";
            AreaBody.Type = ModifierType.Additive;
            AreaBody.ModifierValue = 1;
            quality.Modifiers.Add(AreaBody);
            AttributeModifier RangedBody = new AttributeModifier();
            RangedBody.AttributeName = "RangedBody";
            RangedBody.Type = ModifierType.Additive;
            RangedBody.ModifierValue = 1;
            quality.Modifiers.Add(RangedBody);
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
