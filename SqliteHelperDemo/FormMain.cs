using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqliteHelperDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            Database.DemoDataQueryParams queryParams  = new Database.DemoDataQueryParams()
            {
                Line = 1,
                Column = 2,
                Plat = 3,
            };

            //if (Database.DemoData.Insert(queryParams))
            //{
            //    Console.WriteLine("Insert OK..");
            //}
            Database.DemoData.QueryData(queryParams);
            label_Status.Text = "正在查询中";

            Task.Factory.StartNew(() =>
            {
                var ret = Database.LocalDataQuery.QueryData();
                if (ret.Item1)
                {
                    this.Invoke(new Action(() =>
                    {
                        dataGridView1.DataSource = ret.Item2;
                        label_Status.Text = "查询完毕";
                    }));
                }
            });


        }
    }
}
