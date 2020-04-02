using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM
{
    struct Point
    {
        public int x;
        public int y;
    }

    struct Range {
        public Range(int xmin, int ymin, int xmax, int ymax)
        {
            x_min = xmin;
            y_min = ymin;
            x_max = xmax;
            y_max = ymax;
        }

        public int x_min;
        public int y_min;
        public int x_max;
        public int y_max;
    }

    class Program
    {
        const int MAX_POINTS = 7000;
        const int RANGE_COUNT = 4;

        static Range[] RR = new Range[] {
            new Range(0, 0, 1000, 1000),
            new Range(2000, 2000, 3000, 3000),
            new Range(4000, 4000, 5000, 5000),
            new Range(6000, 6000, 7000, 7000),
        };

        static Point[] middles = new Point[20];
        static List<Point>[] middle_range_points = new List<Point>[20];

        static void Main(string[] args)
        {
            Random r = new Random();
            Point[] ps = new Point[MAX_POINTS];
            for (int i = 0; i < ps.Length; i++)
            {
                int x = (int)((i / 7000f) * RANGE_COUNT);
                Range range = RR[x];
                ps[i] = new Point() {
                    x = r.Next(range.x_min, range.x_max),
                    y = r.Next(range.y_min, range.y_max),
                };
            }

            // random get middle points 
            for (int i = 0; i < middles.Length; i++)
            {
                middles[i] = new Point() {
                    x = r.Next(0, 7000),
                    y = r.Next(0, 7000),
                };
            }

            int count = 1000;
            while (count-- > 0)
            {
                // clear range
                for (int i = 0; i < middles.Length; i++)
                {
                    if (middle_range_points[i] == null)
                    {
                        middle_range_points[i] = new List<Point>();
                    }
                    else {
                        middle_range_points[i].Clear();
                    }
                }

                for (int i = 0; i < MAX_POINTS; i++)
                {
                    Point p = ps[i];
                    int index = GetNearPointIndex(middles, p);
                    middle_range_points[index].Add(p);
                }

                // recalc middle
                for (int i = 0; i < middles.Length; i++)
                {
                    middles[i] = CalMiddle(middle_range_points[i]);
                }
            }

            // recalc middle
            for (int i = 0; i < middles.Length; i++)
            {
                if (middles[i].x <= 0 || middles[i].y <= 0)
                {
                    continue;
                }

                Console.WriteLine("{0} {1} {2}", middles[i].x, middles[i].y, middle_range_points[i].Count);
            }
        }


        public static Point CalMiddle(List<Point> ps)
        {
            if (ps.Count == 0)
            {
                return new Point() { x = -9999, y = -9999 };
            }

            double x = 0;
            double y = 0;

            for (int i = 0; i < ps.Count; i++)
            {
                x += ps[i].x;
                y += ps[i].y;
            }

            return new Point()
            {
                x = (int)(x / ps.Count),
                y = (int)(y / ps.Count)
            };
        }

        public static int GetNearPointIndex(Point[] ps, Point p)
        {
            float min_dis = int.MaxValue;
            int min_index = -1;

            for (int i = 0; i < ps.Length; i++)
            {
                Point p1 = ps[i];
                float distance = (p.x - p1.x) * (p.x - p1.x) + (p.y - p1.y) * (p.y - p1.y);

                if (distance < min_dis)
                {
                    min_dis = distance;
                    min_index = i;
                }
            }

            return min_index;
        }
    }
}
