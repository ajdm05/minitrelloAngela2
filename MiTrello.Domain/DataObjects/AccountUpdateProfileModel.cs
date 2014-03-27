namespace MiniTrello.Domain.DataObjects
{
    public class AccountUpdateProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
    }
}