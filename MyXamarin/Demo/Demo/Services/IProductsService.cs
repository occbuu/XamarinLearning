using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Services
{
    using Demo.Models;

    public interface IProductsService : IService<ProductModel>
    {
        Task<IEnumerable<ProductModel>> QueryProductsAsync(Func<ProductModel, bool> predicate);
    }
}
