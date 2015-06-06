using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD349Project
{
    public class MediumHealthPotion : GameItem
    {
        //Default constructor for MediumHealthPotion
        public MediumHealthPotion()
        {
            _name = "Medium Health Potion";
            _description = "A potion that heals 50 health.";
        }

        public override void Use(GameCharacter character)
        {
            //We can adjust the values to achieve a better balance. AH
            character.GetAttributes()._health += 50.0;
            //Test text
            AddMessage("The potion healed 50 health!");

        }
    }
}
