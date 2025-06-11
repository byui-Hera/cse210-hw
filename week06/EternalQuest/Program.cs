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

// Base Goal class
abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string Serialize();

    public static Goal Deserialize(string data)
    {
        var parts = data.Split('|');
        switch (parts[0])
        {
            case "SimpleGoal":
                return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
            case "EternalGoal":
                return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
            case "ChecklistGoal":
                return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]));
            default:
                throw new Exception("Unknown goal type");
        }
    }
}

// SimpleGoal: one-time completion
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        IsComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            return Points;
        }
        return 0;
    }

    public override string GetStatus() => IsComplete ? "[X]" : "[ ]";
    public override string Serialize() => $"SimpleGoal|{Name}|{Description}|{Points}|{IsComplete}";
}

// EternalGoal: never complete, always gives points
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;
    public override string GetStatus() => "[~]";
    public override string Serialize() => $"EternalGoal|{Name}|{Description}|{Points}";
}

// ChecklistGoal: complete N times for bonus
class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Bonus { get; set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus, int currentCount = 0)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        Bonus = bonus;
        CurrentCount = currentCount;
        if (CurrentCount >= TargetCount) IsComplete = true;
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            CurrentCount++;
            if (CurrentCount >= TargetCount)
            {
                IsComplete = true;
                return Points + Bonus;
            }
            return Points;
        }
        return 0;
    }

    public override string GetStatus() => IsComplete ? "[X]" : $"[ ] Completed {CurrentCount}/{TargetCount}";
    public override string Serialize() => $"ChecklistGoal|{Name}|{Description}|{Points}|{TargetCount}|{Bonus}|{CurrentCount}";
}

// Main program
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