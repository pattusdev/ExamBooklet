using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ExamBooklet.Pages.Booklet.IndexModel;

namespace ExamBooklet.Pages.Booklet
{
    public class CreateModel : PageModel
    {
		public BookletInfo bookletInfo = new BookletInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
        }
        public void OnPost() 
        {

			bookletInfo.BookletId = Request.Form["BookletId"];
			bookletInfo.StudentId = Request.Form["StudentId"];
			bookletInfo.StudentName = Request.Form["StudentName"];
			bookletInfo.FacultyName = Request.Form["FacultyName"];
			bookletInfo.DepartmentName = Request.Form["DepartmentName"];
			bookletInfo.CourseCode = Request.Form["CourseCode"];
			bookletInfo.CourseName = Request.Form["CourseName"];
			bookletInfo.ExamDate = Request.Form["ExamDate"];
			bookletInfo.LectureName = Request.Form["LectureName"];
			bookletInfo.SupervisorName = Request.Form["SupervisorName"];
			bookletInfo.Marks = Request.Form["Marks"];

			

			try
			{
				String connectionString = "Data Source=.;Initial Catalog=ExamBookletDB;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					String sqlQuery = "INSERT INTO ExamBooklet (BookletId,StudentId,StudentName,FacultyName,DepartmentName,CourseCode,CourseName,ExamDate,LectureName,SupervisorName,Marks) VALUES(@BookletId,@StudentId,@StudentName,@FacultyName,@DepartmentName,@CourseCode,@CourseName,@ExamDate,@LectureName,@SupervisorName,@Marks)";
					using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
					{
						cmd.Parameters.AddWithValue("@BookletId", bookletInfo.BookletId);
						cmd.Parameters.AddWithValue("@StudentId", bookletInfo.StudentId);
						cmd.Parameters.AddWithValue("@StudentName", bookletInfo.StudentName);
						cmd.Parameters.AddWithValue("@FacultyName", bookletInfo.FacultyName);
						cmd.Parameters.AddWithValue("@DepartmentName", bookletInfo.DepartmentName);
						cmd.Parameters.AddWithValue("@CourseCode", bookletInfo.CourseCode);
						cmd.Parameters.AddWithValue("@CourseName", bookletInfo.CourseName);
						cmd.Parameters.AddWithValue("@ExamDate", bookletInfo.ExamDate);
						cmd.Parameters.AddWithValue("@LectureName", bookletInfo.LectureName);
						cmd.Parameters.AddWithValue("@SupervisorName", bookletInfo.SupervisorName);
						cmd.Parameters.AddWithValue("@Marks", bookletInfo.Marks);

						cmd.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			

			successMessage = "New Booklet Recorded with Success";

			Response.Redirect("/Booklet/Index");
		}
    }
}
