using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IConfiguration _config;

        static MailAddress to;
        public HomeController(ApplicationContext context, IConfiguration config)
        {
            db = context;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            // Получение токена из cookie
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
                var model = new IndexData { User = null, Users = db.Users.ToList(), Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Vacancy(int? salary, string? city, string? education, int? workexperience, int? employment)
        {
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userId = token.Claims.First(c => c.Type == "userId").Value;

                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == int.Parse(userId));
                List<Employer> employersByCity = new List<Employer>();
                if (city != null)
                {
                    City searchcity = db.Cities.FirstOrDefault(c => c.Name == city);
                    employersByCity = db.Employers.Where(emp => emp.IdCity == searchcity.Id).ToList();
                }

                List<Vacancy> searchvacancies = new List<Vacancy>(); // нужный город
                List<Vacancy> vacancies = new List<Vacancy>();
                if (salary != null && city != null && education != null && workexperience != null && employment != null)
                {
                    int[] ids = new int[employersByCity.Count];
                    for (int i = 0; i < employersByCity.Count; i++)
                    {
                        ids[i] = employersByCity[i].Id;
                    }

                    vacancies = await db.Vacancies.Where(v => v.Salary >= salary).Where(v => v.Education == education).Where(v => v.WorkExperience >= workexperience).Where(v => v.IdTypeOfEmployment == employment).ToListAsync();

                    for (int j = 0; j < vacancies.Count; j++)
                    {
                        for (int o = 0; o < ids.Length; o++)
                        {
                            if (vacancies[j].IdEmployer == ids[o])
                            {
                                searchvacancies.Add(vacancies[j]);
                            }
                        }
                    }

                    var model = new IndexData { User = user, Users = db.Users.ToList(), Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = searchvacancies, Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                    return View(model);
                }
                else
                {
                    vacancies = await db.Vacancies.ToListAsync();
                    var model = new IndexData { User = user, Users = db.Users.ToList(), Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = vacancies, Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }    
        }

        [HttpGet]
        public async Task<IActionResult> Resume()
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

        [HttpGet]
        public async Task<IActionResult> Meeting()
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

        [HttpGet]
        public async Task<IActionResult> Article()
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

        public EmptyResult DelVacancy(int idvacancy)
        {
            Vacancy vacancy = db.Vacancies.FirstOrDefault(vac => vac.Id == idvacancy);
            vacancy.IsActive = false;
            db.Update(vacancy);
            db.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult DelMeetingSeeker(int idseeker, int idmeet)
        {
            Meeting meeting = db.Meetings.FirstOrDefault(m => m.IdResume == idseeker && m.Id == idmeet);
            meeting.IsActive = false;

            db.Update(meeting);
            db.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult DelMeetingEmployer(int idemployer, int idmeet)
        {
            Meeting meeting = db.Meetings.FirstOrDefault(m => m.IdEmployer == idemployer && m.Id == idmeet);
            meeting.IsActive = false;

            db.Update(meeting);
            db.SaveChanges();

            return new EmptyResult();
        }

        public EmptyResult AddResponse(int iduser, int idvacancy)
        {
            Response response = new Response()
            {
                IdUser = iduser,
                IdVacancy = idvacancy,
                DateAndTime = DateTime.Now,
                IsAccepted = 0
            };
            db.Responses.Add(response);
            db.SaveChanges();
            return new EmptyResult();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
