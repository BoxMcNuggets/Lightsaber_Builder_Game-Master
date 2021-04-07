using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Lightsaber_Builder_Game.Models
{
    public class Location : ObservableObject
    {
        #region FIELDS

        private int _id;
        private string _name;
        private string _description;
        private bool _accessible;
        private string _message;
        private string _planetColor;
        private string _planetLog;
        private ObservableCollection<GameItemModelQuantity> _gameItems;
        private ObservableCollection<NPCS> _npcs;

        #endregion

        #region PROPERTIES

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string PlanetColor
        {
            get { return _planetColor; }
            set 
            { 
                _planetColor = value;
                OnPropertyChanged(nameof(PlanetColor));
            }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string PlanetLog
        {
            get { return _planetLog; }
            set 
            { 
                _planetLog = value;
                OnPropertyChanged(nameof(PlanetLog));
            }
        }
        public ObservableCollection<NPCS> NPCs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }
        public ObservableCollection<GameItemModelQuantity> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Location()
        {
            _gameItems = new ObservableCollection<GameItemModelQuantity>();
        }

        #endregion

        #region METHODS

        public void UpdateLocationGameItems()
        {
            ObservableCollection<GameItemModelQuantity> updatedLocationGameItems = new ObservableCollection<GameItemModelQuantity>();

            foreach (GameItemModelQuantity GameItemQuantity in _gameItems)
            {
                updatedLocationGameItems.Add(GameItemQuantity);
            }

            GameItems.Clear();

            foreach (GameItemModelQuantity GameItemQuantity in updatedLocationGameItems)
            {
                GameItems.Add(GameItemQuantity);
            }
        }
        public void AddGameItemModelToLocation(GameItemModelQuantity selectedGameItemQuantity)
        {
            GameItemModelQuantity gameItemQuantity = _gameItems.FirstOrDefault(i => i.GameItemModel.Id == selectedGameItemQuantity.GameItemModel.Id);

            if (gameItemQuantity == null)
            {
                GameItemModelQuantity newGameItemModelQuantity = new GameItemModelQuantity();
                newGameItemModelQuantity.GameItemModel = selectedGameItemQuantity.GameItemModel;
                newGameItemModelQuantity.Quantity = 1;

                _gameItems.Add(newGameItemModelQuantity);
            }
            else
            {
                gameItemQuantity.Quantity++;
            }

            UpdateLocationGameItems();
        }
        public void RemoveGameItemModelFromLocation(GameItemModelQuantity selectedGameItemQuantity)
        {
            GameItemModelQuantity gameItemQuantity = _gameItems.FirstOrDefault(i => i.GameItemModel.Id == selectedGameItemQuantity.GameItemModel.Id);

            if (selectedGameItemQuantity != null)
            {
                if (selectedGameItemQuantity.Quantity == 1)
                {
                    _gameItems.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateLocationGameItems();
        }
        #endregion
        public override string ToString()
        {
            return _name;
        }
    }
}
