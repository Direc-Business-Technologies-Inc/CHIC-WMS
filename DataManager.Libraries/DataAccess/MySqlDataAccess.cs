using Dapper;
using DataManager.Libraries.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Libraries.DataAccess
{
    public class MySqlDataAccess : IMySqlDataAccess
    {
        readonly IConfiguration _configuration;

        public MySqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string _con => _configuration.GetConnectionString("CommonDb");

        public async Task<int> Execute<U>(string query, U parameters, CommandType command)
        {
            var output = 0;
            using (IDbConnection connection = new SqlConnection(_con))
            {
                output = await connection.ExecuteAsync(query, parameters, commandType: command);
            }
            return output;
        }

        public async Task<int> Execute<U>(string query, U parameters, string connectionString, CommandType command)
        {
            var output = 0;
            using (IDbConnection connection = new SqlConnection(_con))
            {
                output = await connection.ExecuteAsync(query, parameters, commandType: command);
            }
            return output;
        }
        public List<dynamic> GetData(string query, CommandType command)
        {
            var rows = new List<dynamic>();

            try
            {
                using (IDbConnection connection = new SqlConnection(_con))
                {
                    var result = connection.Query(query, commandType: command);
                    rows = result.ToList();
                }
            }
            catch (Exception)
            {

            }
            return rows;
        }

        public List<dynamic> GetData(string query, string constr, CommandType command)
        {
            var rows = new List<dynamic>();

            try
            {
                using (IDbConnection connection = new SqlConnection(constr))
                {
                    var result = connection.Query(query, commandType: command);
                    rows = result.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return rows;
        }

        public List<T> GetData<T>(string query, string constr, CommandType command)
        {
            var rows = new List<T>();

            try
            {
                using (IDbConnection connection = new SqlConnection(constr))
                {
                    var result = connection.Query<T>(query, commandType: command);
                    rows = result.ToList();
                }
            }
            catch (Exception)
            {

            }
            return rows;
        }
        public List<T> GetData<T, U>(string query, U parameters, CommandType command)
        {
            var rows = new List<T>();

            try
            {
                using (IDbConnection connection = new SqlConnection(_con))
                {
                    var result = connection.Query<T>(query, parameters, commandType: command);
                    rows = result.ToList();
                }
            }
            catch (Exception)
            {
            
            }
            return rows;
        }

        public List<T> GetData<T, U>(string query, U parameters, string connectionString, CommandType command)
        {
            var rows = new List<T>();

            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    var result = connection.Query<T>(query, parameters, commandType: command);
                    rows = result.AsList();
                }
            }
            catch (Exception)
            {

            }
            return rows;
        }

		public async Task<T> FirstOrDefault<T>(string query)
		{
			T result = default(T); // Initialize with default value for type T
			try
			{
				using (IDbConnection connection = new SqlConnection(_con))
				{
					result = connection.QueryFirstOrDefault<T>(query);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
	}
}
