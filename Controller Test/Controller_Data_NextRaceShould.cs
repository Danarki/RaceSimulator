using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    public class Controller_Data_NextRaceShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
        }

        [Test]
        public void Controller_Data_NextRaceUntilEmptyShouldReturnNull()
        {
            Data.NextRace();
            Race race = Data.CurrentRace;

            while (race != null)
            {
                Data.NextRace();
                if (Data.CurrentRace != null)
                {
                    race = Data.CurrentRace;
                }
                else
                {
                    race = null;
                }
            }

            Assert.AreEqual(null, Data.CurrentRace);
        }
    }
}
