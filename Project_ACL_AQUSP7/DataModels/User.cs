using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ACL_AQUSP7.DataModels
{
    public class User
    {
        public string Username { get; set; }
        public List<string> Permissions { get; set; }

        public User(string username, List<string> permissions)
        {
            Username = username;
            Permissions = permissions;
        }
    }
}
