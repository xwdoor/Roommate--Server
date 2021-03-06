﻿using System;
using System.Collections;
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
        public static string DatabaseFile = @"F:\Roommate.db";

        public static string ConnectionString
        {
            get { return "Data Source =" + DatabaseFile; }
        }
        
        public List<string> TableList;

        #region 私有方法
        
        public SqliteHelper()
        {
            TableList = new List<string>();
            InitDatabaseFile();
        }

        private void InitDatabaseFile()
        {
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
            }
            FindTables();
            CreateTables();
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
            if (!TableList.Contains(tableBill) && !IsTableExist(tableBill))
            {
                ExecSQL(createBillData);
            }

            string tableUser = "R_User";
            string createUser =
                "create table "+tableUser +" (" +
                "_id integer primary key autoincrement, " +
                "userName varchar(20)," +
                "realName varchar(20)," +
                "phone varchar(20)," +
                "password varchar(20)," +
                "mail varchar(20))";
            if (!TableList.Contains(tableUser) && !IsTableExist(tableUser))
            {
                ExecSQL(createUser);
            }
        }

        private bool IsTableExist(string tableName)
        {
            string sql = string.Format("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='{0}'", tableName);
            return ExecSQL(sql)==1;
        }

        /// <summary>
        /// 查找数据库中的所有表格
        /// </summary>
        private void FindTables()
        {
            string sql = "SELECT name FROM SQLITE_MASTER WHERE type='table' ORDER BY name";
            DataTable table = QueryData(sql);
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    TableList.Add((string)row[0]);
                }
            }
            
            Console.WriteLine(TableList);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>执行结果</returns>
        private int ExecSQL(string sql, SQLiteParameter[] parameters = null)
        {
            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            int result = cmd.ExecuteNonQuery();
            
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return result;
        }

        private DataTable QueryData(string sql, ContentValue whereArgs = null)
        {
            DataTable data = new DataTable();

            SQLiteConnection conn = new SQLiteConnection(ConnectionString);
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (whereArgs != null)
            {
                SQLiteParameter[] parameters = BuildParameter(whereArgs);
                cmd.Parameters.AddRange(parameters);
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            
            adapter.Fill(data);
            cmd.Dispose();
            conn.Dispose();
            return data;
        }

        private int InsertData(string sql, ContentValue value)
        {
            SQLiteParameter[] parameters = BuildParameter(value);
            return ExecSQL(sql, parameters);
        }

        /// <summary>
        /// 组建选择条件参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private SQLiteParameter[] BuildParameter(ContentValue value)
        {
            //LinQ实现
            //return value.Keys.Select(key => new SQLiteParameter(key, value.Get(key))).ToArray();

            List<SQLiteParameter> list = new List<SQLiteParameter>();
            foreach (string key in value.Keys)
            {
                SQLiteParameter parameter = new SQLiteParameter("@" + key, value.Get(key));
                list.Add(parameter);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 添加选择的列名
        /// </summary>
        /// <param name="s"></param>
        /// <param name="columns"></param>
        private void AppendColumns(StringBuilder s, string[] columns)
        {
            int n = columns.Length;

            for (int i = 0; i < n; i++)
            {
                string column = columns[i];

                if (column != null)
                {
                    if (i > 0)
                    {
                        s.Append(", ");
                    }
                    s.Append(column);
                }
            }
            s.Append(' ');
        }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">选择的列</param>
        /// <param name="whereClause">选择条件，格式：列名=@列名</param>
        /// <param name="whereArgs">选择条件的具体值，key格式：@列名</param>
        /// <returns></returns>
        public DataTable Query(string tableName, string[] columns, string whereClause, ContentValue whereArgs)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT DISTINCT ");
            if (columns == null || columns.Length == 0)
            {
                sql.Append("* ");
            }
            else
            {
                AppendColumns(sql, columns);
            }
            sql.AppendFormat("FROM {0} ",tableName);
            sql.AppendFormat("WHERE {0}",whereClause);
            
            return QueryData(sql.ToString(),whereArgs);
        }

        public int Insert(string tableName, ContentValue value)
        {
            int resultCode;

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append(tableName);
            sql.Append('(');

            int size = (value != null && value.Keys.Any()) ? value.Keys.Count : 0;
            if (size > 0)
            {
                int i = 0;
                string[] bindArgs = new string[size];
                foreach (string colName in value.Keys)
                {
                    sql.Append((i > 0) ? "," : "");
                    sql.Append(colName);
                    bindArgs[i] = colName;
                    i++;
                }
                sql.Append(')');
                sql.Append(" VALUES (");
                for (i = 0; i < size; i++)
                {
                    sql.Append((i > 0) ? ",@" + bindArgs[i] : "@" + bindArgs[i]);
                }
                sql.Append(')');
                resultCode = InsertData(sql.ToString(), value);
            }
            else
            {
                resultCode = -1;
            }
            

            //string sql = "INSERT INTO R_User(userName,realName,phone,password,mail) VALUES('xwdoor','肖威','18684033888','xwdoor','xwdoor@126.com')";

            return resultCode;
        }
    }
}