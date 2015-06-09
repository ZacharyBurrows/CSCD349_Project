using System;
using System.Collections.Generic;
namespace CSCD349Project
{
    public class Party
    {
        private Cell _OccupiedCell { get; set; }
        private string _Name;
        private List<GameCharacter> _Characters;
        private List<GameItem> _Inventory;
        private static Messaging _messaging;
        public Party(string name, Cell cell)
        {
            _Name = name;
            _OccupiedCell = cell;
            _Characters = new List<GameCharacter>();
            _Inventory  = new List<GameItem>();
        }
        public static void SetMessaging(Messaging messaging)
        {
            if (messaging != null)
                _messaging = messaging;
        }

        public void AddGameItem(GameItem newItem)
        {
            if (newItem == null)
                throw new Exception("The GameItem passed nto AcquireGameItem was null!!!");

            AddMessage("party " + _Name + " recieves new item " + newItem._name);
            if (_Inventory != null)
                _Inventory.Add(newItem);
        }

        public void AddMessage(string message)
        {
            if (_messaging != null && message != null)
                _messaging.AddMessage(message);
        }
        public List<GameItem> GetIventory() { return _Inventory; }
        public List<GameCharacter> GetCharacters() { return _Characters; }
        public string GetName() { return _Name; }        
        public int AddCharacter(GameCharacter character)
        {
            _Characters.Add(character);
            return _Characters.Count;
        }


        public bool RemoveCharacter(GameCharacter character)
        {
            return _Characters.Remove(character);
        }
        
        public override string ToString()
        {
            string output = "\n";
            output += "#--------- party ---------#\n";
            output += "name: " + _Name + "\n";
            output += "cell: \n"     + _OccupiedCell.ToString() + "\n";
            output += "characters: \n" + string.Join("\n", (object[])_Characters.ToArray());
            output += "inventory: \n" + string.Join("\n", (object[])_Inventory.ToArray());
            output += "\n#------------------------#\n";
            return output;
        }

        internal void HandleCharacterDeath(GameCharacter deadPlayer)
        {
            try
            {
                _Characters.Remove(deadPlayer);
            }
            catch
            {

            }
        }

       public void PartyRegenEnergy(double percentRegen)
       {
           if(percentRegen >= 0 && percentRegen <= 100)
           {
               var list = this._Characters;
    
               foreach (var item in list)
                   item.GetAttributes()._energy = (percentRegen / 100)*(item.GetAttributes()._baseEnergy);
           }
       }
    }
}
