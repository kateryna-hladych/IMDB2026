using IMDB2026.DAL.EF.Entities;

namespace IMDB2026.DAL.EF.Repositories.Interfaces;

public interface IProductHistoryRepository : IRepository<ProductHistory>
{
    IEnumerable<ProductHistory> GetByProductId(int productId);
}