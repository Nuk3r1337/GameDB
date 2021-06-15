using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB.Domain.DomainClasses
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Created_At { get; set; }
        public DateTimeOffset Updated_At { get; set; }
        public bool Is_Deactivated { get; set; }
        public Role Role { get; set; }
        public string ConfirmPassword { get; set; }
        public List<Game> Games { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }
    }

    public class Users_Has_Games
    {
        public User user { get; set; }
        public List<Game> Games { get; set; }
    }

    public class Insert_User_Games
    {
        public int Users_id { get; set; }
        public int Games_id { get; set; }
    }
}
