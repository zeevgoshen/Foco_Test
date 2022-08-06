
namespace FocoTest.Services.Tests;
using FocoTest.Models;

public interface ITestService
{
    Task CreateTest(Test test, Users users, TestSite testSite, TestSiteQueue testSiteQueue);
    Task <string> CheckExistingTestByPersonId(string id);
    Task<string> GetNextInLineForTestSite(string siteId);
    //string GetNextInLineForTestSite(string siteId);
}