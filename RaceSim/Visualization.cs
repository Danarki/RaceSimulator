using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

// TODO Methodes toevoegen voor versimpeling
// TODO Methode DetermineLeftCornerCoordinates DetermineRightCornerCoordinates versimpelen

namespace RaceSim
{
    public static class Visualization
    {
        public static Direction Direction; // default East, public for UnitTesting

        private static int[] cursorLocation = new int[2]; // [x, y]

        private static int[] cursorUpdates =  new int[2]; // used for updating cursorLocation after creating graphic

        public static void Initialize()
        {
            Console.SetCursorPosition(cursorLocation[0], cursorLocation[1]);

            cursorLocation[0] = 42;
            cursorLocation[1] = 21;

            cursorUpdates[0] = 0;
            cursorUpdates[1] = 0;

            Direction = Direction.East;

            DrawTrack(Data.CurrentRace.Track); // initial draw

            Data.CurrentRace.DriversChanged += DriversChanged; 

            Data.CurrentRace.RaceChanged += RaceChanged;
        }

        public static void RaceChanged()
        {
            Console.Clear();
            Initialize();
        }

        public static void DriversChanged(DriversChangedEventArgs args)
        {
            DrawTrack(args.Track);

            Data.CurrentRace.startTimer();
        }

        public static string[] AddParticipantsToGraphic(string[] graphic, SectionData sectionData, SectionTypes sectionType, Section section)
        {
            IParticipant participant1 = sectionData.Left;
            IParticipant participant2 = sectionData.Right;
            string[] result = new string[graphic.Length]; // new string array for the result of this function
            Array.Copy(graphic, result, graphic.Length); // make a copy of the graphic, so that we don't edit the original

            if (participant1 != null)
            {
                char participant1Title = ' ';

                if (participant1.Equipment.IsBroken)
                {
                    participant1Title = '@';
                }
                else
                {
                    participant1Title = participant1.Name[0];
                }


                int index = sectionData.DistanceLeft;

                int resultIndex = 2;

                if (sectionType != SectionTypes.Finish)
                {
                    Data.CurrentRace._rounds[participant1] = new Tuple<int, bool>(Data.CurrentRace._rounds[participant1].Item1, true);

                }

                if (sectionType == SectionTypes.Straight || sectionType == SectionTypes.StartGrid || sectionType == SectionTypes.Finish)
                {
                    
                    if (participant1.Direction == Model.Direction.West)
                    {
                        index = (index - 6) * -1;
                        resultIndex = 4;
                    } else if (participant1.Direction == Model.Direction.East && sectionType == SectionTypes.Finish)
                    {
                        if (index >= 3)
                        {
                            Tuple<int,bool> participantRounds = Data.CurrentRace._rounds[participant1];

                            if (participantRounds.Item2)
                            {
                                Data.CurrentRace._rounds[participant1] = new Tuple<int, bool>(participantRounds.Item1 + 1, false);
                            }

                            if (Data.CurrentRace._rounds[participant1].Item1 == Data.CurrentRace.maxRounds)
                            {
                                Data.CurrentRace.removeDriverFromPositions(participant1, section, true);
                            }
                        }
                    }
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    int[] i = DetermineRightCornerCoordinates(participant1.Direction, index, true);

                    index = i[0];
                    resultIndex = i[1];
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    int[] i = DetermineLeftCornerCoordinates(participant1.Direction, index, true);

                    index = i[0];
                    resultIndex = i[1];
                }

                if (sectionType == SectionTypes.Straight)
                {
                    if (participant1.Direction == Model.Direction.South)
                    {
                        resultIndex = index;
                        index = 4;
                    }

                    if (participant1.Direction == Model.Direction.North)
                    {
                        resultIndex = (index - 6) * -1;
                        index = 2;
                    }
                }

                StringBuilder sb = new StringBuilder(result[resultIndex]);

                if (index > 6)
                {
                    sb[6] = participant1Title;
                }
                else
                {

                    sb[index] = participant1Title;
                }

                result[resultIndex] = sb.ToString();
            }

            // check if participant2 is filled as it might be empty with uneven participants
            if (participant2 != null)
            {
                char participant2Title = ' ';

                if (participant2.Equipment.IsBroken)
                {
                    participant2Title = '@';
                }
                else
                {
                    participant2Title = participant2.Name[0];
                }

                int index = sectionData.DistanceRight;

                int resultIndex = 4;

                if (sectionType != SectionTypes.Finish)
                {
                    Data.CurrentRace._rounds[participant2] = new Tuple<int, bool>(Data.CurrentRace._rounds[participant2].Item1, true);
                }

                if (sectionType == SectionTypes.Straight || sectionType == SectionTypes.StartGrid || sectionType == SectionTypes.Finish)
                {

                    if (participant2.Direction == Model.Direction.West)
                    {
                        index = (index - 6) * -1;
                        resultIndex = 2;
                    }
                    else if (participant2.Direction == Model.Direction.East && sectionType == SectionTypes.Finish)
                    {
                        if (index >= 4)
                        {
                            Tuple<int, bool> participantRounds = Data.CurrentRace._rounds[participant2];

                            if (participantRounds.Item2)
                            {
                                Data.CurrentRace._rounds[participant2] = new Tuple<int, bool>(participantRounds.Item1 + 1, false);
                            }

                            if (Data.CurrentRace._rounds[participant2].Item1 == Data.CurrentRace.maxRounds)
                            {
                                Data.CurrentRace.removeDriverFromPositions(participant2, section, false);
                            }
                        }
                    }
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    int[] i = DetermineRightCornerCoordinates(participant2.Direction, index, false);

                    index = i[0];
                    resultIndex = i[1];
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    int[] i = DetermineLeftCornerCoordinates(participant2.Direction, index, false);

                    index = i[0];
                    resultIndex = i[1];
                }

                if (sectionType == SectionTypes.Straight)
                {
                    if (participant2.Direction == Model.Direction.South)
                    {
                        resultIndex = index;
                        index = 2;
                    }

                    if (participant2.Direction == Model.Direction.North)
                    {
                        resultIndex = (index - 6) * -1;
                        index = 4;
                    }
                }

                StringBuilder sb = new StringBuilder(result[resultIndex]);

                if (index > 6)
                {
                    sb[6] = participant2Title;
                }
                else
                {

                    sb[index] = participant2Title;
                }
                result[resultIndex] = sb.ToString();
            }

            return result;
        }

