using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ExamBooklet.Pages.Accounts
{
    public class StudentLoginModel : PageModel
    {
        public BookletInfo bookletInfo = new BookletInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String StudentId = Request.Query["StudentId"];
            String StudentPassword = Request.Query["StudentPassword"];
            if (String.IsNullOrEmpty(StudentId) || String.IsNullOrEmpty(StudentPassword))
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
                    String sqlQuery = "SELECT * FROM Student WHERE StudentId=@StudentId AND StudentPassword=@StudentPassword";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);
                        cmd.Parameters.AddWithValue("@StudentPassword", StudentPassword);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //bookletInfo.StudentId = Request.Form["StudentId"];
                                //bookletInfo.StudentId = "" + reader.GetInt32(1);
                                
                                Response.Redirect("/BookletStudent/View?StudentId=" + StudentId);
                                
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
        public class BookletInfo
        {
            public string BookletId;
            public string StudentId;
            public string StudentName;
            public string FacultyName;
            public string DepartmentName;
            public string CourseCode;
            public string CourseName;
            public string ExamDate;
            public string LectureName;
            public string SupervisorName;
            public string Marks;
            public string Claim;
        }
        
        }
    
}
