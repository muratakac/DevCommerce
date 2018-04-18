using System.Collections.Generic;

namespace DevCommerce.Entities.Concrete
{
    public partial class Culture
    {
        public Culture()
        {
            this.Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
