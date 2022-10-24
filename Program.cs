using System;
using System.Collections.Generic;
using System.Linq;

namespace Suma3
{
    public class Program
    {
        private static int brute = 0;

        public static void Main(string[] args)
        {
            bool print;
            Console.WriteLine("Print subsets? (y/n)");
            string? yn = Console.ReadLine();
            if (yn == "y")
            {
                print = true;
            }
            else if (yn == "n")
            {
                print = false;
            }
            else
            {
                Console.WriteLine("Invalid input - aborting");
                return;
            }

            while (true)
            {
                uint n;
                do
                {
                    try
                    {
                        Console.WriteLine("Enter a number");
#pragma warning disable CS8604
                        n = uint.Parse(Console.ReadLine());
#pragma warning restore CS8604
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                while (true);

                double ans = n % 3 == 1 ?
                 (1d/3d) * (Math.Pow(2, (double)n) + Math.Pow(2, Math.Floor((1d/3d) * n))) :
                 (1d/3d) * (Math.Pow(2, (double)n) + (2 *  Math.Pow(2, Math.Floor((1d / 3d) * n))));
                Console.WriteLine($"Formula yields { ans }");

                List<int> nums = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    nums.Add(i + 1);
                }

                // Permute(ref nums, 0, (int)n - 1);
                IEnumerable<IEnumerable<int>> subsets = SubSetsOf<int>(nums);
                brute = 0;
                for (int i = 0; i < subsets.Count(); i++)
                {
                    int sum = 0;
                    for (int j = 0; j < subsets.ElementAt(i).Count(); j++)
                    {
                        sum += subsets.ElementAt(i).ElementAt(j);
                    }
                    if (sum % 3 == 0)
                    {
                        brute++;
                    }
                    if (print)
                    {
                        PrintList(subsets.ElementAt(i).ToList());
                    }
                }
                Console.WriteLine($"Brute force computation yields { brute }");
            }
        }

        private static IEnumerable<IEnumerable<T>> SubSetsOf<T>(IEnumerable<T> source)
        {
            if (!source.Any())
                return Enumerable.Repeat(Enumerable.Empty<T>(), 1);

            var element = source.Take(1);

            var haveNots = SubSetsOf(source.Skip(1));
            var haves = haveNots.Select(set => element.Concat(set));

            return haves.Concat(haveNots);
        }

        private static void Permute(ref List<int> list, int from, int to)
        {
            if (from == to)
            {
                PrintList(list);
                return;
            }

            for (int i = from; i <= to; i++)
            {
                Swap(ref list, i, from);
                Permute(ref list, from + 1, to);
                Swap(ref list, i, from);
            }
        }

        private static void Swap(ref List<int> list, int i, int j)
        {
            if (i == j)
            {
                return;
            }

            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private static void PrintList(List<int> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("{ } valid");
                return;
            }

            string s = "{ ";
            int a = 0;
            for (int i = 0; i < list.Count; i++)
            {
                s += list[i].ToString() + ", ";
                a += list[i];
            }
            s = s.Remove(s.Length - 2);
            s += " }";
            if (a % 3 == 0)
            {
                s += " valid";
            }
            Console.WriteLine(s);
        }

        /*int F(int x, int n)
        {
            int ret = 1;
            for (int i = 0; i < n; i++)
            {
                ret *= 1 + (int)Math.Pow(x, i + 1);
            }
            return ret;
        }*/
    }
}