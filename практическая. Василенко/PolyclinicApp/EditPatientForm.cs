using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class EditPatientForm : Form
    {
        private DatabaseHelper db;
        private int patientId;

        public EditPatientForm(int patientId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            this.patientId = patientId;

            // ВРУЧНУЮ подключаем обработчики
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            LoadPatientData();
        }

        private void LoadPatientData()
        {
            try
            {
                MessageBox.Show("Загружаем данные пациента ID: " + patientId); // ОТЛАДКА

                string query = "SELECT * FROM Patient WHERE ID = @ID";
                SqlParameter[] parameters = { new SqlParameter("@ID", patientId) };
                DataTable patientData = db.ExecuteQuery(query, parameters);

                if (patientData.Rows.Count > 0)
                {
                    DataRow row = patientData.Rows[0];

                    // Заполняем поля
                    txtLastName.Text = row["LastName"].ToString();
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtMiddleName.Text = row["MiddleName"]?.ToString() ?? "";
                    txtPassport.Text = row["Passport"]?.ToString() ?? "";
                    txtPhone.Text = row["Phone"]?.ToString() ?? "";
                    txtAddress.Text = row["Address"]?.ToString() ?? "";

                    // Загружаем комбобоксы
                    LoadComboBoxes(
                        Convert.ToInt32(row["IDSocialStatus"]),
                        Convert.ToInt32(row["IDPatientStatus"])
                    );

                    MessageBox.Show("Данные загружены!"); // ОТЛАДКА
                }
                else
                {
                    MessageBox.Show("Пациент не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void LoadComboBoxes(int socialStatusId, int patientStatusId)
        {
            // Социальные статусы
            string socialQuery = "SELECT ID, Name FROM SocialStatus";
            DataTable socialStatuses = db.ExecuteQuery(socialQuery);
            cmbSocialStatus.DataSource = socialStatuses;
            cmbSocialStatus.DisplayMember = "Name";
            cmbSocialStatus.ValueMember = "ID";
            cmbSocialStatus.SelectedValue = socialStatusId;

            // Статусы пациентов
            string statusQuery = "SELECT ID, Name FROM PatientStatus";
            DataTable patientStatuses = db.ExecuteQuery(statusQuery);
            cmbPatientStatus.DataSource = patientStatuses;
            cmbPatientStatus.DisplayMember = "Name";
            cmbPatientStatus.ValueMember = "ID";
            cmbPatientStatus.SelectedValue = patientStatusId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка Сохранить нажата!"); // ОТЛАДКА

            if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Заполните фамилию и имя!");
                return;
            }

            try
            {
                string query = @"UPDATE Patient SET 
                                LastName = @LastName, 
                                FirstName = @FirstName, 
                                MiddleName = @MiddleName, 
                                Passport = @Passport, 
                                Phone = @Phone, 
                                Address = @Address, 
                                IDSocialStatus = @SocialStatus, 
                                IDPatientStatus = @PatientStatus
                                WHERE ID = @ID";

                SqlParameter[] parameters = {
                    new SqlParameter("@LastName", txtLastName.Text),
                    new SqlParameter("@FirstName", txtFirstName.Text),
                    new SqlParameter("@MiddleName", txtMiddleName.Text),
                    new SqlParameter("@Passport", txtPassport.Text),
                    new SqlParameter("@Phone", txtPhone.Text),
                    new SqlParameter("@Address", txtAddress.Text),
                    new SqlParameter("@SocialStatus", cmbSocialStatus.SelectedValue),
                    new SqlParameter("@PatientStatus", cmbPatientStatus.SelectedValue),
                    new SqlParameter("@ID", patientId)
                };

                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MessageBox.Show("Данные обновлены!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
