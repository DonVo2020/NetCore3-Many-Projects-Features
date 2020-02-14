using DonVo.MLNet.DataModels;
using DonVo.MLNet.DTOs;
using DonVo.MLNet.Services;
using DonVo.Services.Interfaces.Search;
using DonVo.WebMVC.Controllers.Directories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Search
{
    public class MachineLearningController : Controller
    {
        private readonly IAnomalyDetectionsService<object> _anomalyDetectionsService;
        private readonly IMulticlassClassificationsService<CustomerOrdersMulticlassClassificationsModel> _multiclassClassificationsService;
        private readonly IClusteringService<FactStrategyPlanClusteringData> _clusteringService;
        private readonly IMainDirectories _dropDownList;

        public MachineLearningController(   IAnomalyDetectionsService<object> anomalyDetectionsService,
                                            IMulticlassClassificationsService<CustomerOrdersMulticlassClassificationsModel> multiclassClassificationsService,
                                            IClusteringService<FactStrategyPlanClusteringData> clusteringService,
                                            IMainDirectories dropDownList)
        {
            _dropDownList = dropDownList;
            _anomalyDetectionsService = anomalyDetectionsService;
            _multiclassClassificationsService = multiclassClassificationsService;
            _clusteringService = clusteringService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            await FillingDropDownLists();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadDataAnomalyDetections([Bind("detectName,pValueSize")] string detectName, int pValueSize = 100)
        {
            if (!string.IsNullOrEmpty(detectName))
            {
                var loadData = _anomalyDetectionsService.LoadData();
                var result = detectName == "DetectSpike" ? _anomalyDetectionsService.DetectSpike(pValueSize, loadData.Result)
                                                         : _anomalyDetectionsService.DetectChangePoint(pValueSize, loadData.Result);
                ViewBag.DetectSpike = detectName;
                if (loadData.IsValid && result != null)
                {
                    return View("AnomalyDetectionsResult", result);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadTrainSaveModelMulticlassClassifications([Bind("productKey, productSubcategoryKey")] int productKey, int productSubcategoryKey)
        {
            // Time out when train here, so need to train in Console App
            //var loadData = _multiclassClassificationsService.LoadData();
            //_multiclassClassificationsService.TrainModel(loadData.Result);

            var customerOrdersMulticlassClassificationsDTO = _multiclassClassificationsService.GetMulticlassClassificationsDTO(productKey, productSubcategoryKey);
            var resultDTO = new ResultMulticlassClassificationsDTO
            {
                ProductName = customerOrdersMulticlassClassificationsDTO.ProductName,
                ProductSubcategoryName = customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName,

                ResultLbfgsMaximumEntropy = _multiclassClassificationsService.TestModelLbfgsMaximumEntropy(customerOrdersMulticlassClassificationsDTO),
                ResultNaiveBayes = _multiclassClassificationsService.TestModelNaiveBayes(customerOrdersMulticlassClassificationsDTO),
                ResultSdcaMaximumEntropy = _multiclassClassificationsService.TestModelSdcaMaximumEntropy(customerOrdersMulticlassClassificationsDTO),
                ResultSdcaNonCalibrated = _multiclassClassificationsService.TestModelSdcaNonCalibrated(customerOrdersMulticlassClassificationsDTO)
            };

            if (resultDTO.ResultLbfgsMaximumEntropy != null && resultDTO.ResultNaiveBayes != null &&
                resultDTO.ResultSdcaMaximumEntropy != null && resultDTO.ResultSdcaNonCalibrated != null)
            {
                return View("MulticlassClassificationsResult", resultDTO);
            }
            return BadRequest();
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin,Accountant,Deputy")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SearchSales([Bind("channelKey,storeKey,productKey,promotionKey")] int channelKey, int storeKey, int productKey, int promotionKey)
        //{
        //    var result = await _searchService.SearchSales(channelKey, storeKey, productKey, promotionKey);
        //    if (result.IsValid)
        //    {
        //        return View("ResultSearchSales", result.Result);
        //    }
        //    return BadRequest();
        //}

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public IActionResult LoadTrainSaveModelClustering([Bind("entityKey,scenarioKey,accountKey,productCategoryKey")] int entityKey, int scenarioKey, int accountKey, int productCategoryKey)
        {
            var factStrategyPlanClusteringDTO = _clusteringService.GetFactStrategyPlanClusteringDTO(entityKey, scenarioKey, accountKey, productCategoryKey);

            var result = _clusteringService.TestModelKMeans(factStrategyPlanClusteringDTO);

            if (result != null)
            {
                return View("ClusteringResult", result);
            }
            return BadRequest();
        }

        private async Task FillingDropDownLists()
        {
            ViewBag.Products= await _dropDownList.GetProduct();
            ViewBag.ProductSubcategories = await _dropDownList.GetProductSubcategory();
            //ViewBag.Stores = await _dropDownList.GetStore();
            //ViewBag.Machines = await _dropDownList.GetMachine();
            //ViewBag.Outages = await _dropDownList.GetOutage();
            //ViewBag.Channels = await _dropDownList.GetChannel();
            //ViewBag.Promotions = await _dropDownList.GetPromotion();
            ViewBag.Entities = await _dropDownList.GetEntity();
            ViewBag.Scenarios = await _dropDownList.GetScenario();
            ViewBag.Accountings = await _dropDownList.GetAccount();
            ViewBag.ProductCategories = await _dropDownList.GetProductCategory();
        }

        protected override void Dispose(bool disposing)
        {
            _anomalyDetectionsService.Dispose();
            _multiclassClassificationsService.Dispose();
            _clusteringService.Dispose();
            base.Dispose(disposing);
        }
    }
}
