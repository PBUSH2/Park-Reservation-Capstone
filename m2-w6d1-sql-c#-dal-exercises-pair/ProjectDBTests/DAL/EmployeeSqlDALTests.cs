using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using ProjectDB.Models;

namespace ProjectDB.DAL.Tests
{
    [TestClass]
    public class EmployeeSqlDALTests
    {
        TransactionScope tran;
        private string connectionString = @"Data Source= .\SQLEXPRESS;database = projects; User ID = te_student;Password = sqlserver1";
        private int employeeCount;
        private int newEmployeeId;
        private int newDeptId;
        private int newProjectId;
        private int employeeNoProject;

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("INSERT INTO department VALUES ('Test Department'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                newDeptId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand($"INSERT INTO employee VALUES ({newDeptId},'Mitchel', 'Mayle', 'CEO', '1988-07-22', 'M', '2012-12-12'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                newEmployeeId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) from employee;", conn);
                employeeCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("select COUNT(*) from employee left join project_employee on project_employee.employee_id = employee.employee_id where project_id is null; SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                employeeNoProject = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            EmployeeSqlDAL employeeDAL = new EmployeeSqlDAL(connectionString);

            List<Employee> employeeList = employeeDAL.GetAllEmployees();

            Assert.AreEqual(employeeCount, employeeList.Count);
        }

        [TestMethod]
        public void SearchTest()
        {
            EmployeeSqlDAL employeeDAL = new EmployeeSqlDAL(connectionString);

            List<Employee> employeeList = employeeDAL.Search("Mitchel", "Mayle");

            Assert.AreEqual("Mitchel", employeeList[0].FirstName);
            Assert.AreEqual("Mayle", employeeList[0].LastName);
            Assert.AreEqual(1, employeeList.Count);
        }

        [TestMethod]
        public void GetEmployeesWithoutProjectsTest()
        {
            EmployeeSqlDAL employeeDAL = new EmployeeSqlDAL(connectionString);

            List<Employee> employeeList = employeeDAL.GetEmployeesWithoutProjects();

            Assert.AreEqual(employeeNoProject, employeeList.Count);
        }
    }
}