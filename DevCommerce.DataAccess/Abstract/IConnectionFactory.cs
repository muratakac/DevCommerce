using System.Data;

namespace DevCommerce.DataAccess.Abstract
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
