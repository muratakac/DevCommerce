using Microsoft.AspNetCore.Identity;

namespace DevCommerce.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
