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

        

        public IActionResult Index()
        {
            return View();
        }
    }
}
