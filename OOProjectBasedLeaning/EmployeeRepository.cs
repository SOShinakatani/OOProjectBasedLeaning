using System.Collections.Generic;

namespace OOProjectBasedLeaning.Models
{
    /// <summary>
    /// 従業員情報を管理する静的リポジトリクラス。
    /// 従業員の追加、取得、クリア機能を提供。
    /// </summary>
    public static class EmployeeRepository
    {
        // 登録された従業員のリスト（内部保持用）
        private static readonly List<EmployeeModel> employees = new List<EmployeeModel>();

        /// <summary>
        /// 従業員をリポジトリに追加します。
        /// </summary>
        /// <param name="employee">追加する従業員</param>
        public static void Add(EmployeeModel employee)
        {
            employees.Add(employee);
        }

        /// <summary>
        /// 登録されているすべての従業員を取得します。
        /// 新しいリストとして返すため、外部からの変更は不可。
        /// </summary>
        /// <returns>従業員の一覧</returns>
        public static List<EmployeeModel> GetAll()
        {
            return new List<EmployeeModel>(employees);
        }

        /// <summary>
        /// リポジトリ内の従業員情報をすべて削除します。
        /// </summary>
        public static void Clear()
        {
            employees.Clear();
        }
    }
}
