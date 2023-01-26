using MongoDB.Bson;
using MongoDB.Driver;

namespace WebAppServer.MongoDb;

public class MongoDBAction
{
        public static void AddToDatabase(WeatherForecast quest)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("WeatherForecast");
            
       
            var collection = database.GetCollection<WeatherForecast>("WeatherData");
            collection.InsertOne(quest);
        }

        public static void DeleteById(String id)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("WeatherForecast");
            var collection = database.GetCollection<WeatherForecast>("WeatherData");
            var unit = collection.DeleteOne(x => x.id == ObjectId.Parse(id));
        }

        public static List<WeatherForecast> GetListWeathers()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("WeatherForecast");
            var collection = database.GetCollection<WeatherForecast>("WeatherData");
            var strNames = collection.Find<WeatherForecast>(x => x.TemperatureC != null).ToList<WeatherForecast>();
            return strNames;
        }
    
        public static void UpdateByName(String id, WeatherForecast fs)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("WeatherForecast");
            var collection = database.GetCollection<WeatherForecast>("WeatherData");
            var b = collection.ReplaceOne(x => x.id == ObjectId.Parse(id), fs).ModifiedCount > 0;
        }
        
        public static WeatherForecast FindById(int id)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("WeatherForecast");
            var collection = database.GetCollection<WeatherForecast>("WeatherData");
            WeatherForecast user = collection.Find(x => x.TemperatureC  == id).FirstOrDefault();
            

            if (user == null)
            {
                return null;
            }
            return user;
        }
}