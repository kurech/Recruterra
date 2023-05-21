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
        private readonly IConfiguration _configuration;
        public AccessController(ApplicationContext context, IConfiguration configuration)
        {
            db = context;
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
            var isHaveAccount = db.Users.FirstOrDefault(q => q.Login == email);

            if (isHaveAccount != null)
            {
                return Redirect("~/Access/Signup");
            }

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
            var model = new IndexData { User = null, Users = await db.Users.ToListAsync(), Resumes = await db.Resumes.ToListAsync(), Meetings = await db.Meetings.ToListAsync(), Vacancies = await db.Vacancies.ToListAsync(), Articles = await db.Articles.ToListAsync(), Responses = await db.Responses.ToListAsync(), TypeOfEmployments = await db.TypeOfEmployments.ToListAsync(), Cities = await db.Cities.ToListAsync(), Citizenships = await db.Citizenships.ToListAsync(), Employers = await db.Employers.ToListAsync() };
            return View(model);
        }

        public IActionResult RecoverySendCodeToEmail(string email)
        {
            User user = db.Users.FirstOrDefault(m => m.Login == email);

            if (user != null)
            {
                SendMessageToEmail.SendMessage(
                    email, 
                    "Вам отправлен код для восстановления пароля!", 
                    $"Здравствуйте, {email}.<br>Ваш код восстановления {GenerateCodeForRecoveryPassword.Generate()}<br><br><br> --------------------- <br> 2023 - Recruterra");
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
