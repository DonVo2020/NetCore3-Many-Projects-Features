using System.Collections.Generic;
using DonVo.DataAnalysis.ServiceStack.Models;
using DonVo.DataAnalysis.ServiceStack.ViewModels;

namespace DonVo.DataAnalysis.ServiceStack.Interfaces
{
    /// <summary>
    /// Task 2 and 3
    /// </summary>
    public interface IDeterminingService
    {
        double MultiCorrelationCoefficient(IEnumerable<DeterminingMultiCorrelationModel> data);
        DeterminingMultiFactorViewModel MultiFactorModel(IEnumerable<DeterminingMultiFactorModel> data);
        IEnumerable<IEnumerable<double>> PrincipalComponentAnalysis(IEnumerable<DeterminingPrincipalComponentModel> data, int countComponents);
    }
}