using System.Collections.Generic;

namespace DevCommerce.Entities
{
    public partial class Image 
    {
        public Image()
        {
            this.Images = new HashSet<ProductImage>();
        }

        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
    }
}
