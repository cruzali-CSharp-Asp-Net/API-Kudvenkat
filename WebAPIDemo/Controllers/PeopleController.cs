using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class PeopleController : ApiController
    {

        public IEnumerable<People> Get()
        {            
            return ReturnAllDataPeople().ToList();
        }

        public People Get(int id)
        {
            return ReturnDataPeople(id);
        }

        public People ReturnDataPeople(int idPeople)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["localhost"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectPeopleById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdPeople", System.Data.SqlDbType.Int).Value = idPeople;

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    People people = new People();
                    using (SqlDataReader sqlDR = cmd.ExecuteReader())
                    {
                        while (sqlDR.Read())
                        {
                            people.id = sqlDR.GetInt32(0);
                            people.FirstName = sqlDR.GetString(1);
                            people.LastName = sqlDR.GetString(2);
                            people.EmailAddress = sqlDR.GetString(3);
                            people.PhoneNumber = sqlDR.GetString(4);
                        }
                    }

                    return people;
                }
            }
        }


        public List<People> ReturnAllDataPeople()
        {
            List<People> peopleList = new List<People>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["localhost"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectAllPeople", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    
                    using (SqlDataReader sqlDR = cmd.ExecuteReader())
                    {
                        while (sqlDR.Read())
                        {
                            People peopleOne = new People();

                            peopleOne.id = (int)sqlDR["id"];
                            peopleOne.FirstName = (string)sqlDR["FirstName"];
                            peopleOne.LastName = (string)sqlDR["LastName"];
                            peopleOne.EmailAddress = (string)sqlDR["EmailAddress"];
                            peopleOne.PhoneNumber = (string)sqlDR["PhoneNumber"];

                            peopleList.Add(peopleOne);
                        }
                    }

                    return peopleList;
                }
            }
        }


    }
}
