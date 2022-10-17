using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = CreateLinkedList(sections);
        }

        public LinkedList<Section> CreateLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> SectionList = new LinkedList<Section> { };


            foreach (var section in sections)
            {
                SectionList.AddLast(new Section { SectionType = section });
            }

            return SectionList;
        }
    }
}
