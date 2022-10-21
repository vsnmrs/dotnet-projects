using CalculatorLibrary;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endProgram = false;

            Calculator calculator = new Calculator();

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endProgram)
            {
                string number1, number2;
                double result;

                Console.WriteLine("Choose an option: ");
                Console.WriteLine("\t 1: Use Calculator");
                Console.WriteLine("\t 2: See usage count");
                Console.WriteLine("\t q: Quit programm");

                string command = Console.ReadLine();

                if (command == "1")
                {
                    Console.WriteLine("Type a number and then press ENTER");
                    number1 = Console.ReadLine();

                    double num1;
                    while (!double.TryParse(number1, out num1))
                    {
                        Console.WriteLine("The input is not valid. Enter a number value!");
                        number1 = Console.ReadLine();
                    }

                    Console.WriteLine("Type another number and then press ENTER");
                    number2 = Console.ReadLine();

                    double num2;
                    while (!double.TryParse(number2, out num2))
                    {
                        Console.WriteLine("The input is not valid. Enter a number value!");
                        number2 = Console.ReadLine();
                    }

                    Console.WriteLine("Choose an operation from the following list:");
                    Console.WriteLine("\t a - Add");
                    Console.WriteLine("\t s - Substract");
                    Console.WriteLine("\t m - Multiply");
                    Console.WriteLine("\t d - Divide");
                    Console.Write("Your option:");
                    string operation = Console.ReadLine();

                    try
                    {
                        result = calculator.Compute(num1, num2, operation);

                        if (double.IsNaN(result))
                            Console.WriteLine("This operation will result in a matemathical error!");
                        else
                            Console.WriteLine("Result = " + result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error!" + e.Message);
                    }
                }
                else if (command == "2")
                {
                    Console.WriteLine("Calculator was used {0} time(s)!", calculator.OperationCount.ToString());
                }
                else if (command == "q")
                {
                    endProgram = true;
                    calculator.Finish();
                }

                Console.WriteLine("------------------------\n");
            }
        }
    }
}