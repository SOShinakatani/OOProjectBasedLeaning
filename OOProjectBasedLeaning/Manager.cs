namespace OOProjectBasedLeaning.Models
{
    public class Manager : Employee
    {
        public Manager(int id, string name) : base(id, name)
        {
            // コンストラクタは親クラスに任せるので特に変更なし
        }

        // Manager固有の機能が追加される場合はここに記述
    }
}
