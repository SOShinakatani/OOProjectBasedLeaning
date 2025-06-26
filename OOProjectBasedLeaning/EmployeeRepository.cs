using System.Collections.Generic;
using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public static class EmployeeRepository
    {
        private static List<Employee> employees = new List<Employee>();

        public static void Add(Employee employee)
        {
            if (employee != null && !employees.Contains(employee))
            {
                employees.Add(employee);
            }
        }

        public static List<Employee> GetAll()
        {
            return new List<Employee>(employees);
        }
    }
}
