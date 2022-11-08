using WPF;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;

namespace Controller_Test
{
    [TestFixture]
    public static class WPF_Visualization_DetermineCornerCoordinates_Should
    {
        [Test]
        public static void WPF_Visualization_DetermineLeftCornerCoordinates_ShouldReturnIntArrayCount()
        {
            Direction[] directions = new Direction[]
                { Direction.North, Direction.East, Direction.South, Direction.West };
            List<int[]> cornerResults = new List<int[]>();

            foreach (Direction direction in directions)
            {
                for (int i = 0; i < 7; i++)
                {
                    cornerResults.Add(Visualization.DetermineLeftCornerCoordinates(direction, i, true));
                    cornerResults.Add(Visualization.DetermineLeftCornerCoordinates(direction, i, false));
                }
            }

            Assert.AreEqual(56, cornerResults.Count);
        }

        [Test]
        public static void WPF_Visualization_DetermineRightCornerCoordinates_ShouldReturnIntArrayCount()
        {
            Direction[] directions = new Direction[]
                { Direction.North, Direction.East, Direction.South, Direction.West };
            List<int[]> cornerResults = new List<int[]>();

            foreach (Direction direction in directions)
            {
                for (int i = 0; i < 7; i++)
                {
                    cornerResults.Add(Visualization.DetermineRightCornerCoordinates(direction, i, true));
                    cornerResults.Add(Visualization.DetermineRightCornerCoordinates(direction, i, false));
                }
            }

            Assert.AreEqual(56, cornerResults.Count);
        }
    }
}
