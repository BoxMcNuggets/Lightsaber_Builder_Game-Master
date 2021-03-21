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
    public partial class GameSetupView : Window
    {
        private Player _player;
        public GameSetupView(Player player)
        {
            _player = player;

            InitializeComponent();

            SetupWindow();
        }
        private void SetupWindow()
        {
            //
            // generate lists for each enum to use in the combo boxes
            //
            List<string> races = Enum.GetNames(typeof(Player.RaceType)).ToList();
            List<string> forceSide = Enum.GetNames(typeof(Player.ForceSide)).ToList();
            ForceSideComboBox.ItemsSource = forceSide;
            RaceComboBoxButton.ItemsSource = races;

            //
            // hide error message box initially
            //
            ErrorTextBlock.Visibility = Visibility.Hidden;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;

            if (IsValidInput(out errorMessage))
            {
                //
                // get values from combo boxes
                //
                Enum.TryParse(ForceSideComboBox.SelectionBoxItem.ToString(), out Player.ForceSide forceSide);
                Enum.TryParse(RaceComboBoxButton.SelectionBoxItem.ToString(), out Player.RaceType race);

                //
                // set player properties
                //
                _player.JobTitle = forceSide;
                _player.Race = race;

                Visibility = Visibility.Hidden;
            }
            else
            {
                //
                // display error messages
                //
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = errorMessage;
            }
        }
        private bool IsValidInput(out string errorMessage)
        {
            errorMessage = "";

            if (NameTextBox.Text == "")
            {
                errorMessage += "Player Name is required.\n";
            }
            else
            {
                _player.Name = NameTextBox.Text;
            }
            if (!int.TryParse(AgeTextBox.Text, out int age))
            {
                errorMessage += "Player Age is required and must be an integer.\n";
            }
            else
            {
                _player.Age = age;
            }

            return errorMessage == "" ? true : false;
        }
    }
}
