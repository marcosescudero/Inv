﻿namespace Inv.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Interfaces;
    using Inv.Models;
    using SQLite;
    using Xamarin.Forms;
    

    public class DataService
    {
        #region Properties
        private SQLiteAsyncConnection connection; 
        #endregion

        #region Constructors
        public DataService()
        {
            this.OpenOrCreateDB();
        }
        #endregion

        #region Methods
        private async Task OpenOrCreateDB()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            this.connection = new SQLiteAsyncConnection(databasePath);
            await connection.CreateTableAsync<ItemLocal>().ConfigureAwait(false);
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }

        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }

        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }

        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }

        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }

        public async Task<List<ItemLocal>> GetAllItems()
        {
            try
            {
                var query = await this.connection.QueryAsync<ItemLocal>("select * from [Item]");
                var array = query.ToArray();
                var list = array.Select(p => new ItemLocal
                {
                    ItemId = p.ItemId,
                    Barcode = p.Barcode,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable,
                    MeasureUnitId = p.MeasureUnitId,
                }).ToList();
                return list;
            }
            catch (Exception e)
            {
                var errormessage = e.Message.ToString();
                return null;
            }
        }

        public async Task DeleteAllItems()
        {
            var query = await this.connection.QueryAsync<Item>("delete from [Item]");
        }

        #endregion
    }
}
