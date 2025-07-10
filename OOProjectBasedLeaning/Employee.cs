using System;

namespace OOProjectBasedLeaning.Models
{
    /// <summary>
    /// 社員（Employee）を表す基本クラスです。
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 社員の一意なID。
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 社員の名前。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Employee クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="id">社員ID</param>
        /// <param name="name">社員名（null不可）</param>
        public Employee(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// 社員を会社に関連付けます。
        /// 派生クラスでオーバーライドして使用してください。
        /// </summary>
        /// <param name="company">関連付ける会社</param>
        public virtual void AddCompany(Company company)
        {
            // 基底クラスでは処理なし。必要に応じて派生クラスで実装。
        }

        /// <summary>
        /// 社員の情報を文字列として返します。
        /// </summary>
        /// <returns>名前とIDを含む文字列</returns>
        public override string ToString()
        {
            return $"{Name} (ID: {Id})";
        }
    }
}
