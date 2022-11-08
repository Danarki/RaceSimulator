using WPF;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;
using System.Windows.Documents;
using Controller;

namespace Controller_Test
{
    [TestFixture]
    public static class WPF_Visualization_GetParticipantLocation_Should
    {
        [SetUp]
        public static void SetUp()
        {
            SectionTypes[] raceSections =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
            };

            Track track = new Track("Super Track", raceSections);

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

            Data.CurrentRace = new Race(track, participants);

            foreach (Section section in Data.CurrentRace.Track.Sections)
            {
                SectionData sectionData = Data.CurrentRace.GetSectionData(section);
                sectionData.Left = Data.CurrentRace.Participants[0];
                sectionData.Right = Data.CurrentRace.Participants[1];
                sectionData.Left.Direction = Direction.East;
                sectionData.Right.Direction = Direction.East;
            }
        }

        [Test]
        public static void WPF_Visualization_DetermineParticipantLocation_StraightShouldReturnIntArrayCount()
        {
            bool increaseRounds = false;
            bool giveRoundsTuple = false;
            List<int> list = new List<int>();

            Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };

            Section section = new Section() { SectionType = SectionTypes.Straight };


            foreach (Direction direction in directions)
            {
                for (int i = 0; i < 7; i++)
                {
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, true, ref increaseRounds, ref giveRoundsTuple));
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, false, ref increaseRounds, ref giveRoundsTuple));
                }
            }


            Assert.AreEqual(112, list.Count);
        }

        [Test]
        public static void WPF_Visualization_DetermineParticipantLocation_StartShouldReturnIntArrayCount()
        {
            bool increaseRounds = false;
            bool giveRoundsTuple = false;
            List<int> list = new List<int>();

            Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };

            Section section = new Section() { SectionType = SectionTypes.StartGrid };


            foreach (Direction direction in directions)
            {
                for (int i = 0; i < 7; i++)
                {
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, true, ref increaseRounds, ref giveRoundsTuple));
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, false, ref increaseRounds, ref giveRoundsTuple));
                }
            }


            Assert.AreEqual(112, list.Count);
        }

        [Test]
        public static void WPF_Visualization_DetermineParticipantLocation_FinishShouldReturnIntArrayCount()
        {
            bool increaseRounds = false;
            bool giveRoundsTuple = false;
            List<int> list = new List<int>();

            Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };

            Section section = new Section() { SectionType = SectionTypes.Finish };


            foreach (Direction direction in directions)
            {
                for (int i = 0; i < 7; i++)
                {
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, true, ref increaseRounds, ref giveRoundsTuple));
                    list.AddRange(Visualization.GetParticipantLocation(i, section, direction, false, ref increaseRounds, ref giveRoundsTuple));
                }
            }


            Assert.AreEqual(112, list.Count);
        }

        //[Test]
        //public static void WPF_Visualization_DetermineParticipantLocation_RightCornerShouldReturnIntArrayCount()
        //{
        //    bool increaseRounds = false;
        //    bool giveRoundsTuple = false;
        //    List<int> list = new List<int>();

        //    Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };

        //    Section section = new Section() { SectionType = SectionTypes.LeftCorner };


        //    foreach (Direction direction in directions)
        //    {
        //        for (int i = 0; i < 7; i++)
        //        {
        //            list.AddRange(Visualization.GetParticipantLocation(i, section, direction, true, ref increaseRounds, ref giveRoundsTuple));
        //        }
        //    }


        //    Assert.AreEqual(56, list.Count);
        //}
    }
}
