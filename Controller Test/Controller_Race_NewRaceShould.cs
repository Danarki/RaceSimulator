using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Controller_Test
{
    public class Controller_Race_NewRaceShould
    {
        private Race race { get; set; }
        [SetUp]
        public void SetUp()
        {
            race = new Race(
                new Track(
                    "Track",
                    new SectionTypes[]
                    {
                        SectionTypes.StartGrid,
                        SectionTypes.RightCorner,
                        SectionTypes.RightCorner,
                        SectionTypes.Straight,
                        SectionTypes.Straight,
                        SectionTypes.RightCorner,
                        SectionTypes.RightCorner,
                        SectionTypes.Finish,
                    }), new List<IParticipant>()
                {
                    new Driver
                    {
                        Name = "Daan",
                        TeamColor = TeamColors.Green,
                        Equipment = new Car
                        {
                            Quality = 10,
                            Performance = 10,
                            Speed = 1,
                            IsBroken = false
                        },
                        Direction = Direction.East
                    },

                    new Driver
                    {
                        Name = "Rudolf",
                        TeamColor = TeamColors.Red,
                        Equipment = new Car
                        {
                            Quality = 10,
                            Performance = 10,
                            Speed = 1,
                            IsBroken = false
                        },
                        Direction = Direction.East
                    },
                });
        }

        [Test]
        public void Controller_Race_PointsRemainingShouldReturnIntArray()
        {
            Assert.AreEqual(race._pointsRemaining, new int[3] { 7, 5, 3 });
        }

        [Test]
        public void Controller_Race_MaxRoundsShouldReturnInt()
        {
            Assert.AreEqual(race.maxRounds, 3);
        }

        [Test]
        public void Controller_Race_MaxRoundsShouldReturnDictionary()
        {
            Dictionary<IParticipant, Tuple<int, bool>> newRounds = new Dictionary<IParticipant, Tuple<int, bool>>();

            foreach (IParticipant participant in race.Participants)
            {
                newRounds.Add(participant, new Tuple<int, bool>(0, false));
            }

            Assert.AreEqual(newRounds, race._rounds);
        }

        [Test]
        public void Controller_Race_ParticipantsShouldReturnParticipants()
        {
            List<IParticipant> participants = new List<IParticipant>()
            {
                new Driver
                {
                    Name = "Daan",
                    TeamColor = TeamColors.Green,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 10,
                        Speed = 1,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },

                new Driver
                {
                    Name = "Rudolf",
                    TeamColor = TeamColors.Red,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 10,
                        Speed = 1,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },
            };

            int index = 0;
            foreach (IParticipant participant in participants)
            {
                Driver d = (Driver)race.Participants[index];

                Assert.That(participant.Name, Is.EqualTo(d.Name));
                Assert.That(participant.Direction, Is.EqualTo(d.Direction));
                Assert.That(participant.Points, Is.EqualTo(d.Points));
                Assert.That(participant.TeamColor, Is.EqualTo(d.TeamColor));
                index++;
            }
        }
    }
}