        public static void DrawTrack(Track track)
        {
            Console.SetCursorPosition(cursorLocation[0], cursorLocation[1]);

            cursorUpdates[0] = 0;
            cursorUpdates[1] = 0;

            foreach (var section in track.Sections)
            {
                SectionData sectionData = Data.CurrentRace.GetSectionData(section);
                string[] graphic = new string[7];

                if (sectionData.Left != null || sectionData.Right != null) // there is at least 1 racer on this section
                {
                    string[] returnedGraphic = DetermineGraphic(section.SectionType);
                    graphic = AddParticipantsToGraphic(returnedGraphic, sectionData, section.SectionType, section);
                }
                else
                {
                    graphic = DetermineGraphic(section.SectionType);
                }

                foreach (string s in graphic)
                {
                    Console.WriteLine(s);
                    cursorLocation[1]++;

                    Console.SetCursorPosition(cursorLocation[0], cursorLocation[1]);
                }

                // reset cursor location to the next line and start line + the updates for a different location
                cursorLocation[0] += 7 + cursorUpdates[0];
                cursorLocation[1] -= 7 - cursorUpdates[1];

                if (cursorLocation[1] < 0)
                {
                    cursorLocation[1] = 0;
                }

                if (cursorLocation[0] < 0)
                {
                    cursorLocation[0] = 0;
                }

                Console.SetCursorPosition(cursorLocation[0], cursorLocation[1]);

                cursorUpdates[0] = 0;
                cursorUpdates[1] = 0;
            }
        }

