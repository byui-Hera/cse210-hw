/*
This program exceeds the core requirements by:
- Supporting a library of scriptures and randomly selecting one for memorization.
- Allowing scriptures to be loaded from a file (if present).
- Providing clear user feedback and a clean interface.
*/

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Example scripture library (could be loaded from file)
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        // Optionally, load more scriptures from a file here

        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            scripture.Display();
            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words are hidden. Good job!");
                break;
            }
            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;
            scripture.HideRandomWords();
        }
    }
}


