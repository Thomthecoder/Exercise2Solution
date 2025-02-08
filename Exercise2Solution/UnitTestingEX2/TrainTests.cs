using FluentAssertions;
using Xunit;

namespace UnitTestingEX2
{
    public class TrainTests
    {
        public RailCarType RailCarType { get; private set; }

        [Fact]
        public void Train_CreatedWithEngine_Success()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);

            train.Engine.Should().Be(engine);
        }

        [Fact]
        public void Train_NoEngine_ThrowsArgumentNullException()
        {
            Action act = () => new Train(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MaxGrossWeight_CalculatesCorrectly()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);

            train.MaxGrossWeight.Should().Be(4400 * 2000);
        }

        [Fact]
        public void AddCar_FirstCar_Success()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);
            var car = new RailCar("123", 10000, 50000, 60000, RailCarType.BOX_CAR);

            train.AddCar(car);

            train.TotalCars.Should().Be(1);
        }

        [Fact]
        public void AddCar_NoCar_ThrowsArgumentNullException()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);

            Action act = () => train.AddCar(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("*RailCar required*");
        }

        [Fact]
        public void AddCar_DuplicateSerialNumber_ThrowsArgumentException()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);
            var car1 = new RailCar("123", 10000, 50000, 60000, RailCarType.BOX_CAR);
            var car2 = new RailCar("123", 15000, 55000, 65000, RailCarType.COAL_CAR);

            train.AddCar(car1);
            Action act = () => train.AddCar(car2);
            act.Should().Throw<ArgumentException>().WithMessage("*123*");
        }

        [Fact]
        public void AddCar_ExceedsMaxGrossWeight_ThrowsArgumentException()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);
            var car = new RailCar("123", 10000, 50000, 60000, RailCarType.BOX_CAR);
            car.ReconScaleWeight(4400 * 2000);

            Action act = () => train.AddCar(car);
            act.Should().Throw<ArgumentException>().WithMessage("*exceed*");
        }

        [Fact]
        public void DetachCar_Success()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);
            var car1 = new RailCar("123", 10000, 50000, 60000, RailCarType.BOX_CAR);
            var car2 = new RailCar("456", 15000, 55000, 65000, RailCarType.COAL_CAR);

            train.AddCar(car1);
            train.AddCar(car2);

            var detachedCar = train.DetachCar("123");

            detachedCar.Should().Be(car1);
            train.TotalCars.Should().Be(1);
        }

        [Fact]
        public void DetachCar_NoSerialNumber_ThrowsArgumentNullException()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);

            Action act = () => train.DetachCar(null);
            act.Should().Throw<ArgumentNullException>().WithMessage("*SerialNumber required*");
        }

        [Fact]
        public void DetachCar_SerialNumberNotFound_ThrowsArgumentException()
        {
            var engine = new Engine(4400, 200000);
            var train = new Train(engine);
            var car = new RailCar("123", 10000, 50000, 60000, RailCarType.BOX_CAR);

            train.AddCar(car);

            Action act = () => train.DetachCar("456");
            act.Should().Throw<ArgumentException>().WithMessage("*456*");
        }
    }
}
