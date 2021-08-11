using System;
using System.Collections.Generic;
using System.Text;

namespace ZohoToInsightIntegrator.Contract.DataModels.Insight.Module
{
    public class InsightAvailableModuleDto
    {
        public Guid InsightModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}
