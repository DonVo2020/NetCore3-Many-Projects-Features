using DonVo.MLNet.DataModels;
using DonVo.MLNet.DTOs;
using Microsoft.ML;
using System.Collections.Generic;

namespace DonVo.MLNet.Services
{
    public interface IMulticlassClassificationsService<T> : IMachineLearningService<T> where T: CustomerOrdersMulticlassClassificationsModel
    {
        void TrainModel(IDataView dataView);
        CustomerOrdersMulticlassClassificationsDTO GetMulticlassClassificationsDTO(int productKey, int productSubcategoryKey);
        CustomerOrdersMulticlassClassificationsPredict TestModelLbfgsMaximumEntropy(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO);
        CustomerOrdersMulticlassClassificationsPredict TestModelNaiveBayes(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO);
        CustomerOrdersMulticlassClassificationsPredict TestModelSdcaMaximumEntropy(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO);
        CustomerOrdersMulticlassClassificationsPredict TestModelSdcaNonCalibrated(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO);
        //CustomerOrdersMulticlassClassificationsPredict TestData(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO);
    }
}
