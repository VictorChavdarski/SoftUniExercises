using System;
using WildFarm.Models.Contracts;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Models
{
    public abstract class Animal : ISoundProduceable
     {

        protected Animal(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; set; }
        public double Weight { get; set; }

        public int FoodEaten { get; set; }

        public abstract string ProduceSound();
       
    }
}
