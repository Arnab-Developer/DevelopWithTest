using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccessContract = DevelopWithTest.DataAccess.Contract;
using Models = DevelopWithTest.Models;

namespace DevelopWithTest.DataAccess
{
    /// <summary>
    /// Dataaccess layer of Employee.
    /// </summary>
    public class Employee : DataAccessContract::IEmployee
    {
        private readonly SqlConnection _con;
        private readonly SqlCommand _cmd;
        private readonly SqlDataAdapter _da;
        private readonly DataTable _dt;
        private bool _isDisposed;

        /// <summary>
        /// Connection string of database from where Employee records will be fetched.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Initilize a new instence of Employee.
        /// </summary>
        public Employee()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            _con = new SqlConnection(ConnectionString);
            _cmd = new SqlCommand() { Connection = _con, CommandType = CommandType.Text };
            _da = new SqlDataAdapter(_cmd);
            _dt = new DataTable("Employees");
            _isDisposed = false;
        }

        /// <summary>
        /// Cleanup an Employee instence.
        /// </summary>
        ~Employee()
        {
            DisposeObjects();
        }

        /// <summary>
        /// Get all Employee records.
        /// </summary>
        /// <returns>Collection of Employees.</returns>
        public IList<Models::Employee> GetAll()
        {
            if (!_isDisposed)
            {
                _cmd.CommandText = "SELECT * FROM Employees";

                _da.Fill(_dt);

                IList<Models::Employee> employees = null;
                if (_dt.Rows.Count > 0)
                {
                    employees = _dt.AsEnumerable().Select(dataRowEmployee => new Models::Employee()
                    {
                        Id = dataRowEmployee.Field<int>("EmployeeID"),
                        Name = dataRowEmployee.Field<string>("Name"),
                        Department = dataRowEmployee.Field<string>("Department")
                    }).ToList();
                }

                return employees;
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Get single Employee record by Employee identification number.
        /// </summary>
        /// <param name="employeeId">Employee identification number.</param>
        /// <returns>Single Employee record.</returns>
        public Models::Employee GetById(int employeeId)
        {
            if (!_isDisposed)
            {
                _cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @employeeId";
                _cmd.Parameters.AddWithValue("employeeId", employeeId);

                _da.Fill(_dt);

                Models::Employee employee = null;
                if (_dt.Rows.Count > 0)
                {
                    employee = _dt.AsEnumerable().Select(dataRowEmployee => new Models::Employee()
                    {
                        Id = dataRowEmployee.Field<int>("EmployeeID"),
                        Name = dataRowEmployee.Field<string>("Name"),
                        Department = dataRowEmployee.Field<string>("Department")
                    }).SingleOrDefault();
                }

                return employee;
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Update an Employee record.
        /// </summary>
        /// <param name="employee">Employee to be updated.</param>
        /// <returns>True if success, false if fail.</returns>
        public bool Update(Models.Employee employee)
        {
            if (!_isDisposed)
            {
                _cmd.CommandText = "UPDATE Employees SET Name=@Name, Department=@Department WHERE EmployeeID=@EmployeeID";
                
                _cmd.Parameters.AddWithValue("@Name", employee.Name);
                _cmd.Parameters.AddWithValue("@Department", employee.Department);
                _cmd.Parameters.AddWithValue("@EmployeeID", employee.Id);

                if (_con.State != ConnectionState.Open)
                {
                    _con.Open(); 
                }

                int effectedRecords = _cmd.ExecuteNonQuery();

                if (effectedRecords > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DisposeObjects();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Logic of cleanup. This can be overriden for custom cleanup logic.
        /// </summary>
        protected virtual void DisposeObjects()
        {
            if (!_isDisposed)
            {
                if (_con.State == ConnectionState.Open)
                {
                    _con.Close();
                }
                _con.Dispose();
                _cmd.Dispose();
                _da.Dispose();

                _isDisposed = true;
            }
        }
    }
}
