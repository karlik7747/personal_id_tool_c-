using System;
using System.Collections.Generic;
using IDTools;

namespace personal_id_tool
{
    static class MainClass
    {
        private static void Menu()
        {
            Console.WriteLine("Select operation:\n 1. Find IDs\n 2. Generate IDs\n 3. Exit");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter text:");
                    string text = Console.ReadLine();
                    List<string> correctCodes = LithuanianIDTools.GetCorrectCodes(text);
                    foreach (string code in correctCodes)
                        Console.WriteLine(code);
                    break;
                case "2":
                    Console.WriteLine("How many codes do you want to generate?");
                    int numberOfIDs;
                    if (int.TryParse(Console.ReadLine(), out numberOfIDs))
                        for (int i = 0; i < numberOfIDs; i++)
                            Console.WriteLine(LithuanianIDTools.GenerateValidID());
                    else
                        Console.WriteLine("Bad value!");
                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again");
                    break;
            }
        }
        public static void Main(string[] args)
        {
            while (true)
                Menu();
        }
    }
}
