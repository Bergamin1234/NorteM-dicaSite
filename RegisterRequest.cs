using System.ComponentModel.DataAnnotations;

namespace Nortemedica.API.Contracts.Authentication;

public record RegisterRequest(
    [Required] string Cnpj,
    [Required] string RazaoSocial,
    [Required][EmailAddress] string Email,
    [Required] string Password
);