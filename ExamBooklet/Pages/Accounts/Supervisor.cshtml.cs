using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ExamBooklet.Pages.Accounts
{
    public class SupervisorModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String LecturerName = Request.Query["LecturerName"];
            String LecturerPassword = Request.Query["LecturerPassword"];
            if (String.IsNullOrEmpty(LecturerName) || String.IsNullOrEmpty(LecturerPassword))
            {
                errorMessage = "Please enter both name and password!";
                return;
            }
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=ExamBookletDB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Lecturer WHERE LecturerName=@LecturerName AND LecturerPassword=@LecturerPassword";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@LecturerName", LecturerName);
                        cmd.Parameters.AddWithValue("@LecturerPassword", LecturerPassword);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Response.Redirect("/Booklet/IndexSupervisor");
                            }
                            else
                            {
                                errorMessage = "Name or Password not Correct!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }
}
