using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.DapperConexion
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConexionConfiguration> _configs;
        public FactoryConnection(IOptions<ConexionConfiguration> configs)
        {
            _configs = configs;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open) _connection.Close();
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null) _connection = new SqlConnection(_configs.Value.SqlServer);

            if (_connection.State != ConnectionState.Open) _connection.Open();

            return _connection;
        }
    }
}
