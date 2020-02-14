using System.Collections.Generic;
using DonVo.DataAnalysis.ServiceStack.Models;

namespace DonVo.DataAnalysis.ServiceStack.Interfaces
{
    public interface ITestService
    {
        bool HealthCheck();
        IEnumerable<TestModel> TestPostJson();
        string TestPostCsv();

        Dictionary<string, bool> RunAllTasks();
    }
}