using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{
    public interface Company : Model
    {
        Company AddTimeTracker(TimeTracker timeTracker);
        Employee FindEmployeeByName(string name);
        Company AddEmployee(Employee employee);
        Company RemoveEmployee(Employee employee);
        void ClockIn(Employee employee);
        void ClockOut(Employee employee);
        bool IsAtWork(Employee employee);
    }

    public class CompanyModel : ModelEntity, Company
    {
        private TimeTracker timeTracker = NullTimeTracker.Instance;
        private List<Employee> employees = new List<Employee>();

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

        public Employee FindEmployeeByName(string name)
        {
            return employees.Find(e => e.Name == name) ?? NullEmployee.Instance;
        }

        public Company AddEmployee(Employee employee)
        {
            if (!employees.Exists(e => e.Name == employee.Name))
            {
                employees.Add(employee);
            }
            return this;
        }

        public Company RemoveEmployee(Employee employee)
        {
            employees.RemoveAll(e => e.Name == employee.Name);
            return this;
        }

        public void ClockIn(Employee employee)
        {
            timeTracker.PunchIn(employee.Id);
        }

        public void ClockOut(Employee employee)
        {
            timeTracker.PunchOut(employee.Id);
        }

        public bool IsAtWork(Employee employee)
        {
            return timeTracker.IsAtWork(employee.Id);
        }

        public static List<Employee> StaticEmployeeList { get; } = new List<Employee>
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
                    string name = element.Element("Name")?.Value ?? "Unknown";
                    string role = element.Element("Role")?.Value ?? "Employee";
                    int id = GenerateId(name);

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
                employeeList = StaticEmployeeList;
            }

            return employeeList;
        }

        private int GenerateId(string name)
        {
            return Math.Abs(name.GetHashCode()); // 簡易的なID生成
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
            set { }
        }

        public Company AddTimeTracker(TimeTracker timeTracker) => this;

        public Employee FindEmployeeByName(string name) => NullEmployee.Instance;

        public Company AddEmployee(Employee employee) => this;

        public Company RemoveEmployee(Employee employee) => this;

        public void ClockIn(Employee employee) { }

        public void ClockOut(Employee employee) { }

        public bool IsAtWork(Employee employee) => false;
    }
}
