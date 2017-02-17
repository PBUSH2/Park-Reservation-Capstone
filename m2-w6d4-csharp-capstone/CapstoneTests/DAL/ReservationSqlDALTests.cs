using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Exceptions;

namespace Capstone.DAL.Tests
{
    [TestClass()]
    public class ReservationSqlDALTests
    {
        TransactionScope tran;
        string connectionString = @"Data Source= .\SQLEXPRESS;Initial Catalog = campground; User ID = te_student;Password = sqlserver1";

        [TestInitialize]
        public void Setup()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();             
                //SqlCommand cmd = new SqlCommand();
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }
        [TestMethod()]
        [ExpectedException(typeof(BookingConflictException))]
        public void BookReservationTest()
        {
            Reservation r = new Reservation()
            {
                Name = "Mitchel Mayle",
                CampgroundName = "Blackwoods",
                CreateDate = Convert.ToDateTime("2017-01-01"),
                NumberCampers = 1,
                FromDate = Convert.ToDateTime("2017-01-01"),
                ToDate = Convert.ToDateTime("2017-01-02"),
                SiteId = 1,
                SiteNumber = 1    
                           
            };
            ReservationSqlDAL dal = new ReservationSqlDAL();
            int resNum = dal.BookReservation(r);


        }
    }
}