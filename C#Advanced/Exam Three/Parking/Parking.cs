using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private readonly List<Car> data;

        public Parking()
        {
            this.data = new List<Car>();
        }
        public Parking(string type, int capacity)
            : this()
        {
            this.Type = type;
            this.Capacity = capacity;
        }

        public int Count => this.data.Count;
        public string Type { get; set; }
        public int Capacity { get; set; }

        public void Add(Car car)
        {
            if (this.data.Count + 1 <= this.Capacity)
            {
                this.data.Add(car);
            }
        }

        public bool Remove(string manufacturer, string model)
        {
            Car car = this.data.FirstOrDefault(x => x.Manufacturer == manufacturer && x.Model == model);

            if (car != null)
            {
                this.data.Remove(car);
                return true;
            }
            return false;
        }

        public Car GetLatestCar()
        {
            Car car = this.data.OrderByDescending(x => x.Year).FirstOrDefault();
            if (this.data.Count > 0)
            {
                return car;
            }
            return null;
        }

        public Car GetCar(string manufacturer, string model)
        {
            Car car = this.data.FirstOrDefault(x => x.Manufacturer == manufacturer && x.Model == model);
            if (car != null)
            {
                return car;
            }
            return null;
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The cars are parked in {this.Type}:");
            foreach (Car car in this.data)
            {
                sb.AppendLine(car.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
