using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum TeamColors { Red, Green, Yellow, Grey, Blue };

    public enum Direction
    {
        North,
        East,
        South,
        West
    };

    public interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
        public Direction Direction { get; set; }

        public void updateDirection(SectionTypes sectionType)
        {
            if (sectionType == SectionTypes.RightCorner)
            {
                nextDirection();
            } else if (sectionType == SectionTypes.LeftCorner)
            {
                previousDirection();   
            }
        }

        public void nextDirection()
        {
            if (Direction == Direction.West)
            {
                Direction = Direction.North;
            }
            else
            {
                Direction++;
            }
        }

        public void previousDirection()
        {
            if (Direction == Direction.North)
            {
                Direction = Direction.West;
            }
            else
            {
                Direction--;
            }
        }

        public int getSteps()
        {
           return Equipment.Performance * Equipment.Speed / 10;
        }
    }
}
