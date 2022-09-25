using System.ComponentModel.DataAnnotations;

namespace entity.contact;

public record CreateContactRequest(
    [Required][MaxLength(250)] string Name,
    [Required][MaxLength(250)] string Surname,
    [Required][MaxLength(500)] string CompanyName);