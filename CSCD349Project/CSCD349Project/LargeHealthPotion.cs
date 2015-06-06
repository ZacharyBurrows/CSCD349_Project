using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD349Project
{
    public class LargeHealthPotion : GameItem
    {
        //Default constructor for LargeHealthPotion
        public LargeHealthPotion()
        {
            _name = "Large Health Potion";
            _description = "A potion that heals 75 health.";
        }

        public override void Use(GameCharacter character)
        {
            //We can adjust the values to achieve a better balance. AH
            character.GetAttributes()._health += 75.0;
            //Test text
            AddMessage("The potion healed 75 health!");

        }
    }
}
