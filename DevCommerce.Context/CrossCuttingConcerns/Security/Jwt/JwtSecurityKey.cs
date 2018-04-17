using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevCommerce.Core.CrossCuttingConcerns.Security
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
