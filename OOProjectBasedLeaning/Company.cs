using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{ 

    /// <summary>
    /// 会社の基本的な操作を定義するインターフェース。
    /// </summary>
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



    /// <summary>
    /// 実際の会社のモデルを表すクラス。
    /// 従業員の管理や勤務時間の記録などを行います。
    /// </summary>
    public class CompanyModel : ModelEntity, Company
    {
        /// <summary>
        /// 操作や処理の経過時間を追跡するためのトラッカーです。
        /// </summary>
        /// <remarks>
        /// このフィールドは <see cref="NullTimeTracker"/> のインスタンスで初期化されます。
        /// これは <see cref="TimeTracker"/> クラスの何もしない（no-op）実装です。
        /// 時間の計測が必要な場合は、このフィールドをカスタムの <see cref="TimeTracker"/> 実装に置き換えてください。
        /// </remarks>
        private TimeTracker timeTracker = NullTimeTracker.Instance;

        /// <summary>
        /// 従業員情報を保持する辞書。キーは従業員ID、値は <see cref="Employee"/> オブジェクトです。
        /// </summary>
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public CompanyModel() : this(string.Empty) { }

        public CompanyModel(string name)
        {
            AcquireEmployees().ForEach(employee =>
            {
                employee.AddCompany(this);
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
            employees.Add(employee.Id, employee);
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

        private static List<Employee> staticEmployeeList = new List<Employee>()
        {
            new Manager(1, "Manager1"), new Manager(2, "Manager2"),
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

    /// <summary>
    /// ヌルオブジェクトパターンを用いた、何もしない会社の実装です。
    /// </summary>
    /// <remarks>
    /// このクラスは <see cref="Company"/> インターフェースの安全な代替として機能し、
    /// 実際の処理を行わずに、例外を回避するために使用されます。
    /// </remarks>
    public class NullCompany : ModelEntity, Company, NullObject
    {
        /// <summary>
        /// 唯一のインスタンス（シングルトン）を保持します。
        /// </summary>
        private static Company instance = new NullCompany();

        /// <summary>
        /// プライベートコンストラクタ。外部からのインスタンス化を防ぎます。
        /// </summary>
        private NullCompany() { }

        /// <summary>
        /// <see cref="NullCompany"/> の唯一のインスタンスを取得します。
        /// </summary>
        public static Company Instance => instance;

        /// <summary>
        /// 会社名を取得または設定します。常に空文字列を返し、設定操作は無視されます。
        /// </summary>
        public override string Name
        {
            get => string.Empty;
            set { /* 何もしない */ }
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
