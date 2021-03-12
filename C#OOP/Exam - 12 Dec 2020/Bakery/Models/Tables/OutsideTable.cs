using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        private const decimal INITIAL_PRICE_PER_PERSON = 3.50m;
        public OutsideTable(int tableNumber, int capacity) 
            : base(tableNumber, capacity, INITIAL_PRICE_PER_PERSON)
        {
        }

        public override decimal GetBill()
        {
            return base.GetBill() + this.NumberOfPeople * INITIAL_PRICE_PER_PERSON;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {base.TableNumber}");
            sb.AppendLine($"Type: {base.GetType().Name}");
            sb.AppendLine($"Capacity: {base.Capacity}");
            sb.AppendLine($"Price per Person: {INITIAL_PRICE_PER_PERSON}");

            return sb.ToString().TrimEnd();
        }
    }
}
