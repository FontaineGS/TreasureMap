using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TreasureMap.Model;

namespace TreasureMap.Parsing
{
    internal class AdventureWriter : IWriter
    {
        public void Write(string filename, Map map, List<Adventurer> adventurers)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                //la carte
                writer.WriteLine(WriteMap(map));

                //les montagnes
                foreach(Point mountain in map.Moutains)
                {
                    writer.WriteLine(WriteMountain(mountain));
                }

                foreach(Treasure trasure in map.Treasures)
                {
                    writer.WriteLine(WriteTreasure(trasure));
                }

                foreach (Adventurer adventurer in adventurers)
                {
                    writer.WriteLine(WriteAdventurer(adventurer));
                }
            }
        }


        #region private
        private string WriteAdventurer(Adventurer adventurer)
        {
            string orientation = "";
            switch(adventurer.Orientation)
            {
                case Orientation.Nord:
                    orientation = "N";
                    break;
                case Orientation.Ouest:
                    orientation = "O";
                    break;
                case Orientation.Est:
                    orientation = "E";
                    break;
                case Orientation.Sud:
                    orientation = "S";
                    break;
                default:
                    throw new FormatException();

            }
            return String.Format("A-{0}-{1}-{2}-{3}-{4}",adventurer.Name, adventurer.Position.X, adventurer.Position.Y, orientation, adventurer.Treasure);
        }

        private string WriteTreasure(Treasure trasure)
        {

            return String.Format("T-{0}-{1}-{2}", trasure.Position.X, trasure.Position.Y, trasure.Number);
        }

        private string WriteMountain(Point mountain)
        {
            return String.Format("M-{0}-{1}" , mountain.X , mountain.Y);
        }

        private string WriteMap(Map map)
        {
            return String.Format("C-{0}-{1}", map.Whidth, map.Height);
        }

        #endregion
    }
}
