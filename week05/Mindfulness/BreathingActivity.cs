public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        name = "Breathing Activity";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void RunActivity()
    {
        int timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Console.WriteLine("Breathe in...");
            Countdown(4);
            timeElapsed += 4;

            if (timeElapsed >= duration) break;

            Console.WriteLine("Breathe out...");
            Countdown(6);
            timeElapsed += 6;
        }
    }
}
