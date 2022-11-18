using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model_Tests
{
    public class Model_IParticipant_updateDirectionShould
    {
        private IParticipant participant;

        [SetUp]
        public void SetUp()
        {
            participant = new Driver
            {
                Name = "Daan",
                TeamColor = TeamColors.Green,
                Equipment = new Car
                {
                    Quality = 1,
                    Performance = 1,
                    Speed = 10,
                    IsBroken = false
                },
                Direction = Direction.East
            };
        }

        [Test]
        public void UpdateDirection_EastLeft_ReturnNorth()
        {
            participant.UpdateDirection(SectionTypes.LeftCorner);

            Assert.AreEqual(participant.Direction, Direction.North);
        }

        [Test]
        public void UpdateDirection_EastRight_ReturnSouth()
        {
            participant.UpdateDirection(SectionTypes.RightCorner);

            Assert.AreEqual(participant.Direction, Direction.South);
        }

        [Test]
        public void UpdateDirection_NorthRight_ReturnEast()
        {
            participant.Direction = Direction.North;
            participant.UpdateDirection(SectionTypes.RightCorner);

            Assert.AreEqual(participant.Direction, Direction.East);
        }
        
        [Test]
        public void UpdateDirection_NorthLeft_ReturnWest()
        {
            participant.Direction = Direction.North;
            participant.UpdateDirection(SectionTypes.LeftCorner);

            Assert.AreEqual(participant.Direction, Direction.West);
        }

        [Test]
        public void UpdateDirection_WestRight_ReturnNorth()
        {
            participant.Direction = Direction.West;
            participant.UpdateDirection(SectionTypes.RightCorner);

            Assert.AreEqual(participant.Direction, Direction.North);
        }

        [Test]
        public void UpdateDirection_WestLeft_ReturnSouth()
        {
            participant.Direction = Direction.West;
            participant.UpdateDirection(SectionTypes.LeftCorner); 

            Assert.AreEqual(participant.Direction, Direction.South);
        }

        [Test]
        public void UpdateDirection_SouthRight_ReturnWest()
        {
            participant.Direction = Direction.South;
            participant.UpdateDirection(SectionTypes.RightCorner);

            Assert.AreEqual(participant.Direction, Direction.West);
        }

        [Test]
        public void UpdateDirection_SouthLeft_ReturnEast()
        {
            participant.Direction = Direction.South;
            participant.UpdateDirection(SectionTypes.LeftCorner);

            Assert.AreEqual(participant.Direction, Direction.East);
        }

        [Test]
        public void GetSteps_ReturnInt()
        {
            Assert.AreEqual(participant.GetSteps(), 1);
        }
    }
}
