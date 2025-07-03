namespace OOProjectBasedLeaning.Models
{
    public class NullEmployee : Employee, NullObject
    {
        private static readonly NullEmployee instance = new NullEmployee();

        private NullEmployee() : base(0, string.Empty) { }

        public static Employee Instance => instance;

        public override void AddCompany(Company company) { }

        public Company In() => NullCompany.Instance;

        public void ClockIn() { }

        public void ClockOut() { }

        public bool IsAtWork() => false; // これは削除してOK（Employeeにない）
    }
}
