using WebApi.Configurations.TelegramBot;
using Telegram.Bot;
using Application.Interfaces;
using Infrastructure;
using WebApi.Interfaces;
using WebApi.UpdateHandlers;

var builder = WebApplication.CreateBuilder(args);

var telegramBotConfiguration = builder.Configuration
    .GetSection(TelegramBotConfigurationConstants.ConfigurationSectionName);

builder.Services.Configure<TelegramBotConfiguration>(telegramBotConfiguration);

builder.Services.AddHttpClient(TelegramBotConfigurationConstants.TelegramBotClientName)
    .RemoveAllLoggers()
    .AddTypedClient<ITelegramBotClient>(
        httpClient => new TelegramBotClient(telegramBotConfiguration.Get<TelegramBotConfiguration>()!.Token, httpClient));

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

builder.Services.AddScoped<IGetUpdateHandler, GetUpdateHandler>();

builder.Services.ConfigureTelegramBotMvc();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();