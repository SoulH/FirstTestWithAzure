namespace SuperShoes.Migrations
{
    using SuperShoes.Models;
    using System.Data.Entity.Migrations;
    using Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationContext context)
        {
            context.Stores.AddOrUpdate<Store>(r => r.Id,
                new Store() { Id = 1, Name = "Super Store 1", Address = "Somewhere over the rainbow" },
                new Store() { Id = 2, Name = "Super Store 2", Address = "Somewhere over the rainbow" },
                new Store() { Id = 3, Name = "Super Store 3", Address = "Somewhere over the rainbow" },
                new Store() { Id = 4, Name = "Super Store 4", Address = "Somewhere over the rainbow" }
            );

            context.Articles.AddOrUpdate<Article>(r => r.Id,
                new Article() { Id = 1, Name = "green shoes", Description = "The best quality of shoes in a green color", Price = (decimal)20.15, Total_in_shelf = 25, Total_in_vault = 40, StoreId = 1 },
                new Article() { Id = 2, Name = "yellow shoes", Description = "The best quality of shoes in a yellow color", Price = (decimal)20.15, Total_in_shelf = 25, Total_in_vault = 40, StoreId = 1 },
                new Article() { Id = 3, Name = "blue shoes", Description = "The best quality of shoes in a blue color", Price = (decimal)20.15, Total_in_shelf = 25, Total_in_vault = 40, StoreId = 1 },
                new Article() { Id = 4, Name = "red shoes", Description = "The best quality of shoes in a red color", Price = (decimal)20.15, Total_in_shelf = 25, Total_in_vault = 40, StoreId = 1 }
            );
        }
    }
}
