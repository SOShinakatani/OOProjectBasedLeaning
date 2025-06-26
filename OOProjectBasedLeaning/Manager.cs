using OOProjectBasedLeaning.Models;

namespace OOProjectBasedLeaning
{
    internal class Manager : Employee
    {
        private int v1;
        private string v2;

        public Manager(int v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}