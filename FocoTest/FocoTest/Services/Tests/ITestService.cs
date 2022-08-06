
namespace FocoTest.Services.Tests;
using FocoTest.Models;

public interface ITestService
{
    Task<int> CreateTest(Test test, Users users, TestSite testSite, TestSiteQueue testSiteQueue);
    Task<string> CheckExistingTestByPersonId(string id);
    Task<Test?> GetNextInLineForTestSite(string siteId);
    //string GetNextInLineForTestSite(string siteId);
}