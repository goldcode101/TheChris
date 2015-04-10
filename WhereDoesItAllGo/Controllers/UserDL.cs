using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WhereDoesItAllGo.Controllers
{
    public class UserDL
    {
        public bool Insert(string FirstName, string LastName, string Email, string Password, string InitialBalance)
        {
            string connectionString = "User ID=sa;Password=cl91SqlServer;Initial Catalog=ExpenseLog;Server=CHRIS-PC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Users (LastName, FirstName, Email, Password, InitialBalance) VALUES (@LastName, @FirstName, @Email, @Password, @InitialBalance)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@InitialBalance", Convert.ToDecimal(InitialBalance));

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}