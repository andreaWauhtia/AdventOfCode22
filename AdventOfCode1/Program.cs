using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            var bags = GetElvesBag(input);
            var orderedBags = bags.OrderByDescending(x => x.Value);
            Console.WriteLine($"Heaviest elf: {orderedBags.First().Key} {orderedBags.First().Value}");
            var top3 = orderedBags.Take(3);

            Console.WriteLine($"Top 3 weight: {top3.Sum(x => x.Value)}");
            Console.ReadKey();

        }
        private static Dictionary<int,int> GetElvesBag(string[] input)
        {
            Dictionary<int, List<int>> elvesBags = new Dictionary<int, List<int>>();
            int number = 1;
            List<int> bag = new List<int>();
            foreach (var row in input)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    int val = int.Parse(row);
                    bag.Add(val);
                }
                else
                {
                    elvesBags.Add(number, bag);
                    bag = new List<int>();
                    number++;
                }
            }
            Dictionary<int, int> bags = new Dictionary<int, int>();
            foreach (var key in elvesBags.Keys)
            {
                var content = elvesBags[key];
                bags.Add(key, content.Sum());
            }
            return bags;
        }
    }
}
