using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebApi.Interfaces;

public interface IUpdateHandler
{
    Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}