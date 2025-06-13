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
        private Team _team = new Team();
        private Match _match = new Match();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateLineups_Click(object sender, RoutedEventArgs e)
        {
            // Before generating a lineup they will need to say in a match who is attending?

            _team.LoadPlayerList();
            _team.LoadPositions();
            
            _match.SetPlayersAvailableForMatch(_team);
            _match.GenerateLineUps(_team);

            OutputFile.CreateMatchInformationFile(_match);

            MessageBox.Show("Done");
        }
    }
}
