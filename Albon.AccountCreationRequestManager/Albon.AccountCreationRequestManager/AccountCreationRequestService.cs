namespace Albon.AccountCreationRequestManager
{
    public class AccountCreationRequestService
    {
        public IDatabaseService DatabaseService { get; set; }

        public IAccountCreationCommunicationService AccountCreationCommunicationService { get; set; }

        public IAccountService AccountService { get; set; }

        public AccountCreationRequestService(IDatabaseService databaseService, IAccountCreationCommunicationService accountCreationCommunicationService, IAccountService accountService) 
        {
            DatabaseService = databaseService;
            AccountCreationCommunicationService = accountCreationCommunicationService;
            AccountService = accountService;
        }

        public void Create(string email, string publicKey)
        {
            AccountCreationRequest request = new AccountCreationRequest(email, publicKey);
            AccountCreationCommunicationService.SendActivationCode(email, request.ActivationCode.ToString());
            DatabaseService.Add(request);
        }

        public bool CompleteAccountCreation(string publicKey, int activationCode)
        {
            var request = DatabaseService.Query<AccountCreationRequest>().Single(request => request.PublicKey == publicKey);

            if(activationCode == request.ActivationCode)
            {
                AccountService.CreateAccount(request.EmailAddress, request.PublicKey);
                DatabaseService.Delete(request);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
