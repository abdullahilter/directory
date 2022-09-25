using System.ComponentModel.DataAnnotations;

namespace entity.contact;

public record CreateCommunicationRequest(
    [Required] Guid ContactId,
    [Required][Range(1, 3)] int TypeId,
    [Required][MaxLength(4000)] string Content);