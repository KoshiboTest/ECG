﻿using Emergence.Model;
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
        public int Level;
        public int Size;
        public NpcClass NpcClass;
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
            this.Level = level;
            this.NpcClass = npcClass;
            this.Size = size;
            this.Type = type;

            //Attributes
            Attributes = new NpcAttributeArray(level, npcClass);

            //Skills
            CombatSkills = new List<KeyValuePair<WeaponSkill, int>>();
            SpecializedSkills = new List<KeyValuePair<SpecializedSkill, int>>();
            KnowledgeSkills = new List<KeyValuePair<KnowledgeSkill, int>>();

            //Attacks
            Attacks = new List<NpcAttack>();

            //Size Mods
            #region
            AttributeModifier primaryAttackMod = new AttributeModifier();
            primaryAttackMod.AttributeName = "PrimaryAttack";
            primaryAttackMod.Type = ModifierType.Additive;
            AttributeModifier secondaryAttackMod = new AttributeModifier();
            secondaryAttackMod.AttributeName = "SecondaryAttack";
            secondaryAttackMod.Type = ModifierType.Additive;
            AttributeModifier meleePhysicalMod = new AttributeModifier();
            meleePhysicalMod.AttributeName = "MeleePhysical";
            meleePhysicalMod.Type = ModifierType.Additive;
            AttributeModifier areaPhysicalMod = new AttributeModifier();
            areaPhysicalMod.AttributeName = "AreaPhysical";
            areaPhysicalMod.Type = ModifierType.Additive;
            AttributeModifier rangedPhysicalMod = new AttributeModifier();
            rangedPhysicalMod.AttributeName = "RangedPhysical";
            rangedPhysicalMod.Type = ModifierType.Additive;
            AttributeModifier meleeResolveMod = new AttributeModifier();
            meleeResolveMod.AttributeName = "MeleeResolve";
            meleeResolveMod.Type = ModifierType.Additive;
            AttributeModifier areaResolveMod = new AttributeModifier();
            areaResolveMod.AttributeName = "AreaResolve";
            areaResolveMod.Type = ModifierType.Additive;
            AttributeModifier rangedResolveMod = new AttributeModifier();
            rangedResolveMod.AttributeName = "RangedResolve";
            rangedResolveMod.Type = ModifierType.Additive;
            AttributeModifier meleeBodyMod = new AttributeModifier();
            meleeBodyMod.AttributeName = "MeleeBody";
            meleeBodyMod.Type = ModifierType.Additive;
            AttributeModifier areaBodyMod = new AttributeModifier();
            areaBodyMod.AttributeName = "AreaBody";
            areaBodyMod.Type = ModifierType.Additive;
            AttributeModifier rangedBodyMod = new AttributeModifier();
            rangedBodyMod.AttributeName = "RangedBody";
            rangedBodyMod.Type = ModifierType.Additive;
            AttributeModifier initiativeMod = new AttributeModifier();
            initiativeMod.AttributeName = "Initiative";
            initiativeMod.Type = ModifierType.Additive;
            AttributeModifier durabilityMod = new AttributeModifier();
            durabilityMod.AttributeName = "Durability";
            durabilityMod.Type = ModifierType.Additive;
            AttributeModifier primaryAttackDamageMod = new AttributeModifier();
            primaryAttackDamageMod.AttributeName = "PrimaryAttackDamage";
            primaryAttackDamageMod.Type = ModifierType.Additive;
            AttributeModifier secondaryAttackDamageMod = new AttributeModifier();
            secondaryAttackDamageMod.AttributeName = "SecondaryAttackDamage";
            secondaryAttackDamageMod.Type = ModifierType.Additive;
            AttributeModifier tertiaryAttackDamageMod = new AttributeModifier();
            tertiaryAttackDamageMod.AttributeName = "TertiaryAttackDamage";
            tertiaryAttackDamageMod.Type = ModifierType.Additive;
            AttributeModifier healthPointsMod = new AttributeModifier();
            healthPointsMod.AttributeName = "HealthPoints";
            healthPointsMod.Type = ModifierType.Additive;
            switch (size)
            {
                case 0:
                    primaryAttackMod.ModifierValue = 3;
                    secondaryAttackMod.ModifierValue = 3;
                    meleePhysicalMod.ModifierValue = 3;
                    areaPhysicalMod.ModifierValue = 3;
                    rangedPhysicalMod.ModifierValue = 3;
                    meleeResolveMod.ModifierValue = 3;
                    areaResolveMod.ModifierValue = 3;
                    rangedResolveMod.ModifierValue = 3;
                    meleeBodyMod.ModifierValue = 3;
                    areaBodyMod.ModifierValue = 3;
                    rangedBodyMod.ModifierValue = 3;
                    initiativeMod.ModifierValue = 3;
                    durabilityMod.ModifierValue = -6;
                    primaryAttackDamageMod.ModifierValue = -6;
                    secondaryAttackDamageMod.ModifierValue = -6;
                    tertiaryAttackDamageMod.ModifierValue = -6;
                    healthPointsMod.Type = ModifierType.Multiplicative;
                    healthPointsMod.ModifierValue = .25M;
                    EffectiveStrength = -4;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 1:
                    primaryAttackMod.ModifierValue = 2;
                    secondaryAttackMod.ModifierValue = 2;
                    meleePhysicalMod.ModifierValue = 2;
                    areaPhysicalMod.ModifierValue = 2;
                    rangedPhysicalMod.ModifierValue = 2;
                    meleeResolveMod.ModifierValue = 2;
                    areaResolveMod.ModifierValue = 2;
                    rangedResolveMod.ModifierValue = 2;
                    meleeBodyMod.ModifierValue = 2;
                    areaBodyMod.ModifierValue = 2;
                    rangedBodyMod.ModifierValue = 2;
                    initiativeMod.ModifierValue = 2;
                    durabilityMod.ModifierValue = -4;
                    primaryAttackDamageMod.ModifierValue = -4;
                    secondaryAttackDamageMod.ModifierValue = -4;
                    tertiaryAttackDamageMod.ModifierValue = -4;
                    healthPointsMod.Type = ModifierType.Multiplicative;
                    healthPointsMod.ModifierValue = .5M;
                    EffectiveStrength = -2;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 2:
                    primaryAttackMod.ModifierValue = 1;
                    secondaryAttackMod.ModifierValue = 1;
                    meleePhysicalMod.ModifierValue = 1;
                    areaPhysicalMod.ModifierValue = 1;
                    rangedPhysicalMod.ModifierValue = 1;
                    meleeResolveMod.ModifierValue = 1;
                    areaResolveMod.ModifierValue = 1;
                    rangedResolveMod.ModifierValue = 1;
                    meleeBodyMod.ModifierValue = 1;
                    areaBodyMod.ModifierValue = 1;
                    rangedBodyMod.ModifierValue = 1;
                    initiativeMod.ModifierValue = 1;
                    durabilityMod.ModifierValue = -2;
                    primaryAttackDamageMod.ModifierValue = -2;
                    secondaryAttackDamageMod.ModifierValue = -2;
                    tertiaryAttackDamageMod.ModifierValue = -2;
                    healthPointsMod.Type = ModifierType.Multiplicative;
                    healthPointsMod.ModifierValue = .75M;
                    EffectiveStrength = -1;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 3:
                    EffectiveStrength = 0;
                    break;
                case 4:
                    primaryAttackMod.ModifierValue = -1;
                    secondaryAttackMod.ModifierValue = -1;
                    meleePhysicalMod.ModifierValue = -1;
                    areaPhysicalMod.ModifierValue = -1;
                    rangedPhysicalMod.ModifierValue = -1;
                    meleeResolveMod.ModifierValue = -1;
                    areaResolveMod.ModifierValue = -1;
                    rangedResolveMod.ModifierValue = -1;
                    meleeBodyMod.ModifierValue = -1;
                    areaBodyMod.ModifierValue = -1;
                    rangedBodyMod.ModifierValue = -1;
                    initiativeMod.ModifierValue = -1;
                    durabilityMod.ModifierValue = 2;
                    primaryAttackDamageMod.ModifierValue = 2;
                    secondaryAttackDamageMod.ModifierValue = 2;
                    tertiaryAttackDamageMod.ModifierValue = 2;
                    healthPointsMod.ModifierValue = 5;
                    EffectiveStrength = 2;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 5:
                    primaryAttackMod.ModifierValue = -2;
                    secondaryAttackMod.ModifierValue = -2;
                    meleePhysicalMod.ModifierValue = -2;
                    areaPhysicalMod.ModifierValue = -2;
                    rangedPhysicalMod.ModifierValue = -2;
                    meleeResolveMod.ModifierValue = -2;
                    areaResolveMod.ModifierValue = -2;
                    rangedResolveMod.ModifierValue = -2;
                    meleeBodyMod.ModifierValue = -2;
                    areaBodyMod.ModifierValue = -2;
                    rangedBodyMod.ModifierValue = -2;
                    initiativeMod.ModifierValue = -2;
                    durabilityMod.ModifierValue = 4;
                    primaryAttackDamageMod.ModifierValue = 4;
                    secondaryAttackDamageMod.ModifierValue = 4;
                    tertiaryAttackDamageMod.ModifierValue = 4;
                    healthPointsMod.ModifierValue = 10;
                    EffectiveStrength = 4;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 6:
                    primaryAttackMod.ModifierValue = -3;
                    secondaryAttackMod.ModifierValue = -3;
                    meleePhysicalMod.ModifierValue = -3;
                    areaPhysicalMod.ModifierValue = -3;
                    rangedPhysicalMod.ModifierValue = -3;
                    meleeResolveMod.ModifierValue = -3;
                    areaResolveMod.ModifierValue = -3;
                    rangedResolveMod.ModifierValue = -3;
                    meleeBodyMod.ModifierValue = -3;
                    areaBodyMod.ModifierValue = -3;
                    rangedBodyMod.ModifierValue = -3;
                    initiativeMod.ModifierValue = -3;
                    durabilityMod.ModifierValue = 6;
                    primaryAttackDamageMod.ModifierValue = 6;
                    secondaryAttackDamageMod.ModifierValue = 6;
                    tertiaryAttackDamageMod.ModifierValue = 6;
                    healthPointsMod.ModifierValue = 20;
                    EffectiveStrength = 6;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 7:
                    primaryAttackMod.ModifierValue = -4;
                    secondaryAttackMod.ModifierValue = -4;
                    meleePhysicalMod.ModifierValue = -4;
                    areaPhysicalMod.ModifierValue = -4;
                    rangedPhysicalMod.ModifierValue = -4;
                    meleeResolveMod.ModifierValue = -4;
                    areaResolveMod.ModifierValue = -4;
                    rangedResolveMod.ModifierValue = -4;
                    meleeBodyMod.ModifierValue = -4;
                    areaBodyMod.ModifierValue = -4;
                    rangedBodyMod.ModifierValue = -4;
                    initiativeMod.ModifierValue = -4;
                    durabilityMod.ModifierValue = 8;
                    primaryAttackDamageMod.ModifierValue = 8;
                    secondaryAttackDamageMod.ModifierValue = 8;
                    tertiaryAttackDamageMod.ModifierValue = 8;
                    healthPointsMod.ModifierValue = 30;
                    EffectiveStrength = 8;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 8:
                    primaryAttackMod.ModifierValue = -5;
                    secondaryAttackMod.ModifierValue = -5;
                    meleePhysicalMod.ModifierValue = -5;
                    areaPhysicalMod.ModifierValue = -5;
                    rangedPhysicalMod.ModifierValue = -5;
                    meleeResolveMod.ModifierValue = -5;
                    areaResolveMod.ModifierValue = -5;
                    rangedResolveMod.ModifierValue = -5;
                    meleeBodyMod.ModifierValue = -5;
                    areaBodyMod.ModifierValue = -5;
                    rangedBodyMod.ModifierValue = -5;
                    initiativeMod.ModifierValue = -5;
                    durabilityMod.ModifierValue = 10;
                    primaryAttackDamageMod.ModifierValue = 10;
                    secondaryAttackDamageMod.ModifierValue = 10;
                    tertiaryAttackDamageMod.ModifierValue = 10;
                    healthPointsMod.ModifierValue = 40;
                    EffectiveStrength = 10;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 9:
                    primaryAttackMod.ModifierValue = -6;
                    secondaryAttackMod.ModifierValue = -6;
                    meleePhysicalMod.ModifierValue = -6;
                    areaPhysicalMod.ModifierValue = -6;
                    rangedPhysicalMod.ModifierValue = -6;
                    meleeResolveMod.ModifierValue = -6;
                    areaResolveMod.ModifierValue = -6;
                    rangedResolveMod.ModifierValue = -6;
                    meleeBodyMod.ModifierValue = -6;
                    areaBodyMod.ModifierValue = -6;
                    rangedBodyMod.ModifierValue = -6;
                    initiativeMod.ModifierValue = -6;
                    durabilityMod.ModifierValue = 12;
                    primaryAttackDamageMod.ModifierValue = 12;
                    secondaryAttackDamageMod.ModifierValue = 12;
                    tertiaryAttackDamageMod.ModifierValue = 12;
                    healthPointsMod.ModifierValue = 50;
                    EffectiveStrength = 12;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                case 10:
                    primaryAttackMod.ModifierValue = -7;
                    secondaryAttackMod.ModifierValue = -7;
                    meleePhysicalMod.ModifierValue = -7;
                    areaPhysicalMod.ModifierValue = -7;
                    rangedPhysicalMod.ModifierValue = -7;
                    meleeResolveMod.ModifierValue = -7;
                    areaResolveMod.ModifierValue = -7;
                    rangedResolveMod.ModifierValue = -7;
                    meleeBodyMod.ModifierValue = -7;
                    areaBodyMod.ModifierValue = -7;
                    rangedBodyMod.ModifierValue = -7;
                    initiativeMod.ModifierValue = -7;
                    durabilityMod.ModifierValue = 14;
                    primaryAttackDamageMod.ModifierValue = 14;
                    secondaryAttackDamageMod.ModifierValue = 14;
                    tertiaryAttackDamageMod.ModifierValue = 14;
                    healthPointsMod.ModifierValue = 60;
                    EffectiveStrength = 14;
                    Attributes.AttributeAdjustments.Add(primaryAttackMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackMod);
                    Attributes.AttributeAdjustments.Add(meleePhysicalMod);
                    Attributes.AttributeAdjustments.Add(areaPhysicalMod);
                    Attributes.AttributeAdjustments.Add(rangedPhysicalMod);
                    Attributes.AttributeAdjustments.Add(meleeResolveMod);
                    Attributes.AttributeAdjustments.Add(areaResolveMod);
                    Attributes.AttributeAdjustments.Add(rangedResolveMod);
                    Attributes.AttributeAdjustments.Add(meleeBodyMod);
                    Attributes.AttributeAdjustments.Add(areaBodyMod);
                    Attributes.AttributeAdjustments.Add(rangedBodyMod);
                    Attributes.AttributeAdjustments.Add(initiativeMod);
                    Attributes.AttributeAdjustments.Add(durabilityMod);
                    Attributes.AttributeAdjustments.Add(primaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(secondaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(tertiaryAttackDamageMod);
                    Attributes.AttributeAdjustments.Add(healthPointsMod);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Size is out of range.");
            }

            #endregion

            //Abilities
            Abilities = new List<NpcAbility>();

            //Talents
            Talents = new List<Talent>();
        }
    }
}