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
        private int _id;
        private string _name;
        private string _description;
        private bool _accessible;
        private string _message;
        private string _planetColor;
        private ObservableCollection<GameItemModel> _gameItems;
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
        public ObservableCollection<GameItemModel> GameItems
        {
            get { return _gameItems; }
            set { _gameItems = value; }
        }
        public Location() 
        {

        }
        public override string ToString()
        {
            return _name;
        }
    }
}
