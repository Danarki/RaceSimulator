using Controller;
using Model;
using RaceSim;
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
    public static class Visualization_AddParticipantsToGraphic_Should
    {
        private static Race race { get; set; }

        [SetUp]
        public static void SetUp()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
                SectionTypes.Straight
            };

            Track track = new Track("Super Track", sections);

            List<IParticipant> participants = new List<IParticipant>
            {

                new Driver
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
                },

                new Driver
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
                },
            };

            race = new Race(track, participants);
        }

        [Test]
        public static void Visualization_TwoParticipantsToStartEast_ReturnStringArray()
        {
            string[] wantedResult = { "------+", "      |", "____B_|", "      |", "_E____|", "      |", "------+" };

            Data.CurrentRace = race;

            Section section = race.Track.Sections.First();
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            string[] graphic = new string[7];

            Visualization.Direction = Direction.East;

            if (sectionData.Left != null || sectionData.Right != null) // there is at least 1 racer on this section
            {
                string[] returnedGraphic = Visualization.DetermineGraphic(section.SectionType);
                graphic = Visualization.AddParticipantsToGraphic(returnedGraphic, sectionData, section.SectionType, section);
            }

            Assert.AreEqual(wantedResult, graphic);
        }

        [Test]
        public static void Visualization_OneParticipantToStartEast_ReturnStringArray()
        {
            string[] wantedResult = { "------+", "      |", "____B_|", "      |", "______|", "      |", "------+" };

            Data.CurrentRace = race;

            Section section = race.Track.Sections.First();
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            string[] graphic = new string[7];

            Visualization.Direction = Direction.East;

            sectionData.Right = null;

            if (sectionData.Left != null || sectionData.Right != null) // there is at least 1 racer on this section
            {
                string[] returnedGraphic = Visualization.DetermineGraphic(section.SectionType);
                graphic = Visualization.AddParticipantsToGraphic(returnedGraphic, sectionData, section.SectionType, section);
            }

            Assert.AreEqual(wantedResult, graphic);
        }

        [Test]
        public static void Visualization_OneParticipantToStraightWest_ReturnStringArray()
        {
            string[] wantedResult = { "-------", "       ", "_______", "       ", "__B____", "       ", "-------" };

            Data.CurrentRace = race;

            Section section = new Section();
            section.SectionType = SectionTypes.Straight;

            SectionData sectionData = new SectionData();
            sectionData.Left = Data.CurrentRace.Participants.First();

            string[] graphic = new string[7];

            Visualization.Direction = Direction.West;
            sectionData.Left.Direction = Direction.West;
            sectionData.DistanceLeft = 4;
            sectionData.Right = null;

            if (sectionData.Left != null || sectionData.Right != null) // there is at least 1 racer on this section
            {
                string[] returnedGraphic = Visualization.DetermineGraphic(section.SectionType);
                graphic = Visualization.AddParticipantsToGraphic(returnedGraphic, sectionData, section.SectionType, section);
            }

            Assert.AreEqual(wantedResult, graphic);
        }

        [Test]
        public static void Visualization_TwoParticipantToStraightWest_ReturnStringArray()
        {
            string[] wantedResult = { "-------", "       ", "____E__", "       ", "__B____", "       ", "-------" };

            Data.CurrentRace = race;

            Section section = new Section();
            section.SectionType = SectionTypes.Straight;

            SectionData sectionData = new SectionData();
            sectionData.Left = Data.CurrentRace.Participants.First();
            sectionData.Right = Data.CurrentRace.Participants[1];

            string[] graphic = new string[7];

            Visualization.Direction = Direction.West;
            sectionData.Left.Direction = Direction.West;
            sectionData.DistanceLeft = 4;
            sectionData.Right.Direction = Direction.West;
            sectionData.DistanceRight = 2;

            if (sectionData.Left != null || sectionData.Right != null) // there is at least 1 racer on this section
            {
                string[] returnedGraphic = Visualization.DetermineGraphic(section.SectionType);
                graphic = Visualization.AddParticipantsToGraphic(returnedGraphic, sectionData, section.SectionType, section);
            }

            Assert.AreEqual(wantedResult, graphic);
        }

        //[Test]
        //public static void Visualization_OneParticipant_ReturnStringArray()
        //{
        //    string[] startGrid = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };
        //    string[] wantedResult = { "-------", "      I", "    B I", "      I", "      I", "      I", "-------" };

        //    IParticipant participant1 = new Driver
        //    {
        //        Name = "Bert",
        //        TeamColor = TeamColors.Green,
        //        Equipment = new Car
        //        {
        //            Quality = 10,
        //            Performance = 10,
        //            Speed = 10,
        //            IsBroken = false
        //        }
        //    };

        //    string[] result = Visualization.AddParticipantsToGraphic(startGrid, participant1, null);

        //    Assert.AreEqual(result, wantedResult);
        //}

        //[Test]
        //public static void Visualization_NoParticipants_ReturnStringArray()
        //{
        //    string[] startGrid = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };
        //    string[] wantedResult = { "-------", "      I", "      I", "      I", "      I", "      I", "-------" };

        //    string[] result = Visualization.AddParticipantsToGraphic(startGrid, null, null);

        //    Assert.AreEqual(result, wantedResult);
        //}
    }
}
