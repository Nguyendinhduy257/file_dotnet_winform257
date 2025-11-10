using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_set_up_app
{
    internal class UserSession
    {
        public static string Username { get; private set; }
        public static void SetUser(string username)
        {
            Username = username;
        }

        public static void ClearUser()
        {
            Username = null;
        }
    }
}
