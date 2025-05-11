using Bigstick.BuildingBlocks.Application.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigstick.BuildingBlocks.Infrastructure
{
    public class MsSqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private IDictionary<SqlConnection, SqlConnection> _poolconnection;
        public MsSqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
            _poolconnection = new Dictionary<SqlConnection, SqlConnection>();
        }

        public SqlConnection CreateNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
            _poolconnection.Add(connection, connection);
            connection.Open();

            return connection;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public SqlConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = null;
                this._connection = new SqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        public void Dispose()
        {
            EnsureCloseConnection(_connection);
            if (_poolconnection != null)
            {
                foreach (var conn in _poolconnection) EnsureCloseConnection(conn.Value);
                _poolconnection.Clear();
            }


        }

        private void EnsureCloseConnection(IDbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        IDbConnection ISqlConnectionFactory.CreateNewConnection()
        {
            return CreateNewConnection();
        }

        string ISqlConnectionFactory.GetConnectionString()
        {
            return GetConnectionString();
        }

        IDbConnection ISqlConnectionFactory.GetOpenConnection()
        {
            return GetOpenConnection();
        }
    }
}
