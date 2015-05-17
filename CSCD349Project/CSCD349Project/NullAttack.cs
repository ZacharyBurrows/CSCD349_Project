﻿using System;
namespace CSCD349Project
{
    public class NullAttack : OffensiveAbility
    {
        public NullAttack(){}

        public override string ToString() { return "null Attack"; }
        public void attack(GameCharacter attacker, GameCharacter defender)
        {
            // perform attack
        }


        public bool attackSuccessful(double successRate)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
