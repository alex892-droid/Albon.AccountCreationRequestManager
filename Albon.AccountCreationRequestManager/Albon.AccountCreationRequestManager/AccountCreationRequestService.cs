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

        /// <summary>
        /// Create an account creation request
        /// </summary>
        /// <param name="email">E-mail of the new account</param>
        /// <param name="publicKey">Public key of the new account</param>
        /// <returns>Id of the request</returns>
        public string Create(string email, string publicKey)
        {
            AccountCreationRequest request = new AccountCreationRequest(email, publicKey);
            AccountCreationCommunicationService.SendActivationCode(email, request.ValidationCode.ToString());
            DatabaseService.Add(request);
            return request.Id;
        }

        /// <summary>
        /// Complete the account creation by validating the email
        /// </summary>
        /// <param name="publicKey">Public key of the account</param>
        /// <param name="validationCode">Validation code sent by e-mail</param>
        /// <returns>Is account creation validated ?</returns>
        public bool CompleteAccountCreation(string publicKey, int validationCode)
        {
            var request = DatabaseService.Query<AccountCreationRequest>().Single(request => request.PublicKey == publicKey);

            if(validationCode == request.ValidationCode)
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
