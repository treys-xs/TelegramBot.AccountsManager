using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebApi.Configurations.TelegramBot;
using WebApi.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TelegramBotController : ControllerBase
{
    private readonly IOptions<TelegramBotConfiguration> _configuration;
    private readonly IGetUpdateHandler _updateHandlers;
    private readonly ITelegramBotClient _botClient;

    public TelegramBotController(
        IOptions<TelegramBotConfiguration> configuration,
        IGetUpdateHandler updateHander,
        ITelegramBotClient botClient)
    {
        _configuration = configuration;
        _updateHandlers = updateHander;
        _botClient = botClient;
    }
    
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
        var handler = _updateHandlers.Get(update.Type);
        
        await handler.HandleAsync(_botClient, update, cancellationToken);
        
        return Ok();
    }
}