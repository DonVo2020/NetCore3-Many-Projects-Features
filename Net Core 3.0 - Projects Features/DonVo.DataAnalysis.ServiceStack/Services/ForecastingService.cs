using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Net;
using DonVo.DataAnalysis.ServiceStack.Exceptions;
using DonVo.DataAnalysis.ServiceStack.Interfaces;
using DonVo.DataAnalysis.ServiceStack.Models;
using DonVo.DataAnalysis.ServiceStack.ViewModels;

namespace DonVo.DataAnalysis.ServiceStack.Services
{
    /// <summary>
    /// Task 1
    /// </summary>
    public class ForecastingService : IForecastingService
    {
        private readonly DataAnalysisClient _client;

        public ForecastingService(DataAnalysisClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Task 1.1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Obsolete]
        public IEnumerable<IEnumerable<double>> CorrelationAnalysis(IEnumerable<ForecastingCorrelationModel> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CorrelationAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<IEnumerable<double>>>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Obsolete]
        public IEnumerable<ForecastingSignificanceViewModel> CheckingSignificanceCoefficients(IEnumerable<ForecastingCorrelationModel> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CheckingSignificanceCoefficients", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<ForecastingSignificanceViewModel>>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.3
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countClusters"></param>
        /// <returns></returns>
        [System.Obsolete]
        public ForecastingClusterViewModel ClusterAnalysis(IEnumerable<ForecastingClusterModel> data, int countClusters)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/ClusterAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(data),
                CountCluster = countClusters
            });
            request.AddBody(json);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<ForecastingClusterViewModel>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }
    }
}