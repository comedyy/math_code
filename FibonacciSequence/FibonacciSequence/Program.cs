using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciSequence
{
    struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    struct Matrix
    {
        public Matrix(int x1, int x2, int y1, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public int x1;
        public int x2;
        public int y1;
        public int y2;

        public Matrix Mul(Matrix m)
        {
            return new Matrix()
            {
                x1 = x1 * m.x1 + x2 * m.y1,
                x2 = x1 * m.x2 + x2 * m.y2,
                y1 = y1 * m.x1 + y2 * m.y1,
                y2 = y1 * m.x2 + y2 * m.y2
            };
        }

        public Vector2 Mul(Vector2 v)
        {
            return new Vector2() {
                x = x1 * v.x + x2 * v.y,
                y = y1 * v.x + y2 * v.y
            };
        }

        public override string ToString()
        {
            return string.Format("[{0},{1}]\n[{2},{3}]", x1, x2, y1, y2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] ret = new int[30];

            Matrix m = new Matrix(1, 1, 1, 0);
            Vector2 res = new Vector2(1, 1);
            int b = 10;

            for (int i = 0; i < 30; i++)
            {
                ret[i] = fn(m, res, i);
                Console.WriteLine(ret[i]);
            }

            Console.WriteLine("黄金分割点： {0}",  ret[28] * 1.0f / ret[29]);
        }

        static int fn(Matrix m, Vector2 res, int b)
        {
            while (b > 0)
            {
                if ((b & 1) != 0) res = m.Mul(res);
                m = m.Mul(m);
                b >>= 1;
            }

            return res.x;
        }
    }
}
