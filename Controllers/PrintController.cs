using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Myresume()
        {
            return View();
        }

        public IActionResult PrintPesumePDF(int iduser)
        {
            var resume = db.Resumes.FirstOrDefault(m => m.Id == iduser);
            string documentTitle = $"Резюме {resume.Surname} {resume.Name} - {resume.Position}";

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = documentTitle,
                Out = $@"C:\Users\ranel\OneDrive\{documentTitle}.pdf",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = $"<html><head></head>" +
                $"<body><img src=\"{resume.Photo}\"></img><p class=\"font36\">{resume.Surname} {resume.Name}</p><br></body>" +
                $"</html>",
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "css", "style.css") },
                HeaderSettings = { FontName = "Inter", FontSize = 12, Line = true, Center = $"Recruterra - {resume.Surname} {resume.Name} резюме"},
                FooterSettings = { FontName = "Inter", FontSize = 9, Center = "Страница [page] из [toPage]"}
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            
            var file = _converter.Convert(pdf);
            return File(file, "application/octet-stream");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
