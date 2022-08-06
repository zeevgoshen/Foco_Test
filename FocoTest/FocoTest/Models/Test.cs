using ErrorOr;
using FocoTest.ServiceErrors;
using System.ComponentModel.DataAnnotations;

namespace FocoTest.Models;

public class Test
{
    public const int MaxIdLength = 50;
    public const int MaxPhoneLength = 12;
    public const int MaxFNameLength = 20;
    public const int MaxLNameLength = 20;
    public const string MaxBirthDate = "31/12/2000";
    public const string MinBirthDate = "1/1/1900";

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
    public string DateCreated { get; set; }


    private Test(string id,
                int siteId,
                string phoneNumber,
                string dateOfBirth,
                string firstName,
                string lastName,
                string dateCreated)
                
    {

        Id = id;
        SiteId = siteId;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        FirstName = firstName;
        LastName = lastName;
        DateCreated = dateCreated;
    }

    public static ErrorOr<Test> Create(string id,
                                       int siteId,
                                       string phoneNumber,
                                       string dateOfBirth,
                                       string firstName,
                                       string lastName)
    {
        List<Error> errors = new();

        if (id.Length is > MaxIdLength)
        {
            errors.Add(Errors.Test.InvalidId);
        }

        if (phoneNumber.Length is > MaxPhoneLength)
        {
            errors.Add(Errors.Test.InvalidPhone);
        }

        if (firstName.Length is > MaxFNameLength)
        {
            errors.Add(Errors.Test.InvalidFirstName);
        }

        if (lastName.Length is > MaxLNameLength)
        {
            errors.Add(Errors.Test.InvalidId);
        }

        if (DateTime.Compare(DateTime.Parse(dateOfBirth), DateTime.Parse(MinBirthDate)) < 0 || 
        DateTime.Compare(DateTime.Parse(dateOfBirth), DateTime.Parse(MaxBirthDate)) > 0)
        {
            errors.Add(Errors.Test.InvalidBirthDate);
        }

        if (errors.Count > 0)
        {
            return errors;
        }
        
        return new Test(
            id,
            siteId,
            phoneNumber,
            dateOfBirth,
            firstName,
            lastName,
            DateTime.Now.ToShortDateString());
    }
}
