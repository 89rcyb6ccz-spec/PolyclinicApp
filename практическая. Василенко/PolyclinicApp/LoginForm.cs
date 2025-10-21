using System;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (login == "admin" && password == "12345")
            {
                MainForm mainForm = new MainForm(1, "Администратор", "Администратор");
                mainForm.Show();
                this.Hide();
            }
            else if (login == "doctor" && password == "12345")
            {
                MainForm mainForm = new MainForm(2, "Врач Петров", "Врач");
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtLogin.Text = "admin";
            txtPassword.Text = "12345";
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}