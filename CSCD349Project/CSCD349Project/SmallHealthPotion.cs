//SmallHealthPotion.cs
//Audrey Henry

using System;

namespace CSCD349Project
{
    public class SmallHealthPotion : GameItem
    {
        //Default constructor for SmallHealthPotion
        public SmallHealthPotion()
        {
            _name = "Small Health Potion";
            _description = "A potion that heals 25 health.";
        }

        public override void Use(GameCharacter character)
        {
            //We can adjust the values to achieve a better balance. AH
            character.GetAttributes()._health += 25.0;
            //Test text
            AddMessage("The potion healed 25 health!");

        }
    }
}
