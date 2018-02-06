namespace SuperShoes.Models
{
    using SuperShoes.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ApplicationContext()
            : base("name=DefaultConnection")
        {
        }
        
    }
}