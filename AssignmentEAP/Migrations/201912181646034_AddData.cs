namespace AssignmentEAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Class_Id = c.Int(nullable: false, identity: true),
                        Class_name = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Deleted_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Class_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        RollNumber = c.String(nullable: false, maxLength: 128),
                        Student_Name = c.String(),
                        Avatar = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Class_Id = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Deleted_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RollNumber)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.DisciplineStudents",
                c => new
                    {
                        DisciplineStudent_id = c.Int(nullable: false, identity: true),
                        Discipline_Value = c.Double(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Deleted_at = c.DateTime(nullable: false),
                        Discipline_Discipline_id = c.Int(),
                        Student_RollNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DisciplineStudent_id)
                .ForeignKey("dbo.Disciplines", t => t.Discipline_Discipline_id)
                .ForeignKey("dbo.Students", t => t.Student_RollNumber)
                .Index(t => t.Discipline_Discipline_id)
                .Index(t => t.Student_RollNumber);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Discipline_id = c.Int(nullable: false, identity: true),
                        Discipline_name = c.String(),
                        Description = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Deleted_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Discipline_id);
            
            CreateTable(
                "dbo.DisciplineDefaults",
                c => new
                    {
                        DisciplineDefaultId = c.Int(nullable: false, identity: true),
                        Money = c.Double(nullable: false),
                        Squat_Amout = c.Int(nullable: false),
                        Push_Up = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Deleted_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DisciplineDefaultId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplineStudents", "Student_RollNumber", "dbo.Students");
            DropForeignKey("dbo.DisciplineStudents", "Discipline_Discipline_id", "dbo.Disciplines");
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.DisciplineStudents", new[] { "Student_RollNumber" });
            DropIndex("dbo.DisciplineStudents", new[] { "Discipline_Discipline_id" });
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropTable("dbo.DisciplineDefaults");
            DropTable("dbo.Disciplines");
            DropTable("dbo.DisciplineStudents");
            DropTable("dbo.Students");
            DropTable("dbo.Classes");
        }
    }
}
