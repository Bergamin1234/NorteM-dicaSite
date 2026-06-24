namespace Nortemedica.API.Contracts.Authentication;

public record AuthResponse(
    string Token,
    string Email
);