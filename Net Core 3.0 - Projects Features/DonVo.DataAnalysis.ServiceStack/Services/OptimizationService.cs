using DonVo.DataAnalysis.ServiceStack.Interfaces;

namespace DonVo.DataAnalysis.ServiceStack.Services
{
    /// <summary>
    /// Task 4
    /// </summary>
    public class OptimizationService : IOptimizationService
    {
        private readonly DataAnalysisClient _client;

        public OptimizationService(DataAnalysisClient client)
        {
            _client = client;
        }
    }
}