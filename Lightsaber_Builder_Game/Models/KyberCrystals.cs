using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class KyberCrystals : GameItemModel
    {
        public enum KyberCrystal 
        {
            Red,
            Blue, 
            Black, 
            Green
        }
        public KyberCrystal Color { get; set; }
        public int LightsaberProgress { get; set; }

        public KyberCrystals(int id, string name, int value, KyberCrystal color, string description, int LightsaberProg)
            : base(id, name, value, description) 
        {
            Color = color;
            LightsaberProgress = LightsaberProg;
        }
        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
