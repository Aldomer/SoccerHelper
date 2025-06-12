using SoccerHelper.Classes;
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

namespace SoccerHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Lineup _lineup = new Lineup();
        private Team _team = new Team();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateLineups_Click(object sender, RoutedEventArgs e)
        {
            int match = 1;
            Int32.TryParse(txtMatch.Text, out match);

            _lineup.Generate(_team, match);
            MessageBox.Show("Done");
        }
    }
}
