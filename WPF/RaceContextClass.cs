using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Controller;
using Model;

namespace WPF
{
    public class RaceContextClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Tuple<IParticipant, int>> IsBrokenParticipantsList { get; set; }
        public TimeSpan TimeElapsed { get; set; }
        public List<Tuple<IParticipant, IEquipment>> ParticipantPerformanceList { get; set; }
        private List<IParticipant> participants
        {
            get
            {
                return participants;
            }
            set
            {
                participants = value;
                OnPropertyChanged();
            }

        }

        public void OnDriversChanged(DriversChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        public RaceContextClass()
        {
            IsBrokenParticipantsList = new List<Tuple<IParticipant, int>>();
            ParticipantPerformanceList = new List<Tuple<IParticipant, IEquipment>>();
        }
        
        public TimeSpan StripMilliseconds(TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }

        public void OnPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                List<IParticipant> participants = Data.CurrentRace.Participants;

                var brokenQuery = 
                    from participant in participants
                    where participant.Equipment.IsBroken
                    select participant;

                if (Data.CurrentRace.StartTime != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime start =  Data.CurrentRace.StartTime;
                    TimeSpan timeSpan = now - start;

                    TimeElapsed = StripMilliseconds(timeSpan);
                }

                List<Tuple<IParticipant, int>> previousParticipantsList = IsBrokenParticipantsList;

                IsBrokenParticipantsList = new List<Tuple<IParticipant, int>>();

                foreach (IParticipant brokenParticipant in brokenQuery)
                {
                    bool previousFlag = false;
                    Tuple<IParticipant, int> t = null;
                    foreach (Tuple<IParticipant, int> participantTuple in previousParticipantsList)
                    {
                        if (participantTuple.Item1 == brokenParticipant)
                        {
                            t = participantTuple;
                        }
                    }

                    if (t != null)
                    {
                        Tuple<IParticipant, int> newParticipantTuple = new Tuple<IParticipant, int>(t.Item1, t.Item2 + 1);

                        IsBrokenParticipantsList.Add(newParticipantTuple);
                    }
                    else
                    {
                        Tuple<IParticipant, int> tuple = new Tuple<IParticipant, int>(brokenParticipant, 0);
                        IsBrokenParticipantsList.Add(tuple);
                    }
                }

                ParticipantPerformanceList = new List<Tuple<IParticipant, IEquipment>>();

                foreach (IParticipant participant in participants)
                {
                    ParticipantPerformanceList.Add(new Tuple<IParticipant, IEquipment>(participant, participant.Equipment));
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
