using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;

namespace CSCD349Project
{

    class GameEngine
    {
        private int noLevels = 1;
        private int currentLevel = 1;
        private List<Map> _Maps;
        private Messaging _messaging;
        public static void Main(string[]args)
        {
            GameEngine thisGame = new GameEngine();
            thisGame.Initialize();
            //thisGame.Run();

            Console.ReadKey();
        }

        public GameEngine()
        {
            _Maps = new List<Map>();
            _messaging = new Messaging();
            GameCharacter.SetMessaging(_messaging);
            Party.SetMessaging(_messaging);
            // create the maps
            for (int i = 0; i < noLevels; ++i)
                _Maps.Add(new Map(CSCD349Project.Properties.Resources.level0));




        }


        public void PrintArray(string[] array)
        {
            foreach (string thisLine in (new List<string>(array)))
                Console.WriteLine(thisLine);

        }




        private void  Initialize()
        {
            // Read in and set up maps
            //Console.WriteLine(_Maps[0]);
            
            // Randomly generate items and enemy parties in the map
            _Maps[0].PopulateMap();

            Console.WriteLine(_Maps[0]);



            GameCharacter warrior = new Warrior();
            GameCharacter mage = new Mage();
            GameCharacter scout = new Scout();

            GameCharacter landShark = new LandShark();
            GameCharacter gargoyle = new Gargoyle();
            GameCharacter leprachaun = new Leprachaun();
            
            

            Party goodParty = new Party("good party", _Maps[0].GetCellAt(0, 0));
            goodParty.AddCharacter(warrior);
            goodParty.AddCharacter(mage);
            goodParty.AddCharacter(scout);

            Party badParty = new Party("bad party", _Maps[0].GetCellAt(0, 0));
            badParty.AddCharacter(landShark);
            badParty.AddCharacter(gargoyle);
            badParty.AddCharacter(leprachaun);


            warrior.SetParty(goodParty);
            mage.SetParty(goodParty);
            scout.SetParty(goodParty);

            landShark.SetParty(badParty);
            gargoyle.SetParty(badParty);
            leprachaun.SetParty(badParty);




            warrior.GetAttributes().SetActiveAttack(warrior.GetAttributes().GetAttacks()[0]);
            mage.GetAttributes().SetActiveAttack(mage.GetAttributes().GetAttacks()[0]);
            scout.GetAttributes().SetActiveAttack(scout.GetAttributes().GetAttacks()[0]);
            landShark.GetAttributes().SetActiveAttack(landShark.GetAttributes().GetAttacks()[0]);
            gargoyle.GetAttributes().SetActiveAttack(gargoyle.GetAttributes().GetAttacks()[0]);
            leprachaun.GetAttributes().SetActiveAttack(leprachaun.GetAttributes().GetAttacks()[0]);

            warrior.GetAttributes().SetActiveDefense(warrior.GetAttributes().GetDefenses()[0]);
            mage.GetAttributes().SetActiveDefense(mage.GetAttributes().GetDefenses()[0]);
            scout.GetAttributes().SetActiveDefense(scout.GetAttributes().GetDefenses()[0]);
            landShark.GetAttributes().SetActiveDefense(landShark.GetAttributes().GetDefenses()[0]);
            gargoyle.GetAttributes().SetActiveDefense(gargoyle.GetAttributes().GetDefenses()[0]);
            leprachaun.GetAttributes().SetActiveDefense(leprachaun.GetAttributes().GetDefenses()[0]);



            
         
                warrior.PerformActiveAttack(landShark);
                mage.PerformActiveAttack(gargoyle);
                scout.PerformActiveAttack(leprachaun);

                landShark.PerformActiveAttack(warrior);
                gargoyle.PerformActiveAttack(mage);
                leprachaun.PerformActiveAttack(scout);
            

            
            
            GameItem largeHealthPotion = new LargeHealthPotion();
            GameItem largeEnergyPotion = new LargeEnergyPotion();

            goodParty.AddGameItem(largeHealthPotion);
            goodParty.AddGameItem(largeEnergyPotion);

            Console.WriteLine(goodParty.ToString());
            Console.WriteLine(badParty.ToString());
            PrintArray(_messaging.getLatestMessages());


            // Place good party in map (active cell)

            //end
        }



        private void Run()
        {
            if (currentLevel < noLevels)
            {
                Map thisLevel = _Maps[currentLevel - 1];
                Cell thisCell;

                // If active cell is finish then display victory message and move to next map
                thisCell = thisLevel._activeCell;

                // Start NavigateMap()
                // If there is an enemy in the active cell's enemy party
                    // Start Combat()
            }
            else
            {
                //Awesome victory message (YOU HAVE WON THE GAME!!!)
            }
        }

    
        public void ShowErrorMessage(string message)
        {
            //MessageBox.Show(message);
        }
    }
}
