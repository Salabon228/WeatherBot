public struct ModelMessage
{
    public string City; // Город
    public string DtTxT; // Прогнозируемое время и дата
    public string Description;  // Описание "переменная облачность"
    public string Temp; // Температура
    public string Humidity; // Влажность
    public string WindSpeed;    // скорость ветра
    public string WindDeg;  // Направление ветра в градусах
    public string WindGust; // Порывы ветра
    public string Visibility; // Средняя видимость
    public string Pop; // Вероятность осадков от 0 (0)% до 1 (100%)
    
    public static string ToString(ModelMessage model)
    {
        return $"В {model.City} прогноз на {model.DtTxT}:\n{model.Description}\nтемпература = {model.Temp}°С, "+
        $"\nвлажность {model.Humidity}%  \nскорость ветра {model.WindSpeed} м/с порывы до {model.WindGust} м/с"+
        $"\nнаправление ветра {model.WindDeg}° \nвероятность осадков {model.Pop} %"+
        $"\nвидимость не хуже {model.Visibility} м ";                   
        
    }
}