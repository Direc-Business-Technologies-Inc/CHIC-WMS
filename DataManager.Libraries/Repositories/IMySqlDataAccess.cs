using System.Data;

namespace DataManager.Libraries.Repositories
{
    public interface IMySqlDataAccess
    {
        Task<int> Execute<U>(string query, U parameters, CommandType command);
        Task<int> Execute<U>(string query, U parameters, string connectionString, CommandType command);
        List<T> GetData<T, U>(string query, U parameters, CommandType command);
        List<T> GetData<T, U>(string query, U parameters, string connectionString, CommandType command);
        Task<T> FirstOrDefault<T>(string query);


        List<dynamic> GetData(string query, CommandType command);
        List<dynamic> GetData(string query, string constr, CommandType command);
        List<T> GetData<T>(string query, string constr, CommandType command);
    }
}