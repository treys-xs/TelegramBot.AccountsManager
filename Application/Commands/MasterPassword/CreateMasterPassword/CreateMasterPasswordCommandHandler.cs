using Microsoft.EntityFrameworkCore;
using MediatR;
using Telegram.Bot;
using Application.Interfaces;
using Application.Enums.UserState;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Commands.MasterPassword.CreateMasterPassword;

public class CreateMasterPasswordCommandHandler(
        IApplicationDbContext dbContext,
        ITelegramBotClient botClient) 
    : IRequestHandler<CreateMasterPasswordCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ITelegramBotClient _botClient = botClient;
    
    public async Task Handle(CreateMasterPasswordCommand context, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.State)
            .FirstOrDefaultAsync(user => user.TelegramId == context.ChatId,  cancellationToken);
        
        if (user!.State!.Step == null)
        {
            user.State.Name = (int)UserStates.CreateMasterPassword;
            user.State.Step = (int)MasterPasswordStates.SetPassword;
            
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await _botClient.SendMessage(user.TelegramId, "Пожалуйста, введите мастер-пароль.", 
                cancellationToken: cancellationToken);
            
            return;
        }

        if (user!.State!.Step == (int)MasterPasswordStates.SetPassword)
        {
            user.Password = context.StepData;

            user.State.Name = null;
            user.State.Step = null;
            
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await _botClient.SendMessage(
                user.TelegramId, 
                "Мастер-пароль успешно установлен.",  
                replyMarkup: new InlineKeyboardMarkup()
                {
                    InlineKeyboard = new List<IEnumerable<InlineKeyboardButton>>()
                    {
                        new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData
                            (
                                "Перейти в главное меню.",
                                "GetMainMenu"
                            )
                        },
                    }
                }, 
                cancellationToken: cancellationToken);
        }
    }
}