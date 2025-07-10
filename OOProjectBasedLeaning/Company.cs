using OOProjectBasedLeaning.Models;
using OOProjectBasedLeaning;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{
    // Companyインターフェース：会社の基本的な機能を定義
    public interface Company : Model
    {
        Company AddTimeTracker(TimeTracker timeTracker);          // タイムトラッカーの設定
        Employee FindEmployeeByName(string name);                 // 名前で従業員を検索
        Employee FindEmployeeById(int id);                        // IDで従業員を検索（🔧 追加）
        Company AddEmployee(Employee employee);                   // 従業員の追加
        Company RemoveEmployee(Employee employee);                // 従業員の削除
        void ClockIn(Employee employee, WorkLocation location);   // 出勤打刻
        void ClockOut(Employee employee, WorkLocation location);  // 退勤打刻
        bool IsAtWork(Employee employee);                         // 勤務中かどうかを判定
    }

    // CompanyModel：Companyインターフェースの実装クラス
    public class CompanyModel : ModelEntity, Company
    {
        private TimeTracker timeTracker = NullTimeTracker.Instance; // タイムトラッカー（初期はNull）
        private List<Employee> employees = new List<Employee>();    // 従業員リスト

        public CompanyModel() : this(string.Empty) { }

        public CompanyModel(string name)
        {
            Name = name;

            // XMLまたは静的リストから従業員を取得し、会社に登録
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
            // 同名の従業員がいない場合のみ追加
            if (FindEmployeeByName(employee.Name) is NullEmployee)
            {
                employees.Add(employee);
            }
            return this;
        }

        public Company RemoveEmployee(Employee employee)
        {
            // 名前一致で削除
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

        // XML読み込み失敗時に使用する静的な従業員リスト
        public static List<Employee> StaticEmployeeList { get; } = new List<Employee>
        {
            new Manager(1, "Manager1"),
            new Manager(2, "Manager2"),
            new EmployeeModel(1000, "Employee1000"),
            new EmployeeModel(2000, "Employee2000"),
            new EmployeeModel(3000, "Employee3000")
        };

        // 従業員情報をXMLから取得（失敗時はStaticEmployeeListを返す）
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

        // XMLファイルから従業員情報を読み込む
        private List<Employee> LoadEmployeesFromXml(string path)
        {
            var doc = XDocument.Load(path);
            var employeeList = new List<Employee>();

            foreach (var element in doc.Descendants("Employee"))
            {
                string name = element.Element("Name")?.Value ?? "Unknown";
                string role = element.Element("Role")?.Value ?? "Employee";
                int id = GenerateId(name);

                // 役職に応じてインスタンスを生成
                if (role == "Manager")
                    employeeList.Add(new Manager(id, name));
                else
                    employeeList.Add(new EmployeeModel(id, name));
            }

            return employeeList;
        }

        // 名前から一意なIDを生成（ハッシュ値の絶対値）
        private int GenerateId(string name)
        {
            return Math.Abs(name.GetHashCode());
        }
    }

    // NullCompany：CompanyのNullオブジェクト実装（安全なデフォルト動作）
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

    // NullTimeTracker：TimeTrackerのNullオブジェクト実装
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
