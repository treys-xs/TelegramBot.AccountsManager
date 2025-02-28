using MediatR;
using Telegram.Bot.Types;
using Application.Commands.GetMainMenu;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.CallbackQuery.Commands;

public class TelegramCallbackQueryGetMainMenuCommand : ITelegramCallbackQueryCommand
{
    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new GetMainMenu()
        {
            ChatId = update!.CallbackQuery!.Message!.Chat.Id
        };
            
        await mediator.Send(context, cancellationToken);
    }
}