using System;

namespace OOProjectBasedLeaning.Models
{
    /// <summary>
    /// ヌルオブジェクトパターンを用いた、何もしない従業員の実装。
    /// </summary>
    public class NullEmployee : ModelEntity, Employee, NullObject
    {
        private static readonly Employee instance = new NullEmployee();

        private NullEmployee() { }

        /// <summary>
        /// 唯一のインスタンスを取得します。
        /// </summary>
        public static Employee Instance => instance;

        /// <summary>
        /// 従業員ID。常に0。
        /// </summary>
        public int Id => 0;

        /// <summary>
        /// 名前。常に空文字列。
        /// </summary>
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

        public override string ToString() => "(未所属)";

        public void AddCompany(CompanyModel companyModel)
        {
            throw new NotImplementedException();
        }
    }
}
