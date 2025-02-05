namespace ArcherTools_0._0._1.forms
{
    partial class ViewContainerGUI
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            Line = new DataGridViewTextBoxColumn();
            Config = new DataGridViewTextBoxColumn();
            containerIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            releasesAndItemsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            attachedConfigurationsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            expectedSizeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            containerStatusDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            containerBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)containerBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = SystemColors.AppWorkspace;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Line, Config, containerIdDataGridViewTextBoxColumn, releasesAndItemsDataGridViewTextBoxColumn, attachedConfigurationsDataGridViewTextBoxColumn, expectedSizeDataGridViewTextBoxColumn, containerStatusDataGridViewTextBoxColumn });
            dataGridView1.DataSource = containerBindingSource;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.IndianRed;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.GridColor = Color.Orange;
            dataGridView1.Location = new Point(119, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 192, 128);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(128, 255, 128);
            dataGridViewCellStyle2.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(255, 128, 255);
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = Color.FromArgb(255, 128, 128);
            dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, 128, 255);
            dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Purple;
            dataGridView1.Size = new Size(474, 239);
            dataGridView1.TabIndex = 0;
            // 
            // Line
            // 
            Line.HeaderText = "Line";
            Line.Name = "Line";
            // 
            // Config
            // 
            Config.HeaderText = "Config";
            Config.Name = "Config";
            // 
            // containerIdDataGridViewTextBoxColumn
            // 
            containerIdDataGridViewTextBoxColumn.DataPropertyName = "ContainerId";
            containerIdDataGridViewTextBoxColumn.HeaderText = "ContainerId";
            containerIdDataGridViewTextBoxColumn.Name = "containerIdDataGridViewTextBoxColumn";
            // 
            // releasesAndItemsDataGridViewTextBoxColumn
            // 
            releasesAndItemsDataGridViewTextBoxColumn.DataPropertyName = "ReleasesAndItems";
            releasesAndItemsDataGridViewTextBoxColumn.HeaderText = "ReleasesAndItems";
            releasesAndItemsDataGridViewTextBoxColumn.Name = "releasesAndItemsDataGridViewTextBoxColumn";
            // 
            // attachedConfigurationsDataGridViewTextBoxColumn
            // 
            attachedConfigurationsDataGridViewTextBoxColumn.DataPropertyName = "AttachedConfigurations";
            attachedConfigurationsDataGridViewTextBoxColumn.HeaderText = "AttachedConfigurations";
            attachedConfigurationsDataGridViewTextBoxColumn.Name = "attachedConfigurationsDataGridViewTextBoxColumn";
            // 
            // expectedSizeDataGridViewTextBoxColumn
            // 
            expectedSizeDataGridViewTextBoxColumn.DataPropertyName = "ExpectedSize";
            expectedSizeDataGridViewTextBoxColumn.HeaderText = "ExpectedSize";
            expectedSizeDataGridViewTextBoxColumn.Name = "expectedSizeDataGridViewTextBoxColumn";
            // 
            // containerStatusDataGridViewTextBoxColumn
            // 
            containerStatusDataGridViewTextBoxColumn.DataPropertyName = "ContainerStatus";
            containerStatusDataGridViewTextBoxColumn.HeaderText = "ContainerStatus";
            containerStatusDataGridViewTextBoxColumn.Name = "containerStatusDataGridViewTextBoxColumn";
            // 
            // containerBindingSource
            // 
            containerBindingSource.DataSource = typeof(classes.Container);
            // 
            // ViewContainerGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Name = "ViewContainerGUI";
            Text = "ViewContainerGUI";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)containerBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Line;
        private DataGridViewTextBoxColumn Config;
        private BindingSource containerBindingSource;
        private DataGridViewTextBoxColumn containerIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn releasesAndItemsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn attachedConfigurationsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expectedSizeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn containerStatusDataGridViewTextBoxColumn;
    }
}