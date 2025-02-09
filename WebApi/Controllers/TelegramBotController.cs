using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using WebApi.Configurations.TelegramBot;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TelegramBotController(IOptions<TelegramBotConfiguration> configuration) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SetWebHook(
        [FromServices] ITelegramBotClient bot, 
        CancellationToken cancellationToken)
    {
        await bot.SetWebhook(configuration.Value.WebHookUrl, allowedUpdates: [], cancellationToken: cancellationToken);
        
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> ReceiveMessage(
        [FromBody] Update update, 
        [FromServices] ITelegramBotClient bot, 
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}