using MediatR;
using Telegram.Bot.Types;
using Application.Interfaces;

namespace WebApi.Interfaces;

public interface ITelegramUpdateHandler
{
    Task HandleAsync(IMediator mediator, IApplicationDbContext dbContext, Update update, 
        CancellationToken cancellationToken);
}