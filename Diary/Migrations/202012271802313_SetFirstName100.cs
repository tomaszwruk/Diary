namespace Diary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetFirstName100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 100));

            //mo¿na u¿zyyæ SQL 
            //Sql("indsert......")
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String());
        }
    }
}
