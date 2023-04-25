using DinkToPdf;
using DinkToPdf.Contracts;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PrintController : Controller
    {
        private IConverter _converter;
        private ApplicationContext db;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public PrintController(IConverter converter, ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _converter = converter;
            db = context;
            WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Myresume(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                Resume resume = await db.Resumes.FirstOrDefaultAsync(m => m.Id == id);
                var model = new ResumeData { User = user, Resumes = db.Resumes.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList() };
                return View(model);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Upld(ResumeData rm, int ResumeId)
        {
            Resume resume = db.Resumes.First(re => re.Id == ResumeId);
            string stringFileName = UploadFile(rm, resume);

            resume.Photo = stringFileName;

            db.Resumes.Update(resume);
            db.SaveChanges();

            return RedirectToAction("Myresume", new { id = ResumeId });
        }

        private string UploadFile(ResumeData rm, Resume resume)
        {
            string fileName = null;
            if(rm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                string[] words = rm.ProfileImage.FileName.Split(new char[] { '.' });
                fileName = resume.Id + "." + words[1];
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    rm.ProfileImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // обновление резюме соискателя
        public EmptyResult UpadateSeekerResumeSettings(int iduser, int postcode, string street, string house, string apartment, string position, int salary, string edu, string university, int workex, string typeofemp, string additionalinformation, bool itspublic)
        {
            int idtypeofemp = db.TypeOfEmployments.FirstOrDefault(t => t.Type == typeofemp).Id;

            Resume resume = db.Resumes.FirstOrDefault(user => user.Id == iduser);
            resume.Postcode = postcode;
            resume.Street = street;
            resume.House = house;
            resume.Apartment = apartment;
            resume.Position = position;
            resume.Salary = salary;
            resume.Education = edu;
            resume.University = university;
            resume.WorkExperience = workex;
            resume.AdditionalInformation = additionalinformation;
            resume.ItsPublic = itspublic;
            db.Update(resume);
            db.SaveChanges();
            return new EmptyResult();
        }

        [Route("printseekerresume/{seekerId}", Name = "printseekerresume")]
        public IActionResult PrintResume(int seekerId)
        {
            var resume = db.Resumes.FirstOrDefault(m => m.Id == seekerId);
            string documentTitle = $"Резюме {resume.LastName} {resume.FirstName.Substring(0, 1)}. {resume.MiddleName.Substring(0, 1)}. {resume.Position} от {DateTime.Now.ToShortDateString()}";

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writter = PdfWriter.GetInstance(document, ms);
                document.Open();

                var image = iTextSharp.text.Image.GetInstance($"wwwroot/images/{resume.Id}.png");
                image.Alignment = Element.ALIGN_JUSTIFIED;
                document.Add(image);

                string fullName = resume.LastName != string.Empty && resume.FirstName != string.Empty && resume.MiddleName != string.Empty
                    ? resume.LastName + " " + resume.FirstName + " " + resume.MiddleName
                    : string.Empty;
                Paragraph paral = new Paragraph(resume.LastName + resume.FirstName + resume.MiddleName, new Font(Font.FontFamily.HELVETICA, 20));
                paral.Alignment = Element.ALIGN_LEFT;
                document.Add(paral);

                Paragraph login = new Paragraph($"{fullName}\n{db.Users.FirstOrDefault(u => u.Id == resume.Id).Login}", new Font(Font.FontFamily.HELVETICA, 16));
                login.Alignment = Element.ALIGN_LEFT;
                document.Add(login);

                Paragraph phone = new Paragraph($"{resume.PhoneNumber}", new Font(Font.FontFamily.HELVETICA, 16));
                phone.Alignment = Element.ALIGN_LEFT;
                document.Add(phone);

                var today = DateTime.Today;
                var age = today.Year - resume.DateOfBirth.Value.Year;
                if (resume.DateOfBirth.Value.Date > today.AddYears(-age)) 
                    age--;
                Paragraph seekerage = new Paragraph($"Возраст: {age}", new Font(Font.FontFamily.HELVETICA, 16));
                seekerage.Alignment = Element.ALIGN_LEFT;
                document.Add(seekerage);

                Paragraph city = new Paragraph($"{db.Cities.FirstOrDefault(u => u.Id == resume.IdCity).Name}", new Font(Font.FontFamily.HELVETICA, 16));
                city.Alignment = Element.ALIGN_LEFT;
                document.Add(city);

                Paragraph replacement1 = new Paragraph("Место жительства: ", new Font(Font.FontFamily.HELVETICA, Font.BOLDITALIC, 16));
                replacement1.Alignment = Element.ALIGN_LEFT;
                document.Add(replacement1);

                string replace = resume.Postcode != null && db.Cities.FirstOrDefault(u => u.Id == resume.IdCity).Name != string.Empty && resume.Street != string.Empty && resume.House != string.Empty && resume.Apartment != string.Empty
                    ? resume.Postcode + ", " + db.Cities.FirstOrDefault(u => u.Id == resume.IdCity).Name + ", " + resume.Street + ", " + resume.House + ", " + resume.Apartment
                    : string.Empty;
                Paragraph replacement = new Paragraph($"{replace}", new Font(Font.FontFamily.HELVETICA, 16));
                replacement.Alignment = Element.ALIGN_LEFT;
                document.Add(replacement);

                Paragraph citizenship = new Paragraph($"Гражданство: {db.Citizenships.FirstOrDefault(u => u.Id == resume.IdCitizenship).Name}", new Font(Font.FontFamily.HELVETICA, 16));
                citizenship.Alignment = Element.ALIGN_LEFT;
                document.Add(citizenship);

                Paragraph workex = new Paragraph($"Опыт работы: {resume.WorkExperience}", new Font(Font.FontFamily.HELVETICA, 16));
                workex.Alignment = Element.ALIGN_LEFT;
                document.Add(workex);

                Paragraph edu = new Paragraph($"Образование: {resume.Education}", new Font(Font.FontFamily.HELVETICA, 16));
                edu.Alignment = Element.ALIGN_LEFT;
                document.Add(edu);

                Paragraph univer = new Paragraph($"Учебное заведение: {resume.University}", new Font(Font.FontFamily.HELVETICA, 16));
                univer.Alignment = Element.ALIGN_LEFT;
                document.Add(univer);

                Paragraph position = new Paragraph($"Претендуемая должность: {resume.Position}", new Font(Font.FontFamily.HELVETICA, 16));
                position.Alignment = Element.ALIGN_LEFT;
                document.Add(position);

                Paragraph salary = new Paragraph($"Претендуемая з/п: {resume.Salary}", new Font(Font.FontFamily.HELVETICA, 16));
                salary.Alignment = Element.ALIGN_LEFT;
                document.Add(salary);

                Paragraph typeofemp = new Paragraph($"Тип занятости: {db.TypeOfEmployments.FirstOrDefault(u => u.Id == resume.IdTypeOfEmployment).Type}", new Font(Font.FontFamily.HELVETICA, 16));
                typeofemp.Alignment = Element.ALIGN_LEFT;
                document.Add(typeofemp);

                Paragraph additionalinfo = new Paragraph($"О себе: {resume.AdditionalInformation}", new Font(Font.FontFamily.HELVETICA, 16));
                additionalinfo.Alignment = Element.ALIGN_LEFT;
                document.Add(additionalinfo);

                document.Close();
                writter.Close();
                var constant = ms.ToArray();
                return File(constant, "application/pdf", $"{documentTitle}.pdf");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
