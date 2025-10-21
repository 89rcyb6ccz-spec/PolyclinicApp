using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class PatientsForm : Form
    {
        private DatabaseHelper db;

        public PatientsForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();

            // ВРУЧНУЮ подключаем обработчики
            btnAdd.Click += btnAdd_Click;
            
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            LoadPatients();
        }

        private void LoadPatients()
        {
            string query = @"SELECT p.ID, p.LastName, p.FirstName, p.MiddleName, 
                            p.BirthDate, p.Phone, p.Address, ps.Name as Status
                            FROM Patient p
                            INNER JOIN PatientStatus ps ON p.IDPatientStatus = ps.ID
                            ORDER BY p.LastName";

            DataTable patients = db.ExecuteQuery(query);
            dataGridViewPatients.DataSource = patients;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadPatients(); // Если поле пустое - показываем всех
                return;
            }

            try
            {
                string query = @"SELECT p.ID, p.LastName, p.FirstName, p.MiddleName, 
                        p.BirthDate, p.Phone, p.Address, ps.Name as Status
                        FROM Patient p
                        INNER JOIN PatientStatus ps ON p.IDPatientStatus = ps.ID
                        WHERE p.LastName LIKE @Search OR 
                              p.FirstName LIKE @Search OR 
                              p.Phone LIKE @Search
                        ORDER BY p.LastName";

                SqlParameter[] parameters = { new SqlParameter("@Search", $"%{searchText}%") };
                DataTable patients = db.ExecuteQuery(query, parameters);
                dataGridViewPatients.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка поиска: " + ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPatientForm addForm = new AddPatientForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            

            if (dataGridViewPatients.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewPatients.SelectedRows[0];
                int patientId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                MessageBox.Show("Выбран пациент ID: " + patientId); // ОТЛАДКА

                EditPatientForm editForm = new EditPatientForm(patientId);
                editForm.ShowDialog();

                LoadPatients(); // Всегда обновляем таблицу
            }
            else
            {
                MessageBox.Show("Выберите пациента!");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            

            if (dataGridViewPatients.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewPatients.SelectedRows[0];
                int patientId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                string patientName = selectedRow.Cells["LastName"].Value + " " +
                                   selectedRow.Cells["FirstName"].Value;

                DialogResult result = MessageBox.Show(
                    $"Удалить пациента: {patientName}?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM Patient WHERE ID = @ID";
                    SqlParameter[] parameters = { new SqlParameter("@ID", patientId) };

                    int rowsAffected = db.ExecuteNonQuery(query, parameters);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пациент удален!");
                        LoadPatients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }
        private void dataGridViewPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пустой метод
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

        }
    }
}