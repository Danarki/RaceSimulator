using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Controller;
using Model;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CompetitionWindow _competitionWindow;
        public RaceWindow _raceWindow;

        public DataContextClass DataContextClass;

        public MainWindow()
        {
            InitializeComponent();

            WPFController.Initialize();

            Data.Initialize();

            Data.NextRace();

            DataContextClass = new DataContextClass();
            DataContextClass.TrackName = Data.CurrentRace.Track.Name;
            this.DataContext = DataContextClass;

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceChanged += OnRaceChanged;
            Data.CurrentRace.RaceEnded += OnRaceEnded;
        }

        private void OnDriversChanged(DriversChangedEventArgs e)
        {
            this.ImageComponent.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.ImageComponent.Source = null;
                    this.ImageComponent.Source = Visualization.DrawTrack(e.Track);
                }));

            Data.CurrentRace.StartTimer();
        }

        private void OnRaceEnded()
        {
            Data.CurrentRace = null;
            WPFController.ClearCache();
            DataContextClass.TrackName = "";

            IParticipant winner = Data.Competition.Participants.MaxBy(x => x.Points);

            DataContextClass.WinnerName = "Winnaar: " + winner.Name + " met " + winner.Points + " punten!";
        }

        private void OnRaceChanged()
        {
            if (Data.CurrentRace != null)
            {
                DataContextClass.TrackName = Data.CurrentRace.Track.Name;

                WPFController.ClearCache();
                WPFController.Initialize();

                Track track = Data.CurrentRace.Track;

                Data.CurrentRace.DriversChanged += OnDriversChanged;
                Data.CurrentRace.RaceChanged += OnRaceChanged;
                Data.CurrentRace.RaceEnded += OnRaceEnded;

                if (track != null)
                {
                    this.ImageComponent.Dispatcher.BeginInvoke(
                        DispatcherPriority.Render,
                        new Action(() =>
                        {
                            this.ImageComponent.Source = null;
                            this.ImageComponent.Source = Visualization.DrawTrack(track);
                        }));
                }

                if (_raceWindow != null)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Data.CurrentRace.DriversChanged +=
                            ((RaceContextClass)_raceWindow.DataContext).OnDriversChanged;
                    });
                }

                if (_competitionWindow != null)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Data.CurrentRace.DriversChanged += ((CompetitionContextClass)_competitionWindow.DataContext)
                            .OnDriversChanged;
                    });
                }

                Data.CurrentRace.StartTimer();
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Open_CompetitionWindow(object sender, RoutedEventArgs e)
        {
            if (_competitionWindow == null)
            {

                _competitionWindow = new CompetitionWindow();
                _competitionWindow.InitializeComponent();
                Data.CurrentRace.DriversChanged += ((CompetitionContextClass)_competitionWindow.DataContext).OnDriversChanged;
                _competitionWindow.Show();
            }
            else
            {
                try
                {
                    _competitionWindow.WindowState = WindowState.Minimized;
                    _competitionWindow.Show();
                    _competitionWindow.WindowState = WindowState.Normal;
                }
                catch 
                {
                    _competitionWindow = new CompetitionWindow();
                    _competitionWindow.InitializeComponent();
                    Data.CurrentRace.DriversChanged += ((CompetitionContextClass)_competitionWindow.DataContext).OnDriversChanged;
                    _competitionWindow.Show();
                }
            }
        }

        private void MenuItem_Open_RaceWindow(object sender, RoutedEventArgs e)
        {
            if (_raceWindow == null)
            {
                _raceWindow = new RaceWindow();
                _raceWindow.InitializeComponent();
                Data.CurrentRace.DriversChanged += ((RaceContextClass)_raceWindow.DataContext).OnDriversChanged; 
                _raceWindow.Show();
            }
            else
            {
                try
                {
                    _raceWindow.WindowState = WindowState.Minimized;
                    _raceWindow.Show();
                    _raceWindow.WindowState = WindowState.Normal;
                }
                catch
                {
                    _raceWindow = new RaceWindow();
                    _raceWindow.InitializeComponent();
                    Data.CurrentRace.DriversChanged += ((RaceContextClass)_raceWindow.DataContext).OnDriversChanged;
                    _raceWindow.Show();
                }
            }
        }
    }
}
