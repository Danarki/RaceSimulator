using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Model_Tests
{
    public class Model_Track_CreateLinkedListShould
    {
        [Test]
        public void Track_CreateLinkedList_ReturnLinkedListl()
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

            var result = track.CreateLinkedList(sections);

            Assert.IsInstanceOfType(result, typeof(LinkedList<Section>));
        }
    }
}