        public static int[] DetermineLeftCornerCoordinates(Direction direction, int distance, bool isLeft)
        {
            int index = distance;

            if (index > 6)
            {
                index = 6;
            }
            else if (index < 0)
            {
                index = 0;
            }

            int resultIndex = 2;

            if (isLeft)
            {
                if (direction == Model.Direction.North)
                {
                    switch (index)
                    {
                        case 0:
                            index = 2;
                            resultIndex = 6;
                            break;
                        case 1:
                            index = 2;
                            resultIndex = 5;
                            break;
                        case 2:
                            resultIndex = 5;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 1;
                            resultIndex = 4;
                            break;
                        case 5:
                            index = 1;
                            resultIndex = 4;
                            break;
                        case 6:
                            index = 0;
                            resultIndex = 4;
                            break;
                    }
                } else if (direction == Model.Direction.South)
                {
                    switch (index)
                    {
                        case 0:
                            index = 4;
                            resultIndex = 0;
                            break;
                        case 1:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 2:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 5;
                            resultIndex = 2;
                            break;
                        case 5:
                            index = 5;
                            resultIndex = 2;
                            break;
                        case 6:
                            index = 6;
                            resultIndex = 2;
                            break;
                    }
                }
                else if (direction == Model.Direction.West)
                {
                    switch (index)
                    {
                        case 0:
                            index = 6;
                            resultIndex = 4;
                            break;
                        case 1:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 2:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 5:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 6:
                            index = 4;
                            resultIndex = 6;
                            break;
                    }
                }
                else if (direction == Model.Direction.East)
                {
                    switch (index)
                    {
                        case 2:
                            index = 1;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 5:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 6:
                            index = 2;
                            resultIndex = 0;
                            break; 
                    }
                }

                return new int[] { index, resultIndex };
            }
            else
            {
                if (direction == Model.Direction.East)
                {
                    resultIndex = 4;
                    switch (index)
                    {
                        case 2:
                            index = 3;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 4;
                            break;
                        case 4:
                            resultIndex = 3;
                            break;
                        case 5:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 6:
                            index = 4;
                            resultIndex = 0;
                            break;
                    }
                }
                else if (direction == Model.Direction.North)
                {
                    switch (index)
                    {
                        case 0:
                            index = 4;
                            resultIndex = 6;
                            break;
                        case 1:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 2:
                            index = 4;
                            resultIndex = 3;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 3;
                            resultIndex = 2;
                            break;
                        case 5:
                            index = 1;
                            resultIndex = 2;
                            break;
                        case 6:
                            index = 0;
                            resultIndex = 2;
                            break;
                    }
                }
                else if (direction == Model.Direction.West)
                {
                    switch (index)
                    {
                        case 0:
                            index = 6;
                            resultIndex = 2;
                            break;
                        case 1:
                            index = 5;
                            resultIndex = 2;
                            break;
                        case 2:
                            index = 3;
                            resultIndex = 2;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 2;
                            resultIndex = 3;
                            break;
                        case 5:
                            index = 2;
                            resultIndex = 5;
                            break;
                        case 6:
                            index = 2;
                            resultIndex = 6;
                            break;
                    }
                }
                else if (direction == Model.Direction.South)
                {
                    switch (index)
                    {
                        case 0:
                            index = 2;
                            resultIndex = 0;
                            break;
                        case 1:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 2:
                            resultIndex = 3;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 3;
                            resultIndex = 4;
                            break;
                        case 5:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 6:
                            index = 6;
                            resultIndex = 4;
                            break;
                    }
                }

                return new int[] { index, resultIndex };
            }

            return new int[] { 0, 0 };

        }

