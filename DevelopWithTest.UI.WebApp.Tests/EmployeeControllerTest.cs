﻿using System.Collections.Generic;
using System.Web.Mvc;
using DevelopWithTest.UI.WebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicContract = DevelopWithTest.BusinessLogic.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.UI.WebApp.Tests
{
    /// <summary>
    /// Test cases of Employee controller.
    /// </summary>
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<BusinessLogicContract::IEmployee> _mockBllEmployee;
        private EmployeeController _controllerEmployee;

        /// <summary>
        /// Initilize the test case.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _mockBllEmployee = new Mock<BusinessLogicContract.IEmployee>();
            _controllerEmployee = new EmployeeController(_mockBllEmployee.Object);
        }

        /// <summary>
        /// Test if Index() behave expectedly or not.
        /// </summary>
        [TestMethod]
        public void IsIndexReturnCorrectResults()
        {
            // Arrange.
            _mockBllEmployee
                .Setup(businessLogic => businessLogic.GetAll())
                .Returns(new List<Models::Employee>() 
                { 
                    new Models::Employee() { Id = 1, Name = "emp1", Department = "HR" }, 
                    new Models::Employee() { Id = 2, Name = "emp2", Department = "Software" } 
                });

            // Act.
            ViewResult vrEmployee = _controllerEmployee.Index() as ViewResult;

            IList<Models::Employee> employees = null;
            if (vrEmployee != null)
            {
                employees = vrEmployee.Model as IList<Models::Employee>;
            }

            // Assert.
            Assert.IsNotNull(vrEmployee);
            Assert.IsNotNull(employees);
            Assert.AreEqual(2, employees.Count);
            
            Assert.AreEqual("emp1", employees[0].Name);
            Assert.AreEqual("HR", employees[0].Department);
            
            Assert.AreEqual("emp2", employees[1].Name);
            Assert.AreEqual("Software", employees[1].Department);
        }

        /// <summary>
        /// Test if Details() behave expectedly or not.
        /// </summary>
        [TestMethod]
        public void IsDetailsReturnCorrectResults()
        {
            // Arrange.
            int employeeId = 1;

            _mockBllEmployee
                .Setup(businessLogic => businessLogic.GetById(employeeId))
                .Returns(new Models::Employee() { Id = 2, Name = "emp2", Department = "Software" });

            // Act.
            ViewResult vrEmployee = _controllerEmployee.Details(employeeId) as ViewResult;

            Models::Employee employee = null;
            if (vrEmployee != null)
            {
                employee = vrEmployee.Model as Models::Employee;
            }

            // Assert.
            Assert.IsNotNull(vrEmployee);
            Assert.IsNotNull(employee);

            Assert.AreEqual("emp2", employee.Name);
            Assert.AreEqual("Software", employee.Department);
        }

        /// <summary>
        /// Test if Edit() work properly in the case of successful database update.
        /// </summary>
        [TestMethod]
        public void IsEditWorkingProperlyIfRecordUpdated()
        {
            // Arrange.
            Models::Employee employee = new Models::Employee();

            _mockBllEmployee
                .Setup(businessLogic => businessLogic.Update(employee))
                .Returns(true);

            object nameToTest;

            // Act.
            RedirectToRouteResult rtrEmployee = _controllerEmployee.Edit(employee) as RedirectToRouteResult;

            // Assert.
            Assert.IsNotNull(rtrEmployee);

            Assert.IsTrue(rtrEmployee.RouteValues.TryGetValue("controller", out nameToTest));
            Assert.AreEqual("Employee", nameToTest);

            Assert.IsTrue(rtrEmployee.RouteValues.TryGetValue("action", out nameToTest));
            Assert.AreEqual("Index", nameToTest);
        }

        /// <summary>
        /// Test if Edit() work properly in the case of unsuccessful database update.
        /// </summary>
        [TestMethod]
        public void IsEditWorkingProperlyIfRecordNotUpdated()
        {
            // Arrange.
            Models::Employee employee = new Models::Employee() { Id = 1, Name = "emp1", Department = "Software" };

            _mockBllEmployee
                .Setup(businessLogic => businessLogic.Update(employee))
                .Returns(false);

            // Act.
            ViewResult vrEmployee = _controllerEmployee.Edit(employee) as ViewResult;

            Models::Employee employeeToTest = null;
            if (vrEmployee != null)
            {
                employeeToTest = vrEmployee.Model as Models::Employee;
            }

            // Assert.
            Assert.IsNotNull(vrEmployee);
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
            _controllerEmployee.Dispose();
        }
    }
}
