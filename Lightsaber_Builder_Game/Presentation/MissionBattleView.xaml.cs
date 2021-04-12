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
using Lightsaber_Builder_Game.Models;

namespace Lightsaber_Builder_Game.Presentation
{
    /// <summary>
    /// Interaction logic for MissionBattleView.xaml
    /// </summary>
    public partial class MissionBattleView : Window
    {
        MissionStatusViewModel _missionBattleView;
        public MissionBattleView(MissionStatusViewModel missionBattleViewModel)
        {
            _missionBattleView = missionBattleViewModel;
            DataContext = missionBattleViewModel;

            InitializeWindowTheme();

            InitializeComponent();
        }
        private void InitializeWindowTheme()
        {
            this.Title = "Battle Screen";
        }

        private void MissionStatusWindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RetreatButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBoxResult result = MessageBox.Show("You retreat from the battle.");
        }
    }
}
