using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Server.db
{
    /// <summary>
    /// SQLite数据库封装
    /// </summary>
    public class SqliteHelper
    {
        public static string DatabaseFile = @"F:\GitHub\Roommate--Server\Server\bin\Roommate.db";

        public static string ConnectionString
        {
            get { return "Data Source =" + DatabaseFile; }
        }
        
        public List<string> TableList;

        public SqliteHelper()
        {
            InitDatabaseFile();
            TableList = new List<string>();
        }

        private void InitDatabaseFile()
        {
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                CreateTables();
            }
            else
            {
                FindTables();
            }

        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        private void CreateTables()
        {
            String tableBill = "R_BillData";
            string createBillData = 
                "create table " + tableBill + " (" +
                "_id integer primary key autoincrement, " +
                "money decimal(10,2), " +
                "payerId integer," +
                "billType integer," +
                "date varchar(20)," +
                "desc text)";
            ExecSQL(createBillData);

            string tableUser = "R_User";
            string createUser =
                "create table "+tableUser +" (" +
                "_id integer primary key autoincrement, " +
                "userName varchar(20)," +
                "realName varchar(20)," +
                "phone varchar(20)," +
                "password varchar(20)," +
                "mail varchar(20))";
            ExecSQL(createUser);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        private void ExecSQL(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        /// <summary>
        /// 查找数据库中的所有表格
        /// </summary>
        private void FindTables()
        {
            
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="whereClause"></param>
        /// <param name="whereArgs"></param>
        public DataSet GetUsers(string tableName, string whereClause, string[] whereArgs)
        {
            DataSet dataSet = new DataSet();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.Append(tableName);
            if (!string.IsNullOrEmpty(whereClause))
            {
                sb.Append(" where ");
            }
            
            return dataSet;
        }
    }
}