using System.Data;
using Dapper;

namespace ORM_Dapper
{
	public class DapperCategoriesRepository : ICategoriesRepository
	{
		#region Connection
		private readonly IDbConnection _connection;
		//Constructor
		public DapperCategoriesRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _connection.Query<Category>("SELECT * FROM Categories;");
		}

		#endregion


	}



}
