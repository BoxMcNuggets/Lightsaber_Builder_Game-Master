using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
        private GameItemModelQuantity _currentGameItem;
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
        public GameItemModelQuantity CurrentGameItem
        {
            get { return _currentGameItem; }
            set 
            { 
                _currentGameItem = value;
                OnPropertyChanged(nameof(CurrentGameItem));
            }
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

            GameTimer();
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

        #region Add Item To Inventory
        public void AddItemToInventory()
        {
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                GameItemModelQuantity selectedGameItemModelQuantity = _currentGameItem as GameItemModelQuantity;

                switch (_currentGameItem.GameItemModel)
                {
                    case LightSaberParts lightSaber:
                        AddLightsaberProgress(lightSaber, selectedGameItemModelQuantity);
                        break;
                    case KyberCrystals kybercrystal:
                        AddLightsaberCrystalProgress(kybercrystal, selectedGameItemModelQuantity);
                        break;
                    default:
                        _currentLocation.RemoveGameItemModelFromLocation(selectedGameItemModelQuantity);
                        _player.AddGameItemModelToInventory(selectedGameItemModelQuantity);
                        break;
                }
            }
        }

        #endregion

        #region Remove Item From Inventory
        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemModelQuantity selectedGameItemQuantity = _currentGameItem as GameItemModelQuantity;

                switch (_currentGameItem.GameItemModel)
                {
                    case LightSaberParts lightSaber:
                        RemoveLightsaberPercent(lightSaber);
                        break;
                    case KyberCrystals kybercrystal:
                        RemoveLightsaberCrystalProgress(kybercrystal);
                        break;
                    default:
                        _currentLocation.AddGameItemModelToLocation(selectedGameItemQuantity);
                        _player.RemoveGameItemModelToInventory(selectedGameItemQuantity);
                        break;
                }

            }
        }

        private void AddLightsaberCrystalProgress(KyberCrystals kybercrystal, GameItemModelQuantity selectedGameItemModelQuantity)
        {
            if (_player.LightsaberProgress == 85)
            {
                _player.LightsaberProgress += kybercrystal.LightsaberProgress;
                _currentLocation.RemoveGameItemModelFromLocation(selectedGameItemModelQuantity);
                _player.AddGameItemModelToInventory(selectedGameItemModelQuantity);
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
        private void AddLightsaberProgress(LightSaberParts lightSaber, GameItemModelQuantity selectedGameItemModelQuantity)
        {
            _player.LightsaberProgress += lightSaber.LightsaberProgress;
            _currentLocation.RemoveGameItemModelFromLocation(selectedGameItemModelQuantity);
            _player.AddGameItemModelToInventory(selectedGameItemModelQuantity);
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
            if (_currentGameItem == null)
            {
                MessageBoxResult result = MessageBox.Show("Error: You need to select an item.");
            }
            else
            {
                 switch (_currentGameItem.GameItemModel)
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
        }
        private void HealthItemUse(HealthItems healthItems)
        {
            _player.Health += healthItems.HealthChange;
            _player.Lives += healthItems.LivesChange;
            _player.RemoveGameItemModelToInventory(_currentGameItem);
        }

        private void WeaponsUse(Weapons weapons)
        {
            _player.WeaponsInUse = weapons.Using;
            _player.RemoveGameItemModelToInventory(_currentGameItem);
        }
        #endregion

        #region GameTime

        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }
        public void GameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnGameTimerTick;
            timer.Start();
        }
        void OnGameTimerTick(object sender, EventArgs e)
        {
            _gameTime = DateTime.Now - _gameStartTime;
            MissionTimeDisplay = "Mission Time " + _gameTime.ToString(@"hh\:mm\:ss");
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

        #endregion

        #region EVENTS



        #endregion
    }
}
