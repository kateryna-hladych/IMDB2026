using IMDB2026.DAL.EF.Entities;
using IMDB2026.DAL.EF.Data;
using IMDB2026.DAL.EF.Repositories.Interfaces;

namespace IMDB2026.DAL.EF.Repositories;

public class ProductHistoryRepository : BaseRepository<ProductHistory>, IProductHistoryRepository
{
    public ProductHistoryRepository(ImdbDbContext context) : base(context) { }

    
 public IEnumerable<ProductHistory> GetByProductId(int productId)
    {
        return _context.ProductHistories
            .Where(h => h.ProductId == productId)
            .ToList();
    }
}