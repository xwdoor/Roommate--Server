using System;
using Newtonsoft.Json;
using Roommate.Server.db;
using Roommate.Server.model;

namespace Roommate.Server
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
                    int resultCode = UserDao.Instance.AddUser(user);
                    if (resultCode == 1)
                    {
                        response.Code = 0;
                        user.Password = "";
                        response.Result = JsonConvert.SerializeObject(user);
                    }
                    else
                    {
                        response.Code = 4;
                        response.Error = "数据提交错误";
                    }
                }
            }

            Response.Write(JsonConvert.SerializeObject(response));
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
            int resultCode = UserDao.Instance.AddUser(user);
            ResponseJson response = new ResponseJson();
            if (resultCode == 1)
            {
                response.Code = 0;
                user.Password = "";
                response.Result = JsonConvert.SerializeObject(user);
            }
            Response.Write(JsonConvert.SerializeObject(response));
        }
    }
}