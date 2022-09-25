using api.contact.Controllers;
using Bogus;
using entity.contact;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using service.contact;

namespace contact.UnitTests;

public class ContactsControllerTests
{
    private readonly Mock<IContactService> _contactServiceMock = new Mock<IContactService>();

    [Fact]
    public async Task GetAsync_WithUnexisting_ReturnsNotFound()
    {
        // Arrange
        _contactServiceMock
            .Setup(service => service.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
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
        var expected = CreateRandomContactDetail();

        _contactServiceMock
            .Setup(service => service.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(expected);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.GetAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actual = okResult.Value as ContactDetailResponse;
        actual.Should().BeEquivalentTo(expected);
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
            .Setup(service => service.GetAllAsync(CancellationToken.None))
            .ReturnsAsync(expecteds);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;
        var actual = okResult.Value as IReadOnlyCollection<Contact>;
        actual.Should().BeEquivalentTo(expecteds);
    }

    [Fact]
    public async Task CreateAsync_WithCreateRequest_ReturnsCreatedItem()
    {
        // Arrange
        var request = new CreateContactRequest("lorem", "ipsum", "acme");
        var expected = request.Adapt<Contact>();

        _contactServiceMock
            .Setup(service => service.CreateAsync(request, CancellationToken.None))
            .ReturnsAsync(expected);

        var controller = new ContactsController(_contactServiceMock.Object);

        // Act
        var result = await controller.CreateAsync(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedResult>();

        var actual = (result as CreatedResult).Value as Contact;

        request.Should().BeEquivalentTo(actual, options => options.ExcludingMissingMembers());
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

    private ContactDetailResponse CreateRandomContactDetail()
    {
        var contactDetail = new Faker<ContactDetailResponse>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.Name, f => f.Person.FirstName)
            .RuleFor(x => x.Surname, f => f.Person.LastName)
            .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
            .Generate();

        var communication = new Faker<ContactCommunicationDto>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.TypeId, f => f.PickRandom(
                (int)CommunicationType.TelephoneNumber, (int)CommunicationType.EMailAddress, (int)CommunicationType.Location))
            .RuleFor(x => x.Content, f => f.Random.ToString())
            .Generate();

        contactDetail.Communications = new List<ContactCommunicationDto>() { communication };

        return contactDetail;
    }

    private Contact CreateRandomContact()
    {
        var contact = new Faker<Contact>()
           .RuleFor(x => x.Id, f => Guid.NewGuid())
           .RuleFor(x => x.Name, f => f.Person.FirstName)
           .RuleFor(x => x.Surname, f => f.Person.LastName)
           .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
           .Generate();

        return contact;
    }
}