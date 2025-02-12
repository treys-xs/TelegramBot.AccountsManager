using Telegram.Bot;
using Telegram.Bot.Types;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.Message;

public class MessageHandler : IUpdateHandler
{
    public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}