using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataManager.Libraries.DataAccess;

public class MsSqlDataAccess
{

    public MsSqlDataAccess()
    {
        
    }
    public List<T> GetData<T,U>(string query, U parameters, string connectionString, CommandType command)
	{
		List<T> rows = new List<T>();
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				rows = connection.Query<T>(query, parameters,
					commandType: command).ToList();
			}
		}
		catch (Exception)
		{

			throw;
		}
		return rows;
	}
}
