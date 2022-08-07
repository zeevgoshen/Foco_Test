using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocoTest.Models;

public class Users
{
    
    public string PersonId { get; set; }
    [Required]
    public int SiteId { get; set; }
    [Required]
    [MaxLength(12)]
    public string PhoneNumber { get; set; }
    [Required]
    public string DateOfBirth { get; set; }
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(20)]
    public string LastName { get; set; }
    [Key]
    public string TicketId { get; set; }

    public Users(string personId,
                int siteId,
                string phoneNumber,
                string dateOfBirth,
                string firstName,
                string lastName,
                string ticketId)
    {
        PersonId = personId;
        SiteId = siteId;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        FirstName = firstName;
        LastName = lastName;
        TicketId = ticketId;
    }
}
