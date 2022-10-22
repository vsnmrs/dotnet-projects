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
                Console.WriteLine("Choose an option: ");
                Console.WriteLine("\t 1: Use Calculator");
                Console.WriteLine("\t 2: See usage count");
                Console.WriteLine("\t 3: Show memory");
                Console.WriteLine("\t 4: Clear memory");
                Console.WriteLine("\t q: Quit program");

                string command = Console.ReadLine();

                if (command == "1")
                {
                    string firstOption, secondOption;
                    double num1 = 0;

                    Console.Write("Type a number and then press ENTER");
                    if (calculator.Memory.Count > 0)
                    {
                        Console.WriteLine(", or type m to select a result from memory!");

                        firstOption = Console.ReadLine();
                        
                        if (firstOption == "m")
                        {
                            num1 = GetValueFromMemory(calculator);
                        }
                        else
                        {
                            while (!double.TryParse(firstOption, out num1))
                            {
                                Console.WriteLine("The input is not valid. Enter a number value!");
                                firstOption = Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        firstOption = Console.ReadLine();

                        while (!double.TryParse(firstOption, out num1))
                        {
                            Console.WriteLine("The input is not valid. Enter a number value!");
                            firstOption = Console.ReadLine();
                        }
                    }

                    Console.WriteLine("Type another number to add the second operand, or choose an option:");
                    Console.WriteLine("\t r - Square Root");

                    if (calculator.Memory.Count > 0)
                    {
                        Console.WriteLine("\t m - Select a result from memory");

                        secondOption = Console.ReadLine();

                        double num2 = 0;

                        if (secondOption == "m")
                        {
                            num2 = GetValueFromMemory(calculator);
                        }
                        else
                        {
                            while (!double.TryParse(secondOption, out num2))
                            {
                                Console.WriteLine("The input is not valid. Enter a number value!");
                                secondOption = Console.ReadLine();
                            }
                        }

                        PerformCalculation(calculator, num1, num2);
                    }
                    else
                    {
                        secondOption = Console.ReadLine();

                        if (secondOption == "r")
                        {
                            try
                            {
                                double result = calculator.Compute(num1, secondOption);

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
                        else
                        {
                            double num2;
                            while (!double.TryParse(secondOption, out num2))
                            {
                                Console.WriteLine("The input is not valid. Enter a number value!");
                                secondOption = Console.ReadLine();
                            }

                            PerformCalculation(calculator, num1, num2);
                        }
                    }
                }
                else if (command == "2")
                {
                    Console.WriteLine("Calculator was used {0} time(s)!", calculator.OperationCount.ToString());
                }
                else if (command == "3")
                {
                    calculator.PrintMemory();
                }
                else if (command == "4")
                {
                    calculator.ClearMemory();
                }
                else if (command == "q")
                {
                    endProgram = true;
                    calculator.Finish();
                }

                Console.WriteLine("------------------------\n");
            }
        }

        private static double GetValueFromMemory(Calculator calculator)
        {
            double result = 0;
            Console.WriteLine("Select the number coresponding to the value you want to use");
            calculator.PrintMemory();

            string memIndex = Console.ReadLine();
            int index;

            while (!int.TryParse(memIndex, out index) || (index <= 0 || index > calculator.Memory.Count))
            {
                Console.WriteLine("Incorrect format or selection is out of range, try again!");
                memIndex = Console.ReadLine();
            }

            //cannot get the element from a random index in a queue so we have to convert it to an array then to a list
            result = calculator.Memory.ToArray().ToList()[index - 1].Value;
            Console.WriteLine("Value {0} was selected", result);

            return result;
        }

        private static void PerformCalculation(Calculator calculator, double num1, double num2)
        {
            double result;

            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\t a - Add");
            Console.WriteLine("\t s - Substract");
            Console.WriteLine("\t m - Multiply");
            Console.WriteLine("\t d - Divide");
            Console.WriteLine("\t p - Power");
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
    }
}