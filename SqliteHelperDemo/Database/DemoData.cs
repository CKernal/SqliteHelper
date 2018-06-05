using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SqliteHelperDemo.Database
{
    public class DemoData
    {
        public static bool Insert(DemoDataQueryParams queryParams)
        {
            string sql = "REPLACE INTO [demo_data] ([line_number], [column_number], [plat_number], [update_time]) " +
            "VALUES(@Line, @Column, @Plat, @UpdateTime);";
            //"VALUES(1, 4, 1, '2018-06-03 15:02:00');";

            using (SQLiteCommand cmd = new SQLiteCommand(sql))
            {
                cmd.Parameters.Add("@Line", System.Data.DbType.Int32).Value = queryParams.Line;
                cmd.Parameters.Add("@Column", System.Data.DbType.Int32).Value = queryParams.Column;
                cmd.Parameters.Add("@Plat", System.Data.DbType.Int32).Value = queryParams.Plat;
                cmd.Parameters.Add("@UpdateTime", System.Data.DbType.DateTime).Value = DateTime.Now;

                return DemoDataSource.Instance.Execute(cmd) > 0;
            }
        }

        public static Tuple<bool, DataTable> QueryData(DemoDataQueryParams queryParams)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM [demo_data] ");
            sql.Append("WHERE ");
            sql.Append("[line_number] = @Line;");

            using (SQLiteCommand cmd = new SQLiteCommand(sql.ToString()))
            {

                cmd.Parameters.Add("@Line", System.Data.DbType.Int32).Value = queryParams.Line;


                DataTable table = DemoDataSource.Instance.GetTable(cmd);

                if (table != null)
                {
                    return new Tuple<bool, DataTable>(true, table);
                }
                return new Tuple<bool, DataTable>(false, null);
            }
        }
    }
}
