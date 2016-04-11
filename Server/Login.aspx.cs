using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Server.db;
using Server.model;

namespace Server
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResponseJson response = new ResponseJson();
            string loginName = Request["loginName"];
            string pwd = Request["pwd"];
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(pwd))
            {
                response.Code = 1;
                response.Error = "登录名或密码不能为空";
            }
            else
            {
                try
                {
                    User user = UserDao.Instance.GetUser(loginName, pwd);
                    string json = JsonConvert.SerializeObject(user);
                    if (string.IsNullOrEmpty(json))
                    {
                        response.Code = 2;
                        response.Error = "用户名密码错误";
                    }
                    else
                    {
                        response.Code = 0;
                        response.Result = json;
                    }
                }
                catch (Exception ex)
                {
                    response.Code = -1;
                    response.Error = ex.Message;
                }
            }

            Response.Write(JsonConvert.SerializeObject(response));
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}