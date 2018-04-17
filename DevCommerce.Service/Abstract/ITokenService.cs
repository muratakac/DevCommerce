using DevCommerce.Entities.Concrete;

namespace DevCommerce.Business.Abstract
{
    public interface ITokenService
    {
        bool CheckToken(Token token);
    }
}
