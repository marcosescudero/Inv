
namespace Inv.Domain.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Common.Models;
    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Inv.Common.Models.Location> Locations { get; set; }

        public DbSet<Inv.Common.Models.MeasureUnit> MeasureUnits { get; set; }

        public DbSet<Inv.Common.Models.Count> Counts { get; set; }

    }
}
