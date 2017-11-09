using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo.Services
{
    using Models;

    public class ProductsService2 : IProductsService
    {
        private readonly SQLiteConnection _sqLiteConnection;

        private int idAuto = 0;

        public ProductsService2()
        {
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
            _sqLiteConnection.CreateTable<ProductModel>();
            idAuto = 1;

            var products = _sqLiteConnection.Table<ProductModel>();
            if (products != null)
            {
                idAuto = products.Count() + 1;
            }
        }

        public async Task<bool> AddAsync(ProductModel m)
        {
            try
            {
                m.Id = idAuto;
                var tracking = _sqLiteConnection.Insert(m);
                var ok = tracking > 0 ? true : false;
                idAuto++;
                return await Task.FromResult(ok);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateAsync(ProductModel m)
        {
            try
            {
                var tracking = _sqLiteConnection.Update(m);
                var ok = tracking > 0 ? true : false;
                return await Task.FromResult(ok);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var tracking = _sqLiteConnection.Delete<ProductModel>(id);
                var ok = tracking > 0 ? true : false;
                return await Task.FromResult(ok);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync(bool refresh = false)
        {
            try
            {
                var products = _sqLiteConnection.Table<ProductModel>();
                return await Task.FromResult(products);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductModel> GetAsync(string id)
        {
            try
            {
                var product = _sqLiteConnection.Get<ProductModel>(id);
                return await Task.FromResult(product);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductModel>> QueryProductsAsync(Func<ProductModel, bool> predicate)
        {
            try
            {
                var products = _sqLiteConnection.Query<ProductModel>("select * from ProductModel");
                return await Task.FromResult(products.ToList());
            }
            catch
            {
                return null;
            }
        }
    }
}