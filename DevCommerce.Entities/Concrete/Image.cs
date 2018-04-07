using DevCommerce.Core.Entities;

namespace DevCommerce.Entities
{
    public partial class Image : IEntity
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
    }
}
