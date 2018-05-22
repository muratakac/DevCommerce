using DevCommerce.Core.Entities.AppSettingsModels;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories
{
    public sealed class ConnectionHelper : IConnectionFactory
    {
        public ConnectionHelper(IOptions<DapperConectionOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public DapperConectionOptions Options { get; }

        private static SqlConnection _sqlConnection;
        private static readonly object _lock = new object();

        private ConnectionHelper()
        {

        }

        public IDbConnection GetConnection
        {
            get
            {
                lock (_lock)
                {
                    if (_sqlConnection == null)
                    {
                        _sqlConnection = new SqlConnection(Options.DefaultConnection);
                    }
                }

                return _sqlConnection;
            }
        }
    }
}
