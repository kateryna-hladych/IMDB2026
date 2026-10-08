using System.Text;
using IMDB2026.DAL.EF.Data;
using IMDB2026.DAL.EF.Entities;
using IMDB2026.DAL.EF.Repositories;
using IMDB2026.DAL.EF.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMDB2026.Console;

internal class Program
{
    static void Main(string[] args)
    {
        System.Console.OutputEncoding = Encoding.UTF8;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";

        var optionsBuilder = new DbContextOptionsBuilder<ImdbDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using var context = new ImdbDbContext(optionsBuilder.Options);
        IProductRepository productRepo = new ProductRepository(context);

        while (true)
        {
            System.Console.WriteLine("УПРАВЛІННЯ ТОВАРАМИ:");
            System.Console.WriteLine("1. Показати всі активні товари");
            System.Console.WriteLine("2. Знайти товар за ID");
            System.Console.WriteLine("3. Додати новий товар");
            System.Console.WriteLine("4. Оновити товар");
            System.Console.WriteLine("5. Видалити товар");
            System.Console.WriteLine("0. Спочатку");
            System.Console.Write("цифра: ");

            string choice = System.Console.ReadLine() ?? "";
            System.Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowAll(productRepo);
                    break;
                case "2":
                    ShowById(productRepo);
                    break;
                case "3":
                    Add(productRepo);
                    break;
                case "4":
                    Update(productRepo);
                    break;
                case "5":
                    Delete(productRepo);
                    break;
                case "0":
                    System.Console.WriteLine("\n press клавішу для виходу");
                    System.Console.ReadKey();
                    return;
                default:
                    System.Console.WriteLine("шось не те обрали..");
                    break;
            }
        }
    }

    private static void ShowAll(IProductRepository _productRepo)
    {
        var products = _productRepo.GetAll();

        System.Console.WriteLine("Список товарів:");
        foreach (var p in products)
        {
            System.Console.WriteLine($"ID: {p.ProductId} | Назва: {p.Name} | Ціна продажу: {p.SellingPrice} | Кількість: {p.StockQuantity}");
        }
    }

    private static void ShowById(IProductRepository _productRepo)
    {
        System.Console.Write("Введіть ID товару: ");
        int id = int.Parse(System.Console.ReadLine() ?? "0");

        var product = _productRepo.GetById(id);

        if (product != null)
        {
            System.Console.WriteLine($"ID: {product.ProductId} | Назва: {product.Name} | Ціна купівлі: {product.PurchasePrice} | Ціна продажу: {product.SellingPrice} | Залишок: {product.StockQuantity}");
        }
        else
        {
            System.Console.WriteLine("Товар не знайдено:(");
        }
    }

    private static void Add(IProductRepository _productRepo)
    {
        var product = new Product();

        System.Console.Write("Назва товару: ");
        product.Name = System.Console.ReadLine() ?? "";

        System.Console.Write("ID категорії: ");
        product.CategoryId = int.Parse(System.Console.ReadLine() ?? "1");

        System.Console.Write("Ціна купівлі: ");
        product.PurchasePrice = decimal.Parse(System.Console.ReadLine() ?? "0");

        System.Console.Write("Ціна продажу: ");
        product.SellingPrice = decimal.Parse(System.Console.ReadLine() ?? "0");

        System.Console.Write("Кількість на складі: ");
        product.StockQuantity = int.Parse(System.Console.ReadLine() ?? "0");

        try
        {
            _productRepo.Add(product);
            System.Console.WriteLine("Товар успішно додано:)");
        }
        catch (Exception)
        {
            System.Console.WriteLine("Помилка: Не можна додати товар. Не існує такого ID категорії!!");
        }
    }

    private static void Update(IProductRepository _productRepo)
    {
        System.Console.Write("Введіть ID товару для оновлення: ");
        int id = int.Parse(System.Console.ReadLine() ?? "0");

        var product = _productRepo.GetById(id);

        if (product != null)
        {
            System.Console.Write($"Нова назва ({product.Name}): ");
            string name = System.Console.ReadLine() ?? "";
            if (name != "") product.Name = name;

            System.Console.Write($"Нова ціна продажу ({product.SellingPrice}): ");
            string priceInput = System.Console.ReadLine() ?? "";
            if (priceInput != "") product.SellingPrice = decimal.Parse(priceInput);

            System.Console.Write($"Нова кількість ({product.StockQuantity}): ");
            string qtyInput = System.Console.ReadLine() ?? "";
            if (qtyInput != "") product.StockQuantity = int.Parse(qtyInput);

            _productRepo.Update(product);
            System.Console.WriteLine("Товар оновлено:)");
        }
        else
        {
            System.Console.WriteLine("Товар не знайдено:(");
        }
    }

    private static void Delete(IProductRepository _productRepo)
    {
        System.Console.Write("Введіть ID товару для видалення: ");
        int id = int.Parse(System.Console.ReadLine() ?? "0");

        _productRepo.Delete(id);
        System.Console.WriteLine("Товар видалено!!");
    }
}