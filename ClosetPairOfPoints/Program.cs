using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ClosetPairOfPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordinate program = new Coordinate();
            List<Coordinate> Coordinates = program.GenerateCoordinate(100000);

            //Brute Force times.
            Stopwatch sw = Stopwatch.StartNew();
            var result = program.BruteForce(Coordinates);
            sw.Stop();
            Console.WriteLine("Bruce Force Times = {0} ms, Closet Point = {1}", sw.Elapsed.TotalMilliseconds, result);

            //Divine and Conquer times.
            Stopwatch sw2 = Stopwatch.StartNew();
            //Sort Coordinate before parse it to DivineAndConquer!!!
            List<Coordinate> SortedCoordinate = Coordinates.OrderBy(x => x.ValueX).ToList();
            //Let get started!!
            var result2 = program.DivineAndConquer(SortedCoordinate);
            sw2.Stop();
            Console.WriteLine("Divine and Conquer Times = {0} ms, Closet Point = {1}", sw2.Elapsed.TotalMilliseconds, result2);

            Console.ReadLine();
        }
    }
}
