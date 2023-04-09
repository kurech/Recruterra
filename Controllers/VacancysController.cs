using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class VacancysController : Controller
    {
        private ApplicationContext db;
        public VacancysController(ApplicationContext context)
        {
            db = context;
        }

        [Route("Vacancys/OneOfTheVacancy/{id:int}/{id2:int}")]
        public async Task<IActionResult> OneOfTheVacancy(int id, int id2)
        {
            Vacancy vacancy = await db.Vacancies.FirstOrDefaultAsync(p => p.Id == id);
            User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id2);
            var model = new OneOfTheVacancyData { Vacancy = vacancy, User = user, Users = db.Users.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }
    }
}
