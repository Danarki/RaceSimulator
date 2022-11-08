using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Controller;
using Color = System.Drawing.Color;
using Model;


namespace WPF
{
    public static class WPFController
    {
        private static Dictionary<string, Bitmap> bitmapDictionary;

        public static void Initialize()
        {
            bitmapDictionary = new Dictionary<string, Bitmap>();

            if (Data.CurrentRace != null)
            {
                foreach (IParticipant participant in Data.CurrentRace.Participants)
                {
                    participant.Equipment.Quality = 10;
                    participant.Equipment.IsBroken = false;
                    participant.Direction = Direction.East;
                }
            }
        }

        public static Bitmap GetEmptyBitmap(int width, int height)
        {
            string key = "empty";

            if (!bitmapDictionary.ContainsKey(key))
            {
                bitmapDictionary.Add(key, new Bitmap(width, height));
                Graphics graphics = Graphics.FromImage(bitmapDictionary[key]);
                graphics.Clear(Color.LightGreen);
            }

            return (Bitmap) bitmapDictionary[key].Clone();
        }

        public static string GetParticipantFileName(IParticipant participant, Direction direction)
        {
            if (participant.Equipment.IsBroken)
            {
                return _participantCrashed;
            }

            TeamColors color = participant.TeamColor;

            if (color == TeamColors.Blue)
            {
                if (direction == Direction.East)
                {
                    return _participantBlueEast;
                }

                if (direction == Direction.North)
                {
                    return _participantBlueNorth;
                }

                if (direction == Direction.West)
                {
                    return _participantBlueWest;
                }

                if (direction == Direction.South)
                {
                    return _participantBlueSouth;
                }

            }

            if (color == TeamColors.Green)
            {
                if (direction == Direction.East)
                {
                    return _participantGreenEast;
                }

                if (direction == Direction.North)
                {
                    return _participantGreenNorth;
                }

                if (direction == Direction.West)
                {
                    return _participantGreenWest;
                }

                if (direction == Direction.South)
                {
                    return _participantGreenSouth;
                }
            }

            if (color == TeamColors.Grey)
            {
                if (direction == Direction.East)
                {
                    return _participantGrayEast;
                }

                if (direction == Direction.North)
                {
                    return _participantGrayNorth;
                }

                if (direction == Direction.West)
                {
                    return _participantGrayWest;
                }

                if (direction == Direction.South)
                {
                    return _participantGraySouth;
                }
            }

            if (color == TeamColors.Yellow)
            {
                if (direction == Direction.East)
                {
                    return _participantYellowEast;
                }

                if (direction == Direction.North)
                {
                    return _participantYellowNorth;
                }

                if (direction == Direction.West)
                {
                    return _participantYellowWest;
                }

                if (direction == Direction.South)
                {
                    return _participantYellowSouth;
                }

            }

            if (color == TeamColors.Red)
            {
                if (direction == Direction.East)
                {
                    return _participantRedEast;
                }

                if (direction == Direction.North)
                {
                    return _participantRedNorth;
                }

                if (direction == Direction.West)
                {
                    return _participantRedWest;
                }

                if (direction == Direction.South)
                {
                    return _participantRedSouth;
                }
            }

            return _participantCrashed;
        }

        public static string GetSectionFilename(SectionTypes sectionType, Direction direction)
        {
            if (sectionType == SectionTypes.StartGrid)
            {
                return _startGrid;
            }

            if (sectionType == SectionTypes.Finish)
            {
                if (direction == Direction.East || direction == Direction.West)
                {
                    return _finishHorizontal;
                }

                if (direction == Direction.North || direction == Direction.South)
                {
                    return _finishVertical;
                }
            }

            if (sectionType == SectionTypes.RightCorner)
            {
                if (direction == Direction.East)
                {
                    return _turnRightEast;
                }

                if (direction == Direction.West)
                {
                    return _turnRightWest;
                }

                if (direction == Direction.North)
                {
                    return _turnRightNorth;
                }

                if (direction == Direction.South)
                {
                    return _turnRightSouth;
                }
            }

            if (sectionType == SectionTypes.LeftCorner)
            {
                if (direction == Direction.East)
                {
                    return _turnLeftEast;
                }

                if (direction == Direction.West)
                {
                    return _turnLeftWest;
                }

                if (direction == Direction.North)
                {
                    return _turnLeftNorth;
                }

                if (direction == Direction.South)
                {
                    return _turnLeftSouth;
                }
            }

            if (sectionType == SectionTypes.Straight)
            {
                if (direction == Direction.East || direction == Direction.West)
                {
                    return _straightHorizontal;
                }

                if (direction == Direction.North || direction == Direction.South)
                {
                    return _straightVertical;
                }
            }

            return "";
        }

        public static Bitmap GetImage(string name, bool rotate, Direction direction)
        {
            if (name == "")
            {
                return null;
            }

            bitmapDictionary.TryGetValue(name, out Bitmap bmp);

            if (bmp == null)
            {
                string path = "../../../" + name;

                bmp = new Bitmap(path);

                bitmapDictionary.Add(name, bmp);
            }

            return bmp;
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static void ClearCache()
        {
            bitmapDictionary.Clear();
        }

        #region graphicsNames

        private static string _participantGreenEast = @"Images/GreenEast.png";
        private static string _participantGreenNorth = @"Images/GreenNorth.png";
        private static string _participantGreenWest = @"Images/GreenWest.png";
        private static string _participantGreenSouth = @"Images/GreenSouth.png";

        private static string _participantYellowEast = @"Images/YellowEast.png";
        private static string _participantYellowNorth = @"Images/YellowNorth.png";
        private static string _participantYellowWest = @"Images/YellowWest.png";
        private static string _participantYellowSouth = @"Images/YellowSouth.png";

        private static string _participantBlueEast = @"Images/BlueEast.png";
        private static string _participantBlueNorth = @"Images/BlueNorth.png";
        private static string _participantBlueWest = @"Images/BlueWest.png";
        private static string _participantBlueSouth = @"Images/BlueSouth.png";

        private static string _participantGrayEast = @"Images/GrayEast.png";
        private static string _participantGrayNorth = @"Images/GrayNorth.png";
        private static string _participantGrayWest = @"Images/GrayWest.png";
        private static string _participantGraySouth = @"Images/GraySouth.png";

        private static string _participantRedEast = @"Images/RedEast.png";
        private static string _participantRedNorth = @"Images/RedNorth.png";
        private static string _participantRedWest = @"Images/RedWest.png";
        private static string _participantRedSouth = @"Images/RedSouth.png";

        private static string _participantCrashed = @"Images/Crashed.png";

        private static string _finishHorizontal = @"Images/FinishHorizontal.png";
        private static string _finishVertical = @"Images/FinishVertical.png";
        private static string _straightHorizontal = @"Images/StraightHorizontal.png";
        private static string _straightVertical = @"Images/StraightVertical.png";
        private static string _startGrid = @"Images/StartGrid.png";
        private static string _turnRightEast = @"Images/EastRightCorner.png";
        private static string _turnRightNorth = @"Images/NorthRightCorner.png";
        private static string _turnRightWest = @"Images/WestRightCorner.png";
        private static string _turnRightSouth = @"Images/SouthRightCorner.png";


        private static string _turnLeftNorth = _turnRightEast;
        private static string _turnLeftSouth = _turnRightWest;
        private static string _turnLeftEast = _turnRightSouth;
        private static string _turnLeftWest = _turnRightNorth;

        #endregion
    }
}
