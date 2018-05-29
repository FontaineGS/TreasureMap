using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Model;

namespace TreasureMap.Parsing
{
    interface IParser
    {
        void Parse(string filename, Map map, List<Adventurer> adventurers);
    }
}
