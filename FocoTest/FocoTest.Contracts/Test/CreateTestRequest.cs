namespace FocoTest.Contracts.Test{}

public record CreateTestRequest
(
    string Id,
    string SiteId,
    string PhoneNumber,
    string DateOfBirth,
    string FirstName,
    string LastName
);
