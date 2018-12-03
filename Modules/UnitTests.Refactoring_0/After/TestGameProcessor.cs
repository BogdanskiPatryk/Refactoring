using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring_0.After;

namespace UnitTests.Refactoring_0.After
{
    [TestClass]
    public class TestGameProcessor
    {
        [TestMethod]
        public void GameProcessor_Step_0()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            Assert.AreEqual(7, map.Fields.Count(x => x.Live));
        }
        
        [TestMethod]
        public void GameProcessor_Step_1()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            processor.Proceed(1);
            Assert.AreEqual(6, map.Fields.Count(x => x.Live));
        }

        [TestMethod]
        public void GameProcessor_Step_2()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            processor.Proceed(2);
            Assert.AreEqual(4, map.Fields.Count(x => x.Live));
        }

        [TestMethod]
        public void GameProcessor_Step_3()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            processor.Proceed(3);
            Assert.AreEqual(7, map.Fields.Count(x => x.Live));
        }

        [TestMethod]
        public void GameProcessor_Step_4()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            processor.Proceed(4);
            Assert.AreEqual(6, map.Fields.Count(x => x.Live));
        }

        [TestMethod]
        public void GameProcessor_Step_5()
        {
            Map map = GetMap();
            GameProcessor processor = new GameProcessor(map, new GameRulesDefault());
            processor.Proceed(5);
            Assert.AreEqual(6, map.Fields.Count(x => x.Live));
        }

        private Map GetMap()
        {
            return new Map(new Tuple<int, int>[] { new Tuple<int, int>(1, 1), new Tuple<int, int>(2, 2), new Tuple<int, int>(3, 3), new Tuple<int, int>(3, 4), new Tuple<int, int>(4, 4), new Tuple<int, int>(4, 3), new Tuple<int, int>(2, 3) }); ;
        }
    }
}
