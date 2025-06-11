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