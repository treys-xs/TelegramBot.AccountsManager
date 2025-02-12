using MediatR;
using Telegram.Bot.Types;
using WebApi.Interfaces;
using WebApi.UpdateHandlers.Message.Commands;

namespace WebApi.UpdateHandlers.Message;

public class TelegramMessageHandler : ITelegramUpdateHandler
{
    private readonly GetTelegramMessageCommand _commands = new();
    
    public async Task HandleAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var command = _commands.Get(update.Message!.Text!.Trim());
        
        await command.ExecuteAsync(mediator, update, cancellationToken);
    }
}