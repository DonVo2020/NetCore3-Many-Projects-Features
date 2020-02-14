using RestSharp;
using System.Net;
using DonVo.DataAnalysis.ServiceStack.Interfaces;

namespace DonVo.DataAnalysis.ServiceStack.Services
{
    public class HomeService : IHomeService
    {
        private readonly DataAnalysisClient _client;

        public HomeService(DataAnalysisClient client)
        {
            _client = client;
        }

        public bool HealthCheck()
        {
            var request = new RestRequest("api/Home/HealtCheck", Method.GET);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}