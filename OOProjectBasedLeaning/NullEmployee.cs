namespace OOProjectBasedLeaning.Models
{
    public class NullEmployee : ModelEntity, Employee, NullObject
    {
        private static readonly Employee instance = new NullEmployee();

        private NullEmployee() { }

        public static Employee Instance => instance;

        public int Id => 0;

        public override string Name
        {
            get => string.Empty;
            set { }
        }

        public Employee AddCompany(Company company) => this;
        public Employee RemoveCompany() => this;
        public Company In() => NullCompany.Instance;
        public void ClockIn() { }
        public void ClockOut() { }
        public bool IsAtWork() => false;
    }
}
