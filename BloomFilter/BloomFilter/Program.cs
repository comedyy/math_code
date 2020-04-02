// https://blog.csdn.net/daliaojie/article/details/10931359
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    class Program
    {
        const int M = 32000000;
        const int N = 1000000;
        const int k = 8;

        static void Main(string[] args)
        {
            HashSet<string> black = new HashSet<string>();
            HashSet<string> check = new HashSet<string>();
            byte[] bs = new byte[M];
            List<Func<string, int>> lst_func = new List<Func<string, int>>() {
                HashCode.Hash1,
                HashCode.Hash2,
                HashCode.Hash3,
                HashCode.Hash4,
                HashCode.Hash5,
                HashCode.Hash6,
                HashCode.Hash7,
                HashCode.Hash8,
            };

            while (black.Count < N)
            {
                black.Add(getRandomString(32));
            }

            while (check.Count < N)
            {
                string c = getRandomString(32);
                if (!black.Contains(c))
                {
                    check.Add(c);
                }
            }

            Console.WriteLine(black.Count);
            Console.WriteLine(check.Count);


            foreach (var s in black)
            {
                foreach (var func in lst_func)
                {
                    int k = func(s) % M;
                    bs[k] = (byte)1;
                }
            }

            int not_pass_count = 0;
            foreach (var s in check)
            {
                bool is_all_1 = true;
                foreach (var func in lst_func)
                {
                    int k = func(s) % M;
                    is_all_1 &= (bs[k] == (byte)1);

                    if (!is_all_1)
                    {
                        not_pass_count++;
                        break;
                    }
                }
            }

            Console.WriteLine(not_pass_count * 1.0f / N);
        }

        static Random m_rnd = new Random();
        public static char getRandomChar()
        {
            int ret = m_rnd.Next(122);
            while (ret < 48 || (ret > 57 && ret < 65) || (ret > 90 && ret < 97))
            {
                ret = m_rnd.Next(122);
            }
            return (char)ret;
        }
        public static string getRandomString(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(getRandomChar());
            }
            return sb.ToString();
        }
    }
}
