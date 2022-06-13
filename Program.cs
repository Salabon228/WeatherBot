using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    private static async Task Main(string[] args)
    {
        
        
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

            Console.WriteLine($"Пришло сообщение '{messageText}' в чат {chatId}.");

            await botClient.SendTextMessageAsync(chatId, messageText, replyMarkup: GetButtons());

            List<ModelMessage> GetUrl(string zip)
            {
                string token_weater = System.IO.File.ReadAllText("C://token_weather.txt");       
                string url = $"http://api.openweathermap.org/data/2.5/forecast?zip={zip},ru&appid={token_weater}&cnt=3&units=metric&lang=ru";
                HttpClient hc = new HttpClient();
                string json = hc.GetStringAsync(url).Result; // здесь ответ от сервера
                JsonParse.Init(json);
                List<ModelMessage> msgs = JsonParse.Parse();
                return msgs;
            }
            

            
            switch (messageText)
            {
                case "Бологое":
                    List<ModelMessage> perem = GetUrl("171080");
                    for (int i = 0; i < perem.Count; i++)
                    {
                       
                        string msgForBot = ModelMessage.ToString(perem[i]);

                    // Echo received message text
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: msgForBot,
                        // parseMode: ParseMode.MarkdownV2,
                        disableNotification: true,
                        cancellationToken: cancellationToken);
                        
                    }
                    break;
                case "Ярославль":
                    perem = GetUrl("150010");
                    for (int i = 0; i < perem.Count; i++)
                    {
                       
                        string msgForBot = ModelMessage.ToString(perem[i]);

                    // Echo received message text
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: msgForBot,
                        // parseMode: ParseMode.MarkdownV2,
                        disableNotification: true,
                        cancellationToken: cancellationToken);
                        
                    }
                    break;
                case "Хотилово":
                    perem = GetUrl("171098");
                    for (int i = 0; i < perem.Count; i++)
                    {
                       
                        string msgForBot = ModelMessage.ToString(perem[i]);

                    // Echo received message text
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: msgForBot,
                        // parseMode: ParseMode.MarkdownV2,
                        disableNotification: true,
                        cancellationToken: cancellationToken);
                        
                    }
                    break;
                case "Москва":
                    perem = GetUrl("101000");
                    for (int i = 0; i < perem.Count; i++)
                    {
                       
                        string msgForBot = ModelMessage.ToString(perem[i]);

                    // Echo received message text
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: msgForBot,
                        // parseMode: ParseMode.MarkdownV2,
                        disableNotification: true,
                        cancellationToken: cancellationToken);
                        
                    }
                    break;
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

    private static IReplyMarkup GetButtons()
    {
        return new ReplyKeyboardMarkup("1")
        {
            Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{ new KeyboardButton("weather") { Text = "Хотилово"}, new KeyboardButton("3") { Text = "Бологое"}},
                new List<KeyboardButton>{ new KeyboardButton("2") { Text = "Ярославль"}, new KeyboardButton("3") { Text = "Москва"}}
            }

        };
        
    }


}