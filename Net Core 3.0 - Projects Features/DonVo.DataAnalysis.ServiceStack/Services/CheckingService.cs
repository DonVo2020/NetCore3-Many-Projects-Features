using DonVo.DataAnalysis.ServiceStack.Interfaces;

namespace DonVo.DataAnalysis.ServiceStack.Services
{
    /// <summary>
    /// Task 5
    /// </summary>
    public class CheckingService : ICheckingService
    {
        private readonly DataAnalysisClient _client;

        public CheckingService(DataAnalysisClient client)
        {
            _client = client;
        }
    }
}