using System;
using StackExchange.Redis;
using System.IO;
using Core;

namespace Cypher;

public class Gallery : Disposer
{
    private ConnectionMultiplexer redis { set; get; }  = ConnectionMultiplexer.Connect("localhost");
    private  IDatabase db { 
        get { return redis.GetDatabase();}
    }  

    public List<Data.Gallery> RedisConn()
    {
        var database = db;
        string bucket_dir = @"/home/fuad/buckets/michaelsoft/emp-img";
    
        database.StringSet("Number-of-values", "none");
        string k1;
        string k2;
        int hash = 0;
        List<byte[]> images = new List<byte[]>();
        var Data = new List<Data.Gallery>();
        if (Directory.Exists(bucket_dir))
        {
            var files = Directory.GetFiles(bucket_dir);
            if (!db.StringGet("420").HasValue)
            {
                CacheItem(ref hash, files ,database);
            }

            hash = 0;
            Data = (from r in files
                select new Data.Gallery
                {
                    Name = (hash++).ToString(),
                    image = db.StringGet(hash.ToString()),
                }
            ).ToList();

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

    public void CacheItem(ref int hash, string[] files, IDatabase database)
    {
        foreach (var image in files)
        {
            // byte[] imageData = File.ReadAllBytes(Path.GetFileName(files[hash]));
            byte[] imageData = File.ReadAllBytes(files[hash]);

            database.StringSet(hash.ToString(), Convert.ToBase64String(imageData));
            hash++;

        }
    }
}
