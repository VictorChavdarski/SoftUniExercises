using CarDealer.Data;
using CarDealer.DataTransferObjects.Input;
using CarDealer.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //var supplierXml = File.ReadAllText("../../../Datasets/suppliers.xml");

            var partsXml = File.ReadAllText("../../../Datasets/parts.xml");

            var result = ImportParts(context, partsXml);

            Console.WriteLine(result);
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(PartInputModel[]), new XmlRootAttribute("Parts"));
            var textRead = new StringReader(inputXml);

            var partsDto = xmlSerializer
                .Deserialize(textRead) as PartInputModel[];

            var suppliersId = context.Suppliers.Select(x => x.Id).ToList();

            var parts = partsDto
                .Where(s => suppliersId.Contains(s.SupplierId))
                .Select(x => new Part
            {
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                SupplierId = x.SupplierId
            })
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";

        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(SupplierInputModel[]), new XmlRootAttribute("Supplier"));
            var textRead = new StringReader(inputXml);

            var suppliersDto = xmlSerializer
                .Deserialize(textRead) as SupplierInputModel[];

            var suppliers = suppliersDto.Select(x => new Supplier
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
                .ToList();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
              
            return $"Successfully imported {suppliers.Count}";
        }
    }
}