namespace RealEstates.Services
{
    using RealEstates.Data;
    using RealEstates.Models;
    using RealEstates.Services.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PropertiesService : IPropertiesService
    {
        private readonly ApplicationDbContext dbContext;

        public PropertiesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(string district, int floor, int totalFloors,
                int size, int yardSize, int year, 
                string propertyType, string buildingType, int price)
        {
            var property = new Property
            {
                Size = size,
                Price = price <= 0 ? null : price,
                Floor = floor <= 0 || floor > 255 ? null : (byte)floor,
                TotalFloors = totalFloors <= 0 || totalFloors > 255 ? null : (byte)totalFloors,
                YardSize = yardSize <= 0 ? null : yardSize,
                Year = year <= 1800 ? null : year
            };

            var dbDistrict = dbContext.Districts.FirstOrDefault(x => x.Name == district);
            if (dbDistrict == null)
            {
                dbDistrict = new District { Name = district };
            }

            property.District = dbDistrict;

            var dbPropertyType = dbContext.PropertyTypes.FirstOrDefault(x => x.Type == propertyType);
            if (dbPropertyType == null)
            {
                dbPropertyType = new PropertyType { Type = propertyType };
            }

            property.Type = dbPropertyType;

            var dbBuildingType = dbContext.BuildingTypes.FirstOrDefault(x => x.Type == buildingType);
            if (dbBuildingType == null)
            {
                dbBuildingType = new BuildingType { Type = buildingType };
            }

            property.BuildingType = dbBuildingType;

            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
        }

        public decimal AveragePricePerSquareMeter()
        {
            return dbContext.Properties.Where(x => x.Price.HasValue)
                .Average(x => x.Price / (decimal)x.Size) ?? 0;
        }

        public IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize)
        {
            var properties = dbContext.Properties
                 .Where(x => x.Price >= minPrice && x.Price <= maxPrice && x.Size >= minSize && x.Size <= maxSize)
                 .Select(x => new PropertyInfoDto
                 {
                    Size = x.Size,
                    Price = x.Price ?? 0,
                    BuildingType = x.BuildingType.Type,
                    DistrictName = x.District.Name,
                    PropertyType = x.Type.Type

                 })
                 .ToList();

            return properties;
        }
    }
}
