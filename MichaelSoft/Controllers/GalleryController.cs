using Microsoft.AspNetCore.Mvc;

namespace MichaelSoft.Controllers;

public class GalleryController : Controller
{
    // GET
    public IActionResult Index()
    {
        using var obj = new Cypher.Gallery();
        var gallery  = obj.RedisConn();
        return View(gallery);
    }
}