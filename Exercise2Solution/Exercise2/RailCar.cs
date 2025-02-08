using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSystem
{
    public class RailCar
    {
        public string SerialNumber { get; }
        public int LightWeight { get; set; }
        public int Capacity { get; set; }
        public int LoadLimit { get; set; }
        public RailCarType Type { get; }
        public bool InService { get; set; }

        public RailCar(string serialNumber, int lightWeight, int capacity, int loadLimit, RailCarType type)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new ArgumentNullException(nameof(serialNumber), "Serial number cannot be null or empty.");
            if (lightWeight <= 0 || capacity <= 0 || loadLimit <= 0)
                throw new ArgumentOutOfRangeException("Weights must be positive and non-zero.");
            if (capacity >= loadLimit)
                throw new ArgumentException("Capacity must be less than Load Limit.");

            SerialNumber = serialNumber;
            LightWeight = lightWeight;
            Capacity = capacity;
            LoadLimit = loadLimit;
            Type = type;
            InService = true;
        }

        public bool IsFull => NetWeight >= 0.9 * Capacity;

        public int NetWeight => GrossWeight - LightWeight;

        private int _grossWeight;
        public int GrossWeight
        {
            get => _grossWeight;
            set
            {
                if (value < LightWeight)
                    throw new ArgumentException("Scale Error - Gross weight cannot be less than Light Weight.");
                if (value > LoadLimit + LightWeight)
                    throw new ArgumentException("Unsafe Load - Gross weight exceeds safe limit.");

                _grossWeight = value;
            }
        }

        public void ReconScaleWeight(int grossWeight)
        {
            GrossWeight = grossWeight;
        }

        public override string ToString()
        {
            return $"{SerialNumber},{LightWeight},{Capacity},{LoadLimit},{Type},{InService}";
        }
    }
}
