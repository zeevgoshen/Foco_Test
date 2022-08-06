using DataAccess.Data;
using FocoTest.Models;
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

    public string CheckExistingTestByPersonId(string id)
    {
        try
        {

            var existingUser = _context.Users!.SingleOrDefault(p => p.Id == id);
            var ticketId = "";

            if (existingUser is not null)
            {
                ticketId = existingUser.TicketId;

                var ticket = _context.TestSiteQueue!.SingleOrDefault(p => p.TicketId == ticketId);

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
    
    public void CreateTest(
        Test test,
        Users user,
        TestSite testSite,
        TestSiteQueue testSiteQueue)
    {

        // checks for existing user,test,status open ticket
        //

        var existingUser = _context.Users!.SingleOrDefault(p => p.Id == user.Id);

        _context.Tests.Add(test);

        if (existingUser is null)
        {
            _context.Users.Add(user);   
            
        }
        _context.TestSites.Add(testSite);
        _context.TestSiteQueue.Add(testSiteQueue);
        _context.SaveChanges();

        // Send sms using phone + ticketid
    }


    public string GetNextInLineForTestSite(string siteId)
    {
        //var data = _context.TestSiteQueue
        //    .Select(p => p.TicketStatus == "Open" && p.SiteId == siteId).ToList();


        //var testSite = _context.TestSiteQueue!.SelectMany(p => p.SiteId == siteId);
        //var ndata = data.OrderBy(p1 => p1.Id).FirstOrDefault();

        //var query = _context.TestSiteQueue.GetAsyncEnumerator();
        //if (testSite is not null)
        //{
        //    var nextInLine = testSite.Select(p => p.TicketId).FirstOrDefault();
        //}

        //var data1 = _context.TestSiteQueue
        //    .Where(p => p.SiteId == siteId).OrderBy(p => p.Id)
        //    .Select(p => p.TicketStatus == "Open").FirstOrDefault();

        var data1 = _context.TestSiteQueue
    .Where(p => p.SiteId == siteId).OrderBy(p => p.Id)
    .Select(p => p.TicketStatus == "Open");

        var d1 = data1.FirstOrDefault();



        //.Where(p => p.SiteId == siteId)
        //.Select(p => p.TicketId)
        //.FirstOrDefault();
        return "";

    }
}
