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
    public static class WPF_Controller_GetSectionFilename_Should
    {
        [Test]
        public static void WPF_Controller_GetSectionFilename_ShouldReturnStringArrayCount()
        {
            Section[] sections = new[]
            {
                new Section() { SectionType = SectionTypes.Straight },
                new Section() { SectionType = SectionTypes.StartGrid },
                new Section() { SectionType = SectionTypes.Finish },
                new Section() { SectionType = SectionTypes.RightCorner },
                new Section() { SectionType = SectionTypes.LeftCorner },
            };

            Direction[] directions = new Direction[]
            {
                Direction.North,
                Direction.East,
                Direction.South,
                Direction.West,
            };

            List<string> strings = new List<string>();

            foreach (Section section in sections)
            {
                foreach (Direction direction in directions)
                {
                    strings.Add(WPFController.GetSectionFilename(section.SectionType, direction));
                }
            }

            Assert.AreEqual(20, strings.Count);
        }
    }
}
