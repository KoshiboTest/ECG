using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Collections.ObjectModel;
using Emergence.Model;

namespace Emergence.ViewModel
{
    public class Armory : INotifyPropertyChanged
    {
        private ObservableCollection<WeaponVM> baseWeapons;
        public ObservableCollection<WeaponVM> BaseWeapons
        {
            get
            {
                return baseWeapons;
            }
            private set
            {
                baseWeapons = value;
                NotifyPropertyChanged("BaseWeapons");
            }
        }

        private ObservableCollection<AmpVM> baseAmps;
        public ObservableCollection<AmpVM> BaseAmps
        {
            get
            {
                return baseAmps;
            }
            set
            {
                baseAmps = value;
                NotifyPropertyChanged("BaseAmps");
            }
        }

        private ObservableCollection<ArmorVM> baseArmors;
        public ObservableCollection<ArmorVM> BaseArmors
        {
            get
            {
                return baseArmors;
            }
            set
            {
                baseArmors = value;
                NotifyPropertyChanged("BaseArmors");
            }
        }

        private ObservableCollection<WeaponModVM> weaponMods;
        public ObservableCollection<WeaponModVM> WeaponMods
        {
            get
            {
                return weaponMods;
            }
            private set
            {
                weaponMods = value;
                NotifyPropertyChanged("WeaponMods");
            }
        }

        private ObservableCollection<AmpModVM> ampMods;
        public ObservableCollection<AmpModVM> AmpMods
        {
            get
            {
                return ampMods;
            }
            set
            {
                ampMods = value;
                NotifyPropertyChanged("AmpMods");
            }
        }

        private ObservableCollection<ArmorModVM> armorMods;
        public ObservableCollection<ArmorModVM> ArmorMods
        {
            get
            {
                return armorMods;   
            }
            set
            {
                armorMods = value;
                NotifyPropertyChanged("ArmorMods");
            }
        }

        private ObservableCollection<WeaponVM> yourWeapons;
        public ObservableCollection<WeaponVM> YourWeapons
        {
            get
            {
                return yourWeapons;
            }
            private set
            {
                yourWeapons = value;
                NotifyPropertyChanged("YourWeapons");
            }
        }

        private ObservableCollection<AmpVM> yourAmps;
        public ObservableCollection<AmpVM> YourAmps
        {
            get
            {
                return yourAmps;
            }
            private set
            {
                yourAmps = value;
                NotifyPropertyChanged("YourWeapons");
            }
        }

        public RelayCommand<WeaponVM> BuyCommand;
        
        public void Buy(object w)
        {
            if (w is WeaponVM)
            {
                yourWeapons.Add(w as WeaponVM);
            }
            else if (w is AmpVM)
            {
                yourAmps.Add(w as AmpVM);
            }
        }

        public string Upgrade(object w, object m)
        {
            if (w is WeaponVM && m is WeaponModVM)
            {
                WeaponVM ww = w as WeaponVM;
                WeaponModVM wm = m as WeaponModVM;
                if (wm.ApplyTo(ww))
                {
                    return string.Format("Applied {0} mod to {1}.", wm.Name, ww.Name);
                }
                else
                {
                    return wm.ApplyError;
                }
            }
            else
            {
                return "Args are fucked.";
            }
        }

        public Armory()
        {
            InitializeWeapons();
            InitializeWeaponMods();
        }

