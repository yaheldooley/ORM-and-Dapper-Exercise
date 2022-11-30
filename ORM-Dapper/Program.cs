using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace ORM_Dapper
{
	public partial class Program
    {
        static void Main(string[] args)
        {
			// Setup Config
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// Connect to DB
			string connString = config.GetConnectionString("DefaultConnection");
			IDbConnection conn = new MySqlConnection(connString);
			

			while(true)
			{
				Console.Clear();
				Console.WriteLine("Choose A Database:");
				Console.WriteLine("1 = Departments");
				Console.WriteLine("2 = Products");
				var input = Console.ReadLine();
				if (input == "1")
				{
					ManageDepartments(conn);
				}
				else if (input == "2")
				{
					ManageProducts(conn);
				}
			}
		}

		private static void ManageDepartments(IDbConnection conn)
		{
			var repo = new DapperDepartmentRepository(conn);
			while (true)
			{
				Console.Clear();
				Console.WriteLine("== Departments ==\n");
				var departments = repo.GetAllDepartments();
				foreach(var d in departments)
				{
					Console.WriteLine(d.Name);
				}
				Console.WriteLine("\n== End ==\n\n");

				Console.WriteLine("Would you like to...");
				Console.WriteLine("1 - Create");
				//Console.WriteLine("2 - Read");
				//Console.WriteLine("3 - Update");
				Console.WriteLine("4 - Delete");
				Console.WriteLine("e - Escape");

				bool escape = false;
				var crudChoice = Console.ReadLine();
				switch (crudChoice)
				{
					case "1":
						Console.WriteLine("Type name for new department:");
						var newDepartment = Console.ReadLine();
						if (!string.IsNullOrEmpty(newDepartment)) repo.InsertDepartment(newDepartment);
						break;

					case "2":
						
						break;

					case "3":

						break;

					case "4":
						Console.WriteLine("Type name of department to delete:");
						var existingDepartment = Console.ReadLine();
						if (!string.IsNullOrEmpty(existingDepartment)) repo.DeleteDepartment(existingDepartment);
						break;

					case "e":
						escape = true;
						break;
					default:

						break;
				}

				if (escape) break;
			}
		}

		private static void ManageProducts(IDbConnection conn)
		{
			var repo = new DapperProductRepository(conn);

			while (true)
			{
				Console.Clear();
				Console.WriteLine("== Products ==\n");
				var products = repo.GetAllProducts();
				foreach (var p in products)
				{
					Console.WriteLine(p.Name);
				}
				Console.WriteLine("\n== End ==\n\n");

				Console.WriteLine("Would you like to...");
				Console.WriteLine("1 - Create");
				//Console.WriteLine("2 - Read");
				//Console.WriteLine("3 - Update");
				Console.WriteLine("4 - Delete");
				Console.WriteLine("e - Escape");

				bool escape = false;
				var crudChoice = Console.ReadLine();
				switch (crudChoice)
				{
					case "1":
						Console.WriteLine("Type name for new product:");
						var newProductName = Console.ReadLine();

						bool validPrice = false;
						double price = 0;
						while (!validPrice)
						{
							Console.WriteLine("Enter its price:");
							var priceEntry = Console.ReadLine();
							
							validPrice = double.TryParse(priceEntry, out price);
						}

						var allCats = new DapperCategoriesRepository(conn).GetAllCategories();
						Console.WriteLine("== Categories ==\n");
						foreach (var cat in allCats)
						{
							Console.WriteLine($"{cat.CategoryID} - {cat.Name}");
						}
						Console.WriteLine("\n== End ==\n\n");

						bool validInt = false;
						int catId = 0;
						
						while (!validInt)
						{
							
							Console.WriteLine("Enter its CategoryID:");
							var priceEntry = Console.ReadLine();
							bool validInput = int.TryParse(priceEntry, out catId);

							validInt = validInput;
						}

						repo.CreateProduct(newProductName,price, catId);
						break;

					case "2":

						break;

					case "3":

						break;

					case "4":
						Console.WriteLine("Type name of product to delete:");
						var existingProduct = Console.ReadLine();
						if (!string.IsNullOrEmpty(existingProduct)) repo.DeleteProductByName(existingProduct);
						break;

					case "e":
						escape = true;
						break;
					default:

						break;
				}

				if (escape) break;
			}
		}
    }

}
