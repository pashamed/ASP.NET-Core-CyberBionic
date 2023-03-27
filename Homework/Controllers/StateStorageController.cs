using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Homework.Services;
using System;


namespace Homework
{
    public class StateStorageController : Controller
    {
        private const string sessionKey = ".Session";
        private const string cookieKey = ".MyAppV.Session";
        private const string cookieKeyWrite = ".CookiesM";

        private static Dictionary<string,DateTime> cookieKeySave = new Dictionary<string, DateTime>();

        Visitors _visitors;


        public StateStorageController(Visitors visitors)
        {
            _visitors = visitors;
        }

        public IActionResult Cookies()
        {
            return View();
        }


        public IActionResult ShowCookies()
        {
            bool check = false;
            ViewBag.Cookies = Request.Cookies[cookieKeyWrite];
            foreach (var item in cookieKeySave)
            {
                if (item.Key == Request.Cookies[cookieKeyWrite])
                {
                    check = true;
                    ViewBag.CookiesTime = item.Value;
                }
            }
            if (!check)
            {
                ViewBag.Cookies = "Cookies not found";
                ViewBag.CookiesTime = "Time not found";
            }
           
            return View();
        }


        [HttpPost]
        public IActionResult AddCookies(string value, DateTime timeExpires)
        {
            CookieOptions options = new CookieOptions();
            //options.Expires = DateTime.Now.AddMinutes(1);
            options.Expires = timeExpires;

            Response.Cookies.Append(cookieKeyWrite, value, options);


            bool check = false;
            foreach (var item in cookieKeySave)
                if (item.Key == value)
                    check = true;

            if (!check)
                cookieKeySave.Add(value, timeExpires);
            else
            {
                cookieKeySave.Remove(value);
                cookieKeySave.Add(value, timeExpires);
            }
            return LocalRedirect($"~/StateStorage/ShowCookies/");
        }



        public IActionResult Visitors()
        {
            ViewBag.Cookies = Request.Cookies[cookieKey];
            ViewBag.SessionId = HttpContext.Session.Id;

            return View(_visitors.Count);
        }

        public void SetSession()
        {
            HttpContext.Session.SetString(sessionKey, HttpContext.Session.Id);
            string session = HttpContext.Session.GetString(sessionKey);
            _visitors.AddSession(session);
        }

        public void CheckSession()
        {
            string value = HttpContext.Session.GetString(sessionKey);
            if (value == null)
                SetSession();
            else
                _visitors.Check(value);
        }


        [HttpPost]
        public JsonResult CountkUser()
        {
            var value = _visitors.Count ;
            return Json(value);
        }

        [HttpPost]
        public JsonResult SessionUser()
        {
            CheckSession();
            return Json("Ok");
        }

    }
}
