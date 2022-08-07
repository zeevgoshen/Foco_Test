
namespace FocoTest.Services.Tests;
using FocoTest.Models;

public interface ITestService
{
    Task<int> CreateTest(Test test, Users users, TestSite testSite, TestSiteQueue testSiteQueue);
    Task<string> CheckExistingTestByPersonId(string id, int siteId);
    Task<Test?> GetNextInLineForTestSite(int siteId);
}