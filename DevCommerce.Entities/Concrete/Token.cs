namespace DevCommerce.Entities.Concrete
{
    public partial class Token
    {
        public int Id { get; set; }
        public string TokenKey { get; set; }
        public string TokenValue { get; set; }
        public string ProjectName { get; set; }
        public string CompanyName { get; set; }

    }
}
