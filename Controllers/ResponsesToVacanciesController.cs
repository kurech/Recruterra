﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;

namespace WebApplication2.Controllers
{
    public class ResponsesToVacanciesController : Controller
    {
        private ApplicationContext db;

        public ResponsesToVacanciesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Responses(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
                return NotFound();
        }

        public string ResponseResult(int? isAccepted)
        {
            switch (isAccepted)
            {
                case 0:
                    return "Отклик в обработке";
                case 1:
                    return "Отклик принят";
                case 2:
                    return "Отклик отклонили";
                default:
                    return string.Empty;
            }
        }
    }
}
