using System;
using System.Data;
using System.Data.Common;


namespace SqliteHelper.Database
{
    public abstract class DataSource<Connection, Command, Transaction, DataAdapter>
        where Connection : DbConnection, new()
        where Command : DbCommand
        where Transaction : DbTransaction
        where DataAdapter : DbDataAdapter, new()
    {
        private const int INVALID_VALUE = -1;

        protected string m_connStr;

        private Connection m_conTransaction;

        /*private DataSource(string connectionString)
        {
            this.m_connStr = connectionString;
        }*/

        protected abstract void Init();

        protected abstract void ExceptionProcess(Exception ex);

        public Transaction CreateTransaction()
        {
            this.m_conTransaction = new Connection();
            this.m_conTransaction.ConnectionString = this.m_connStr;
            this.m_conTransaction.Open();
            return (Transaction)this.m_conTransaction.BeginTransaction();
        }

        public bool TransactionCommit(Transaction tran)
        {
            try
            {
                tran.Commit();
                this.m_conTransaction.Close();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ExceptionProcess(ex);
                return false;
            }
        }

        public int Execute(Command cmd)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    conn.ConnectionString = this.m_connStr;
                    conn.Open();
                    cmd.Connection = conn;
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ExceptionProcess(ex);
                    return INVALID_VALUE;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int Execute(Command cmd, Transaction tran)
        {
            try
            {
                cmd.Connection = this.m_conTransaction;
                cmd.Transaction = tran;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
                return INVALID_VALUE;
            }
        }

        public T GetValue<T>(Command cmd)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    conn.ConnectionString = this.m_connStr;
                    conn.Open();
                    cmd.Connection = conn;
                    object result = cmd.ExecuteScalar();
                    return (T)Convert.ChangeType(result, typeof(T));
                }
                catch (Exception ex)
                {
                    ExceptionProcess(ex);
                    return default(T);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public T GetValue<T>(Command cmd, Transaction tran)
        {
            try
            {
                cmd.Connection = this.m_conTransaction;
                cmd.Transaction = tran;
                object result = cmd.ExecuteScalar();
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
                return default(T);
            }
        }

        public DataTable GetTable(Command cmd)
        {
            using (Connection conn = new Connection())
            {
                try
                {
                    conn.ConnectionString = this.m_connStr;
                    conn.Open();
                    cmd.Connection = conn;

                    DataAdapter adapter = new DataAdapter();
                    adapter.SelectCommand = cmd;

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
                catch (Exception ex)
                {
                    ExceptionProcess(ex);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
