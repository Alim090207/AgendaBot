using AgendaBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;

namespace AgendaBot
{
    public class ServiceHandler : IUpdateHandler
    {
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            bool user1 = await ServicesBot.SearchUserAsync(update.Message.From.Username);
            if (!user1)
            {
                var user = new Users()
                {
                    UserName = update.Message.From.Username,
                    ChatId = update.Message.MessageId,

                };
                var u = await ServicesBot.AddUserAsync(user);
            }
            if (update.Message.Text == "/start")
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: " FAQ (\n/ if u will write note after newtodo it will add new todo,\n / type todos and see all todo)",

                    cancellationToken: cancellationToken);
            }
            else if (update.Message.Text.StartsWith("/newtodo") && update.Message.Text.Replace("/newtodo", " ").Trim().Count() > 0)
            {
                var todo = new Todos()
                {
                    UsersId = await ServicesBot.GetUserByIdAsync(update.Message.From.Username),
                    Description = update.Message.Text.Replace("/newtodo", " "),
                };

                bool res = await ServicesBot.AddTodoAsync(todo);
                if (res)
                { 
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: " Added new todo ;) ",

                        cancellationToken: cancellationToken);
                }
                else
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: " Cannot Add .",

                        cancellationToken: cancellationToken);
                }
            }
            else if (update.Message.Text.StartsWith("/todos"))
            {
                var userid = await ServicesBot.GetUserByIdAsync(update.Message.From.Username);

                var res = await ServicesBot.getAlltodos(userid);
                if (res != null)
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: String.Join(",\n", res.Select(x => x.Description)),

                        cancellationToken: cancellationToken);
                }
                else
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: " No todos found.",

                        cancellationToken: cancellationToken);
                }
            }
            else
            {
                Message message1 = await botClient.SendStickerAsync(
                chatId: update.Message.Chat.Id,
                sticker: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"),
                cancellationToken: cancellationToken);
            }
        }
    }
}
