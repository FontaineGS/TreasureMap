using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Model;
using TreasureMap.Parsing;

namespace TreasureMap
{
    public class AdventureContext
    {
        private List<Adventurer> adventurers = new List<Adventurer>();

        private Map map = new Map();


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
