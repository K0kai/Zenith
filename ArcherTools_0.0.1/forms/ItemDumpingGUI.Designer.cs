namespace ArcherTools_0._0._1.forms
{
    partial class ItemDumpingGUI
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
            title_Label = new Label();
            description_Label = new Label();
            close_Btn = new Button();
            sendItems_btn = new Button();
            itemValues_Box = new TextBox();
            lines_label = new Label();
            selectedLn_Label = new Label();
            sendToCtn_btn = new Button();
            SuspendLayout();
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(408, 35);
            title_Label.TabIndex = 1;
            title_Label.Text = "Title";
            title_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // description_Label
            // 
            description_Label.AutoEllipsis = true;
            description_Label.Dock = DockStyle.Top;
            description_Label.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            description_Label.ForeColor = Color.Lavender;
            description_Label.Location = new Point(0, 35);
            description_Label.Name = "description_Label";
            description_Label.Size = new Size(408, 39);
            description_Label.TabIndex = 2;
            description_Label.Text = "Description";
            description_Label.TextAlign = ContentAlignment.MiddleCenter;
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
            close_Btn.Location = new Point(389, -3);
            close_Btn.Name = "close_Btn";
            close_Btn.Size = new Size(22, 23);
            close_Btn.TabIndex = 3;
            close_Btn.Text = "X";
            close_Btn.UseVisualStyleBackColor = false;
            close_Btn.Click += close_Btn_Click;
            // 
            // sendItems_btn
            // 
            sendItems_btn.BackColor = SystemColors.ControlDarkDark;
            sendItems_btn.BackgroundImageLayout = ImageLayout.None;
            sendItems_btn.Cursor = Cursors.Hand;
            sendItems_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            sendItems_btn.FlatAppearance.BorderSize = 0;
            sendItems_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            sendItems_btn.FlatStyle = FlatStyle.Flat;
            sendItems_btn.ForeColor = SystemColors.ButtonFace;
            sendItems_btn.Location = new Point(146, 473);
            sendItems_btn.Margin = new Padding(25, 3, 25, 3);
            sendItems_btn.Name = "sendItems_btn";
            sendItems_btn.Size = new Size(61, 21);
            sendItems_btn.TabIndex = 8;
            sendItems_btn.Text = "Send";
            sendItems_btn.UseVisualStyleBackColor = false;
            sendItems_btn.Click += sendItems_btn_Click;
            // 
            // itemValues_Box
            // 
            itemValues_Box.BackColor = Color.FromArgb(40, 40, 40);
            itemValues_Box.Cursor = Cursors.IBeam;
            itemValues_Box.ForeColor = Color.GhostWhite;
            itemValues_Box.Location = new Point(12, 77);
            itemValues_Box.Multiline = true;
            itemValues_Box.Name = "itemValues_Box";
            itemValues_Box.PlaceholderText = "24/12/6/3/1.5";
            itemValues_Box.ScrollBars = ScrollBars.Vertical;
            itemValues_Box.Size = new Size(382, 374);
            itemValues_Box.TabIndex = 0;
            
            // 
            // lines_label
            // 
            lines_label.AutoSize = true;
            lines_label.ForeColor = SystemColors.ButtonHighlight;
            lines_label.Location = new Point(12, 454);
            lines_label.Name = "lines_label";
            lines_label.Size = new Size(46, 15);
            lines_label.TabIndex = 9;
            lines_label.Text = "Lines: 0";
            // 
            // selectedLn_Label
            // 
            selectedLn_Label.AutoSize = true;
            selectedLn_Label.ForeColor = SystemColors.ButtonHighlight;
            selectedLn_Label.Location = new Point(12, 476);
            selectedLn_Label.Name = "selectedLn_Label";
            selectedLn_Label.Size = new Size(88, 15);
            selectedLn_Label.TabIndex = 10;
            selectedLn_Label.Text = "Selected Line: 0";
            // 
            // sendToCtn_btn
            // 
            sendToCtn_btn.BackColor = SystemColors.ControlDarkDark;
            sendToCtn_btn.BackgroundImageLayout = ImageLayout.None;
            sendToCtn_btn.Cursor = Cursors.Hand;
            sendToCtn_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            sendToCtn_btn.FlatAppearance.BorderSize = 0;
            sendToCtn_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            sendToCtn_btn.FlatStyle = FlatStyle.Flat;
            sendToCtn_btn.ForeColor = SystemColors.ButtonFace;
            sendToCtn_btn.Location = new Point(281, 473);
            sendToCtn_btn.Margin = new Padding(25, 3, 25, 3);
            sendToCtn_btn.Name = "sendToCtn_btn";
            sendToCtn_btn.Size = new Size(113, 21);
            sendToCtn_btn.TabIndex = 11;
            sendToCtn_btn.Text = "Send To Container";
            sendToCtn_btn.UseVisualStyleBackColor = false;
            sendToCtn_btn.Click += sendToCtn_btn_Click;
            // 
            // ItemDumpingGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = close_Btn;
            ClientSize = new Size(408, 497);
            Controls.Add(sendToCtn_btn);
            Controls.Add(selectedLn_Label);
            Controls.Add(lines_label);
            Controls.Add(sendItems_btn);
            Controls.Add(close_Btn);
            Controls.Add(description_Label);
            Controls.Add(title_Label);
            Controls.Add(itemValues_Box);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ItemDumpingGUI";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ItemDumpingGUI";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label title_Label;
        private Label description_Label;
        private Button close_Btn;
        private Button sendItems_btn;
        private TextBox itemValues_Box;
        private Label lines_label;
        private Label selectedLn_Label;
        private Button sendToCtn_btn;
    }
}