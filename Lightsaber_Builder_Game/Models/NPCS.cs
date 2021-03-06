using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public abstract class NPCS : Character
    {
        public string Description { get; set; }
        public string Information 
        {
            get 
            {
                return InformationText();
            }
            set 
            {

            }
        }
        public NPCS() 
        {

        }
        public NPCS(int id, string name, int health, RaceType race, string description)
            : base(id, name, race) 
        {
            Id = id;
            Name = name;
            Health = health;
            Race = race;
            Description = description;
        }

        protected NPCS(int id, string name, RaceType race, string description)
        {
            Id = id;
            Name = name;
            Race = race;
            Description = description;
        }

        protected abstract string InformationText();
    }
}
