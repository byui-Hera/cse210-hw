/// My exceeding the requirements portion stems from the design and implementation of the code. 
// - I have implemented the ability to save to multiple files and change the save location multiple times within a single session while persisting the location across program instances. 
// - Unsaved entries are checked upon loading, and the user is prompted on how to handle them.
// - I've added the option for the user to choose whether they’d like a writing prompt when adding an entry, and the program begins with the WriteEntry method to encourage engagement.
// - I’ve added five additional prompts to create more diverse and meaningful entries.
// - The program supports both relative and absolute paths, giving users flexibility over where and how journal files are saved.
// - A title prompt for journal entries ensures that entries are not static and allows for more personalized organization.
// - Finally, I’ve added a check to prevent overwriting files by appending a counter `(n)` to the filename if the file already exists, ensuring each file has a unique name.

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