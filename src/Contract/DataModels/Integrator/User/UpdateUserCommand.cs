namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.User
{
    public class UpdateUserCommand
    {
        public string UserId { get; set; }
        public string AccountId { get; set; }
        public string RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
