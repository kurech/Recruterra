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

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        static MailAddress to;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PasswordRecovery()
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == 1);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
            {
                id = 1;
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Vacancy(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public IActionResult Myresume()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Account(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Resume(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Meeting(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Article(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddVacancy(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Responses(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        public EmptyResult AcceptResponse(int idresponse)
        {
            var response = db.Responses.FirstOrDefault(m => m.Id == idresponse);
            var vacancy = db.Vacancies.FirstOrDefault(m => m.Id == response.IdVacancy);
            var user = db.Users.FirstOrDefault(m => m.Id == response.IdUser);
            response.IsAccepted = 1;
            db.Update(response);
            db.SaveChanges();

            MailAddress from = new MailAddress("laba_oaip@mail.ru", "Recruterra");
            to = new MailAddress(user.Login);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Ваш отклик принят!";
            m.Body = $"Отклик на вакансию <b> {vacancy.Position} </b> принят!<br>Ожидайте назначение встречи от работодателя.<br><br> --------------------- <br> 2023 - Recruterra";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("laba_oaip@mail.ru", "6rdUR8HTnFbfbcHDLGxn");
            smtp.EnableSsl = true;
            smtp.SendMailAsync(m);

            return new EmptyResult();
        }

        public EmptyResult DismissResponse(int idresponse)
        {
            var response = db.Responses.FirstOrDefault(m => m.Id == idresponse);
            response.IsAccepted = 2;
            db.Update(response);
            db.SaveChanges();
            return new EmptyResult();
        }

        [Route("Home/AddMeeting/{id:int}/{id2:int}")]
        public async Task<IActionResult> AddMeeting(int id, int id2)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
            Resume resume = await db.Resumes.FirstOrDefaultAsync(p => p.Id == id2);

            var model = new MeetingsData { User = user, Resume = resume, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Employers = db.Employers.ToList(), Accounts = db.Accounts.ToList() };
            return View(model);
        }


        public EmptyResult AddMeet(int idemployer, int idresume, DateTime dateandtime)
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
            MailAddress from = new MailAddress("laba_oaip@mail.ru", "Recruterra");
            to = new MailAddress(someoneSendingUser.Login);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Вам назначена встреча с работодателем!";
            m.Body = $"Здравствуйте, {someoneSendingResume.FirstName}.<br>Встреча запланирована на {dateandtime.ToString("f")}<br>Адрес встречи: офис {employer.СompanyName} (Улица: {employer.Street}, дом {employer.House}, помещение {employer.Apartment})<br><i>Не забудьте взять свои документы!<i><br> --------------------- <br> 2023 - Recruterra";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("laba_oaip@mail.ru", "6rdUR8HTnFbfbcHDLGxn");
            smtp.EnableSsl = true;
            smtp.SendMailAsync(m);

            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult Signup(string email, string password, string role) // регистрация
        {
            User user = new User() { Login = email, Password = PasswordHashing.GetHashString(password), Role = role, CreateDate = DateTime.Now };
            db.Users.Add(user);
            db.SaveChanges();

            if (role == "Соискатель")
            {
                Resume resume = new Resume() { Id = user.Id };
                db.Resumes.Add(resume);
            }
            else if (role == "Работодатель")
            {
                Employer employer = new Employer() { Id = user.Id };
            }    

            db.SaveChanges();
            
            return Redirect("~/Home/Signin");
        }

        [HttpPost]
        public IActionResult Signin(string email, string password) // авторизация
        {
            var user = db.Users.FirstOrDefault(el => el.Login == email && el.Password == PasswordHashing.GetHashString(password));

            if (user != null)
            {
                Account account = new Account() { IdUser = user.Id, Auth = 1 };
                db.Accounts.Add(account);
            }

            db.SaveChanges();

            return Redirect($"~/Home/Index/{user.Id}");
        }

        public EmptyResult RecoverySendCodeToEmail(string email)
        {
            User user = db.Users.FirstOrDefault(m => m.Login == email);

            if(user != null)
            {
                MailAddress from = new MailAddress("recruterra@mail.ru", "Recruterra");
                to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Вам отправлен код для восстановления пароля!";
                m.Body = $"Здравствуйте, {email}.<br>Ваш код восстановления {GenerateCodeForRecoveryPassword.Generate()}<br><br><br> --------------------- <br> 2023 - Recruterra";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("recruterra@mail.ru", "WkEXb6t8ULG0u7rVRbwj");
                smtp.EnableSsl = true;
                smtp.SendMailAsync(m);
            }

            return new EmptyResult();
        }

        public EmptyResult UpdatePasswordInRecovery(string email, string code, string newpassword)
        {
            User user = db.Users.FirstOrDefault(m => m.Login == email);
            if (code == GenerateCodeForRecoveryPassword.CurentCode)
            {
                user.Password = PasswordHashing.GetHashString(newpassword);
                db.Update(user);
                db.SaveChanges();
            }

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

        public EmptyResult AddVac(string vacposition, string vacobligations, int vacsalary, int vacworkex, string vacdescrip, string vacedu, int vactypeofemp, int vacisactive, int idemployer)
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
                IsActive = vacisactive
            };
            db.Vacancies.Add(vacancy);
            db.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult DelVacancy(int idvacancy)
        {
            Vacancy vacancy = db.Vacancies.FirstOrDefault(vac => vac.Id == idvacancy);
            vacancy.IsActive = 0;
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

        public EmptyResult UpadateAccountAfterLogout(int idaccount)
        {
            var account = db.Accounts.FirstOrDefault(m => m.Id == idaccount);
            account.Auth = 0;

            db.Accounts.Update(account);
            db.SaveChanges();

            return new EmptyResult();
        }

        // обновление основной информации соискателя
        public EmptyResult UpadateSeekerProfileSettings(int iduser, string login, string lastname, string firstname, string middlename, string gender, DateTime dateofbirth, string phone, string city, string citizenship)
        {
            int idcity = db.Cities.FirstOrDefault(c => c.Name == city).Id;
            int idcitizenship = db.Citizenships.FirstOrDefault(cz => cz.Name == citizenship).Id;
            User user = db.Users.FirstOrDefault(user => user.Id == iduser);
            user.Login = login;
            db.Update(user);
            db.SaveChanges();

            Resume resume = db.Resumes.FirstOrDefault(user => user.Id == iduser);
            resume.LastName = lastname;
            resume.FirstName = firstname;
            resume.MiddleName = middlename;
            resume.Gender = gender;
            resume.DateOfBirth = dateofbirth;
            resume.PhoneNumber = phone;
            resume.IdCity = idcity;
            resume.IdCitizenship = idcitizenship;
            db.Update(resume);
            db.SaveChanges();
            return new EmptyResult();
        }

        // обновление основной информации работодателя
        public EmptyResult UpadateEmployerProfileSettings(int iduser, string login, string companyname, string msrn, string lastname, string firstname, string middlename, DateTime creationdate, string city, int postcode, string street, string house, string apartment)
        {
            int idcity = db.Cities.FirstOrDefault(c => c.Name == city).Id;

            User user = db.Users.FirstOrDefault(user => user.Id == iduser);
            user.Login = login;
            db.Update(user);
            db.SaveChanges();

            Employer employer = db.Employers.FirstOrDefault(user => user.Id == iduser);
            employer.СompanyName = companyname;
            employer.LastName = lastname;
            employer.FirstName = firstname;
            employer.MiddleName = middlename;
            employer.CreationDate = creationdate;
            employer.Postcode = postcode;
            employer.IdCity = idcity;
            employer.Street = street;
            employer.House = house;
            employer.Apartment = apartment;
            employer.MSRN = msrn;
            db.Update(employer);
            db.SaveChanges();
            return new EmptyResult();
        }

        public static List<Meeting> GetMeetings(int iduser) // week meetings
        {
            List<Meeting> meet = new List<Meeting>();
            //foreach(var a in db.Meetings)
            //{
            //    if(a.DateAndTime.Date == DateTime.Now.Date)
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(1))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(2))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(3))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(4))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(5))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(6))
            //    {
            //        meet.Add(a);
            //    }
            //    if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(7))
            //    {
            //        meet.Add(a);
            //    }
            //}
            return meet;
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
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
