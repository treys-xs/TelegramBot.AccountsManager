using Telegram.Bot.Types.Enums;

namespace WebApi.Interfaces;

public interface IGetUpdateHandler
{ 
    IUpdateHandler Get(UpdateType updateType);
}