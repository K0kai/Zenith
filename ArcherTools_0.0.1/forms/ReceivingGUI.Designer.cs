namespace ArcherTools_0._0._1.forms
{
    partial class ReceivingGUI
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
            close_Btn = new Button();
            startscript_Btn = new Button();
            panel1 = new Panel();
            status_Label = new Label();
            cleanExcel_Btn = new Button();
            itemToExcel_Btn = new Button();
            title_Label = new Label();
            description_Label = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // close_Btn
            // 
            close_Btn.BackColor = Color.Indigo;
            close_Btn.BackgroundImageLayout = ImageLayout.None;
            close_Btn.DialogResult = DialogResult.Cancel;
            close_Btn.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            close_Btn.FlatAppearance.BorderSize = 0;
            close_Btn.FlatStyle = FlatStyle.Flat;
            close_Btn.ForeColor = Color.Lavender;
            close_Btn.Location = new Point(406, 0);
            close_Btn.Name = "close_Btn";
            close_Btn.Size = new Size(22, 23);
            close_Btn.TabIndex = 4;
            close_Btn.Text = "X";
            close_Btn.UseVisualStyleBackColor = false;
            close_Btn.Click += close_Btn_Click;
            // 
            // startscript_Btn
            // 
            startscript_Btn.BackColor = SystemColors.ControlDarkDark;
            startscript_Btn.BackgroundImageLayout = ImageLayout.None;
            startscript_Btn.Cursor = Cursors.Hand;
            startscript_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            startscript_Btn.FlatAppearance.BorderSize = 0;
            startscript_Btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            startscript_Btn.FlatStyle = FlatStyle.Flat;
            startscript_Btn.ForeColor = SystemColors.ButtonFace;
            startscript_Btn.Location = new Point(126, 3);
            startscript_Btn.Margin = new Padding(25, 3, 25, 3);
            startscript_Btn.Name = "startscript_Btn";
            startscript_Btn.Size = new Size(159, 39);
            startscript_Btn.TabIndex = 5;
            startscript_Btn.Text = "Start Script";
            startscript_Btn.UseVisualStyleBackColor = false;
            startscript_Btn.Click += startscript_Btn_Click;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(status_Label);
            panel1.Controls.Add(cleanExcel_Btn);
            panel1.Controls.Add(itemToExcel_Btn);
            panel1.Controls.Add(startscript_Btn);
            panel1.Location = new Point(12, 118);
            panel1.Name = "panel1";
            panel1.Size = new Size(402, 291);
            panel1.TabIndex = 6;
            // 
            // status_Label
            // 
            status_Label.Dock = DockStyle.Bottom;
            status_Label.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            status_Label.ForeColor = Color.Lavender;
            status_Label.Location = new Point(0, 247);
            status_Label.Name = "status_Label";
            status_Label.Size = new Size(402, 44);
            status_Label.TabIndex = 9;
            status_Label.Text = "Status: Waiting...";
            status_Label.TextAlign = ContentAlignment.BottomCenter;
            // 
            // cleanExcel_Btn
            // 
            cleanExcel_Btn.BackColor = SystemColors.ControlDarkDark;
            cleanExcel_Btn.BackgroundImageLayout = ImageLayout.None;
            cleanExcel_Btn.Cursor = Cursors.Hand;
            cleanExcel_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            cleanExcel_Btn.FlatAppearance.BorderSize = 0;
            cleanExcel_Btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            cleanExcel_Btn.FlatStyle = FlatStyle.Flat;
            cleanExcel_Btn.ForeColor = SystemColors.ButtonFace;
            cleanExcel_Btn.Location = new Point(135, 102);
            cleanExcel_Btn.Margin = new Padding(25, 3, 25, 3);
            cleanExcel_Btn.Name = "cleanExcel_Btn";
            cleanExcel_Btn.Size = new Size(141, 28);
            cleanExcel_Btn.TabIndex = 7;
            cleanExcel_Btn.Text = "Clean Excel";
            cleanExcel_Btn.UseVisualStyleBackColor = false;
            cleanExcel_Btn.Click += cleanExcel_Btn_Click;
            // 
            // itemToExcel_Btn
            // 
            itemToExcel_Btn.BackColor = SystemColors.ControlDarkDark;
            itemToExcel_Btn.BackgroundImageLayout = ImageLayout.None;
            itemToExcel_Btn.Cursor = Cursors.Hand;
            itemToExcel_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            itemToExcel_Btn.FlatAppearance.BorderSize = 0;
            itemToExcel_Btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            itemToExcel_Btn.FlatStyle = FlatStyle.Flat;
            itemToExcel_Btn.ForeColor = SystemColors.ButtonFace;
            itemToExcel_Btn.Location = new Point(135, 57);
            itemToExcel_Btn.Margin = new Padding(25, 3, 25, 3);
            itemToExcel_Btn.Name = "itemToExcel_Btn";
            itemToExcel_Btn.Size = new Size(141, 28);
            itemToExcel_Btn.TabIndex = 6;
            itemToExcel_Btn.Text = "Items to Excel";
            itemToExcel_Btn.UseVisualStyleBackColor = false;
            itemToExcel_Btn.Click += itemToExcel_Btn_Click;
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(428, 46);
            title_Label.TabIndex = 7;
            title_Label.Text = "Title";
            title_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // description_Label
            // 
            description_Label.Dock = DockStyle.Top;
            description_Label.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            description_Label.ForeColor = Color.Lavender;
            description_Label.Location = new Point(0, 46);
            description_Label.Name = "description_Label";
            description_Label.Size = new Size(428, 42);
            description_Label.TabIndex = 8;
            description_Label.Text = "Description";
            description_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ReceivingGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = close_Btn;
            ClientSize = new Size(428, 421);
            Controls.Add(close_Btn);
            Controls.Add(description_Label);
            Controls.Add(title_Label);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReceivingGUI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ReceivingGUI";
            TopMost = true;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button close_Btn;
        private Button startscript_Btn;
        private Panel panel1;
        private Button cleanExcel_Btn;
        private Button itemToExcel_Btn;
        private Label title_Label;
        private Label description_Label;
        private Label status_Label;
    }
}