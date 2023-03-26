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

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        static MailAddress to;
        public Vacancy _vacancy = new Vacancy();
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vacancy()
        {
            return View();
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
        public IActionResult Account()
        {
            return View();
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

        [HttpGet]
        public IActionResult AddVacancy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Responses()
        {
            var users = db.Users.ToList();
            var resumes = db.Resumes.ToList();
            var vacancies = db.Vacancies.ToList();
            var responses = db.Responses.ToList();
            var model = new ResponsesData { Users = users, Resumes = resumes, Vacancies = vacancies, Responses = responses };
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

            _vacancy = vacancy;
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
        public IActionResult Signup(string email, string password, string role, string photo)
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
                command.CommandText = "INSERT INTO user(Id,Login,Password,Role,Photo) VALUES(NULL, @login, @password, @role, @photo)";
                command.Parameters.AddWithValue("?login", email);
                command.Parameters.AddWithValue("?password", password);
                command.Parameters.AddWithValue("?role", role);
                command.Parameters.AddWithValue("?photo", photo);
                command.ExecuteNonQuery();
                //Photo = @photoresume, Name = @nameresume, Surname = @surnameresume, DateOfBirth = @dateofresume, PhoneNumber = @phoneresume, IdCity = @cityresume, IdCitizenship = @citizenshipresume, Position = @positionresume, Salary = @salaryresume, Education = @eduresume, WorkExperience = @workresume, IdTypeOfEmployment = @employresume, AdditionalInformation = @addinforesume WHERE Id = @iduser"
                var comm = conne.CreateCommand();
                comm.CommandText = "INSERT INTO resume(Id,Name,Surname,Photo,DateOfBirth,PhoneNumber,IdCity,IdCitizenship,Position,Salary,Education,WorkExperience,IdTypeOfEmployment,AdditionalInformation) VALUES(NULL, @nameresume, @surnameresume, @photoresume, @dateofresume, @phoneresume, @cityresume, @citizenshipresume, @positionresume, @salaryresume, @eduresume, @workresume, @employresume, @addinforesume)";
                comm.Parameters.AddWithValue("?photoresume", "https://i.pinimg.com/originals/6f/f5/f2/6ff5f248a5cad4d81cb33a75f7ff8c80.jpg");
                comm.Parameters.AddWithValue("?nameresume", "null");
                comm.Parameters.AddWithValue("?surnameresume", "null");
                comm.Parameters.AddWithValue("?dateofresume", dateTime.ToString(format));
                comm.Parameters.AddWithValue("?phoneresume", "null");
                comm.Parameters.AddWithValue("?cityresume", 0);
                comm.Parameters.AddWithValue("?citizenshipresume", 0);
                comm.Parameters.AddWithValue("?positionresume", "null");
                comm.Parameters.AddWithValue("?salaryresume", 0);
                comm.Parameters.AddWithValue("?eduresume", "null");
                comm.Parameters.AddWithValue("?workresume", 0);
                comm.Parameters.AddWithValue("?employresume", 0);
                comm.Parameters.AddWithValue("?addinforesume", "null");
                comm.ExecuteNonQuery();
            }

            return Redirect("~/Home/Signin");
        }

        [HttpPost]
        public IActionResult Signin(string email, string password)
        {
            var user = users().FirstOrDefault(el => el.Login == email && el.Password == password);
            if (user != null)
            {
                string connString = "Data Source=localhost;" +
                  "Initial Catalog=recruterra;" +
                  "User id=recrut;" +
                  "Password=ronell7815;";
                using (var conne = new MySqlConnection(connString))
                {
                    conne.Open();
                    var command = conne.CreateCommand();
                    command.CommandText = "INSERT INTO account(Id, IdUser,Auth) VALUES(NULL, @userid, @auth)";
                    command.Parameters.AddWithValue("?userid", user.Id);
                    command.Parameters.AddWithValue("?auth", 1);
                    command.ExecuteNonQuery();
                }
                return Redirect("~/Home/Index");
            }
            else
            {
                string connString = "Data Source=localhost;" +
                  "Initial Catalog=recruterra;" +
                  "User id=recrut;" +
                  "Password=ronell7815;";
                using (var conne = new MySqlConnection(connString))
                {
                    conne.Open();
                    var command = conne.CreateCommand();
                    command.CommandText = "INSERT INTO account(Id, IdUser,Auth) VALUES(NULL, @userid, @auth)";
                    command.Parameters.AddWithValue("?userid", user.Id);
                    command.Parameters.AddWithValue("?auth", 0);
                    command.ExecuteNonQuery();
                }
                return Redirect("~/Home/Index");
            }
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

        public EmptyResult AddVac(string vacposition, int vacsalary, string vaccity, int vacworkex, string vacdescrip, string vacedu, string vactypeofemp)
        {
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "INSERT INTO vacancy(Id,Position,Salary,IdCity,WorkExperience,Description,Education,IdTypeOfEmployment) VALUES(NULL, @vacposition, @vacsalary, @vaccity, @vacworkex, @vacdescrip, @vacedu, @vactypeofemp)";
                command.Parameters.AddWithValue("?vacposition", vacposition);
                command.Parameters.AddWithValue("?vacsalary", vacsalary);
                command.Parameters.AddWithValue("?vaccity", GetCityByIdCity(vaccity));
                command.Parameters.AddWithValue("?vacworkex", vacworkex);
                command.Parameters.AddWithValue("?vacdescrip", vacdescrip);
                command.Parameters.AddWithValue("?vacedu", vacedu);
                command.Parameters.AddWithValue("?vactypeofemp", GetTypeEmploymentByIdCity(vactypeofemp));
                command.ExecuteNonQuery();
            }
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
            string connString = "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            using (var conne = new MySqlConnection(connString))
            {
                conne.Open();
                var command = conne.CreateCommand();
                command.CommandText = "UPDATE account SET Auth = 0 WHERE Id = @idaccount";
                command.Parameters.AddWithValue("?idaccount", idaccount);
                command.ExecuteNonQuery();
            }
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
