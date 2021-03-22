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
        private ObservableCollection<GameItemModel> _gameItems;
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
            set { _planetLog = value; }
        }

        public ObservableCollection<GameItemModel> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public Location()
        {
            _gameItems = new ObservableCollection<GameItemModel>();
        }
        #endregion

        #region METHODS
        public void UpdateLocationGameItems()
        {
            ObservableCollection<GameItemModel> updatedLocationGameItems = new ObservableCollection<GameItemModel>();

            foreach (GameItemModel GameItem in _gameItems)
            {
                updatedLocationGameItems.Add(GameItem);
            }

            GameItems.Clear();

            foreach (GameItemModel gameItem in updatedLocationGameItems)
            {
                GameItems.Add(gameItem);
            }
        }
        public void AddGameItemModelToLocation(GameItemModel selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Add(selectedGameItem);
            }

            UpdateLocationGameItems();
        }
        public void RemoveGameItemModelFromLocation(GameItemModel selectedGameItem)
        {
            if (selectedGameItem != null)
            {
                _gameItems.Remove(selectedGameItem);
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
