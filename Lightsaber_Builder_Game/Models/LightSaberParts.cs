using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class LightSaberParts : GameItemModel
    {
        public enum LightsaberPart 
        {
            BladeEmitter,
            MainHilt,
            Controls,
            HandGrip,
            EnergyCore
        }

        public LightsaberPart Type { get; set; }

        public LightSaberParts(int id, string name, int value, LightsaberPart type, string description)
    : base(id, name, value, description)
        {
            Type = type;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
