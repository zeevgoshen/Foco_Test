using Microsoft.AspNetCore.Mvc;
using FocoTest.Models;
using FocoTest.Services.Tests;
using ErrorOr;
using FocoTest.Constants;

namespace FocoTest.Controllers { }


//[Route("[controller]")]
public class TestsController : ApiController
{
    private readonly ITestService _testService;
    private readonly ISmsService _smsService;

    public TestsController(ITestService testService, ISmsService smsService)
    {
        _testService = testService;
        _smsService = smsService;
    }


    [HttpPost("/tests/sites/{siteId:int}/actions/callnext")]
    public async Task<IActionResult> CallNext(string siteId)
    {
        var testeNextInLine = await _testService.GetNextInLineForTestSite(siteId);

        if (testeNextInLine == null)
        {
            return NoContent();
        }
        return Ok(value: testeNextInLine);
    }



    [HttpPost("/tests/sites/{siteId:int}/customers")]
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
        var user = new Users(
            request.Id,
            siteId,
            request.PhoneNumber,
            request.DateOfBirth,
            request.FirstName,
            request.LastName,
            response.ticketId.ToString());


        var testSite = new TestSite(request.Id, siteId, response.ticketId.ToString());
        var testSiteQueue = new TestSiteQueue(siteId, response.ticketId.ToString(), Strings.NEW_TICKET);

        
        int result = await _testService.CreateTest(test, user, testSite, testSiteQueue);

        if (result > 0)
        {
            // mock send sms
            _smsService.SendSmsMessage(user.PhoneNumber, Strings.SCHEDULED_TEST);
            return Ok(value: response.ticketId);
        }
        return NoContent();


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
