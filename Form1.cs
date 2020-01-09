using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace InventoryPrototype
{
    public partial class Form1 : Form
    {
        string MysqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=inventory_system;";

        public Form1()
        {
            InitializeComponent();
        }
      
        private void runQuery()
        {
            MySqlConnection databaseConnection = new MySqlConnection(MysqlConnectionString);

            databaseConnection.Open();
            MySqlDataAdapter sqlquery = new MySqlDataAdapter("SELECT * FROM items", databaseConnection);
            DataTable dt1 = new DataTable();
            sqlquery.Fill(dt1);

            dataGridView1.DataSource = (dt1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            runQuery();
        }
    }
}
