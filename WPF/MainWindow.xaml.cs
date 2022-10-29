using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private Window1 _window1;
        private Window2 _window2;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DriversEventHandler = delegate {};

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

            //DataContextClass = new DataContextClass("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceChanged += OnRaceChanged;
            Data.CurrentRace.RaceEnded += OnRaceChanged;
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

            Data.CurrentRace.startTimer();
        }

        private void OnRaceEnded()
        {
            Data.CurrentRace = null;
            WPFController.ClearCache();
            this.ImageComponent.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.ImageComponent.Source = null;
                    this.ImageComponent.Source = Visualization.EndRace();
                }));
        }

        private void OnRaceChanged()
        {
            //Data.NextRace();
            DataContextClass.TrackName = Data.CurrentRace.Track.Name;

            WPFController.ClearCache();
           WPFController.Initialize();

           Track track = Data.CurrentRace.Track;
           
           Data.CurrentRace.DriversChanged += OnDriversChanged;
           Data.CurrentRace.RaceChanged += OnRaceChanged;
           Data.CurrentRace.RaceEnded += OnRaceEnded;

           if (track == null)
           {
               int a = 0;
           }
           else
           {
               this.ImageComponent.Dispatcher.BeginInvoke(
                   DispatcherPriority.Render,
                   new Action(() =>
                   {
                       this.ImageComponent.Source = null;
                       this.ImageComponent.Source = Visualization.DrawTrack(track);
                   }));
           }

           Data.CurrentRace.startTimer();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Open_Window1(object sender, RoutedEventArgs e)
        {
            _window1 = new Window1();
            _window1.InitializeComponent();
            _window1.Show();
        }

        private void MenuItem_Open_Window2(object sender, RoutedEventArgs e)
        {
            _window2 = new Window2();
            _window2.InitializeComponent();
            _window2.Show();
        }
    }
}
