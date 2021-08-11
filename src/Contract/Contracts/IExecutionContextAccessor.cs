namespace ZohoToInsightIntegrator.Contract.Contracts
{
    public interface IExecutionContextAccessor
    {
        string Email { get; }
        bool IsSuperAdmin { get; }
        bool IsAdmin { get; }
        bool IsAvailable { get; }
        string IpAddress { get; }
    }
}