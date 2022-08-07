namespace FocoTest.Contracts.Test{}

public record CreateTestRequest
(
    string Id,
    int SiteId,
    string PhoneNumber,
    string DateOfBirth,
    string FirstName,
    string LastName
);
