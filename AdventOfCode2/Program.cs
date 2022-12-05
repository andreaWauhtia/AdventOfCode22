using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace AdventOfCode2
{
    public class Program
    {

        public enum FirstColumn
        {
            /// <summary>
            /// Pierre
            /// </summary>
            [Description("A")]//Pierre
            A = 1,
            /// <summary>
            /// Papier
            /// </summary>
            [Description("B")]
            B = 2,

     /// <summary>
     /// Ciseaux
     /// </summary>
            [Description("C")]
            C = 3
        }
        public enum SecondColumn
        {
            /// <summary>
            /// Pierre
            /// </summary>
            [Description("X")] 
            X = 1,
            /// <summary>
            /// Papier
            /// </summary>
            [Description("Y")]
            Y = 2,
            /// <summary>
            /// Ciseaux
            /// </summary>
            [Description("Z")]
            Z = 3
        }
        public static void Main(string[] args)
        {
            var partHistory = ReadInput();
            int result = 0;
            
            //Parallel.ForEach(partHistory, part =>
            foreach (var part in partHistory)
            {
                
                FirstColumn firstColumn;
                Enum.TryParse(part.Substring(0,1), out firstColumn);
                SecondColumn secondColumn;
                Enum.TryParse(part.Substring(2, 1), out secondColumn);
                bool? partResult = GetPartResult(firstColumn, secondColumn);
                result += ((int)secondColumn + GetPartResultValue(partResult));
                Console.WriteLine($"{part} ==> {partResult} ");
            }//);
            Console.WriteLine($"Total: {result}");
            Console.ReadKey();
        }

     

        private static string[] ReadInput()
        {
            return File.ReadAllLines("input.txt");
        }
        /// <summary>
        /// On cherche à voir si joueur deux à gagner
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool? GetPartResult(FirstColumn first, SecondColumn second)
        {
            if ((int)first == (int)second) return null;
            else {
                switch (first)
                {
                    case FirstColumn.A:
                        {
                            if (second == SecondColumn.Y) return true;
                            else  return false;
                        }
                        break;
                    case FirstColumn.B:
                        {
                            if (second == SecondColumn.Z) return true;
                            else return false;
                        }
                        break;
                    case FirstColumn.C:
                        {
                            if (second == SecondColumn.X) return true;
                            else return false;
                        }
                        break;
                    default: return null;
                }
            }
        }
        private static int GetPartResultValue(bool? result)
        {
            return result.HasValue ? result.Value ? 6 : 0 : 3;
        }
    }
}
