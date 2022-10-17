using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            Competition.Tracks = new Queue<Track> { };

            AddParticipants();
            
            AddTracks();
        }

        public static void NextRace()
        {
            Track track = Competition.NextTrack();

            if(track != null)
            {
                if (CurrentRace != null)
                {
                    CurrentRace.stopTimer();
                }

                CurrentRace = null;
                CurrentRace = new Race(track, Competition.Participants);
                CurrentRace.startTimer();
            }
            else
            {
                CurrentRace = null;
            }
        }

        public static void AddParticipants()
        {
            List<IParticipant> participants = new List<IParticipant>
            {

                new Driver
                {
                    Name = "Daan",
                    TeamColor = TeamColors.Green,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 3,
                        Speed = 10,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },

                new Driver
                {
                    Name = "Rudolf",
                    TeamColor = TeamColors.Red,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 3,
                        Speed = 10,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },

                //new Driver
                //{
                //    Name = "Alberto",
                //    TeamColor = TeamColors.Yellow,
                //    Equipment = new Car
                //    {
                //        Quality = 1,
                //        Performance = 1,
                //        Speed = 10,
                //        IsBroken = false
                //    },
                //    Direction = Direction.East
                //}

            };


            Competition.Participants = participants;
        }


        public static void AddTracks()
        {
            SectionTypes[] sections =
            {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
                SectionTypes.Straight,
            };

            Competition.Tracks.Enqueue(new Track("Super Track", sections));

            //SectionTypes[] sections2 =
            //{
            //    SectionTypes.StartGrid,
            //    SectionTypes.RightCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.Straight,
            //    SectionTypes.RightCorner,
            //    SectionTypes.RightCorner,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.RightCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.RightCorner,
            //    SectionTypes.RightCorner,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.LeftCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.RightCorner,
            //    SectionTypes.RightCorner,
            //    SectionTypes.Straight,
            //    SectionTypes.Finish,
            //    SectionTypes.Straight,
            //};

            //Competition.Tracks.Enqueue(new Track("Super Track2", sections2));

            SectionTypes[] sections3 =
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Finish,
                SectionTypes.Straight,
            };

            Competition.Tracks.Enqueue(new Track("Super Track3", sections3));

        }
    }
}