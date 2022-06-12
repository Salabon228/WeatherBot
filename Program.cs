// using Newtonsoft;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using Telegram.Bot;

// string token_weater = File.ReadAllText("C://token_weather.txt");
// string tokenBot = File.ReadAllText("C://token.txt");
// string url = $"http://api.openweathermap.org/data/2.5/forecast?zip=171098,ru&appid={token_weater}&cnt=3&units=metric&lang=ru";


// HttpClient hc = new HttpClient();

// string json = hc.GetStringAsync(url).Result; // здесь ответ от сервера

// JsonParse.Init(json);
// List<ModelMessage> msgs = JsonParse.Parse();

// Bot.Init(tokenBot);

//     for (int i = 0; i < msgs.Count; i++)
//     {
//         System.Console.WriteLine(ModelMessage.ToString(msgs[i]));
//         System.Console.WriteLine();
//         string msgForBot = ModelMessage.ToString(msgs[i]);
//         Bot.SendMessage("428548883", msgForBot);
//         Thread.Sleep(1000);
//     }

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


internal class Program
{
    private static async Task Main(string[] args)
    {
        string token_weater = System.IO.File.ReadAllText("C://token_weather.txt");
        
        string url = $"http://api.openweathermap.org/data/2.5/forecast?zip=171098,ru&appid={token_weater}&cnt=3&units=metric&lang=ru";

        HttpClient hc = new HttpClient();

        string json = hc.GetStringAsync(url).Result; // здесь ответ от сервера

        JsonParse.Init(json);

        List<ModelMessage> msgs = JsonParse.Parse();
        
        var botClient = new TelegramBotClient(System.IO.File.ReadAllText("C://token.txt"));

        using var cts = new CancellationTokenSource();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };
        botClient.StartReceiving(updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token);

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        // Send cancellation request to stop bot
        cts.Cancel();





        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type != UpdateType.Message)
                return;
            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            for (int i = 0; i < msgs.Count; i++)
            {
                // System.Console.WriteLine(ModelMessage.ToString(msgs[i]));
                // System.Console.WriteLine();
                string msgForBot = ModelMessage.ToString(msgs[i]);
                // Thread.Sleep(1000);
            

            // Echo received message text
            Message sendMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: msgForBot,
                cancellationToken: cancellationToken);
            }
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}