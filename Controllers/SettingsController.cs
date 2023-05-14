using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment WebHostEnvironment;
        static MailAddress to;
        public SettingsController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;
            WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Account()
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

        // обновление основной информации соискателя
        public EmptyResult UpadateSeekerProfileSettings(int iduser, string lastname, string firstname, string middlename, string gender, DateTime dateofbirth, string phone, string city, string citizenship)
        {
            int idcity = db.Cities.FirstOrDefault(c => c.Name == city).Id;
            int idcitizenship = db.Citizenships.FirstOrDefault(cz => cz.Name == citizenship).Id;

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
        public EmptyResult UpadateEmployerProfileSettings(int iduser, string companyname, string msrn, string lastname, string firstname, string middlename, DateTime creationdate, string city, int postcode, string street, string house, string apartment)
        {
            int idcity = db.Cities.FirstOrDefault(c => c.Name == city).Id;

            Employer employer = db.Employers.FirstOrDefault(user => user.Id == iduser);
            employer.CompanyName = companyname;
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

        [HttpGet]
        public async Task<IActionResult> Responses()
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

        [Route("downloadseekerstats/{seekerId}", Name = "downloadseekerstats")]
        public ActionResult Export(int seekerId)
        {
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                int lastrow = 0;
                var firstNameWithInitials = db.Resumes.FirstOrDefault(s => s.Id == seekerId).LastName + " " + db.Resumes.FirstOrDefault(s => s.Id == seekerId).FirstName.Substring(0, 1) + ". " + db.Resumes.FirstOrDefault(s => s.Id == seekerId).MiddleName.Substring(0, 1) + ".";

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var responses = db.Responses.Where(r => r.IdUser == seekerId).ToList();
                    var countNeutralResponses = responses.Where(pos => pos.IsAccepted == 0).Count();
                    var countPosiveResponses = responses.Where(pos => pos.IsAccepted == 1).Count();
                    var countNegativeResponses = responses.Where(pos => pos.IsAccepted == 2).Count();

                    var worksheet = workbook.Worksheets.Add("Статистика откликов");

                    worksheet.Column(1).Width = 50;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 20;

                    worksheet.Cell(1, 1).Value = "Наименование вакансии";
                    worksheet.Cell(1, 2).Value = "Заработная плата";
                    worksheet.Cell(1, 3).Value = "Результат";
                    worksheet.Row(1).Style.Font.Bold = true;

                    for (int i = 0; i < responses.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = db.Vacancies.FirstOrDefault(v => v.Id == responses[i].IdVacancy).Position;
                        worksheet.Cell(i + 2, 2).Value = db.Vacancies.FirstOrDefault(v => v.Id == responses[i].IdVacancy).Salary;
                        worksheet.Cell(i + 2, 3).Value = ResponseResult(responses[i].IsAccepted);
                        lastrow = i + 1;
                    }

                    worksheet.Cell(lastrow + 2, 2).Value = "% принятых";
                    worksheet.Cell(lastrow + 2, 3).Value = (countPosiveResponses * 100 / responses.Count()).ToString() + "%";
                    worksheet.Cell(lastrow + 2, 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    worksheet.Cell(lastrow + 3, 2).Value = "% отклоненных";
                    worksheet.Cell(lastrow + 3, 3).Value = (countNegativeResponses * 100 / responses.Count()).ToString() + "%";
                    worksheet.Cell(lastrow + 3, 3).Style.Fill.BackgroundColor = XLColor.PastelRed;
                    worksheet.Cell(lastrow + 4, 2).Value = "% обрабатываемых";
                    worksheet.Cell(lastrow + 4, 3).Value = (countNeutralResponses * 100 / responses.Count()).ToString() + "%";
                    worksheet.Cell(lastrow + 4, 3).Style.Fill.BackgroundColor = XLColor.LightGray;

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Flush();

                        return new FileContentResult(stream.ToArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            FileDownloadName = $"Статистика {firstNameWithInitials} {DateTime.Now.Date.ToString("D")}.xlsx"
                        };
                    }
                }
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }

            
        }

        public string ResponseResult(int? isAccepted)
        {
            switch (isAccepted)
            {
                case 0:
                    return "Отклик в обработке";
                case 1:
                    return "Отклик принят";
                case 2:
                    return "Отклик отклонили";
                default:
                    return string.Empty;
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyResume()
        {
            string jwtToken = Request.Cookies["recruterra"];

            if (Request.Cookies["recruterra"] != null)
            {
                var jwt = Request.Cookies["recruterra"];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userId = token.Claims.First(c => c.Type == "userId").Value;

                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == int.Parse(userId));
                var model = new ResumeData { User = user, Resumes = db.Resumes.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList() };
                return View(model);
            }
            else
            {
                return RedirectToAction("Signin", "Access");
            }
        }

        [HttpPost]
        public IActionResult Upld(ResumeData rm, int ResumeId)
        {
            Resume resume = db.Resumes.First(re => re.Id == ResumeId);
            string stringFileName = UploadFile(rm, resume);

            resume.Photo = stringFileName;

            db.Resumes.Update(resume);
            db.SaveChanges();

            return RedirectToAction("MyResume", new { id = ResumeId });
        }

        private string UploadFile(ResumeData rm, Resume resume)
        {
            string fileName = null;
            if (rm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                string[] words = rm.ProfileImage.FileName.Split(new char[] { '.' });
                fileName = resume.Id + "." + words[1];

                string imagesPath = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                string checkfilePath = Path.Combine(imagesPath, fileName);
                if(System.IO.File.Exists(checkfilePath))
                {
                    System.IO.File.Delete(checkfilePath);
                }

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    rm.ProfileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // обновление резюме соискателя
        public EmptyResult UpadateSeekerResumeSettings(int iduser, string position, int salary, string edu, string university, int workex, int typeofemp, string additionalinformation, bool itspublic)
        {
            Resume resume = db.Resumes.FirstOrDefault(user => user.Id == iduser);
            resume.Position = position;
            resume.Salary = salary;
            resume.Education = edu;
            resume.University = university;
            resume.WorkExperience = workex;
            resume.IdTypeOfEmployment = typeofemp;
            resume.AdditionalInformation = additionalinformation;
            resume.ItsPublic = itspublic;
            db.Update(resume);
            db.SaveChanges();
            return new EmptyResult();
        }
    }
}
