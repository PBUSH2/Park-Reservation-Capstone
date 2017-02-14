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
    public class ProjectSqlDALTests
    {
        TransactionScope tran;
        private string connectionString = @"Data Source= .\SQLEXPRESS;database = projects; User ID = te_student;Password = sqlserver1";
        private int projectCount;
        private int newDeptId;
        private int newEmployeeId;
        private int newProjectId;
        private int testProjectId;
        
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

                cmd = new SqlCommand("INSERT INTO project VALUES ('Test Project', '2000-01-01', '2000-12-12'); SELECT CAST(SCOPE_IDENTITY() AS int);", conn);
                newProjectId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO project VALUES ('New Project', '2000-01-01', '2000-12-12'); SELECT CAST(SCOPE_IDENTITY() AS int);", conn);
                testProjectId = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand($"INSERT INTO project_employee VALUES ({testProjectId}, {newEmployeeId});", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT COUNT(*) FROM project; SELECT CAST(SCOPE_IDENTITY() AS int);", conn);
                projectCount = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            ProjectSqlDAL projectDAL = new ProjectSqlDAL(connectionString);

            List<Project> projectList = projectDAL.GetAllProjects();

            Assert.AreEqual(projectCount, projectList.Count);
        }

        [TestMethod]
        public void AssignEmployeeToProjectTest()
        {
            ProjectSqlDAL projectDAL = new ProjectSqlDAL(connectionString);

            bool didWork = projectDAL.AssignEmployeeToProject(newProjectId, newEmployeeId);

            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void RemoveEmployeeFromProjectTest()
        {
            ProjectSqlDAL projectDAL = new ProjectSqlDAL(connectionString);

            bool didWork = projectDAL.RemoveEmployeeFromProject(testProjectId, newEmployeeId);

            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            ProjectSqlDAL projectDAL = new ProjectSqlDAL(connectionString);

            Project newProject = new Project
            {
                Name = "Test Project 2",
                StartDate = DateTime.Parse("2000-12-12"),
                EndDate = DateTime.Parse("2000-12-31")
            };

            bool didWork = projectDAL.CreateProject(newProject);

            Assert.AreEqual(true, didWork);
        }
    }
}