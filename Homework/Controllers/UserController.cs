using Microsoft.AspNetCore.Mvc;
using Homework.Models;
using System;

namespace Homework.Controllers
{

    public class RegisterThirdModel
    {
        public RegistrationModelForAnAppointment GetModel { get; set; }
        public EnumLanguageType LanguageType { get; set; }
    }

    public class UserController : Controller
    {
        public IActionResult RegisterFirst()
        {
            return View();
        }

        public IActionResult RegisterSecond()
        {
            return View();
        }

        public IActionResult RegisterThird()
        {
            var model = new RegisterThirdModel()
            {
                LanguageType = EnumLanguageType.Basic
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RegisterFirst(RegistrationModelForAnAppointment model)
        {
            if (model.DateOfConsultation < DateTime.Now.AddDays(1))
            {
                ModelState.AddModelError(nameof(model.DateOfConsultation), $"Вы ввели неверную дату, вы не можете указать дату меньше {DateTime.Now.AddDays(1)} ");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Result = 1;
                return View(model);
            }
            else
                return View();


        }

        [HttpPost]
        public IActionResult RegisterSecond(RegistrationModelForAnAppointment model)
        {
            if (model.DateOfConsultation < DateTime.Now.AddDays(1))
            {
                ModelState.AddModelError(nameof(model.DateOfConsultation), $"Вы ввели неверную дату, вы не можете указать дату меньше {DateTime.Now.AddDays(1)} ");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Result = 1;
                return View(model);
            }
            else
                return View();
        }

        [HttpPost]
        public IActionResult RegisterThird(RegisterThirdModel getmodefroml)
        {
            if (getmodefroml.GetModel.DateOfConsultation < DateTime.Now.AddDays(1))
            {
                ModelState.AddModelError(nameof(getmodefroml.GetModel.DateOfConsultation), $"Вы ввели неверную дату, вы не можете указать дату меньше {DateTime.Now.AddDays(1)} ");
                ModelState.AddModelError(nameof(getmodefroml.GetModel) + "." + nameof(getmodefroml.GetModel.DateOfConsultation), $"Вы ввели неверную дату, вы не можете указать дату меньше {DateTime.Now.AddDays(1)} ");
            }

            if ((int)((DateTime)getmodefroml.GetModel.DateOfConsultation).DayOfWeek == 1 && (getmodefroml.LanguageType == EnumLanguageType.Basic) )
            {

                ModelState.AddModelError(nameof(getmodefroml.LanguageType), $"Консультация по продукту «Основы» не может проходить по понедельникам");
                ModelState.AddModelError(nameof(getmodefroml.GetModel) + "." + nameof(getmodefroml.GetModel.DateOfConsultation), $"Консультация по продукту «Основы» не может проходить по понедельникам");

            }

            if (ModelState.IsValid)
            {
                ViewBag.Result = 1;
                return View(getmodefroml);
            }
            else
                return View();
        }
    }
}