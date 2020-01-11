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
        MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=inventory_system;");
        public Form1()
        {
            InitializeComponent();
        }
      
        private void runQuery()
        {

            con.Open();
            MySqlDataAdapter sqlquery = new MySqlDataAdapter("SELECT * FROM items", con);
            DataTable dt1 = new DataTable();
            sqlquery.Fill(dt1);

            dataGridView1.DataSource = (dt1);
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            runQuery();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.CommandText = "INSERT INTO items (item_name, stocks ,item_code , price) VALUES (@item_name, @stocks, @item_code , @price);";
            cmd.Parameters.AddWithValue("@item_name", txt_item_name.Text);
            cmd.Parameters.AddWithValue("@stocks", txt_stocks.Text);
            cmd.Parameters.AddWithValue("@item_code", txt_item_code.Text);
            cmd.Parameters.AddWithValue("@price", txt_price.Text);
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Added!");
                con.Close();

                txt_item_code.Text = "";
                txt_item_name.Text = "";
                txt_item_code.Text = "";
                txt_price.Text = "";
                txt_stocks.Text = "";
                textBox2.Text = ""; 
                con.Close();
                runQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
                con.Close();
            }
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            txt_item_code.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_item_name.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txt_price.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txt_stocks.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
