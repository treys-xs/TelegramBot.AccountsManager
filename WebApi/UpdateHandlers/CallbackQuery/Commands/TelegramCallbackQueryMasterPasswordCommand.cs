using MediatR;
using Telegram.Bot.Types;
using Application.Commands.MasterPassword;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.CallbackQuery.Commands;

public class TelegramCallbackQueryMasterPasswordCommand : ITelegramCallbackQueryCommand
{
    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new MasterPasswordCommand()
        {
            ChatId = update.Message!.Chat.Id
        };
            
        await mediator.Send(context, cancellationToken);
    }
}