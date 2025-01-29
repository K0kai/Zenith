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
            components = new System.ComponentModel.Container();
            rcvSet_Btn = new Button();
            pagelabel = new Label();
            label1 = new Label();
            toolsCfg_Panel = new Panel();
            checkfordefault_checkbox = new CheckBox();
            autocreatecfg_checkbox = new CheckBox();
            panel1 = new Panel();
            presetList = new ListBox();
            presetList_dropbtn = new Button();
            voicelines_checkbtn = new CheckBox();
            label2 = new Label();
            return_Btn = new Button();
            presetDropTimer = new System.Windows.Forms.Timer(components);
            overlayTip_lbl = new Label();
            toolsCfg_Panel.SuspendLayout();
            panel1.SuspendLayout();
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
            rcvSet_Btn.Click += rcvSet_Btn_Click;
            // 
            // pagelabel
            // 
            pagelabel.Dock = DockStyle.Top;
            pagelabel.Font = new Font("Franklin Gothic Medium Cond", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pagelabel.ForeColor = Color.Lavender;
            pagelabel.Location = new Point(0, 0);
            pagelabel.Name = "pagelabel";
            pagelabel.Size = new Size(396, 30);
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
            toolsCfg_Panel.Controls.Add(checkfordefault_checkbox);
            toolsCfg_Panel.Controls.Add(autocreatecfg_checkbox);
            toolsCfg_Panel.Controls.Add(rcvSet_Btn);
            toolsCfg_Panel.Controls.Add(label1);
            toolsCfg_Panel.Location = new Point(12, 75);
            toolsCfg_Panel.Name = "toolsCfg_Panel";
            toolsCfg_Panel.Size = new Size(136, 357);
            toolsCfg_Panel.TabIndex = 27;
            // 
            // checkfordefault_checkbox
            // 
            checkfordefault_checkbox.AutoSize = true;
            checkfordefault_checkbox.ForeColor = Color.Lavender;
            checkfordefault_checkbox.Location = new Point(13, 97);
            checkfordefault_checkbox.Name = "checkfordefault_checkbox";
            checkfordefault_checkbox.Size = new Size(118, 19);
            checkfordefault_checkbox.TabIndex = 32;
            checkfordefault_checkbox.Text = "Check for Default";
            checkfordefault_checkbox.UseVisualStyleBackColor = true;
            // 
            // autocreatecfg_checkbox
            // 
            autocreatecfg_checkbox.AutoSize = true;
            autocreatecfg_checkbox.ForeColor = Color.Lavender;
            autocreatecfg_checkbox.Location = new Point(13, 72);
            autocreatecfg_checkbox.Name = "autocreatecfg_checkbox";
            autocreatecfg_checkbox.Size = new Size(111, 19);
            autocreatecfg_checkbox.TabIndex = 31;
            autocreatecfg_checkbox.Text = "Auto Create Cfg";
            autocreatecfg_checkbox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(presetList);
            panel1.Controls.Add(presetList_dropbtn);
            panel1.Controls.Add(voicelines_checkbtn);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(249, 75);
            panel1.MaximumSize = new Size(134, 357);
            panel1.Name = "panel1";
            panel1.Size = new Size(134, 357);
            panel1.TabIndex = 28;
            // 
            // presetList
            // 
            presetList.BackColor = Color.DimGray;
            presetList.BorderStyle = BorderStyle.FixedSingle;
            presetList.ForeColor = SystemColors.Menu;
            presetList.FormattingEnabled = true;
            presetList.ItemHeight = 15;
            presetList.Location = new Point(8, 92);
            presetList.MaximumSize = new Size(125, 137);
            presetList.MinimumSize = new Size(1, 1);
            presetList.Name = "presetList";
            presetList.Size = new Size(116, 137);
            presetList.TabIndex = 30;
            presetList.MouseDoubleClick += presetDoubleClick;
            // 
            // presetList_dropbtn
            // 
            presetList_dropbtn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            presetList_dropbtn.BackColor = Color.DimGray;
            presetList_dropbtn.BackgroundImageLayout = ImageLayout.None;
            presetList_dropbtn.Cursor = Cursors.Hand;
            presetList_dropbtn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            presetList_dropbtn.FlatAppearance.BorderSize = 0;
            presetList_dropbtn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            presetList_dropbtn.FlatStyle = FlatStyle.Flat;
            presetList_dropbtn.ForeColor = SystemColors.ButtonFace;
            presetList_dropbtn.Location = new Point(8, 65);
            presetList_dropbtn.Margin = new Padding(25, 3, 25, 3);
            presetList_dropbtn.Name = "presetList_dropbtn";
            presetList_dropbtn.Size = new Size(116, 25);
            presetList_dropbtn.TabIndex = 29;
            presetList_dropbtn.Text = "Presets";
            presetList_dropbtn.UseVisualStyleBackColor = false;
            presetList_dropbtn.Click += presetList_dropbtn_Click;
            // 
            // voicelines_checkbtn
            // 
            voicelines_checkbtn.AutoSize = true;
            voicelines_checkbtn.Checked = true;
            voicelines_checkbtn.CheckState = CheckState.Checked;
            voicelines_checkbtn.ForeColor = Color.Lavender;
            voicelines_checkbtn.Location = new Point(8, 40);
            voicelines_checkbtn.Name = "voicelines_checkbtn";
            voicelines_checkbtn.Size = new Size(116, 19);
            voicelines_checkbtn.TabIndex = 28;
            voicelines_checkbtn.Text = "Enable Voicelines";
            voicelines_checkbtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Franklin Gothic Medium Cond", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Lavender;
            label2.Location = new Point(34, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 24);
            label2.TabIndex = 27;
            label2.Text = "Program";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // return_Btn
            // 
            return_Btn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            return_Btn.BackColor = SystemColors.ControlDarkDark;
            return_Btn.BackgroundImageLayout = ImageLayout.None;
            return_Btn.Cursor = Cursors.Hand;
            return_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            return_Btn.FlatAppearance.BorderSize = 0;
            return_Btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            return_Btn.FlatStyle = FlatStyle.Flat;
            return_Btn.ForeColor = SystemColors.ButtonFace;
            return_Btn.Location = new Point(155, 389);
            return_Btn.Margin = new Padding(25, 3, 25, 3);
            return_Btn.Name = "return_Btn";
            return_Btn.Size = new Size(85, 25);
            return_Btn.TabIndex = 27;
            return_Btn.Text = "Go back";
            return_Btn.UseVisualStyleBackColor = false;
            return_Btn.Click += return_Btn_Click;
            // 
            // presetDropTimer
            // 
            presetDropTimer.Interval = 25;
            presetDropTimer.Tick += presetDropTimer_Tick;
            // 
            // overlayTip_lbl
            // 
            overlayTip_lbl.Dock = DockStyle.Top;
            overlayTip_lbl.Font = new Font("Arial Rounded MT Bold", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            overlayTip_lbl.ForeColor = Color.FromArgb(255, 128, 128);
            overlayTip_lbl.Location = new Point(0, 30);
            overlayTip_lbl.Name = "overlayTip_lbl";
            overlayTip_lbl.Size = new Size(396, 18);
            overlayTip_lbl.TabIndex = 29;
            overlayTip_lbl.Text = "Press \"END\" key to move to next step";
            overlayTip_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SettingsUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(overlayTip_lbl);
            Controls.Add(return_Btn);
            Controls.Add(panel1);
            Controls.Add(toolsCfg_Panel);
            Controls.Add(pagelabel);
            Name = "SettingsUserControl";
            Size = new Size(396, 444);
            toolsCfg_Panel.ResumeLayout(false);
            toolsCfg_Panel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button rcvSet_Btn;
        private Label pagelabel;
        private Label label1;
        private Panel toolsCfg_Panel;
        private Panel panel1;
        private Label label2;
        private CheckBox voicelines_checkbtn;
        private Button return_Btn;
        private System.Windows.Forms.Timer presetDropTimer;
        private Button presetList_dropbtn;
        private ListBox presetList;
        private CheckBox autocreatecfg_checkbox;
        private CheckBox checkfordefault_checkbox;
        private Label overlayTip_lbl;
    }
}
