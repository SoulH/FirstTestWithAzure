using SuperShoesModels.Entities;
using System.Data.Entity;

namespace SuperShoesApi.Models
{
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