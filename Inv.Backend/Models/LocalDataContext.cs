
namespace Inv.Backend.Models
{
    using Domain.Models;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Inv.Common.Models.MeasureUnit> MeasureUnits { get; set; }
    }
}