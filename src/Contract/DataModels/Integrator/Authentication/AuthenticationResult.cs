namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string authenticationError)
        {
            IsAuthenticated = false;
            AuthenticationError = authenticationError;
        }

        public AuthenticationResult(AuthenticatedUserDto user)
        {
            this.IsAuthenticated = true;
            this.User = user;
        }

        public bool IsAuthenticated { get; }

        public string AuthenticationError { get; }

        public AuthenticatedUserDto User { get; }
    }
}
