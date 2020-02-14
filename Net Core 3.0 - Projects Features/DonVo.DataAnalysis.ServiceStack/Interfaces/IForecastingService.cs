using System.Collections.Generic;
using DonVo.DataAnalysis.ServiceStack.Models;
using DonVo.DataAnalysis.ServiceStack.ViewModels;

namespace DonVo.DataAnalysis.ServiceStack.Interfaces
{
    /// <summary>
    /// Task 1
    /// </summary>
    public interface IForecastingService
    {
        IEnumerable<IEnumerable<double>> CorrelationAnalysis(IEnumerable<ForecastingCorrelationModel> data);
        IEnumerable<ForecastingSignificanceViewModel> CheckingSignificanceCoefficients(IEnumerable<ForecastingCorrelationModel> data);
        ForecastingClusterViewModel ClusterAnalysis(IEnumerable<ForecastingClusterModel> data, int countClusters);
    }
}