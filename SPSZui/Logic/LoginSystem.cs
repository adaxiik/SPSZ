using System;

namespace SPSZui.Logic
{
    public class LoginSystem
    {
        public static int? Id { get;private set; } = null;
        public static bool IsLoggedIn{ get;private set; } = false;
        public static bool LogIn(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                Id = 69;
                IsLoggedIn = true;
                return true;
            }
            else
            {
                Id = null;
                IsLoggedIn = false;
                return false;
            }
        }
        public static void LogOut()
        {
            Id = null;
            IsLoggedIn = false;
        }
    }
}