using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] rimNumbers = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
            string num1 = "", num2 = "", operation = "";
            int arabNum1 = 0, arabNum2 = 0;
            bool rimSymbol = false, arabSymbol = false;

            
            Console.WriteLine("введите выражение используя либо римские либо арабские цифры от 0 до 10");

            string input = Console.ReadLine();

            bool gramatika = false;
            string pattern = @"[+\-*/]";
            MatchCollection matches = Regex.Matches(input, pattern);//ищем какая будет операция

            char[] separators = new char[] { ' ','+','*','/','-' }; 
            string[] exprethion = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (matches.Count == 1 && exprethion.Count()== 2) 
            {
                //получение значений из введенных данных
                gramatika = true;
                operation = matches[0].Value;
                num1 = exprethion[0];
                num2 = exprethion[1];
            }

            if (gramatika)
            {
                //проверям введены римские символы или нет
                for (int i = 1; i < rimNumbers.Length; i++)
                {
                    if (rimNumbers[i] == num1)
                    {
                        arabNum1 = i;
                    }
                    if (rimNumbers[i] == num2)
                    {
                        arabNum2 = i;
                    }
                    if (arabNum1 > 0 && arabNum2 > 0)
                    {
                        rimSymbol = true;
                        break;
                    }
                }
                //вычисление если введены римские цифры
                if (rimSymbol)
                {
                    int rezalt = Count(operation, arabNum1, arabNum2, rimSymbol);

                    Console.WriteLine("Результат: " + ConvertToRim(rezalt));
                }
                //если введены арабские, преобразование стринг в цифры и вычисление
                if ((int.TryParse(num1, out arabNum1) == true && int.TryParse(num2, out arabNum2) == true) && (rimSymbol == false) && (arabNum1 < 11) && (arabNum2 < 11))
                {
                    int rezalt = Count(operation, arabNum1, arabNum2);
                    arabSymbol = true;

                    Console.WriteLine("Результат: " + rezalt);
                }
                // если введены не римские и не арабские цифры 
                if (rimSymbol == false && arabSymbol == false)
                {
                    Console.WriteLine("Неверный формат цифр");
                }


            }

            else
            {
                Console.WriteLine("Неверный формат ввода");
            }

            Console.ReadKey();
        }


        public static int Count(string operation, int arabNum1, int arabNum2, bool rim = false)
        {
            int result = 0;
            switch (operation)
            {
                case "+":
                    result = arabNum1 + arabNum2;
                    break;
                case "-":
                    if (rim == true && arabNum1 < arabNum2)
                    {
                        Console.WriteLine("Неверное выражение(в римской системе нет отрицательных чисел)");
                        break;
                    }
                    else
                    {
                        result = arabNum1 - arabNum2;
                    }
                    break;
                case "/":
                    if (rim == true && arabNum1 < arabNum2)
                    {
                        Console.WriteLine("Неверное выражение");
                        break;
                    }
                    if (arabNum2 == 0)
                    {
                        Console.WriteLine("Неверное выражение");
                        break;
                    }
                    else
                    {
                        result = arabNum1 / arabNum2;
                    }
                    break;
                case "*":
                    result = arabNum1 * arabNum2;
                    break;
                default:
                    Console.WriteLine("Неверное выражение");
                    break;
            }

            return result;
        }

        public static string ConvertToRim(int number)
        {
            string[] rimHundred = { "", "C" };
            string[] rimTen = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] rimNumbers = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

            string result = "";

            string arabNum = number.ToString();

            int length = arabNum.Length;

            for (int i = length; i > 0; i--)
            {
                if (i == 1)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    result += rimNumbers[index] + " ";
                }

                if (i == 2)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    result += rimTen[index] + " ";
                }

                if (i == 3)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    result += rimHundred[index] + " ";
                }
            }
            return result;
        }
    }
}