        public static int[] DetermineRightCornerCoordinates(Direction direction, int distance, bool isLeft)
        {
            int index = distance;

            if (index > 6)
            {
                index = 6;
            } else if (index < 0)
            {
                index = 0;
            }

            int resultIndex = 2;

            if (isLeft)
            {
                if (direction == Model.Direction.East)
                {
                    switch (index)
                    {
                        case 2:
                            index = 3;
                            break;
                        case 3:
                            index = 4;
                            break;
                        case 4:
                            index = 4;
                            resultIndex = 3;
                            break;
                        case 5:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 6:
                            index = 4;
                            resultIndex = 6;
                            break;
                    }
                }
                else if (direction == Model.Direction.West)
                {
                    index = (index - 6) * -1;
                    switch (index)
                    {
                        case 0:
                            index = 2;
                            resultIndex = 0;
                            break;
                        case 1:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 2:
                            index = 2;
                            resultIndex = 3;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 3;
                            resultIndex = 4;
                            break;
                        case 5:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 6:
                            resultIndex = 4;
                            break;
                    }
                }
                else if (direction == Model.Direction.South)
                {
                    switch (index)
                    {
                        case 0:
                            index = 4;
                            resultIndex = 0;
                            break;
                        case 1:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 2:
                            index = 4;
                            resultIndex = 3;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 3;
                            resultIndex = 4;
                            break;
                        case 5:
                            index = 1;
                            resultIndex = 4;
                            break;
                        case 6:
                            index = 0;
                            resultIndex = 4;
                            break;
                    }
                }
                else if (direction == Model.Direction.North)
                {
                    switch (index)
                    {
                        case 0:
                            index = 2;
                            resultIndex = 6;
                            break;
                        case 1:
                            index = 2;
                            resultIndex = 5;
                            break;
                        case 2:
                            resultIndex = 3;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 3;
                            resultIndex = 2;
                            break;
                        case 5:
                            resultIndex = 2;
                            break;
                        case 6:
                            resultIndex = 2;
                            break;
                    }
                }

                return new int[] { index, resultIndex };
            }
            else
            {
                if (direction == Model.Direction.West)
                {
                    switch (index)
                    {
                        case 0:
                            index = 6;
                            resultIndex = 2;
                            break;
                        case 1:
                            index = 5;
                            resultIndex = 2;
                            break;
                        case 2:
                            index = 5;
                            resultIndex = 2;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 5:
                            index = 4;
                            resultIndex = 1;
                            break;
                        case 6:
                            index = 4;
                            resultIndex = 0;
                            break;
                    }
                }
                else if (direction == Model.Direction.South)
                {
                    switch (index)
                    {
                        case 0:
                            index = 2;
                            resultIndex = 0;
                            break;
                        case 1:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 2:
                            index = 2;
                            resultIndex = 1;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 2;
                            break;
                        case 4:
                            index = 1;
                            resultIndex = 2;
                            break;
                        case 5:
                            index = 1;
                            resultIndex = 2;
                            break;
                        case 6:
                            index = 0;
                            resultIndex = 2;
                            break;
                    }
                }
                else if (direction == Model.Direction.East)
                {
                    switch (index)
                    {
                        case 0:
                            index = 0;
                            resultIndex = 4;
                            break;
                        case 1:
                            index = 1;
                            resultIndex = 4;
                            break;
                        case 2:
                            index = 1;
                            resultIndex = 4;
                            break;
                        case 3:
                            index = 2;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 2;
                            resultIndex = 5;
                            break;
                        case 5:
                            index = 2;
                            resultIndex = 5;
                            break;
                        case 6:
                            index = 2;
                            resultIndex = 6;
                            break;
                    }
                }
                else if (direction == Model.Direction.North)
                {
                    switch (index)
                    {
                        case 0:
                            index = 4;
                            resultIndex = 6;
                            break;
                        case 1:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 2:
                            index = 4;
                            resultIndex = 5;
                            break;
                        case 3:
                            index = 4;
                            resultIndex = 4;
                            break;
                        case 4:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 5:
                            index = 5;
                            resultIndex = 4;
                            break;
                        case 6:
                            index = 6;
                            resultIndex = 4;
                            break;
                    }
                }

                return new int[] { index, resultIndex };
            }



            return new int[] { 0, 0 };

        }

        /*
         * This function gets a SectionType as parameter. It first checks what Direction the track is facing.
         * After this is determined, it checks what SectionType is given, updates the cursorUpdates variable so that after rendering the graphics the cursor will be at the right location for the next rendering.
         * After setting cursorUpdates the function returns the string array from graphics.
         *
         */
        public static string[] DetermineGraphic(SectionTypes sectionType)
        {
            if (Direction == Direction.North) // facing North
            {
                if (sectionType == SectionTypes.Finish)
                {
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] -= 7;

                    return _finishVertical;
                }

                if (sectionType == SectionTypes.Straight)
                {
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] -= 7;

                    return _straightVertical;
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    Direction++;

                    return _turnRightNorth;
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    Direction = Direction.West;
                    cursorUpdates[0] -= 14;

                    return _turnLeftNorth;
                }
            }

