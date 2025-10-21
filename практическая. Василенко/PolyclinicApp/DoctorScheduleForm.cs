using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class DoctorScheduleForm : Form
    {
        private DatabaseHelper db;
        private int doctorId;

        public DoctorScheduleForm(int doctorId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            this.doctorId = doctorId;
            LoadMySchedule();
        }

        private void LoadMySchedule()
        {
            string query = @"SELECT 
                            a.ID, p.LastName + ' ' + p.FirstName as PatientName,
                            a.AppointmentDate, a.Status
                            FROM Appointment a
                            INNER JOIN Patient p ON a.IDPatient = p.ID
                            WHERE a.IDDoctor = @DoctorID 
                            AND CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
                            ORDER BY a.AppointmentDate";

            SqlParameter[] parameters = { new SqlParameter("@DoctorID", doctorId) };
            DataTable schedule = db.ExecuteQuery(query, parameters);
            dataGridViewMySchedule.DataSource = schedule;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMySchedule();
        }

        private void dtpScheduleDate_ValueChanged(object sender, EventArgs e)
        {
            // Фильтр по дате
        }
    }
}
