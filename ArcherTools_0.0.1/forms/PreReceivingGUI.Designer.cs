namespace ArcherTools_0._0._1.forms
{
    partial class PreReceivingGUI
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
            description_Label = new Label();
            title_Label = new Label();
            panel1 = new Panel();
            imaFile_lbl = new Label();
            viewCtn_btn = new Button();
            status_Label = new Label();
            imaprercv_Btn = new Button();
            close_Btn = new Button();
            miscManifest_lbl = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // description_Label
            // 
            description_Label.Dock = DockStyle.Top;
            description_Label.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            description_Label.ForeColor = Color.Lavender;
            description_Label.Location = new Point(0, 46);
            description_Label.Name = "description_Label";
            description_Label.Size = new Size(454, 42);
            description_Label.TabIndex = 10;
            description_Label.Text = "Description";
            description_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(454, 46);
            title_Label.TabIndex = 9;
            title_Label.Text = "Title";
            title_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(miscManifest_lbl);
            panel1.Controls.Add(imaFile_lbl);
            panel1.Controls.Add(viewCtn_btn);
            panel1.Controls.Add(status_Label);
            panel1.Controls.Add(imaprercv_Btn);
            panel1.Location = new Point(12, 117);
            panel1.Name = "panel1";
            panel1.Size = new Size(433, 265);
            panel1.TabIndex = 11;
            // 
            // imaFile_lbl
            // 
            imaFile_lbl.BackColor = Color.Transparent;
            imaFile_lbl.Cursor = Cursors.Hand;
            imaFile_lbl.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            imaFile_lbl.ForeColor = Color.WhiteSmoke;
            imaFile_lbl.Location = new Point(0, 0);
            imaFile_lbl.Name = "imaFile_lbl";
            imaFile_lbl.Size = new Size(207, 15);
            imaFile_lbl.TabIndex = 11;
            imaFile_lbl.Text = "IMA Manifest:";
            imaFile_lbl.TextAlign = ContentAlignment.BottomCenter;
            imaFile_lbl.Click += label1_Click;
            // 
            // viewCtn_btn
            // 
            viewCtn_btn.BackColor = SystemColors.ControlDarkDark;
            viewCtn_btn.BackgroundImageLayout = ImageLayout.None;
            viewCtn_btn.Cursor = Cursors.Hand;
            viewCtn_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            viewCtn_btn.FlatAppearance.BorderSize = 0;
            viewCtn_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            viewCtn_btn.FlatStyle = FlatStyle.Flat;
            viewCtn_btn.ForeColor = SystemColors.ButtonFace;
            viewCtn_btn.Location = new Point(63, 70);
            viewCtn_btn.Margin = new Padding(25, 3, 25, 3);
            viewCtn_btn.Name = "viewCtn_btn";
            viewCtn_btn.Size = new Size(101, 22);
            viewCtn_btn.TabIndex = 10;
            viewCtn_btn.Text = "View Containers";
            viewCtn_btn.UseVisualStyleBackColor = false;
            // 
            // status_Label
            // 
            status_Label.Dock = DockStyle.Bottom;
            status_Label.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            status_Label.ForeColor = Color.Lavender;
            status_Label.Location = new Point(0, 250);
            status_Label.Name = "status_Label";
            status_Label.Size = new Size(433, 15);
            status_Label.TabIndex = 9;
            status_Label.Text = "Status: Waiting...";
            status_Label.TextAlign = ContentAlignment.BottomCenter;
            // 
            // imaprercv_Btn
            // 
            imaprercv_Btn.BackColor = SystemColors.ControlDarkDark;
            imaprercv_Btn.BackgroundImageLayout = ImageLayout.None;
            imaprercv_Btn.Cursor = Cursors.Hand;
            imaprercv_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            imaprercv_Btn.FlatAppearance.BorderSize = 0;
            imaprercv_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            imaprercv_Btn.FlatStyle = FlatStyle.Flat;
            imaprercv_Btn.ForeColor = SystemColors.ButtonFace;
            imaprercv_Btn.Location = new Point(33, 18);
            imaprercv_Btn.Margin = new Padding(25, 3, 25, 3);
            imaprercv_Btn.Name = "imaprercv_Btn";
            imaprercv_Btn.Size = new Size(159, 44);
            imaprercv_Btn.TabIndex = 5;
            imaprercv_Btn.Text = "Intermarket Container Creation";
            imaprercv_Btn.UseVisualStyleBackColor = false;
            // 
            // close_Btn
            // 
            close_Btn.BackColor = Color.FromArgb(184, 44, 95);
            close_Btn.BackgroundImageLayout = ImageLayout.None;
            close_Btn.DialogResult = DialogResult.Cancel;
            close_Btn.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            close_Btn.FlatAppearance.BorderSize = 0;
            close_Btn.FlatStyle = FlatStyle.Flat;
            close_Btn.ForeColor = Color.Lavender;
            close_Btn.Location = new Point(432, 0);
            close_Btn.Name = "close_Btn";
            close_Btn.Size = new Size(22, 23);
            close_Btn.TabIndex = 12;
            close_Btn.Text = "X";
            close_Btn.UseVisualStyleBackColor = false;
            // 
            // miscManifest_lbl
            // 
            miscManifest_lbl.BackColor = Color.Transparent;
            miscManifest_lbl.Cursor = Cursors.Hand;
            miscManifest_lbl.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            miscManifest_lbl.ForeColor = Color.WhiteSmoke;
            miscManifest_lbl.Location = new Point(213, 0);
            miscManifest_lbl.Name = "miscManifest_lbl";
            miscManifest_lbl.Size = new Size(217, 15);
            miscManifest_lbl.TabIndex = 12;
            miscManifest_lbl.Text = "Misc. Manifest:";
            miscManifest_lbl.TextAlign = ContentAlignment.BottomCenter;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlDarkDark;
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(278, 18);
            button1.Margin = new Padding(25, 3, 25, 3);
            button1.Name = "button1";
            button1.Size = new Size(90, 44);
            button1.TabIndex = 13;
            button1.Text = "Manifest CFG Creation";
            button1.UseVisualStyleBackColor = false;
            // 
            // PreReceivingGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(454, 406);
            Controls.Add(close_Btn);
            Controls.Add(panel1);
            Controls.Add(description_Label);
            Controls.Add(title_Label);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PreReceivingGUI";
            Text = "PreReceivingGUI";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label description_Label;
        private Label title_Label;
        private Panel panel1;
        private Button viewCtn_btn;
        private Label status_Label;
        private Button cleanExcel_Btn;
        private Button itemToExcel_Btn;
        private Button imaprercv_Btn;
        private Label imaFile_lbl;
        private Button close_Btn;
        private Label miscManifest_lbl;
        private Button button1;
    }
}