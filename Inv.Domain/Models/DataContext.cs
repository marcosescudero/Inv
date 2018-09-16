
namespace Inv.Domain.Models
{
    using System.Data.Entity;
    using Common.Models;
    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        public DbSet<Item> Items { get; set; }

        public System.Data.Entity.DbSet<Inv.Common.Models.MeasureUnit> MeasureUnits { get; set; }
    }
}
