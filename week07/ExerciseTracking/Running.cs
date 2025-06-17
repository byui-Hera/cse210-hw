using System;

public class Running : Activity
{
    private double _distance; // in kilometers

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / Duration) * 60;
    }

    public override double GetPace()
    {
        return Duration / _distance;
    }
}
