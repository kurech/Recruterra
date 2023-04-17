using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;

namespace WebApplication2.Controllers
{
    public class ResponsesToVacanciesController : Controller
    {
        private ApplicationContext db;

        public ResponsesToVacanciesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Responses(int? id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(m => m.Id == id);
                var model = new IndexData { User = user, Resumes = db.Resumes.ToList(), Meetings = db.Meetings.ToList(), Vacancies = db.Vacancies.ToList(), Articles = db.Articles.ToList(), Responses = db.Responses.ToList(), Accounts = db.Accounts.ToList(), TypeOfEmployments = db.TypeOfEmployments.ToList(), Cities = db.Cities.ToList(), Citizenships = db.Citizenships.ToList(), Employers = db.Employers.ToList() };
                return View(model);
            }
            else
                return NotFound();
        }

        [Route("downloadseekerstats/{seekerId}", Name = "downloadseekerstats")]
        public ActionResult Export(int seekerId)
        {
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
    }
}
