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
            txt_item_code.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txt_item_name.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_price.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txt_stocks.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "UPDATE items SET item_name = '"+txt_item_name.Text+"', stocks = '"+txt_stocks.Text+"', item_code = '"+txt_item_code.Text+"',price = '"+txt_price.Text+"'";
            MySqlDataAdapter SDA = new MySqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Update Successfully!");
            txt_item_code.Text = "";
            txt_item_name.Text = "";
            txt_price.Text = "";
            txt_stocks.Text = "";
            runQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "DELETE FROM items where item_code = '" + txt_item_code.Text + "'";
            MySqlDataAdapter SDA = new MySqlDataAdapter(query, con);
            SDA.SelectCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Removed!!");
            runQuery();
        }

        public void searchData()
        {
            string query = "SELECT * FROM items WHERE CONCAT(`item_name`,`stocks`,`item_code`,`price`) like '%"+ textBox1.Text.ToString()+"%'";
            MySqlDataAdapter SDA = new MySqlDataAdapter(query, con);
            DataTable table = new DataTable();
            SDA.Fill(table);
            dataGridView1.DataSource = table;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchData();
        }
    }
}
