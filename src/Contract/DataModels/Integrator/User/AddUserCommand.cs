using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class AddUserCommand
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
        public string RoleId { get; set; }
        public string AccountId { get; set; }
    }
}
