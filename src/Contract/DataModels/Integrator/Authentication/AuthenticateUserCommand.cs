using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class AuthenticateUserCommand
    {
        private string _email;
        private string _password;
        public string Email
        {
            get => _email.DecryptStringAes();
            set => _email = value;
        }
        public string Password
        {
            get => _password.DecryptStringAes();
            set => _password = value;
        }
    }
}
