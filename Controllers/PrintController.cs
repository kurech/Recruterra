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
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList() };
                return View(model);
            }
            else
                return NotFound();
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
