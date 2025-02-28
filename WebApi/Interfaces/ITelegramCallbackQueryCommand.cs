using MediatR;
using Telegram.Bot.Types;

namespace WebApi.Interfaces;

public interface ITelegramCallbackQueryCommand
{
    Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken);
}