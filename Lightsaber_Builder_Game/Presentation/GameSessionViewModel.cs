using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lightsaber_Builder_Game.Models;

namespace Lightsaber_Builder_Game.Presentation
{
    public class GameSessionViewModel : ObservableObject
    {
        #region ENUMS



        #endregion

        #region FIELDS

        private DateTime _gameStartTime;
        private string _gameTimeDisplay;
        private TimeSpan _gameTime;

        private Player _player;

        private Map _gamemap;
        private Location _currentLocation;
        private ObservableCollection<Location> _accessibleLocations;
        private string _currentLocationName;
        private GameItemModel _currentGameItem;
        private string _currentLocationInformation;
        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return _currentLocation.Message; }
        }
        public string PlanetLog
        {
            get { return _currentLocation.PlanetLog; }
        }
        public Map GameMap
        {
            get { return _gamemap; }
            set { _gamemap = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                _currentLocationInformation = _currentLocation.Description;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public ObservableCollection<Location> AccessibleLocations
        {
            get
            {
                return _accessibleLocations;
            }
            set
            {
                _accessibleLocations = value;
                OnPropertyChanged(nameof(AccessibleLocations));
            }
        }

        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set
            {
                _currentLocationName = value;
                OnPlayerMove();
                OnPropertyChanged("CurrentLocation");
            }
        }

        public string MissionTimeDisplay
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(MissionTimeDisplay));
            }
        }
        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }

        public GameItemModel CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(
            Player player,
            Map gamemap)
        {
            _player = player;
            _gamemap = gamemap;

            _currentLocation = _gamemap.Currentlocation;
            _accessibleLocations = new ObservableCollection<Location>();

            InitializeView();
        }

        #endregion

        #region METHODS
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAccessibleLocations();
            _currentLocationInformation = CurrentLocation.Description;
            _player.UpdateInventoryCategories();
        }

        #region ACTION METHODS
        public void AddItemToInventory()
        {
            //
            // confirm a game item selected and is in current location
            // subtract from location and add to inventory
            //
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                //
                // cast selected game item 
                //
                GameItemModel selectedGameItem = _currentGameItem as GameItemModel;

                _currentLocation.RemoveGameItemModelFromLocation(selectedGameItem);
                _player.AddGameItemToInventory(selectedGameItem);
            }
        }
        public void RemoveItemFromInventory()
        {
            //
            // confirm a game item selected
            // subtract from inventory and add to location
            //
            if (_currentGameItem != null)
            {
                //
                // cast selected game item 
                //
                GameItemModel selectedGameItem = _currentGameItem as GameItemModel;

                _currentLocation.AddGameItemModelToLocation(selectedGameItem);
                _player.RemoveGameItemFromInventory(selectedGameItem);
            }
        }
        #endregion
        private void OnPlayerMove()
        {
            //
            // set new current location
            //
            foreach (Location location in AccessibleLocations)
            {
                if (location.Name == _currentLocationName)
                {
                    _currentLocation = location;
                }
            }
            //_currentLocation = AccessibleLocations.FirstOrDefault(l => l.Name == _currentLocationName);
            OnPropertyChanged(nameof(MessageDisplay));
            OnPropertyChanged(nameof(PlanetLog));
            //
            // update the list of locations
            //
            UpdateAccessibleLocations();
        }
        private void UpdateAccessibleLocations()
        {
            //
            // reset accessible locations list
            //
            _accessibleLocations.Clear();

            //
            // add all accessible locations to list
            //
            foreach (Location location in _gamemap.Locations)
            {
                if (
                    location.Accessible == true)
                {
                    _accessibleLocations.Add(location);
                }
            }

            //
            // remove current location
            //
            _accessibleLocations.Remove(_accessibleLocations.FirstOrDefault(l => l == _currentLocation));

            //
            // notify list box in view to update
            //
            OnPropertyChanged(nameof(AccessibleLocations));
        }



        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }
        #endregion

        #region EVENTS



        #endregion
    }
}
