using System.Reflection;
using System.Timers;
using Model;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;
using Timer = System.Timers.Timer;

namespace Controller
{
    public delegate void DriversChangedDelegate(DriversChangedEventArgs e);
    public delegate void RaceChangedDelegate();
    public delegate void RaceEndedDelegate();

    /* Timer event = 0.5 seconds
     * Length section = 70 meter, 1 char = 10 meter
     * 
     */
    public class Race
    {
        public event DriversChangedDelegate DriversChanged;
        public event RaceChangedDelegate RaceChanged;
        public event RaceEndedDelegate RaceEnded;
        //TODO Race end event maken

        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;
        private int[] _randomBoundaries = new int[2];
        private Dictionary<Section, SectionData> _positions;
        private Timer timer;
        public Dictionary<IParticipant, Tuple<int, bool>> _rounds;
        public int maxRounds = 3;
        public int[] _pointsRemaining = new int[3];

        public void startTimer()
        {
            timer.Start();
        }

        public void stopTimer()
        {
            timer.Stop();
        }

        public void removeTimer()
        {
            timer = null;
        }

        public void EndRace()
        {
            Console.Clear();
            removeTimer();
            
            // TODO add implementation for end screen?
        }

        public Race(Track track, List<IParticipant> participants)
        {
            timer = new Timer(500); // 0.5 sec
            timer.Elapsed += OnTimedEvent;

            Track = track;
            Participants = participants;

            StartTime = DateTime.Now;

            _random = new Random(DateTime.Now.Millisecond);
            _randomBoundaries[0] = 0;
            _randomBoundaries[1] = 10;

            _positions = new Dictionary<Section, SectionData>();
            _rounds = new Dictionary<IParticipant, Tuple<int, bool>>();

            _pointsRemaining = new int[3] { 7, 5, 3 };

            PlaceParticipantsOnTrack(Track, Participants);

            foreach (IParticipant participant in Participants)
            {
                _rounds.Add(participant, new Tuple<int, bool>(0, false));
            }
        }

        public void removeDriverFromPositions(IParticipant participant, Section section, bool isLeft)
        {
            int points = 1;

            if (_pointsRemaining.Length > 0)
            {
                points = _pointsRemaining[0];
                _pointsRemaining = _pointsRemaining.Skip(1).ToArray();
            }

            if (isLeft)
            {
                participant.Points += points;
                _positions[section].Left = null;
                _positions[section].DistanceLeft = 0;
            }
            else
            {
                participant.Points += points;
                _positions[section].Right = null;
                _positions[section].DistanceRight = 0;
            }
        }

