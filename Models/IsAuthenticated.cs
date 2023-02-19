using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models
{
    public class IsAuthenticated
    {
        public IEnumerable<Meeting> meet { get; set; }
        public SelectList days { get; set; }
        public static User GetUser(int id)
        {
            var user = users().FirstOrDefault(m => m.Id == id);
            return user;
        }

        public static bool GetResponseUser(int idUser, int IdVacancy) // провека отклика юзера по вакансии
        {
            var response = responses().FirstOrDefault(m => m.IdUser == idUser && m.IdVacancy == IdVacancy);
            if (response != null)
                return true; // у юзера уже есть отклик на эту вакансию
            else
                return false;
        }

        //public static bool GetMeetingUser(int idUser, int IdMeeting) // провека встреч юзера
        //{
        //    var meeting = meetings().FirstOrDefault(m => m.IdUser == idUser && m.Id == IdMeeting);
        //    if (meeting != null)
        //        return true; // у юзера уже 
        //    else
        //        return false;
        //}

        public static Vacancy GetVacancyById(int id)
        {
            var vacancy = vacancies().FirstOrDefault(m => m.Id == id);
            return vacancy;
        }
        public static string GetCityById(int id) // получение города по айди
        {
            var city = cities().FirstOrDefault(m => m.Id == id);
            if (city != null)
                return city.Name;
            else
                return "null";
        }
        public static string GetTypeOfEmpById(int id) // получение типа по айди
        {
            var type = typeOfEmployments().FirstOrDefault(m => m.Id == id);
            if (type != null)
                return type.Type;
            else
                return "null";
        }
        public static string GetCitizenshipById(int id) // получение гражданства по айди
        {
            var citizenship = citizenships().FirstOrDefault(m => m.Id == id);
            if (citizenship != null)
                return citizenship.Name;
            else
                return "null";
        }

        public static Resume GetResumeById(int id) // получение резюме по айди
        {
            var resume = resumes().FirstOrDefault(m => m.Id == id);
            return resume;
        }

        public static Article GetArticleById(int id) // получение статьи по айди
        {
            var article = articles().FirstOrDefault(m => m.Id == id);
            return article;
        }

        public static Meeting GetMeetingToday(int iduser)
        {
            var meet = meetings().FirstOrDefault(m => m.DateAndTime.Date == DateTime.Now.Date && m.IdUser == iduser);
            return meet;
        }

        public static Meeting GetMeetingById(int id)
        {
            var meet = meetings().FirstOrDefault(m => m.Id == id);
            return meet;
        }

        public static List<Meeting> GetAllMeetings(int idUser)
        {
            List<Meeting> meet = new List<Meeting>();
            foreach (var a in meetings())
            {
                if (a.IdUser == idUser)
                {
                    meet.Add(a);
                }
            }
            return meet;
        }

        public static List<City> cities()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<City> list1 = new List<City>();
            string query = "select * from city";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new City
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<TypeOfEmployment> typeOfEmployments()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<TypeOfEmployment> list1 = new List<TypeOfEmployment>();
            string query = "select * from typeofemployment";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new TypeOfEmployment
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Type = dr["Type"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<User> users()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<User> list1 = new List<User>();
            string query = "select * from user";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Login = dr["Login"].ToString(),
                    Password = dr["Password"].ToString(),
                    Role = dr["Role"].ToString(),
                    Photo = dr["Photo"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Vacancy> vacancies()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Vacancy> list1 = new List<Vacancy>();
            string query = "select * from vacancy";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Vacancy
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Position = dr["Position"].ToString(),
                    Salary = Convert.ToInt32(dr["Salary"]),
                    IdCity = Convert.ToInt32(dr["IdCity"]),
                    WorkExperience = Convert.ToInt32(dr["WorkExperience"]),
                    Description = dr["Description"].ToString(),
                    Education = dr["Education"].ToString(),
                    IdTypeOfEmployment = Convert.ToInt32(dr["IdTypeOfEmployment"])
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Meeting> meetings()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Meeting> list1 = new List<Meeting>();
            string query = "select * from meeting";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Meeting
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Surname = dr["Surname"].ToString(),
                    Description = dr["Description"].ToString(),
                    DateAndTime = (DateTime)dr["DateAndTime"],
                    IdUser = Convert.ToInt32(dr["IdUser"])
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Resume> resumes()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Resume> list1 = new List<Resume>();
            string query = "select * from resume";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Resume
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Surname = dr["Surname"].ToString(),
                    Photo = dr["Photo"].ToString(),
                    DateOfBirth = (DateTime)dr["DateOfBirth"],
                    PhoneNumber = dr["PhoneNumber"].ToString(),
                    IdCity = Convert.ToInt32(dr["IdCity"]),
                    IdCitizenship = Convert.ToInt32(dr["IdCitizenship"]),
                    Position = dr["Position"].ToString(),
                    Salary = Convert.ToInt32(dr["Salary"]),
                    Education = dr["Education"].ToString(),
                    WorkExperience = Convert.ToInt32(dr["WorkExperience"]),
                    IdTypeOfEmployment = Convert.ToInt32(dr["IdTypeOfEmployment"]),
                    AdditionalInformation = dr["AdditionalInformation"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Citizenship> citizenships()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Citizenship> list1 = new List<Citizenship>();
            string query = "select * from citizenship";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Citizenship
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Article> articles()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Article> list1 = new List<Article>();
            string query = "select * from article";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Article
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Photo = dr["Photo"].ToString()
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Response> responses()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Response> list1 = new List<Response>();
            string query = "select * from response";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Response
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IdUser = Convert.ToInt32(dr["IdUser"]),
                    IdVacancy = Convert.ToInt32(dr["IdVacancy"]),
                    DateAndTime = (DateTime)dr["DateAndTime"]
                });
            }
            conn.Close();
            return list1;
        }

        public static List<Account> accounts()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString =
              "Data Source=localhost;" +
              "Initial Catalog=recruterra;" +
              "User id=recrut;" +
              "Password=ronell7815;";
            List<Account> list1 = new List<Account>();
            string query = "select * from account";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = conn;
            conn.Open();
            MySqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                list1.Add(new Account
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IdUser = Convert.ToInt32(dr["IdUser"]),
                    Auth = Convert.ToInt32(dr["Auth"])
                });
            }
            conn.Close();
            return list1;
        }

        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
