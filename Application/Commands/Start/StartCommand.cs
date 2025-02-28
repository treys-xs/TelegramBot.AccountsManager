using MediatR;

namespace Application.Commands.Start;

public class StartCommand : IRequest
{
    public long ChatId { get; set; }
    public string Username { get; set; } = string.Empty;
}