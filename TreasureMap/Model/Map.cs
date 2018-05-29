using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Model
{
    internal class Map
    {
        #region fields
        private int height;
        private int whidth;
        private List<Point> moutains;
        private List<Treasure> treasures;
        #endregion

        #region properties
        public int Height { get => height; set => height = value; }
        public int Whidth { get => whidth; set => whidth = value; }
        internal List<Point> Moutains { get => moutains; set => moutains = value; }
        internal List<Treasure> Treasures { get => treasures; set => treasures = value; }
        #endregion

        public Map()
        {
            moutains = new List<Point>();
            treasures = new List<Treasure>();
        }
    }
}
