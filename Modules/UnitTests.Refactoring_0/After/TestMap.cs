using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring_0.After;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Refactoring_0.After
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void Map_Ctor()
        {
            Map map = new Map(new[] { new Tuple<int, int>(0, 0), new Tuple<int, int>(0, 1), new Tuple<int, int>(0, 1) });
            Assert.AreEqual(2, map.Fields.Count());
        }

        [TestMethod]
        public void Map_IsAnyLive()
        {
            Map map = new Map(new[] { new Tuple<int, int>(0, 0), new Tuple<int, int>(0, 1)});
            Assert.IsTrue(map.IsAnyLive);
            map.Fields.ElementAt(0).Live = false;
            Assert.IsTrue(map.IsAnyLive);
            map.Fields.ElementAt(1).Live = false;
            Assert.IsFalse(map.IsAnyLive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Map_CalculateLivingNeighbours_Exception()
        {
            Map map = new Map();
            map.CalculateLivingNeighbours(null);
        }

        [TestMethod]
        public void Map_CalculateLivingNeighbours()
        {
            Map map = new Map(new[] { new Tuple<int, int>(0, 0), new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0) });
            Assert.AreEqual(2, map.CalculateLivingNeighbours(new Field() { X = 0, Y = 0 }));
        }



        [TestMethod]
        public void Map_AddNeighbours()
        {
            Map map = new Map(new[] { new Tuple<int, int>(0, 0) });
            map.AddNeighbours();
            Assert.AreEqual(9, map.Fields.Count());

            map = new Map(new[] { new Tuple<int, int>(0, 0), new Tuple<int, int>(5, 5) });
            map.AddNeighbours();
            Assert.AreEqual(18, map.Fields.Count());

            map = new Map(new[] { new Tuple<int, int>(0, 0), new Tuple<int, int>(2, 0) });
            map.AddNeighbours();
            Assert.AreEqual(15, map.Fields.Count());
        }
    }
}
