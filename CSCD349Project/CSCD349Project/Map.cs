using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCD349Project
{
    public sealed class Map
    {
        private int[] _Dimensions;
        private Cell[,] _Cells;
        private Cell _ActiveCell, _StartCell, _FinishCell;
        private AbstractEnemyFactory _EnemyFactory;
        private AbstractItemFactory _ItemFactory;

        /*
         * public Map(int[] dimensions, int[] startCoordinates, int[] finishCoordinates, AbstractEnemyFactory enemyFactory, AbstractItemFactory itemFactory)
        {
            _Cells = new Cell[dimensions[0],dimensions[1]];
            _Dimensions = dimensions;
            int r,c;
            for(r = 0; r < dimensions[0] - 1; ++r)
            {
                for(c = 0; c < dimensions[1] - 1; ++c)
                {
                    _Cells[r, c] = new Cell(new int[] { r, c }, this, true, false, false);
                }
            }
            _EnemyFactory = enemyFactory;
            _ItemFactory = itemFactory;

            //Set start, finish, and active cells
            _StartCell = _Cells[startCoordinates[0], startCoordinates[1]];
            _FinishCell = _Cells[finishCoordinates[0], finishCoordinates[1]];
            _ActiveCell = _StartCell;
        }
         * */

        //Build a map from file
        //CONSIDER MAKING THIS A CLASS
        public Map(string mapFile)
        {
            _EnemyFactory = new LevelOneEnemyFactory();
            _ItemFactory = new LevelOneItemFactory();
            int noRows = 0;
            int noCols = 0;

            // read in a map from a map file
            try
            {
                // get dimensions
                using (StringReader sr = new StringReader(mapFile))
                {
                    string line = "";
                    char[] lineChars;
                    char curChar;
                    int[] curCoords;

                    //Read in dimensions
                    if ((line = sr.ReadLine()) != null)
                        noRows = Convert.ToInt32(line);
                    if ((line = sr.ReadLine()) != null)
                        noCols = Convert.ToInt32(line);

                    _Dimensions = new int[]{noRows, noCols};

                    _Cells = new Cell[noRows, noCols];
                    
                    //Read in map
                    for(int r=0; r<noRows; r++)
                    {
                        line = sr.ReadLine();
                        lineChars = line.ToCharArray();
                        for(int c=0; c<noCols; c++)
                        {
                            curCoords = new int[2];
                            curCoords[0] = r; curCoords[1] = c;
                            curChar = lineChars[c];
                            
                            //int[] coordinates, Map thisMap, bool traversable, bool start, bool finish
                            switch(curChar)
                            {
                                case 's'://start cell
                                    _Cells[r,c] = new Cell(curCoords, this, true, true, false);
                                    _StartCell = _Cells[r,c];
                                    break;
                                case 'f'://finish cell
                                    _Cells[r, c] = new Cell(curCoords, this, true, false, true);
                                    _FinishCell = _Cells[r,c];
                                    break;
                                case 't'://traversable cell
                                    _Cells[r, c] = new Cell(curCoords, this, true, false, false);
                                    break;
                                case 'n'://nontraversable cell
                                    _Cells[r, c] = new Cell(curCoords, this, false, false, false);
                                    break;
                            }
                        }
                    }



                    //Read in map
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //thisMap = new Map(new int[] { noRows, noCols });
        }
        public void PopulateMap()
        {
            for (int r = 0; r < _Dimensions[0]; ++r)
            {
                for(int c = 0; c < _Dimensions[1]; ++c)
                {
                    if (_Cells[r, c]._traversable && !_Cells[r, c]._isStart && !_Cells[r, c]._isFinish)
                    {
                        _Cells[r, c].GenerateEnemies();
                        _Cells[r, c].GenerateItems();
                    }
                }
            }
        }
        public Cell GetCellAt(int row, int column)
        {
            return _Cells[row, column];
        }
        
        public AbstractEnemyFactory GetEnemyFactory()
        {
            return _EnemyFactory;
        }

        public AbstractItemFactory GetItemFactory()
        {
            return _ItemFactory;
        }

        public Cell _activeCell
        {
            get { return _ActiveCell; }
        }
        
        public Cell _startCell
        {
            get { return _StartCell; }
        }
        
        public Cell _finishCell
        {
            get { return _finishCell; }
        }


        public override string ToString()
        {
            string output = TraversableToString();
            output += NOEnemiesToString();
            output += NOItemsToString();

            return output;
        }

        private string TraversableToString()
        {
            string output = "\n\n---------------------------------";
            output += "\nTRAVERSABLE REPRESENTATION";
            output += "\n---------------------------------";

            int r, c;
            for (r = 0; r < _Dimensions[0]; ++r)
            {
                output += "\n";
                for (c = 0; c < _Dimensions[1]; ++c)
                {
                    //_Cells[r, c] = new Cell(new int[] { r, c }, this);
                    if (_Cells[r, c]._isStart)
                        output += "S";
                    else if (_Cells[r, c]._isFinish)
                        output += "F";
                    else if (_Cells[r, c] == _ActiveCell)
                        output += "X";
                    else
                    {
                        if (_Cells[r, c]._traversable)
                            output += "+";
                        else
                            output += "-";
                    }
                }
                
            }

            return output;
        }



        private string NOItemsToString()
        {
            string output = "\n\n---------------------------------";
            output += "\nITEMS PER CELL";
            output += "\n---------------------------------";
            int r, c;
            for (r = 0; r < _Dimensions[0]; ++r)
            {
                output += "\n";
                for (c = 0; c < _Dimensions[1]; ++c)
                {
                    output += _Cells[r, c]._items.Count;
                }
                
            }

            return output;
        }

        private string NOEnemiesToString()
        {
            string output = "\n\n---------------------------------";
            output += "\nENEMIES PER CELL";
            output += "\n---------------------------------";
            int r, c;
            for (r = 0; r < _Dimensions[0]; ++r)
            {
                output += "\n";
                for (c = 0; c < _Dimensions[1]; ++c)
                {
                    output += _Cells[r, c]._enemies.GetCharacters().Count;
                }
                
            }

            return output;
        }
    }
}
