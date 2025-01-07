namespace BankUI.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Name { get { return FirstName + ' ' + LastName; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public List<AccountResponse>? Accounts { get; set; }
    }
}
