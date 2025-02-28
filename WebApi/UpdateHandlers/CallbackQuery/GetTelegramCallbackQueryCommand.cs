using WebApi.Interfaces;
using WebApi.UpdateHandlers.CallbackQuery.Commands;

namespace WebApi.UpdateHandlers.CallbackQuery;

public class GetTelegramCallbackQueryCommand
{
    private readonly Dictionary<string, ITelegramCallbackQueryCommand> _commands = new()
    {
        { "CreateMasterPassword", new TelegramCallbackQueryCreateMasterPasswordCommand() },
        { "GetMainMenu", new TelegramCallbackQueryGetMainMenuCommand() },
    };

    public ITelegramCallbackQueryCommand Get(string commandString)
    {
        if (!_commands.TryGetValue(commandString, out var command))
            throw new KeyNotFoundException();

        return command!;
    }
}