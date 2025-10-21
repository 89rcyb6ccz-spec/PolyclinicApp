using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class AddDoctorForm : Form
    {
        private DatabaseHelper db;

        public AddDoctorForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            // Специализации
            string specQuery = "SELECT ID, Name FROM Specialization";
            DataTable specializations = db.ExecuteQuery(specQuery);
            cmbSpecialization.DataSource = specializations;
            cmbSpecialization.DisplayMember = "Name";
            cmbSpecialization.ValueMember = "ID";

            // Квалификации
            string qualQuery = "SELECT ID, Name FROM Qualification";
            DataTable qualifications = db.ExecuteQuery(qualQuery);
            cmbQualification.DataSource = qualifications;
            cmbQualification.DisplayMember = "Name";
            cmbQualification.ValueMember = "ID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Заполните фамилию и имя!");
                return;
            }

            try
            {
                string query = @"INSERT INTO Doctor (LastName, FirstName, MiddleName, IDSpecialization, IDQualification)
                                VALUES (@LastName, @FirstName, @MiddleName, @Specialization, @Qualification)";

                SqlParameter[] parameters = {
                    new SqlParameter("@LastName", txtLastName.Text),
                    new SqlParameter("@FirstName", txtFirstName.Text),
                    new SqlParameter("@MiddleName", txtMiddleName.Text),
                    new SqlParameter("@Specialization", cmbSpecialization.SelectedValue),
                    new SqlParameter("@Qualification", cmbQualification.SelectedValue)
                };

                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MessageBox.Show("Врач добавлен!");
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