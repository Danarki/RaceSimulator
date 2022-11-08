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
    public static class WPF_Controller_GetParticipantFilename_Should
    {
        [Test]
        public static void WPF_Controller_GetParticipantFilename_ShouldReturnStringArrayCount()
        {
            List<IParticipant> participants = new List<IParticipant>()
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
                new Driver
                {
                    Name = "Jaap",
                    TeamColor = TeamColors.Yellow,
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
                    Name = "Johan",
                    TeamColor = TeamColors.Grey,
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
                    Name = "Gerda",
                    TeamColor = TeamColors.Blue,
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
                    Name = "Knaapje",
                    TeamColor = TeamColors.Blue,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 10,
                        Speed = 10,
                        IsBroken = true
                    }
                },
            };

            Direction[] directions = new Direction[]
            {
                Direction.North,
                Direction.East,
                Direction.South,
                Direction.West,
            };

            List<string> strings = new List<string>();

            foreach (IParticipant participant in participants)
            {
                foreach (Direction direction in directions)
                {
                    strings.Add(WPFController.GetParticipantFileName(participant, direction));
                }
            }

            Assert.AreEqual(24, strings.Count);
        }
    }
}
