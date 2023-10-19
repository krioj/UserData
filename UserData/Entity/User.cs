
namespace UserData.Entitys
{
    public class User : EntityBase
    {

        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        public bool Administrator { get; set; }
        public bool Moderator { get; set; }

        public override string ToString()
        {
            if (Administrator == true)
                return $"Id: {Id}, NickName: {NickName} (Administrator)";
            if (Moderator == true)
                return $"Id: {Id}, NickName: {NickName} (Moderator)";
            return $"Id: {Id}, NickName: {NickName}";
        }

    }
}