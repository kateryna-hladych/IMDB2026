using IMDB2026.DAL.EF.Entities;

namespace IMDB2026.DAL.EF.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetActiveProducts();
}