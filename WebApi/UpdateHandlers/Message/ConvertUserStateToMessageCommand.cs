using Application.Enums.UserState;

namespace WebApi.UpdateHandlers.Message;

public class ConvertUserStateToMessageCommand
{
    private readonly Dictionary<UserStates, string> _commands = new()
    {
        { UserStates.Authentication, "/command" }
    };

    public string Get(UserStates userState)
    {
        if (!_commands.TryGetValue(userState, out var command))
            throw new KeyNotFoundException();

        return command!;
    }
}