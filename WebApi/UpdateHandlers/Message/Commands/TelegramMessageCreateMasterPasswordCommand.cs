using MediatR;
using Telegram.Bot.Types;
using Application.Commands.MasterPassword.CreateMasterPassword;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.Message.Commands;

public class TelegramMessageCreateMasterPasswordCommand : ITelegramMessageCommand
{
    public bool AuthenticationRequired { get; } = false;

    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new CreateMasterPasswordCommand()
        {
            ChatId = update!.Message!.Chat.Id,
            StepData = update.Message!.Text!.Trim()
        };

        await mediator.Send(context, cancellationToken);
    }
}