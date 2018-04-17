using DevCommerce.Business.Abstract;
using DevCommerce.DataAccess.Abstract;
using DevCommerce.Entities.Concrete;

namespace DevCommerce.Business.Concrete
{
    public class TokenService: ITokenService
    {
        ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public bool CheckToken(Token token)
        {
           var result =  _tokenRepository.Contains(x => x.TokenKey == token.TokenKey
            && x.TokenValue == token.TokenValue
            && x.ProjectName == token.ProjectName
            && x.CompanyName == token.CompanyName);

            return result;
        }
    }
}
