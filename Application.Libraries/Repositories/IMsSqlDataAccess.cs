using System.Data;

namespace Application.Libraries.Repositories
{
    public interface IMsSqlDataAccess
    {
        int Execute<U>(string query, U parameters, string connectionString, CommandType command);
        Task<int> ExecuteAsync<U>(string query, U parameters, string connectionString, CommandType command);
        string GetConnection(string sAppName);
        DataTable GetData(string sQuery, string Connectionstring);
        List<T> GetData<T, U>(string query, U parameters, string connectionString, CommandType command);
        Task<List<T>> GetDataAsync<T, U>(string query, U parameters, string connectionString, CommandType command);
        T FirstOrDefault<T, U>(string query, U parameters, string connectionString, CommandType command);
        Task<T> FirstOrDefaultAsync<T, U>(string query, U parameters, string connectionString, CommandType command);

	}
}