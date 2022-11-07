using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model_Tests
{
    [TestFixture]
    public class Model_Competititon_NextTrackShould
    {
        private Competition _competition;
        
        [SetUp]        
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };

            Track track = new Track("Super Track", sections);

            _competition.Tracks = new Queue<Track> { };

            _competition.Tracks.Enqueue(track);

            var result = _competition.NextTrack();

            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };

            Track track = new Track("Super Track", sections);

            _competition.Tracks = new Queue<Track> { };

            _competition.Tracks.Enqueue(track);

            var result = _competition.NextTrack();

            result = _competition.NextTrack();

            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            };

            Track track = new Track("Super Track", sections);

            _competition.Tracks = new Queue<Track> { };

            _competition.Tracks.Enqueue(track);

            SectionTypes[] sections2 =
            {
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            };

            Track track2 = new Track("Super Track 2", sections);

            _competition.Tracks.Enqueue(track2);
            
            var result = _competition.NextTrack();


            var result2 = _competition.NextTrack();
           
            Assert.AreEqual(result, track);
            Assert.AreEqual(result2, track2);
        }
    }
}
