using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.Message.Commands;

public class GetTelegramMessageCommand
{
    private readonly Dictionary<string, ITelegramMessageCommand> _commands = new()
    {
        { "/start", new TelegramMessageStartCommand() }
    };

    public ITelegramMessageCommand Get(string commandString)
    {
        if (!_commands.TryGetValue(commandString, out var command))
            throw new KeyNotFoundException();

        return command!;
    }
}