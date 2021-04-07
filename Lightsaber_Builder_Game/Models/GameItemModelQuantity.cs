using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public class GameItemModelQuantity
    {
        public GameItemModel GameItemModel { get; set; }
        public int Quantity { get; set; }

        public GameItemModelQuantity()
        {

        }

        public GameItemModelQuantity(GameItemModel gameItem, int quantity)
        {
            GameItemModel = gameItem;
            Quantity = quantity;
        }
    }
}
