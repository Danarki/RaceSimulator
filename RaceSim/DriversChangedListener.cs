using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;

namespace RaceSim
{
    public class DriversChangedListener
    {
        public void DriversChanged(DriversChangedEventArgs args)
        {
            Visualization.DriversChanged(args);
        }
    }
}
