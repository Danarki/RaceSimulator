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

        public void UpdateDirection(SectionTypes sectionType)
        {
            if (sectionType == SectionTypes.RightCorner)
            {
                NextDirection();
            }
            else if (sectionType == SectionTypes.LeftCorner)
            {
                PreviousDirection();
            }
        }

        public void NextDirection()
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

        public void PreviousDirection()
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

        public int GetSteps()
        {
            return Equipment.Performance * Equipment.Speed / 10;
        }
    }
}