using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "theBatman");
            db.StringSet("ziyaretci", 100);


            return View();
        }

        public IActionResult Show()
        {
            //var value = db.StringGet("name");
            //var value = db.StringLength("name");
            var value = db.StringGetRange("name", 0, 3);  //belirtilen aralıktaki karakterleri getirir.
            
            if (value.HasValue)
                ViewBag.Value = value.ToString();
            //db.StringIncrement("ziyaretci", 30); arttırma işlemi
            var count = db.StringDecrementAsync("ziyaretci", 5).Result; //asenkron olarak eksiltme işlemi
            var number = db.StringGet("ziyaretci");
            if (number.HasValue)
                ViewBag.Number = number;
            return View();
        }
    }
}
