namespace OnlineShop.Models.Products.Computers
{
    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Peripherals;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => this.components;

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals;

        public override double OverallPerformance => this.CalculateOverallPerformance();

        public override decimal Price =>
            this.Peripherals.Sum(x => x.Price) +
            this.Components.Sum(x => x.Price) +
            base.Price;


        public void AddComponent(IComponent component)
        {
            if (this.components.Any(x=>x.GetType() == component.GetType()))
            {
                string msg = string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name,
                    this.GetType().Name,
                    this.Id);

                throw new ArgumentException(msg);
            }

            this.components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.peripherals.Any(x => x.GetType() == peripheral.GetType()))
            {
                string msg = string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name,
                    this.GetType().Name,
                    this.Id);

                throw new ArgumentException(msg);
            }

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!this.components.Any(x => x.GetType().Name == componentType))
            {
                string msg = string.Format(ExceptionMessages.NotExistingComponent, componentType.GetType().Name,
                    this.GetType().Name,
                    this.Id);

                throw new ArgumentException(msg);
            }

            var component = this.components.FirstOrDefault(x => x.GetType().Name == componentType);
            this.components.Remove(component);

            return component;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!this.peripherals.Any(x => x.GetType().Name == peripheralType))
            {
                string msg = string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType.GetType().Name,
                    this.GetType().Name,
                    this.Id);

                throw new ArgumentException(msg);
            }

            var peripheral = this.peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheral);

            return peripheral;
        }

        private double CalculateOverallPerformance()
        {
            if (this.components.Count == 0)
            {
                return base.OverallPerformance;
            }

            var result = base.OverallPerformance + this.Components.Average(x => x.OverallPerformance);

            return result;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({this.components.Count}):");

            foreach (var component in components)
            {
                sb.AppendLine($"  {component.ToString()}");
            }

            string averageResult = this.Peripherals.Count == 0 ? "0.00" :
                this.Peripherals.Average(x => x.OverallPerformance).ToString("f2");

            sb.AppendLine($" Peripherals ({this.peripherals.Count}); Average Overall Performance ({averageResult}):");

            foreach (var peripheral in this.peripherals)
            {
                sb.AppendLine($"  {peripheral.ToString()}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
