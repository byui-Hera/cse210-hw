using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Activity running = new Running(new DateTime(2022, 11, 3), 30, 5.0);
        Activity cycling = new Cycling(new DateTime(2022, 11, 3), 30, 20.0);
        Activity swimming = new Swimming(new DateTime(2022, 11, 3), 30, 20);

        List<Activity> activities = new List<Activity> { running, cycling, swimming };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
