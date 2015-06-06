using System;
namespace CSCD349Project
{
    public abstract class GameCharacter : GameObject, IComparable<GameCharacter>
    {
        private static int noInstances = 0;
        private int _ID;
        private CharacterAttributes _Attributes;
        private Party _Party;
        private string _Name;
        private static Messaging _Messaging;
        private static Random random;

        public void SetParty(Party newParty) 
        { 
            if(newParty != null)
            {
                _Party = newParty;
            }
        }

        public GameCharacter(string name) : base(name)
        {
            _Name = name;
            _ID = noInstances ++;
            random = new Random();
        }
       
        public static void SetMessaging(Messaging _messaging){_Messaging = _messaging;}

        // This constructor is used to create a GamaCharacter instance with a particular id for the purposes 
        // of searching for a character in a list.
        public GameCharacter(int id) : base("")
        {
            _ID = id;
            random = new Random();
        }
    
        public void PerformActiveAttack(GameCharacter enemy)
        {
            CharacterAttributes attackerAttributes = this._Attributes;
            OffensiveAbility activeAttack = attackerAttributes.GetActiveAttack();
            Double attackDamage = 0.0;

            //Does the attacker have enough Energy to perform the attack? 
            //Is the attack successful given the active attack's success rate?
            if (attackerAttributes._energy >= activeAttack._energyRequired)
            {
                if (AbilitySuccessful(activeAttack._successRate))
                {
                    attackerAttributes._energy -= activeAttack._energyRequired;
                    
                    attackDamage = (attackerAttributes._power) * activeAttack._baseDamage;
                    AddMessage(this.GetName() + "\'s " + activeAttack.ToString() + " attack upon " + enemy.GetName() + " was successful for " + attackDamage + " attack damage!");

                    enemy.PerformActiveDefense(attackDamage);
                    //AddMessage(this.GetName() + "\'s attack and " + enemy.GetName() + "\'s defense are completed. Moving on.\n\n");
                }
                else//Attack Unsuccesful
                    AddMessage(this.GetName() + "\'s " + activeAttack.ToString() + " attack missed " + enemy.GetName() + "!");
            }
            else//Insufficient Energy
                AddMessage(this.GetName() + "is too tired to use " + activeAttack._abilityName + " on " + enemy.GetName());
        }

        private void AddMessage(string thisMessage)
        {
            if(_Messaging != null)
            {
                _Messaging.AddMessage(thisMessage);
            }
        }

        public void PerformActiveDefense(Double incomingDamage)
        {
            CharacterAttributes defenderAttributes = this._Attributes;
            DefensiveAbility activeDefense = defenderAttributes.GetActiveDefense();

            Double damagePrevented = defenderAttributes._armor;

            if (AbilitySuccessful(activeDefense._successRate))
            {
                damagePrevented += activeDefense._armorIncrease;
                AddMessage(this.GetName() + "'s " + activeDefense.ToString() + " defense was successful for a " + damagePrevented + " damage decrease!");

                defenderAttributes._health -= Math.Max(0, incomingDamage - damagePrevented);
                AddMessage(this.GetName() + " lost " + Math.Max(0,incomingDamage - damagePrevented) + " health points");
            }
            else
            {
                AddMessage(this.GetName() + "'s " + activeDefense.ToString() + " defense was unsuccessful! No damage will be prevented!");
                defenderAttributes._health -= Math.Max(0, incomingDamage);
                AddMessage(this.GetName() + " lost " + Math.Max(0,incomingDamage) + " health points");
            }
            if (GetAttributes()._health == 0)
                HandleDeath();
        }

        private void HandleDeath()
        {
            _Messaging.AddMessage(GetName() + " has died :(");
            _Party.HandleCharacterDeath(this);
        }


        private bool AbilitySuccessful(double abilitySuccessRate)
        {
            int percentChance = random.Next(101);//-1, 101);//generate random number between 0 and 100
            
            //AddMessage("In AbilitySuccesful(), Random number is: " + percentChance + " abilitySuccessRate is: " + abilitySuccessRate);

            if (percentChance <= abilitySuccessRate * 100)
                return true;
            return false;
        }

        public int CompareTo(GameCharacter other)
        {
 	        return _ID - other._ID;
        }

        public CharacterAttributes GetAttributes()
        {
            return _Attributes;
        }

        public void SetAttributes(CharacterAttributes attributes)
        {
            if(attributes != null)
                _Attributes = attributes;
        }

        public override string ToString()
        {
            string output = "----- Game Character    id:" + _ID  + "-----\n";
            output += "name:" + GetName() + "\n";
            output += (_Attributes.GetIsGoodGuy()) ? "goodguy\n" : "badguy";
            output += "health: " + _Attributes._health + "\n";
            output += "energy: " + _Attributes._energy + "\n";
            output += "power: "  + _Attributes._power + "\n";
            output += "armor: "  + _Attributes._armor + "\n";
            output += "----------------------------------------------------\n";
            
            return output;
        }
    }
}