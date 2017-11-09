using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.DAL
{
    using Models;
    using Services;

    public class ProductsService : IProductsService
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsService(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<bool> AddAsync(ProductModel m)
        {
            try
            {
                var tracking = await _databaseContext.Products.AddAsync(m);

                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;

                return isAdded;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(ProductModel m)
        {
            try
            {
                var tracking = _databaseContext.Update(m);

                await _databaseContext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;

                return isModified;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var product = await _databaseContext.Products.FindAsync(id);

                var tracking = _databaseContext.Remove(product);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync(bool refresh = false)
        {
            try
            {
                var products = await _databaseContext.Products.ToListAsync();

                return products;
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
                var product = await _databaseContext.Products.FindAsync(id);

                return product;
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
                var products = _databaseContext.Products.Where(predicate);
                return await Task.FromResult(products.ToList());
            }
            catch
            {
                return null;
            }
        }
    }
}