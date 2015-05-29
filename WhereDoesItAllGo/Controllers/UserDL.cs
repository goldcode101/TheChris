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
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT UserID, Email, FirstName, LastName, InitialBalance, Password FROM Users WHERE UserID = @UserID");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@UserID", userId);

                connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        user.UserID = Convert.ToInt32(dr["UserID"]);
                        user.Email = dr["Email"].ToString();
                        user.FirstName = dr["FirstName"].ToString();
                        user.LastName = dr["LastName"].ToString();
                        user.InitialBalance = Convert.ToDecimal(dr["InitialBalance"]);
                        user.Password = dr["Password"].ToString();
                    }
                }
            }

            return user;
        }

        public int ValidateUser(string Email, string Password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT UserID FROM Users WHERE (Email = @Email AND Password = @Password)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //if (dr != null)
                //    return Convert.ToInt32(dr[0].ToString());
                //else
                //    return -1;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return Convert.ToInt32(dr["UserID"].ToString());
                    }
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}