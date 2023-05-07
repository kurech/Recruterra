using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        static MailAddress to;
        public AccessController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            db = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string email, string password)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Login == email && u.Password == PasswordHashing.GetHashString(password));
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }

            var jwtConfig = _configuration.GetSection("JwtConfig").Get<JwtConfig>();

            // создание утверждений (claims) для токена
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // создание JWT токена
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("username", user.Login),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtConfig.ExpirationInMinutes),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // добавление токена в cookie
            Response.Cookies.Append(jwtConfig.CookieName, tokenHandler.WriteToken(token), new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(jwtConfig.ExpirationInMinutes)
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string email, string password, string role) // регистрация
        {
            User user = new User() { Login = email, Password = PasswordHashing.GetHashString(password), Role = role, CreateDate = DateTime.Now };
            db.Users.Add(user);
            db.SaveChanges();

            if (role == "Соискатель")
            {
                Resume resume = new Resume() { Id = user.Id, ItsPublic = false };
                db.Resumes.Add(resume);
            }
            else if (role == "Работодатель")
            {
                Employer employer = new Employer() { Id = user.Id, IsApproved = false };
                db.Employers.Add(employer);
            }

            db.SaveChanges();

            return Redirect("~/Access/Signin");
        }

        [HttpGet]
        public async Task<IActionResult> PasswordRecovery()
        {
            var model = new IndexData { User = null, Users = db.Users.ToList(), Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
            return View(model);
        }

        public IActionResult RecoverySendCodeToEmail(string email)
        {
            User user = db.Users.FirstOrDefault(m => m.Login == email);

            if (user != null)
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

                return new EmptyResult();
            }
            else
            {
                return Redirect("~/Access/Signin");
            } 
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

        public EmptyResult UpadateAccountAfterLogout()
        {
            Response.Cookies.Delete("recruterra");
            return new EmptyResult();
        }
    }
}