            if (Direction == Direction.East) // facing East
            {
                if (sectionType == SectionTypes.Finish)
                {
                    return _finishHorizontal;
                }

                if (sectionType == SectionTypes.Straight)
                {
                    return _straightHorizontal;
                }

                if (sectionType == SectionTypes.StartGrid)
                {
                    return _startGrid;
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    Direction++;
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] += 7;

                    return _turnRightEast;
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    Direction--;
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] -= 7;

                    return _turnLeftEast;
                }
            }

            if (Direction == Direction.South) // facing South
            {
                if (sectionType == SectionTypes.Finish)
                {
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] += 7;

                    return _finishVertical;
                }

                if (sectionType == SectionTypes.Straight || sectionType == SectionTypes.StartGrid)
                {
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] += 7;

                    return _straightVertical;
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    Direction++;
                    cursorUpdates[0] -= 14;

                    return _turnRightSouth;
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    Direction--;

                    return _turnLeftSouth;
                }
            }

            if (Direction == Direction.West) // facing West
            {
                if (sectionType == SectionTypes.Finish)
                {
                    cursorUpdates[0] -= 14;
                    cursorUpdates[1] += 0; 

                    return _finishHorizontal;
                }

                if (sectionType == SectionTypes.Straight || sectionType == SectionTypes.StartGrid)
                {
                    cursorUpdates[0] -= 14;
                    cursorUpdates[1] += 0;

                    return _straightHorizontal;
                }

                if (sectionType == SectionTypes.RightCorner)
                {
                    Direction = Direction.North;
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] -= 7;

                    return _turnRightWest;
                }

                if (sectionType == SectionTypes.LeftCorner)
                {
                    Direction--;
                    cursorUpdates[0] -= 7;
                    cursorUpdates[1] += 7;

                    return _turnLeftWest;
                }
            }

            return null;
        }

        #region graphics

        private static string[] _finishHorizontal =
        {
            "-------", 
            "   #   ", 
            "___#___", 
            "   #   ", 
            "___#___", 
            "   #   ", 
            "-------"
        };

        private static string[] _finishVertical =
        {
            "I | | I",
            "I | | I",
            "I | | I",
            "I#####I",
            "I | | I",
            "I | | I",
            "I | | I",
        };

        private static string[] _straightHorizontal =
        {
            "-------", 
            "       ", 
            "_______", 
            "       ", 
            "_______", 
            "       ", 
            "-------"
        };

        private static string[] _startGrid =
        {
            "------+", 
            "      |", 
            "______|", 
            "      |", 
            "______|", 
            "      |", 
            "------+"
        };

        private static string[] _straightVertical =
        {
            "I | | I",
            "I | | I",
            "I | | I",
            "I | | I",
            "I | | I",
            "I | | I",
            "I | | I",
        };

        private static string[] _turnRightEast = 
        { 
            "------\\", 
            "      I",
            "____  I", 
            "    | I", 
            "__. | I", 
            "  | | I", 
            "\\ | | I"

        };

        private static string[] _turnRightNorth = 
        {
            "/------", 
            "I      ", 
            "I  ____", 
            "I |    ", 
            "I | .__", 
            "I | |  ", 
            "I | | /"
        };

        private static string[] _turnRightWest = 
        { 
            "I | | \\", 
            "I | |  ",
            "I | |__", 
            "I |    ", 
            "I |____", 
            "I      ", 
            "\\------"
        };

        private static string[] _turnRightSouth = 
        {
            "/ | | I", 
            "  | | I", 
            "__| | I", 
            "    | I", 
            "____| I", 
            "      I", 
            "------/"
        };

        private static string[] _turnLeftNorth = _turnRightEast;
        private static string[] _turnLeftSouth = _turnRightWest;
        private static string[] _turnLeftEast = _turnRightSouth;
        private static string[] _turnLeftWest = _turnRightNorth;

        #endregion
    }
}
