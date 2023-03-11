using System;
using System.Collections.Generic;
using System.IO.Pipes;

namespace Flashcards
{
    internal class UserInput
    {
        public static void GetUserInput()
        {
            bool closeApplication = false;

            while (!closeApplication)
            {
                Console.WriteLine("[1] Manage Stacks");
                Console.WriteLine("[2] Manage FlashCards");
                Console.WriteLine("[3] Study");
                Console.WriteLine("[4] View Study Sessions Data");
                Console.WriteLine("[0] Exit Program");

                string response = Console.ReadLine();

                switch (response)
                {
                    case "0":
                        {
                            closeApplication = true;
                            break;
                        }
                    case "1":
                        {
                            bool returnToMainMenu = false;

                            while (!returnToMainMenu)
                            {
                                //show the current stack

                                //show options
                                Console.WriteLine("  [1] Add new stack");
                                Console.WriteLine("  [2] Select a stack to edit");
                                Console.WriteLine("  [3] Remove stack");
                                Console.WriteLine("  [0] Return to main menu");

                                string res = Console.ReadLine();

                                switch (res)
                                {
                                    case "0":
                                        returnToMainMenu = true;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Command not recognized, try again!");
                            break;
                        }
                }
            }
        }
    }
}
