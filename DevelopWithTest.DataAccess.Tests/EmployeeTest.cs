using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess = DevelopWithTest.DataAccess;
using DataAccessContract = DevelopWithTest.DataAccess.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.DataAccess.Tests
{
    /// <summary>
    /// Test cases of Employee data access layer. This is an integration test by 
    /// connecting database. This test depends on real data.
    /// </summary>
    [TestClass]
    public class EmployeeTest
    {
        private DataAccessContract::IEmployee _dataAccessEmployee;

        /// <summary>
        /// Initilize the test case.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _dataAccessEmployee = new DataAccess::Employee();
        }

        /// <summary>
        /// Test if GetAll() return correct record from database or not.
        /// </summary>
        [TestMethod]
        public void IsGetAllReturnCorrectData()
        {
            // Act.
            IList<Models::Employee> employees = _dataAccessEmployee.GetAll();

            // Assert.
            Assert.IsNotNull(employees);
            Assert.AreEqual(4, employees.Count);

            Assert.AreEqual("emp1", employees[0].Name);
            Assert.AreEqual("Software", employees[0].Department);

            Assert.AreEqual("emp1", employees[1].Name);
            Assert.AreEqual("Software", employees[1].Department);

            Assert.AreEqual("emp2", employees[2].Name);
            Assert.AreEqual("HR", employees[2].Department);

            Assert.AreEqual("emp3", employees[3].Name);
            Assert.AreEqual("HR", employees[3].Department);
        }

        /// <summary>
        /// Test if GetById() return correct record from database or not.
        /// </summary>
        [TestMethod]
        public void IsGetByIdReturnCorrectData()
        {
            // Arrange.
            int employeeId = 1;

            // Act.
            Models::Employee employee = _dataAccessEmployee.GetById(employeeId);

            // Assert.
            Assert.IsNotNull(employee);

            Assert.AreEqual("emp1", employee.Name);
            Assert.AreEqual("Software", employee.Department);
        }

        /// <summary>
        /// Cleanup test case.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _dataAccessEmployee.Dispose();
        }
    }
}
