using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Demo.Models;
    using SQLite;
    using System.Linq;
    using Xamarin.Forms;

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
                var isAdded = tracking > 0 ? true : false;
                idAuto++;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ProductModel m)
        {
            try
            {
                var tracking = _sqLiteConnection.Update(m);
                var isModified = tracking > 0 ? true : false;

                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var tracking = _sqLiteConnection.Delete<ProductModel>(id);
                var isDeleted = tracking > 0 ? true : false;

                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync(bool refresh = false)
        {
            try
            {
                var products = _sqLiteConnection.Table<ProductModel>();

                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ProductModel> GetAsync(string id)
        {
            try
            {
                var product = _sqLiteConnection.Get<ProductModel>(id);

                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductModel>> QueryProductsAsync(Func<ProductModel, bool> predicate)
        {
            try
            {
                var products = _sqLiteConnection.Query<ProductModel>("select * from ProductModel");

                return products.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
