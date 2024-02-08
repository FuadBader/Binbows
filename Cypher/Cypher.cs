using System;
using StackExchange.Redis;
using System.IO;

namespace Cypher;

public class Cypher
{
    public void RedisConn()
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"); 
        IDatabase db = redis.GetDatabase();
        byte[] imageData = File.ReadAllBytes("../../buckets/michaelsoft/emp-img/1(1).png");
        db.StringSet("image_key", imageData);
        Console.WriteLine("Image uploaded to Redis.");
    }
}