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

namespace EmergenceApp
{
    /// <summary>
    /// Interaction logic for ChooseAbilityOrQuality.xaml
    /// </summary>
    public partial class ChooseAbilityOrQuality : Window
    {
        public ChooseAbilityOrQuality()
        {
            InitializeComponent();
        }

        public bool IsQuality { get; internal set; }
        public bool IsAbility { get; internal set; }

        private void Ability_Click(object sender, RoutedEventArgs e)
        {
            IsAbility = true;
            IsQuality = false;
            Close();
        }

        private void Quality_Click(object sender, RoutedEventArgs e)
        {
            IsAbility = false;
            IsQuality = true;
            Close();
        }
    }
}
