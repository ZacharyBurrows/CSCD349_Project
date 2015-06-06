using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD349Project
{
    class LargeEnergyPotion : GameItem
    {
        public LargeEnergyPotion()
        {
            _name = "Large Energy Potion";
            _description = "A potion that restores 50 energy points.";
        }

        public override void Use(GameCharacter character)
        {
            character.GetAttributes()._energy += 50;
            AddMessage("The potion restored 50 energy points!");
        }
    }
}