        protected virtual void OnTimedEvent(object sender, ElapsedEventArgs args)
        {
            timer.Stop();

            bool setLeft = false;
            bool setRight = false;

            int newLeft = 0;
            int newRight = 0;

            int sectionIndex = 1;

            bool driversLeft = false;

            foreach (IParticipant participant in Participants)
            {
                Random identifier = new Random(DateTime.Now.Millisecond);

                if (identifier.Next(_randomBoundaries[0], _randomBoundaries[1]) == _random.Next(_randomBoundaries[0], _randomBoundaries[1]))
                {
                    participant.Equipment.IsBroken = true;
                    participant.Equipment.Quality--;
                }
            }

            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                bool updateDrivers = false;

                if (!setLeft)
                {
                    if (sectionData.Left != null)
                    {
                        driversLeft = true;

                        if (sectionData.Left.Equipment.Quality <= 0)
                        {
                            sectionData.Left = null;
                        }
                        else
                        {
                            if (sectionData.Left.Equipment.IsBroken)
                            {
                                Random identifier = new Random(DateTime.Now.Millisecond);
                                int identifierInt =
                                    identifier.Next(_randomBoundaries[0] / 2, _randomBoundaries[1] / 2) +
                                    sectionData.Left.Equipment.Quality;

                                if (identifierInt > _random.Next(_randomBoundaries[0], _randomBoundaries[1] * 2))
                                {
                                    sectionData.Left.Equipment.IsBroken = false;
                                }
                            }


                            if (!sectionData.Left.Equipment.IsBroken)
                            {
                                var steps = sectionData.Left.getSteps();
                                var position = sectionData.DistanceLeft + steps;

                                if (position <= 6)
                                {
                                    _positions[section].DistanceLeft = position;
                                }
                                else
                                {
                                    _positions[section].DistanceLeft = position;

                                    newLeft = position - 7;

                                    updateDrivers = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    driversLeft = true;
                    setLeft = false;
                }

                if (!setRight)
                {
                    if (sectionData.Right != null)
                    {
                        driversLeft = true;

                        if (sectionData.Right.Equipment.Quality <= 0)
                        {
                            sectionData.Right = null;
                        }
                        else
                        {
                            if (sectionData.Right.Equipment.IsBroken)
                            {
                                Random identifier = new Random(DateTime.Now.Millisecond);
                                int identifierInt =
                                    identifier.Next(_randomBoundaries[0] / 2, _randomBoundaries[1] / 2) +
                                    sectionData.Right.Equipment.Quality;
                                if (identifierInt > _random.Next(_randomBoundaries[0], _randomBoundaries[1]))
                                {
                                    sectionData.Right.Equipment.IsBroken = false;
                                }
                            }


                            if (!sectionData.Right.Equipment.IsBroken)
                            {
                                var steps = sectionData.Right.getSteps();
                                var position = sectionData.DistanceRight + steps;

                                if (position <= 6)
                                {
                                    _positions[section].DistanceRight = position;
                                }
                                else
                                {
                                    _positions[section].DistanceRight = position;

                                    newRight = position - 7;

                                    updateDrivers = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    driversLeft = true;
                    setRight = false;
                }


                if (updateDrivers)
                {
                    Section nextSection = null;
                    int x = 0;
                    foreach (Section section2 in Data.CurrentRace.Track.Sections)
                    {
                        if (x == sectionIndex)
                        {
                            nextSection = section2;
                            break;
                        }

                        x++;
                    }

                    MoveDriver(section, nextSection, newLeft, newRight, sectionData.Left,
                        sectionData.Right, ref setLeft, ref setRight);
                }
                sectionIndex++;
            }

            if (driversLeft)
            {
                DriversChangedRaise();
            }

            if (!driversLeft)
            {
                stopTimer();

                RaceChangedRaise();
                
                Data.NextRace();

                if (Data.CurrentRace == null)
                {
                    RaceEndedRaise();
                    return;
                }

                RaceChangedRaise();
            }
        }

        public void MoveDriver(Section currentSection, Section nextSection, int newLeft, int newRight, IParticipant participant, IParticipant participant2, ref bool setLeft, ref bool setRight)
        {
            if (nextSection == null)
            {
                nextSection = Track.Sections.First();
            }

            SectionData nextPosition = GetSectionData(nextSection);
            SectionData currentPosition = _positions[currentSection];
            bool leftUpdated = false;
            bool rightUpdated = false;

            if (currentPosition.Left == null && currentPosition.Right == null)
            {
                return;
            }

            if (nextPosition.Left == null && currentPosition.DistanceLeft > 6)
            {
                currentPosition.Left.updateDirection(currentSection.SectionType);

                _positions[nextSection].Left = participant;
                _positions[nextSection].DistanceLeft = newLeft;

                _positions[currentSection].Left = null;
                _positions[currentSection].DistanceLeft = 0;


                leftUpdated = true;
            }
            else if (nextPosition.Right == null && currentPosition.DistanceLeft > 6)
            {
                currentPosition.Left.updateDirection(currentSection.SectionType);

                _positions[nextSection].Right = participant;
                _positions[nextSection].DistanceRight = newLeft;

                _positions[currentSection].Left = null;
                _positions[currentSection].DistanceLeft = 0;


                leftUpdated = true;
                rightUpdated = true;

                var steps = nextPosition.Left.getSteps();
                var position = nextPosition.DistanceLeft + steps;

                _positions[nextSection].DistanceLeft = position;
            }

            if (nextPosition.Right == null && currentPosition.DistanceRight > 6)
            {
                currentPosition.Right.updateDirection(currentSection.SectionType);

                if (nextPosition.Left == null)
                {
                    _positions[nextSection].Left = participant2;
                    _positions[nextSection].DistanceLeft = newRight;

                    leftUpdated = true;
                }
                else
                {
                    _positions[nextSection].Right = participant2;

                    _positions[nextSection].DistanceRight = newRight;

                    if (!leftUpdated && rightUpdated)
                    {
                        var steps = nextPosition.Left.getSteps();
                        var position = nextPosition.DistanceLeft + steps;

                        _positions[nextSection].DistanceLeft = position;
                    }

                }

                _positions[currentSection].Right = null;
                _positions[currentSection].DistanceRight = 0;
                rightUpdated = true;
            }

            setLeft = leftUpdated;
            setRight = rightUpdated;
        }

        public void DriversChangedRaise()
        {
            DriversChangedEventArgs a = new DriversChangedEventArgs();

            a.Track = Data.CurrentRace.Track;

            DriversChanged?.Invoke(a);
        }

        public void RaceEndedRaise()
        {
            RaceEnded?.Invoke();
        }

        public void RaceChangedRaise()
        {
            RaceChanged?.Invoke();
        }

        public void RandomizeEquipment(List<IParticipant> participants)
        {
            foreach (var participant in participants)
            {
                participant.Equipment.Quality = new Random(DateTime.Now.Millisecond).Next(1, 10);
                participant.Equipment.Performance = new Random(DateTime.Now.Millisecond).Next(1, 10);
            }
        }

        public SectionData GetSectionData(Section section)
        {
            SectionData returnable = _positions.GetValueOrDefault(section);

            if (returnable != null)
            {
                return returnable;
            }
            else
            {
                _positions.Add(section, new SectionData());

                return _positions.GetValueOrDefault(section);
            }
        }

        public void PlaceParticipantsOnTrack(Track track, List<IParticipant> participants)
        {
            LinkedListNode<Section> sectionNode = track.Sections.First;

            IParticipant participantsBuffer = null;

            IParticipant lastParticipant = participants.Last();

            foreach (IParticipant participant in participants)
            {
                if (participantsBuffer != null) // check if buffer is full (2 participants loaded for SectionData) or if participant is last in list
                {
                    SectionData sectionData = new SectionData();

                    sectionData.Right = participant;
                    sectionData.DistanceRight = 1;
                    sectionData.Left = participantsBuffer;
                    sectionData.DistanceLeft = 4;

                    _positions.Add(sectionNode.Value, sectionData);

                    if (sectionNode == track.Sections.First)
                    {
                        sectionNode = track.Sections.Last; // go to last node
                    }
                    else
                    {
                        sectionNode = sectionNode.Previous; // previous node
                    }

                    participantsBuffer = null; // empty the buffer
                }
                else if (lastParticipant == participant)
                {
                    SectionData sectionData = new SectionData();

                    sectionData.Left = participant;
                    sectionData.DistanceLeft = 4;

                    _positions.Add(sectionNode.Value, sectionData);
                }
                else
                {
                    participantsBuffer = participant;
                }
            }
        }
    }
}
