using System.ComponentModel.DataAnnotations;

namespace api.contact.Requests;

public record CreateContactRequest(
    [Required][MaxLength(250)] string Name,
    [Required][MaxLength(250)] string Surname,
    [Required][MaxLength(500)] string CompanyName);