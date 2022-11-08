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
    public static class WPF_Visualization_GetTrackSize_Should
    {
        [Test]
        public static void WPF_Visualization_DetermineParticipantLocation_StraightShouldReturnIntArrayCount()
        {
            SectionTypes[] raceSections =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
            };

            Track track = new Track("Super Track", raceSections);
            List<int> list = Visualization.GetTrackSize(track);
            int x = list[0];
            int y = list[1];


            Assert.AreEqual(1600, x);
            Assert.AreEqual(960, y);
        }
    }
}
