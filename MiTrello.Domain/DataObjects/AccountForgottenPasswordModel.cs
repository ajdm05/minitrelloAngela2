namespace MiniTrello.Domain.DataObjects
{
    public class AccountForgottenPasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}