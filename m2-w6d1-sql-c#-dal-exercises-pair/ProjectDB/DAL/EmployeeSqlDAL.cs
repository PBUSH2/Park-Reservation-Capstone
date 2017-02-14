using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
        private const string SQL_SearchByName = @"Select * from employee where first_name = @firstName and last_name = @lastName;";
        private const string SQL_AllEmployees = "select * from employee;";
        private const string SQL_EmployeesWithoutProjects = "select * from employee left join project_employee on project_employee.employee_id = employee.employee_id where project_id is null";

        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AllEmployees, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee e = new Employee();
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);


                        employeeList.Add(e);
                    }
                }


            }
            catch (SqlException e)
            {
                throw;
            }
            return employeeList;
        }

        public List<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SearchByName, conn);
                    cmd.Parameters.AddWithValue("@firstName", firstname);
                    cmd.Parameters.AddWithValue("@lastName", lastname);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {


                        Employee e = new Employee();
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);


                        employeeList.Add(e);
                    }
                }


            }
            catch (SqlException e)
            {
                throw;
            }
            return employeeList;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_EmployeesWithoutProjects, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee e = new Employee();
                        e.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        e.FirstName = Convert.ToString(reader["first_name"]);
                        e.LastName = Convert.ToString(reader["last_name"]);
                        e.JobTitle = Convert.ToString(reader["job_title"]);
                        e.Gender = Convert.ToString(reader["gender"]);
                        e.BirthDate = Convert.ToDateTime(reader["birth_date"]);


                        employeeList.Add(e);
                    }
                }


            }
            catch (SqlException e)
            {
                throw;
            }
            return employeeList;
        }
    }
}
