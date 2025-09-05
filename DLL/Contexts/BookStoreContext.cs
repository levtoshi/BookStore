using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delay> Delays { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<FullName> FullNames { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Producer> Produsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSignInInfo> UserSingInInfos { get; set; }
    }
}