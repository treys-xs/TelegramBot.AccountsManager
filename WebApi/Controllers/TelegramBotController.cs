using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebApi.Configurations.TelegramBot;
using WebApi.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TelegramBotController(
    IOptions<TelegramBotConfiguration> configuration,
    IGetTelegramUpdateHandler telegramUpdateHandler,
    ITelegramBotClient botClient,
    IMediator mediator,
    IApplicationDbContext dbContext)
    : ControllerBase
{
    private readonly IOptions<TelegramBotConfiguration> _configuration = configuration;
    private readonly IGetTelegramUpdateHandler _telegramUpdateHandler = telegramUpdateHandler;
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly IMediator _mediator = mediator;
    private readonly IApplicationDbContext _dbContext = dbContext;
    
    [HttpGet]
    public async Task<IActionResult> SetWebHook(CancellationToken cancellationToken)
    {
        await _botClient.SetWebhook(_configuration.Value.WebHookUrl, 
            allowedUpdates: [], cancellationToken: cancellationToken);
        
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> ReceiveMessage(
        [FromBody] Update update, 
        CancellationToken cancellationToken)
    {
        try
        {
            var handler = _telegramUpdateHandler.Get(update.Type);
            
            await handler.HandleAsync(_mediator, _dbContext, update, cancellationToken);
        }
        catch (Exception ex)
        {
        }

        return Ok();
    }
}