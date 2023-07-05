namespace ECommerce.Domain.Entities
{
    public class User : Entity
    {
        private User() { }
        public User(string fullname, DateTime? birthDate, ApplicationUser applicationUser)
        {
            FullName = fullname;
            Birthday = birthDate;
            ApplicationUser = applicationUser;
        }

        public string FullName { get; private set; }
        public DateTime? Birthday { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }
    }
}
