using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.Infrastructure; // GetService extension method
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction
using Packt.Shared;

using static System.Console;

CitiesWithCompanies();



 static void CitiesWithCompanies2()
{
    using (Northwind db = new())
    {

        IQueryable<Customer>? customers = db.Customers;

        /*grouping by name of city and creating anonimous objects with name of city and 
        name of companies in that city */
        var citiesWithCompanies = customers
                                  .GroupBy(
                                      x => x.City, 
                                      (key, customers) => new
                                      {
                                          City = key,
                                          Companies = customers.Select(x => x.CompanyName)
                                      });
        if ((customers is null) || (!customers.Any()))
        {
            WriteLine("Don't exist cities with companies.");
            return;
        }

        WriteLine("The cities with companies are : ");

        foreach (var x in citiesWithCompanies) {
        WriteLine(x.City);
        }

        /*
        WriteLine();
        foreach (var x in citiesWithCompanies)
        {
            WriteLine(x.City);
            foreach(var c in x.Companies)
            {
                WriteLine("   " + c);
            }
            WriteLine();
        }
        */

        WriteLine();
        WriteLine("Enter the name of the city : ");

        string city = ReadLine();

        if (city != null && citiesWithCompanies.Any(c => c.City.Equals(city)))
        {
            var cityCompanies = citiesWithCompanies.Where(x => x.City.Equals(city));

            WriteLine($"{city} is head office of companies:");
            foreach(var c in cityCompanies)
            {
                foreach(var x in c.Companies)
                {
                    WriteLine(x);
                }
            }
        }
        else
        {
            WriteLine("There is no match for your search.");
        }
    }
}

static void CitiesWithCompanies()
{
    using (Northwind db = new())
    {

        IQueryable<Customer>? customers = db.Customers;

        /*grouping by name of city and creating anonimous objects with name of city and 
        name of companies in that city */
        var citiesWithCompanies = customers?
                                  .GroupBy(
                                      x => x.City,
                                      (key, customers) => new
                                      {
                                          City = key,
                                          Companies = customers.Select(x => x.CompanyName)
                                      });

        try
        {
            if (citiesWithCompanies == null)
            {
                throw new Exception();
            }

            WriteLine("The cities with companies are : ");

            foreach (var x in citiesWithCompanies)
            {
                WriteLine($"{x.City} {x.Companies.Count()}");
            }

            WriteLine();
            WriteLine("Enter the name of the city : ");
            string? city = ReadLine();

            WriteLine();
            var Companies = citiesWithCompanies.Where(x => x.City == city).First().Companies;

            WriteLine($"{city} is head office of companies:");
            foreach (var x in Companies)
            {
                WriteLine(x);
            }
        }
        catch (Exception e)
        {
            WriteLine("There is no matched cities or companies.");
            Console.WriteLine("Error: {0}", e.Message);
        }
    }
}
