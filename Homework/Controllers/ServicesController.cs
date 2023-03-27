﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Homework.Models;
using Homework.Services;

namespace Homework
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Внедрение зависимости непосредственно методу действия, который будет ее использовать
        public IActionResult Day([FromServices] DayService dayService)
        {
            List<Day> day = dayService.DayServiceReady();
            ViewBag.Day = day;
            return View();
        }

        public IActionResult Months([FromServices] MonthsService monthsService)
        {
            List<Months> months = monthsService.MonthsServiceRead();
            ViewBag.Months = months;
            return View();
        }

    }
}
