/// My exceeding the requirements portion stems from the design and implementation of the code. 
// - I have implemented the ability to save to multiple files and change the save location multiple times within a single session while persisting the location across program instances. 
// - Unsaved entries are checked upon loading, and the user is prompted on how to handle them.
// - I've added the option for the user to choose whether they’d like a writing prompt when adding an entry, and the program begins with the WriteEntry method to encourage engagement.
// - I’ve added five additional prompts to create more diverse and meaningful entries.
// - The program supports both relative and absolute paths, giving users flexibility over where and how journal files are saved.
// - A title prompt for journal entries ensures that entries are not static and allows for more personalized organization.
// - Finally, I’ve added a check to prevent overwriting files by appending a counter `(n)` to the filename if the file already exists, ensuring each file has a unique name.

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        journal.WriteEntry();

        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the current/loaded Journal entries");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");

            if (!string.IsNullOrEmpty(Journal.SaveLocation))
            {
                Console.WriteLine("6. Change Save Folder Location");
            }
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveJournal();
                    break;
                case "4":
                    Console.WriteLine("\nCurrent Save Folder: " + Journal.SaveLocation);
                    Console.WriteLine("Available files:");
                    if (Directory.Exists(Journal.SaveLocation))
                    {
                        string[] files = Directory.GetFiles(Journal.SaveLocation);
                        foreach (string file in files)
                        {
                            Console.WriteLine(Path.GetFileName(file));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failure: The Specified folder does not exist.");
                    }

                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.UnsavedEntriesCheck();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    journal.UnsavedEntriesCheck();
                    running = false;
                    break;
                case "6":
                    if (!string.IsNullOrEmpty(Journal.SaveLocation))
                    {
                        journal.SetSaveLocation();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}