using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class DoctorsForm : Form
    {
        private DatabaseHelper db;

        public DoctorsForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            string query = @"SELECT d.ID, d.LastName, d.FirstName, d.MiddleName, 
                            s.Name as Specialization, q.Name as Qualification
                            FROM Doctor d
                            INNER JOIN Specialization s ON d.IDSpecialization = s.ID
                            INNER JOIN Qualification q ON d.IDQualification = q.ID
                            ORDER BY d.LastName";

            DataTable doctors = db.ExecuteQuery(query);
            dataGridViewDoctors.DataSource = doctors;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDoctorForm addForm = new AddDoctorForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadDoctors();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewDoctors.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewDoctors.SelectedRows[0];
                int doctorId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                EditDoctorForm editForm = new EditDoctorForm(doctorId);
                editForm.ShowDialog();
                LoadDoctors();
            }
            else
            {
                MessageBox.Show("Выберите врача для редактирования");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDoctors.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewDoctors.SelectedRows[0];
                int doctorId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                string doctorName = selectedRow.Cells["LastName"].Value + " " +
                                  selectedRow.Cells["FirstName"].Value;

                DialogResult result = MessageBox.Show(
                    $"Удалить врача: {doctorName}?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM Doctor WHERE ID = @ID";
                    SqlParameter[] parameters = { new SqlParameter("@ID", doctorId) };

                    int rowsAffected = db.ExecuteNonQuery(query, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Врач удален!");
                        LoadDoctors();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите врача для удаления");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDoctors();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

        }
    }
}
