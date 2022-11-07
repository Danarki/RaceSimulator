using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    public class Controller_Race_GetSectionDataShould
    {
        private Race race;

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
        public void Controller_Race_NewRaceGetSectionDataShouldReturnSectionData()
        {
            Track t = race.Track;

            SectionData sectionData = race.GetSectionData(t.Sections.First());

            Assert.That(race.Participants[0], Is.EqualTo(sectionData.Left));
            Assert.That(4, Is.EqualTo(sectionData.DistanceLeft));
            Assert.That(race.Participants[1], Is.EqualTo(sectionData.Right));
            Assert.That(1, Is.EqualTo(sectionData.DistanceRight));
        }

        // Weird test errors: SectionData.Left is never null, while it is null, thats why I made this work-around
        [Test]
        public void Controller_Race_NewRaceGetSectionDataShouldReturnNull()
        {
            Track t = race.Track;
            LinkedListNode<Section> firstSection = t.Sections.First;

            SectionData sectionData = race.GetSectionData(firstSection.Next.Value);

            bool leftNullFlag = true;
            bool rightNullFlag = true;

            if (sectionData.Left != null)
            {
                leftNullFlag = false;
            }

            if (sectionData.Right != null)
            {
                rightNullFlag = false;
            }

            Assert.True(leftNullFlag);
            Assert.That(0, Is.EqualTo(sectionData.DistanceLeft));
            Assert.True(rightNullFlag);
            Assert.That(0, Is.EqualTo(sectionData.DistanceRight));
        }

        [Test]
        public void Controller_Race_NewRaceGetFaultySectionDataShouldReturnNewSectionData()
        {
            Section s = new Section();
            SectionData sectionData = race.GetSectionData(s);

            SectionData e = new SectionData();
            e.Left = null;
            e.DistanceLeft = 0;
            e.Right = null;
            e.DistanceRight = 0;

            Assert.That(sectionData.Left, Is.EqualTo(e.Left));
            Assert.That(sectionData.DistanceLeft, Is.EqualTo(e.DistanceLeft));
            Assert.That(sectionData.Right, Is.EqualTo(e.Right));
            Assert.That(sectionData.DistanceRight, Is.EqualTo(e.DistanceRight));
        }
    }
}
