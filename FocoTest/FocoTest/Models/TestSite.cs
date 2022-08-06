using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocoTest.Models;
public class TestSite
{
    public string Id { get; set; }
    [Key]
    public int SiteId { get; set; }

    [Key]
    public string TicketId { get; set; }
    
    public TestSite(string id, int siteId, string ticketId)
    {
        Id = id;
        SiteId = siteId;
        TicketId = ticketId;
    }
}
