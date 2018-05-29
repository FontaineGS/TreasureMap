using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Model
{
    internal class Adventurer
    {
        #region field
        private Point position;

        private Orientation orientation;

        private Queue<Move> moveQueue;

        private string name;

        private int treasure;
        #endregion

        #region property
        internal Point Position { get => position; set => position = value; }
        internal Orientation Orientation { get => orientation; set => orientation = value; }
        internal Queue<Move> MoveQueue { get => moveQueue; set => moveQueue = value; }
        public string Name { get => name; set => name = value; }
        public int Treasure { get => treasure; set => treasure = value; }
        #endregion

        public Adventurer()
        {
            MoveQueue = new Queue<Move>();
        }
    }

    internal enum Move
    {
        Front,
        Left,
        Right
    }

    internal enum Orientation
    {
        Nord = 0,
        Sud = 2,
        Est = 1,
        Ouest = 3
    }
}
