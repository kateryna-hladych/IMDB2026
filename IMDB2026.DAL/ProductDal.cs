using Microsoft.Data.SqlClient;
using IMDB2026.DTO;
//ГОЛОВНИЙ МІСТ МІЖ SSMS І VS
//Він виконує 4 основні дії з товарами : читає, додає, оновлює та видаляє
namespace IMDB2026.DAL
{
    public class ProductDal
    {
        private readonly string connectionString;

        public ProductDal(string connectionString)
        {
            this.connectionString = connectionString;
        }
                            
        public List<ProductDto> GetAll()
        {
            var products = new List<ProductDto>();
            const string sql = @"SELECT ProductId, CategoryId, Name, PurchasePrice, SellingPrice, StockQuantity, IsBlocked 
                                FROM dbo.Products WHERE IsBlocked = 0";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            connection.Open();   //відкриває активний канал зв'язку з базою даних

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new ProductDto
                {
                    ProductId = reader.GetInt32(0),
                    CategoryId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    PurchasePrice = reader.GetDecimal(3),
                    SellingPrice = reader.GetDecimal(4),
                    StockQuantity = reader.GetInt32(5),
                    IsBlocked = reader.GetBoolean(6)
                });
            }
            return products;
        }

        public int Create(ProductDto product)
        {
            const string sql = @"
                INSERT INTO dbo.Products (CategoryId, Name, PurchasePrice, SellingPrice, StockQuantity, IsBlocked)
                VALUES (@CategoryId, @Name, @PurchasePrice, @SellingPrice, @StockQuantity, 0);
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
            command.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

            connection.Open();
            return (int)command.ExecuteScalar()!;
        }

        public void Update(ProductDto product)
        {
            const string sql = @"UPDATE dbo.Products 
                                SET Name = @Name, SellingPrice = @SellingPrice, StockQuantity = @StockQuantity 
                                WHERE ProductId = @ProductId";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductId", product.ProductId);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int productId)
        {
            const string sql = "UPDATE dbo.Products SET IsBlocked = 1 WHERE ProductId = @ProductId";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}