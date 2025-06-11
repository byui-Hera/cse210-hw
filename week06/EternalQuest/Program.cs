// ===============================================================
// Eternal Quest - Exceeds Requirements
// - Implements robust file save/load with progress persistence.
// - Uses a flexible serialization/deserialization system for goals.
// - User-friendly menu with clear prompts and error handling.
// - Easily extendable for new goal types.
// - (Add any other creative features you included here.)
// ===============================================================
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        ShowTitleScreen();
        Load();
        while (true)
        {
            Console.WriteLine($"\nScore: {score}");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": Save(); break;
                case "5": Load(); break;
                case "6": Save(); return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void ShowTitleScreen()
    {
        Console.Clear();
        Console.WriteLine("===============================================");
        Console.WriteLine("           Eternal Quest: Demo");
        Console.WriteLine("                 ...");
        Console.WriteLine("             CSE 210");
        Console.WriteLine("  Brigham Young University-Idaho");
        Console.WriteLine("===============================================");
        Console.WriteLine();
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type: 1) Simple 2) Eternal 3) Checklist");
        string type = Console.ReadLine();
        Console.Write("Name: "); string name = Console.ReadLine();
        Console.Write("Description: "); string desc = Console.ReadLine();
        Console.Write("Points: "); int pts = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, desc, pts));
                break;
            case "2":
                goals.Add(new EternalGoal(name, desc, pts));
                break;
            case "3":
                Console.Write("Target count: "); int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus: "); int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, desc, pts, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid type.");
                break;
        }
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} {goals[i].Name} ({goals[i].Description})");
        }
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to record.");
            return;
        }
        ListGoals();
        Console.Write("Which goal did you accomplish? ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= goals.Count)
        {
            int pts = goals[idx - 1].RecordEvent();
            score += pts;
            Console.WriteLine($"You earned {pts} points!");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    static void Save()
    {
        using (StreamWriter sw = new StreamWriter("goals.txt"))
        {
            sw.WriteLine(score);
            foreach (var goal in goals)
                sw.WriteLine(goal.Serialize());
        }
        Console.WriteLine("Progress saved.");
    }

    static void Load()
    {
        if (File.Exists("goals.txt"))
        {
            var lines = File.ReadAllLines("goals.txt");
            if (lines.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(lines[0]))
                    score = int.Parse(lines[0]);
                else
                    score = 0;
                goals.Clear();
                for (int i = 1; i < lines.Length; i++)
                    goals.Add(Goal.Deserialize(lines[i]));
            }
        }
    }
}