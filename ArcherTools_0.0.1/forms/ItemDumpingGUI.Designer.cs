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
            SuspendLayout();
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(406, 35);
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
            description_Label.Size = new Size(406, 39);
            description_Label.TabIndex = 2;
            description_Label.Text = "Description";
            description_Label.TextAlign = ContentAlignment.MiddleCenter;
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
            sendItems_btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            sendItems_btn.FlatStyle = FlatStyle.Flat;
            sendItems_btn.ForeColor = SystemColors.ButtonFace;
            sendItems_btn.Location = new Point(167, 457);
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
            lines_label.Location = new Point(12, 457);
            lines_label.Name = "lines_label";
            lines_label.Size = new Size(46, 15);
            lines_label.TabIndex = 9;
            lines_label.Text = "Lines: 0";
            // 
            // ItemDumpingGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            CancelButton = close_Btn;
            ClientSize = new Size(406, 480);
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
    }
}