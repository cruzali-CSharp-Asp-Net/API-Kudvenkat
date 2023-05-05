using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDataAccess
{
    public class PeopleController
    {
        public DataTable ReturnDataPeople(int idPeople)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["localhost"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspSelectPeopleById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdPeple", System.Data.SqlDbType.Int).Value = idPeople;

                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    DataTable dtPeople = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtPeople);
                    return dtPeople;
                }
            }
        }

    }



}
