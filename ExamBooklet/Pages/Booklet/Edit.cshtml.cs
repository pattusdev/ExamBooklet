using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Claims;
using static ExamBooklet.Pages.Booklet.IndexModel;

namespace ExamBooklet.Pages.Booklet
{
    public class EditModel : PageModel
    {
		public BookletInfo bookletInfo = new BookletInfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
        {
			String id = Request.Query["BookletId"];
			try
			{
				String connectionString = "Data Source=.;Initial Catalog=ExamBookletDB;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					String sqlQuery = "SELECT * FROM ExamBooklet WHERE BookletId=@BookletId";
					using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
					{
						cmd.Parameters.AddWithValue("@BookletId", id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
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
								if (!reader.IsDBNull(10))
								{
									bookletInfo.Marks = reader.GetString(10);
								}
								else
								{
									bookletInfo.Marks = "";
								}
								if (!reader.IsDBNull(11))
								{
									bookletInfo.Claim = reader.GetString(11);
								}
								else
								{
									bookletInfo.Claim = "";
								}

								
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
			bookletInfo.Claim = Request.Form["Claim"];

			//if (bookletInfo.name.Length == 0 || patientInfo.email.Length == 0 || patientInfo.phone.Length == 0 || patientInfo.address.Length == 0)
			//{
			//errorMessage = "All fields are required";
			//return;
			//}
			
			try
			{
				String connectionString = "Data Source=.;Initial Catalog=ExamBookletDB;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					String sqlQuery = "UPDATE ExamBooklet SET Marks=@Marks,Claim=@Claim WHERE BookletId=@BookletId";
					using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
					{
						cmd.Parameters.AddWithValue("@BookletId", bookletInfo.BookletId);

						if (string.IsNullOrEmpty(bookletInfo.Marks))
						{
							cmd.Parameters.AddWithValue("@Marks", DBNull.Value);
						}
						else
						{
							cmd.Parameters.AddWithValue("@Marks", bookletInfo.Marks);
						}
						
						if (string.IsNullOrEmpty(bookletInfo.Claim))
						{
							cmd.Parameters.AddWithValue("@Claim", DBNull.Value);
						}
						else
						{
							cmd.Parameters.AddWithValue("@Claim", bookletInfo.Claim);
						}


						cmd.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			bookletInfo.BookletId = "";
			bookletInfo.StudentId = "";
			bookletInfo.StudentName = "";
			bookletInfo.FacultyName = "";
			bookletInfo.DepartmentName = "";
			bookletInfo.CourseCode = "";
			bookletInfo.CourseName = "";
			bookletInfo.ExamDate = "";
			bookletInfo.LectureName = "";
			bookletInfo.SupervisorName = "";
			bookletInfo.Marks = "";
			bookletInfo.Claim = "";

			successMessage = "Booklet Updated with Success";

			Response.Redirect("/Booklet/Index");
		}
	}
}
