using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class MissionLightsaberParts : Mission
    {
        private int _id;
        private string _name;
        private MissionStatus _status;
        private List<GameItemModelQuantity> _requiredGameItems;

        public MissionLightsaberParts() 
        {

        }
        public MissionLightsaberParts(int id, string name, MissionStatus status, List<GameItemModelQuantity> requiredGameItems)
            : base(id, name, status) 
        {
            _id = id;
            _name = name;
            _status = status;
            _requiredGameItems = requiredGameItems;
        }


    }
}
