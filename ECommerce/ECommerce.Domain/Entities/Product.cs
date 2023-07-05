namespace ECommerce.Domain.Entities
{
    public class Product : Entity
    {
        private Product() { }
        public Product(string description, string barCode, Company company)
        {
            Description = description;
            BarCode = barCode;
            Company = company;
        }

        public string Description { get; private set; }
        public string BarCode { get; private set; }
        public Company Company { get; private set; }
    }
}
