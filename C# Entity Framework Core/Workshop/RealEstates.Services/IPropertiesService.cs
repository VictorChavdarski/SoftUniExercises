﻿namespace RealEstates.Services
{
    using RealEstates.Services.Models;
    using System.Collections.Generic;
    public interface IPropertiesService
    {
        void Add(string district, int floor,
                 int totalFloors, int size,
                 int yardSize, int year,
                 string propertyType, string buildingType, int price);

        decimal AveragePricePerSquareMeter();

        IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize);
    }
}
