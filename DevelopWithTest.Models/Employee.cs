namespace DevelopWithTest.Models
{
    /// <summary>
    /// Employee model. This class has been used throughout 
    /// this project in different layers.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Identification number of Employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Department of Employee.
        /// </summary>
        public string Department { get; set; }
    }
}
