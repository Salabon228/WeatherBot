using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string token_weater = File.ReadAllText("C://token_weather.txt");
string url = $"http://api.openweathermap.org/data/2.5/forecast?zip=171098,ru&appid={token_weater}&cnt=40&units=metric&lang=ru";

HttpClient hc = new HttpClient();

string json = hc.GetStringAsync(url).Result; // здесь ответ от сервера

JsonParse.Init(json);
List<ModelMessage> msgs = JsonParse.Parse();

for (int i = 0; i < msgs.Count; i++)
{
    System.Console.WriteLine(ModelMessage.ToString(msgs[i]));
    System.Console.WriteLine();
}







