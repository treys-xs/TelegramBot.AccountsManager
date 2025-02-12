using Telegram.Bot.Types.Enums;

namespace WebApi.Interfaces;

public interface IGetTelegramUpdateHandler
{ 
    ITelegramUpdateHandler Get(UpdateType updateType);
}