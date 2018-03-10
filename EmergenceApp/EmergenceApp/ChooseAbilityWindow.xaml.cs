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

namespace EmergenceApp
{
    /// <summary>
    /// Interaction logic for ChooseAbilityWindow.xaml
    /// </summary>
    public partial class ChooseAbilityWindow : Window
    {
        private Lair lair;

        public int abilityIndex { get; internal set; }

        public ChooseAbilityWindow(Lair lair)
        {
            this.lair = lair;
            this.DataContext = lair;
            InitializeComponent();
        }

        private void Done(object sender, RoutedEventArgs e)
        {
            abilityIndex = AbilitiesList.SelectedIndex;
            Close();
        }
    }
}
