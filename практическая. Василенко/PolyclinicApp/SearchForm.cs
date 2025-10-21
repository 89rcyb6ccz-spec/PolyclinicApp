using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class SearchForm : Form
    {
        private DatabaseHelper db;

        public SearchForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT 
                                p.LastName + ' ' + p.FirstName as Пациент,
                                d.LastName + ' ' + d.FirstName as Врач,
                                diag.DiseaseName as Диагноз
                                FROM Patient p
                                INNER JOIN Diagnosis diag ON p.ID = diag.IDPatient
                                INNER JOIN Doctor d ON diag.IDDoctor = d.ID
                                WHERE diag.DiseaseName LIKE @Diagnosis";

                SqlParameter[] parameters = {
                    new SqlParameter("@Diagnosis", $"%{txtDiagnosis.Text}%")
                };

                DataTable results = db.ExecuteQuery(query, parameters);
                dataGridViewResults.DataSource = results;
                lblResultCount.Text = $"Найдено: {results.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDiagnosis.Clear();
            dataGridViewResults.DataSource = null;
            lblResultCount.Text = "Найдено: 0";
        }

        private void txtDiagnosis_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblResultCount_Click(object sender, EventArgs e)
        {

        }
    }
}