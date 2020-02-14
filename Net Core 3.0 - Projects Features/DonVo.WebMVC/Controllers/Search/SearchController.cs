using DonVo.Services.Interfaces.Search;
using DonVo.WebMVC.Controllers.Directories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IMainDirectories _dropDownList;

        public SearchController(ISearchService searchService, IMainDirectories dropDownList)
        {
            _searchService = searchService;
            _dropDownList = dropDownList;
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
        public async Task<IActionResult> SearchInventory([Bind("productKey,storeKey")] int productKey, int storeKey)
        {
            var result = await _searchService.SearchInventory(productKey, storeKey);
            if (result.IsValid)
            {
                return View("ResultSearch", result.Result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchITSLA([Bind("storeKey,machineKey,outageKey")] int storeKey, int machineKey, int outageKey)
        {
            var result = await _searchService.SearchITSLA(storeKey, machineKey, outageKey);
            if (result.IsValid)
            {
                return View("ResultSearchITSLA", result.Result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchSales([Bind("channelKey,storeKey,productKey,promotionKey")] int channelKey, int storeKey, int productKey, int promotionKey)
        {
            var result = await _searchService.SearchSales(channelKey, storeKey, productKey, promotionKey);
            if (result.IsValid)
            {
                return View("ResultSearchSales", result.Result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchStrategyPlans([Bind("entityKey,scenarioKey,accountKey,productCategoryKey")] int entityKey, int scenarioKey, int accountKey, int productCategoryKey)
        {
            var result = await _searchService.SearchStrategyPlans(entityKey, scenarioKey, accountKey, productCategoryKey);
            if (result.IsValid)
            {
                return View("ResultSearchStrategyPlans", result.Result);
            }
            return BadRequest();
        }



        private async Task FillingDropDownLists()
        {
            ViewBag.Products= await _dropDownList.GetProduct();
            ViewBag.Currencies = await _dropDownList.GetCurrency();
            ViewBag.Stores = await _dropDownList.GetStore();
            ViewBag.Machines = await _dropDownList.GetMachine();
            ViewBag.Outages = await _dropDownList.GetOutage();
            ViewBag.Channels = await _dropDownList.GetChannel();
            ViewBag.Promotions = await _dropDownList.GetPromotion();
            ViewBag.Entities = await _dropDownList.GetEntity();
            ViewBag.Scenarios = await _dropDownList.GetScenario();
            ViewBag.Accountings = await _dropDownList.GetAccount();
            ViewBag.ProductCategories = await _dropDownList.GetProductCategory();
        }

        protected override void Dispose(bool disposing)
        {
            _searchService.Dispose();
            base.Dispose(disposing);
        }
    }
}
