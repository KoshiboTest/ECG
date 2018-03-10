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

        private void AddAmpMod(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            AmpModVM am = (AmpModVM)b.DataContext;
            AmpVM a = b.CommandParameter as AmpVM;
            if (a != null && am.ApplyTo(a))
            {
            }
            else if (a == null)
            {
                MessageBox.Show("Select a weapon.");
            }
            else
            {
                MessageBox.Show(((AmpModVM)((Button)sender).DataContext).ApplyError);
            }
        }

        //Change quality
        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            ((WeaponVM)((Button)sender).DataContext).IncreaseQuality();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            ((WeaponVM)((Button)sender).DataContext).DecreaseQuality();
        }

        private void cmdUpAmp_Click(object sender, RoutedEventArgs e)
        {
            ((AmpVM)((Button)sender).DataContext).IncreaseQuality();
        }

        private void cmdDownAmp_Click(object sender, RoutedEventArgs e)
        {
            ((AmpVM)((Button)sender).DataContext).DecreaseQuality();
        }

        //Remove
        private void RemoveWeaponMod(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            WeaponModVM wm = (WeaponModVM)b.DataContext;
            WeaponVM w = b.CommandParameter as WeaponVM;
            wm.RemoveFrom(w);
        }

        private void RemoveAmpMod(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            AmpModVM am = (AmpModVM)b.DataContext;
            AmpVM a = b.CommandParameter as AmpVM;
            am.RemoveFrom(a);
        }
        //Randomize
        private void RandomizeWeapon(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            //Select random item
            int weaponIndex = r.Next(ArmoryViewModel.BaseWeapons.Count);
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
                int modIndex = r.Next(ArmoryViewModel.WeaponMods.Count);
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

        private void RandomizeAmp(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            //Select random item
            int AmpIndex = r.Next(ArmoryViewModel.BaseAmps.Count);
            baseAmpList.SelectedIndex = AmpIndex;
            //select quality
            ((AmpVM)baseAmpList.SelectedItem).Mods.Clear();
            int quality = new Random().Next(4);
            while (quality < (int)((AmpVM)baseAmpList.SelectedItem).Quality)
            {
                ((AmpVM)baseAmpList.SelectedItem).DecreaseQuality();
            }
            while (quality > (int)((AmpVM)baseAmpList.SelectedItem).Quality)
            {
                ((AmpVM)baseAmpList.SelectedItem).IncreaseQuality();
            }
            //set mods
            while (quality > 0)
            {
                int modIndex = r.Next(ArmoryViewModel.AmpMods.Count);
                if (ArmoryViewModel.AmpMods[modIndex].ApplyTo(((AmpVM)baseAmpList.SelectedItem)))
                {
                    quality--;
                }
                else if (ArmoryViewModel.AmpMods[modIndex].ApplyError.Contains("no mods"))
                {
                    return;
                }
                else if (quality > 1)
                {
                    if (ArmoryViewModel.AmpMods[modIndex].ApplyError.Contains("Heavy Power Rune"))
                    {
                        if ( 
                        ArmoryViewModel.AmpMods.FirstOrDefault(wm => wm.Name == "Heavy Power Rune").ApplyTo(((AmpVM)baseAmpList.SelectedItem)) &&
                        ArmoryViewModel.AmpMods[modIndex].ApplyTo(((AmpVM)baseAmpList.SelectedItem)))
                        {
                            quality -= 2;
                        }
                        else
                        {
                            quality--;
                            MessageBox.Show("RuhRoh");
                        }
                    }
                    else if (ArmoryViewModel.AmpMods[modIndex].ApplyError.Contains("Power Rune"))
                    {
                        if (
                        ArmoryViewModel.AmpMods.FirstOrDefault(wm => wm.Name == "Power Rune").ApplyTo(((AmpVM)baseAmpList.SelectedItem)) &&
                        ArmoryViewModel.AmpMods[modIndex].ApplyTo(((AmpVM)baseAmpList.SelectedItem)))
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

        //Add to NPC
        private void AddWeaponToSelectedNPC(object sender, RoutedEventArgs e)
        {
            Weapon w = ((WeaponVM)baseWeaponList.SelectedItem).model;
            NonPlayerCharacter npc = ((NPCQuickReferenceVM)EnemiesList.SelectedItem).model;
            NpcWeaponAttack wa = new NpcWeaponAttack();
            wa.Name = w.Name;
            wa.Weapon = w;
            npc.Attacks.Add(wa);
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

        private void AddAmpToSelectedNPC(object sender, RoutedEventArgs e)
        {
            Amp a = ((AmpVM)baseAmpList.SelectedItem).model;
            NonPlayerCharacter npc = ((NPCQuickReferenceVM)EnemiesList.SelectedItem).model;
            NpcAmpAttack aa = new NpcAmpAttack();
            aa.Name = a.Name;
            aa.Amp = a;
            npc.Attacks.Add(aa);
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

        //Remove attack
        private void RemoveAttack(object sender, RoutedEventArgs e)
        {
            NonPlayerCharacter npc = ((NPCQuickReferenceVM)EnemiesList.SelectedItem).model;
            int attackNumber = (int.Parse((sender as Button).CommandParameter.ToString()));
            npc.Attacks.RemoveAt(attackNumber - 1);
            ((NPCQuickReferenceVM)EnemiesList.SelectedItem).NotifyAttacks();
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

        private void CreateBlankEnemy(object sender, RoutedEventArgs e)
        {
            NewNPCWindow win = new EmergenceApp.NewNPCWindow();
            win.Lair = LairViewModel;
            win.Show();
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

        private void AddSelectedTalent(object sender, RoutedEventArgs e)
        {
            int index = TalentsList.SelectedIndex;
            NPCQuickReferenceVM enemy = ((NPCQuickReferenceVM)EnemiesList.SelectedItem);
            LairViewModel.AddTalentByIndex(enemy, index);
        }
    }
}
