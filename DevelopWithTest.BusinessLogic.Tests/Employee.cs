using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogic = DevelopWithTest.BusinessLogic;
using BusinessLogicContract = DevelopWithTest.BusinessLogic.Contract;
using DataAccessContract = DevelopWithTest.DataAccess.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.BusinessLogic.Tests
{
    /// <summary>
    /// Test cases of Employee business logic layer. This is an unit test by 
    /// using Moq framework. This test does not depends on real data.
    /// </summary>
    [TestClass]
    public class Employee
    {
        private BusinessLogicContract::IEmployee _businessLogicEmployee;
        private Mock<DataAccessContract::IEmployee> _mockEmployeeDataAccess;

        /// <summary>
        /// Initilize the test case.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _mockEmployeeDataAccess = new Mock<DataAccessContract.IEmployee>();
            _businessLogicEmployee = new BusinessLogic::Employee(_mockEmployeeDataAccess.Object);
        }

        /// <summary>
        /// Test if GetAll() behave expectedly or not.
        /// </summary>
        [TestMethod]
        public void IsGetAllReturnCorrectData()
        {
            // Arrange.
            _mockEmployeeDataAccess
                .Setup(dataAccess => dataAccess.GetAll())
                .Returns(new List<Models::Employee>() 
                { 
                    new Models::Employee() { Id = 1, Name = "emp1", Department = "HR" }, 
                    new Models::Employee() { Id = 2, Name = "emp2", Department = "Software" } 
                });

            // Act.
            IList<Models::Employee> employees = _businessLogicEmployee.GetAll();

            // Assert.
            Assert.IsNotNull(employees);
            Assert.AreEqual(2, employees.Count);

            Assert.AreEqual("emp1", employees[0].Name);
            Assert.AreEqual("HR", employees[0].Department);

            Assert.AreEqual("emp2", employees[1].Name);
            Assert.AreEqual("Software", employees[1].Department);           
        }

        /// <summary>
        /// Test if GetById() behave expectedly or not.
        /// </summary>
        [TestMethod]
        public void IsGetByIdReturnCorrectData()
        {
            // Arrange.
            int employeeId = 1;

            _mockEmployeeDataAccess
                .Setup(dataAccess => dataAccess.GetById(employeeId))
                .Returns(new Models::Employee() { Id = 2, Name = "emp2", Department = "Software" });

            // Act.
            Models::Employee employee = _businessLogicEmployee.GetById(employeeId);

            // Assert.
            Assert.IsNotNull(employee);

            Assert.AreEqual("emp2", employee.Name);
            Assert.AreEqual("Software", employee.Department);
        }

        /// <summary>
        /// Test if Update() work properly in the case of successful database update.
        /// </summary>
        [TestMethod]
        public void IsUpdateWorkingProperlyIfRecordUpdated()
        {
            // Arrange.
            Models::Employee employee = new Models::Employee();

            _mockEmployeeDataAccess
                .Setup(dataAccess => dataAccess.Update(employee))
                .Returns(true);

            // Act.
            bool isUpdated = _businessLogicEmployee.Update(employee);

            // Assert.
            Assert.IsTrue(isUpdated);
        }

        /// <summary>
        /// Test if Update() work properly in the case of unsuccessful database update.
        /// </summary>
        [TestMethod]
        public void IsUpdateWorkingProperlyIfRecordNotUpdated()
        {
            // Arrange.
            Models::Employee employee = new Models::Employee();

            _mockEmployeeDataAccess
                .Setup(dataAccess => dataAccess.Update(employee))
                .Returns(false);

            // Act.
            bool isUpdated = _businessLogicEmployee.Update(employee);

            // Assert.
            Assert.IsFalse(isUpdated);
        }

        /// <summary>
        /// Cleanup test case.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _businessLogicEmployee.Dispose();
        }
    }
}
