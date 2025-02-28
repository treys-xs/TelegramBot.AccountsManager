using MediatR;
using Telegram.Bot.Types;
using WebApi.Interfaces;
using Application.Commands.Authentication;

namespace WebApi.UpdateHandlers.Message.Commands;

public class TelegramMessageAuthenticationCommand : ITelegramMessageCommand
{
    public bool AuthenticationRequired { get; } = false;

    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new AuthenticationCommand()
        {
            MasterPassword = update.Message!.Text!.Trim()
        };

        await mediator.Send(context, cancellationToken);
    }
}