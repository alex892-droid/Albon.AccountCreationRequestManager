namespace Albon.AccountCreationRequestManager
{
    public interface IAccountCreationCommunicationService
    {
        public void SendActivationCode(string emailAddress, string activationCode);
    }
}
