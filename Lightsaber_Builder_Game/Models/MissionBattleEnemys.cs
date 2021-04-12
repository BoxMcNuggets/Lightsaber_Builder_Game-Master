using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    class MissionBattleEnemys : Mission
    {
        private int _id;
        private string _name;
        private MissionStatus _status;
        private List<NPCS> _requiredNPCs;

        public List<NPCS> RequiredNPCS
        {
            get { return _requiredNPCs; }
            set { _requiredNPCs = value; }
        }
        public MissionBattleEnemys() 
        {

        }
        public MissionBattleEnemys(int id, string name, MissionStatus status, List<NPCS> requiredNPCS)
            : base(id, name, status)
        {
            _id = id;
            _name = name;
            _status = status;
            _requiredNPCs = requiredNPCS;
        }
        public List<NPCS> NPCSNotDefeated(List<NPCS> NPCSNotDefeated)
        {
            List<NPCS> nPCSToDefeate = new List<NPCS>();

            foreach (var requiredNPCS in _requiredNPCs)
            {
                if (!NPCSNotDefeated.Any(l => l.Id == requiredNPCS.Id))
                {
                    nPCSToDefeate.Add(requiredNPCS);
                }
            }
            return nPCSToDefeate;
        }

    }
}
