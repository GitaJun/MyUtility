using System;
using System.Data.SqlClient;

namespace WorkChargerMigration
{
    /// <summary>
    /// SqlServer数据库处理器
    /// </summary>
    public class SqlServerDBHandler
    {
        private readonly string server, uid, pwd, database;
        /// <summary>
        /// 创建SqlServer数据库处理器
        /// </summary>
        /// <param name="server">服务器名</param>
        /// <param name="uid">账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库名</param>
        public SqlServerDBHandler(string server, string uid, string pwd, string database)
        {
            this.server = server;
            this.uid = uid;
            this.pwd = pwd;
            this.database = database;
        }

        /// <summary>
        /// 从数据库中获取数据，若执行的SQL语句结果无数据，则抛出异常
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="read">委托：利用闭包，操作通过执行数据库语句创造的SqlDataReader对象，进行数据读取</param>
        /// <returns>数据载体</returns>
        public void GetData(string sql, Action<SqlDataReader> read)
        {
            using (SqlConnection connection = new SqlConnection($"server={server};uid={uid};pwd={pwd};database={database}"))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) throw new Exception("数据库中无数据");
                        read(reader);
                    }
                }
            }
        }

        /// <summary>
        /// 更新数据库数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>更新行数</returns>
        public int UpdateData(string sql)
        {
            using (SqlConnection connection = new SqlConnection($"server={server};uid={uid};pwd={pwd};database={database}"))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    return command.ExecuteNonQuery();
                }
            }
        }

    }
}
