using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Mail { get; set; }
    }
}