using Application.Enums.UserState;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Application.Interfaces;
using Domain;

namespace Application.Commands.Start;

public class StartCommandHandler(
        ITelegramBotClient botClient,
        IApplicationDbContext dbContext) 
    : IRequestHandler<StartCommand>
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly IApplicationDbContext _dbContext = dbContext;
    
    public async Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        await _botClient.SendMessage(request.ChatId, $"START TEXT", cancellationToken: cancellationToken);

        var user = await _dbContext.Users
            .Include(user => user.State)
            .FirstOrDefaultAsync(user => user.TelegramId == request.ChatId, cancellationToken);

        if (user == null)
        {
            user = new User()
            {
                Id = Guid.CreateVersion7(),
                TelegramId = request.ChatId,
                Nickname = request.Username,
            };

            var userState = new UserState()
            {
                Id = Guid.CreateVersion7(),
                User = user
            };
            
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.UserStates.AddAsync(userState, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        if (user.Password == null)
        {
            await _botClient.SendMessage(
                request.ChatId,
                text: $"РЕКОМЕНДУЕМ СОЗДАТЬ МАСТЕР ПАРОЛЬ",
                replyMarkup: new InlineKeyboardMarkup()
                {
                    InlineKeyboard = new List<IEnumerable<InlineKeyboardButton>>()
                    {
                        new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData
                            (
                                "Создать мастер-пароль",
                                "CreateMasterPassword"
                            )
                        },
                        new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData
                            (
                                "Нет, спасибо.",
                                "MainMenu"
                            )
                        }
                    }
                },
                cancellationToken: cancellationToken
            );
            
            return;
        }

        user!.State!.Name = (int)UserStates.Authentication;
        
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}