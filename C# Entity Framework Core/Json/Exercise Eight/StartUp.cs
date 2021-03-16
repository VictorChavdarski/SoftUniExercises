using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        static IMapper mapper;

        public static void Main(string[] args)
        {
            var productShopContext = new ProductShopContext();
            productShopContext.Database.EnsureDeleted();
            productShopContext.Database.EnsureCreated();


            //string usersJson = File.ReadAllText("../../../Datasets/users.json");
            //string productsJson = File.ReadAllText("../../../Datasets/products.json");
            //string categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            //string categoriesProductsJson = File.ReadAllText("../../../Datasets/categories-products.json");

            //ImportUsers(productShopContext, usersJson);
            //ImportProducts(productShopContext, productsJson);
            //ImportCategories(productShopContext, categoriesJson);
            //ImportCategoryProducts(productShopContext, categoriesProductsJson);

            //GetProductsInRange(productShopContext);

            //GetSoldProducts(productShopContext);

            //GetCategoriesByProductsCount(productShopContext);

            var result = GetUsersWithProducts(productShopContext);

            Console.WriteLine(result);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(x => x.ProductsSold.Any(b => b.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Where(x => x.BuyerId != null).Count(),
                        products = u.ProductsSold.Where(x => x.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                })
                .OrderByDescending(x => x.soldProducts.products.Count())
                .ToList();

            var resultObject = new
            {
                usersCount = context.Users.Where(x => x.ProductsSold.Any(b => b.BuyerId != null)).Count(),
                users = users
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var resultJson = JsonConvert.SerializeObject(resultObject, Formatting.Indented, jsonSerializerSettings);

            return resultJson;

        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {

            var categoriesInfo = context.Categories.Select(x => new
            {
                category = x.Name,
                productsCount = x.CategoryProducts.Count,
                averagePrice = x.CategoryProducts.Count == 0 ? 0.ToString("f2") : x.CategoryProducts.Average(p => p.Product.Price).ToString("f2"),
                totalRevenue = x.CategoryProducts.Sum(p => p.Product.Price).ToString("f2")
            })
                .OrderByDescending(x => x.productsCount)
                .ToList();

            var result = JsonConvert.SerializeObject(categoriesInfo);

            return result;

        }


        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = user.ProductsSold.Where(p => p.BuyerId != null)
                                       .Select(x => new
                                       {
                                           name = x.Name,
                                           price = x.Price,
                                           buyerFirstName = x.Buyer.FirstName,
                                           buyerLastName = x.Buyer.LastName
                                       })
                                       .ToList()
                })
                .OrderBy(x => x.firstName)
                .ThenBy(x => x.lastName)
                .ToList();

            var result = JsonConvert.SerializeObject(users);

            return result;
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                                  .Where(p => p.Price >= 500 && p.Price <= 1000)
                                  .Select(product => new
                                  {
                                      name = product.Name,
                                      price = product.Price,
                                      seller = product.Seller.FirstName + " " + product.Seller.LastName
                                  })
                                  .OrderBy(x => x.price)
                                  .ToList();

            var result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoCategoriesProducts = JsonConvert.DeserializeObject<IEnumerable<CategoryProductInputModel>>(inputJson);

            var categoryProducts = mapper.Map<IEnumerable<CategoryProduct>>(dtoCategoriesProducts);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";


        }


        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoCategories = JsonConvert
                .DeserializeObject<IEnumerable<CategoryInputModel>>(inputJson)
                .Where(c => c.Name != null)
                .ToList();

            var categories = mapper.Map<IEnumerable<Category>>(dtoCategories);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";

        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoProducts = JsonConvert.DeserializeObject<IEnumerable<ProductInputModel>>(inputJson);

            var products = mapper.Map<IEnumerable<Product>>(dtoProducts);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";

        }


        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            InitializeAutoMapper();

            var dtoUsers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel>>(inputJson);

            var users = mapper.Map<IEnumerable<User>>(dtoUsers);
            
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        private static void InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            mapper = config.CreateMapper();
        }

    }
}