using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        private readonly List<Pet> data;
        private Clinic()
        {
            this.data = new List<Pet>();
        }
        public Clinic(int capacity)
            :this()
        {
            this.Capacity = capacity;
        }
        public int Capacity { get; set; }

        public int Count => this.data.Count;

        public void Add(Pet pet)
        {
            if (this.data.Count+1<=this.Capacity)
            {
                this.data.Add(pet);
            }
        }
        public bool Remove(string name)
        {
            Pet pet = this.data.FirstOrDefault(x => x.Name == name);
            if (pet!=null)
            {
                this.data.Remove(pet);
                return true;
            }
            return false;
        }
        public Pet GetPet(string name,string owner)
        {
            Pet pet = this.data.FirstOrDefault(x => x.Name == name && x.Owner == owner);
            if (pet!=null)
            {
                return pet;
            }
            return null;
        }
        public Pet GetOldestPet()
        {
            Pet pet = this.data.OrderByDescending(x => x.Age).FirstOrDefault();
            return pet;
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The clinic has the following patients:");
            foreach (Pet pet in this.data)
            {
                sb.AppendLine($"Pet {pet.Name} with owner: {pet.Owner}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
