﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Course.Entities;

namespace Course
{
    class Program
    {

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2};
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1};
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1};

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1},
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2},
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1},
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2},
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3},
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3},
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2},
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category= c3},
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1}
            };


            var r1 = products.Where(product => product.Category.Tier == 1 && product.Price < 900.0);
            Print("Tier 1 and Price < 900: ", r1);

            var r2 = products.Where(product => product.Category.Name == "Tools").Select(product => product.Name);
            Print("Names of products from tools: ", r2);

            var r3 = products.Where(product => product.Name[0] == 'C').Select(product => new { product.Name, product.Price, CategoryName = product.Category.Name});
            Print("Names Started With 'C' AND ANONYMOUS OBJECT", r3);

            var r4 = products.Where(product => product.Category.Tier == 1).OrderBy(product => product.Price).ThenBy(product => product.Name);
            Print("Tier 1 Order By Price Then By Name ", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("Tier 1 Order By Price Then By Name Skip 2 Take 4", r5);

            Console.WriteLine();

            var r6 = products.First();
            Console.WriteLine($"First test: {r6}");

            Console.WriteLine();

            var r7 = products.Where(p => p.Price > 3000).FirstOrDefault();
            Console.WriteLine($"First test (FirstOrDefault): {r7}");

            Console.WriteLine();

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine($"Single or Default {r8}");

            Console.WriteLine();

            var r9 = products.Where(p => p.Id == 243).SingleOrDefault();
            Console.WriteLine($"Single or Default {r9}");

            Console.WriteLine();

            var r10 = products.Max(product => product.Price);
            Console.WriteLine($"Max Price: {r10}");

            Console.WriteLine();

            var r11 = products.Min(product => product.Price);
            Console.WriteLine($"Min Price: {r11}");

            Console.WriteLine();

            var r12 = products.Where(product => product.Category.Id == 1).Sum(product => product.Price);
            Console.WriteLine($"Category 1 Sum prices: {r12}");

            Console.WriteLine();

            var r13 = products.Where(product => product.Category.Id == 1).Average(product => product.Price);
            Console.WriteLine($"Category 1 Average price: {r13}");

            Console.WriteLine();

            var r14 = products.Where(product => product.Category.Id == 5).Select(product => product.Price).DefaultIfEmpty(0).Average();
            Console.WriteLine($"Category 5 Average price: {r14}");

            Console.WriteLine();

            var r15 = products.Where(product => product.Category.Id == 1)
                .Select(product => product.Price)
                .Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine($"Category 1 aggregate sum: {r15}");

            Console.WriteLine();

            var r16 = products.GroupBy(product => product.Category);
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine($"Category: {group.Key.Name}: ");
                foreach (Product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}