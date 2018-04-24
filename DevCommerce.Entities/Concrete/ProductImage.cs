namespace DevCommerce.Entities
{
    public partial class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public bool IsThumbnail { get; set; }
        public bool IsDefault { get; set; }
        public  Product Product { get; set; }
        public  Image Image { get; set; }
    }
}
