using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.db
{
    /// <summary>
    /// 用户数据库操作
    /// </summary>
    public class UserDao
    {
        public UserDao()
        {
        
        }

        public static string TableBill = "R_User";
        public static string ColumnId = "_id";
        public static string ColumnUserName = "userName";
        public static string ColumnRealName = "realName";
        public static string ColumnPhone = "phone";
        public static string ColumnPassword = "password";
        public static string ColumnMail = "mail";
    }
}