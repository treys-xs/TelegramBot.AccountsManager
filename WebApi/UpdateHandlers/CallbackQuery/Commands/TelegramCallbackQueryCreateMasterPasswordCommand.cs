using MediatR;
using Telegram.Bot.Types;
using Application.Commands.MasterPassword.CreateMasterPassword;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.CallbackQuery.Commands;

public class TelegramCallbackQueryCreateMasterPasswordCommand : ITelegramCallbackQueryCommand
{
    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new CreateMasterPasswordCommand()
        {
            ChatId = update!.CallbackQuery!.Message!.Chat.Id
        };
            
        await mediator.Send(context, cancellationToken);
    }
}