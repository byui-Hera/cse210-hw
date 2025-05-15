/*
This program exceeds the core requirements in the following ways:
1. Additional prompts have been added to provide more variety and creativity for journal entries.
2. The program uses a structured format (| separator) for saving and loading entries, making it easy to parse and manage.
3. The program includes input validation for file names to prevent errors when saving or loading files.
4. The program provides clear feedback to the user for each action, improving the user experience.
5. The design follows the principle of abstraction by separating concerns into different classes (Entry, Journal, Program).
*/

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToFile();
                    break;
                case "4":
                    journal.LoadFromFile();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }
}