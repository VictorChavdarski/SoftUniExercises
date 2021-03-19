﻿namespace RealEstates.Models
{
    using System.Collections.Generic;
    public class PropertyType
    {
        public PropertyType()
        {
            this.Properties = new HashSet<Property>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

    }
}
