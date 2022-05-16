using System;

namespace calc
{
    class Calculator
    {
        public static double CalcOperations(double num1, double num2, string operations)
        {
            double res = double.NaN;
            switch (operations)
            {
                case "+":
                    res = num1 + num2;
                    break;
                case "-":
                    res = num1 - num2;
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        res = num1 / num2;
                    }
                    break;
                case "*":
                    res = num1 * num2;
                    break;
                default:
                    break;
            }
            return res;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool end = false;
            while (!end)
            {
                double result = 0;
                string In1 = "";
                string In2 = "";
                double Num1 = 0;
                double Num2 = 0;
                Console.Write("Enter a number \n");
                In1 = Console.ReadLine();
                if (!double.TryParse(In1, out Num1))
                {
                    Console.WriteLine("Please, enter only integer or double numbers");
                    break;
                }

                Console.Write("Enter another number \n");
                In2 = Console.ReadLine();
                if (!double.TryParse(In2, out Num2))
                {
                    Console.WriteLine("Please, enter only integer or double numbers");
                    break;
                }

                Console.WriteLine("Choose an operation:");
                Console.WriteLine("+");
                Console.WriteLine("-");
                Console.WriteLine("*");
                Console.WriteLine("/\n");

                string operations = Console.ReadLine();
                result = Calculator.CalcOperations(Num1, Num2, operations);
                Console.WriteLine(result);
                Console.WriteLine("Enter 'n' if you want to exit the programm else press Enter");
                if (Console.ReadLine() == "n") end = true;
            }
        }
    }
}