using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lightsaber_Builder_Game.Models
{
    public class Player : Character, IBattle
    {
        #region ENUMS

        public enum ForceSide { LightSide, DarkSide }

        private const int DEFENDER_DAMAGE_ADJUSTMENT = 5;
        private const int MAXIMUM_RETREAT_DAMAGE = 10;

        #endregion

        #region FIELDS

        private int _lightsaberProgress;
        private int _lives;
        private int _health;
        private Weapons _currentGameItemWeapon;
        private BattleEnum _battleEnum;
        private string _weaponsInUse;
        private ForceSide _forceSide;
        private List<Location> _locationsVisited;
        private List<NPCS> _npcsDefeated;
        private ObservableCollection<GameItemModelQuantity> _inventory;
        private ObservableCollection<GameItemModelQuantity> _healthItems;
        private ObservableCollection<GameItemModelQuantity> _credits;
        private ObservableCollection<GameItemModelQuantity> _weapons;
        private ObservableCollection<GameItemModelQuantity> _lightSaberParts;
        private ObservableCollection<GameItemModelQuantity> _kyberCrystals;
        private ObservableCollection<Mission> _missions;
        private BattleEnum _battleMode;

        #endregion

        #region PROPERTIES

        public int LightsaberProgress
        {
            get { return _lightsaberProgress; }
            set 
            {
                _lightsaberProgress = value;
                OnPropertyChanged(nameof(LightsaberProgress));
            }
        }
        public string WeaponsInUse
        {
            get { return _weaponsInUse; }
            set 
            {
                _weaponsInUse = value;
                OnPropertyChanged(nameof(WeaponsInUse));
            }
        }
        public int Lives
        {
            get { return _lives; }
            set 
            {
                _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }
        public ForceSide JobTitle
        {
            get { return _forceSide; }
            set 
            {
                _forceSide = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }
        public int Health
        {
            get { return _health; }
            set 
            {
                _health = value;

                if (_health > 100)
                {
                    _health = 100;
                }
                else if (_health <= 0)
                {
                    _health = 100;
                    _lives--;
                }
                OnPropertyChanged(nameof(Health));
            }
        }
        public Weapons CurrentGameItemWeapon
        {
            get { return _currentGameItemWeapon; }
            set { _currentGameItemWeapon = value; }
        }
        public BattleEnum BattleEnumName
        {
            get { return _battleEnum; }
            set { _battleEnum = value; }
        }

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }
        public List<NPCS> NPCSDefeated
        {
            get { return _npcsDefeated; }
            set { _npcsDefeated = value; }
        }

        public ObservableCollection<GameItemModelQuantity> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public ObservableCollection<GameItemModelQuantity> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }
        public ObservableCollection<GameItemModelQuantity> HealthItems
        {
            get { return _healthItems; }
            set { _healthItems = value; }
        }
        public ObservableCollection<GameItemModelQuantity> Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        public ObservableCollection<GameItemModelQuantity> LightSaberParts
        {
            get { return _lightSaberParts; }
            set { _lightSaberParts = value; }
        }
        public ObservableCollection<GameItemModelQuantity> KyberCrystals
        {
            get { return _kyberCrystals; }
            set { _kyberCrystals = value; }
        }
        public ObservableCollection<Mission> Mission
        {
            get { return _missions; }
            set { _missions = value; }
        }
        public BattleEnum BattleMode
        {
            get { return _battleMode; }
            set { _battleMode = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<Location>();
            _npcsDefeated = new List<NPCS>();
            _weapons = new ObservableCollection<GameItemModelQuantity>();
            _healthItems = new ObservableCollection<GameItemModelQuantity>();
            _credits = new ObservableCollection<GameItemModelQuantity>();
            _lightSaberParts = new ObservableCollection<GameItemModelQuantity>();
            _missions = new ObservableCollection<Mission>();
        }

        #endregion

        #region METHODS

        #region Missions

        public void UpdateMissionStatus()
        {
            foreach (Mission mission in _missions.Where(m => m.Status == Models.Mission.MissionStatus.Incomplete))
            {
                if (mission is MissionLightsaberParts)
                {
                    if (((MissionLightsaberParts)mission).GameItemModelQuantityMissionToDo(_inventory.ToList()).Count == 0)
                    {
                        mission.Status = Models.Mission.MissionStatus.Complete;
                    }
                }
                else if (mission is MissionBattleEnemys)
                {
                    if (((MissionBattleEnemys)mission).NPCSNotDefeated(_npcsDefeated).Count == 0)
                    {
                        mission.Status = Models.Mission.MissionStatus.Complete;
                    }
                }
                else
                {
                    throw new Exception("Unknown Mission Child Class");
                }
            }
        }

        #endregion

        #region Inventory

        public void UpdateInventoryCategories()
        {
            HealthItems.Clear();
            Weapons.Clear();
            Credits.Clear();
            LightSaberParts.Clear();


            foreach (var gameItemModelQuantity in _inventory)
            {
                if (gameItemModelQuantity.GameItemModel is HealthItems) HealthItems.Add(gameItemModelQuantity);
                if (gameItemModelQuantity.GameItemModel is Weapons) Weapons.Add(gameItemModelQuantity);
                if (gameItemModelQuantity.GameItemModel is Credits) Credits.Add(gameItemModelQuantity);
                if (gameItemModelQuantity.GameItemModel is LightSaberParts) LightSaberParts.Add(gameItemModelQuantity);

            }
        }
        public void AddGameItemModelToInventory(GameItemModelQuantity selectedGameItemModelQuantity)
        {
            GameItemModelQuantity gameItemModelQuantity = _inventory.FirstOrDefault(i => i.GameItemModel.Id == selectedGameItemModelQuantity.GameItemModel.Id);

            if (gameItemModelQuantity == null)
            {
                GameItemModelQuantity newGameItemModelQuantity = new GameItemModelQuantity();
                newGameItemModelQuantity.GameItemModel = selectedGameItemModelQuantity.GameItemModel;
                newGameItemModelQuantity.Quantity = 1;

                _inventory.Add(newGameItemModelQuantity);
            }
            else
            {
                gameItemModelQuantity.Quantity++;
            }

            UpdateInventoryCategories();
        }
        public void RemoveGameItemModelToInventory(GameItemModelQuantity selectedGameItemModelQuantity)
        {
            GameItemModelQuantity gameItemModelQuantity = _inventory.FirstOrDefault(i => i.GameItemModel.Id == selectedGameItemModelQuantity.GameItemModel.Id);

            if (gameItemModelQuantity != null)
            {
                if (selectedGameItemModelQuantity.Quantity == 1)
                {
                    _inventory.Remove(gameItemModelQuantity);
                }
                else
                {
                    gameItemModelQuantity.Quantity--;
                }
            }
            UpdateInventoryCategories();
        }
        public void AddQuestsToQuests(Mission missions) 
        {
            Mission missionsAdd = _missions.FirstOrDefault(m => m.Id == missions.Id);
            if (missionsAdd == null)
            {
                Mission newMissions = new Mission();
                newMissions.Id = missions.Id;

                _missions.Add(newMissions);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Error: You already have the missions");
            }
        }

        #endregion

        public int Attack()
        {
            int hitPoints = random.Next(CurrentGameItemWeapon.MinimumDamage, CurrentGameItemWeapon.MaximumDamage);

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
        public int Retreat()
        {
            int hitPoints = MAXIMUM_RETREAT_DAMAGE;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }

        public override string DefaultGreeting()
        {
            string article = "a";

            List<string> vowels = new List<string>() { "A", "E", "I", "O", "U" };

            if (vowels.Contains(_forceSide.ToString().Substring(0, 1)));
            {
                article = "A";
            }

            return $"Hello, my name is {_name} and I am {article} {_forceSide}.";
        }

        #endregion

        #region EVENTS



        #endregion

    }
}
