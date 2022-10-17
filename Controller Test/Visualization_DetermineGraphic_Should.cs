using Model;
using RaceSim;
using Assert = NUnit.Framework.Assert;

namespace Controller_Test
{
    [TestFixture]
    public static class Visualization_DetermineGraphic_Should
    {
        [Test]
        public static void Visualization_GraphicStart_ReturnStringArray()
        {
            Visualization.Direction = Direction.East;

            string[] EastStart =
            {
                "------+",
                "      |",
                "______|",
                "      |",
                "______|",
                "      |",
                "------+"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.StartGrid);

            Assert.AreEqual(EastStart, result);
        }

        [Test]
        public static void Visualization_GraphicFacingEastFinish_ReturnStringArray()
        {
            Visualization.Direction = Direction.East;

            string[] EastFinish =
            {
                "-------",
                "   #   ",
                "___#___",
                "   #   ",
                "___#___",
                "   #   ",
                "-------"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.Finish);

            Assert.AreEqual(EastFinish, result);
        }

        [Test]
        public static void Visualization_GraphicFacingEastStraight_ReturnStringArray()
        {
            Visualization.Direction = Direction.East;

            string[] EastStraight = {
                "-------",
                "       ",
                "_______",
                "       ",
                "_______",
                "       ",
                "-------"
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.Straight);

            Assert.AreEqual(EastStraight, result);
        }


        [Test]
        public static void Visualization_GraphicFacingEastLeft_ReturnStringArray()
        {
            Visualization.Direction = Direction.East;
            
            string[] EastLeft = {
                "/ | | I",
                "  | | I",
                "__| | I",
                "    | I",
                "____| I",
                "      I",
                "------/"
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.LeftCorner);

            Assert.AreEqual(EastLeft, result);
        }

        [Test]
        public static void Visualization_GraphicFacingEastRight_ReturnStringArray()
        {
            Visualization.Direction = Direction.East;
            
            string[] EastRight = {
                "------\\",
                "      I",
                "____  I",
                "    | I",
                "__. | I",
                "  | | I",
                "\\ | | I"

            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.RightCorner);

            Assert.AreEqual(EastRight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingNorthFinish_ReturnStringArray()
        {
            Visualization.Direction = Direction.North;

            string[] NorthFinish = {
                "I | | I",
                "I | | I",
                "I | | I",
                "I#####I",
                "I | | I",
                "I | | I",
                "I | | I",
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.Finish);

            Assert.AreEqual(NorthFinish, result);
        }

        [Test]
        public static void Visualization_GraphicFacingNorthStraight_ReturnStringArray()
        {
            Visualization.Direction = Direction.North;

            string[] NorthStraight = {
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.Straight);

            Assert.AreEqual(NorthStraight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingNorthLeft_ReturnStringArray()
        {
            Visualization.Direction = Direction.North;

            string[] NorthLeft = {
                "------\\",
                "      I",
                "____  I",
                "    | I",
                "__. | I",
                "  | | I",
                "\\ | | I"

            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.LeftCorner);

            Assert.AreEqual(NorthLeft, result);
        }

        [Test]
        public static void Visualization_GraphicFacingNorthRight_ReturnStringArray()
        {
            Visualization.Direction = Direction.North;

            string[] NorthRight = {
                "/------",
                "I      ",
                "I  ____",
                "I |    ",
                "I | .__",
                "I | |  ",
                "I | | /"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.RightCorner);

            Assert.AreEqual(NorthRight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingSouthFinish_ReturnStringArray()
        {
            Visualization.Direction = Direction.South;

            string[] SouthFinish = {
                "I | | I",
                "I | | I",
                "I | | I",
                "I#####I",
                "I | | I",
                "I | | I",
                "I | | I",
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.Finish);

            Assert.AreEqual(SouthFinish, result);
        }

        [Test]
        public static void Visualization_GraphicFacingSouthStraight_ReturnStringArray()
        {
            Visualization.Direction = Direction.South;

            string[] SouthStraight = {
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
                "I | | I",
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.Straight);

            Assert.AreEqual(SouthStraight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingSouthLeft_ReturnStringArray()
        {
            Visualization.Direction = Direction.South;

            string[] SouthLeft = {
                "I | | \\",
                "I | |  ",
                "I | |__",
                "I |    ",
                "I |____",
                "I      ",
                "\\------"
            };
            
            string[] result = Visualization.DetermineGraphic(SectionTypes.LeftCorner);

            Assert.AreEqual(SouthLeft, result);
        }

        [Test]
        public static void Visualization_GraphicFacingSouthRight_ReturnStringArray()
        {
            Visualization.Direction = Direction.South;

            string[] SouthRight = {
                "/ | | I",
                "  | | I",
                "__| | I",
                "    | I",
                "____| I",
                "      I",
                "------/"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.RightCorner);

            Assert.AreEqual( SouthRight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingWestFinish_ReturnStringArray()
        {
            Visualization.Direction = Direction.West;

            string[] WestFinish = {
                "-------",
                "   #   ",
                "___#___",
                "   #   ",
                "___#___",
                "   #   ",
                "-------"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.Finish);

            Assert.AreEqual(WestFinish, result);
        }

        [Test]
        public static void Visualization_GraphicFacingWestStraight_ReturnStringArray()
        {
            Visualization.Direction = Direction.West;

            string[] WestStraight = {
                "-------",
                "       ",
                "_______",
                "       ",
                "_______",
                "       ",
                "-------"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.Straight);

            Assert.AreEqual(WestStraight, result);
        }

        [Test]
        public static void Visualization_GraphicFacingWestLeft_ReturnStringArray()
        {
            Visualization.Direction = Direction.West;

            string[] WestLeft = {
                "/------",
                "I      ",
                "I  ____",
                "I |    ",
                "I | .__",
                "I | |  ",
                "I | | /"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.LeftCorner);

            Assert.AreEqual(WestLeft, result);
        }

        [Test]
        public static void Visualization_GraphicFacingWestRight_ReturnStringArray()
        {
            Visualization.Direction = Direction.West;

            string[] WestRight = {
                "I | | \\",
                "I | |  ",
                "I | |__",
                "I |    ",
                "I |____",
                "I      ",
                "\\------"
            };

            string[] result = Visualization.DetermineGraphic(SectionTypes.RightCorner);

            Assert.AreEqual(WestRight, result);
        }
    }
}
