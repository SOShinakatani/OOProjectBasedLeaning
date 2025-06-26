using System;
using System.Collections.Generic;

namespace OOProjectBasedLeaning.Models
{
    public interface Employee
    {
        int Id { get; }
        string Name { get; set; }

        void AddCompany(CompanyModel companyModel);
    }

    public class EmployeeModel : Employee
    {
        private static int nextId = 1;
        public int Id { get; }
        public string Name { get; set; }

        public EmployeeModel(int v, string name)
        {
            Id = nextId++;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
