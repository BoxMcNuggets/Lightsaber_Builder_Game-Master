using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Lightsaber_Builder_Game.Models
{
    public class Player : Character
    {
        #region ENUMS

        public enum ForceSide { LightSide, DarkSide }

        #endregion

        #region FIELDS

        private int _lives;
        private int _health;
        private ForceSide _forceSide;
        private List<Location> _locationsVisited;
        private ObservableCollection<GameItemModel> _inventory = new ObservableCollection<GameItemModel>();
        private ObservableCollection<GameItemModel> _healthItems;
        private ObservableCollection<GameItemModel> _credits;
        private ObservableCollection<GameItemModel> _weapons;
        private ObservableCollection<GameItemModel> _lightSaberParts;

        #endregion

        #region PROPERTIES

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

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        public ObservableCollection<GameItemModel> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public ObservableCollection<GameItemModel> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }

        public ObservableCollection<GameItemModel> HealthItems
        {
            get { return _healthItems; }
            set { _healthItems = value; }
        }

        public ObservableCollection<GameItemModel> Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        public ObservableCollection<GameItemModel> LightSaberParts
        {
            get { return _lightSaberParts; }
            set { _lightSaberParts = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public Player()
        {
            _locationsVisited = new List<Location>();
            _weapons = new ObservableCollection<GameItemModel>();
            _healthItems = new ObservableCollection<GameItemModel>();
            _credits = new ObservableCollection<GameItemModel>();
            _lightSaberParts = new ObservableCollection<GameItemModel>();
        }

        #endregion

        #region METHODS
        public void UpdateInventoryCategories()
        {
            HealthItems.Clear();
            Weapons.Clear();
            Credits.Clear();
            LightSaberParts.Clear();

            foreach (var gameItem in _inventory)
            {
                if (gameItem is HealthItems) HealthItems.Add(gameItem);
                if (gameItem is Weapons) Weapons.Add(gameItem);
                if (gameItem is Credits) Credits.Add(gameItem);
                if (gameItem is LightSaberParts) LightSaberParts.Add(gameItem);
            }
        }
        public void AddGameItemToInventory(GameItemModel selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Add(selectedGameItem);
            }
        }
        public void RemoveGameItemFromInventory(GameItemModel selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _inventory.Remove(selectedGameItem);
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
