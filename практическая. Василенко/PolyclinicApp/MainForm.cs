using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PolyclinicApp
{
    public partial class MainForm : Form
    {
        private int userId;
        private string userName;
        private string userRole;

        public MainForm(int userId, string userName, string userRole)
        {
            InitializeComponent();

            this.userId = userId;
            this.userName = userName;
            this.userRole = userRole;

            MessageBox.Show($"Добро пожаловать, {userName} ({userRole})!", "Вход выполнен");

            // Показываем кто вошел
            this.Text = $"Поликлиника - {userName} ({userRole})";

            // Настройка прав для врача
            if (userRole == "Врач")
            {
                btnDoctors.Visible = false;    // Врач не управляет врачами
                btnSearch.Visible = false;     // Врач не видит общие отчеты
                btnPatients.Text = "Мои пациенты";
                btnAppointments.Text = "Мое расписание";
                btnPatients.Click += btnPatients_Click;
                btnDoctors.Click += btnDoctors_Click;
                btnAppointments.Click += btnAppointments_Click;
                btnSearch.Click += btnSearch_Click;  // ЭТА СТРОКА ВАЖНА!
                btnExit.Click += btnExit_Click;
                btnAppointments.Click += (s, e) => { new AppointmentsForm().Show(); };
            }

        }
    
    

        private void btnPatients_Click(object sender, EventArgs e)
        {
            // ПРОСТО ОТКРЫВАЕМ ФОРМУ ПАЦИЕНТОВ
            PatientsForm patientsForm = new PatientsForm();
            patientsForm.Show();
        }

        private void BtnDoctors_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Врачи");
        }

        private void BtnAppointments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Записи");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnDoctors_Click(object sender, EventArgs e)
        {
            DoctorsForm doctorsForm = new DoctorsForm();
            doctorsForm.ShowDialog();
        }
        private void btnAppointments_Click(object sender, EventArgs e)
        {
            AppointmentsForm appointmentsForm = new AppointmentsForm();
            appointmentsForm.Show();  // или appointmentsForm.ShowDialog();
            
        }

        private void btnAppointments_Click_1(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAppointments_Click_2(object sender, EventArgs e)
        {

        }
    }
}
