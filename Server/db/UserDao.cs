using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Server.model;

namespace Server.db
{
    /// <summary>
    /// 用户数据库操作
    /// </summary>
    public class UserDao
    {
        private readonly SqliteHelper sqliteHelper;
        public static readonly UserDao Instance = new UserDao();

        private UserDao()
        {
            sqliteHelper = new SqliteHelper();
        }

        public static string TableUser = "R_User";
        public static string ColumnId = "_id";
        public static string ColumnUserName = "userName";
        public static string ColumnRealName = "realName";
        public static string ColumnPhone = "phone";
        public static string ColumnPassword = "password";
        public static string ColumnMail = "mail";

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户信息</returns>
        public User GetUser(string loginName, string pwd)
        {
            ContentValue value = new ContentValue();
            value.Put(ColumnUserName, loginName);
            value.Put(ColumnRealName, loginName);
            value.Put(ColumnPhone, loginName);
            value.Put(ColumnMail, loginName);
            value.Put(ColumnPassword, pwd);
            DataTable table = sqliteHelper.Query(TableUser, null,
                string.Format("{0}=@{0} OR {1}=@{1} OR {2}=@{2} OR {3}=@{3} AND {4}=@{4}", ColumnUserName,
                    ColumnRealName, ColumnPhone, ColumnMail, ColumnPassword), value);

            User user = null;
            if (table.Rows.Count > 0)
            {
                user = new User
                {
                    Id = Convert.ToInt32(table.Rows[0][ColumnId].ToString().Trim()),
                    UserName = table.Rows[0][ColumnUserName].ToString().Trim(),
                    RealName = table.Rows[0][ColumnRealName].ToString().Trim(),
                    Phone = table.Rows[0][ColumnPhone].ToString().Trim(),
                    Mail = table.Rows[0][ColumnMail].ToString().Trim()
                };
            }
            return user;
        }

        public int AddUser(User user)
        {
            ContentValue value = new ContentValue();
            value.Put(ColumnUserName, user.UserName);
            value.Put(ColumnRealName, user.RealName);
            value.Put(ColumnPhone, user.Phone);
            value.Put(ColumnMail, user.Mail);
            value.Put(ColumnPassword, user.Password);
            int result = sqliteHelper.Insert(TableUser,value);
            return result;
        }
    }
}