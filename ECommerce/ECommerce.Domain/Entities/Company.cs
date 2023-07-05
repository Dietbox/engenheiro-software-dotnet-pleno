namespace ECommerce.Domain.Entities
{
    public class Company : Entity
    {
        public Company() { }

        public Company(string name, string taxId, ApplicationUser applicationUser)
        {
            TradingName = name;
            TaxId = taxId;
            ApplicationUser = applicationUser;
        }

        public string TradingName { get; private set; }
        public string TaxId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }
    }
}
