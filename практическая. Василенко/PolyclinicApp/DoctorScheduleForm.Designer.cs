namespace PolyclinicApp
{
    partial class DoctorScheduleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewMySchedule = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMySchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMySchedule
            // 
            this.dataGridViewMySchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMySchedule.Location = new System.Drawing.Point(12, 246);
            this.dataGridViewMySchedule.Name = "dataGridViewMySchedule";
            this.dataGridViewMySchedule.Size = new System.Drawing.Size(776, 181);
            this.dataGridViewMySchedule.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(381, 44);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(91, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Мое расписание";
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Location = new System.Drawing.Point(132, 145);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(200, 20);
            this.dtpScheduleDate.TabIndex = 2;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.dtpScheduleDate_ValueChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(688, 141);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(29, 151);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Дата";
            // 
            // DoctorScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridViewMySchedule);
            this.Name = "DoctorScheduleForm";
            this.Text = "DoctorScheduleForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMySchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMySchedule;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDate;
    }
}