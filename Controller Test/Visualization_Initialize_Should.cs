using Model;
using RaceSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace Controller_Test
{
    [TestFixture]
    public static class Visualization_Initialize_Should
    {
        [SetUp]
        public static void SetUp()
        {
        }

        [Test]
        public static void Visualization_TwoParticipants_ReturnStringArray()
        {
            Visualization.Initialize();
            Assert.AreEqual(Visualization.Direction, Direction.East);
            
        }
    }
}
