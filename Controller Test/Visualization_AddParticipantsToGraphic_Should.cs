using Model;
using RaceSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    [TestFixture]
    public static class Visualization_AddParticipantsToGraphic_Should
    {
        [Test]
        public static void Visualization_TwoParticipants_ReturnStringArray()
        {
            string[] startGrid = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };
            string[] wantedResult = { "-------", "      I", "    B I", "      I", " E    I", "      I", "-------" };

            IParticipant participant1 = new Driver
            {
                Name = "Bert",
                TeamColor = TeamColors.Green,
                Equipment = new Car
                {
                    Quality = 10,
                    Performance = 10,
                    Speed = 10,
                    IsBroken = false
                }
            };

            IParticipant participant2 = new Driver
            {
                Name = "Ernie",
                TeamColor = TeamColors.Red,
                Equipment = new Car
                {
                    Quality = 2,
                    Performance = 2,
                    Speed = 0,
                    IsBroken = false
                }
            };

            //string[] result = Visualization.AddParticipantsToGraphic(startGrid, participant1, participant2);

            //Assert.AreEqual(result, wantedResult);
        }

        [Test]
        public static void Visualization_OneParticipant_ReturnStringArray()
        {
            string[] startGrid = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };
            string[] wantedResult = { "-------", "      I", "    B I", "      I", "      I", "      I", "-------" };

            IParticipant participant1 = new Driver
            {
                Name = "Bert",
                TeamColor = TeamColors.Green,
                Equipment = new Car
                {
                    Quality = 10,
                    Performance = 10,
                    Speed = 10,
                    IsBroken = false
                }
            };

            //string[] result = Visualization.AddParticipantsToGraphic(startGrid, participant1, null);

            //Assert.AreEqual(result, wantedResult);
        }

        [Test]
        public static void Visualization_NoParticipants_ReturnStringArray()
        {
            string[] startGrid = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };
            string[] wantedResult = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };

            //string[] result = Visualization.AddParticipantsToGraphic(startGrid, null, null);

            //Assert.AreEqual(result, wantedResult);
        }
    }
}
