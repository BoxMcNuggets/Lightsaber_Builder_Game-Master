using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Lightsaber_Builder_Game.Models;
using Lightsaber_Builder_Game.DataLayer;

namespace Lightsaber_Builder_Game.Presentation
{
    public class GameSessionViewModel : ObservableObject
    {
        #region ENUMS



        #endregion

        #region CONSTANTS

        const string TAB = "\t";
        const string NEW_LINE = "\n";

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

        public GameSessionViewModel(Player player, Map gamemap)
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
            InitializeMissionBattleViewModel();
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
                        _player.UpdateMissionStatus();
                        break;
                    case KyberCrystals kybercrystal:
                        AddLightsaberCrystalProgress(kybercrystal, selectedGameItemModelQuantity);
                        _player.UpdateMissionStatus();
                        break;
                    default:
                        _currentLocation.RemoveGameItemModelFromLocation(selectedGameItemModelQuantity);
                        _player.AddGameItemModelToInventory(selectedGameItemModelQuantity);
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

        #endregion

        #region Remove Item From Inventory
        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemModelQuantity selectedGameItemModelQuantity = _currentGameItem as GameItemModelQuantity;

                switch (_currentGameItem.GameItemModel)
                {
                    case LightSaberParts lightSaber:
                        RemoveLightsaberPercent(lightSaber, selectedGameItemModelQuantity);
                        _player.UpdateMissionStatus();
                        break;
                    case KyberCrystals kybercrystal:
                        RemoveLightsaberCrystalProgress(kybercrystal, selectedGameItemModelQuantity);
                        _player.UpdateMissionStatus();
                        break;
                    default:
                        _currentLocation.AddGameItemModelToLocation(selectedGameItemModelQuantity);
                        _player.RemoveGameItemModelToInventory(selectedGameItemModelQuantity);
                        break;
                }

            }
        }

        private void RemoveLightsaberCrystalProgress(KyberCrystals kybercrystal, GameItemModelQuantity selectedGameItemModelQuantity)
        {
            _player.LightsaberProgress -= kybercrystal.LightsaberProgress;
            _currentLocation.AddGameItemModelToLocation(selectedGameItemModelQuantity);
            _player.RemoveGameItemModelToInventory(selectedGameItemModelQuantity);
        }
        private void RemoveLightsaberPercent(LightSaberParts lightSaber, GameItemModelQuantity selectedGameItemModelQuantity)
        {
            _player.LightsaberProgress -= lightSaber.LightsaberProgress;
            _currentLocation.AddGameItemModelToLocation(selectedGameItemModelQuantity);
            _player.RemoveGameItemModelToInventory(selectedGameItemModelQuantity);
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
                _player.UpdateMissionStatus();
                _player.NPCSDefeated.Add(_currentNPC);
                if (CurrentNPC.Name == "Emperor Palpatine")
                {
                    if (MessageBox.Show("Do you want to accept the quests?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
                else if (CurrentNPC.Name == "Obi-Wan Kenobi")
                {
                    if (MessageBox.Show("Do you want to accept the quests?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        
                    }
                    else
                    {
                        // Do Nothing
                    }
                }
            }
        }
        private string GenerateMissionDefeatDetail(MissionBattleEnemys mission) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            sb.AppendLine("All Enemies");
            foreach (var location in mission.RequiredNPCS)
            {
                sb.AppendLine(TAB + location.Name);
            }
            if (mission.Status == Mission.MissionStatus.Incomplete)
            {
                sb.AppendLine("NPCs to defeat");
                foreach (var location in mission.NPCSNotDefeated(_player.NPCSDefeated))
                {
                    sb.AppendLine(TAB + location.Name);
                }
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }
        private string GenerateMissionLightsaberDetail(MissionLightsaberParts mission) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            sb.AppendLine("All Required Lightsaber Parts");
            foreach (var gameItemModelQuantity in mission.RequiredgameItemModelQuantities)
            {
                sb.Append(TAB + gameItemModelQuantity.GameItemModel.Name);
                sb.AppendLine($" ( {gameItemModelQuantity.Quantity} ) ");
            }
            if (mission.Status == Mission.MissionStatus.Incomplete)
            {
                sb.AppendLine("Lightsaber Parts To Gather");
                foreach (var gameItemModelQuantity in mission.GameItemModelQuantityMissionToDo(_player.Inventory.ToList()))
                {
                    int quantityInInventory = 0;
                    GameItemModelQuantity gameItemModelQuantityGathered = _player.Inventory.FirstOrDefault(gi => gi.GameItemModel.Id == gameItemModelQuantity.GameItemModel.Id);
                    if (gameItemModelQuantityGathered != null)
                    {
                        quantityInInventory = gameItemModelQuantityGathered.Quantity;
                    }
                    sb.Append(TAB + gameItemModelQuantity.GameItemModel.Name);
                    sb.AppendLine($" ( {gameItemModelQuantity.Quantity - quantityInInventory} ) ");
                }
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }
        private string GenerateMissionStatusInfo() 
        {
            string missionStatusInformation;

            double totalMissions = _player.Mission.Count();
            double missionCompleted = _player.Mission.Where(m => m.Status == Mission.MissionStatus.Complete).Count();

            int percentMissionsComplete = (int)((missionCompleted / totalMissions) * 100);
            missionStatusInformation = $"Missions Complete: {percentMissionsComplete}%" + NEW_LINE;

            if (percentMissionsComplete == 0)
            {
                missionStatusInformation += "No missions complete";
            }
            else if (percentMissionsComplete <= 50)
            {
                missionStatusInformation += "You are half way there";
            }
            else if (percentMissionsComplete == 100)
            {
                missionStatusInformation += "Congratulations, you have completed all missions";
            }

            return missionStatusInformation;
        }
        private MissionStatusViewModel InitializeMissionBattleViewModel()
        {
            MissionStatusViewModel missionStatusViewModel = new MissionStatusViewModel();

            missionStatusViewModel.MissionInformation = GenerateMissionStatusInfo();

            missionStatusViewModel.Missions = new List<Mission>(_player.Mission);
            foreach (Mission mission in missionStatusViewModel.Missions)
            {
                if (mission is MissionBattleEnemys)
                    mission.StatusDetail = GenerateMissionDefeatDetail((MissionBattleEnemys)mission);

                if (mission is MissionLightsaberParts)
                    mission.StatusDetail = GenerateMissionLightsaberDetail((MissionLightsaberParts)mission);
            }

            return missionStatusViewModel;
        }
        public void OpenMissionBattleView() 
        {
            if (_currentNPC.Id == 106 || _currentNPC.Id == 108 || _currentNPC.Id == 101 || _currentNPC.Id == 109)
            {
                MissionBattleView missionBattleView = new MissionBattleView(InitializeMissionBattleViewModel());

                missionBattleView.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Error: You Cannot Battle This NPC");
            }
        }

        #endregion

        #region EVENTS



        #endregion
    }
}
