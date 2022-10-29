using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Controller;
using Model;

namespace WPF
{
    public static class Visualization
    {
        private static int blockWidthAndHeigth = 46;

        public static BitmapSource EndRace()
        {
            return WPFController.CreateBitmapSourceFromGdiBitmap(null);
        }

        public static List<int> GetParticipantLocation(int dis, Section section, Direction direction, bool isLeft, ref bool increaseRounds, ref bool giveRoundsTuple)
        {
            List<int> list = new List<int>();

            SectionData sectionData = Data.CurrentRace.GetSectionData(section);

            if (section.SectionType == SectionTypes.Straight || section.SectionType == SectionTypes.StartGrid || section.SectionType == SectionTypes.Finish)
            {
                if (section.SectionType == SectionTypes.Finish && dis > 4)
                {
                    increaseRounds = true;
                }

                if (section.SectionType != SectionTypes.Finish)
                {
                    giveRoundsTuple = true;
                }

                if (isLeft)
                {
                    if (direction == Direction.East)
                    {
                        list.Add(dis * blockWidthAndHeigth);
                        list.Add(blockWidthAndHeigth * 2);
                    }

                    if (direction == Direction.West)
                    {
                        list.Add(dis * blockWidthAndHeigth * -1 + 319);
                        list.Add(blockWidthAndHeigth * 4);
                    }

                    if (direction == Direction.South)
                    {
                        list.Add(blockWidthAndHeigth * 4);
                        list.Add(dis * blockWidthAndHeigth);
                    }

                    if (direction == Direction.North)
                    {
                        list.Add(blockWidthAndHeigth * 2);
                        list.Add(dis * blockWidthAndHeigth * -1 + 319);
                    }
                }
                else
                {
                    if (direction == Direction.East)
                    {
                        list.Add(dis * blockWidthAndHeigth);
                        list.Add(blockWidthAndHeigth * 4);
                    }

                    if (direction == Direction.West)
                    {
                        list.Add(dis * blockWidthAndHeigth * -1 + 319);
                        list.Add(blockWidthAndHeigth * 2);
                    }

                    if (direction == Direction.South)
                    {
                        list.Add(blockWidthAndHeigth * 2);
                        list.Add(dis * blockWidthAndHeigth);
                    }

                    if (direction == Direction.North)
                    {
                        list.Add(blockWidthAndHeigth * 4);
                        list.Add(dis * blockWidthAndHeigth * -1 + 319);
                    }
                }
            }

            if (section.SectionType == SectionTypes.LeftCorner)
            {
                int[] a = null;
                if (isLeft)
                {
                    a = DetermineLeftCornerCoordinates(sectionData.Left, dis, isLeft);
                }
                else
                {
                    a = DetermineLeftCornerCoordinates(sectionData.Right, dis, isLeft);
                }

                list.Add(a[0] * blockWidthAndHeigth);
                list.Add(a[1] * blockWidthAndHeigth);
            }

            if (section.SectionType == SectionTypes.RightCorner)
            {
                int[] a = null;
                if (isLeft)
                {
                    a = DetermineRightCornerCoordinates(sectionData.Left, dis, isLeft);
                }
                else
                {
                    a = DetermineRightCornerCoordinates(sectionData.Right, dis, isLeft);
                }

                list.Add(a[0] * blockWidthAndHeigth);
                list.Add(a[1] * blockWidthAndHeigth);
            }

            return list;
        }

        public static int[] DetermineLeftCornerCoordinates(IParticipant participant, int distance, bool isLeft)
        {
            int xPosition = distance;

            if (xPosition > 6)
            {
                xPosition = 6;
            }
            else if (xPosition < 0)
            {
                xPosition = 0;
            }

            int yPosition = 2;

            if (isLeft)
            {
                if (participant.Direction == Model.Direction.North)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 2;
                            yPosition = 6;
                            break;
                        case 1:
                            xPosition = 2;
                            yPosition = 5;
                            break;
                        case 2:
                            yPosition = 5;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 1;
                            yPosition = 4;
                            break;
                        case 5:
                            xPosition = 1;
                            yPosition = 4;
                            break;
                        case 6:
                            xPosition = 0;
                            yPosition = 4;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.South)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 4;
                            yPosition = 0;
                            break;
                        case 1:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 2:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 5;
                            yPosition = 2;
                            break;
                        case 5:
                            xPosition = 5;
                            yPosition = 2;
                            break;
                        case 6:
                            xPosition = 6;
                            yPosition = 2;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.West)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 6;
                            yPosition = 4;
                            break;
                        case 1:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 2:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 5:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 6:
                            xPosition = 4;
                            yPosition = 6;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.East)
                {
                    switch (xPosition)
                    {
                        case 2:
                            xPosition = 1;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 5:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 6:
                            xPosition = 2;
                            yPosition = 0;
                            break;
                    }
                }

