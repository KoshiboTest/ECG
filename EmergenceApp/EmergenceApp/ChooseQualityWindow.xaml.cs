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
    /// Interaction logic for ChooseQualityWindow.xaml
    /// </summary>
    public partial class ChooseQualityWindow : Window
    {
        private Lair lair;

        public int qualityIndex { get; internal set; }

        public ChooseQualityWindow(Lair lair)
        {
            this.lair = lair;
            this.DataContext = lair;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            qualityIndex = QualitiesList.SelectedIndex;
            Close();
        }
    }
}
