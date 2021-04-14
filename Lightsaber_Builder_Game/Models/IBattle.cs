using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    interface IBattle
    {
        Weapons CurrentGameItemWeapon { get; set; }
        BattleEnum BattleMode { get; set; }

        //
        // methods to return hit point values (0 - 100) for each battle mode
        //
        int Attack();
        int Retreat();
    }
}
