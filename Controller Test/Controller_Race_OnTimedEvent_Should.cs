using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    public class Controller_Race_OnTimedEvent_Should
    {
        private Race race { get; set; }

        [SetUp]
        public void SetUp()
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
                    Name = "Daan",
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
                    Name = "Rudolf",
                    TeamColor = TeamColors.Red,
                    Equipment = new Car
                    {
                        Quality = 2,
                        Performance = 2,
                        Speed = 0,
                        IsBroken = false
                    }
                },

                new Driver
                {
                    Name = "Alberto",
                    TeamColor = TeamColors.Yellow,
                    Equipment = new Car
                    {
                        Quality = 7,
                        Performance = 8,
                        Speed = 8,
                        IsBroken = false
                    }
                }
            };

            race = new Race(track, participants);
        }

        [Test]
        public void Controller_Race_OnTimedEventShouldUpdateDrivers()
        {

            SectionData oldSectionData = new SectionData();
            oldSectionData.Left = new Driver
            {
                Name = "Daan",
                TeamColor = TeamColors.Green,
                Equipment = new Car
                {
                    Quality = 10,
                    Performance = 10,
                    Speed = 10,
                    IsBroken = false
                }
            };
            oldSectionData.DistanceLeft = 4;
            oldSectionData.Right = new Driver
            {
                Name = "Rudolf",
                TeamColor = TeamColors.Red,
                Equipment = new Car
                {
                    Quality = 2,
                    Performance = 2,
                    Speed = 0,
                    IsBroken = false
                }
            };
            oldSectionData.DistanceRight = 2;

            Data.CurrentRace = race;

            race.startTimer();
            Thread.Sleep(500);
            race.stopTimer();

            Section section = race.Track.Sections.First();
            SectionData sectionData = race.GetSectionData(section);

            Assert.AreNotEqual(sectionData, oldSectionData);
        }
    }
}
