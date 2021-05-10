using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB.Dto.DataClasses
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Date_Created { get; set; }
        public DateTimeOffset Date_Updated { get; set; }
        public bool Is_Deactivated { get; set; }
        public Role Role { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }
    }
}
