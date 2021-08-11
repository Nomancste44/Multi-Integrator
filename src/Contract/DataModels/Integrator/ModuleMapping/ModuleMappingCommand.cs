namespace ZohoToInsightIntegrator.Contract.DataModels.Integrator.ModuleMapping
{
    public class ModuleMappingCommand
    {
        public string MappedModuleId { get; set; }
        public string ZohoModuleId { get; set; }
        public string InsightModuleId { get; set; }
    }
}
