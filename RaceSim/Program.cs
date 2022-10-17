using System;
using Model;
using Controller;

namespace RaceSim
{
    class Program
    {
        public static void Main()
        {
            Data.Initialize();

            Data.NextRace();

            Visualization.Initialize();

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}