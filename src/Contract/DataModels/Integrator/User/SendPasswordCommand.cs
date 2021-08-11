using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class SendPasswordCommand
    {
        private string _email;
        public string Email
        {
            get => _email.DecryptStringAes();
            set => _email = value;
        }
    }
}
