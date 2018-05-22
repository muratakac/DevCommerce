using System.Data;

namespace DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
