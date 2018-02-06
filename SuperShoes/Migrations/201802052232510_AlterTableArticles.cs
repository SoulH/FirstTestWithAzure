namespace SuperShoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Total_in_shelf", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "Total_in_vault", c => c.Int(nullable: false));
            DropColumn("dbo.Articles", "TotalInShelf");
            DropColumn("dbo.Articles", "TotalInVault");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "TotalInVault", c => c.String(maxLength: 20));
            AddColumn("dbo.Articles", "TotalInShelf", c => c.String(maxLength: 20));
            DropColumn("dbo.Articles", "Total_in_vault");
            DropColumn("dbo.Articles", "Total_in_shelf");
        }
    }
}