                return new int[] { xPosition, yPosition };
            }
            else
            {
                if (participant.Direction == Model.Direction.East)
                {
                    yPosition = 4;
                    switch (xPosition)
                    {
                        case 2:
                            xPosition = 3;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 4;
                            break;
                        case 4:
                            yPosition = 3;
                            break;
                        case 5:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 6:
                            xPosition = 4;
                            yPosition = 0;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.North)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 4;
                            yPosition = 6;
                            break;
                        case 1:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 2:
                            xPosition = 4;
                            yPosition = 3;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 3;
                            yPosition = 2;
                            break;
                        case 5:
                            xPosition = 1;
                            yPosition = 2;
                            break;
                        case 6:
                            xPosition = 0;
                            yPosition = 2;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.West)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 6;
                            yPosition = 2;
                            break;
                        case 1:
                            xPosition = 5;
                            yPosition = 2;
                            break;
                        case 2:
                            xPosition = 3;
                            yPosition = 2;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 2;
                            yPosition = 3;
                            break;
                        case 5:
                            xPosition = 2;
                            yPosition = 5;
                            break;
                        case 6:
                            xPosition = 2;
                            yPosition = 6;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.South)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 2;
                            yPosition = 0;
                            break;
                        case 1:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 2:
                            yPosition = 3;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 3;
                            yPosition = 4;
                            break;
                        case 5:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 6:
                            xPosition = 6;
                            yPosition = 4;
                            break;
                    }
                }

                return new int[] { xPosition, yPosition };
            }

            return new int[] { 0, 0 };

        }

        public static int[] DetermineRightCornerCoordinates(IParticipant participant, int distance, bool isLeft)
        {
            int xPosition = distance;

            if (xPosition > 6)
            {
                xPosition = 6;
            }
            else if (xPosition < 0)
            {
                xPosition = 0;
            }

            int yPosition = 2;

            if (isLeft)
            {
                if (participant.Direction == Model.Direction.East)
                {
                    switch (xPosition)
                    {
                        case 2:
                            xPosition = 3;
                            break;
                        case 3:
                            xPosition = 4;
                            break;
                        case 4:
                            xPosition = 4;
                            yPosition = 3;
                            break;
                        case 5:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 6:
                            xPosition = 4;
                            yPosition = 6;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.West)
                {
                    xPosition = (xPosition - 6) * -1;
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 2;
                            yPosition = 0;
                            break;
                        case 1:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 2:
                            xPosition = 2;
                            yPosition = 3;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 3;
                            yPosition = 4;
                            break;
                        case 5:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 6:
                            yPosition = 4;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.South)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 4;
                            yPosition = 0;
                            break;
                        case 1:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 2:
                            xPosition = 4;
                            yPosition = 3;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 3;
                            yPosition = 4;
                            break;
                        case 5:
                            xPosition = 1;
                            yPosition = 4;
                            break;
                        case 6:
                            xPosition = 0;
                            yPosition = 4;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.North)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 2;
                            yPosition = 6;
                            break;
                        case 1:
                            xPosition = 2;
                            yPosition = 5;
                            break;
                        case 2:
                            yPosition = 3;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 3;
                            yPosition = 2;
                            break;
                        case 5:
                            yPosition = 2;
                            break;
                        case 6:
                            yPosition = 2;
                            break;
                    }
                }

                return new int[] { xPosition, yPosition };
            }
            else
            {
                if (participant.Direction == Model.Direction.West)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 6;
                            yPosition = 2;
                            break;
                        case 1:
                            xPosition = 5;
                            yPosition = 2;
                            break;
                        case 2:
                            xPosition = 5;
                            yPosition = 2;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 5:
                            xPosition = 4;
                            yPosition = 1;
                            break;
                        case 6:
                            xPosition = 4;
                            yPosition = 0;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.South)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 2;
                            yPosition = 0;
                            break;
                        case 1:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 2:
                            xPosition = 2;
                            yPosition = 1;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 2;
                            break;
                        case 4:
                            xPosition = 1;
                            yPosition = 2;
                            break;
                        case 5:
                            xPosition = 1;
                            yPosition = 2;
                            break;
                        case 6:
                            xPosition = 0;
                            yPosition = 2;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.East)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 0;
                            yPosition = 4;
                            break;
                        case 1:
                            xPosition = 1;
                            yPosition = 4;
                            break;
                        case 2:
                            xPosition = 1;
                            yPosition = 4;
                            break;
                        case 3:
                            xPosition = 2;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 2;
                            yPosition = 5;
                            break;
                        case 5:
                            xPosition = 2;
                            yPosition = 5;
                            break;
                        case 6:
                            xPosition = 2;
                            yPosition = 6;
                            break;
                    }
                }
                else if (participant.Direction == Model.Direction.North)
                {
                    switch (xPosition)
                    {
                        case 0:
                            xPosition = 4;
                            yPosition = 6;
                            break;
                        case 1:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 2:
                            xPosition = 4;
                            yPosition = 5;
                            break;
                        case 3:
                            xPosition = 4;
                            yPosition = 4;
                            break;
                        case 4:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 5:
                            xPosition = 5;
                            yPosition = 4;
                            break;
                        case 6:
                            xPosition = 6;
                            yPosition = 4;
                            break;
                    }
                }

                return new int[] { xPosition, yPosition };
            }
        }

        public static void IncreaseRound(IParticipant participant, ref bool RemoveParticipant)
        {
            Tuple<int, bool> participantRounds = Data.CurrentRace._rounds[participant];

            if (participantRounds.Item2)
            {
                Data.CurrentRace._rounds[participant] = new Tuple<int, bool>(participantRounds.Item1 + 1, false);
            }

            if (Data.CurrentRace._rounds[participant].Item1 == Data.CurrentRace.maxRounds)
            {
                RemoveParticipant = true;
            }
        }

        public static void AddParticipantsToBitmap(int x, int y, Graphics graphic, Direction direction, Section section)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);

            IParticipant leftParticipant = sectionData.Left;
            IParticipant rightParticipant = sectionData.Right;

            if (leftParticipant != null)
            {
                int participantDistance = sectionData.DistanceLeft;
                Direction participantDirection = leftParticipant.Direction;

                if (participantDistance >= 4)
                {
                    participantDirection = UpdateDirection(participantDirection, section.SectionType);
                }

                string participantFileName = WPFController.GetParticipantFileName(leftParticipant, participantDirection);

                Bitmap sectionBitmap = WPFController.GetImage(participantFileName, true, leftParticipant.Direction);

                if (sectionBitmap == null)
                {
                    return;
                }

                bool increaseRounds = false;
                bool giveRoundsTuple = false;
                List<int> list = GetParticipantLocation(participantDistance, section, direction, true, ref increaseRounds, ref giveRoundsTuple);

                if (giveRoundsTuple)
                {
                    Data.CurrentRace._rounds[leftParticipant] = new Tuple<int, bool>(Data.CurrentRace._rounds[leftParticipant].Item1, true);
                }

                if (increaseRounds)
                {
                    bool removeDriver = false;
                    IncreaseRound(leftParticipant, ref removeDriver);

                    if (removeDriver)
                    {
                        Data.CurrentRace.removeDriverFromPositions(leftParticipant, section, true);
                    }
                }

                int xParticipant = list[0] + x;
                int yParticipant = list[1] + y;

                graphic.DrawImage(sectionBitmap, xParticipant, yParticipant, 46, 46);
            }

            if (rightParticipant != null)
            {
                int participantDistance = sectionData.DistanceRight;
                Direction participantDirection = rightParticipant.Direction;

                if (participantDistance >= 4)
                {
                    participantDirection = UpdateDirection(participantDirection, section.SectionType);
                }

                string participantFileName = WPFController.GetParticipantFileName(rightParticipant, participantDirection);

                Bitmap sectionBitmap = WPFController.GetImage(participantFileName, true, rightParticipant.Direction);

                if (sectionBitmap == null)
                {
                    return;
                }

                bool increaseRounds = false;
                bool giveRoundsTuple = false;
                List<int> list = GetParticipantLocation(participantDistance, section, direction, false, ref increaseRounds, ref giveRoundsTuple);

                if (giveRoundsTuple)
                {
                    Data.CurrentRace._rounds[rightParticipant] = new Tuple<int, bool>(Data.CurrentRace._rounds[rightParticipant].Item1, true);
                }

                if (increaseRounds)
                {
                    bool removeDriver = false;
                    IncreaseRound(rightParticipant, ref removeDriver);

                    if (removeDriver)
                    {
                        Data.CurrentRace.removeDriverFromPositions(rightParticipant, section, false);
                    }
                }
                int xParticipant = list[0] + x;
                int yParticipant = list[1] + y;

                graphic.DrawImage(sectionBitmap, xParticipant, yParticipant, 46, 46);
            }
        }

        public static BitmapSource DrawTrack(Track track)
        {
            if (Data.CurrentRace == null)
            {
                return null;
            }

            List<int> dimensionsList = GetTrackSize(track);
            int width = dimensionsList[0] + 1;
            int heigth = dimensionsList[1] + 1;

            int DrawLocationX = dimensionsList[2];
            int DrawLocationY = dimensionsList[3];

            Bitmap bmp = WPFController.GetEmptyBitmap(width, heigth);
            Graphics graphics = Graphics.FromImage(bmp);

            Direction direction = Direction.East;

            foreach (Section section in track.Sections)
            {
                DrawSectionOnBitmap(DrawLocationX, DrawLocationY, direction, graphics, section);

                SectionData sectionData = Data.CurrentRace.GetSectionData(section);
                SectionTypes sectionType = section.SectionType;

                if (sectionData.Left != null || sectionData.Right != null)
                {
                    AddParticipantsToBitmap(DrawLocationX, DrawLocationY, graphics, direction, section);
                }

                direction = UpdateDirection(direction, section.SectionType);

                if (direction == Direction.East)
                {
                    DrawLocationX += 320;
                }
                else if (direction == Direction.West)
                {
                    DrawLocationX -= 320;
                }
                else if (direction == Direction.South)
                {
                    DrawLocationY += 320;
                }
                else if (direction == Direction.North)
                {
                    DrawLocationY -= 320;
                }
            }

            return WPFController.CreateBitmapSourceFromGdiBitmap(bmp);
        }

        public static void DrawSectionOnBitmap(int x, int y, Direction direction, Graphics graphic, Section section)
        {
            Bitmap sectionBitmap = WPFController.GetImage(WPFController.GetFilename(section.SectionType, direction), false, Direction.East);

            if (sectionBitmap == null)
            {
                return;
            }

            graphic.DrawImage(sectionBitmap, x, y, 320, 320);
        }

        public static Direction UpdateDirection(Direction direction, SectionTypes sectionType)
        {
            if (sectionType == SectionTypes.RightCorner)
            {
                if (direction == Direction.West)
                {
                    direction = Direction.North;
                }
                else
                {
                    direction++;
                }
            }

            if (sectionType == SectionTypes.LeftCorner)
            {
                if (direction == Direction.North)
                {
                    direction = Direction.West;
                }
                else
                {
                    direction--;
                }
            }

            return direction;
        }

        public static List<int> GetTrackSize(Track track)
        {
            int currentX = -320;
            int currentY = 0;

            List<int> x = new List<int>();
            List<int> y = new List<int>();

            x.Add(0);
            y.Add(0);

            Direction direction = Direction.East;
            
            foreach (Section section in track.Sections)
            {
                if (direction == Direction.East)
                {
                    currentX += 320;
                }

                if (direction == Direction.West)
                {
                    currentX -= 320;
                }

                if (direction == Direction.North)
                {
                    currentY += 320;
                }

                if (direction == Direction.South)
                {
                    currentY -= 320;
                }
                
                direction = UpdateDirection(direction, section.SectionType);

                x.Add(currentX);
                y.Add(currentY);
            }

            int lowestX = x.Min();
            int highestX = x.Max();
            int lowestY = y.Min();
            int highestY = y.Max();

            bool xFlag = false;
            bool yFlag = false;

            if (lowestX < 0)
            {
                lowestX *= -1;
                xFlag = true;
            }

            if (lowestY < 0)
            {
                lowestY *= -1;
                yFlag = true; 
            }

            int xDimension = lowestX + highestX + 320;
            int yDimension = lowestY + highestY + 320;

            if (xFlag)
            {
                lowestX *= -1;
            }
            
            int startX;

            if (lowestX == 0)
            {
                startX = highestX;
            }
            else
            {
                startX = lowestX * -1;
            }

            int startY;

            if (highestY == 0)
            {
                startY = 0;
            }
            else
            {
                startY = highestY;
            }

            List<int> returnList = new List<int>() { xDimension, yDimension, startX, startY };

            return returnList;
        }
    }
}
