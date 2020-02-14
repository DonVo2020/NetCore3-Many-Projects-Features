using DonVo.MLNet.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.MLNet.DTOs
{
    public class ResultMulticlassClassificationsDTO
    {
        public string ProductName { get; set; }
        public string ProductSubcategoryName { get; set; }
        public CustomerOrdersMulticlassClassificationsPredict ResultLbfgsMaximumEntropy { get; set; }
        public CustomerOrdersMulticlassClassificationsPredict ResultNaiveBayes { get; set; }
        public CustomerOrdersMulticlassClassificationsPredict ResultSdcaMaximumEntropy { get; set; }
        public CustomerOrdersMulticlassClassificationsPredict ResultSdcaNonCalibrated { get; set; }
    }
}
