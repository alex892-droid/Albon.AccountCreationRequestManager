namespace Albon.AccountCreationRequestManager
{
    public interface IAccountService
    {
        public void CreateAccount(string emailAddress, string publicKey);
    }
}
