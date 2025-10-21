using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class AddPatientForm : Form
    {
        private DatabaseHelper db;

        public AddPatientForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            try
            {
                // Загрузка социальных статусов
                string socialQuery = "SELECT ID, Name FROM SocialStatus";
                DataTable socialStatuses = db.ExecuteQuery(socialQuery);
                cmbSocialStatus.DataSource = socialStatuses;
                cmbSocialStatus.DisplayMember = "Name";
                cmbSocialStatus.ValueMember = "ID";

                // Загрузка статусов пациентов
                string statusQuery = "SELECT ID, Name FROM PatientStatus";
                DataTable patientStatuses = db.ExecuteQuery(statusQuery);
                cmbPatientStatus.DataSource = patientStatuses;
                cmbPatientStatus.DisplayMember = "Name";
                cmbPatientStatus.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка заполнения обязательных полей
            if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Заполните фамилию и имя!", "Ошибка");
                return;
            }

            try
            {
                string query = @"INSERT INTO Patient (LastName, FirstName, MiddleName, BirthDate, 
                                Passport, Phone, Address, IDSocialStatus, IDPatientStatus)
                                VALUES (@LastName, @FirstName, @MiddleName, @BirthDate, 
                                @Passport, @Phone, @Address, @SocialStatus, @PatientStatus)";

                SqlParameter[] parameters = {
                    new SqlParameter("@LastName", txtLastName.Text),
                    new SqlParameter("@FirstName", txtFirstName.Text),
                    new SqlParameter("@MiddleName", txtMiddleName.Text),
                    new SqlParameter("@BirthDate", DateTime.Now.Date), // Используем текущую дату
                    new SqlParameter("@Passport", txtPassport.Text),
                    new SqlParameter("@Phone", txtPhone.Text),
                    new SqlParameter("@Address", txtAddress.Text),
                    new SqlParameter("@SocialStatus", cmbSocialStatus.SelectedValue),
                    new SqlParameter("@PatientStatus", cmbPatientStatus.SelectedValue)
                };

                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MessageBox.Show("Пациент успешно добавлен!", "Успех");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить пациента", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пациента: " + ex.Message, "Ошибка");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }
    }
}