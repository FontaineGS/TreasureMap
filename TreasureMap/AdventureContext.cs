using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Model;
using TreasureMap.Parsing;
//apparemment necessaire pour les tests
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("TreasureMapTest")]

namespace TreasureMap
{
    public class AdventureContext
    {
        internal List<Adventurer> adventurers = new List<Adventurer>();

        internal Map map = new Map();


        public void Load(string filename)
        {
            AdventureParser parser = new AdventureParser();
            parser.Parse(filename, map, adventurers);
        }

        public void Write(string filename)
        {
            AdventureWriter writer = new AdventureWriter();
            writer.Write(filename, map, adventurers);
        }
        
        public void Compute()
        {
            AdventurerResolver resolver = new AdventurerResolver(map, adventurers);
            resolver.Resolve();
        }


    }
}
