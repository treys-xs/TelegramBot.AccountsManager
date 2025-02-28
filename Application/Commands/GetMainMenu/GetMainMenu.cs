using MediatR;

namespace Application.Commands.GetMainMenu;

public class GetMainMenu : IRequest
{
    public long ChatId { get; set; }
}