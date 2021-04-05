using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private NPCS _currentNPC;

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
        public NPCS CurrentNPC
        {
            get { return _currentNPC; }
            set 
            {
                _currentNPC = value;
                OnPropertyChanged(nameof(CurrentNPC));
            }
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

        #region Add Lightsaber Parts/Progress
        public void AddItemToInventory()
        {
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {

                GameItemModel selectedGameItem = _currentGameItem as GameItemModel;
                switch (_currentGameItem)
                {
                    case LightSaberParts lightSaber:
                        AddLightsaberProgress(lightSaber, selectedGameItem);
                        break;
                    case KyberCrystals kybercrystal:
                        AddLightsaberCrystalProgress(kybercrystal, selectedGameItem);
                        break;
                    default:
                        _currentLocation.RemoveGameItemModelFromLocation(selectedGameItem);
                        _player.AddGameItemToInventory(selectedGameItem);
                        break;
                }
            }
        }
        private void AddLightsaberCrystalProgress(KyberCrystals kybercrystal, GameItemModel selectedGameItem)
        {
            if (_player.LightsaberProgress == 85)
            {
                _player.LightsaberProgress += kybercrystal.LightsaberProgress;
                _currentLocation.RemoveGameItemModelFromLocation(selectedGameItem);
                _player.AddGameItemToInventory(selectedGameItem);
            }
            else if (_player.LightsaberProgress == 100)
            {
                MessageBoxResult result = MessageBox.Show("Error: You are already holding a Kyber Crystal");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Error: You need to gather all the lightsaber parts first.");
            }
        }
        private void AddLightsaberProgress(LightSaberParts lightSaber, GameItemModel selectedGameItem)
        {
            _player.LightsaberProgress += lightSaber.LightsaberProgress;
            _currentLocation.RemoveGameItemModelFromLocation(selectedGameItem);
            _player.AddGameItemToInventory(selectedGameItem);
        }
        #endregion

        #region Remove Lightsaber Parts/Progress
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
                    case KyberCrystals kybercrystal:
                        RemoveLightsaberCrystalProgress(kybercrystal);
                        break;
                    default:
                        break;
                }
                _currentLocation.AddGameItemModelToLocation(selectedGameItem);
                _player.RemoveGameItemFromInventory(selectedGameItem);
            }
        }
        private void RemoveLightsaberCrystalProgress(KyberCrystals kybercrystal)
        {
            _player.LightsaberProgress -= kybercrystal.LightsaberProgress;
        }
        private void RemoveLightsaberPercent(LightSaberParts lightSaber)
        {
            _player.LightsaberProgress -= lightSaber.LightsaberProgress;
        }
        #endregion

        #region Player Movement 
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
        #endregion

        #region On Use
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
            _currentLocation.PlanetLog += $"You have restored {_currentGameItem.Value} healthpoints.";
            _player.RemoveGameItemFromInventory(_currentGameItem);
        }

        private void WeaponsUse(Weapons weapons)
        {
            _player.WeaponsInUse = weapons.Using;
            _player.RemoveGameItemFromInventory(_currentGameItem);
        }
        #endregion

        public void OnPlayerTalk() 
        {
            if (CurrentNPC != null && CurrentNPC is ISpeak)
            {
                ISpeak speakingNPC = CurrentNPC as ISpeak;
                CurrentLocationInformation = speakingNPC.Speak();
            }
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
