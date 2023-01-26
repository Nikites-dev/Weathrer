using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAppServer;

public class WeatherForecast
{
    [BsonIgnoreIfDefault]
    public ObjectId id;
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}