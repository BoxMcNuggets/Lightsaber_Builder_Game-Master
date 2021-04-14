using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public class NPCSpeak : NPCS, ISpeak
    {
        public List<string> Messages { get; set; }

        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }
        public NPCSpeak() 
        {

        }
        public NPCSpeak(int id, string name, RaceType race, string description, List<string> messages)
            : base(id, name, race, description) 
        {
            Messages = messages;

        }
        public string Speak() 
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "";
            }
        }
        private string GetMessage() 
        {
            Random r = new Random();
            int messageIndex = r.Next(0, Messages.Count());
            return Messages[messageIndex];
        }

    }
}
