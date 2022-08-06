using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocoTest.Models;

public class TestSiteQueue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Key]
    public int SiteId { get; set; }
    
    public string TicketId { get; set; }

    public string TicketStatus { get; set; }

    public TestSiteQueue(int siteId, string ticketId, string ticketStatus)
    {
        SiteId = siteId;
        TicketId = ticketId;
        TicketStatus = ticketStatus;
    }
}
