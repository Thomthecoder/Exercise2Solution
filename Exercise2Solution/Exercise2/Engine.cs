namespace TrainSystem
{
    public class Engine
{
    public int Horsepower { get; }
    public int Weight { get; }

    public Engine(int horsepower, int weight)
    {
        if (horsepower <= 0 || weight <= 0)
            throw new ArgumentOutOfRangeException("Horsepower and weight must be positive and non-zero.");

        Horsepower = horsepower;
        Weight = weight;
    }
}
}