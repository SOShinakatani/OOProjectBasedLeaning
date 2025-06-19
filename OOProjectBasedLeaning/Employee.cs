using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OOProjectBasedLeaning
{
    /// <summary>
    /// 従業員の基本的な操作を定義するインターフェース。
    /// </summary>
    public interface Employee : Model
    {
        const int NEW = 0;

        int Id { get; }

        Employee AddCompany(Company company);

        Employee RemoveCompany();

        Company In();

        void ClockIn();

        void ClockOut();

        bool IsAtWork();
    }

    /// <summary>
    /// 従業員の基本モデルクラス。
    /// </summary>
    public class EmployeeModel : ModelEntity, Employee
    {
        private int id;
        private Company company = NullCompany.Instance;

        public EmployeeModel() : this(Employee.NEW) { }

        public EmployeeModel(int id) : this(id, string.Empty) { }

        public EmployeeModel(string name) : this(Employee.NEW, name) { }

        public EmployeeModel(int id, string name)
        {
            this.id = id;
            Name = name;
        }

        public override int GetHashCode() => Id;

        public override bool Equals(object? obj)
        {
            if (obj is Employee other)
            {
                return Id == other.Id;
            }
            return false;
        }

        public int Id => id;

        public Employee AddCompany(Company company)
        {
            this.company = company.AddEmployee(this);
            return this;
        }

        public Employee RemoveCompany()
        {
            company.RemoveEmployee(this);
            company = NullCompany.Instance;
            return this;
        }

        public Company In() => company;

        public void ClockIn() => company.ClockIn(this);

        public void ClockOut() => company.ClockOut(this);

        public bool IsAtWork() => company.IsAtWork(this);
    }

    /// <summary>
    /// マネージャーを表すクラス。部下管理機能を追加可能。
    /// </summary>
    public class Manager : EmployeeModel
    {
        private List<Employee> subordinates = new List<Employee>();

        public Manager() : base(Employee.NEW) { }

        public Manager(int id) : base(id, string.Empty) { }

        public Manager(string name) : base(Employee.NEW, name) { }

        public Manager(int id, string name) : base(id, name) { }

        /// <summary>
        /// 部下を追加します。
        /// </summary>
        public void AddSubordinate(Employee employee)
        {
            if (!subordinates.Contains(employee))
            {
                subordinates.Add(employee);
            }
        }

        /// <summary>
        /// 部下を削除します。
        /// </summary>
        public void RemoveSubordinate(Employee employee)
        {
            subordinates.Remove(employee);
        }

        /// <summary>
        /// 部下一覧を取得します。
        /// </summary>
        public List<Employee> GetSubordinates()
        {
            return new List<Employee>(subordinates);
        }
    }

    /// <summary>
    /// ヌルオブジェクトパターンを用いた、何もしない従業員の実装。
    /// </summary>
    public class NullEmployee : ModelEntity, Employee, NullObject
    {
        private static Employee instance = new NullEmployee();

        private NullEmployee() { }

        public static Employee Instance => instance;

        public int Id => Employee.NEW;

        public override string Name
        {
            get => string.Empty;
            set { /* do nothing */ }
        }

        public Employee AddCompany(Company company) => this;

        public Employee RemoveCompany() => this;

        public Company In() => NullCompany.Instance;

        public void ClockIn() { }

        public void ClockOut() { }

        public bool IsAtWork() => false;
    }
}
