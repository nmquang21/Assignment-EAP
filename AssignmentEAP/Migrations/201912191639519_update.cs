namespace AssignmentEAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "Updated_at", c => c.DateTime());
            AlterColumn("dbo.Classes", "Deleted_at", c => c.DateTime());
            AlterColumn("dbo.Students", "Deleted_at", c => c.DateTime());
            AlterColumn("dbo.DisciplineStudents", "Deleted_at", c => c.DateTime());
            AlterColumn("dbo.Disciplines", "Updated_at", c => c.DateTime());
            AlterColumn("dbo.Disciplines", "Deleted_at", c => c.DateTime());
            AlterColumn("dbo.DisciplineDefaults", "Updated_at", c => c.DateTime());
            AlterColumn("dbo.DisciplineDefaults", "Deleted_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplineDefaults", "Deleted_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DisciplineDefaults", "Updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Disciplines", "Deleted_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Disciplines", "Updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DisciplineStudents", "Deleted_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "Deleted_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Classes", "Deleted_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Classes", "Updated_at", c => c.DateTime(nullable: false));
        }
    }
}
