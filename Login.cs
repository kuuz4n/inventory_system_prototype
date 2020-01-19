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
    public partial class Login : Form
    {
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySqlConnection conn = new MySqlConnection("datasource = 127.0.0.1; port=3306;username=root;password=;database=inventory_system;");

        public Login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            da = new MySqlDataAdapter("SELECT `username`, `password` FROM `login_table` WHERE `username` = '" + username_txt.Text + "' AND `password` = '" + password_txt.Text + "'", conn);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.AddWithValue("@user", username_txt.Text);
            da.SelectCommand.Parameters.AddWithValue("@pass", password_txt.Text);
            da.SelectCommand.Connection = conn;


            try
            {
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                DataTable tb = new DataTable();
                da.Fill(tb);
                if (tb.Rows.Count > 0)
                {
                    this.Hide();
                    Form1 dashboard = new Form1();
                    dashboard.Show();
                }

                else
                {
                    MessageBox.Show("Login Error!");
                    password_txt.Text = "";
                    username_txt.Text = "";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            finally
            {
                conn.Close();
                da.Dispose();
            }
        }

    }
}
