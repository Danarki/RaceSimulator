using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    public class Controller_Race_TimerShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
        }

        [Test]
        public void Controller_Race_StartTimerShouldReturnNull()
        {
            Data.CurrentRace.startTimer();
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
