using WebApi.Interfaces;
using WebApi.UpdateHandlers.CallbackQuery.Commands;

namespace WebApi.UpdateHandlers.CallbackQuery;

public class GetTelegramCallbackQueryCommand
{
    private readonly Dictionary<string, ITelegramCallbackQueryCommand> _commands = new()
    {
        { "MasterPassword", new TelegramCallbackQueryMasterPasswordCommand() }
    };

    public ITelegramCallbackQueryCommand Get(string commandString)
    {
        if (!_commands.TryGetValue(commandString, out var command))
            throw new KeyNotFoundException();

        return command!;
    }
}