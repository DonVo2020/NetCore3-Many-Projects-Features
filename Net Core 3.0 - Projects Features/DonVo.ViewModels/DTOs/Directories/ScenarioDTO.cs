using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class ScenarioDTO
    {
        public int ScenarioKey { get; set; }
        public string ScenarioLabel { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateScenarioDTO
    {
        public int ScenarioKey { get; set; }
        public string ScenarioLabel { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioDescription { get; set; }
    }
}
