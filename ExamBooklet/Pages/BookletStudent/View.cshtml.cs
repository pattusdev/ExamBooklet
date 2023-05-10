using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ExamBooklet.Pages.Booklet.IndexModel;

namespace ExamBooklet.Pages.Booklet
{
    public class ViewModel : PageModel
    {
        public List<BookletInfo> listBooklets = new List<BookletInfo>();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet(string StudentId)
        {
            if (string.IsNullOrEmpty(StudentId))
            {
                errorMessage = "Student ID not found!";
                return;
            }
            String id = Request.Query["StudentId"];
            listBooklets.Clear();
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=ExamBookletDB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM ExamBooklet WHERE StudentId=@StudentId";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
						cmd.Parameters.AddWithValue("@StudentId", id);

						using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookletInfo bookletInfo = new BookletInfo();

                                //bookletInfo.StudentId = Request.Form["StudentId"];

                                bookletInfo.BookletId = "" + reader.GetInt32(0);
                                bookletInfo.StudentId = "" + reader.GetInt32(1);
                                bookletInfo.StudentName = reader.GetString(2);
                                bookletInfo.FacultyName = reader.GetString(3);
                                bookletInfo.DepartmentName = reader.GetString(4);
                                bookletInfo.CourseCode = reader.GetString(5);
                                bookletInfo.CourseName = reader.GetString(6);
                                bookletInfo.ExamDate = reader.GetDateTime(7).ToString();
                                bookletInfo.LectureName = reader.GetString(8);
                                bookletInfo.SupervisorName = reader.GetString(9);
                                bookletInfo.Marks = reader.IsDBNull(10) ? "Marks Not Available" : reader.GetString(10);
                                bookletInfo.Claim = reader.IsDBNull(11) ? "No Claim" : reader.GetString(11);


                                listBooklets.Add(bookletInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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
