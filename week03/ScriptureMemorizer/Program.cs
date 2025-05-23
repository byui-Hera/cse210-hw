// Above expectation, added a Program and Hider classes, 2 additional scriptures and random picking of scriptures
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me.")
        };

        Random random = new Random();
        Scripture selectedScripture = scriptures[random.Next(scriptures.Count)];
        Hider hider = new Hider(selectedScripture);

        while (true)
        {
            Console.Clear();
            hider.DisplayScripture();
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;
            hider.HideRandomWords();
            if (hider.AllWordsHidden())
                break;
        }
    }
}

class Scripture
{
    public Reference Reference { get; }
    public List<Word> Words { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int EndVerse { get; }

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        if (StartVerse == EndVerse)
            return $"{Book} {Chapter}:{StartVerse}";
        else
            return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? "_" : Text;
    }
}

class Hider
{
    private Scripture scripture;
    private Random random;

    public Hider(Scripture scripture)
    {
        this.scripture = scripture;
        random = new Random();
    }

    public void DisplayScripture()
    {
        Console.WriteLine(scripture.Reference);
        foreach (var word in scripture.Words)
        {
            Console.Write(word + " ");
        }
    }

    public void HideRandomWords()
    {
        int wordsToHide = random.Next(1, 4); // Hide 1 to 3 words at a time
        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(scripture.Words.Count);
            scripture.Words[index].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return scripture.Words.All(word => word.IsHidden);
    }
}