namespace OOProjectBasedLeaning.Models
{
    public interface Employee
    {
        int Id { get; }
        string Name { get; set; }

        Employee AddCompany(Company company);
        Employee RemoveCompany();
        Company In();
        void ClockIn();
        void ClockOut();
        bool IsAtWork();
    }

    public class EmployeeModel : ModelEntity, Employee
    {
        private static int nextId = 1;
        private Company company = NullCompany.Instance;

        public int Id { get; }
        public override string Name { get; set; }

        public EmployeeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public EmployeeModel(string name) : this(nextId++, name) { }

        public override string ToString() => Name;

        public Employee AddCompany(Company company)
        {
            this.company = company;
            return this;
        }

        public Employee RemoveCompany()
        {
            this.company = NullCompany.Instance;
            return this;
        }

        public Company In() => company;

        public void ClockIn() => company.ClockIn(this);

        public void ClockOut() => company.ClockOut(this);

        public bool IsAtWork() => company.IsAtWork(this);
    }

    public class Manager : EmployeeModel
    {
        public Manager(int id, string name) : base(id, name) { }
        public Manager(string name) : base(name) { }
    }
}
