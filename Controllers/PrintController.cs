using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PrintController : Controller
    {
        private IConverter _converter;
        private ApplicationContext db;
        public PrintController(IConverter converter, ApplicationContext context)
        {
            _converter = converter;
            db = context;
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

        // обновление резюме соискателя
        public EmptyResult UpadateSeekerResumeSettings(int iduser, byte[] photo, int postcode, string street, string house, string apartment, string position, int salary, string edu, string university, int workex, string typeofemp, string additionalinformation, bool itspublic)
        {
            int idtypeofemp = db.TypeOfEmployments.FirstOrDefault(t => t.Type == typeofemp).Id;

            Resume resume = db.Resumes.FirstOrDefault(user => user.Id == iduser);
            //resume.Photo = photo;
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

        public FileContentResult PrintPesumePDF(int iduser)
        {
            var resume = db.Resumes.FirstOrDefault(m => m.Id == iduser);
            string documentTitle = $"Резюме {resume.LastName} {resume.FirstName.Substring(0, 1)} {resume.MiddleName.Substring(0, 1)} {resume.Position} от {DateTime.Now.Date}";

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = documentTitle,
                Out = $@"C:\Users\ranel\Downloads\{documentTitle}.pdf",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = PDFTemplateGenerator.GetHTMLString(resume),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetDirectoryRoot("C:\\Users\\ranel\\source\\repos\\WebApplication2\\wwwroot\\"), "css", "style.css") },
                HeaderSettings = { FontName = "Inter", FontSize = 12, Line = true, Center = $"Recruterra - {resume.LastName} {resume.FirstName.Substring(0, 1)} {resume.MiddleName.Substring(0, 1)} резюме"},
                FooterSettings = { FontName = "Inter", FontSize = 9, Center = "Страница [page] из [toPage]"}
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            
            _converter.Convert(pdf);

            return File(_converter.Convert(pdf), "application/octet-stream", $"{documentTitle}.pdf");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
