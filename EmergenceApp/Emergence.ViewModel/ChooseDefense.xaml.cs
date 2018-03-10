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

namespace Emergence.ViewModel
{
    /// <summary>
    /// Interaction logic for ChooseDefense.xaml
    /// </summary>
    public partial class ChooseDefense : Window
    {
        public ChooseDefense(string prompt)
        {
            this.DataContext = prompt;
            InitializeComponent();
        }

        public int ChosenDefense { get; internal set; }

        private void Melee_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 0;
            Close();
        }

        private void Area_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 1;
            Close();
        }

        private void Ranged_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 2;
            Close();
        }

        private void Physical_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 3;
            Close();
        }

        private void Resolve_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 4;
            Close();
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            ChosenDefense = 5;
            Close();
        }
    }
}
