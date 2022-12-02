using System.Runtime.CompilerServices;
using System;
using SPSZDataLayer.GlobalConfig;
using SPSZDomainLayer.Mapper;
using SPSZDomainLayer.Model;
using SPSZDataLayer.TableGateway;

namespace SPSZDomainLayer.Service
{
    public class LoginSystem
    {
        public static int? Id { get;private set; } = null;
        public static Person LogedAs { get; private set; } = null;
        public static bool IsLoggedIn{ get;private set; } = false;

        private static bool AsParent(Parent parent, string password)
        {
            if (parent.Password == DataUtils.ToMD5(password))
            {
                Id = parent.Id;
                IsLoggedIn = true;
                LogedAs = parent;
                return true;
            }
            return false;
        }


        private static bool AsTeacher(Teacher teacher, string pass)
        {
            if (teacher.Password == DataUtils.ToMD5(pass))
            {
                Id = teacher.Id;
                IsLoggedIn = true;
                LogedAs = teacher;
                return true;
            }
            return false;
        }

        public static bool LogIn(int loginID, string password)
        {
            var r = Config.Connection.ParentTG.GetById(loginID);
            if (r is null){
                r = Config.Connection.TeacherTG.GetById(loginID);
                if(r is null)
                    return false;
            
                return AsTeacher(TeacherMapper.FromRow(r), password);
            }

            return AsParent(ParentMapper.FromRow(r), password);

        }
        public static void LogOut()
        {
            Id = null;
            IsLoggedIn = false;
        }
    }
}