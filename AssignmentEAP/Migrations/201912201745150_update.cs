namespace AssignmentEAP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DisciplineStudents", "Updated_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplineStudents", "Updated_at", c => c.DateTime(nullable: false));
        }
    }
}
