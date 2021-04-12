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

namespace Lightsaber_Builder_Game.Presentation
{
    /// <summary>
    /// Interaction logic for GameSessionView.xaml
    /// </summary>
    public partial class GameSessionView : Window
    {
        GameSessionViewModel _gameSessionViewModel;
        public GameSessionView(GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;

            InitializeWindowTheme();

            InitializeComponent();
        }

        private void InitializeWindowTheme()
        {
            this.Title = "Non Official Lucasfilms Game";
        }

        #region Buttons Click

        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (LocationItemsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.AddItemToInventory();
            }
        }
        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryItemsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.RemoveItemFromInventory();
            }
        }
        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryItemsDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnUseGameItem();
            }
        }
        private void TalkToButton_Click(object sender, RoutedEventArgs e)
        {
            if (NPCSDataGrid.SelectedItem != null)
            {
                _gameSessionViewModel.OnPlayerTalk();
            }
        }
        private void AcceptQuestButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void EnterLair_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void BattleQuestButton_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.OpenMissionBattleView();
        }

        #endregion


    }
}
