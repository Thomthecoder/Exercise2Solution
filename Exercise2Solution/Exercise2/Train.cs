namespace TrainSystem
{
    internal class Train
    {
        public Engine Engine { get; private set; }
        public int MaxGrossWeight => Engine.Horsepower * 2000;
        public int GrossWeight => RailCars.Sum(rc => rc.GrossWeight) + Engine.Weight;
        public List<RailCar> RailCars { get; private set; } = new List<RailCar>();
        public int TotalCars => RailCars.Count;

        public Train(Engine engine)
        {
            if (engine == null)
                throw new ArgumentNullException(nameof(engine), "Engine cannot be null.");

            Engine = engine;
        }

        public void AddCar(RailCar car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car), "RailCar required.");
            if (RailCars.Any(rc => rc.SerialNumber == car.SerialNumber))
                throw new ArgumentException($"RailCar with serial number {car.SerialNumber} already exists.");
            if (GrossWeight + car.GrossWeight > MaxGrossWeight)
                throw new ArgumentException($"Adding this car would exceed the maximum gross weight limit of {MaxGrossWeight} pounds.");

            RailCars.Add(car);
        }

        public RailCar DetachCar(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new ArgumentNullException(nameof(serialNumber), "SerialNumber required.");

            var car = RailCars.FirstOrDefault(rc => rc.SerialNumber == serialNumber);
            if (car == null)
                throw new ArgumentException($"RailCar with serial number {serialNumber} not found.");

            RailCars.Remove(car);
            return car;
        }
    }
}
