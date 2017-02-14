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
    public class DepartmentSqlDALTests
    {
        TransactionScope tran;
        private int departmentId;
        private string connectionString = @"Data Source= .\SQLEXPRESS;database = projects; User ID = te_student;Password = sqlserver1";
        private int departmentCount = 0;

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("INSERT INTO department VALUES ('Test Department'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                departmentId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM department;", conn);
                departmentCount = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreateDepartmentTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);
            Department dept = new Department
            {
                Name = "Test Department 2"
            };

            bool didWork = departmentDAL.CreateDepartment(dept);

            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void UpdateDepartmentTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);
            Department dept = new Department
            {
                Name = "Test Department New Name",
                Id = departmentId
            };

            bool didWork = departmentDAL.UpdateDepartment(dept);

            Assert.AreEqual(true, didWork);
            Assert.AreEqual(departmentId, dept.Id);
        }

        [TestMethod]
        public void GetDepartmentsTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);

            List<Department> deptList = departmentDAL.GetDepartments();

            Assert.IsNotNull(deptList);
            Assert.AreEqual(departmentCount, deptList.Count);
        }
    }
}