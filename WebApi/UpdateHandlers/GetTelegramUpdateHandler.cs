using Telegram.Bot.Types.Enums;
using WebApi.Interfaces;
using WebApi.UpdateHandlers.CallbackQuery;
using WebApi.UpdateHandlers.Message;

namespace WebApi.UpdateHandlers;

public class GetTelegramUpdateHandler : IGetTelegramUpdateHandler
{
    private readonly Dictionary<UpdateType, ITelegramUpdateHandler> _handlers = new()
    {
        { UpdateType.Message, new TelegramMessageHandler() },
        { UpdateType.CallbackQuery, new TelegramCallbackQueryHandler() },
    };

    public ITelegramUpdateHandler Get(UpdateType updateType)
    {
        if (!_handlers.TryGetValue(updateType, out var handler))
            throw new KeyNotFoundException();
        
        return handler!;
    }
}