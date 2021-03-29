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

            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {

                GameItemModel selectedGameItem = _currentGameItem as GameItemModel;
                switch (_currentGameItem)
                {
                    case LightSaberParts lightSaber:
                        AddLightsaberProgress(lightSaber);
                        break;
                    default:
                        break;
                }
                _currentLocation.RemoveGameItemModelFromLocation(selectedGameItem);
                _player.AddGameItemToInventory(selectedGameItem);
            }
        }

        private void AddLightsaberProgress(LightSaberParts lightSaber)
        {
            _player.LightsaberProgress += lightSaber.LightsaberProgress;
        }

        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemModel selectedGameItem = _currentGameItem as GameItemModel;
                switch (_currentGameItem)
                {
                    case LightSaberParts lightSaber:
                        RemoveLightsaberPercent(lightSaber);
                        break;
                    default:
                        break;
                }
                _currentLocation.AddGameItemModelToLocation(selectedGameItem);
                _player.RemoveGameItemFromInventory(selectedGameItem);
            }
        }

        private void RemoveLightsaberPercent(LightSaberParts lightSaber)
        {
            _player.LightsaberProgress -= lightSaber.LightsaberProgress;
        }
        #endregion
        private void OnPlayerMove()
        {
            foreach (Location location in AccessibleLocations)
            {
                if (location.Name == _currentLocationName)
                {
                    _currentLocation = location;
                }
            }
            OnPropertyChanged(nameof(MessageDisplay));
            OnPropertyChanged(nameof(PlanetLog));

            UpdateAccessibleLocations();
        }
        private void UpdateAccessibleLocations()
        {
            _accessibleLocations.Clear();

            foreach (Location location in _gamemap.Locations)
            {
                if (location.Accessible == true)
                {
                    _accessibleLocations.Add(location);
                }
            }

            _accessibleLocations.Remove(_accessibleLocations.FirstOrDefault(l => l == _currentLocation));

            OnPropertyChanged(nameof(AccessibleLocations));
        }
        public void OnUseGameItem()
        {
            switch (_currentGameItem)
            {
                case HealthItems healthItems:
                    HealthItemUse(healthItems);
                    break;
                case Weapons weapons:
                    WeaponsUse(weapons);
                    break;
                default:
                    break;
            }
        }

        private void HealthItemUse(HealthItems healthItems)
        {
            _player.Health += healthItems.HealthChange;
            _player.Lives += healthItems.LivesChange;
            _player.RemoveGameItemFromInventory(_currentGameItem);
        }

        private void WeaponsUse(Weapons weapons)
        {
            //Player.WeaponsInUse = weapons.Using;
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
