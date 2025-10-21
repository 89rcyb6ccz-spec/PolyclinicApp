using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class DoctorPatientsForm : Form
    {
        private DatabaseHelper db;
        private int doctorId;

        public DoctorPatientsForm(int doctorId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            this.doctorId = doctorId;
            LoadMyPatients();
        }

        private void LoadMyPatients()
        {
            string query = @"SELECT DISTINCT 
                            p.ID, p.LastName, p.FirstName, p.MiddleName, 
                            p.Phone, p.Address, diag.DiseaseName as LastDiagnosis
                            FROM Patient p
                            INNER JOIN Diagnosis diag ON p.ID = diag.IDPatient
                            WHERE diag.IDDoctor = @DoctorID
                            ORDER BY p.LastName";

            SqlParameter[] parameters = { new SqlParameter("@DoctorID", doctorId) };
            DataTable patients = db.ExecuteQuery(query, parameters);
            dataGridViewMyPatients.DataSource = patients;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMyPatients();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Поиск по моим пациентам
        }
    }
}
