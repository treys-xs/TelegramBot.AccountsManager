using MediatR;

namespace Application.Commands.Authentication;

public class AuthenticationCommand : IRequest
{
    public long ChatId { get; set; }
    public string MasterPassword { get; set; } = string.Empty;
}