        private void InitializeWeapons()
        {
            yourWeapons = new ObservableCollection<WeaponVM>();
            baseWeapons = new ObservableCollection<WeaponVM>();
            baseAmps = new ObservableCollection<AmpVM>();
            baseArmors = new ObservableCollection<ArmorVM>();

            #region Weapons
            WeaponVM Unarmed = new WeaponVM();
            WeaponVM Knife = new WeaponVM();
            WeaponVM CombatGlove = new WeaponVM();
            WeaponVM ShortSword = new WeaponVM();
            WeaponVM LongSword = new WeaponVM();
            WeaponVM GreatSword = new WeaponVM();
            WeaponVM Hatchet = new WeaponVM();
            WeaponVM Axe = new WeaponVM();
            WeaponVM GreatAxe = new WeaponVM();
            WeaponVM Mace = new WeaponVM();
            WeaponVM Hammer = new WeaponVM();
            WeaponVM Maul = new WeaponVM();
            WeaponVM Staff = new WeaponVM();
            WeaponVM Spear = new WeaponVM();
            WeaponVM Whip = new WeaponVM();
            WeaponVM Flail = new WeaponVM();
            WeaponVM Polearm = new WeaponVM();
            WeaponVM StunGun = new WeaponVM();
            WeaponVM LightPistol = new WeaponVM();
            WeaponVM HeavyPistol = new WeaponVM();
            WeaponVM HandCannon = new WeaponVM();
            WeaponVM SMG = new WeaponVM();
            WeaponVM Shotgun = new WeaponVM();
            WeaponVM LightRifle = new WeaponVM();
            WeaponVM HeavyRifle = new WeaponVM();
            WeaponVM Longbow = new WeaponVM();
            WeaponVM Shortbow = new WeaponVM();
            WeaponVM LightCrossbow = new WeaponVM();
            WeaponVM HeavyCrossbow = new WeaponVM();
            WeaponVM LightThrown = new WeaponVM();
            WeaponVM HeavyThrown = new WeaponVM();
            WeaponVM Buckler = new WeaponVM();
            WeaponVM HeavyShield = new WeaponVM();
            WeaponVM TowerShield = new WeaponVM();
            WeaponVM GrenadeLauncher = new WeaponVM();
            WeaponVM RocketLauncher = new WeaponVM();
            WeaponVM Flamethrower = new WeaponVM();
            WeaponVM MissileLauncher = new WeaponVM();
            WeaponVM Taser = new WeaponVM();


            Unarmed.Accuracy = 2;
            Unarmed.Damage = 0;
            Unarmed.CM = 3;
            Unarmed.Size = 0;
            Unarmed.Type = DamageType.Bludgeoning;
            Unarmed.Cost = 0;
            Unarmed.Skill = WeaponSkill.Unarmed;
            Unarmed.Properties = WeaponProperty.Attached | WeaponProperty.NonLethal | WeaponProperty.NoMods;
            Unarmed.Name = "Unarmed";
            Unarmed.Range = Range.Melee;

            Knife.Accuracy = 1;
            Knife.Damage = 1;
            Knife.CM = 3;
            Knife.Size = 1;
            Knife.Type = DamageType.Piercing | DamageType.Slashing;
            Knife.Cost = 50;
            Knife.Skill = WeaponSkill.CloseCombat;
            Knife.Properties = WeaponProperty.Agile | WeaponProperty.Penetrating | WeaponProperty.Thrown;
            Knife.Name = "Knife";
            Knife.Range = Range.Melee;

            CombatGlove.Accuracy = 1;
            CombatGlove.Damage = 1;
            CombatGlove.CM = 3;
            CombatGlove.Size = 0;
            CombatGlove.Type = DamageType.Bludgeoning;
            CombatGlove.Cost = 50;
            CombatGlove.Skill = WeaponSkill.Unarmed;
            CombatGlove.Properties = WeaponProperty.Attached | WeaponProperty.Unarmed;
            CombatGlove.Name = "Combat Glove";
            CombatGlove.Range = Range.Melee;

            ShortSword.Accuracy = 0;
            ShortSword.Damage = 3;
            ShortSword.CM = 3;
            ShortSword.Size = 3;
            ShortSword.Type = DamageType.Piercing | DamageType.Slashing;
            ShortSword.Cost = 125;
            ShortSword.Skill = WeaponSkill.CloseCombat;
            ShortSword.Properties = WeaponProperty.Defensive | WeaponProperty.Agile | WeaponProperty.Penetrating;
            ShortSword.Name = "Short Sword";
            ShortSword.Range = Range.Melee;

            LongSword.Accuracy = -1;
            LongSword.Damage = 5;
            LongSword.CM = 3;
            LongSword.Size = 4;
            LongSword.Type = DamageType.Slashing;
            LongSword.Cost = 250;
            LongSword.Skill = WeaponSkill.Dueling;
            LongSword.Properties = WeaponProperty.Defensive | WeaponProperty.Penetrating;
            LongSword.Name = "Long Sword";
            LongSword.Range = Range.Melee;

            GreatSword.Accuracy = -2;
            GreatSword.Damage = 7;
            GreatSword.CM = 3;
            GreatSword.Size = 6;
            GreatSword.Type = DamageType.Slashing;
            GreatSword.Cost = 500;
            GreatSword.Skill = WeaponSkill.Heavy;
            GreatSword.Properties = WeaponProperty.Vicious1 | WeaponProperty.Defensive;
            GreatSword.Name = "Great Sword";
            GreatSword.Range = Range.Melee;

            Hatchet.Accuracy = 0;
            Hatchet.Damage = 4;
            Hatchet.CM = 4;
            Hatchet.Size = 3;
            Hatchet.Type = DamageType.Slashing;
            Hatchet.Cost = 125;
            Hatchet.Skill = WeaponSkill.CloseCombat;
            Hatchet.Properties = WeaponProperty.CritModPlus1;
            Hatchet.Name = "Hatchet";
            Hatchet.Range = Range.Melee;

            Axe.Accuracy = -1;
            Axe.Damage = 6;
            Axe.CM = 4;
            Axe.Size = 4;
            Axe.Type = DamageType.Slashing;
            Axe.Cost = 250;
            Axe.Skill = WeaponSkill.Dueling;
            Axe.Properties = WeaponProperty.CritModPlus1;
            Axe.Name = "Axe";
            Axe.Range = Range.Melee;

            GreatAxe.Accuracy = -3;
            GreatAxe.Damage = 9;
            GreatAxe.CM = 4;
            GreatAxe.Size = 6;
            GreatAxe.Type = DamageType.Slashing;
            GreatAxe.Cost = 500;
            GreatAxe.Skill = WeaponSkill.Heavy;
            GreatAxe.Properties = WeaponProperty.Vicious1 | WeaponProperty.CritModPlus1;
            GreatAxe.Name = "Great Axe";
            GreatAxe.Range = Range.Melee;

            Mace.Accuracy = 0;
            Mace.Damage = 4;
            Mace.CM = 3;
            Mace.Size = 3;
            Mace.Type = DamageType.Bludgeoning;
            Mace.Cost = 125;
            Mace.Skill = WeaponSkill.CloseCombat;
            Mace.Properties = WeaponProperty.Thundering | WeaponProperty.Agile;
            Mace.Name = "Mace";
            Mace.Range = Range.Melee;

            Hammer.Accuracy = -2;
            Hammer.Damage = 6;
            Hammer.CM = 3;
            Hammer.Size = 4;
            Hammer.Type = DamageType.Bludgeoning;
            Hammer.Cost = 250;
            Hammer.Skill = WeaponSkill.Dueling;
            Hammer.Properties = WeaponProperty.Blasting | WeaponProperty.Thundering | WeaponProperty.Vicious1;
            Hammer.Name = "Hammer";
            Hammer.Range = Range.Melee;

            Maul.Accuracy = -3;
            Maul.Damage = 8;
            Maul.CM = 3;
            Maul.Size = 7;
            Maul.Type = DamageType.Bludgeoning;
            Maul.Cost = 500;
            Maul.Skill = WeaponSkill.Heavy;
            Maul.Properties = WeaponProperty.Blasting | WeaponProperty.Thundering | WeaponProperty.Vicious1 | WeaponProperty.CritKnockback;
            Maul.Name = "Maul";
            Maul.Range = Range.Melee;

            Staff.Accuracy = 0;
            Staff.Damage = 4;
            Staff.CM = 3;
            Staff.Size = 5;
            Staff.Type = DamageType.Bludgeoning;
            Staff.Cost = 50;
            Staff.Skill = WeaponSkill.Heavy;
            Staff.Properties = WeaponProperty.Hafted | WeaponProperty.Defensive | WeaponProperty.Reach | WeaponProperty.CritModLess1;
            Staff.Name = "Staff";
            Staff.Range = Range.Melee;

            Spear.Accuracy = -1;
            Spear.Damage = 5;
            Spear.CM = 3;
            Spear.Size = 5;
            Spear.Type = DamageType.Piercing;
            Spear.Cost = 250;
            Spear.Skill = WeaponSkill.Heavy;
            Spear.Properties = WeaponProperty.Hafted | WeaponProperty.Defensive | WeaponProperty.Penetrating | WeaponProperty.Thundering | WeaponProperty.Reach;
            Spear.Name = "Spear";
            Spear.Range = Range.Melee;

            Whip.Accuracy = 0;
            Whip.Damage = 4;
            Whip.CM = 3;
            Whip.Size = 3;
            Whip.Type = DamageType.Slashing;
            Whip.Cost = 100;
            Whip.Skill = WeaponSkill.Dueling;
            Whip.Properties = WeaponProperty.Agile | WeaponProperty.Limited4 | WeaponProperty.Confounding | WeaponProperty.Entangling | WeaponProperty.Reach | WeaponProperty.NonLethal;
            Whip.Name = "Whip";
            Whip.Range = Range.Melee;

            Flail.Accuracy = -1;
            Flail.Damage = 5;
            Flail.CM = 3;
            Flail.Size = 4;
            Flail.Type = DamageType.Bludgeoning;
            Flail.Cost = 250;
            Flail.Skill = WeaponSkill.Dueling;
            Flail.Properties = WeaponProperty.Confounding | WeaponProperty.Blasting | WeaponProperty.Entangling;
            Flail.Name = "Flail";
            Flail.Range = Range.Melee;

            Polearm.Accuracy = -2;
            Polearm.Damage = 8;
            Polearm.CM = 3;
            Polearm.Size = 6;
            Polearm.Type = DamageType.Bludgeoning | DamageType.Slashing | DamageType.Piercing;
            Polearm.Cost = 500;
            Polearm.Skill = WeaponSkill.Heavy;
            Polearm.Properties = WeaponProperty.VariableDamage | WeaponProperty.Reach;
            Polearm.Name = "Polearm";
            Polearm.Range = Range.Melee;

            StunGun.Accuracy = 0;
            StunGun.Damage = 7;
            StunGun.CM = 6;
            StunGun.Size = 1;
            StunGun.Type = DamageType.Electricity;
            StunGun.Cost = 300;
            StunGun.Skill = WeaponSkill.CloseCombat;
            StunGun.Properties = WeaponProperty.Limited0 | WeaponProperty.CritModPlus3 | WeaponProperty.NonLethal;
            StunGun.Name = "Stun Gun";
            StunGun.Range = Range.Melee;

            LightPistol.Accuracy = 0;
            LightPistol.Damage = 3;
            LightPistol.CM = 3;
            LightPistol.AmmoCapacity = 10;
            LightPistol.Size = 2;
            LightPistol.Type = DamageType.Ballistic;
            LightPistol.Cost = 500;
            LightPistol.Skill = WeaponSkill.Shortarms;
            LightPistol.Properties = WeaponProperty.Consistent3 | WeaponProperty.SemiAuto4;
            LightPistol.Name = "Light Pistol";
            LightPistol.Range = Range.Pistol;

            HeavyPistol.Accuracy = -1;
            HeavyPistol.Damage = 6;
            HeavyPistol.CM = 3;
            HeavyPistol.AmmoCapacity = 8;
            HeavyPistol.Size = 3;
            HeavyPistol.Type = DamageType.Ballistic;
            HeavyPistol.Cost = 800;
            HeavyPistol.Skill = WeaponSkill.Shortarms;
            HeavyPistol.Properties = WeaponProperty.Consistent3 | WeaponProperty.Vicious1 | WeaponProperty.SemiAuto3;
            HeavyPistol.Name = "Heavy Pistol";
            HeavyPistol.Range = Range.Pistol;

            HandCannon.Accuracy = -2;
            HandCannon.Damage = 8;
            HandCannon.CM = 4;
            HandCannon.AmmoCapacity = 5;
            HandCannon.Size = 4;
            HandCannon.Type = DamageType.Ballistic;
            HandCannon.Cost = 1000;
            HandCannon.Skill = WeaponSkill.Shortarms;
            HandCannon.Properties = WeaponProperty.Consistent4 | WeaponProperty.Vicious2 | WeaponProperty.CritModPlus1;
            HandCannon.Name = "Hand Cannon";
            HandCannon.Range = Range.Pistol;

            SMG.Accuracy = -1;
            SMG.Damage = 5;
            SMG.CM = 3;
            SMG.AmmoCapacity = 20;
            SMG.Size = 5;
            SMG.Type = DamageType.Ballistic;
            SMG.Cost = 1500;
            SMG.Skill = WeaponSkill.Shortarms;
            SMG.Properties = WeaponProperty.Consistent3 | WeaponProperty.SemiAuto3 | WeaponProperty.FullAuto15;
            SMG.Name = "SMG";
            SMG.Range = Range.SMG;

            Shotgun.Accuracy = -2;
            Shotgun.Damage = 8;
            Shotgun.CM = 4;
            Shotgun.AmmoCapacity = 6;
            Shotgun.Size = 6;
            Shotgun.Type = DamageType.Ballistic;
            Shotgun.Cost = 500;
            Shotgun.Skill = WeaponSkill.Longarms;
            Shotgun.Properties = WeaponProperty.Consistent4 | WeaponProperty.Vicious3 | WeaponProperty.CritModPlus1;
            Shotgun.Name = "Shotgun";
            Shotgun.Range = Range.Shotgun;

            LightRifle.Accuracy = -1;
            LightRifle.Damage = 6;
            LightRifle.CM = 3;
            LightRifle.AmmoCapacity = 10;
            LightRifle.Size = 5;
            LightRifle.Type = DamageType.Ballistic;
            LightRifle.Cost = 750;
            LightRifle.Skill = WeaponSkill.Longarms;
            LightRifle.Properties = WeaponProperty.Consistent3 | WeaponProperty.SemiAuto3 | WeaponProperty.Vicious1;
            LightRifle.Name = "Light Rifle";
            LightRifle.Range = Range.Rifle;

            HeavyRifle.Accuracy = -2;
            HeavyRifle.Damage = 9;
            HeavyRifle.CM = 4;
            HeavyRifle.AmmoCapacity = 1;
            HeavyRifle.Size = 8;
            HeavyRifle.Type = DamageType.Ballistic;
            HeavyRifle.Cost = 1500;
            HeavyRifle.Skill = WeaponSkill.Longarms;
            HeavyRifle.Properties = WeaponProperty.Consistent4 | WeaponProperty.Vicious2 | WeaponProperty.TwoHanded | WeaponProperty.CritModPlus1;
            HeavyRifle.Name = "Heavy Rifle";
            HeavyRifle.Range = Range.HeavyRifle;

            Longbow.Accuracy = -1;
            Longbow.Damage = 7;
            Longbow.CM = 3;
            Longbow.AmmoCapacity = 1;
            Longbow.Size = 6;
            Longbow.Type = DamageType.Piercing;
            Longbow.Cost = 400;
            Longbow.Skill = WeaponSkill.Bows;
            Longbow.Properties = WeaponProperty.TwoHanded | WeaponProperty.ManualLoad | WeaponProperty.Hindering | WeaponProperty.Reusable;
            Longbow.Name = "Longbow";
            Longbow.Range = Range.Bows;

            Shortbow.Accuracy = 0;
            Shortbow.Damage = 5;
            Shortbow.CM = 3;
            Shortbow.AmmoCapacity = 1;
            Shortbow.Size = 4;
            Shortbow.Type = DamageType.Piercing;
            Shortbow.Cost = 200;
            Shortbow.Skill = WeaponSkill.Bows;
            Shortbow.Properties = WeaponProperty.TwoHanded | WeaponProperty.ManualLoad | WeaponProperty.Hindering | WeaponProperty.Reusable;
            Shortbow.Name = "Shortbow";
            Shortbow.Range = Range.Bows;

            LightCrossbow.Accuracy = -1;
            LightCrossbow.Damage = 7;
            LightCrossbow.CM = 3;
            LightCrossbow.AmmoCapacity = 1;
            LightCrossbow.Size = 4;
            LightCrossbow.Type = DamageType.Piercing;
            LightCrossbow.Cost = 300;
            LightCrossbow.Skill = WeaponSkill.Bows;
            LightCrossbow.Properties = WeaponProperty.ManualLoad | WeaponProperty.Hindering | WeaponProperty.Reusable;
            LightCrossbow.Name = "Light Crossbow";
            LightCrossbow.Range = Range.Bows;

            HeavyCrossbow.Accuracy = -2;
            HeavyCrossbow.Damage = 9;
            HeavyCrossbow.CM = 3;
            HeavyCrossbow.AmmoCapacity = 1;
            HeavyCrossbow.Size = 6;
            HeavyCrossbow.Type = DamageType.Piercing;
            HeavyCrossbow.Cost = 500;
            HeavyCrossbow.Skill = WeaponSkill.Bows;
            HeavyCrossbow.Properties = WeaponProperty.ManualLoad | WeaponProperty.Hindering | WeaponProperty.Reusable;
            HeavyCrossbow.Name = "Heavy Crossbow";
            HeavyCrossbow.Range = Range.Bows;

            LightThrown.Accuracy = 1;
            LightThrown.Damage = 2;
            LightThrown.CM = 3;
            LightThrown.Size = 1;
            LightThrown.Type = DamageType.Piercing;
            LightThrown.Cost = 30;
            LightThrown.Skill = WeaponSkill.Thrown;
            LightThrown.Properties = WeaponProperty.QuickDraw | WeaponProperty.Agile | WeaponProperty.OneHanded;
            LightThrown.Name = "Light Thrown";
            LightThrown.Range = Range.Thrown;

            HeavyThrown.Accuracy = -1;
            HeavyThrown.Damage = 6;
            HeavyThrown.CM = 3;
            HeavyThrown.Size = 4;
            HeavyThrown.Type = DamageType.Bludgeoning | DamageType.Piercing | DamageType.Slashing;
            HeavyThrown.Cost = 40;
            HeavyThrown.Skill = WeaponSkill.Thrown;
            HeavyThrown.Properties = WeaponProperty.VariableDamage | WeaponProperty.OneHanded;
            HeavyThrown.Name = "Heavy Thrown";
            HeavyThrown.Range = Range.Thrown;

            Buckler.Accuracy = 0;
            Buckler.Damage = 0;
            Buckler.CM = 3;
            Buckler.Size = 2;
            Buckler.Type = DamageType.Bludgeoning;
            Buckler.Cost = 200;
            Buckler.Skill = WeaponSkill.CloseCombat;
            Buckler.Properties = WeaponProperty.Attached | WeaponProperty.ShieldingL2;
            Buckler.Name = "Buckler";
            Buckler.Range = Range.Melee;

            HeavyShield.Accuracy = -1;
            HeavyShield.Damage = 1;
            HeavyShield.CM = 3;
            HeavyShield.Size = 6;
            HeavyShield.Type = DamageType.Bludgeoning;
            HeavyShield.Cost = 400;
            HeavyShield.Skill = WeaponSkill.Dueling;
            HeavyShield.Properties = WeaponProperty.Strapped | WeaponProperty.ShieldingL4;
            HeavyShield.Name = "Heavy Shield";
            HeavyShield.Range = Range.Melee;

            TowerShield.Accuracy = -2;
            TowerShield.Damage = 2;
            TowerShield.CM = 3;
            TowerShield.Size = 8;
            TowerShield.Type = DamageType.Bludgeoning;
            TowerShield.Cost = 800;
            TowerShield.Skill = WeaponSkill.Heavy;
            TowerShield.Properties = WeaponProperty.Strapped | WeaponProperty.ShieldingH4;
            TowerShield.Name = "Tower Shield";
            TowerShield.Range = Range.Melee;

            GrenadeLauncher.Accuracy = -1;
            GrenadeLauncher.Damage = 0;
            GrenadeLauncher.CM = 3;
            GrenadeLauncher.AmmoCapacity = 1;
            GrenadeLauncher.Size = 5;
            GrenadeLauncher.Type = DamageType.Bludgeoning;
            GrenadeLauncher.Cost = 1500;
            GrenadeLauncher.Skill = WeaponSkill.Launcher;
            GrenadeLauncher.Properties = WeaponProperty.OrdinanceFiring | WeaponProperty.RangeShotgun;
            GrenadeLauncher.Name = "Grenade Launcher";
            GrenadeLauncher.Range = Range.Shotgun;

            RocketLauncher.Accuracy = -2;
            RocketLauncher.Damage = 0;
            RocketLauncher.CM = 3;
            RocketLauncher.AmmoCapacity = 1;
            RocketLauncher.Size = 6;
            RocketLauncher.Type = DamageType.Bludgeoning;
            RocketLauncher.Cost = 2500;
            RocketLauncher.Skill = WeaponSkill.Launcher;
            RocketLauncher.Properties = WeaponProperty.OrdinanceFiring | WeaponProperty.RangeRifle;
            RocketLauncher.Name = "Rocket Launcher";
            RocketLauncher.Range = Range.Rifle;

            Flamethrower.Accuracy = 0;
            Flamethrower.Damage = 4;
            Flamethrower.CM = 3;
            Flamethrower.AmmoCapacity = 4;
            Flamethrower.Size = 7;
            Flamethrower.Type = DamageType.Fire;
            Flamethrower.Cost = 2000;
            Flamethrower.Skill = WeaponSkill.Launcher;
            Flamethrower.Properties = WeaponProperty.Cone | WeaponProperty.Consistent6;
            Flamethrower.Name = "Flamethrower";
            Flamethrower.Range = Range.ThirtyFootCone;

            MissileLauncher.Accuracy = -3;
            MissileLauncher.Damage = 0;
            MissileLauncher.CM = 3;
            MissileLauncher.AmmoCapacity = 1;
            MissileLauncher.Size = 7;
            MissileLauncher.Type = DamageType.Bludgeoning;
            MissileLauncher.Cost = 4000;
            MissileLauncher.Skill = WeaponSkill.Launcher;
            MissileLauncher.Properties = WeaponProperty.OrdinanceFiring | WeaponProperty.RangeHeavyRifle;
            MissileLauncher.Name = "Missile Launcher";
            MissileLauncher.Range = Range.HeavyRifle;

            Taser.Accuracy = 1;
            Taser.Damage = 2;
            Taser.CM = 3;
            Taser.AmmoCapacity = 2;
            Taser.Size = 1;
            Taser.Type = DamageType.Electricity;
            Taser.Cost = 250;
            Taser.Skill = WeaponSkill.Launcher;
            Taser.Properties = WeaponProperty.Lethal1 | WeaponProperty.NonLethal | WeaponProperty.LowImpact | WeaponProperty.Range20;
            Taser.Name = "Taser";
            Taser.Range = Range.TwentyFeet;

            baseWeapons.Add(Unarmed);
            baseWeapons.Add(Knife);
            baseWeapons.Add(CombatGlove);
            baseWeapons.Add(ShortSword);
            baseWeapons.Add(LongSword);
            baseWeapons.Add(GreatSword);
            baseWeapons.Add(Hatchet);
            baseWeapons.Add(Axe);
            baseWeapons.Add(GreatAxe);
            baseWeapons.Add(Mace);
            baseWeapons.Add(Hammer);
            baseWeapons.Add(Maul);
            baseWeapons.Add(Staff);
            baseWeapons.Add(Spear);
            baseWeapons.Add(Whip);
            baseWeapons.Add(Flail);
            baseWeapons.Add(Polearm);
            baseWeapons.Add(StunGun);
            baseWeapons.Add(LightPistol);
            baseWeapons.Add(HeavyPistol);
            baseWeapons.Add(HandCannon);
            baseWeapons.Add(SMG);
            baseWeapons.Add(Shotgun);
            baseWeapons.Add(LightRifle);
            baseWeapons.Add(HeavyRifle);
            baseWeapons.Add(Longbow);
            baseWeapons.Add(Shortbow);
            baseWeapons.Add(LightCrossbow);
            baseWeapons.Add(HeavyCrossbow);
            baseWeapons.Add(LightThrown);
            baseWeapons.Add(HeavyThrown);
            baseWeapons.Add(Buckler);
            baseWeapons.Add(HeavyShield);
            baseWeapons.Add(TowerShield);
            baseWeapons.Add(GrenadeLauncher);
            baseWeapons.Add(RocketLauncher);
            baseWeapons.Add(Flamethrower);
            baseWeapons.Add(MissileLauncher);
            baseWeapons.Add(Taser);
            #endregion

            #region Amps
            AmpVM ring = new AmpVM();
            AmpVM glove = new AmpVM();
            AmpVM wand = new AmpVM();
            AmpVM scepter = new AmpVM();
            AmpVM staff = new AmpVM();
            AmpVM circlet = new AmpVM();

            ring.Name = "Ring";
            ring.Accuracy = 2;
            ring.Damage = -2;
            ring.Charges = 3;
            ring.Size = 0;
            ring.Cost = 750;
            ring.Range = Range.Pistol;
            ring.Type = DamageType.Untyped;
            ring.Properties = AmpProperty.Infusing;

            glove.Name = "Glove";
            glove.Accuracy = -1;
            glove.Damage = 4;
            glove.Charges = 4;
            glove.Size = 1;
            glove.Cost = 500;
            glove.Range = Range.Melee;
            glove.Type = DamageType.Bludgeoning;
            glove.Properties = AmpProperty.Battering;

            wand.Name = "Wand";
            wand.Accuracy = 1;
            wand.Damage = 0;
            wand.Charges = 4;
            wand.Size = 2;
            wand.Cost = 300;
            wand.Range = Range.Pistol;
            wand.Type = DamageType.Force;
            wand.Properties = AmpProperty.Dueling;

            scepter.Name = "Scepter";
            scepter.Accuracy = 0;
            scepter.Damage = 2;
            scepter.Charges = 5;
            scepter.Size = 3;
            scepter.Cost = 500;
            scepter.Range = Range.Melee | Range.TenFootRadius;
            scepter.Type = DamageType.Psychic;
            scepter.Properties = AmpProperty.Destructive;

            staff.Name = "Staff";
            staff.Accuracy = -2;
            staff.Damage = 6;
            staff.Charges = 5;
            staff.Size = 4;
            staff.Cost = 750;
            staff.Range = Range.SMG | Range.FiveFootRadius;
            staff.Type = DamageType.Force;
            staff.Properties = AmpProperty.Reaching;

            circlet.Name = "Circlet";
            circlet.Accuracy = 0;
            circlet.Damage = 2;
            circlet.Charges = 3;
            circlet.Size = 1;
            circlet.Cost = 500;
            circlet.Range = Range.FifteenFootCone;
            circlet.Type = DamageType.Psychic;
            circlet.Properties = AmpProperty.Compulsive;

            baseAmps.Add(ring);
            baseAmps.Add(glove);
            baseAmps.Add(wand);
            baseAmps.Add(scepter);
            baseAmps.Add(staff);
            baseAmps.Add(circlet);
            #endregion

            #region Armors
            ArmorVM vest = new ArmorVM();
            ArmorVM breastplate = new ArmorVM();
            ArmorVM battlesuit = new ArmorVM();
            ArmorVM plate = new ArmorVM();
            ArmorVM mageforged = new ArmorVM();
            ArmorVM combatshell = new ArmorVM();

            vest.Name = "Armored Vest";
            vest.Class = "Light";
            vest.Type = "Flexible";
            vest.ArmorValue = 2;
            vest.ArmorPenalty = 1;
            vest.SpeedPenalty = 0;
            vest.Cost = 300;
            vest.Properties = ArmorProperty.Innocuous;

            breastplate.Name = "Breastplate";
            breastplate.Class = "Light";
            breastplate.Type = "Rigid";
            breastplate.ArmorValue = 3;
            breastplate.ArmorPenalty = 1;
            breastplate.SpeedPenalty = 1;
            breastplate.Cost = 300;
            breastplate.Properties = ArmorProperty.Sturdy;

            battlesuit.Name = "Battle Suit";
            battlesuit.Class = "Heavy";
            battlesuit.Type = "Flexible";
            battlesuit.ArmorValue = 5;
            battlesuit.ArmorPenalty = 2;
            battlesuit.SpeedPenalty = 1;
            battlesuit.Cost = 1500;
            battlesuit.Properties = ArmorProperty.Versatile;

            plate.Name = "Plate Armor";
            plate.Class = "Heavy";
            plate.Type = "Rigid";
            plate.ArmorValue = 6;
            plate.ArmorPenalty = 2;
            plate.SpeedPenalty = 2;
            plate.Cost = 1500;
            plate.Properties = ArmorProperty.Encompassing;

            mageforged.Name = "Mageforged";
            mageforged.Class = "Advanced";
            mageforged.Type = "Flexible";
            mageforged.ArmorValue = 8;
            mageforged.ArmorPenalty = 3;
            mageforged.SpeedPenalty = 2;
            mageforged.Cost = 7500;
            mageforged.Properties = ArmorProperty.Fused;

            combatshell.Name = "Combat Shell";
            combatshell.Class = "Advanced";
            combatshell.Type = "Rigid";
            combatshell.ArmorValue = 9;
            combatshell.ArmorPenalty = 3;
            combatshell.SpeedPenalty = 3;
            combatshell.Cost = 7500;
            combatshell.Properties = ArmorProperty.CombatArmature;

            baseArmors.Add(vest);
            baseArmors.Add(breastplate);
            baseArmors.Add(battlesuit);
            baseArmors.Add(plate);
            baseArmors.Add(mageforged);
            baseArmors.Add(combatshell);

            #endregion
        }
        private void InitializeWeaponMods()
        {
            weaponMods = new ObservableCollection<WeaponModVM>();

            #region WeaponMods
            WeaponModVM Channeling = new WeaponModVM();
            Channeling.Name = "Channeling";
            Channeling.Cost = 750;
            Channeling.Effect = "The weapon counts as an Amp and a weapon.  When wielded as an Amp it has 4 charges and uses the weapon's accuracy and 1/2 of its damage modifier.";
            weaponMods.Add(Channeling);

            WeaponModVM Destructive = new WeaponModVM();
            Destructive.Name = "Destructive";
            Destructive.Cost = 750;
            Destructive.Effect = "[2 Stamina] The weapon gains +2 damage for your next attack.";
            weaponMods.Add(Destructive);

            WeaponModVM Elemental = new WeaponModVM();
            Elemental.Name = "Elemental";
            Elemental.Cost = 500;
            Elemental.Effect = "[4/1 Stamina] When active, the weapon gains the {Fire, Electricity, Acid or Cold (Choose 1 when mod is applied)} damage type.";
            weaponMods.Add(Elemental);

            WeaponModVM Hungry = new WeaponModVM();
            Hungry.Name = "Hungry";
            Hungry.Cost = 750;
            Hungry.Effect = "The weapon gains +1 to its Vicious property";
            weaponMods.Add(Hungry);

            WeaponModVM Hurling = new HurlingModVM();
            Hurling.Name = "Hurling";
            Hurling.Cost = 1000;
            Hurling.Effect = "The Weapon gains the thrown property. (Note: You should take the Aerodynamic mod instead!)";
            weaponMods.Add(Hurling);

            WeaponModVM LightShielding = new WeaponModVM();
            LightShielding.Name = "Light Shielding";
            LightShielding.Cost = 500;
            LightShielding.Effect = "[4/1 Stamina] While active, provides light cover with a toughness of 2 or if added to a shield, increases the shield's toughness by 2.";
            weaponMods.Add(LightShielding);

            WeaponModVM Lucky = new WeaponModVM();
            Lucky.Name = "Lucky";
            Lucky.Cost = 500;
            Lucky.Effect = "When making attacks with the weapon, both 1s and 2s can be rerolled using spirit.";
            weaponMods.Add(Lucky);

            WeaponModVM PowerRune = new WeaponModVM();
            PowerRune.Name = "Power Rune";
            PowerRune.Cost = 1000;
            PowerRune.Effect = "Activation of attack powers used with the weapon are at -1 stamina cost.";
            weaponMods.Add(PowerRune);

            WeaponModVM Seeking = new WeaponModVM();
            Seeking.Name = "Seeking";
            Seeking.Cost = 1500;
            Seeking.Effect = "[4/1 Stamina] While active, the weapon gains +1 accuracy.";
            weaponMods.Add(Seeking);

            MeleeWeaponModVM Aerodynamic = new MeleeWeaponModVM();
            Aerodynamic.Name = "Aerodynamic";
            Aerodynamic.Cost = 500;
            Aerodynamic.Effect = "The weapon gains the thrown property.  If the weapon already has the thrown property, double the max range.";
            weaponMods.Add(Aerodynamic);

            BalancedModVM Balanced = new BalancedModVM();
            Balanced.Name = "Balanced";
            Balanced.Cost = 100;
            Balanced.Effect = "The weapon gains the thrown property.";
            weaponMods.Add(Balanced);

            WeaponModVM Collapsing = new WeaponModVM();
            Collapsing.Name = "Collapsing";
            Collapsing.Cost = 350;
            Collapsing.Effect = "The weapon gains a collapsed state.  While in this state, its size is reduced by 2 but it cannot be used as a weapon.  Spend 3 MI to convert it between the origial and collapsed states.";
            weaponMods.Add(Collapsing);

            PistolModVM CombatButtStock = new PistolModVM();
            CombatButtStock.Name = "Combat Butt Stock";
            CombatButtStock.Cost = 200;
            CombatButtStock.Effect = "Change the range profile of the weapon to SMG range.  Increase the size of the weapon by 2.";
            weaponMods.Add(CombatButtStock);

            PBSModVM Cruel = new PBSModVM();
            Cruel.Name = "Cruel";
            Cruel.Cost = 650;
            Cruel.Effect = "The weapon gains +1 to CM.";
            weaponMods.Add(Cruel);

            MeleeWeaponModVM Elongated = new MeleeWeaponModVM();
            Elongated.Name = "Elongated";
            Elongated.Cost = 200;
            Elongated.Effect = "The weapon gains the reach property.";
            weaponMods.Add(Elongated);

            CylIModVM ExpandedAmmo = new CylIModVM();
            ExpandedAmmo.Name = "Expanded Ammo";
            ExpandedAmmo.Cost = 500;
            ExpandedAmmo.Effect = "Increase the ammo capacity of the weapon by 50%.";
            weaponMods.Add(ExpandedAmmo);

            FirearmsModVM ExtendedBarrel = new FirearmsModVM();
            ExtendedBarrel.Name = "Extended Barrel";
            ExtendedBarrel.Cost = 500;
            ExtendedBarrel.Effect = "Increase the size of the weapon by 1 and double its max range.";
            weaponMods.Add(ExtendedBarrel);

            WeaponModVM Guards = new WeaponModVM();
            Guards.Name = "Guards";
            Guards.Cost = 500;
            Guards.Effect = "The weapon gains the defensive property if it did not already have it.";
            weaponMods.Add(Guards);

            WeaponModVM Innocuous = new WeaponModVM();
            Innocuous.Name = "Innocuous";
            Innocuous.Cost = 250;
            Innocuous.Effect = "When concealed, increase the MCR to locate the weapon by 2.";
            weaponMods.Add(Innocuous);

            CylIModVM MagFeed = new CylIModVM();
            MagFeed.Name = "MagFeed";
            MagFeed.Cost = 500;
            MagFeed.Effect = "The weapon becomes magazine fed and the base ammo capacity becomes 5.";
            weaponMods.Add(MagFeed);

            WeaponModVM MagecellPowered = new WeaponModVM();
            MagecellPowered.Name = "Magecell-Powered";
            MagecellPowered.Cost = 1000;
            MagecellPowered.Effect = "No effect.";
            weaponMods.Add(MagecellPowered);

            WeaponModVM Oversized = new WeaponModVM();
            Oversized.Name = "Oversized";
            Oversized.Cost = 500;
            Oversized.Effect = "Increase the size and damage of the weapon by 2.  Reduce the accuracy of the weapon by 1.";
            weaponMods.Add(Oversized);

            FirearmsModVM Porting = new FirearmsModVM();
            Porting.Name = "Porting";
            Porting.Cost = 500;
            Porting.Effect = "Increases the rate of fire of semi-auto weapons by 1.  Increases the rate of fire of full-auto weapons by 5.";
            weaponMods.Add(Porting);

            LRifleShotgunModVM TacticalConversion = new LRifleShotgunModVM();
            TacticalConversion.Name = "Tactical Conversion";
            TacticalConversion.Cost = 250;
            TacticalConversion.Effect = "Change the range profile of the weapon to SMG range.";
            weaponMods.Add(TacticalConversion);

            WeaponModVM Undersized = new WeaponModVM();
            Undersized.Name = "Undersized";
            Undersized.Cost = 100;
            Undersized.Effect = "Increase the accuracy of the weapon by 1.  Reduce the size and damage of the weapon by 2.";
            weaponMods.Add(Undersized);

            WeaponModVM Bonded = new WeaponModVM();
            Bonded.Name = "Bonded";
            Bonded.Cost = 2500;
            Bonded.Effect = "[2 Stamina] The weapon can be called to you by a mental command as a quick action from up to 1000 ft away.  It will travel 500 ft per turn to deliver itself to your hand, automatically avoiding any obstacles between itself and you.  Additionally, you cannot be disarmed of this weapon and the weapon can be armed from a holster for 1 less MI than normal.";
            weaponMods.Add(Bonded);

            RequiresPowerRuneModVM Deadly = new RequiresPowerRuneModVM();
            Deadly.Name = "Deadly";
            Deadly.Cost = 3500;
            Deadly.Effect = "Increase the damage of the weapon by 2.  Requires Power Rune.";
            weaponMods.Add(Deadly);

            WeaponModVM ElementalShroud = new WeaponModVM();
            ElementalShroud.Name = "Elemental Shroud";
            ElementalShroud.Cost = 4000;
            ElementalShroud.Effect = "[6/2 Stamina] When active, the weapon gains +2 damage and the {Fire, Electricity, Acid or Cold (Choose 1 when mod is applied)} damage type.";
            weaponMods.Add(ElementalShroud);

            WeaponModVM HeavyPowerRune = new WeaponModVM();
            HeavyPowerRune.Name = "Heavy Power Rune";
            HeavyPowerRune.Cost = 5000;
            HeavyPowerRune.Effect = "Activation of manuvers, attack augments and enhancements used with/on the weapon are at -1 stamina cost.";
            weaponMods.Add(HeavyPowerRune);

            WeaponModVM Returning = new WeaponModVM();
            Returning.Name = "Returning";
            Returning.Cost = 3000;
            Returning.Effect = "The weapon gains +1 CM and if not armed, returns to the user at the end of each turn.";
            weaponMods.Add(Returning);

            WeaponModVM Shielding = new WeaponModVM();
            Shielding.Name = "Shielding";
            Shielding.Cost = 2000;
            Shielding.Effect = "[6/2 Stamina] While active, provides light cover with a toughness of 4 or if added to a shield, increases the shield's toughness by 2.";
            weaponMods.Add(Shielding);

            WeaponModVM Transforming = new WeaponModVM();
            Transforming.Name = "Transforming";
            Transforming.Cost = 4000;
            Transforming.Effect = "The weapon can be transformed into another weapon, chosen when the mod is installed.  All other mods function in the new form, if applicable.  Transforming the weapon is done as a quick action.";
            weaponMods.Add(Transforming);

            FirearmsModVM AdditionalFireMode = new FirearmsModVM();
            AdditionalFireMode.Name = "Additional Fire Mode";
            AdditionalFireMode.Cost = 1500;
            AdditionalFireMode.Effect = "Fire Mode SS -> SA2, SA(x) -> BF(x) or SA(x) -> FA(4x).";
            weaponMods.Add(AdditionalFireMode);

            WeaponModVM Attached = new WeaponModVM();
            Attached.Name = "Attached";
            Attached.Cost = 1500;
            Attached.Effect = "Combines the weapon with another weapon of at least 2 sizes smaller.  Add half the smaller weapon's size to this weapon's size.  When the user is armed with one weapon they are considered to be armed with both.";
            weaponMods.Add(Attached);

            FirearmsModVM CaselessAmmo = new FirearmsModVM();
            CaselessAmmo.Name = "Caseless Ammunition";
            CaselessAmmo.Cost = 3000;
            CaselessAmmo.Effect = "The ammunition capacity of the weapon increases by 25%.  Increases the rate of fire of semi-auto weapons by 1.  Increases the rate of fire of full-auto weapons by 5.  Double the cost of all ammunition and magazines.";
            weaponMods.Add(CaselessAmmo);

            WeaponModVM CustomBalance = new WeaponModVM();
            CustomBalance.Name = "Custom Balance";
            CustomBalance.Cost = 3500;
            CustomBalance.Effect = "Increase the accuracy of the weapon by 1.";
            weaponMods.Add(CustomBalance);

            WeaponModVM Ejecting = new MeleeWeaponModVM();
            Ejecting.Name = "Ejecting";
            Ejecting.Cost = 1000;
            Ejecting.Effect = "Weapons with this mod can be used to make a Weapon {Ranged (Pistol)/Ballistic+Weapon} attack using the normal weapon's skill.  Once used in this manner, the weapon cannot be used to make melee attacks (except improvised) until the ejected portion is rearmed (a combat action)";
            weaponMods.Add(Ejecting);

            PBSModVM HammeringOrVibro = new PBSModVM();
            HammeringOrVibro.Name = "Hammering/Vibro";
            HammeringOrVibro.Cost = 3500;
            HammeringOrVibro.Effect = "Weapon gains Lethal 1.";
            weaponMods.Add(HammeringOrVibro);

            FirearmsModVM HeavyBore = new FirearmsModVM();
            HeavyBore.Name = "Heavy Bore";
            HeavyBore.Cost = 1500;
            HeavyBore.Effect = "Increase the damage and size of the weapon by 2.  Decrease the accuracy of the weapon by 1.  Reduce the ammo capacity of the weapon by 25%.  This mod can be applied a max of 2 times.";
            weaponMods.Add(HeavyBore);

            WeaponModVM HeavyMagecell = new WeaponModVM();
            HeavyMagecell.Name = "Heavy Magecell-Powered";
            HeavyMagecell.Cost = 1500;
            HeavyMagecell.Effect = "If the weapon has one or more mods which require Magecell-Powered, the weapon gains +1 damage.";
            weaponMods.Add(HeavyMagecell);

            CrossbowModVM Multishot = new CrossbowModVM();
            Multishot.Name = "Multi-Shot";
            Multishot.Cost = 1000;
            Multishot.Effect = "Double the ammo capacity of a crossbow.  This mod can be applied 2 times.";
            weaponMods.Add(Multishot);

            RequiresMagecellPoweredVM PoweredCollapsing = new RequiresMagecellPoweredVM();
            PoweredCollapsing.Name = "Powered Collapsing";
            PoweredCollapsing.Cost = 1500;
            PoweredCollapsing.Effect = "The weapon gains a collapsed state.  While in this state, its size is reduced by 2 but it cannot be used as a weapon.  Spend a quick action to convert it between the origial and collapsed states.  Requires Magecell-Powered.";
            weaponMods.Add(PoweredCollapsing);

            PoweredDrawModVM PoweredDraw = new PoweredDrawModVM();
            PoweredDraw.Name = "Powered Draw";
            PoweredDraw.Cost = 3500;
            PoweredDraw.Effect = "The weapon gains Lethal 1.  Requies Magecell-Powered.  Bows only.";
            weaponMods.Add(PoweredDraw);

            RequiresPoweredCollapsingVM Reaching = new RequiresPoweredCollapsingVM();
            Reaching.Name = "Reaching";
            Reaching.Cost = 2000;
            Reaching.Effect = "The weapon gains the reach property and +1 to attack, damage and crit mod on reaction attacks from openings.  Requires Powered Collapsing.";
            weaponMods.Add(Reaching);

            FirearmsModVM ShockPad = new FirearmsModVM();
            ShockPad.Name = "Shock Pad";
            ShockPad.Cost = 1500;
            ShockPad.Effect = "The weapon gains +1 to attack with FA attacks vs targets allocated 2 or more groupings.";
            weaponMods.Add(ShockPad);

            RequiresHeavyPowerRuneModVM Annihilation = new RequiresHeavyPowerRuneModVM();
            Annihilation.Name = "Annihilation";
            Annihilation.Cost = 15000;
            Annihilation.Effect = "The weapon gains vicious +1.  Damage die rolls of 1 or 2 can be rerolled.  Requires Heavy Power Rune.";
            weaponMods.Add(Annihilation);

            EnergyFormModVM EnergyForm = new EnergyFormModVM();
            EnergyForm.Name = "Energy Form";
            EnergyForm.Cost = 25000;
            EnergyForm.Effect = "[8/2 Stamina] While active, energy form weapons lose all normal damage types and gain the {Holy, Unholy, Acid, Lightning, Fire or Cold damage type (chosen when the mod is applied)}.  Additonally, the weapon gains +2 damage and CM.  While active, a bow with this mod will consume no ammunition and has no manual reload cost.  Any tech mod with Magecell or Heavy Magecell requirments do not function while this mod is active.  Melee and Bows only.  Requires Heavy Power Rune.";
            weaponMods.Add(EnergyForm);

            WeaponModVM Enlivening = new WeaponModVM();
            Enlivening.Name = "Enlivening";
            Enlivening.Cost = 20000;
            Enlivening.Effect = "[8 Stamina] Triggered action, when you score a crit with this weapon you regain 4HP in your first damage track or 1HP in your second damage track.  The total amount of health regained in the 2nd damage track is limited to your level per 24 hours.";
            weaponMods.Add(Enlivening);

            WeaponModVM Fortuitous = new WeaponModVM();
            Fortuitous.Name = "Fortuitous";
            Fortuitous.Cost = 20000;
            Fortuitous.Effect = "When making attacks with the weapon, 1s, 2s and 3s can be rerolled using spirit";
            weaponMods.Add(Fortuitous);

            RequiresPowerRuneModVM GreaterShielding = new RequiresPowerRuneModVM();
            GreaterShielding.Name = "Greater Shielding";
            GreaterShielding.Cost = 15000;
            GreaterShielding.Effect = "[10/3 Stamina] While active, grants Heavy cover with a toughness of 6.  Requires Power Rune.";
            weaponMods.Add(GreaterShielding);

            WeaponModVM Phasing = new WeaponModVM();
            Phasing.Name = "Phasing";
            Phasing.Cost = 25000;
            Phasing.Effect = "[8 Stamina] Your next attack with the weapon ignores all cover.  If no LOS to target, the attack is still blind.";
            weaponMods.Add(Phasing);

            WeaponModVM SpellStoring = new WeaponModVM();
            SpellStoring.Name = "Spell Storing";
            SpellStoring.Cost = 20000;
            SpellStoring.Effect = "The weapon can store Maneuver spell(s) with a total stamina cost of 12 or less (before any applicable cost reductions).  Spells are stored into the weapon by casting the spell then touching the weapon (note down the attack and damage modifiers at the time of casting).  Once stored, the spell(s) may be cast as a combat action using the original attack and damage modifiers or dismissed as a quick action to free up the spell storing capacity.";
            weaponMods.Add(SpellStoring);

            WeaponModVM Summoned = new WeaponModVM();
            Summoned.Name = "Summoned";
            Summoned.Cost = 10000;
            Summoned.Effect = "When this mod is applied, the weapon is keyed to you.  As a free action you can summon the weapon from anywhere on Kython so long as teleportation between the two locations is possible.  You cannot be disarmed of the weapon.";
            weaponMods.Add(Summoned);

            EnergyFieldModVM EnergyField = new EnergyFieldModVM();
            EnergyField.Name = "Energy Field";
            EnergyField.Cost = 20000;
            EnergyField.Effect = "The weapon gains +1 to damage, CM, Lethal and Vicious properties.  Requires Heavy Magecell-Powered";
            weaponMods.Add(EnergyField);

            RequiresHeavyMagecellPoweredModVM Hellfire = new RequiresHeavyMagecellPoweredModVM();
            Hellfire.Name = "Hellfire";
            Hellfire.Cost = 25000;
            Hellfire.Effect = "Increase the FA rate of fire by 15.  Increase the size of the weapon by 1.  Requires Heavy Magecell-Powered.";
            weaponMods.Add(Hellfire);

            HighTechModVM Laser = new HighTechModVM();
            Laser.Name = "Laser";
            Laser.Cost = 25000;
            Laser.Effect = "The weapon causes slashing damage and gains the AP property.  Firearm only.  Cannot be combined with caseless ammo mod.  Requires Magecell-Powered.";
            weaponMods.Add(Laser);

            HighTechModVM ParticleBeam = new HighTechModVM();
            ParticleBeam.Name = "Particle Beam";
            ParticleBeam.Cost = 35000;
            ParticleBeam.Effect = "The weapon loses all damage types.  The weapon gains the AP property and +1 CM.  Firearm only.  Cannot be combined with Laser, Caseless or Plasma mods.  Requires Heavy Magecell-Powered.";
            weaponMods.Add(ParticleBeam);

            WeaponModVM PerfectlyEngineered = new WeaponModVM();
            PerfectlyEngineered.Name = "PerfectlyEngineered";
            PerfectlyEngineered.Cost = 50000;
            PerfectlyEngineered.Effect = "The weapon gains +1 accuracy, +1 CM and +2 damage.";
            weaponMods.Add(PerfectlyEngineered);

            HighTechModVM Plasma = new HighTechModVM();
            Plasma.Name = "Plasma";
            Plasma.Cost = 45000;
            Plasma.Effect = "The weapon now causes fire and ballistic damage as well as gaining Lethal 1, CM +2.  Plasma weapons can only be fired in SS or SA modes.  Canot be combined with Laser mod.  Requires Heavy Magecell-Powered.";
            weaponMods.Add(Plasma);

            //HACK
            WeaponModVM[] modArray = new WeaponModVM[weaponMods.Count];
            weaponMods.CopyTo(modArray, 0);
            var costSortedMods = modArray.OrderBy(wm => wm.Cost);
            weaponMods.Clear();
            foreach (WeaponModVM w in costSortedMods)
            {
                weaponMods.Add(w);
            }
            #endregion

            ampMods = new ObservableCollection<ViewModel.AmpModVM>();

            #region AmpMods
            AmpModVM cruel = new AmpModVM();
            cruel.Name = "Cruel";
            cruel.Cost = 650;
            cruel.Effect = "+1 to CM for all Spells cast using this Amp.";
            ampMods.Add(cruel);

            AmpModVM destructive = new AmpModVM();
            destructive.Name = "Destructive";
            destructive.Cost = 750;
            destructive.Effect = "Gain +2 damage for your next attack. [2 Stamina]";
            ampMods.Add(destructive);

            AmpModVM imbued = new AmpModVM();
            imbued.Name = "Imbued";
            imbued.Cost = 500;
            imbued.Effect = "The Charges of this Amp are increased by 3.";
            ampMods.Add(imbued);

            RequiresPowerRuneAmpModVM lesserSpellpower = new RequiresPowerRuneAmpModVM();
            lesserSpellpower.Name = "Lesser Spellpower";
            lesserSpellpower.Cost = 1000;
            lesserSpellpower.Effect = "Treat your Presence Attribute as 3 when determining the damage of spells cast through this Amp. Requires Power Rune.";
            ampMods.Add(lesserSpellpower);

            AmpModVM lucky = new AmpModVM();
            lucky.Name = "Lucky";
            lucky.Cost = 500;
            lucky.Effect = "You can treat results of 1 and 2 as 1s for the purpose of Spirit re-rolls when making attacks with this Amp.";
            ampMods.Add(lucky);

            AmpModVM powerRune = new AmpModVM();
            powerRune.Name = "Power Rune";
            powerRune.Cost = 1000;
            powerRune.Effect = "Required for certain other Mods. Activation of attack powers used with the Amp are at a -1 to Stamina cost.";
            ampMods.Add(powerRune);

            AmpModVM reaching = new AmpModVM();
            reaching.Name = "Reaching";
            reaching.Cost = 500;
            reaching.Effect = "The Amp gains the Reaching property if it did not already possess it.";
            ampMods.Add(reaching);

            StaffModVM stunted = new StaffModVM();
            stunted.Name = "Stunted";
            stunted.Cost = 250;
            stunted.Effect = "The Amp loses the Reaching property. Staff only.";
            ampMods.Add(stunted);

            AmpModVM wild = new AmpModVM();
            wild.Name = "Wild";
            wild.Cost = 500;
            wild.Effect = "Spells cast through the Amp gain +1 Vicious.";
            ampMods.Add(wild);

            AmpModVM bonded = new AmpModVM();
            bonded.Name = "Bonded";
            bonded.Cost = 2500;
            bonded.Effect = "The Amp can be called to you by a mental command as a Quick Action [2 Stamina] from up to 1,000’ away. The Amp will travel 500’ per turn to deliver itself to your hand, automatically avoiding any obstacles between itself and you. Additionally, you cannot be disarmed of this Amp and the Amp can be Armed from its Holster for 1 less MI than its Holster indicates.";
            ampMods.Add(bonded);

            RequiresPowerRuneAmpModVM deadly = new RequiresPowerRuneAmpModVM();
            deadly.Name = "Deadly";
            deadly.Cost = 3500;
            deadly.Effect = "Deadly: Increase the base damage of the Amp by 2. Requires: Power Rune.";
            ampMods.Add(deadly);

            RequiresPowerRuneAmpModVM greaterImbued = new RequiresPowerRuneAmpModVM();
            greaterImbued.Name = "Greater Imbued";
            greaterImbued.Cost = 5000;
            greaterImbued.Effect = "The Charges of this Amp are increased by 7. Requires Power Rune.";
            ampMods.Add(greaterImbued);

            AmpModVM heavyPowerRune = new AmpModVM();
            heavyPowerRune.Name = "Heavy Power Rune";
            heavyPowerRune.Cost = 5000;
            heavyPowerRune.Effect = "Reduce the Stamina cost of all Maneuvers or Attack Augments used through this Amp by 1.";
            ampMods.Add(heavyPowerRune);

            AmpModVM homing = new AmpModVM();
            homing.Name = "Homing";
            homing.Cost = 3500;
            homing.Effect = "The Accuracy of the Amp is increased by 1.";
            ampMods.Add(homing);

            RequiresPowerRuneAmpModVM spellpower = new RequiresPowerRuneAmpModVM();
            spellpower.Name = "Spellpower";
            spellpower.Cost = 4000;
            spellpower.Effect = "The caster treats his Presence Attribute as 5 when determining the damage of his spells cast through this Amp.Requires Power Rune.";
            ampMods.Add(spellpower);

            AmpModVM terrible = new AmpModVM();
            terrible.Name = "Terrible";
            terrible.Cost = 3500;
            terrible.Effect = "Spells cast through this Amp become Lethal 1. Requires Power Rune.";
            ampMods.Add(terrible);

            AmpModVM transforming = new AmpModVM();
            transforming.Name = "Transforming";
            transforming.Cost = 4000;
            transforming.Effect = "This Amp can be Transformed from 1 Amp to another.All Mods remain intact but fail to function if the Mod does not work on the Amp’s new form. Transforming Amps have only 1 alternate form that is selected when this Mod is installed.Transforming the Amp between its 2 forms is done as a Quick Action.";
            ampMods.Add(transforming);

            AmpModVM transmuting = new AmpModVM();
            transmuting.Name = "Transmuting";
            transmuting.Cost = 3000;
            transmuting.Effect = "When this Mod is installed, 2 damage types are selected (i.e.Fire and Acid). When you make an attack that contains 1 of these damage types, that damage type can be replaced for the other type selected for the attack.";
            ampMods.Add(transmuting);

            RequiresHeavyPowerRuneAmpModVM annihilation = new RequiresHeavyPowerRuneAmpModVM();
            annihilation.Name = "Annihilation";
            annihilation.Cost = 15000;
            annihilation.Effect = "Gain Vicious +1; when using Vicious on this weapon, die results of 2 can be re - rolled as if they were 1’s. Requires Heavy Power Rune.";
            ampMods.Add(annihilation);

            RequiresHeavyPowerRuneAmpModVM eternal = new RequiresHeavyPowerRuneAmpModVM();
            eternal.Name = "Eternal";
            eternal.Cost = 45000;
            eternal.Effect = "This Amp does not have a Charges Attribute and does not have a limit to the number of times it can be used before recharging. All Spells and Enhancements cast through Eternal Amps have their Stamina cost reduced by 1 to activate and maintain. Requires Heavy Power Rune.";
            ampMods.Add(eternal);

            AmpModVM fortuitous = new AmpModVM();
            fortuitous.Name = "Fortuitous";
            fortuitous.Cost = 20000;
            fortuitous.Effect = "You can treat any roll of 1, 2, or 3 as a result of 1 for the purpose of using Spirit re-rolls.";
            ampMods.Add(fortuitous);

            RequiresHeavyPowerRuneAmpModVM greaterSpellpower = new RequiresHeavyPowerRuneAmpModVM();
            greaterSpellpower.Name = "Greater Spellpower";
            greaterSpellpower.Cost = 35000;
            greaterSpellpower.Effect = "You treat your Presence Attribute as 7 when determining the damage of your spells cast through this Amp. Requires Heavy Power Rune.";
            ampMods.Add(greaterSpellpower);

            RequiresHeavyPowerRuneAmpModVM powerswell = new RequiresHeavyPowerRuneAmpModVM();
            powerswell.Name = "Powerswell Rune";
            powerswell.Cost = 15000;
            powerswell.Effect = "You can maintain a single spell that was cast through this Amp with a maintenance cost of 5 or less, without spending Stamina. Requires Heavy Power Rune.";
            ampMods.Add(powerswell);

            RequiresHeavyPowerRuneAmpModVM sacrifice = new RequiresHeavyPowerRuneAmpModVM();
            sacrifice.Name = "Sacrifice Rune";
            sacrifice.Cost = 30000;
            sacrifice.Effect = "As a Quick Action, you can lose a number of HP up to your Presence Attribute in your 2nd Damage Track. In exchange, you gain triple that number in Stamina.Any Stamina in excess of your normal maximum is lost at the end of your current turn. Requires Heavy Power Rune.";
            ampMods.Add(sacrifice);

            AmpModVM spellStoring = new AmpModVM();
            spellStoring.Name = "Spell Storing";
            spellStoring.Cost = 20000;
            spellStoring.Effect = "Store attack spells with a with a total Stamina cost of no more than 20 (before caster cost reductions). The spells are placed into the weapon by casting the spell and touching the weapon. Stored spells are stored until expended or dismissed. To expend the spell, spend a Combat Action and attack as if you had cast the spell (using the caster’s original attack and damage). To dismiss the spell, spend a Quick Action. The weapon glows a brilliant color and the space is then free to store additional spells.";
            ampMods.Add(spellStoring);

            ampMods.OrderBy(am => am.Name);
            #endregion

            armorMods = new ObservableCollection<ArmorModVM>();

            #region
            ArmorModVM cloaked = new ArmorModVM();
            cloaked.Name = "Cloaked";
            cloaked.Cost = 1000;
            cloaked.Effect = "Cloaked (as the Illusion): Activate to become Cloaked for 1 round. [4 Stamina]";
            armorMods.Add(cloaked);

            ArmorModVM invigorating = new ArmorModVM();
            invigorating.Name = "Invigorating";
            invigorating.Cost = 1000;
            invigorating.Effect = "Gain +1 HP per track and +2 Stamina";
            armorMods.Add(invigorating);

            ArmorModVM powerRune1 = new ArmorModVM();
            powerRune1.Name = "Power Rune";
            powerRune1.Cost = 1000;
            powerRune1.Effect = "Required for certain other Mods. + 1 to Stamina Regen.";
            armorMods.Add(powerRune1);

            RequiresPowerRuneArmorModVM quick = new RequiresPowerRuneArmorModVM();
            quick.Name = "Quick";
            quick.Cost = 1000;
            quick.Effect = "Activate to gain the Hastened condition for 1 round. Requires Power Rune.";
            armorMods.Add(quick);

            ArmorModVM resistance = new ViewModel.ArmorModVM();
            resistance.Name = "Resistance";
            resistance.Cost = 650;
            resistance.Effect = "Gain Light Fortification.";
            armorMods.Add(resistance);

            ArmorModVM soulBond = new ViewModel.ArmorModVM();
            soulBond.Name = "Soul Bond";
            soulBond.Cost = 750;
            soulBond.Effect = "Gain +1 to Long-Term Recovery and to your total HP in each Track.";
            armorMods.Add(soulBond);

            ArmorModVM enhancedMobility = new ViewModel.ArmorModVM();
            enhancedMobility.Name = "Enhanced Mobility";
            enhancedMobility.Cost = 500;
            enhancedMobility.Effect = "Reduces Speed Penalty by 1.";
            armorMods.Add(enhancedMobility);

            ArmorModVM extraProtection = new ViewModel.ArmorModVM();
            extraProtection.Name = "Extra Protection";
            extraProtection.Cost = 500;
            extraProtection.Effect = "Increase Armor Value by 1 and Speed Penalty by 1.";
            armorMods.Add(extraProtection);

            ArmorModVM holster = new ArmorModVM();
            holster.Name = "Holster";
            holster.Cost = 500;
            holster.Effect = "The armor gains storage for weapons up to a combined Size of 8.";
            armorMods.Add(holster);

            ArmorModVM lightFort = new ViewModel.ArmorModVM();
            lightFort.Name = "Light Fortification";
            lightFort.Cost = 650;
            lightFort.Effect = "Increase Armor vs. one damage type by 2.";
            armorMods.Add(lightFort);

            ArmorModVM lightWMount = new ViewModel.ArmorModVM();
            lightWMount.Name = "Light Weapon Mount";
            lightWMount.Cost = 300;
            lightWMount.Effect = "Mount a Size 3 or smaller weapon onto the armor. You are always considered Armed when wearing the armor. Any attempts to Conceal the weapon incur a Concealment penalty of + 4.";
            armorMods.Add(lightWMount);

            ArmorModVM mcPowered = new ViewModel.ArmorModVM();
            mcPowered.Name = "Magecell-Powered";
            mcPowered.Cost = 1000;
            mcPowered.Effect = "Prerequisite for certain other Mods. You can Power Tether 1 weapon to the armor to meet the Magecell-Powered requirement for other Mods installed on that weapon(see Weapon Attachments on page 214).";
            armorMods.Add(mcPowered);

            ArmorModVM dimensionalHolster = new ViewModel.ArmorModVM();
            dimensionalHolster.Name = "Dimensional Holster";
            dimensionalHolster.Cost = 5000;
            dimensionalHolster.Effect = "Gain a Concealed Holster for weapons with Size of 6 or less. Mundane forms of detection will always fail to detect the space.A MCR 25 Alteration Check w will detect the presence of the space but not its contents.";
            armorMods.Add(dimensionalHolster);

            ArmorModVM fleet = new ViewModel.ArmorModVM();
            fleet.Name = "Fleet";
            fleet.Cost = 2500;
            fleet.Effect = "Gain +1 Speed and +5 to Jump Checks.";
            armorMods.Add(fleet);

            ArmorModVM hpRune = new ViewModel.ArmorModVM();
            hpRune.Name = "Heavy Power Rune";
            hpRune.Cost = 5000;
            hpRune.Effect = "This Mod is a requirement for certain other Mods and counts as a Power Rune for the purposes of meeting prerequisites. Gain + 2 to Stamina Regen.";
            armorMods.Add(hpRune);

            RequiresPowerRuneArmorModVM lifeSpring = new RequiresPowerRuneArmorModVM();
            lifeSpring.Name = "Life Spring";
            lifeSpring.Cost = 2500;
            lifeSpring.Effect = "Gain +4 Stamina and +1 to Resistance Checks. Requires Power Rune.";
            armorMods.Add(lifeSpring);

            RequiresPowerRuneArmorModVM phaseRune = new RequiresPowerRuneArmorModVM();
            phaseRune.Name = "Phase Rune";
            phaseRune.Cost = 5000;
            phaseRune.Effect = "Gain +5 to Athletics Checks to escape and ignore Rough terrain.You can Push Through for 1 additional MI, and you gain + 1 to Physical defenses.Requires Power Rune.";
            armorMods.Add(phaseRune);

            ArmorModVM retracting = new ViewModel.ArmorModVM();
            retracting.Name = "Retracting";
            retracting.Cost = 5000;
            retracting.Effect = "The armor can be dismissed or summoned as a Combat Action. While dismissed, the armor retracts into itself and becomes a somewhat innocuous version of itself. While retracted, the armor provides none of its armor or Mod benefits, and the effective Size of the armor becomes 1 for determining its Concealment modifier.";
            armorMods.Add(retracting);

            RequiresPowerRuneArmorModVM runeEtched = new RequiresPowerRuneArmorModVM();
            runeEtched.Name = "Rune-Etched";
            runeEtched.Cost = 3500;
            runeEtched.Effect = "Freely maintain 1 Enhancement cast on yourself with a Maintenance cost of 3 or less. +1 to damage with Spells. Requires Power Rune.";
            armorMods.Add(runeEtched);

            ArmorModVM spellWard = new ViewModel.ArmorModVM();
            spellWard.Name = "Spell Ward";
            spellWard.Cost = 3500;
            spellWard.Effect = "Increase your Armor by 2 vs. all Spells.";
            armorMods.Add(spellWard);

            ArmorModVM customFit = new ViewModel.ArmorModVM();
            customFit.Name = "Custom Fitted";
            customFit.Cost = 3500;
            customFit.Effect = "Reduces Armor Penalty by 1.";
            armorMods.Add(customFit);

            ArmorModVM enviroSeal = new ViewModel.ArmorModVM();
            enviroSeal.Name = "Environmental Seal";
            enviroSeal.Cost = 4000;
            enviroSeal.Effect = "Gain +4 to armor vs. Area attacks and environmental effects. The armor also gains a 1-hour sealed breathing supply.";
            armorMods.Add(enviroSeal);

            ArmorModVM fort = new ViewModel.ArmorModVM();
            fort.Name = "Fortification";
            fort.Cost = 3000;
            fort.Effect = "Increase your armor by 4 vs. 1 damage type; does not stack with other types of Fortification.";
            armorMods.Add(fort);

            ArmorModVM hMagecell = new ViewModel.ArmorModVM();
            hMagecell.Name = "Heavy Magecell";
            hMagecell.Cost = 2000;
            hMagecell.Effect = "Prerequisite for certain other Mods. You can Power Tether (see Weapon Attachments on page 214) weapons and other items to the armor to power them and to meet the Magecell-Powered or Heavy Magecell requirements for other Mods on those items. Counts as Magecell-Powered as well as Heavy Magecell for meeting requirements.";
            armorMods.Add(hMagecell);

            ArmorModVM jumpAssist = new ViewModel.ArmorModVM();
            jumpAssist.Name = "Jump Assist";
            jumpAssist.Cost = 4000;
            jumpAssist.Effect = "Gain +5 to Jump Checks and +1 to Speed. Requires Powered Servos.";
            armorMods.Add(jumpAssist);

            ArmorModVM jumpJets = new ViewModel.ArmorModVM();
            jumpJets.Name = "Jump Jets";
            jumpJets.Cost = 5000;
            jumpJets.Effect = "You add +30 to your next Jump Check.Requires Magecell-Powered. [6 Stamina]";
            armorMods.Add(jumpJets);

            RequiresMageCellPoweredArmorModVM lightShields = new RequiresMageCellPoweredArmorModVM();
            lightShields.Name = "Light Shields";
            lightShields.Cost = 6500;
            lightShields.Effect = "This Mod creates a protective bubble around you.Use a Quick Action to create a damage buffer with HP equal to 5 + your level. When you suffer damage from an attack or environmental effect, that damage is removed from this buffer before any damage can be inflicted on any of your Damage Tracks. This Mod has no effect on direct loss of HP. Damage taken to the buffer does not count as damage to you. This Mod must be activated during each encounter; leaving it on in between encounters will destroy the Mod. The damage buffer lasts until the end of the encounter. This Mod can only be activated once per encounter. Requires: Magecell-Powered.";
            armorMods.Add(lightShields);

            RequiresMageCellPoweredArmorModVM poweredServos = new RequiresMageCellPoweredArmorModVM();
            poweredServos.Name = "Powered Servos";
            poweredServos.Cost = 3000;
            poweredServos.Effect = "Gain +1 to Strength and an additional +1 to Li ing power. Requires Magecell-Powered.";
            armorMods.Add(poweredServos);

            ArmorModVM reinforcement = new ViewModel.ArmorModVM();
            reinforcement.Name = "Reinforcement";
            reinforcement.Cost = 3000;
            reinforcement.Effect = "Gain -1 to the CM of attacks against you, and add your level to your 3rd Damage Track.";
            armorMods.Add(reinforcement);

            RequiresPowerRuneArmorModVM conduitRune = new RequiresPowerRuneArmorModVM();
            conduitRune.Name = "Conduit Rune";
            conduitRune.Cost = 30000;
            conduitRune.Effect = "When you lose HP, regain spent Stamina equal to ¼ the HP lost. Requires Power Rune.";
            armorMods.Add(conduitRune);

            RequiresHeavyPowerRuneArmorModVM greaterSpellWard = new ViewModel.RequiresHeavyPowerRuneArmorModVM();
            greaterSpellWard.Name = "Greater Spell Ward";
            greaterSpellWard.Cost = 55000;
            greaterSpellWard.Effect = "Gain +4 Armor Value vs. Spells. Requires Heavy Power Rune.";
            armorMods.Add(greaterSpellWard);

            ArmorModVM powerful = new ViewModel.ArmorModVM();
            powerful.Name = "Powerful";
            powerful.Cost = 25000;
            powerful.Effect = "Gain +1 to an Attribute of your choice.";

            RequiresHeavyPowerRuneArmorModVM runeInscribed = new RequiresHeavyPowerRuneArmorModVM();
            runeInscribed.Name = "Rune Inscribed";
            runeInscribed.Cost = 20000;
            runeInscribed.Effect = "Gain 1 Slot for Runescribing in a body location of your choice.Gain access to the Augmentation only while the armor is worn. Requires Heavy Power Rune.";
            armorMods.Add(runeInscribed);

            ArmorModVM sacrificeRune = new ArmorModVM();
            sacrificeRune.Name = "Sacrifice Rune";
            sacrificeRune.Cost = 20000;
            sacrificeRune.Effect = "As a Quick Action, you can lose HP up to your Presence Attribute. In exchange, gain double that number in Stamina. Any Stamina in excess of your normal maximum is lost at the end of your current turn.";
            armorMods.Add(sacrificeRune);

            ArmorModVM summoned = new ViewModel.ArmorModVM();
            summoned.Name = "Summoned";
            summoned.Cost = 20000;
            summoned.Effect = "As a Quick Action, you can summon the armor from anywhere on Kython. The armor appears on you as if you had donned it, as long as Teleportation between the 2 places is possible. The armor comes with a location key, a small piece of the armor that can be placed at a location. Dismiss the armor as a Quick Action;             when dismissed the armor Teleports to the             location key until summoned. The armor is permanently keyed to you and the Mod must be removed and reapplied for it to be keyed to a new owner.If you are already wearing armor when you summon the armor, it takes the place of the worn armor until the summoned armor is dismissed.";
            armorMods.Add(summoned);

            ArmorModVM warded = new ViewModel.ArmorModVM();
            warded.Name = "Warded";
            warded.Cost = 15000;
            warded.Effect = "Gain Light Fortification vs. all Area and Resolve attacks.";
            armorMods.Add(warded);

            ArmorModVM advSystems = new ViewModel.ArmorModVM();
            advSystems.Name = "Advanced Systems";
            advSystems.Cost = 20000;
            advSystems.Effect = "You gain 1 Slot for Cybernetics in a body location of your choosing.Gain the effects of the Augmentation only while the armor is worn. Requires Powered Servos.";
            armorMods.Add(advSystems);

            ArmorModVM hFort = new ViewModel.ArmorModVM();
            hFort.Name = "Heavy Fortification";
            hFort.Cost = 40000;
            hFort.Effect = "Increase the armor’s Armor Value by 4 vs. 3 damage types of your choosing. This effect cannot be stacked with other types of Fortification.";
            armorMods.Add(hFort);

            HeavyServosArmorModVM hServos = new ViewModel.HeavyServosArmorModVM();
            hServos.Name = "Heavy Servos";
            hServos.Cost = 25000;
            hServos.Effect = "Gain +2 to Strength. Increase the Armor Penalty of the armor by 1 and the Speed Penalty of the armor by 2.Gain an additional +2 to Li ing power. Requires Heavy Power Cell; does not stack with Powered Servos.";
            armorMods.Add(hServos);

            RequiresHeavyMagecellPoweredArmorModVM hShields = new RequiresHeavyMagecellPoweredArmorModVM();
            hShields.Name = "Heavy Shields";
            hShields.Cost = 40000;
            hShields.Effect = "A protective bubble forms around you. Use a Quick Action to create a buffer with HP equal to 20 + double your level.When you suffer damage from an attack or environmental effect, HP are removed from this buffer before any damage can be in icted to any of your damage tracks. This Mod has no effect on direct loss of HP. Damage taken to the buffer does not count as damage to the user. This Mod must be activated during each encounter; leaving it on in between encounters will destroy the Mod. The damage buffer lasts until the end of the encounter. This Mod can only be activated once per encounter.Requires: Heavy Magecell.";
            armorMods.Add(hShields);

            RequiresPoweredServosArmorModVM powDon = new RequiresPoweredServosArmorModVM();
            powDon.Name = "Powered Donning";
            powDon.Cost = 15000;
            powDon.Effect = "Donning the armor is now a Combat Action.While stored, the armor is considered 2 Sizes smaller. Requires Powered Servos.";
            armorMods.Add(powDon);

            RequiresHeavyMagecellPoweredArmorModVM thrusters = new RequiresHeavyMagecellPoweredArmorModVM();
            thrusters.Name = "Thrusters";
            thrusters.Cost = 60000;
            thrusters.Effect = "Gain Flight and Overland Flight (as the Talents in Kinesis). Requires Heavy Magecell.";
            armorMods.Add(thrusters);

            #endregion
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
