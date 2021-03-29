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
            EnergyCore,
        }

        public LightsaberPart Type { get; set; }
        public int LightsaberProgress { get; set; }

        public LightSaberParts(int id, string name, int value, LightsaberPart type, string description, int LightsaberProg)
    : base(id, name, value, description)
        {
            Type = type;
            LightsaberProgress = LightsaberProg;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
