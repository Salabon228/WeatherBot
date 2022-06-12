public struct Bot
{
    static string token;
    static string baseUri;
    static HttpClient hc = new HttpClient();
    
    public static void Init(string publicToken)
    {
        token = File.ReadAllText("C://token.txt");
        baseUri = $"https://api.telegram.org/bot{token}/";
    }
    
    public static void SendMessage(string id, string text)
    {
        baseUri = $"https://api.telegram.org/bot{token}/";
        string url = $"{baseUri}sendMessage?chat_id={id}&text={text}";
        var req = hc.GetStringAsync(url).Result;
    }

}
// https://api.telegram.org/bot5157919814:AAFIcb8U-7MaGohs2_c9a-8YI-FTIZIfUlk/sendMessage?chat_id=428548883&text=СООБЩЕНИЕ
// отправляет это сообщение адресату