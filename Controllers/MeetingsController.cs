using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MeetingsController : Controller
    {
        private ApplicationContext db;
        public MeetingsController(ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> Meeting(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }
    }
}
