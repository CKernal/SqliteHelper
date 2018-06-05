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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
        }
    }
}
