using MediatR;
using Telegram.Bot.Types;
using Application.Interfaces;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.CallbackQuery;

public class TelegramCallbackQueryHandler : ITelegramUpdateHandler
{
    private readonly GetTelegramCallbackQueryCommand _commands = new();
    
    public async Task HandleAsync(IMediator mediator, IApplicationDbContext dbContext, Update update, 
        CancellationToken cancellationToken)
    {
        var commandName = update!.CallbackQuery!.Data;
        
        var command = _commands.Get(commandName!);
        
        await command.ExecuteAsync(mediator, update, cancellationToken);
    }
}