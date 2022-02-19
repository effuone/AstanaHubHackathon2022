using QMarket.Api.Interfaces;
using QMarket.Api.Models;

namespace QMarket.Api.Repositories
{
    public class OrderRepository : IProductRepository
    {
        public Task<int> CreateAsync(Product model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Product model)
        {
            throw new NotImplementedException();
        }
    }
}