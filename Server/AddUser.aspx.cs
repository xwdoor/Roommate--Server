using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Server.db;
using Server.model;

namespace Server
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseJson response = new ResponseJson();

            string userName = Request["userName"];
            string realName = Request["realName"];
            string phone = Request["phone"];
            string mail = Request["mail"];
            string password = Request["password"];

            if (string.IsNullOrEmpty(password))
            {
                response.Code = 3;
                response.Error = "用户信息不能为空";
            }
            else
            {
                if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(realName)
                    && string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(mail))
                {
                    response.Code = 3;
                    response.Error = "用户信息不能为空";
                }
                else
                {
                    User user = new User()
                    {
                        UserName = userName,
                        RealName = realName,
                        Phone = phone,
                        Mail = mail,
                        Password = password
                    };
                    UserDao.Instance.AddUser(user);
                }
            }
        }

        protected void ButtonAddUser_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserName = "xwdoor",
                RealName = "肖威",
                Phone = "18684033888",
                Mail = "xwdoor@126.com",
                Password = "xwdoor"
            };
            UserDao.Instance.AddUser(user);
        }
    }
}