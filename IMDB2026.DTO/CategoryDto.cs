namespace IMDB2026.DTO;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
