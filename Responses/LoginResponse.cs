namespace BankUI.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public Guid UserId { get; set; }
    }
}
