using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SqliteHelperDemo.Database
{
    public class LocalDataQuery
    {
        public static Tuple<bool, DataTable> QueryData()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM [LocalDataQuery] , [ChannelStepDataStepNumber] ;");
            //sql.Append("WHERE ");
            //sql.Append("[Id] = @Id;");

            using (SQLiteCommand cmd = new SQLiteCommand(sql.ToString()))
            {
                cmd.Parameters.Add("@Id", System.Data.DbType.Int32).Value = 1;
                DataTable table = LocalDataQuerySource.Instance.GetTable(cmd);

                if (table != null)
                {
                    return new Tuple<bool, DataTable>(true, table);
                }
                return new Tuple<bool, DataTable>(false, null);
            }
        }
    }
}
