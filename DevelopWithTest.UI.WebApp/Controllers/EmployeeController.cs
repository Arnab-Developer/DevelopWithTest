using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogicContract = DevelopWithTest.BusinessLogic.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.UI.WebApp.Controllers
{
    /// <summary>
    /// Employee controller.
    /// </summary>
    public class EmployeeController : Controller
    {
        private readonly BusinessLogicContract::IEmployee _bllEmployee;

        /// <summary>
        /// Initilize a new instence of Employee controller.
        /// </summary>
        /// <param name="bllEmployee"></param>
        public EmployeeController(BusinessLogicContract::IEmployee bllEmployee)
        {
            _bllEmployee = bllEmployee;
        }

        /// <summary>
        /// Startup funtionality of this controller. Get all Employee
        /// records and return view.
        /// </summary>
        /// <returns>View with all Employee records.</returns>
        public ActionResult Index()
        {
            IList<Models::Employee> emps = _bllEmployee.GetAll();
            return View(emps);
        }

        /// <summary>
        /// Get single Employee record by Employee identification
        /// number and return view.
        /// </summary>
        /// <param name="id">Employee identification number.</param>
        /// <returns>View with single Employee record.</returns>
        public ActionResult Details(int id)
        {
            Models::Employee employee = _bllEmployee.GetById(id);
            return View(employee);
        }

        /// <summary>
        /// Dispose the Employee controller instence.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            _bllEmployee.Dispose();            
        }
    }
}
