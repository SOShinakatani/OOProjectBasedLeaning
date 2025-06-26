using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{
    public interface Company : Model
    {
        Company AddTimeTracker(TimeTracker timeTracker);
        Employee FindEmployeeById(int id);
        Company AddEmployee(Employee employee);
        Company RemoveEmployee(Employee employee);
        void ClockIn(Employee employee);
        void ClockOut(Employee employee);
        bool IsAtWork(Employee employee);
    }

    public class CompanyModel : ModelEntity, Company
    {
        private TimeTracker timeTracker = NullTimeTracker.Instance;
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public CompanyModel() : this(string.Empty) { }

        public CompanyModel(string name)
        {
            Name = name;
            AcquireEmployees().ForEach(employee =>
            {
                employee.AddCompany(this);
                AddEmployee(employee);
            });
        }

        public Company AddTimeTracker(TimeTracker timeTracker)
        {
            this.timeTracker = timeTracker;
            return this;
        }

        public Employee FindEmployeeById(int id)
        {
            return employees.GetValueOrDefault(id, NullEmployee.Instance);
        }

        public Company AddEmployee(Employee employee)
        {
            if (!employees.ContainsKey(employee.Id))
            {
                employees.Add(employee.Id, employee);
            }
            return this;
        }

        public Company RemoveEmployee(Employee employee)
        {
            if (employees.ContainsKey(employee.Id))
            {
                employees.Remove(employee.Id);
            }
            return this;
        }

        public void ClockIn(Employee employee)
        {
            timeTracker.PunchIn(FindEmployeeById(employee.Id).Id);
        }

        public void ClockOut(Employee employee)
        {
            timeTracker.PunchOut(FindEmployeeById(employee.Id).Id);
        }

        public bool IsAtWork(Employee employee)
        {
            return timeTracker.IsAtWork(FindEmployeeById(employee.Id).Id);
        }

        private static readonly List<Employee> staticEmployeeList = new List<Employee>
        {
            new Manager(1, "Manager1"),
            new Manager(2, "Manager2"),
            new EmployeeModel(1000, "Employee1000"),
            new EmployeeModel(2000, "Employee2000"),
            new EmployeeModel(3000, "Employee3000")
        };

        private List<Employee> AcquireEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {
                XDocument doc = XDocument.Load("employees.xml");
                foreach (var element in doc.Descendants("Employee"))
                {
                    int id = int.Parse(element.Element("Id")?.Value ?? "0");
                    string name = element.Element("Name")?.Value ?? "Unknown";
                    string role = element.Element("Role")?.Value ?? "Employee";

                    if (role == "Manager")
                    {
                        employeeList.Add(new Manager(id, name));
                    }
                    else
                    {
                        employeeList.Add(new EmployeeModel(id, name));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading employees: " + ex.Message);
                employeeList = staticEmployeeList;
            }

            return employeeList;
        }
    }

    public class NullCompany : ModelEntity, Company, NullObject
    {
        private static readonly Company instance = new NullCompany();

        private NullCompany() { }

        public static Company Instance => instance;

        public override string Name
        {
            get => string.Empty;
            set { /* do nothing */ }
        }

        public Company AddTimeTracker(TimeTracker timeTracker) => this;

        public Employee FindEmployeeById(int id) => NullEmployee.Instance;

        public Company AddEmployee(Employee employee) => this;

        public Company RemoveEmployee(Employee employee) => this;

        public void ClockIn(Employee employee) { }

        public void ClockOut(Employee employee) { }

        public bool IsAtWork(Employee employee) => false;
    }
}
