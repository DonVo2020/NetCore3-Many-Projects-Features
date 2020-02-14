using System.Collections.Generic;

namespace DonVo.DataAnalysis.ServiceStack.ViewModels
{
    public class DeterminingMultiFactorViewModel
    {
        public IEnumerable<double> RegressionModel { get; set; }
        public IEnumerable<double> Standardization { get; set; }
        public IEnumerable<double> SignificanceTest { get; set; }
        public IEnumerable<double> ConfidenceInterval { get; set; }
    }
}