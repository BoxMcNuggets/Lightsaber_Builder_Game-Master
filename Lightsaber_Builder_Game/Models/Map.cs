using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Lightsaber_Builder_Game.Models
{
    public class Map
    {
        #region FIELDS

        private List<Location> _locations;
        private Location _currentlocation;
        private ObservableCollection<Location> _accessibleLocations;

        #endregion

        #region PROPERTIES

        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }
        public Location Currentlocation
        {
            get { return _currentlocation; }
            set { _currentlocation = value; }
        }
        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessibleLocations; }
            set { _accessibleLocations = value; }
        }

        #endregion

        public Map() 
        {
            _locations = new List<Location>();
        }
        public void Move(Location location) 
        {
            _currentlocation = location;
        }
        public bool CanMove(Location location) 
        {
            return location.Accessible;
        }
    }
}
