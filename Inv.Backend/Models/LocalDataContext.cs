
namespace Inv.Backend.Models
{
    using Domain.Models;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Inv.Common.Models.MeasureUnit> MeasureUnits { get; set; }

        public System.Data.Entity.DbSet<Inv.Common.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<Inv.Common.Models.Bin> Bins { get; set; }
    }
}