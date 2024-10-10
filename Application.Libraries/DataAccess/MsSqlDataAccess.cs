using Microsoft.Data.SqlClient;
namespace Application.Libraries.DataAccess;

public class MsSqlDataAccess : IMsSqlDataAccess
{
	private readonly IConfiguration _configuration;

	public MsSqlDataAccess(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GetConnection(string sAppName)
	{
		var output = _configuration.GetConnectionString(sAppName);
		return output;
	}

	public T FirstOrDefault<T, U>(string query, U parameters, string connectionString, CommandType command)
	{
		T result = default(T); // Initialize with default value for type T
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				result = connection.QueryFirstOrDefault<T>(query, parameters,
					commandType: command);
			}
		}
		catch (Exception)
		{
			throw;
		}
		return result;
	}

	public async Task<T> FirstOrDefaultAsync<T, U>(string query, U parameters, string connectionString, CommandType command)
	{
		T result = default(T); // Initialize with default value for type T
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters,
					commandType: command);
			}
		}
		catch (Exception)
		{
			throw;
		}
		return result;
	}

	public List<T> GetData<T, U>(string query, U parameters, string connectionString, CommandType command)
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

	public async Task<List<T>> GetDataAsync<T, U>(string query, U parameters, string connectionString, CommandType command)
	{
		List<T> rows = new List<T>();
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				var result = await connection.QueryAsync<T>(query, parameters,
					commandType: command);
				rows = result.AsList();
			}
		}
		catch (Exception)
		{
			throw;
		}
		return rows;
	}

	public int Execute<U>(string query, U parameters, string connectionString, CommandType command)
	{
		int output = 0;
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				output = connection.Execute(query, parameters,
					commandType: command);
			}
		}
		catch (Exception)
		{
			throw;
		}
		return output;
	}

	public async Task<int> ExecuteAsync<U>(string query, U parameters, string connectionString, CommandType command)
	{
		int output = 0;
		try
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				output = await connection.ExecuteAsync(query, parameters,
					commandType: command);
			}
		}
		catch (Exception)
		{

			throw;
		}
		return output;
	}

	#region Old Sheesh
	public DataTable GetData(string sQuery, string Connectionstring)
	{
		try
		{
			using (DataTable dt = new DataTable())
			{
				using (SqlConnection con = new SqlConnection(Connectionstring))
				{
					using (SqlCommand cmd = new SqlCommand(sQuery, con))
					{
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						con.Open();
						da.Fill(dt);
						con.Close();
					}
				}

				return dt;
			}
		}
		catch (Exception ex)
		{
			// You should avoid using an empty catch block, but for simplicity, I'll keep it for now.
			return null;
		}

	}
	#endregion
}
