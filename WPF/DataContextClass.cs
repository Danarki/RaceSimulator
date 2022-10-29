using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;
using RaceSim;

namespace WPF
{
    public class DataContextClass : INotifyPropertyChanged
    {
        //public delegate void PropertyChangedDelegate(PropertyChangedEventArgs e);
       
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string TrackName {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }

        }
      
        public DataContextClass(){}

        public DataContextClass(string name)
        {
            this.name = name;
            OnPropertyChanged();
        }


        public void OnPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
