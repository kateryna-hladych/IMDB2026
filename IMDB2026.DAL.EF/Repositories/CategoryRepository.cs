using IMDB2026.DAL.EF.Data;
using IMDB2026.DAL.EF.Entities;
using IMDB2026.DAL.EF.Repositories.Interfaces;

namespace IMDB2026.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ImdbDbContext context) : base(context) { }
}