namespace OOProjectBasedLeaning.Models
{
    public class NullEmployee : Employee, NullObject
    {
        private static readonly NullEmployee instance = new NullEmployee();

        // コンストラクタは外部からのインスタンス生成を防ぐためprivateに
        private NullEmployee() : base(0, string.Empty) { }

        // シングルトンインスタンスを公開
        public static Employee Instance => instance;

        // NullObjectパターンのため、何もしないメソッド群
        public override void AddCompany(Company company) { }

        public Company In() => NullCompany.Instance;

        public void ClockIn() { }

        public void ClockOut() { }

        // Employeeに定義されていないため削除
        // public bool IsAtWork() => false;
    }
}
