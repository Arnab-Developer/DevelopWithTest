using System.Collections.Generic;
using BusinessLogicContract = DevelopWithTest.BusinessLogic.Contract;
using DataAccessContract = DevelopWithTest.DataAccess.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.BusinessLogic
{
    /// <summary>
    /// Business logic layer of Employee.
    /// </summary>
    public class Employee : BusinessLogicContract::IEmployee
    {
        /// <summary>
        /// Gets or sets data access Employee instence.
        /// </summary>
        public DataAccessContract::IEmployee _dataAccessEmployee { get; set; }

        /// <summary>
        /// Initilize a new instence of Employee.
        /// </summary>
        /// <param name="dataAccessEmployee">Data access Employee instence.</param>
        public Employee(DataAccessContract::IEmployee dataAccessEmployee)
        {
            _dataAccessEmployee = dataAccessEmployee;
        }

        /// <summary>
        /// Get single Employee record by Employee identification number.
        /// </summary>
        /// <param name="employeeId">Employee identification number.</param>
        /// <returns>Single Employee record.</returns>
        public IList<Models::Employee> GetAll()
        {
            return _dataAccessEmployee.GetAll();
        }

        /// <summary>
        /// Get single Employee record by Employee identification number.
        /// </summary>
        /// <param name="employeeId">Employee identification number.</param>
        /// <returns>Single Employee record.</returns>
        public Models::Employee GetById(int employeeId)
        {
            return _dataAccessEmployee.GetById(employeeId);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _dataAccessEmployee.Dispose();
        }
    }
}
