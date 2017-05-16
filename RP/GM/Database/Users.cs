using System.ComponentModel.DataAnnotations;

namespace Roleplay.User
{
    public class Users
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

        public string Social { get; set; }
    }
}
