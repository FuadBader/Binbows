using System;
using StackExchange.Redis;
using System.IO;
using Core;

namespace Cypher;

public class Cypher : Disposer
{
    public Data.Redis RedisConn()
    {
        
        string bucket_dir = @"/home/fuad/buckets/michaelsoft/emp-img";
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();
        db.StringSet("Number-of-values", "none");
        string k1;
        string k2;
        
        if (Directory.Exists(bucket_dir))
        {
            string[] files = Directory.GetFiles(bucket_dir);
            int numberOfFiles = files.Length;
            // Console.WriteLine($"Number of files in {bucket_dir}: {numberOfFiles}");
            db.StringSet("Number-of-values", numberOfFiles.ToString());
        }
        else
        {
            db.StringSet("Number-of-values", "sum ting go wong");
        }
        // byte[] imageData = File.ReadAllBytes("../../buckets/michaelsoft/emp-img/1(1).png");
        // db.StringSet("image_key", imageData);
        // Console.WriteLine("Image uploaded to Redis.");

        return new Data.Redis
        {
            Key = "Number Of Values",
            Value = db.StringGet("Number-of-values")
        };
    }
}