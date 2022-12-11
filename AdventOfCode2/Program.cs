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
            [Description("Pierre")]//Pierre
            A = 1,
            /// <summary>
            /// Papier
            /// </summary>
            [Description("Papier")]
            B = 2,

     /// <summary>
     /// Ciseaux
     /// </summary>
            [Description("Ciseaux")]
            C = 3
        }
        public enum SecondColumn
        {
            /// <summary>
            /// Pierre
            /// </summary>
            [Description("Pierre")] 
            X = 1,
            /// <summary>
            /// Papier
            /// </summary>
            [Description("Papier")]
            Y = 2,
            /// <summary>
            /// Ciseaux
            /// </summary>
            [Description("Ciseaux")]
            Z = 3
        }

        public enum ExpectedResultColumn
        {
            /// <summary>
            /// Loose
            /// </summary>
            [Description("Loose")]
            X = 0,
            /// <summary>
            /// Draw
            /// </summary>
            [Description("Draw")]
            Y = 3,
            /// <summary>
            /// Win
            /// </summary>
            [Description("Win")]
            Z = 6
        }
        public static void Main(string[] args)
        {
            var partHistory = ReadInput();
            //Part1(partHistory);
            Part2(partHistory);
        }

        private static void Part1(string[] partHistory)
        {
            int result = 0;

            Parallel.ForEach(partHistory, part =>
            {

                FirstColumn firstColumn;
                Enum.TryParse(part.Substring(0, 1), out firstColumn);
                SecondColumn secondColumn;
                Enum.TryParse(part.Substring(2, 1), out secondColumn);
                bool? partResult = GetPartResult(firstColumn, secondColumn);
                result += ((int)secondColumn + GetPartResultValue(partResult));
                Console.WriteLine($"{part} ==> {partResult} ");
            });
            Console.WriteLine($"Total: {result}");
            Console.ReadKey();
        }
        /// <summary>
        /// Cette fois, la 2ème info désigne le résultat de la partie
        /// </summary>
        /// <param name="partHistory"></param>
        private static void Part2(string[] partHistory)
        {
            int result = 0;

            foreach(var part in partHistory)
             {

                FirstColumn firstColumn;
                Enum.TryParse(part.Substring(0, 1), out firstColumn);
                ExpectedResultColumn resultColumn;
                Enum.TryParse(part.Substring(2, 1), out resultColumn);
                int partForm = GetPartForm(firstColumn, resultColumn);
                int partResult = (partForm + (int)resultColumn);
                result += partResult;
            };
          
            Console.WriteLine($"PART 2 Total: {result}");
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

        /// <summary>
        /// On cherche à voir si joueur deux à gagner
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static int GetPartForm(FirstColumn first, ExpectedResultColumn second)
        {
            switch(second){
                case ExpectedResultColumn.X:
                    {
                        if (first == FirstColumn.A) return (int)SecondColumn.Z;
                        if (first == FirstColumn.B) return (int)SecondColumn.X;
                        if (first == FirstColumn.C) return (int)SecondColumn.Y;
                    }
                    break;
                case ExpectedResultColumn.Y:
                    {
                        if (first == FirstColumn.A) return (int)SecondColumn.X;
                        if (first == FirstColumn.B) return (int)SecondColumn.Y;
                        if (first == FirstColumn.C) return (int)SecondColumn.Z;
                    }
                    break;
                case ExpectedResultColumn.Z:
                    {
                        if (first == FirstColumn.A) return (int)SecondColumn.Y;
                        if (first == FirstColumn.B) return (int)SecondColumn.Z;
                        if (first == FirstColumn.C) return (int)SecondColumn.X;
                    }
                    break;
            }
            return 0;
        }

        private static int GetPartResultValue(bool? result)
        {
            return result.HasValue ? result.Value ? 6 : 0 : 3;
        }

    }
    public static class EnumExtensions
    {

        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
