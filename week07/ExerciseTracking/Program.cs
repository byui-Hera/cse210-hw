using System;
using System.Collections.Generic;

public abstract class Activity
{
    // Common properties for all activities
    public DateTime Date { get; set; }
    public int Duration { get; set; } // in minutes

    public Activity(DateTime date, int duration)
    {
        Date = date;
        Duration = duration;
    }

    // Abstract methods to be overridden by derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Method to generate the summary (can be overridden by derived classes)
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {this.GetType().Name} ({Duration} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

public class Running : Activity
{
    private double distance; // in kilometers

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        // Speed in kph = (distance / time) * 60
        return (distance / Duration) * 60;
    }

    public override double GetPace()
    {
        // Pace in min per km = time / distance
        return Duration / distance;
    }
}

public class Cycling : Activity
{
    private double speed; // in kph

    public Cycling(DateTime date, int duration, double speed) : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        // Distance in km = (speed * time) / 60
        return (speed * Duration) / 60;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        // Pace in min per km = 60 / speed
        return 60 / speed;
    }
}

public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int duration, int laps) : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        // Distance in km = laps * 50 meters / 1000
        return laps * 50 / 1000.0;
    }

    public override double GetSpeed()
    {
        // Speed in kph = (distance / time) * 60
        return (GetDistance() / Duration) * 60;
    }

    public override double GetPace()
    {
        // Pace in min per km = time / distance
        return Duration / GetDistance();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Creating instances of each activity type
        Activity running = new Running(new DateTime(2022, 11, 3), 30, 5.0); // 5 km in 30 minutes
        Activity cycling = new Cycling(new DateTime(2022, 11, 3), 30, 20.0); // 20 kph for 30 minutes
        Activity swimming = new Swimming(new DateTime(2022, 11, 3), 30, 20); // 20 laps in 30 minutes

        // List of activities
        List<Activity> activities = new List<Activity> { running, cycling, swimming };

        // Displaying summaries for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
