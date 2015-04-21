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
        string connectionString = "User ID=sa;Password=cl91SqlServer;Initial Catalog=ExpenseLog;Server=CHRIS-PC";

        public int Insert(string FirstName, string LastName, string Email, string Password, string InitialBalance)
        {
            int userId;
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
                try
                {
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    userId = -1;
                }
            }

            return userId;
        }

        public User GetUser(int userId)
        {
            User user;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Users (UserID) VALUES (@UserID)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                user = new User();
            }

            return user;
        }
    }
}