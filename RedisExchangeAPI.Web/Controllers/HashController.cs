using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public string hashKey { get; set; } = "hashList";

        public HashController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            Dictionary<string,string> list = new Dictionary<string,string>();
            if (db.KeyExists(hashKey)) {
                db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    list.Add(x.Name, x.Value);
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, string value)
        {
            db.HashSet(hashKey, name, value);
            return RedirectToAction("Index");   
        }

        public IActionResult DeleteItem(string name)
        {
            db.HashDelete(hashKey, name);
            return RedirectToAction("Index");   
        }
    }
}
