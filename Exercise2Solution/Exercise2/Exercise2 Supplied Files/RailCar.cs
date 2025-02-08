using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSystem
{
    public class RailCar
    {
        // Backing fields for properties
        private string _serialNumber;   // Backing field for the SerialNumber property
        private int _lightWeight;       // Backing field for the LightWeightr property
        private int _capacity;          // Backing field for Capacity property
        private int _loadLimit;         // Backing field for LoadLimit property
        //private RailCarType _type;      // Backing field for Type property

        /// <summary>
        /// Uniquely identifies a specific car; may include characters and digits in the number. 
        /// Once set, cannot be altered.
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("RailCar Serial Number not specified or is empty");
                }
                _serialNumber = value.Trim();
            }
        }

        /// <summary>
        /// The weight of the railcar in pounds when empty.
        /// </summary>
        public int LightWeight
        {
            get
            {
                return _lightWeight;
            }
            private set
            {
                if (!Utilities.IsPositiveNonZero(value))
                {
                    throw new ArgumentException($"RailCar Light Weight {value} cannot be negative");
                }
                if (!Utilities.InHundreds(value))
                {
                    throw new ArgumentException($"RailCar Light Weight {value} must be in 100 pound units (eg 147700)");
                }
                _lightWeight = value;
            }
        }

        /// <summary>
        /// Standard maximum Net Weight in pounds. 
        /// This is the "ballpark" figure used when loading a railcar. 
        /// The actual scale weight may be slightly higher or lower for what is considered a "full" load.
        /// 
        /// Capacity is always less than the Load Limit.
        /// </summary>
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            private set
            {
                if (!Utilities.IsPositiveNonZero(value))
                {
                    throw new ArgumentNullException("RailCar Capacity cannot be negative");
                }
                if (!Utilities.InHundreds(value))
                {
                    throw new ArgumentNullException("RailCar Capacity must be in 100 pound units (eg 147700)");
                }
                if (value >= LoadLimit)
                {
                    throw new ArgumentNullException("RailCar Capacity must be less than Load Limit");
                }
                _capacity = value;
            }
        }

        /// <summary>
        /// Absolute maximum Net Weight in pounds allowed for safety purposes. 
        /// This does not include the Light Weight (weight of empty railcar). 
        /// Exceeding this weight makes the railcar unsafe.
        /// </summary>
        public int LoadLimit
        {
            get
            {
                return _loadLimit;
            }
            private set
            {
                if (!Utilities.IsPositiveNonZero(value))
                {
                    throw new ArgumentException($"RailCar Load Limit {value} cannot be negative");
                }
                if (!Utilities.InHundreds(value))
                {
                    throw new ArgumentException($"RailCar Load Limit {value} must be in 100 pound units (eg 147700)");
                }
                _loadLimit = value;
            }
        }

        /// <summary>
        /// Value that represents the type of this car. 
        /// Once set, cannot be altered.
        /// </summary>
        public RailCarType Type { get; private set; }

        /// <summary>
        /// Value that represents whether the car is inservice or not.
        /// </summary>
        public bool Inservice { get; set; }

        /// <summary>
        /// The weight of the freight and the railcar.
        /// </summary>
        public int GrossWeight { get; private set; }

        /// <summary>
        /// The weight of the freight only that does not include the Light Weight.
        /// </summary>
        public int NetWeight { get { return GrossWeight - LightWeight; } }

        /// <summary>
        /// Any weight within 90% of the Capacity is considered as a "full load".
        /// </summary>
        public bool IsFull
        {
            get { return (double)NetWeight > (Capacity * .9); }
        }

        /// <summary>
        /// The railcar is defaulted to inservice when created. 
        /// </summary>
        public RailCar(
            string serialnumber,
            int lightweight,
            int capacity,
            int loadlimit,
            RailCarType type)
        {
            SerialNumber = serialnumber;
            LightWeight = lightweight;
            LoadLimit = loadlimit;
            Capacity = capacity;
            Type = type;
            Inservice = true;
            GrossWeight = LightWeight;
        }

        /// <summary>
        /// When recording scale weights, the gross weight is given; 
        /// this is the actual weight of the RailCar with its load. 
        /// This value must be between the Light Weight and the gross Load Limit (LoadLimit + LightWeight). 
        /// If the gross weight supplied is less than the Light Weight, 
        /// an error message is issued begining with the phrase "Scale Error -". 
        /// If the weight is over the safe limit (LoadLimit + LightWeight), 
        /// an error message is issued begining with "Unsafe Load -".
        /// </summary>
        public void RecordScaleWeight(int grossweight)
        {
            if (!Utilities.IsPositiveNonZero(grossweight))
            {
                throw new ArgumentException($"Gross weight {grossweight} must be positive");
            }
            if (grossweight < LightWeight)
            {
                throw new ArgumentException($"Scale Error: Gross weight {grossweight} must be more than {LightWeight}");

            }
            if (grossweight > (LoadLimit + LightWeight))
            {
                throw new ArgumentException($"Unsafe Load: Gross weight {grossweight} must be less than {LightWeight + LoadLimit}");

            }
            GrossWeight = grossweight;
        }

        /// <summary>
        /// Return the instance values in a comma separated value string of the stored data.
        /// </summary>
        public override string ToString()
        {
            return $"{SerialNumber},{LightWeight},{Capacity},{LoadLimit},{Type},{Inservice}";
        }
    }
}