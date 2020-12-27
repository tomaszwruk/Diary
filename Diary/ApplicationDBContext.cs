using Diary.Models.Configurations;
using Diary.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext()
            : base("name=ApplicationDBContext")
        {
        }

        public DbSet<Student> Students { get; set; }//tworzy tabele

        public DbSet<Group> Groups { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        //konfiguracja dodatkokwych opcji na baie podczas tworzenia
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}