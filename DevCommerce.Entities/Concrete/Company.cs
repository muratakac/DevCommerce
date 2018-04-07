using DevCommerce.Core.Entities;

namespace DevCommerce.Entities
{
    public partial class Company : IEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
