using Microsoft.AspNetCore.Mvc;
using FocoTest.Models;
using FocoTest.Services.Tests;
using ErrorOr;

namespace FocoTest.Controllers { }


//[Route("[controller]")]
public class TestsController : ApiController
{
    private readonly ITestService _testService;

    public TestsController(ITestService testService)
    {
        _testService = testService;
    }


    [HttpPost("/tests/sites/{siteId:guid}/actions/callnext")]
    public async Task<IActionResult> CallNext(string siteId)
    {
        var test = await _testService.GetNextInLineForTestSite(siteId);

        
        //ErrorOr<Test> requestToTestResult = Test.Create(
        //    request.Id,
        //    siteId,
        //    request.PhoneNumber,
        //    request.DateOfBirth,
        //    request.FirstName,
        //    request.LastName);

        //if (requestToTestResult.IsError)
        //{
        //    return Problem(requestToTestResult.Errors);
        //}

        //var test = requestToTestResult.Value;
        //// save to DB
        ////_testService.CreateTest(test);
        //TestResponse response = MapTestResponse(test);
        return Ok(value: test);


    }



    [HttpPost("/tests/sites/{siteId:guid}/customers")]
    public async Task<IActionResult> PerformCheckin(string siteId, CreateTestRequest request)
    {
        // check for existing test for the person, if the user exists, check
        // his ticket number and status
        string ticketId = "";

        ticketId = await CheckForExistingOpenCase(request.Id);

        if (ticketId != "")
        {
            // user already in line
            return Ok(value: ticketId);

        }

        // Creating a new user test.

        ErrorOr<Test> requestToTestResult = Test.Create(
            request.Id,
            siteId,
            request.PhoneNumber,
            request.DateOfBirth,
            request.FirstName,
            request.LastName);

        if (requestToTestResult.IsError)
        {
            return Problem(requestToTestResult.Errors);
        }

        var test = requestToTestResult.Value;

        TestResponse response = MapTestResponse(test);

        // insert to users
        var users = new Users(
            request.Id,
            siteId,
            request.PhoneNumber,
            request.DateOfBirth,
            request.FirstName,
            request.LastName,
            response.ticketId.ToString());


        var testSite = new TestSite(request.Id, siteId, response.ticketId.ToString());
        var testSiteQueue = new TestSiteQueue(siteId, response.ticketId.ToString(), "Open");

        // save to DB
        //if (ModelState.IsValid)
        //{
        await _testService.CreateTest(test, users, testSite, testSiteQueue);
        //}

        return Ok(value: response.ticketId);


    }

    private async Task<string> CheckForExistingOpenCase(string id)
    {
        string ticketId = "";
        if (id != null)
        {
            ticketId = await _testService.CheckExistingTestByPersonId(id);
        }
        return ticketId;
    }

    static TestResponse MapTestResponse(Test test)
    {
        Random rnd = new Random();

        return new TestResponse(
            test.Id,
            test.SiteId,
            test.PhoneNumber,
            test.DateOfBirth,
            test.FirstName,
            test.LastName,
            rnd.Next(10000).ToString());
    }
}
