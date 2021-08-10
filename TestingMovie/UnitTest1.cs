using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieStore.DBUtility;
using System;
using System.Data;

namespace TestingMovie
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FirstTest()
        {
            DBOperation operation = new DBOperation();
            Assert.AreEqual(operation.GetConnectionState(), ConnectionState.Open);
        }

        [TestMethod]
        public void SecondTest()
        {
            DBOperation operation = new DBOperation();
            operation.CloseConnection();
            Assert.AreEqual(operation.GetConnectionState(), ConnectionState.Closed);
        }
    }
}
