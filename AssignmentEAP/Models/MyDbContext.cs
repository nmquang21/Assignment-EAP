namespace AssignmentEAP.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }
        public DbSet<Class> Classes { set; get; }
        public DbSet<Student> Students { set; get; }
        public DbSet<Discipline> Disciplines { set; get; }
        public DbSet<DisciplineDefault> DisciplineDefaults { set; get; }
        public DbSet<DisciplineStudent> DisciplineStudents { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MyDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
