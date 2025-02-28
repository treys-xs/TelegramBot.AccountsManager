using MediatR;
using Telegram.Bot;

namespace Application.Commands.GetMainMenu;

public class GetMainMenuHandler(
        ITelegramBotClient botClient) 
    : IRequestHandler<GetMainMenu>
{
    private readonly ITelegramBotClient _botClient = botClient;
    
    public async Task Handle(GetMainMenu context, CancellationToken cancellationToken)
    {
        await _botClient.SendMessage(context.ChatId, "Главное меню", cancellationToken: cancellationToken);
    }
}