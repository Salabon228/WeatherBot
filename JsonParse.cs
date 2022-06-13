using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public struct JsonParse
{
    static string json;
    public static string Json { get => json; set => json = value; }
    public static void Init(string jsonString)
    {
        Json = jsonString;
    }

    public static List<ModelMessage> Parse()
    {
        List<ModelMessage> messagers = new();
        JObject result = JObject.Parse(Json);
        JToken list = result["list"]!;
        JToken city = result["city"]!;
        foreach (JToken zapros in list)
        {
            
            ModelMessage mm = new();
            mm.Temp = zapros["main"]!["temp"]!.ToString();
            mm.Humidity = zapros["main"]!["humidity"]!.ToString();
            mm.DtTxT = zapros["dt_txt"]!.ToString();
            mm.City = city["name"]!.ToString();
            mm.WindSpeed = zapros["wind"]!["speed"]!.ToString();
            mm.WindDeg = zapros["wind"]!["deg"]!.ToString();
            mm.WindGust = zapros["wind"]!["gust"]!.ToString();
            mm.Pop = zapros["pop"]!.ToString();
            mm.Visibility = zapros["visibility"]!.ToString();            
            foreach (JToken item in zapros["weather"]!)
            {
                mm.Description = item["description"]!.ToString();
            }
            messagers.Add(mm);
        }
        return messagers;
    }

}


