using Microsoft.AspNetCore.Mvc;

namespace MichaelSoft.Controllers;

public class RedisController : Controller
{
    // GET
    public IActionResult Index()
    {
        using var obj = new Cypher.Cypher();
        var Redis =obj.RedisConn();
        
        return View(Redis);
    }
}