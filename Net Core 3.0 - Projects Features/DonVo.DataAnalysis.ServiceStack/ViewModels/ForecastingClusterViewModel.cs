using System.Collections.Generic;

namespace DonVo.DataAnalysis.ServiceStack.ViewModels
{
    public class ForecastingClusterViewModel
    {
        public IEnumerable<IEnumerable<double>> X { get; set; }
        public IEnumerable<IEnumerable<double>> Y { get; set; }
        public IEnumerable<IEnumerable<double>> Centers { get; set; }
    }
}