using System.ComponentModel.DataAnnotations;

namespace FocoTest.Models;

public class Users
{
    [Required]
    [MaxLength(50)]
    [Key]
    public string Id { get; set; }
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
    public string TicketId { get; set; }

    public Users(string id,
                int siteId,
                string phoneNumber,
                string dateOfBirth,
                string firstName,
                string lastName,
                string ticketId)

    {

        Id = id;
        SiteId = siteId;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        FirstName = firstName;
        LastName = lastName;
        TicketId = ticketId;


    }
}
