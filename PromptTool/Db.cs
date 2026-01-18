using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptTool
{
    public static class Db
    {
        public static SqlSugarClient GetDb()
        {
            return new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = "Data Source=data.db",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            },
            db =>
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);
                };
            });
        }
    }
}
