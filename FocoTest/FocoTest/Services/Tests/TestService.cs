using DataAccess.Data;
using FocoTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FocoTest.Services.Tests;


public class TestService : ITestService
{
    // here we would store in EF or DB or repository
    // this is in-memory storage, so we can use it for testing

    //private static readonly Dictionary<string, Test> _tests = new();

    //private static readonly Queue<Test> _tests = new();
    private readonly DataContext _context;

    public TestService(DataContext context)
    {
        _context = context;
    }

    public async Task<string> CheckExistingTestByPersonId(string id)
    {
        try
        {

            var existingUser = await _context.Users!.SingleOrDefaultAsync(p => p.Id == id);
            var ticketId = "";

            if (existingUser is not null)
            {
                ticketId = existingUser.TicketId;

                var ticket = await _context.TestSiteQueue!.SingleOrDefaultAsync(p => p.TicketId == ticketId);

                if (ticket is not null)
                {
                    if (ticket.TicketStatus == "Open")
                    {
                        ticketId = ticket.TicketId;

                    }
                    else
                    {
                        ticketId = "";
                    }
                }
            }
            return ticketId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task CreateTest(
        Test test,
        Users user,
        TestSite testSite,
        TestSiteQueue testSiteQueue)
    {

        // checks for existing user,test,status open ticket
        //

        var existingUser = (from m in _context.Users
                            where m.Id == user.Id
                            select m.Id);

        var existingTest = await _context.Tests!.SingleAsync(p => p.Id == user.Id);

        var existingTestSite = (from m in _context.TestSites
                      where m.SiteId == existingTest.SiteId
                                select m.SiteId);

        if (existingUser.Count() == 0)
        {
            _context.Users.Add(user);
        }

        if (existingTest is null)
        {
            _context.Tests.Add(test);
        }

        if (existingTestSite.Count() == 0)
        {
            _context.TestSites.Add(testSite);
        }
        
        _context.TestSiteQueue.Add(testSiteQueue);
        _context.SaveChanges();

        // Send sms using phone + ticketid
    }


    //   public async Task<string> GetNextInLineForTestSite(string siteId)
    public async Task<Test?> GetNextInLineForTestSite(string siteId)
    {
        string ticketId = string.Empty;

        var test = await _context.TestSiteQueue.OrderBy(p => p.Id).
            FirstOrDefaultAsync(p => p.SiteId == siteId
        && p.TicketStatus == "Open");


        if (test is not null)
        {
            test.TicketStatus = "Closed";
            ticketId = test.TicketId;
            _context.SaveChanges();

            // Send SMS

        }
        
        Test? result = await GetPersonDetailsByTicketId(ticketId);


        return result;

    }

    public async Task<Test?> GetPersonDetailsByTicketId(string ticketId)
    {
        var existingUser = await _context.Users!.SingleOrDefaultAsync(p => p.TicketId == ticketId);
        
        
        if (existingUser is not null)
        {

            var test = await _context.Tests!.SingleOrDefaultAsync(p => p.Id == existingUser.Id);
            
            if (test is not null)
            {
                return test;
            }
            
            //var user = new Users(existingUser.Id,
            //    existingUser.SiteId,
            //    existingUser.PhoneNumber,
            //    existingUser.DateOfBirth,
            //    existingUser.FirstName,
            //    existingUser.LastName,
            //    existingUser.TicketId);
        }
        return null;
    }
}
