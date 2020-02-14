using RestSharp;

namespace DonVo.DataAnalysis.ServiceStack
{
    public class DataAnalysisClient : RestClient
    {
        public DataAnalysisClient(string baseUrl) : base(baseUrl) { }
    }
}