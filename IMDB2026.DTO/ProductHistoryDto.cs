namespace IMDB2026.DTO;

public class ProductHistoryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AddedQuantity { get; set; }  
    public string Comment { get; set; } = string.Empty; 
}