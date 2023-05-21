using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunterBrowserClient
{
    public class HhUser
    {
        internal string Login { get; set; }
        internal string Password { get; set; }

        public HhUser(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
