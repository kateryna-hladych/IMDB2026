namespace IMDB2026.DAL.EF.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
