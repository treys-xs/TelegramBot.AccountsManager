using MediatR;
using Telegram.Bot.Types;
using Application.Commands.GetMainMenu;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.Message.Commands;

public class TelegramMessageGetMainMenuCommand : ITelegramMessageCommand
{
    public bool AuthenticationRequired { get; } = true;

    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new GetMainMenu()
        {
            ChatId = update!.Message!.Chat.Id,
        };

        await mediator.Send(context, cancellationToken);
    }
}