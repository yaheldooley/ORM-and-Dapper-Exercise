using System.Data;
using System.Data.Common;
using Dapper;

namespace ORM_Dapper
{
	public class DapperDepartmentRepository : IDepartmentRepository
	{
		#region Connection
		private readonly IDbConnection _connection;
		//Constructor
		public DapperDepartmentRepository(IDbConnection connection)
		{
			_connection = connection;
		}

		#endregion

		public IEnumerable<Department> GetAllDepartments()
		{
			return _connection.Query<Department>("SELECT * FROM Departments;");
		}

		public void InsertDepartment(string newDepartmentName)
		{
			_connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
			 new { departmentName = newDepartmentName });
		}

		public void DeleteDepartment(string existingDepartmentName)
		{
			_connection.Execute($"DELETE FROM Departments AS D WHERE D.Name = '{existingDepartmentName}';");
		}
	}



}
