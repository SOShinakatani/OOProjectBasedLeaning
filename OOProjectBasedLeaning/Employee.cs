namespace OOProjectBasedLeaning.Models
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public virtual void AddCompany(Company company) { }

        public override string ToString()
        {
            return $"{Name} (ID: {Id})";
        }
    }
}
