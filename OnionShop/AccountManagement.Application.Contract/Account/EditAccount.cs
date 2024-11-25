namespace AccountManagement.Application.Contract.Account
{
    public class EditAccount : CreateAccount
    {
        public long Id { get; set; }
        public string LastSendSms { get; set; }
    }
}
