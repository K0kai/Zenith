namespace ArcherTools_0._0._1
{
    partial class SettingsUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rcvSet_Btn = new Button();
            pagelabel = new Label();
            label1 = new Label();
            toolsCfg_Panel = new Panel();
            toolsCfg_Panel.SuspendLayout();
            SuspendLayout();
            // 
            // rcvSet_Btn
            // 
            rcvSet_Btn.Anchor = AnchorStyles.Top;
            rcvSet_Btn.BackColor = SystemColors.ControlDarkDark;
            rcvSet_Btn.BackgroundImageLayout = ImageLayout.None;
            rcvSet_Btn.Cursor = Cursors.Hand;
            rcvSet_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            rcvSet_Btn.FlatAppearance.BorderSize = 0;
            rcvSet_Btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            rcvSet_Btn.FlatStyle = FlatStyle.Flat;
            rcvSet_Btn.ForeColor = SystemColors.ButtonFace;
            rcvSet_Btn.Location = new Point(15, 40);
            rcvSet_Btn.Margin = new Padding(25, 3, 25, 3);
            rcvSet_Btn.Name = "rcvSet_Btn";
            rcvSet_Btn.Size = new Size(109, 26);
            rcvSet_Btn.TabIndex = 24;
            rcvSet_Btn.Text = "Set up Receiving";
            rcvSet_Btn.UseVisualStyleBackColor = false;
            // 
            // pagelabel
            // 
            pagelabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            pagelabel.AutoSize = true;
            pagelabel.Font = new Font("Franklin Gothic Medium Cond", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pagelabel.ForeColor = Color.Lavender;
            pagelabel.Location = new Point(137, 0);
            pagelabel.Name = "pagelabel";
            pagelabel.Size = new Size(86, 30);
            pagelabel.TabIndex = 25;
            pagelabel.Text = "Settings";
            pagelabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Franklin Gothic Medium Cond", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Lavender;
            label1.Location = new Point(45, 0);
            label1.Name = "label1";
            label1.Size = new Size(46, 24);
            label1.TabIndex = 26;
            label1.Text = "Tools";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // toolsCfg_Panel
            // 
            toolsCfg_Panel.Controls.Add(rcvSet_Btn);
            toolsCfg_Panel.Controls.Add(label1);
            toolsCfg_Panel.Location = new Point(12, 75);
            toolsCfg_Panel.Name = "toolsCfg_Panel";
            toolsCfg_Panel.Size = new Size(136, 357);
            toolsCfg_Panel.TabIndex = 27;
            // 
            // SettingsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(toolsCfg_Panel);
            Controls.Add(pagelabel);
            Name = "SettingsUserControl";
            Size = new Size(397, 444);
            toolsCfg_Panel.ResumeLayout(false);
            toolsCfg_Panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button rcvSet_Btn;
        private Label pagelabel;
        private Label label1;
        private Panel toolsCfg_Panel;
    }
}
