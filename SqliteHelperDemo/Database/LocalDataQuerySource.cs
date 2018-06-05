using SqliteHelper.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqliteHelperDemo.Database
{
    public class LocalDataQuerySource : DataSource<SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteDataAdapter>
    {
        #region 单例空间...

        private static readonly string m_defaultFilepath = Path.Combine(Application.StartupPath, @"localDataQuery.db");

        private static LocalDataQuerySource m_instance;

        public static LocalDataQuerySource Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new LocalDataQuerySource(m_defaultFilepath);
                    m_instance.Init();
                }
                return m_instance;
            }
        }

        #endregion

        private LocalDataQuerySource(string filepath)
        {
            this.m_connStr = String.Format(@"Data Source ={0};Version = 3;", filepath);
        }

        protected override void Init()
        {
            //string sql = "CREATE TABLE IF NOT EXISTS [demo_data] (" +
            //             "[line_number] INTEGER," +
            //             "[column_number] INTEGER," +
            //             "[plat_number] INTEGER," +
            //             "[update_time] DATE NOT NULL );";
            //SQLiteCommand cmd = new SQLiteCommand(sql);
            //Execute(cmd);
        }

        protected override void ExceptionProcess(Exception ex)
        {
            Console.Write(string.Format("数据库操作命令出现异常{0}", ex));
        }
    }
}
