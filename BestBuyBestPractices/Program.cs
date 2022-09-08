
using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string connString = config.GetConnectionString("DefaultConnection");

IDbConnection conn = new MySqlConnection(connString);

var departmentRepo = new DapperDepartmentRepository(conn);

departmentRepo.InsertDepartment("Jennie's New Department");

var departments = departmentRepo.GetAllDepartments();

foreach (var department in departments)
{
    Console.WriteLine(department.DepartmentID);
    Console.WriteLine(department.Name);
    Console.WriteLine();
    Console.WriteLine();
}

var productRepo = new DapperProductRepository(conn);

//var productToUpdate = productRepo.GetProduct(940);

//productToUpdate.Name = "Updated!!";
//productToUpdate.Price = 14.99;
//productToUpdate.CategoryID = 1;
//productToUpdate.OnSale = true;
//productToUpdate.StockLevel = 1000;
//productRepo.UpdateProduct(productToUpdate);

productRepo.DeleteProduct(940);

var products = productRepo.GetAllProducts();
foreach (var product in products)
{
    Console.WriteLine(product.Name);
    Console.WriteLine(product.ProductID);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.CategoryID);
    Console.WriteLine(product.OnSale);
    Console.WriteLine(product.StockLevel);
    Console.WriteLine();
    Console.WriteLine();


}
