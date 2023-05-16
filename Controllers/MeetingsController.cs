using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ApplicationContext db;
        public MeetingsController(ApplicationContext context)
        {
            db = context;
        }

        [Route("Meetings/AddMeeting/{id:int}/{id2:int}")]
        public async Task<IActionResult> AddMeeting(int id, int id2)
        {
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userId = token.Claims.First(c => c.Type == "userId").Value;

                User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                Resume resume = await db.Resumes.FirstOrDefaultAsync(p => p.Id == id2);

                var model = new MeetingsData { User = user, Resume = resume, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }
        }

        public EmptyResult AdditionalMeeting(int idemployer, int idresume, DateTime dateandtime)
        {
            Resume seeker = db.Resumes.FirstOrDefault(s => s.Id == idresume);
            Employer employer = db.Employers.FirstOrDefault(e => e.Id == idemployer);

            Meeting meeting = new Meeting()
            {
                DateAndTime = dateandtime,
                IsActive = true,
                IdEmployer = idemployer,
                IdResume = idresume
            };
            db.Meetings.Add(meeting);
            db.SaveChanges();
            
            var someoneSendingResume = seeker;
            var someoneSendingUser = db.Users.FirstOrDefault(m => m.Id == someoneSendingResume.Id);

            SendMessageToEmail.SendMessage(someoneSendingUser.Login,
                "Вам назначена встреча с работодателем!",
                $"Здравствуйте, {someoneSendingResume.FirstName}.<br>Встреча запланирована на {dateandtime.ToString("f")}<br>Адрес встречи: офис {employer.CompanyName} (Улица: {employer.Street}, дом {employer.House}, помещение {employer.Apartment})<br><i>Не забудьте взять свои документы!<i><br> --------------------- <br> 2023 - Recruterra");

            return new EmptyResult();
        }
    }
}
