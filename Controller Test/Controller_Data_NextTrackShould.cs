using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller_Test
{
    public class Controller_Data_NextTrackShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
        }

        [Test]
        public void Controller_Data_NextTrackShouldReturnTrack()
        {
            Track t = null;
            t = Data.Competition.Tracks.Peek();
            Data.NextRace();

            Assert.AreEqual(Data.CurrentRace.Track, t);
        }

        [Test]
        public void Controller_Data_NextTrackUntilEmptyShouldReturnNull()
        {
            Data.NextRace();
            Track t = Data.CurrentRace.Track;

            while (t != null)
            {
                Data.NextRace();
                if (Data.CurrentRace != null)
                {
                    t = Data.CurrentRace.Track;
                }
                else
                {
                    t = null;
                }
            }

            Assert.AreEqual(null, t);
        }
    }
}
