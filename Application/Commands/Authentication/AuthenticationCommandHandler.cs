using MediatR;
using Telegram.Bot;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Commands.Authentication;

public class AuthenticationCommandHandler(
        ITelegramBotClient botClient,
        IApplicationDbContext dbContext)  
    : IRequestHandler<AuthenticationCommand>
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly IApplicationDbContext _dbContext = dbContext;
    
    
    public async Task Handle(AuthenticationCommand context, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.State)
            .FirstOrDefaultAsync(user => user.TelegramId == context.ChatId, cancellationToken);

        if (user!.Password != context.MasterPassword)
        {
            await _botClient.SendMessage(user.TelegramId, "Неверный пароль, попробуйте ещё раз.", 
                cancellationToken: cancellationToken);
            
            return;
        }

        user!.State!.Name = null;
        user!.State!.Step = null;

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _botClient.SendMessage(
            user.TelegramId,
            "Вы успешно авторизовались.",
            replyMarkup: new InlineKeyboardMarkup()
            {
                InlineKeyboard = new List<IEnumerable<InlineKeyboardButton>>()
                {
                    new List<InlineKeyboardButton>()
                    {
                        InlineKeyboardButton.WithCallbackData
                        (
                            "Перейти в главное меню.",
                            "MainMenu"
                        )
                    },
                }
            },
            cancellationToken: cancellationToken);
    }
}