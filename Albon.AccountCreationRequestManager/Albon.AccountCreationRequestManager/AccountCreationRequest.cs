using AttributeSharedKernel;

namespace Albon.AccountCreationRequestManager
{
    public class AccountCreationRequest
    {
        [DatabaseKey]
        public string Id { get; set; }

        public string EmailAddress { get; set; }

        public string PublicKey { get; set; }

        public int ValidationCode { get; set; }

        public AccountCreationRequest() { }

        public AccountCreationRequest(string email, string publicKey)
        {
            Id = Guid.NewGuid().ToString();
            EmailAddress = email;
            PublicKey = publicKey;
            ValidationCode = new Random().Next(100000, 999999);
        }
    }
}
