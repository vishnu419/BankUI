namespace BankUI.Responses
{
    public class UserAccountResponse
    {
        public string UserAccountId { get; set; }

        public string AccountNumber { get; set; }

        public Guid UserId { get; set; }

        public string IFSCCode { get; set; }
    }
}
