using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class NPCBattle : NPCS, ISpeak, IBattle
    {
        private const int ENEMY_DAMAGE_ADJUSTMENT = 10;
        private const int MAXIMUM_RETREAT_DAMAGE = 10;

        public List<string> Messages { get; set; }
        public BattleEnum BattleMode { get; set; }
        public Weapons CurrentGameItemWeapon { get; set; }

        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }
        public NPCBattle() 
        {

        }
        public NPCBattle(
            int id,
            string name,
            int health,
            RaceType race,
            string description,
            List<string> messages,
            Weapons currentWeapon)
            : base(id, name, health, race, description) 
        {
            Health = health;
            Messages = messages;
            CurrentGameItemWeapon = currentWeapon;
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
            int messageIndex = random.Next(0, Messages.Count());
            return Messages[messageIndex];
        }
        public int Attack()
        {
            int hitPoints = random.Next(CurrentGameItemWeapon.MinimumDamage, CurrentGameItemWeapon.MaximumDamage);

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
        public int Retreat()
        {
            int hitPoints = MAXIMUM_RETREAT_DAMAGE;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
    }
}
