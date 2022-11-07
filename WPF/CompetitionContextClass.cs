using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input.Manipulations;
using System.Windows.Threading;
using Controller;
using Model;

namespace WPF
{
    public class CompetitionContextClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Tuple<IParticipant, int>> ParticipantPointsList { get; set; }
        public List<Track> TrackList { get; set; }
        public string nextTrackName { get; set; }

        public void OnDriversChanged(DriversChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        public CompetitionContextClass()
        {
            ParticipantPointsList = new List<Tuple<IParticipant, int>>();
        }

        public void OnPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                List<IParticipant> pList = Data.Competition.Participants;
                List<IParticipant> sortedPList = pList.OrderByDescending(p => p.Points).ToList();
                
                ParticipantPointsList = new List<Tuple<IParticipant, int>>();

                foreach (IParticipant participant in sortedPList)
                {
                    ParticipantPointsList.Add(new Tuple<IParticipant, int>(participant, participant.Points));
                }

                TrackList = Data.Competition.Tracks.ToList();

                if (TrackList.Count > 1)
                {
                    nextTrackName = TrackList[1].Name;
                }
                else
                {
                    nextTrackName = "Dit is de laatste track!";
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
