using api.contact.Controllers;
using entity.contact;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using service.contact;
using System.Collections.Generic;

namespace contact.UnitTests;

public class ContactsControllerTests
{
    private readonly Mock<IContactService> _contactServiceMock = new Mock<IContactService>();

    [Fact]
    public async Task GetAsync_WithUnexisting_ReturnsNotFound()
    {
        // Arrange
        _contactServiceMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync((ContactDetailResponse)null);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.GetAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAsync_WithExisting_ReturnsExpectedContactDetail()
    {
        // Arrange
        var expected = CreateRandomContactDetailResponse();

        _contactServiceMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(expected);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.GetAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actual = okResult.Value as ContactDetailResponse;
        actual.Should().BeEquivalentTo(expected, options => options.ComparingByMembers<ContactDetailResponse>());
    }

    [Fact]
    public async Task GetAllAsync_WithExisting_ReturnsAll()
    {
        // Arrange
        IReadOnlyCollection<Contact> expecteds = new[] {
            CreateRandomContact(),
            CreateRandomContact(),
            CreateRandomContact() };

        _contactServiceMock
            .Setup(repo => repo.GetAllAsync(CancellationToken.None))
            .ReturnsAsync(expecteds);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actual = okResult.Value as IReadOnlyCollection<Contact>;
        actual.Should().BeEquivalentTo(expecteds, options => options.ComparingByMembers<Contact>());
    }

    [Fact]
    public async Task CreateAsync_WithCreateRequest_ReturnsCreatedItem()
    {
        // Arrange
        var request = new CreateContactRequest("lorem", "ipsum", "acme");
        var expected = request.Adapt<Contact>();

        _contactServiceMock
            .Setup(repo => repo.CreateAsync(request, CancellationToken.None))
            .ReturnsAsync(expected);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.CreateAsync(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedResult>();

        var actual = (result as CreatedResult).Value as Contact;

        request.Should().BeEquivalentTo(actual, options => options.ComparingByMembers<Contact>().ExcludingMissingMembers());
    }

    [Fact]
    public async Task DeleteAsync_WithExisting_ReturnsNoContent()
    {
        // Arrange
        var existing = CreateRandomContact();

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.DeleteAsync(existing.Id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    private ContactDetailResponse CreateRandomContactDetailResponse()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = "Abdullah",
            Surname = "ÝLTER",
            CompanyName = "ACME CORP",
            Communications = new List<ContactCommunicationDto>()
            {
                new ContactCommunicationDto()
                {
                    Id = Guid.NewGuid(),
                    TypeId = 1,
                    Content = "5554443322"
                }
            }
        };
    }

    private Contact CreateRandomContact()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = "Abdullah",
            Surname = "ÝLTER",
            CompanyName = "ACME CORP"
        };
    }
}