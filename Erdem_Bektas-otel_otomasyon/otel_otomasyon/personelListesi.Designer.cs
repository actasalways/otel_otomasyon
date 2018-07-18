namespace otel_otomasyon
{
    partial class personelListesi
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.personelBilgileriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.otel_otomasyon_databaseDataSet1 = new otel_otomasyon.otel_otomasyon_databaseDataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.personelBilgileriTableAdapter = new otel_otomasyon.otel_otomasyon_databaseDataSet1TableAdapters.personelBilgileriTableAdapter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.personelBilgileriBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otel_otomasyon_databaseDataSet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // reportViewer1
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.personelBilgileriBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "otel_otomasyon.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(6, 19);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(761, 424);
            this.reportViewer1.TabIndex = 0;
            // 
            // personelBilgileriTableAdapter
            // 
            this.personelBilgileriTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.reportViewer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1121, 532);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(893, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 106);
            this.button1.TabIndex = 1;
            this.button1.Text = "Filtrele";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // personelListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 556);
            this.Controls.Add(this.groupBox1);
            this.Name = "personelListesi";
            this.Text = "personelListesi";
            this.Load += new System.EventHandler(this.personelListesi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.personelBilgileriBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otel_otomasyon_databaseDataSet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource personelBilgileriBindingSource;
        private otel_otomasyon_databaseDataSet1 otel_otomasyon_databaseDataSet1;
        private otel_otomasyon_databaseDataSet1TableAdapters.personelBilgileriTableAdapter personelBilgileriTableAdapter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}