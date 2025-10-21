using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class EditDoctorForm : Form
    {
        private DatabaseHelper db;
        private int doctorId;

        public EditDoctorForm(int doctorId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            this.doctorId = doctorId;
            LoadDoctorData();
        }

        private void LoadDoctorData()
        {
            try
            {
                string query = "SELECT * FROM Doctor WHERE ID = @ID";
                SqlParameter[] parameters = { new SqlParameter("@ID", doctorId) };
                DataTable doctorData = db.ExecuteQuery(query, parameters);

                if (doctorData.Rows.Count > 0)
                {
                    DataRow row = doctorData.Rows[0];

                    txtLastName.Text = row["LastName"].ToString();
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtMiddleName.Text = row["MiddleName"]?.ToString() ?? "";

                    LoadComboBoxes(
                        Convert.ToInt32(row["IDSpecialization"]),
                        Convert.ToInt32(row["IDQualification"])
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void LoadComboBoxes(int specializationId, int qualificationId)
        {
            string specQuery = "SELECT ID, Name FROM Specialization";
            DataTable specializations = db.ExecuteQuery(specQuery);
            cmbSpecialization.DataSource = specializations;
            cmbSpecialization.DisplayMember = "Name";
            cmbSpecialization.ValueMember = "ID";
            cmbSpecialization.SelectedValue = specializationId;

            string qualQuery = "SELECT ID, Name FROM Qualification";
            DataTable qualifications = db.ExecuteQuery(qualQuery);
            cmbQualification.DataSource = qualifications;
            cmbQualification.DisplayMember = "Name";
            cmbQualification.ValueMember = "ID";
            cmbQualification.SelectedValue = qualificationId;
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
                string query = @"UPDATE Doctor SET 
                                LastName = @LastName, 
                                FirstName = @FirstName, 
                                MiddleName = @MiddleName, 
                                IDSpecialization = @Specialization, 
                                IDQualification = @Qualification
                                WHERE ID = @ID";

                SqlParameter[] parameters = {
                    new SqlParameter("@LastName", txtLastName.Text),
                    new SqlParameter("@FirstName", txtFirstName.Text),
                    new SqlParameter("@MiddleName", txtMiddleName.Text),
                    new SqlParameter("@Specialization", cmbSpecialization.SelectedValue),
                    new SqlParameter("@Qualification", cmbQualification.SelectedValue),
                    new SqlParameter("@ID", doctorId)
                };

                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MessageBox.Show("Данные врача обновлены!");
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
            this.Close();
        }
    }
} 

