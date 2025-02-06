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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewContainerGUI));
            containerBindingSource = new BindingSource(components);
            panel1 = new Panel();
            title_Label = new Label();
            panel2 = new Panel();
            lineCfg_DtGridView = new DataGridView();
            lineColumn = new DataGridViewTextBoxColumn();
            configColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)containerBindingSource).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lineCfg_DtGridView).BeginInit();
            SuspendLayout();
            // 
            // containerBindingSource
            // 
            containerBindingSource.DataSource = typeof(classes.Container);
            // 
            // panel1
            // 
            panel1.Controls.Add(title_Label);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(263, 54);
            panel1.TabIndex = 1;
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.FlatStyle = FlatStyle.Flat;
            title_Label.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(263, 42);
            title_Label.TabIndex = 2;
            title_Label.Text = "Viewing Configurations for:";
            title_Label.TextAlign = ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.Controls.Add(lineCfg_DtGridView);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(263, 396);
            panel2.TabIndex = 2;
            // 
            // lineCfg_DtGridView
            // 
            lineCfg_DtGridView.AllowUserToDeleteRows = false;
            lineCfg_DtGridView.AllowUserToResizeColumns = false;
            lineCfg_DtGridView.AllowUserToResizeRows = false;
            lineCfg_DtGridView.BackgroundColor = Color.FromArgb(64, 64, 64);
            lineCfg_DtGridView.BorderStyle = BorderStyle.Fixed3D;
            lineCfg_DtGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            lineCfg_DtGridView.Columns.AddRange(new DataGridViewColumn[] { lineColumn, configColumn });
            lineCfg_DtGridView.Dock = DockStyle.Fill;
            lineCfg_DtGridView.GridColor = Color.WhiteSmoke;
            lineCfg_DtGridView.Location = new Point(0, 0);
            lineCfg_DtGridView.Name = "lineCfg_DtGridView";
            lineCfg_DtGridView.RowHeadersVisible = false;
            lineCfg_DtGridView.RowHeadersWidth = 30;
            lineCfg_DtGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            lineCfg_DtGridView.Size = new Size(263, 396);
            lineCfg_DtGridView.TabIndex = 0;
            // 
            // lineColumn
            // 
            lineColumn.DataPropertyName = "Lines";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle1.ForeColor = Color.LightYellow;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "-999";
            dataGridViewCellStyle1.SelectionBackColor = Color.Brown;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Info;
            lineColumn.DefaultCellStyle = dataGridViewCellStyle1;
            lineColumn.HeaderText = "Lines";
            lineColumn.Name = "lineColumn";
            lineColumn.Resizable = DataGridViewTriState.False;
            lineColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            lineColumn.Width = 40;
            // 
            // configColumn
            // 
            configColumn.DataPropertyName = "Configs";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.ForeColor = Color.LightYellow;
            dataGridViewCellStyle2.Format = "00/0/0/0/0/0";
            dataGridViewCellStyle2.NullValue = "10/0/0/0/0/0";
            dataGridViewCellStyle2.SelectionBackColor = Color.Brown;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.Info;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            configColumn.DefaultCellStyle = dataGridViewCellStyle2;
            configColumn.HeaderText = "Configs";
            configColumn.Name = "configColumn";
            configColumn.Resizable = DataGridViewTriState.False;
            configColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            configColumn.Width = 175;
            // 
            // ViewContainerGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(263, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ViewContainerGUI";
            Text = "ViewContainerGUI";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)containerBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lineCfg_DtGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource containerBindingSource;
        private Panel panel1;
        private Panel panel2;
        private Label title_Label;
        private DataGridView lineCfg_DtGridView;
        private DataGridViewTextBoxColumn lineColumn;
        private DataGridViewTextBoxColumn configColumn;
    }
}