namespace BankUI.Models
{
    public class UserAccount
    {
        public string AccountNumber { get; set; }

        public string IFSCCode { get; set; }

        public Guid UserId { get; set; }

        public string UserAccountId { get; set; }
    }
}
