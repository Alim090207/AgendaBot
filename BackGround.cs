
using Telegram.Bot;

namespace AgendaBot
{
    public class BackGround : BackgroundService
    {
        private TelegramBotClient client;

        public BackGround(TelegramBotClient client)
        {
            this.client = client;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("nma gaaap");
            client.StartReceiving<ServiceHandler>(null, stoppingToken);
            return Task.CompletedTask;
        }
    }
}
