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
        public ApplicationContext db;

        public static bool GetResponseUser(int idUser, int IdVacancy) // провека отклика юзера по вакансии
        {
            var response = responses().FirstOrDefault(m => m.IdUser == idUser && m.IdVacancy == IdVacancy);
            if (response != null)
                return true; // у юзера уже есть отклик на эту вакансию
            else
                return false;
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

        public static Article GetArticleById(int id) // получение статьи по айди
        {
            var article = articles().FirstOrDefault(m => m.Id == id);
            return article;
        }

        //public static List<Response> responsesss()
        //{
        //    List<Response> list1 = new List<Response>();
        //    string query = "select * from city";
        //    MySqlCommand comm = new MySqlCommand(query);
        //    comm.Connection = conn;
        //    conn.Open();
        //    MySqlDataReader dr = comm.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        list1.Add(new City
        //        {
        //            Id = Convert.ToInt32(dr["Id"]),
        //            Name = dr["Name"].ToString()
        //        });
        //    }
        //    conn.Close();
        //    return list1;
        //}

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
