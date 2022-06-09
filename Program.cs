using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

string url = "http://api.openweathermap.org/data/2.5/forecast?zip=171098,ru&appid=6f9b33ecd53f0c1be1b22830785323c9&cnt=40&units=metric&lang=ru";

HttpClient hc = new HttpClient();

string json = hc.GetStringAsync(url).Result; // здесь ответ от сервера

JsonParse.Init(json);
List<ModelMessage> msgs = JsonParse.Parse();

for (int i = 0; i < msgs.Count; i++)
{
    System.Console.WriteLine(ModelMessage.ToString(msgs[i]));
    System.Console.WriteLine();
}







