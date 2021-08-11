using System;
using System.Collections.Generic;
using System.Text;
using ZohoToInsightIntegrator.Contract.Utility;

namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class ResetPasswordCommand
    {
        private string _password;
        public string Password
        {
            get => _password.DecryptStringAes();
            set => _password = value;
        }
        public string UserId { get; set; }

    }
}
