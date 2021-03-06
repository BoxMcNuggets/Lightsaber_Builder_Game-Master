using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public class Weapons : GameItemModel
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public string Using { get; set; }

        public Weapons(int id, string name, int value, int minDamage, int maxDamage, string description, string inUse)
            : base(id, name, value, description)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            Using = inUse;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nDamage: {MinimumDamage}-{MaximumDamage}";
        }
    }
}
