namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var mostCraziestAuthors = context.Authors
                .Select(x => new
                {
                    AuthorName = x.FirstName + " " + x.LastName,
                    Books = x.AuthorsBooks
                    .OrderByDescending(b => b.Book.Price)
                    .Select(b => new
                    {
                        BookName = b.Book.Name,
                        BookPrice = b.Book.Price.ToString("f2"),
                    })
                    .ToArray()
                })
                .ToArray()
                .OrderByDescending(c => c.Books.Length)
                .ThenBy(n => n.AuthorName)
                .ToArray();

            var json = JsonConvert.SerializeObject(mostCraziestAuthors, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var sb = new StringBuilder();

            ExportBookDto[] books = context.Books
                 .Where(b => b.PublishedOn < date && b.Genre == Genre.Science)
                 .ToArray()
                 .OrderByDescending(b => b.Pages)
                 .ThenByDescending(b => b.PublishedOn)
                 .Select(b => new ExportBookDto()
                 {
                     Name = b.Name,
                     Date = b.PublishedOn.ToString("d"),
                     Pages = b.Pages
                 })
                 .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportBookDto[]), new XmlRootAttribute("Books"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var stringWriter = new StringWriter(sb))
            {
                xmlSerializer.Serialize(stringWriter,books, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}