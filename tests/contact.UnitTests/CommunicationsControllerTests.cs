using api.contact.Controllers;
using Bogus;
using entity.contact;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using service.contact;

namespace contact.UnitTests;

public class CommunicationsControllerTests
{
    private readonly Mock<ICommunicationService> _communicationServiceMock = new Mock<ICommunicationService>();

    [Fact]
    public async Task CreateAsync_WithCreateRequest_ReturnsCreatedItem()
    {
        // Arrange
        var request = new CreateCommunicationRequest(Guid.NewGuid(), (int)CommunicationType.Location, "acme");
        var expected = request.Adapt<Communication>();

        _communicationServiceMock
            .Setup(service => service.CreateAsync(request, CancellationToken.None))
            .ReturnsAsync(expected);

        var controller = new CommunicationsController(_communicationServiceMock.Object);

        // Act
        var result = await controller.CreateAsync(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedResult>();

        var actual = (result as CreatedResult).Value as Communication;

        request.Should().BeEquivalentTo(actual, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteAsync_WithExisting_ReturnsNoContent()
    {
        // Arrange
        var existing = CreateRandomCommunication();

        var controller = new CommunicationsController(_communicationServiceMock.Object);

        // Act
        var result = await controller.DeleteAsync(existing.Id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    private Communication CreateRandomCommunication()
    {
        var communication = new Faker<Communication>()
           .RuleFor(x => x.Id, f => Guid.NewGuid())
           .RuleFor(x => x.ContactId, f => Guid.NewGuid())
           .RuleFor(x => x.TypeId, f => f.PickRandom(
                (int)CommunicationType.TelephoneNumber, (int)CommunicationType.EMailAddress, (int)CommunicationType.Location))
           .RuleFor(x => x.Content, f => f.Random.ToString())
           .Generate();

        return communication;
    }
}