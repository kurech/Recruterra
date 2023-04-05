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
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList() };
                return View(model);
            }
            else
            {
                id = 1;
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList() };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Vacancy()
        {
            var model = new VacanciesData { Vacancies = db.Vacancies.ToList(), Users = db.Users.ToList(), Responses = db.Responses.ToList() };
            return View(model);
        }

        [HttpGet]
        public IActionResult Myresume()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Meeting()
        {
            var users = db.Users.ToList();
            var resumes = db.Resumes.ToList();
            var meetings = db.Meetings.ToList();
            var model = new MeetingsData { Users = users, Resumes = resumes, Meetings = meetings };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Account(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList() };
            return View(model);
        }

        [HttpGet]
        public IActionResult Resume()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Article()
        {
            return View();
        }

        public async Task<IActionResult> OneOfTheVacancy(int id)
        {
            Vacancy vacancy = await db.Vacancies.FirstOrDefaultAsync(p => p.Id == id);
            var model = new OneOfTheVacancyData { Vacancy = vacancy, Users = db.Users.ToList(), Responses = db.Responses.ToList() };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddVacancy()
        {
            var model = new VacanciesData { Vacancies = db.Vacancies.ToList(), Users = db.Users.ToList(), Responses = db.Responses.ToList() };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Responses(int? id)
        {
            User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
            var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList() };
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

        [HttpGet]
        public IActionResult AddMeeting(int id)
        {
            List<User> users = db.Users.Where(m => m.Id == id).ToList();
            List<Resume> resumes = db.Resumes.Where(m => m.Id == id).ToList();
            var model = new ResponsesData { Users = users, Resumes = resumes };
            return View(model);
        }

        public EmptyResult AddMeet(int iduser, string name, string surname, string descrip, DateTime dateandtime)
        {
            Meeting meeting = new Meeting()
            {
                Name = name, Surname = surname, Description = descrip, DateAndTime = dateandtime, IdUser = iduser
            };
            db.Meetings.Add(meeting);
            db.SaveChanges();

            var someoneSendingResume = db.Resumes.FirstOrDefault(m => m.Name == name && m.Surname == surname);
            var someoneSendingUser = db.Users.FirstOrDefault(m => m.Id == someoneSendingResume.Id);
            MailAddress from = new MailAddress("laba_oaip@mail.ru", "Recruterra");
            to = new MailAddress(someoneSendingUser.Login);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Вам назначена встреча с работодателем!";
            m.Body = $"Здравствуйте, {name}.<br>Встреча запланирована на {dateandtime.ToString("f")}<br><br> --------------------- <br> 2023 - Recruterra";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("laba_oaip@mail.ru", "6rdUR8HTnFbfbcHDLGxn");
            smtp.EnableSsl = true;
            smtp.SendMailAsync(m);

            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult Signup(string email, string password, string role, string photo) // регистрация
        {
            DateTime dateTime = DateTime.Now;

            User user = new User() { Login = email, Password = PasswordHashing.GetHashString(password), Role = role, Photo = photo };
            db.Users.Add(user);
            db.SaveChanges();

            if (role == "Соискатель")
            {
                Resume resume = new Resume() { Id = user.Id, DateOfBirth = dateTime, IdCity = 0, IdCitizenship = 0, Salary = 0, WorkExperience = 0 };
                db.Resumes.Add(resume);
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

        public EmptyResult AddResponse(int iduser, int idvacancy)
        {
            string format = "yyyy-MM-dd HH:mm:ss.FFFFFFF";
            DateTime dateTime = DateTime.Now;
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "INSERT INTO response(Id,IdUser,IdVacancy,DateAndTime) VALUES(NULL, @iduser, @idvacancy, @dateandtime)";
                command.Parameters.AddWithValue("?iduser", iduser);
                command.Parameters.AddWithValue("?idvacancy", idvacancy);
                command.Parameters.AddWithValue("?dateandtime", dateTime.ToString(format));
                command.ExecuteNonQuery();
            }
            return new EmptyResult();
        }

        public EmptyResult AddVac(string vacposition, int vacsalary, string vaccity, int vacworkex, string vacdescrip, string vacedu, string vactypeofemp, int idemployer)
        {
            Vacancy vacancy = new Vacancy() { Position = vacposition, Salary = vacsalary, IdCity = db.Cities.FirstOrDefault(m => m.Name == vaccity).Id, WorkExperience = vacworkex, Description = vacdescrip, Education = vacedu, IdTypeOfEmployment = db.TypeOfEmployments.FirstOrDefault(m => m.Type == vactypeofemp).Id, IdUser = idemployer};
            db.Vacancies.Add(vacancy);
            db.SaveChanges();
            return new EmptyResult();
        }

        public EmptyResult DelMeeting(int iduser, int idmeet)
        {
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "DELETE FROM meeting WHERE IdUser = @iduser AND Id = @idmeet";
                command.Parameters.AddWithValue("?iduser", iduser);
                command.Parameters.AddWithValue("?idmeet", idmeet);
                command.ExecuteNonQuery();
            }
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

        public ViewResult UpadateProfileSettings(int iduser, string photo, string password)
        {
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "UPDATE user SET Photo = @photo, Password = @password WHERE Id = @iduser";
                command.Parameters.AddWithValue("?iduser", iduser);
                command.Parameters.AddWithValue("?photo", photo);
                command.Parameters.AddWithValue("?password", password);
                command.ExecuteNonQuery();
            }
            return View("Account");
        }

        public EmptyResult UpdateResumeSettings(int iduser, string photoresume, string nameresume, string surnameresume, string positionresume, int salaryresume, DateTime dateofresume, string phoneresume, string cityresume, string citizenshipresume, string eduresume, int workresume, string employresume, string addinforesume)
        {
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "UPDATE resume SET Photo = @photoresume, Name = @nameresume, Surname = @surnameresume, DateOfBirth = @dateofresume, PhoneNumber = @phoneresume, IdCity = @cityresume, IdCitizenship = @citizenshipresume, Position = @positionresume, Salary = @salaryresume, Education = @eduresume, WorkExperience = @workresume, IdTypeOfEmployment = @employresume, AdditionalInformation = @addinforesume WHERE Id = @iduser";
                command.Parameters.AddWithValue("?iduser", iduser);
                command.Parameters.AddWithValue("?photoresume", photoresume);
                command.Parameters.AddWithValue("?nameresume", nameresume);
                command.Parameters.AddWithValue("?surnameresume", surnameresume);
                command.Parameters.AddWithValue("?dateofresume", (DateTime)dateofresume);
                command.Parameters.AddWithValue("?phoneresume", phoneresume);
                command.Parameters.AddWithValue("?cityresume", GetCityByIdCity(cityresume));
                command.Parameters.AddWithValue("?citizenshipresume", GetCitizenshipByIdCity(citizenshipresume));
                command.Parameters.AddWithValue("?positionresume", positionresume);
                command.Parameters.AddWithValue("?salaryresume", salaryresume);
                command.Parameters.AddWithValue("?eduresume", eduresume);
                command.Parameters.AddWithValue("?workresume", workresume);
                command.Parameters.AddWithValue("?employresume", GetTypeEmploymentByIdCity(employresume));
                command.Parameters.AddWithValue("?addinforesume", addinforesume);
                command.ExecuteNonQuery();
            }
            return new EmptyResult();
        }

        public static List<Meeting> GetMeetings(int iduser) // week meetings
        {
            List<Meeting> meet = new List<Meeting>();
            foreach(var a in IsAuthenticated.meetings())
            {
                if(a.DateAndTime.Date == DateTime.Now.Date)
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(1))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(2))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(3))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(4))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(5))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(6))
                {
                    meet.Add(a);
                }
                if (a.DateAndTime.Date == DateTime.Now.Date.AddDays(7))
                {
                    meet.Add(a);
                }
            }
            return meet;
        }

        public static int GetCityByIdCity(string cityName)
        {
            var city = IsAuthenticated.cities().FirstOrDefault(m => m.Name == cityName);
            return city.Id;
        }
        public static int GetCitizenshipByIdCity(string citizenshipName)
        {
            var citizenship = IsAuthenticated.citizenships().FirstOrDefault(m => m.Name == citizenshipName);
            return citizenship.Id;
        }
        public static int GetTypeEmploymentByIdCity(string typeName)
        {
            var type = IsAuthenticated.typeOfEmployments().FirstOrDefault(m => m.Type == typeName);
            return type.Id;
        }
        public List<User> users ()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<User> list1 = new List<User>();
            string query = "select * from user";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Login = dr["Login"].ToString(),
                    Password = dr["Password"].ToString(),
                    Role = dr["Role"].ToString(),
                    Photo = dr["Photo"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Meeting> meetings()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Meeting> list1 = new List<Meeting>();
            string query = "select * from meeting";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Meeting
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Surname = dr["Surname"].ToString(),
                    Description = dr["Description"].ToString(),
                    DateAndTime = (DateTime)dr["DateAndTime"],
                    IdUser = Convert.ToInt32(dr["IdUser"])
                });
            }
            conn.Close();
            return list1;
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
