using System;
using System.Collections.Generic;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.DataAccess.Contract
{
    /// <summary>
    /// Dataaccess layer of Employee contract.
    /// </summary>
    public interface IEmployee : IDisposable
    {
        /// <summary>
        /// Get all Employee records.
        /// </summary>
        /// <returns>Collection of Employees.</returns>
        IList<Models::Employee> GetAll();

        /// <summary>
        /// Get single Employee record by Employee identification number.
        /// </summary>
        /// <param name="employeeId">Employee identification number.</param>
        /// <returns>Single Employee record.</returns>
        Models::Employee GetById(int employeeId);
    }
}
