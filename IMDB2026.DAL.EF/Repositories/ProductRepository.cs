using IMDB2026.DAL.EF.Entities;
using IMDB2026.DAL.EF.Data;
using IMDB2026.DAL.EF.Repositories.Interfaces;

namespace IMDB2026.DAL.EF.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ImdbDbContext context) : base(context) { }

    public IEnumerable<Product> GetActiveProducts() =>
        _context.Products.Where(p => !p.IsBlocked).ToList();
}