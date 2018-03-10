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
using System.Windows.Shapes;
using Emergence.ViewModel;
using Emergence.Model;

namespace EmergenceApp
{
    /// <summary>
    /// Interaction logic for NewNPCWindow.xaml
    /// </summary>
    public partial class NewNPCWindow : Window
    {
        public NewNPCWindow()
        {
            InitializeComponent();
        }

        public Lair Lair { get; internal set; }
        private List<int> abilityIndexes = new List<int>();
        private List<int> qualityIndexes = new List<int>();
        private List<int> talentIndexes = new List<int>();

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateNPC(object sender, RoutedEventArgs e)
        {
            int level = int.Parse(Level.Value.ToString());
            int size = int.Parse(Size.Value.ToString());
            NpcClass nc = (NpcClass)Enum.Parse(typeof(NpcClass), ((ComboBoxItem)Class.SelectedItem).Content.ToString());
            NpcType nt = (NpcType)Enum.Parse(typeof(NpcType), ((ComboBoxItem)Type.SelectedItem).Content.ToString());
            Archetype arch = (Archetype)Enum.Parse(typeof(Archetype), ((ComboBoxItem)Archetype.SelectedItem).Content.ToString());

            Random r = new Random();
            int numberOfAbilities = 0;
            int numberOfQualities = 0;
            int numberOfTalents = 0;
            switch (nc)
            {
                case NpcClass.Foe:
                    numberOfAbilities = 1;
                    numberOfQualities = 2;
                    numberOfTalents = 0;
                    break;
                case NpcClass.Grunt:
                    numberOfAbilities = 1;
                    numberOfQualities = 0;
                    numberOfTalents = 0;

                    ChooseAbilityOrQuality a = new ChooseAbilityOrQuality();
                    a.Owner = this;
                    a.ShowDialog();
                    if (a.IsQuality)
                    {
                        numberOfQualities++;
                    }
                    else
                    {
                        numberOfAbilities++;
                    }
                    break;
                case NpcClass.Antagonist:
                    numberOfAbilities = 1;
                    numberOfQualities = 2;
                    numberOfTalents = 0;

                    ChooseAbilityOrQuality b = new ChooseAbilityOrQuality();
                    b.Owner = this;
                    b.ShowDialog();
                    if (b.IsQuality)
                    {
                        numberOfQualities++;
                    }
                    else
                    {
                        numberOfAbilities++;
                    }
                    break;
            }
            switch (nt)
            {
                case NpcType.Natural:
                    ChooseAbilityOrQuality c = new ChooseAbilityOrQuality();
                    c.Owner = this;
                    c.ShowDialog();
                    if (c.IsQuality)
                    {
                        numberOfQualities++;
                    }
                    else
                    {
                        numberOfAbilities++;
                    }
                    break;
                default:
                    break;
            }
            while (numberOfAbilities > 0)
            {
                ChooseAbilityWindow abilityWindow = new ChooseAbilityWindow(Lair);
                abilityWindow.Owner = this;
                abilityWindow.ShowDialog();
                if (abilityWindow.abilityIndex == 9)
                {
                    numberOfTalents++;
                }
                else if (abilityWindow.abilityIndex == 10)
                {
                    numberOfQualities++;
                }
                else
                {
                    abilityIndexes.Add(abilityWindow.abilityIndex);
                }
                numberOfAbilities--;
            }
            while (numberOfQualities > 0)
            {
                ChooseQualityWindow qualityWindow = new ChooseQualityWindow(Lair);
                qualityWindow.Owner = this;
                qualityWindow.ShowDialog();
                qualityIndexes.Add(qualityWindow.qualityIndex);
                numberOfQualities--;
            }
            while (numberOfTalents > 0)
            {
                ChooseTalentWindow talentWindow = new ChooseTalentWindow(Lair);
                talentWindow.Owner = this;
                talentWindow.ShowDialog();
                talentIndexes.Add(talentWindow.talentIndex);
                numberOfTalents--;
            }

            Lair.AddNewNPC(Name.Text, level, size, nc, nt, arch, abilityIndexes, qualityIndexes, talentIndexes);
            Close();
        }
    }
}
