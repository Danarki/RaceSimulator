using Controller;
using Model;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Controller_Test
{
    public class Controller_Race_CreateRaceShould
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
        public void Race_CreateRace_ReturnRace()
        {
            Assert.IsInstanceOfType(race, typeof(Race));
            Assert.AreEqual(race.Track.Name, "Super Track");
            Assert.AreEqual(race.Participants.First().Name, "Daan");
        }
    }
}
