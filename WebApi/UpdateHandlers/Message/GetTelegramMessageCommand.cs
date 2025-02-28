using WebApi.Interfaces;
using WebApi.UpdateHandlers.Message.Commands;

namespace WebApi.UpdateHandlers.Message;

public class GetTelegramMessageCommand
{
    private readonly Dictionary<string, ITelegramMessageCommand> _commands = new()
    {
        { "/start", new TelegramMessageStartCommand() },
        { "/authentication", new TelegramMessageAuthenticationCommand() },
        { "/createMasterPassword", new TelegramMessageCreateMasterPasswordCommand() },
        { "/getMainMenu", new TelegramMessageGetMainMenuCommand() },
    };

    public ITelegramMessageCommand Get(string commandString)
    {
        if (!_commands.TryGetValue(commandString, out var command))
            throw new KeyNotFoundException();

        return command!;
    }
}