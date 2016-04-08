using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Server.db;

namespace Server
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            SqliteHelper sqlite = new SqliteHelper();
            sqlite.Insert();

            ContentValue values = new ContentValue();
            values.Put("@userName","xwdoor");
            DataTable table = sqlite.Query("R_User", new[] { "userName","phone" }, "userName=@userName", values);
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(row.ItemArray[0]);
            }
        }
    }
}