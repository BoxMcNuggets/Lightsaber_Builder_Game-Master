using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class HealthItems : GameItemModel
    {
        public int HealthChange { get; set; }
        public int LivesChange { get; set; }

        public HealthItems(int id, string name, int value, int healthChange, int livesChange, string description)
            : base(id, name, value, description)
        {
            HealthChange = healthChange;
            LivesChange = livesChange;
        }

        public override string InformationString()
        {
            if (HealthChange != 0)
            {
                return $"{Name}: {Description}\nHealth: {HealthChange}";
            }
            else if (LivesChange != 0)
            {
                return $"{Name}: {Description}\nLives: {LivesChange}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }
    }
}
