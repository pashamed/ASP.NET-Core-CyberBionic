using Homework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Homework.Services;


namespace Homework.Controllers
{
    public class FilterController : Controller
    {
        private const string cookieKey = ".MyAppV.Session";


        public IActionResult GetUniqueUsers(CountUsersAttribute countUsers )
        {
            //ViewBag.Cookies = Request.Cookies[cookieKey];
            ViewBag.CountUsers = countUsers.CountUniqueUsers;
            return View();
        }

        public IActionResult LogUserAction(WriterAndReadService writerAndRead)
        {
            var listlog = writerAndRead.GetLog();

            return View(listlog);
        }

      
    }
}
 