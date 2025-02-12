using Telegram.Bot.Types.Enums;
using WebApi.Interfaces;
using WebApi.UpdateHandlers.Message;

namespace WebApi.UpdateHandlers;

public class GetUpdateHandler : IGetUpdateHandler
{
    private readonly Dictionary<UpdateType, IUpdateHandler> _handlers;

    public GetUpdateHandler()
    {
        _handlers = new Dictionary<UpdateType, IUpdateHandler>()
        {
            { UpdateType.Message, new MessageHandler() }
        };
    }

    public IUpdateHandler Get(UpdateType updateType)
    {
        return _handlers[updateType];
    }
}