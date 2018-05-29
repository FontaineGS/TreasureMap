using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreasureMap;

namespace TreasureMapTest
{
    [TestClass]
    public class ParseTests
    {
        public const string FILENAME = @"C:\REPOSITORY\TreasureMap\test1.txt";


        [TestMethod]
        public void TestMapParsing()
        {
            AdventureContext context = new AdventureContext();
            context.Load(FILENAME);

            int height = context.map.Height;
            int width = context.map.Whidth;

            Assert.AreEqual(width, 3);
            Assert.AreEqual(height, 4);

        }

        [TestMethod]
        public void TestTreasureNumberParsing()
        {
            AdventureContext context = new AdventureContext();
            context.Load(FILENAME);
            
            Assert.AreEqual(context.map.Treasures.Count, 2);
            
        }

        [TestMethod]
        public void TestFirstTreasureParsing()
        {
            AdventureContext context = new AdventureContext();
            context.Load(FILENAME);
            
            Assert.AreEqual(context.map.Treasures[0].Number, 2);
            Assert.AreEqual(context.map.Treasures[0].Position.X, 0);
            Assert.AreEqual(context.map.Treasures[0].Position.Y, 3);
        }

        [TestMethod]
        public void TestMountainNumberParsing()
        {

            AdventureContext context = new AdventureContext();
            context.Load(FILENAME);
            Assert.AreEqual(context.map.Moutains.Count, 2);
        }

        [TestMethod]
        public void TestAdventurerParsing()
        {

            AdventureContext context = new AdventureContext();
            context.Load(FILENAME);
            Assert.AreEqual(context.adventurers.Count, 1);
            Assert.AreEqual(context.adventurers[0].Name, "Lara");
            Assert.AreEqual(context.adventurers[0].Orientation, TreasureMap.Model.Orientation.Sud);

        }


    }
}
