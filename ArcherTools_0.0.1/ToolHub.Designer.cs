namespace ArcherTools_0._0._1
{
    partial class ToolHub
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
            panel1 = new Panel();
            pagelabel = new Label();
            introlabel = new Label();
            copyrights = new LinkLabel();
            receiveBtn = new Button();
            ToolsListPanel = new FlowLayoutPanel();
            vpnConnect_btn = new Button();
            settings_Btn = new Button();
            panel2 = new Panel();
            testbutton = new Button();
            panel1.SuspendLayout();
            ToolsListPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pagelabel);
            panel1.Controls.Add(introlabel);
            panel1.Controls.Add(copyrights);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(670, 114);
            panel1.TabIndex = 7;
            // 
            // pagelabel
            // 
            pagelabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            pagelabel.AutoSize = true;
            pagelabel.Font = new Font("Franklin Gothic Medium Cond", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pagelabel.ForeColor = Color.Lavender;
            pagelabel.Location = new Point(249, 90);
            pagelabel.Name = "pagelabel";
            pagelabel.Size = new Size(159, 24);
            pagelabel.TabIndex = 2;
            pagelabel.Text = "List of available Tools";
            pagelabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // introlabel
            // 
            introlabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            introlabel.AutoEllipsis = true;
            introlabel.AutoSize = true;
            introlabel.Font = new Font("Franklin Gothic Medium Cond", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            introlabel.ForeColor = Color.FromArgb(184, 44, 95);
            introlabel.Location = new Point(298, 41);
            introlabel.Name = "introlabel";
            introlabel.Size = new Size(60, 34);
            introlabel.TabIndex = 1;
            introlabel.Text = "Sup.";
            introlabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // copyrights
            // 
            copyrights.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            copyrights.AutoSize = true;
            copyrights.BackColor = Color.Transparent;
            copyrights.DisabledLinkColor = Color.Black;
            copyrights.Font = new Font("Segoe UI", 9F);
            copyrights.ForeColor = SystemColors.ButtonHighlight;
            copyrights.LinkArea = new LinkArea(26, 5);
            copyrights.LinkColor = SystemColors.ActiveCaption;
            copyrights.Location = new Point(223, 0);
            copyrights.Name = "copyrights";
            copyrights.Size = new Size(233, 21);
            copyrights.TabIndex = 0;
            copyrights.TabStop = true;
            copyrights.Tag = "https://github.com/K0kai";
            copyrights.Text = "This tool was created by: K0kai (Luis Maia)";
            copyrights.UseCompatibleTextRendering = true;
            copyrights.VisitedLinkColor = Color.FromArgb(255, 128, 128);
            // 
            // receiveBtn
            // 
            receiveBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            receiveBtn.BackColor = Color.FromArgb(47, 56, 74);
            receiveBtn.BackgroundImageLayout = ImageLayout.None;
            receiveBtn.Cursor = Cursors.Hand;
            receiveBtn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            receiveBtn.FlatAppearance.BorderSize = 0;
            receiveBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            receiveBtn.FlatStyle = FlatStyle.Flat;
            receiveBtn.ForeColor = SystemColors.ButtonFace;
            receiveBtn.Location = new Point(25, 3);
            receiveBtn.Margin = new Padding(25, 3, 25, 3);
            receiveBtn.Name = "receiveBtn";
            receiveBtn.Size = new Size(166, 34);
            receiveBtn.TabIndex = 2;
            receiveBtn.Text = "Receiving";
            receiveBtn.UseVisualStyleBackColor = false;
            receiveBtn.Click += receiveBtn_Click_1;
            // 
            // ToolsListPanel
            // 
            ToolsListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            ToolsListPanel.Controls.Add(receiveBtn);
            ToolsListPanel.Controls.Add(vpnConnect_btn);
            ToolsListPanel.Controls.Add(settings_Btn);
            ToolsListPanel.Controls.Add(testbutton);
            ToolsListPanel.FlowDirection = FlowDirection.TopDown;
            ToolsListPanel.Location = new Point(218, 188);
            ToolsListPanel.Name = "ToolsListPanel";
            ToolsListPanel.Size = new Size(216, 435);
            ToolsListPanel.TabIndex = 6;
            // 
            // vpnConnect_btn
            // 
            vpnConnect_btn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            vpnConnect_btn.BackColor = Color.FromArgb(47, 56, 74);
            vpnConnect_btn.BackgroundImageLayout = ImageLayout.None;
            vpnConnect_btn.Cursor = Cursors.Hand;
            vpnConnect_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            vpnConnect_btn.FlatAppearance.BorderSize = 0;
            vpnConnect_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            vpnConnect_btn.FlatStyle = FlatStyle.Flat;
            vpnConnect_btn.ForeColor = SystemColors.ButtonFace;
            vpnConnect_btn.Location = new Point(45, 43);
            vpnConnect_btn.Margin = new Padding(25, 3, 25, 3);
            vpnConnect_btn.Name = "vpnConnect_btn";
            vpnConnect_btn.Size = new Size(126, 25);
            vpnConnect_btn.TabIndex = 4;
            vpnConnect_btn.Text = "Connect to VPN";
            vpnConnect_btn.UseVisualStyleBackColor = false;
            vpnConnect_btn.Click += vpnConnect_btn_Click;
            // 
            // settings_Btn
            // 
            settings_Btn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            settings_Btn.BackColor = Color.FromArgb(47, 56, 74);
            settings_Btn.BackgroundImageLayout = ImageLayout.None;
            settings_Btn.Cursor = Cursors.Hand;
            settings_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            settings_Btn.FlatAppearance.BorderSize = 0;
            settings_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            settings_Btn.FlatStyle = FlatStyle.Flat;
            settings_Btn.ForeColor = SystemColors.ButtonFace;
            settings_Btn.Location = new Point(45, 74);
            settings_Btn.Margin = new Padding(25, 3, 25, 3);
            settings_Btn.Name = "settings_Btn";
            settings_Btn.Size = new Size(126, 25);
            settings_Btn.TabIndex = 5;
            settings_Btn.Text = "Settings";
            settings_Btn.UseVisualStyleBackColor = false;
            settings_Btn.Click += settings_Btn_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(184, 44, 95);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 673);
            panel2.Name = "panel2";
            panel2.Size = new Size(670, 10);
            panel2.TabIndex = 8;
            // 
            // testbutton
            // 
            testbutton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            testbutton.BackColor = Color.FromArgb(47, 56, 74);
            testbutton.BackgroundImageLayout = ImageLayout.None;
            testbutton.Cursor = Cursors.Hand;
            testbutton.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            testbutton.FlatAppearance.BorderSize = 0;
            testbutton.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            testbutton.FlatStyle = FlatStyle.Flat;
            testbutton.ForeColor = SystemColors.ButtonFace;
            testbutton.Location = new Point(45, 105);
            testbutton.Margin = new Padding(25, 3, 25, 3);
            testbutton.Name = "testbutton";
            testbutton.Size = new Size(126, 25);
            testbutton.TabIndex = 6;
            testbutton.Text = "TestButton";
            testbutton.UseVisualStyleBackColor = false;
            testbutton.Click += testbutton_Click;
            // 
            // ToolHub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(36, 42, 54);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(ToolsListPanel);
            Name = "ToolHub";
            Size = new Size(670, 683);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ToolsListPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label pagelabel;
        private Label introlabel;
        private LinkLabel copyrights;
        private Button receiveBtn;
        private FlowLayoutPanel ToolsListPanel;
        private Button vpnConnect_btn;
        private Button settings_Btn;
        private Panel panel2;
        private Button testbutton;
    }
}
