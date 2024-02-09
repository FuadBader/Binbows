using System;
using StackExchange.Redis;
using System.IO;
using Core;

namespace Cypher;

public class Gallery : Disposer
{
    public List<Data.Gallery> RedisConn()
    {
        string bucket_dir = @"/home/fuad/buckets/michaelsoft/emp-img";
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();
        db.StringSet("Number-of-values", "none");
        string k1;
        string k2;
        int hash = 0;
        List<byte[]> images = new List<byte[]>();
        var Data = new List<Data.Gallery>();
        if (Directory.Exists(bucket_dir))
        {
            string[] files = Directory.GetFiles(bucket_dir);
            foreach (var image in files)
            {
                byte[] imageData = File.ReadAllBytes(Path.GetFileName(image));
                db.StringSet(hash.ToString(), imageData.ToString());
                hash++;
            }
            
            hash = 0;

            Data = (from r in files
                select new Data.Gallery
                {
                    Name = (hash++).ToString(),
                    image = db.StringGet(hash.ToString()),
                }).ToList();
            
            // foreach (var image in files)
            // {
            //     byte[] imageData = db.StringGet(hash.ToString());
            //     images.Add(imageData);
            //     hash++;
            //     
            // }
        }
        
        return Data;
    }
}