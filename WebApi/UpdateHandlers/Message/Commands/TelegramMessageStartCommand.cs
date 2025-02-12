using MediatR;
using Telegram.Bot.Types;
using WebApi.Interfaces;
using Application.Commands.Start;

namespace WebApi.UpdateHandlers.Message.Commands;

public class TelegramMessageStartCommand : ITelegramMessageCommand
{
    public async Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken)
    {
        var context = new StartCommand()
        {
            ChatId = update!.Message!.Chat.Id,
            Username = update!.Message!.Chat.Username
        };
        
        await mediator.Send(context);
    }
}