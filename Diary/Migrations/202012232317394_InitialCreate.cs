namespace Diary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Comments = c.String(),
                        Activities = c.Boolean(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RatingStudents",
                c => new
                    {
                        Rating_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rating_Id, t.Student_Id })
                .ForeignKey("dbo.Ratings", t => t.Rating_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Rating_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RatingStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.RatingStudents", "Rating_Id", "dbo.Ratings");
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropIndex("dbo.RatingStudents", new[] { "Student_Id" });
            DropIndex("dbo.RatingStudents", new[] { "Rating_Id" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropTable("dbo.RatingStudents");
            DropTable("dbo.Ratings");
            DropTable("dbo.Students");
            DropTable("dbo.Groups");
        }
    }
}
