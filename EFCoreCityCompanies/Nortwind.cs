using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder

using static System.Console;

namespace Packt.Shared;

// this manages the connection to the database
public class Northwind : DbContext
{
    // these properties map to tables in the database 
    public DbSet<Customer>? Customers { get; set; }
    

    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder)
    {

        string connection = "Data Source=DESKTOP-6TT8T1O\\LAPCEVICSQL;" +
          "Initial Catalog=Northwind;" +
          "Integrated Security=true;" +
          "MultipleActiveResultSets=true;";

        optionsBuilder.UseSqlServer(connection);

    }

    protected override void OnModelCreating(
      ModelBuilder modelBuilder)
    {
        // example of using Fluent API instead of attributes
        // to limit the length of a category name to under 15
        modelBuilder.Entity<Customer>()
          .Property(customer => customer.CompanyName)
          .IsRequired() // NOT NULL
          .HasMaxLength(40);


        
    }
}

