using System.Collections.Generic;

namespace OOProjectBasedLeaning.Models
{
    public static class EmployeeRepository
    {
        private static readonly List<EmployeeModel> employees = new List<EmployeeModel>();

        public static void Add(EmployeeModel employee)
        {
            employees.Add(employee);
        }

        public static List<EmployeeModel> GetAll()
        {
            return new List<EmployeeModel>(employees);
        }

        public static void Clear()
        {
            employees.Clear();
        }
    }
}
