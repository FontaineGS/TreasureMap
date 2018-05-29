using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TreasureMap.Model;

namespace TreasureMap.Parsing
{
    internal class AdventureParser : IParser
    {
        public void Parse(string filename, Map map, List<Adventurer> adventurers)
        {
           
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    // on récupére le premier caractéres
                    char firstChar = line[0];
                    switch(firstChar)
                    {
                        case '#':
                            break;
                        case 'C':
                            LoadMap(line, map);
                            break;
                        case 'M':
                            LoadMoutains(line, map);
                            break;
                        case 'T':
                            LoadTreasure(line, map);
                            break;
                        case 'A':
                            LoadAdventurer(line, adventurers);
                            break;
                        default:
                            throw new FormatException();
                    }
                }
            }
        }

        #region private

        private void LoadMap(string line, Map map)
        {

            //On récupére les données
            var paramList = ParseLine(line);
            map.Height = int.Parse(paramList[2]);
            map.Whidth = int.Parse(paramList[1]);
        }

        private void LoadTreasure(string line, Map map)
        {

            //On récupére les données
            var paramList = ParseLine(line);

            Point position;
            int nb;
            position.X = int.Parse(paramList[1]);
            position.Y = int.Parse(paramList[2]);
            nb = int.Parse(paramList[3]);


            Treasure treasure;
            treasure.Position = position;
            treasure.Number = nb;


            map.Treasures.Add(treasure);
        }

        private void LoadMoutains(string line, Map map)
        {
            //On récupére les données
            var paramList = ParseLine(line);


            Point position;
            position.X = int.Parse(paramList[1]);
            position.Y = int.Parse(paramList[2]);

            map.Moutains.Add(position);
        }

        private void LoadAdventurer(string line, List<Adventurer> adventurers)
        {
            var paramList = ParseLine(line);


            Point position;
            position.X = int.Parse(paramList[2]);
            position.Y = int.Parse(paramList[3]);

            string name = paramList[1];

            char charO = paramList[4][0];


            Orientation orientation;
            switch (charO)
            {
                case 'N':
                    orientation = Orientation.Nord;
                    break;
                case 'O':
                    orientation = Orientation.Ouest;
                    break;
                case 'S':
                    orientation = Orientation.Sud;
                    break;
                case 'E':
                    orientation = Orientation.Est;
                    break;
                default:
                    throw new FormatException();
            }

            Queue<Move> queue = new Queue<Move>();

            string charmoves = paramList[5];
            foreach(char c in charmoves)
            {
                switch (c)
                {
                    case 'A':
                        queue.Enqueue(Move.Front);
                        break;
                    case 'G':
                        queue.Enqueue(Move.Left);
                        break;
                    case 'D':
                        queue.Enqueue(Move.Right);
                        break;
                    default:
                        throw new FormatException();
                }
            }

            //On remplit l'aventurier et on l'ajoute

            Adventurer adventurer = new Adventurer();
            adventurer.Position = position;
            adventurer.Name = name;
            adventurer.Orientation = orientation;
            adventurer.MoveQueue = queue;
            adventurer.Treasure = 0;


            adventurers.Add(adventurer);
        }

        #region utilities
        private string[] ParseLine(string line)
            { 
       
            var paramList = line.Split('-');

            //Imprécision sur le format, donc dans le doute . . .
            for(int i = 0; i<paramList.Length; i++)
            {
                paramList[i] = paramList[i].Trim();
            }

            return paramList;
        }

        #endregion 

        #endregion

    }
}
