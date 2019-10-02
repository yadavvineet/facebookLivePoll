namespace FacebookLivePoll.Web.Migrations.DbMConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PollName = c.String(maxLength: 30, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Polls");
        }
    }
}
