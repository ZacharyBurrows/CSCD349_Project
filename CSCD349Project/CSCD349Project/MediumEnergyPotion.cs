using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD349Project
{
    class MediumEnergyPotion : GameItem
    {
        public MediumEnergyPotion()
        {
            _name = "Medium Energy Potion";
            _description = "A potion that restores 35 energy points.";
        }

        public override void Use(GameCharacter character)
        {
            character.GetAttributes()._energy += 35;
            AddMessage("The potion restored 35 energy points!");
        }
    }
}
