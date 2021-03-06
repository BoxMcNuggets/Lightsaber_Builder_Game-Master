using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public class Character : ObservableObject
    {
        #region ENUMERABLES

        public enum RaceType
        {
            Human,
            Kaminoan,
            Ithorians,
            MonCalamari,
            Togruta,
            Zabrak,
            Droid,
            Unknown
        }

        #endregion

        #region FIELDS

        protected int _id;
        protected string _name;
        private int _health;
        protected int _locationId;
        protected int _age;
        protected RaceType _race;

        protected Random random = new Random();

        #endregion

        #region PROPERTIES

        public int Id
        {
            get { return _id; }
            set 
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Health
        {
            get { return _health; }
            set 
            { 
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        public int LocationId
        {
            get { return _locationId; }
            set 
            {
                _locationId = value;
                OnPropertyChanged(nameof(LocationId));
            }
        }
        public int Age
        {
            get { return _age; }
            set 
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
        public RaceType Race
        {
            get { return _race; }
            set 
            {
                _race = value;
                OnPropertyChanged(nameof(Race));
            }
        }

        #endregion

        #region CONSTRUCTORS

        public Character()
        {

        }

        public Character(int id, string name, RaceType race)
        {
            _id = id;
            _name = name;
            _race = race;
        }

        #endregion

        #region METHODS

        public virtual string DefaultGreeting()
        {
            return $"Hello, my name is {_name} and I am a {_race}.";
        }

        #endregion
    }
}
