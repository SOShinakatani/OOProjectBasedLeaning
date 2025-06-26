using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    public class Manager : EmployeeModel
    {
        public Manager(int id, string name) : base(id, name)
        {
        }

        public Manager(string name) : base(name)
        {
        }

        // Manager 独自の機能を追加したい場合はここに記述
    }
}
