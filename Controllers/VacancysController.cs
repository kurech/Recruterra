using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userId = token.Claims.First(c => c.Type == "userId").Value;

                Vacancy vacancy = await db.Vacancies.FirstOrDefaultAsync(p => p.Id == id);
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == int.Parse(userId));
                var model = new OneOfTheVacancyData { Vacancy = vacancy, User = user, Users = db.Users.ToList(), Responses = db.Responses.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddVacancy()
        {
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userId = token.Claims.First(c => c.Type == "userId").Value;

                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == int.Parse(userId));
                var model = new IndexData { User = user, Users = db.Users.ToList(), Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }
        }

        public EmptyResult AdditionalVacancy(string vacposition, string vacobligations, int vacsalary, int vacworkex, string vacdescrip, string vacedu, int vactypeofemp, int idemployer)
        {
            Vacancy vacancy = new Vacancy()
            {
                Position = vacposition,
                Obligations = vacobligations,
                Salary = vacsalary,
                WorkExperience = vacworkex,
                Description = vacdescrip,
                Education = vacedu,
                IdTypeOfEmployment = vactypeofemp,
                IdEmployer = idemployer,
                IsActive = true,
                IsConfirmed = false,
                OptimalSalary = 0
            };
            db.Vacancies.Add(vacancy);
            db.SaveChanges();
            return new EmptyResult();
        }
    }
}
