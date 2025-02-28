using Microsoft.EntityFrameworkCore;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Domain;
using Application.Interfaces;
using Application.Enums.UserState;

namespace Application.Commands.Start;

public class StartCommandHandler(
        ITelegramBotClient botClient,
        IApplicationDbContext dbContext) 
    : IRequestHandler<StartCommand>
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly IApplicationDbContext _dbContext = dbContext;
    
    public async Task Handle(StartCommand context, CancellationToken cancellationToken)
    {
        await _botClient.SendMessage(context.ChatId, $"START TEXT", cancellationToken: cancellationToken);

        var user = await _dbContext.Users
            .Include(user => user.State)
            .FirstOrDefaultAsync(user => user.TelegramId == context.ChatId, cancellationToken);

        if (user == null)
        {
            user = new User()
            {
                Id = Guid.CreateVersion7(),
                TelegramId = context.ChatId,
                Nickname = context.Username,
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
                context.ChatId,
                text: $"Необходимо создать мастер-пароль.",
                replyMarkup: new InlineKeyboardMarkup()
                {
                    InlineKeyboard = new List<IEnumerable<InlineKeyboardButton>>()
                    {
                        new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData
                            (
                                "Создать мастер-пароль",
                                "MasterPassword"
                            )
                        },
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