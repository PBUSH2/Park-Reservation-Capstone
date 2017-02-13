using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
        private const string SQL_CreateDepartment = @"Insert into department values (@deptName);";
        private const string SQL_UpdateDepartment = @"Update department set name = @newName where department_id = @id;";
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            List<Department> deptList = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"select * from department order by department_ID;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department d = new Department();
                        d.Id = Convert.ToInt32(reader["department_id"]);
                        d.Name = Convert.ToString(reader["name"]);

                        deptList.Add(d);
                    }
                }


            }
            catch (Exception e)
            {
                throw;
            }
            return deptList;
        }

        public bool CreateDepartment(Department newDepartment)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreateDepartment, conn);

                    cmd.Parameters.AddWithValue("@deptName", newDepartment.Name);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UpdateDepartment, conn);

                    cmd.Parameters.AddWithValue("@newName", updatedDepartment.Name);
                    cmd.Parameters.AddWithValue("@id", updatedDepartment.Id);
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
