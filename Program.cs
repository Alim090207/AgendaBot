using AgendaBot;
using AgendaBot.Data;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
var token = builder.Configuration["token"];

builder.Services.AddSingleton(new TelegramBotClient(token));
builder.Services.AddHostedService<BackGround>();
builder.Services.AddDbContext<DbData>();

var app = builder.Build();

app.Run();
