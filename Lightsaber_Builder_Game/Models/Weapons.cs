using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class Weapons : GameItemModel
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public Weapons(int id, string name, int value, int minDamage, int maxDamage, string description)
            : base(id, name, value, description)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nDamage: {MinimumDamage}-{MaximumDamage}";
        }
    }
}
