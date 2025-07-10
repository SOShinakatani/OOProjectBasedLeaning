using OOProjectBasedLeaning.Models;
using OOProjectBasedLeaning;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{
    public interface Company : Model
    {
        Company AddTimeTracker(TimeTracker timeTracker);
        Employee FindEmployeeByName(string name);
        Employee FindEmployeeById(int id); // 🔧 追加
        Company AddEmployee(Employee employee);
        Company RemoveEmployee(Employee employee);
        void ClockIn(Employee employee, WorkLocation location);
        void ClockOut(Employee employee, WorkLocation location);
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
            foreach (var employee in AcquireEmployees())
            {
                employee.AddCompany(this);
                AddEmployee(employee);
            }
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

        public Employee FindEmployeeById(int id) // 🔧 追加
        {
            return employees.Find(e => e.Id == id) ?? NullEmployee.Instance;
        }

        public Company AddEmployee(Employee employee)
        {
            if (FindEmployeeByName(employee.Name) is NullEmployee)
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

        public void ClockIn(Employee employee, WorkLocation location)
        {
            timeTracker.PunchIn(employee.Id, location);
        }

        public void ClockOut(Employee employee, WorkLocation location)
        {
            timeTracker.PunchOut(employee.Id, location);
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
            try
            {
                return LoadEmployeesFromXml("employees.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("従業員情報の読み込みに失敗しました: " + ex.Message);
                return StaticEmployeeList;
            }
        }

        private List<Employee> LoadEmployeesFromXml(string path)
        {
            var doc = XDocument.Load(path);
            var employeeList = new List<Employee>();

            foreach (var element in doc.Descendants("Employee"))
            {
                string name = element.Element("Name")?.Value ?? "Unknown";
                string role = element.Element("Role")?.Value ?? "Employee";
                int id = GenerateId(name);

                if (role == "Manager")
                    employeeList.Add(new Manager(id, name));
                else
                    employeeList.Add(new EmployeeModel(id, name));
            }

            return employeeList;
        }

        private int GenerateId(string name)
        {
            return Math.Abs(name.GetHashCode());
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
        public Employee FindEmployeeById(int id) => NullEmployee.Instance; // 🔧 追加
        public Company AddEmployee(Employee employee) => this;
        public Company RemoveEmployee(Employee employee) => this;
        public void ClockIn(Employee employee, WorkLocation location) { }
        public void ClockOut(Employee employee, WorkLocation location) { }
        public bool IsAtWork(Employee employee) => false;
    }

    // 🔽 NullTimeTrackerクラス
    public class NullTimeTracker : TimeTracker
    {
        private static readonly TimeTracker instance = new NullTimeTracker();

        private NullTimeTracker() { }

        public static TimeTracker Instance => instance;

        public void PunchIn(int employeeId) { }
        public void PunchIn(int employeeId, WorkLocation location) { }
        public void PunchOut(int employeeId) { }
        public void PunchOut(int employeeId, WorkLocation location) { }
        public bool IsAtWork(int employeeId) => false;
    }
}
