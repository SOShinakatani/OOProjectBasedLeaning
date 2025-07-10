using OOProjectBasedLeaning.Models;
using OOProjectBasedLeaning;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static TimeTrackerModel;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// 会社の基本インターフェース。
    /// 従業員の管理や出退勤の操作を定義。
    /// </summary>
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

    /// <summary>
    /// 実際の会社の実装クラス。
    /// 従業員の管理や出退勤の記録を行う。
    /// </summary>
    public class CompanyModel : ModelEntity, Company
    {
        private TimeTracker timeTracker = NullTimeTracker.Instance;
        private List<Employee> employees = new List<Employee>();

        /// <summary>
        /// 名前なしの会社を初期化。
        /// </summary>
        public CompanyModel() : this(string.Empty) { }

        /// <summary>
        /// 指定された名前で会社を初期化し、従業員情報を読み込む。
        /// </summary>
        public CompanyModel(string name)
        {
            Name = name;
            foreach (var employee in AcquireEmployees())
            {
                employee.AddCompany(this);
                AddEmployee(employee);
            }
        }

        /// <summary>
        /// 出退勤管理用のタイムトラッカーを設定。
        /// </summary>
        public Company AddTimeTracker(TimeTracker timeTracker)
        {
            this.timeTracker = timeTracker;
            return this;
        }

        /// <summary>
        /// 名前から従業員を検索。見つからない場合は NullEmployee を返す。
        /// </summary>
        public Employee FindEmployeeByName(string name)
        {
            return employees.Find(e => e.Name == name) ?? NullEmployee.Instance;
        }

        /// <summary>
        /// 従業員を追加。重複する名前の従業員は追加しない。
        /// </summary>
        public Company AddEmployee(Employee employee)
        {
            if (FindEmployeeByName(employee.Name) is NullEmployee)
            {
                employees.Add(employee);
            }
            return this;
        }

        /// <summary>
        /// 指定された従業員を削除（名前一致で削除）。
        /// </summary>
        public Company RemoveEmployee(Employee employee)
        {
            employees.RemoveAll(e => e.Name == employee.Name);
            return this;
        }

        /// <summary>
        /// 従業員の出勤を記録。
        /// </summary>
        public void ClockIn(Employee employee) => Track(employee, timeTracker.PunchIn);

        /// <summary>
        /// 従業員の退勤を記録。
        /// </summary>
        public void ClockOut(Employee employee) => Track(employee, timeTracker.PunchOut);

        /// <summary>
        /// 従業員が勤務中かどうかを判定。
        /// </summary>
        public bool IsAtWork(Employee employee) => timeTracker.IsAtWork(employee.Id);

        /// <summary>
        /// 出退勤処理の共通化メソッド。
        /// </summary>
        private void Track(Employee employee, Action<int> action)
        {
            action(employee.Id);
        }

        /// <summary>
        /// XML読み込み失敗時の代替従業員リスト。
        /// </summary>
        public static List<Employee> StaticEmployeeList { get; } = new List<Employee>
        {
            new Manager(1, "Manager1"),
            new Manager(2, "Manager2"),
            new EmployeeModel(1000, "Employee1000"),
            new EmployeeModel(2000, "Employee2000"),
            new EmployeeModel(3000, "Employee3000")
        };

        /// <summary>
        /// 従業員情報を XML ファイルから取得。
        /// 読み込み失敗時は StaticEmployeeList を返す。
        /// </summary>
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

        /// <summary>
        /// XML ファイルから従業員情報を読み込む。
        /// </summary>
        private List<Employee> LoadEmployeesFromXml(string path)
        {
            var doc = XDocument.Load(path);
            var employeeList = new List<Employee>();

            foreach (var element in doc.Descendants("Employee"))
            {
                string name = element.Element("Name")?.Value ?? "Unknown";
                string role = element.Element("Role")?.Value ?? "Employee";
                int id = GenerateId(name);

                employeeList.Add(role == "Manager"
                    ? new Manager(id, name)
                    : new EmployeeModel(id, name));
            }

            return employeeList;
        }

        /// <summary>
        /// 名前から一意な ID を生成（ハッシュ値を使用）。
        /// </summary>
        private int GenerateId(string name)
        {
            return Math.Abs(name.GetHashCode());
        }
    }

    /// <summary>
    /// Null オブジェクトパターンによる Company の空実装。
    /// 実体が存在しない会社を表す。
    /// </summary>
    public class NullCompany : ModelEntity, Company, NullObject
    {
        private static readonly Company instance = new NullCompany();

        private NullCompany() { }

        /// <summary>
        /// NullCompany のインスタンス（シングルトン）。
        /// </summary>
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
