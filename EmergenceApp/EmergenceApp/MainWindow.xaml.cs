using Emergence.Model;
using Emergence.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmergenceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ArmoryViewModel = new Armory();
            BarracksViewModel = new Barracks();
            LairViewModel = new Lair();

            DataContext DC = new Emergence.ViewModel.DataContext();
            DC.Armory = ArmoryViewModel;
            DC.Barracks = BarracksViewModel;
            DC.Lair = LairViewModel;
            this.DataContext = DC;
            InitializeComponent();
        }

        #region Armory
        Armory ArmoryViewModel;
        //Buy
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ArmoryViewModel.Buy(((Button)sender).CommandParameter);
        }

        //ApplyMod
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            WeaponModVM wm = (WeaponModVM)b.DataContext;
            WeaponVM w = b.CommandParameter as WeaponVM;
            if (w != null && wm.ApplyTo(w))
            {
            }
            else if (w == null)
            {
                MessageBox.Show("Select a weapon.");
            }
            else
            {
                MessageBox.Show(((WeaponModVM)((Button)sender).DataContext).ApplyError);
            }
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            ((WeaponVM)((Button)sender).DataContext).IncreaseQuality();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            ((WeaponVM)((Button)sender).DataContext).DecreaseQuality();
        }

        //Remove
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            WeaponModVM wm = (WeaponModVM)b.DataContext;
            WeaponVM w = b.CommandParameter as WeaponVM;
            wm.RemoveFrom(w);
        }

        //Randomize
        private void RandomizeWeapon(object sender, RoutedEventArgs e)
        {
            //Select random item
            int weaponIndex = new Random().Next(ArmoryViewModel.BaseWeapons.Count);
            baseWeaponList.SelectedIndex = weaponIndex;
            //select quality
            ((WeaponVM)baseWeaponList.SelectedItem).Mods.Clear();
            int quality = new Random().Next(4);
            while (quality < (int)((WeaponVM)baseWeaponList.SelectedItem).Quality)
            {
                ((WeaponVM)baseWeaponList.SelectedItem).DecreaseQuality();
            }
            while (quality > (int)((WeaponVM)baseWeaponList.SelectedItem).Quality)
            {
                ((WeaponVM)baseWeaponList.SelectedItem).IncreaseQuality();
            }
            //set mods
            while (quality > 0)
            {
                int modIndex = new Random().Next(ArmoryViewModel.WeaponMods.Count);
                if (ArmoryViewModel.WeaponMods[modIndex].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)))
                {
                    quality--;
                }
                else if (ArmoryViewModel.WeaponMods[modIndex].ApplyError.Contains("no mods"))
                {
                    return;
                }
                else if (quality > 1)
                {
                    if (ArmoryViewModel.WeaponMods[modIndex].ApplyError.Contains("Heavy Power Rune"))
                    {
                        if (
                        //ViewModel.WeaponMods[29].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) 
                        ArmoryViewModel.WeaponMods.FirstOrDefault(wm => wm.Name == "Heavy Power Rune").ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods[modIndex].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)))
                        {
                            quality -= 2;
                        }
                        else
                        {
                            quality--;
                            MessageBox.Show("RuhRoh");
                        }
                    }
                    else if (ArmoryViewModel.WeaponMods[modIndex].ApplyError.Contains("Heavy Magecell-Powered"))
                    {
                        if (
                        //ViewModel.WeaponMods[28].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods.FirstOrDefault(wm => wm.Name == "Heavy Magecell-Powered").ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods[modIndex].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)))
                        {
                            quality -= 2;
                        }
                        else
                        {

                            quality--;
                            MessageBox.Show("RuhRoh");
                        }
                    }
                    else if (ArmoryViewModel.WeaponMods[modIndex].ApplyError.Contains("Magecell-Powered"))
                    {
                        if (
                        //ViewModel.WeaponMods[37].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods.FirstOrDefault(wm => wm.Name == "Magecell-Powered").ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods[modIndex].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)))
                        {
                            quality -= 2;
                        }
                        else
                        {

                            quality--;
                            MessageBox.Show("RuhRoh");
                        }
                    }
                    else if (ArmoryViewModel.WeaponMods[modIndex].ApplyError.Contains("Power Rune"))
                    {
                        if (
                        //ViewModel.WeaponMods[46].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods.FirstOrDefault(wm => wm.Name == "Power Rune").ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)) &&
                        ArmoryViewModel.WeaponMods[modIndex].ApplyTo(((WeaponVM)baseWeaponList.SelectedItem)))
                        {
                            quality -= 2;
                        }
                        else
                        {

                            quality--;
                            MessageBox.Show("RuhRoh");
                        }
                    }
                }
            }
        }

        #endregion

        #region Barracks
        Barracks BarracksViewModel;
        #endregion

        #region Lair
        Lair LairViewModel;

        //Randomize Enemy
        private void CreateRandomEnemy(object sender, RoutedEventArgs e)
        {
            LairViewModel.GenerateRandomEnemy();
        }
        #endregion

        private void AddWeaponToSelectedNPC(object sender, RoutedEventArgs e)
        {
            Weapon w = ((WeaponVM)baseWeaponList.SelectedItem).model;
            NonPlayerCharacter npc = ((NPCQuickReferenceVM)EnemiesList.SelectedItem).model;
            if (w is RangedWeapon)
            {
                RangedWeapon rw = w as RangedWeapon;
                NpcRangedAttack ra = new Emergence.Model.NpcRangedAttack();
                ra.Name = rw.Name;
                ra.Weapon = rw;
                ra.RangeType = rw.RangeType;
                npc.Attacks.Add(ra);
            }
            else
            {
                NpcMeleeAttack ma = new NpcMeleeAttack();
                ma.Name = w.Name;
                ma.Weapon = w;
                npc.Attacks.Add(ma);
            }
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttack");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttackDamage");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttackCM");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttackRange");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttackArea");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("PrimaryAttackProperties");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttack");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttackDamage");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttackCM");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttackRange");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttackArea");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("SecondaryAttackProperties");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttack");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttackDamage");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttackCM");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttackRange");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttackArea");
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyPropertyChanged("TertiaryAttackProperties");
        }

        private void DeleteSelectedEnemy(object sender, RoutedEventArgs e)
        {
            LairViewModel.Enemies.Remove((NPCQuickReferenceVM)EnemiesList.SelectedItem);
        }

        private void SaveEnemies(object sender, RoutedEventArgs e)
        {
            LairViewModel.Save();
        }

        private void LoadEnemies(object sender, RoutedEventArgs e)
        {
            LairViewModel.Load();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            LairViewModel.Enemies.Add(new NPCQuickReferenceVM());
        }

        private void AddSelectedQuality(object sender, RoutedEventArgs e)
        {
            NpcQuality creatureQuality = ((NpcQuality)QualitiesList.SelectedItem);
            NPCQuickReferenceVM enemy = ((NPCQuickReferenceVM)EnemiesList.SelectedItem);
            LairViewModel.AddQualityByName(enemy, creatureQuality.Name);
        }

        private void AddRandomQuality(object sender, RoutedEventArgs e)
        {
            NPCQuickReferenceVM enemy = ((NPCQuickReferenceVM)EnemiesList.SelectedItem);
            LairViewModel.AddRandomQuality(enemy);
        }

        private void AddSelectedAblity(object sender, RoutedEventArgs e)
        {
            NpcAbility creatureAbility = ((NpcAbility)AbilitiesList.SelectedItem);
            NPCQuickReferenceVM enemy = ((NPCQuickReferenceVM)EnemiesList.SelectedItem);
            LairViewModel.AddAbilityByName(enemy, creatureAbility.Name);
        }

        private void AddRandomTalent(object sender, RoutedEventArgs e)
        {
            NPCQuickReferenceVM enemy = ((NPCQuickReferenceVM)EnemiesList.SelectedItem);
            LairViewModel.AddRandomTalent(enemy);
        }
    }
}
