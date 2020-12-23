using Diary.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Diary.Models.Configurations
{
    class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); //no autoInc

            HasKey(x => x.Id);
        }
    }
}
