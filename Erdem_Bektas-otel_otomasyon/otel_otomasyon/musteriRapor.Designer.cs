namespace otel_otomasyon
{
    partial class musteriRapor
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.personelBilgileriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.otel_otomasyon_databaseDataSet1 = new otel_otomasyon.otel_otomasyon_databaseDataSet1();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.btnFID = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTC = new System.Windows.Forms.TextBox();
            this.btnFTC = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.personelBilgileriTableAdapter = new otel_otomasyon.otel_otomasyon_databaseDataSet1TableAdapters.personelBilgileriTableAdapter();
            this.musteriBilgileriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new otel_otomasyon.DataSet1();
            this.musteriBilgileriTableAdapter = new otel_otomasyon.DataSet1TableAdapters.musteriBilgileriTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.personelBilgileriBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otel_otomasyon_databaseDataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musteriBilgileriBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // personelBilgileriBindingSource
            // 
            this.personelBilgileriBindingSource.DataMember = "personelBilgileri";
            this.personelBilgileriBindingSource.DataSource = this.otel_otomasyon_databaseDataSet1;
            // 
            // otel_otomasyon_databaseDataSet1
            // 
            this.otel_otomasyon_databaseDataSet1.DataSetName = "otel_otomasyon_databaseDataSet1";
            this.otel_otomasyon_databaseDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 180);
            this.panel1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Controls.Add(this.groupBox1);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1160, 157);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Filtreleme";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbID);
            this.groupBox2.Controls.Add(this.btnFID);
            this.groupBox2.Location = new System.Drawing.Point(581, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 127);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(187, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Müşteri ID :";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(271, 32);
            this.tbID.MaxLength = 11;
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(142, 20);
            this.tbID.TabIndex = 48;
            // 
            // btnFID
            // 
            this.btnFID.Location = new System.Drawing.Point(190, 67);
            this.btnFID.Name = "btnFID";
            this.btnFID.Size = new System.Drawing.Size(223, 37);
            this.btnFID.TabIndex = 13;
            this.btnFID.Text = "Filtrele";
            this.btnFID.UseVisualStyleBackColor = true;
            this.btnFID.Click += new System.EventHandler(this.btnFID_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbTC);
            this.groupBox1.Controls.Add(this.btnFTC);
            this.groupBox1.Location = new System.Drawing.Point(19, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 127);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(187, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Müşteri TC :";
            // 
            // tbTC
            // 
            this.tbTC.Location = new System.Drawing.Point(271, 32);
            this.tbTC.MaxLength = 11;
            this.tbTC.Name = "tbTC";
            this.tbTC.Size = new System.Drawing.Size(142, 20);
            this.tbTC.TabIndex = 48;
            // 
            // btnFTC
            // 
            this.btnFTC.Location = new System.Drawing.Point(190, 67);
            this.btnFTC.Name = "btnFTC";
            this.btnFTC.Size = new System.Drawing.Size(223, 37);
            this.btnFTC.TabIndex = 13;
            this.btnFTC.Text = "Filtrele";
            this.btnFTC.UseVisualStyleBackColor = true;
            this.btnFTC.Click += new System.EventHandler(this.btnFTC_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 180);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 481);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.musteriBilgileriBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "otel_otomasyon.Report3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1184, 481);
            this.reportViewer1.TabIndex = 0;
            // 
            // personelBilgileriTableAdapter
            // 
            this.personelBilgileriTableAdapter.ClearBeforeFill = true;
            // 
            // musteriBilgileriBindingSource
            // 
            this.musteriBilgileriBindingSource.DataMember = "musteriBilgileri";
            this.musteriBilgileriBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // musteriBilgileriTableAdapter
            // 
            this.musteriBilgileriTableAdapter.ClearBeforeFill = true;
            // 
            // musteriRapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "musteriRapor";
            this.Text = "musteriRapor";
            this.Load += new System.EventHandler(this.musteriRapor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.personelBilgileriBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otel_otomasyon_databaseDataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.musteriBilgileriBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Button btnFID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTC;
        private System.Windows.Forms.Button btnFTC;
        private System.Windows.Forms.BindingSource musteriBilgileriBindingSource;
        private DataSet1 DataSet1;
        private DataSet1TableAdapters.musteriBilgileriTableAdapter musteriBilgileriTableAdapter;
        private System.Windows.Forms.BindingSource personelBilgileriBindingSource;
        private otel_otomasyon_databaseDataSet1 otel_otomasyon_databaseDataSet1;
        private otel_otomasyon_databaseDataSet1TableAdapters.personelBilgileriTableAdapter personelBilgileriTableAdapter;
    }
}