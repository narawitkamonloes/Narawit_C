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

namespace PROJECT
{
    public partial class Forgot_your_password : Form
    {
        public Forgot_your_password()
        {
            InitializeComponent();
        }
        private MySqlConnection databaseConnection()
        {
            string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionSting);
            return conn;
        }


        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void Forgot_your_password_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '*')
            {
                pictureBox4.BringToFront();

                textPassword.PasswordChar = '\0';
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '\0')
            {
                pictureBox5.BringToFront();

                textPassword.PasswordChar = '*';
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string password = textPassword.Text;
            string confirm = textConfirm.Text;
            if (password == confirm)
            {
                if (Namedata.Text == "Phone")
                {
                    String EditPhone = textPhonAndEmail.Text;
                    MySqlConnection conn = databaseConnection();
                    String sql = "UPDATE user SET Password ='" + textConfirm.Text + "' WHERE Phone = '" + EditPhone + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("The password reset is complete.");
                        Close();
                        this.Hide();
                        Form1 f = new Form1();
                        f.Show();
                    }
                }
                if (Namedata.Text == "E-mail")
                {
                    String EditEmail = textPhonAndEmail.Text;
                    MySqlConnection conn = databaseConnection();
                    String sql = "UPDATE user SET Password ='" + textConfirm.Text + "' WHERE Email = '" + EditEmail + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("The password reset is complete.");
                        Close();
                        this.Hide();
                        Form1 f = new Form1();
                        f.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match");
                textConfirm.Text = String.Empty;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Namedata.Text = ("Phone");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Namedata.Text = ("E-mail");
        }
    }
}
