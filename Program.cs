using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] rimNumbers = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
            string num1="", num2="", operation = "";
            int arabNum1 = 0, arabNum2 = 0;
            bool rimSymbol = false, arabSymbol = false;

            //ввод данных и очистка вырожения от пробелов
            Console.WriteLine("Start program...\nInput: ");

            string input = Console.ReadLine();
            
            input = input.Trim();
            string[] exprethion = input.Split(' ');

            List<string> trimExpration = new List<string>();
            for (int i = 0; i < exprethion.Length; i++)
            {
                if (exprethion[i] != "")
                {
                    trimExpration.Add(exprethion[i]);
                }
            }
            //получение значений из введенных данных
            if (trimExpration.Count == 3)
            {
                num1 = trimExpration[0];
                operation = trimExpration[1];
                num2 = trimExpration[2];
            }
            else
            {
                Console.WriteLine($"Некорректный формат ввода {trimExpration.Count}/ {num1}/ {operation}/ {num2}");
            }
            //проверям введены римские символы или нет
            for (int i = 1; i <rimNumbers.Length; i++)
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

                Console.WriteLine("Output: " + ConvertToRim(rezalt));
            }
            //если введены арабские, преобразование стринг в цифры и вычисление
            if ((int.TryParse(num1, out arabNum1) == true && int.TryParse(num2, out arabNum2) == true) && (rimSymbol == false) && (arabNum1 < 11) && (arabNum2 < 11))
            {
                int rezalt = Count(operation, arabNum1, arabNum2);
                arabSymbol = true;

                Console.WriteLine("Output: " + rezalt);
            }
            // если введены не римские и не арабские цифры 
            if (rimSymbol == false && arabSymbol == false)
            {
                Console.WriteLine("Некорректный формат чисел");
            }

            Console.WriteLine("Program finish");
            Console.ReadKey();
        }

        private static int Count(string operation, int arabNum1, int arabNum2, bool rim=false)
        {
            int rezalt = 0;
            switch (operation)
            {
                case "+":
                    rezalt = arabNum1 + arabNum2;
                    break;
                case "-":
                    if (rim==true && arabNum1 < arabNum2)
                    {
                        Console.WriteLine("Неверное выражение(в римской системе нет отрицательных чисел)");
                        break;
                    }
                    else
                    {
                        rezalt = arabNum1 - arabNum2;
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
                        rezalt = arabNum1 / arabNum2;
                    }
                    break;
                case "*":
                    rezalt = arabNum1 * arabNum2;
                    break;
                default:
                    Console.WriteLine("Неверное выражение");
                    break;
            }

            return rezalt;
        }//вычисляет (А +-/* В)

        private static string ConvertToRim(int number) 
        {
            string[] rimHundred = {"","C" };
            string[] rimTen = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] rimNumbers = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };
            
            string rezult = "";
           
            string arabNum = number.ToString();
            
            int length = arabNum.Length;

            for (int i = length; i>0; i--) 
            {
                if (i == 1)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    rezult +=rimNumbers[index] + " ";
                }

                if (i == 2)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    rezult += rimTen[index] + " ";
                }

                if (i == 3)
                {
                    int index = int.Parse(arabNum[length - i].ToString());

                    rezult += rimHundred[index]+ " ";
                }
            }
            return rezult;
        }// конвертирует арабские цифры в римские
    }
}
