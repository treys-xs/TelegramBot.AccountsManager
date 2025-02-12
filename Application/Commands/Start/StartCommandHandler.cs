using MediatR;
using Telegram.Bot;

namespace Application.Commands.Start;

public class StartCommandHandler(ITelegramBotClient botClient) : IRequestHandler<StartCommand>
{
    private readonly ITelegramBotClient _botClient = botClient;
    
    public async Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        await _botClient.SendMessage(request.ChatId, $"Hello, {request.Username}");
    }
}