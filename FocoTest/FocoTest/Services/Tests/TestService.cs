using DataAccess.Data;
using FocoTest.Constants;
using FocoTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FocoTest.Services.Tests;


public class TestService : ITestService
{
    private readonly DataContext _context;

    public TestService(DataContext context)
    {
        _context = context;
    }

    public async Task<string> CheckExistingTestByPersonId(string id, int siteId)
    {
        try
        {

            var existingUser = await _context.Users!.AsNoTracking()
                .SingleOrDefaultAsync(
                p => p.PersonId == id && p.SiteId == siteId);

            var ticketId = "";

            if (existingUser is not null)
            {
                ticketId = existingUser.TicketId;

                var ticket = await _context.TestSiteQueue!.SingleOrDefaultAsync(
                    p => p.TicketId == ticketId);

                if (ticket is not null)
                {
                    if (ticket.TicketStatus == Strings.NEW_TICKET)
                    {
                        ticketId = ticket.TicketId;

                    }
                    else
                    {
                        ticketId = "";
                    }
                } else
                {
                    existingUser.TicketId = "";
                    _context.SaveChanges();
                }
            }
            return ticketId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> CreateTest(
        Test test,
        Users user,
        TestSite testSite,
        TestSiteQueue testSiteQueue)
    {
        // checks for existing user,test,status open ticket
        
        await AddEntitiesToDB(test, user, testSite);
        _context.TestSiteQueue.Add(testSiteQueue);

        return  _context.SaveChanges();
    }

    private async Task<Task> AddEntitiesToDB(Test test, Users user, TestSite testSite)
    {
        Users existingUser = _context.Users!.AsNoTracking().FirstOrDefault(
                p => p.PersonId == user.PersonId && p.SiteId == user.SiteId);

        Test existingTest = _context.Tests!.FirstOrDefault(
                p => p.PersonId == user.PersonId && p.SiteId == user.SiteId);


        var existingTestSite = (from m in _context.TestSites
                                where m.SiteId == testSite.SiteId
                                select m.SiteId);

        if (existingUser is not null)
        {
            _context.Users.Remove(existingUser);
            _context.Users.Add(user);
        }
        else
        {
            _context.Users.Add(user);
        }

        if (existingTest is not null)
        {
            _context.Tests.Remove(existingTest);
            _context.Tests.Add(test);
        }
        else
        {
            _context.Tests.Add(test);
        }

        if (existingTestSite.Count() == 0)
        {
            _context.TestSites.Add(testSite);
        }
        return Task.CompletedTask;
    }

    public async Task<Test?> GetNextInLineForTestSite(int siteId)
    {
        var testSiteQueue = await _context.TestSiteQueue.OrderBy(p => p.Id).
            FirstOrDefaultAsync(p => p.SiteId == siteId
        && p.TicketStatus == "Open");

        Test? result = null;

        if (testSiteQueue is not null)
        {
            result = await GetPersonDetailsByTicketId(testSiteQueue.TicketId);
            //
            // Change ticket state to "In-Process"/"Waiting for results..."
            // do some work, then close the ticked
            //
            if (result is not null)
            {
                // update queue ticket
                testSiteQueue.TicketStatus = Strings.OLD_TICKET;

                _context.SaveChanges();
            }
        }
        return result;
    }

    public async Task<Test?> GetPersonDetailsByTicketId(string ticketId)
    {
        var existingUser = await _context.Users!
            .SingleAsync(
            p => p.TicketId == ticketId);
        
        if (existingUser is not null)
        {
            Test existingTest = await _context.Tests!
                .SingleAsync(
                p => p.TicketId == existingUser.TicketId);

            if (existingTest is not null)
            {
                return existingTest;
            }
        }
        return null;
    }
}
