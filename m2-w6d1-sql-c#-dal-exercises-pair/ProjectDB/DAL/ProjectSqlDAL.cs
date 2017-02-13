using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class ProjectSqlDAL
    {
        private const string SQL_CreateProject = @"Insert into project values (@projectName, @startDate, @endDate);";
        private const string SQL_AssignEmployeeToProject = @"Insert into project_employee values (@project_id, @employee_id);";
        private const string SQL_DeleteEmployeeFromProject = @"Delete from project_employee where project_id = @project_id and employee_id = @employee_id;";
        private const string SQL_AllProjects = "Select * from project;";
        private string connectionString;

        // Single Parameter Constructor
        public ProjectSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> projectList = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AllProjects, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Project p = new Project();
                        p.ProjectId = Convert.ToInt32(reader["project_id"]);
                        p.Name = Convert.ToString(reader["name"]);                        
                        p.StartDate = Convert.ToDateTime(reader["from_date"]);
                        p.EndDate = Convert.ToDateTime(reader["to_date"]);
              


                        projectList.Add(p);
                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }
            return projectList;
        }

        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AssignEmployeeToProject, conn);

                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DeleteEmployeeFromProject, conn);

                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool CreateProject(Project newProject)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateProject, conn);

                    cmd.Parameters.AddWithValue("@projectName", newProject.Name);
                    cmd.Parameters.AddWithValue("@startDate", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", newProject.EndDate);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
