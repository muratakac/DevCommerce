using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Concrete.DapperRepositories.Abstract;
using DevCommerce.Entities.Concrete;
using System.Linq;

namespace DevCommerce.Business.Concrete
{
    public class TokenService : ITokenService
    {
        ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public bool CheckToken(Token token)
        {
            return _tokenRepository.All().Any(x => x.TokenKey == token.TokenKey
            && x.TokenValue == token.TokenValue
            && x.ProjectName == token.ProjectName
            && x.CompanyName == token.CompanyName);
        }
    }
}
