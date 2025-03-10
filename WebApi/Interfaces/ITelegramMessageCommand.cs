﻿using MediatR;
using Telegram.Bot.Types;

namespace WebApi.Interfaces;

public interface ITelegramMessageCommand
{
    public bool AuthenticationRequired { get; } 
    
    Task ExecuteAsync(IMediator mediator, Update update, CancellationToken cancellationToken);
}