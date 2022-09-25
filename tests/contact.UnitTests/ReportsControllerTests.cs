using api.contact.Controllers;
using Bogus;
using entity.contact;
using FluentAssertions;
using Moq;
using service.contact;

namespace contact.UnitTests;

public class ReportsControllerTests
{
    private readonly Mock<ICommunicationService> _communicationServiceMock = new Mock<ICommunicationService>();

    [Fact]
    public async Task GetReportAsync_ReturnsData()
    {
        // Arrange
        var expecteds = CreateRandomContactReportResponse();

        _communicationServiceMock
            .Setup(service => service.GetReportAsync(CancellationToken.None))
            .ReturnsAsync(expecteds);

        var controller = new ReportsController(_communicationServiceMock.Object);

        // Act
        var result = await controller.GetReportAsync(CancellationToken.None);

        // Assert
        result.Should().BeOfType<List<ContactReportResponse>>();

        result.Should().BeEquivalentTo(expecteds);
    }

    private List<ContactReportResponse> CreateRandomContactReportResponse()
    {
        var results = new Faker<ContactReportResponse>()
           .RuleFor(x => x.Location, f => f.Random.Word())
           .RuleFor(x => x.ContactCountInThatLocation, f => f.Random.Number())
           .RuleFor(x => x.CommunicationCountInThatLocation, f => f.Random.Number())
           .Generate(5);

        return results;
    }
}