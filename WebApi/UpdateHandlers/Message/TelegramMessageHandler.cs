using Application.Commons.Exceptions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Telegram.Bot.Types;
using Application.Interfaces;
using Application.Enums.UserState;
using WebApi.Interfaces;

namespace WebApi.UpdateHandlers.Message;

public class TelegramMessageHandler : ITelegramUpdateHandler
{
    private readonly GetTelegramMessageCommand _commands = new();
    private readonly ConvertUserStateToMessageCommand _convertToCommand = new();
    
    public async Task HandleAsync(IMediator mediator,  IApplicationDbContext dbContext, Update update, 
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Include(user => user.State)
            .FirstOrDefaultAsync(user => user.TelegramId == update!.Message!.Chat.Id, cancellationToken);
        
        var commandName = user != null && user!.State!.Name != null 
                ? _convertToCommand.Get((UserStates)user!.State!.Name)
                : update.Message!.Text!.Trim();
        
        var command = _commands.Get(commandName);

        if (command.AuthenticationRequired)
        {
            if (user == null)
                throw new NotFoundUserException();
            
            if(!user.IsAuthenticated)
                throw new NotAuthenticationUserException();
        }
        
        await command.ExecuteAsync(mediator, update, cancellationToken);
    }
}