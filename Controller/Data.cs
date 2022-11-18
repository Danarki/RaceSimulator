﻿using Model;
using System.ComponentModel;

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
                    CurrentRace.StopTimer();
                }

                CurrentRace = null;
                CurrentRace = new Race(track, Competition.Participants);
                CurrentRace.StartTimer();
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
                        Performance = 0,
                        Speed = 1,
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
                        Performance = 0,
                        Speed = 1,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },

                new Driver
                {
                    Name = "Alberto",
                    TeamColor = TeamColors.Yellow,
                    Equipment = new Car
                    {
                        Quality = 10,
                        Performance = 10,
                        Speed = 1,
                        IsBroken = false
                    },
                    Direction = Direction.East
                },

            };


            Competition.Participants = participants;
        }


        public static void AddTracks()
        {
            SectionTypes[] sections =
            {
                SectionTypes.Straight,
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
            };


            Competition.Tracks.Enqueue(new Track("Super Track", sections));

            SectionTypes[] sections2 =
            {
                SectionTypes.Straight,
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Finish,
            };

            Competition.Tracks.Enqueue(new Track("Super Track BIG!", sections2));

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

            //Competition.Tracks.Enqueue(new Track("Super Track3", sections3));
        }
    }
}