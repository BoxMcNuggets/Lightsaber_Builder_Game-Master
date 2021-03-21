using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class Credits : GameItemModel
    {
        public enum Currency
        {
            Credits,
            MandalorianCredits,
            Coaxium
        }

        public Currency Credit { get; set; }

        public Credits(int id, string name, int value, Currency credit, string description)
            : base(id, name, value, description)
        {
            Credit = credit;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
