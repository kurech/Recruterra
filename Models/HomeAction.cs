using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class HomeAction
    {
        private readonly ApplicationContext db;

        public HomeAction(ApplicationContext context)
        {
            db = context;
        }

        public bool AdditionalResponse(int iduser, int idvacancy)
        {
            try
            {
                Response response = new Response()
                {
                    IdUser = iduser,
                    IdVacancy = idvacancy,
                    DateAndTime = DateTime.Now,
                    IsAccepted = 0
                };
                db.Responses.Add(response);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
