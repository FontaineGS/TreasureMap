using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Model;

namespace TreasureMap.Parsing
{
    internal interface IWriter
    {
        void Write(string filename, Map map, List<Adventurer> adventurers);
    }
}
