﻿
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

        public DbSet<Product> Products { get; set; }
    }
}