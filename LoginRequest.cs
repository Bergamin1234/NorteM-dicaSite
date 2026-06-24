using System.ComponentModel.DataAnnotations;

namespace Nortemedica.API.Contracts.Authentication;

public record LoginRequest(
    [Required][EmailAddress] string Email,
    [Required] string Password
);