using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosetPairOfPoints
{
    class Coordinate
    {
        float X;
        float Y;

        public Coordinate() { }

        public Coordinate(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public float ValueX
        {
            get { return X; }
        }

        public float ValueY
        {
            get { return Y; }
        }

        public float Distance(Coordinate Co1, Coordinate Co2)
        {
            return (float)Math.Sqrt(Math.Abs((Co1.X - Co2.X)* (Co1.X - Co2.X) + (Co1.Y - Co2.Y) * (Co1.Y - Co2.Y)));
        }

        public List<Coordinate> GenerateCoordinate(int number)
        {
            List<Coordinate> randomCoordinate = new List<Coordinate> { };
            Random random = new Random(10);
            randomCoordinate = Enumerable.Range(0, number).Select(i => new Coordinate((float)random.NextDouble(), (float)random.NextDouble())).ToList();

            return randomCoordinate;
        }

        public float BruteForce(List<Coordinate> Co)
        {
            float result = float.MaxValue;
            for (var i = 0; i < Co.Count; i++)
            {
                for (var j = i + 1; j < Co.Count; j++)
                {
                    var distance = Distance(Co[i], Co[j]);
                    if (distance < result)
                    {
                        result = distance;
                    }
                }            
            }
            return result;
        }

        public float DivineAndConquer(List<Coordinate> Co)
        {
            if (Co.Count <= 4)
            {
                return BruteForce(Co);
            }

            var mid = Co.Count / 2;
            var LeftCoordinate = Co.Take(mid).ToList();
            var LeftResult = DivineAndConquer(LeftCoordinate);

            var RightCoordinate = Co.Skip(mid).ToList();
            var RightResult = DivineAndConquer(RightCoordinate);

            var Result = (LeftResult > RightResult) ? RightResult : LeftResult;
            var midLine = (Co[mid-1].X + Co[mid].X) / 2;

            //Check the closet points around the middle line
            var CoordinateOnMiddleLine = Co.Where(x => x.ValueX >= midLine - Result && x.ValueX <= midLine + Result).ToList();
            var SortCoordinateByY = CoordinateOnMiddleLine.OrderBy(x => x.ValueY).ToList();

            for (int i = 0; i < SortCoordinateByY.Count; i++)
            {
                for(int j = i + 1; j < SortCoordinateByY.Count; j++)
                {
                    var temp_result = Distance(SortCoordinateByY[i], SortCoordinateByY[j]);
                    if (temp_result < Result)
                    {
                        Result = temp_result;
                    }
                }
            }
            return Result;
        }
    }
}
