using Dapper;
using System.Data;

namespace ORM_Dapper
{
	public class DapperProductRepository : IProductRepository
	{
		#region Connection
		private readonly IDbConnection _connection;
		//Constructor
		public DapperProductRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		#endregion

		public IEnumerable<Product> GetAllProducts()
		{
			return _connection.Query<Product>("SELECT * FROM Products;");
		}

		public void CreateProduct(string newName, double price, int categoryID)
		{
			_connection.Execute($"INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@name, {price}, {categoryID});",
			 new { Name = newName });
		}

		public void DeleteProductByName(string existingProductName)
		{
			_connection.Execute($"DELETE FROM Products AS P WHERE P.Name = '{existingProductName}';");
		}

	}

}
