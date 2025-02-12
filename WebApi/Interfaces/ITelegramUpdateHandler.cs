using MediatR;
using Telegram.Bot.Types;

namespace WebApi.Interfaces;

public interface ITelegramUpdateHandler
{
    Task HandleAsync(IMediator mediator,Update update, CancellationToken cancellationToken);
}