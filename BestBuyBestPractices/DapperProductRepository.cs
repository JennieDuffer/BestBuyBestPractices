using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection connection)
        {
            _conn = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (name, price, @categoryID)", 
                new {name = name, price = price, categoryID = categoryID});
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }

               public Product GetProduct(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products Where ProductID = @id",
                new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products SET Name = @name, Price = @price, CategoryID = @categoryID, OnSale = @onsale, StockLevel = @stocklevel " +
                "Where ProductID = @id",
            new { name = product.Name, price = product.Price, categoryID = product.CategoryID, onsale = product.OnSale, stocklevel = product.StockLevel, id = product.ProductID }) ;
        }
        public void DeleteProduct(int id)
        {
            _conn.Execute("DELETE From sales Where ProductID = @id", new { id = id });
            _conn.Execute("DELETE From reviews Where ProductID = @id", new { id = id });
            _conn.Execute("DELETE From products Where ProductID = @id", new { id = id });
        }
    }

}
