using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PolyclinicApp
{
    public partial class AppointmentsForm : Form
    {
        private DatabaseHelper db;

        public AppointmentsForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();

            // ВРУЧНУЮ подключаем обработчики
            btnAdd.Click += btnAdd_Click;
            btnCancel.Click += btnCancel_Click;
            btnRefresh.Click += btnRefresh_Click;

            LoadComboBoxes();
            LoadAppointments();
        }

        private void LoadComboBoxes()
        {
            // Пациенты
            string patientsQuery = "SELECT ID, LastName + ' ' + FirstName as FullName FROM Patient";
            DataTable patients = db.ExecuteQuery(patientsQuery);
            cmbPatients.DataSource = patients;
            cmbPatients.DisplayMember = "FullName";
            cmbPatients.ValueMember = "ID";

            // Врачи
            string doctorsQuery = "SELECT ID, LastName + ' ' + FirstName as FullName FROM Doctor";
            DataTable doctors = db.ExecuteQuery(doctorsQuery);
            cmbDoctors.DataSource = doctors;
            cmbDoctors.DisplayMember = "FullName";
            cmbDoctors.ValueMember = "ID";
        }

        private void LoadAppointments()
        {
            string query = @"SELECT a.ID, 
                            p.LastName + ' ' + p.FirstName as PatientName,
                            d.LastName + ' ' + d.FirstName as DoctorName, 
                            a.AppointmentDate, a.Status
                            FROM Appointment a
                            INNER JOIN Patient p ON a.IDPatient = p.ID
                            INNER JOIN Doctor d ON a.IDDoctor = d.ID
                            ORDER BY a.AppointmentDate DESC";

            DataTable appointments = db.ExecuteQuery(query);
            dataGridViewAppointments.DataSource = appointments;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null)
            {
                MessageBox.Show("Выберите пациента и врача!");
                return;
            }

            try
            {
                string query = @"INSERT INTO Appointment (IDPatient, IDDoctor, AppointmentDate, Status)
                                VALUES (@PatientID, @DoctorID, @AppointmentDate, 'Запланирован')";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", cmbPatients.SelectedValue),
                    new SqlParameter("@DoctorID", cmbDoctors.SelectedValue),
                    new SqlParameter("@AppointmentDate", dtpAppointmentDate.Value)
                };

                int result = db.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MessageBox.Show("Запись на прием создана!");
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dataGridViewAppointments.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewAppointments.SelectedRows[0];
                int appointmentId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                string query = "UPDATE Appointment SET Status = 'Отменен' WHERE ID = @ID";
                SqlParameter[] parameters = { new SqlParameter("@ID", appointmentId) };

                db.ExecuteNonQuery(query, parameters);
                LoadAppointments();
            }
            else
            {
                MessageBox.Show("Выберите запись для отмены");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpAppointmentDate_ValueChanged(object sender, EventArgs e)
        {

        }

 
    }
}
