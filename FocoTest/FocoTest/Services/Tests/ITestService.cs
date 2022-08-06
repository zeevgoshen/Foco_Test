
namespace FocoTest.Services.Tests;
using FocoTest.Models;

public interface ITestService
{
    void CreateTest(Test test, Users users, TestSite testSite, TestSiteQueue testSiteQueue);
    string CheckExistingTestByPersonId(string id);
    Task<string> GetNextInLineForTestSite(string siteId);
    //string GetNextInLineForTestSite(string siteId);
}