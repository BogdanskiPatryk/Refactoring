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
    public class TestGameRulesDefault
    {
        [TestMethod]
        public void GameRulesDefault_NeedContinue()
        {
            GameRulesDefault rules = new GameRulesDefault();
            Assert.IsTrue(rules.NeedContinue(new Map(new[] { new Tuple<int, int>(0, 0) })));
            Assert.IsFalse(rules.NeedContinue(new Map()));
        }

        [TestMethod]
        public void GameRulesDefault_CalculateLiveStatus_Live()
        {
            GameRulesDefault rules = new GameRulesDefault();
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 0));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 1));
            Assert.IsTrue(rules.CalculateLiveStatus(new Field() { Live = true }, 2));
            Assert.IsTrue(rules.CalculateLiveStatus(new Field() { Live = true }, 3));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 4));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 5));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 6));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 7));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = true }, 8));
        }

        [TestMethod]
        public void GameRulesDefault_CalculateLiveStatus_Death()
        {
            GameRulesDefault rules = new GameRulesDefault();
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 0));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 1));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 2));
            Assert.IsTrue(rules.CalculateLiveStatus(new Field() { Live = true }, 3));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 4));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 5));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 6));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 7));
            Assert.IsFalse(rules.CalculateLiveStatus(new Field() { Live = false }, 8));
        }
    }
}
