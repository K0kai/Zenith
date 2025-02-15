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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            pagelabel = new Label();
            copyrights = new LinkLabel();
            introlabel = new Label();
            receiveBtn = new Button();
            ToolsListPanel = new FlowLayoutPanel();
            methods_Btn = new Button();
            prercv_Btn = new Button();
            vpnConnect_btn = new Button();
            settings_Btn = new Button();
            testbutton = new Button();
            return_Btn = new Button();
            panel2 = new Panel();
            connectToVpn_Btn = new ToolTip(components);
            preRcv_tooltip = new ToolTip(components);
            panel3 = new Panel();
            panel1.SuspendLayout();
            ToolsListPanel.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pagelabel);
            panel1.Controls.Add(copyrights);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 143);
            panel1.TabIndex = 7;
            // 
            // pagelabel
            // 
            pagelabel.Dock = DockStyle.Bottom;
            pagelabel.Font = new Font("Franklin Gothic Medium Cond", 20.25F);
            pagelabel.ForeColor = Color.FromArgb(184, 44, 95);
            pagelabel.Location = new Point(0, 99);
            pagelabel.Name = "pagelabel";
            pagelabel.Size = new Size(350, 44);
            pagelabel.TabIndex = 2;
            pagelabel.Text = "Zenith";
            pagelabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // copyrights
            // 
            copyrights.BackColor = Color.Transparent;
            copyrights.DisabledLinkColor = Color.Black;
            copyrights.Dock = DockStyle.Top;
            copyrights.Font = new Font("Segoe UI", 9F);
            copyrights.ForeColor = SystemColors.ButtonHighlight;
            copyrights.LinkArea = new LinkArea(26, 5);
            copyrights.LinkColor = SystemColors.ActiveCaption;
            copyrights.Location = new Point(0, 0);
            copyrights.Name = "copyrights";
            copyrights.Size = new Size(350, 21);
            copyrights.TabIndex = 0;
            copyrights.TabStop = true;
            copyrights.Tag = "https://github.com/K0kai";
            copyrights.Text = "This tool was created by: K0kai";
            copyrights.TextAlign = ContentAlignment.TopCenter;
            copyrights.UseCompatibleTextRendering = true;
            copyrights.VisitedLinkColor = Color.FromArgb(255, 128, 128);
            // 
            // introlabel
            // 
            introlabel.AutoEllipsis = true;
            introlabel.Dock = DockStyle.Top;
            introlabel.Font = new Font("Franklin Gothic Medium Cond", 14.25F);
            introlabel.ForeColor = Color.Lavender;
            introlabel.Location = new Point(0, 0);
            introlabel.Name = "introlabel";
            introlabel.Size = new Size(350, 42);
            introlabel.TabIndex = 1;
            introlabel.Text = "Sup.";
            introlabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // receiveBtn
            // 
            receiveBtn.BackColor = Color.FromArgb(47, 56, 74);
            receiveBtn.BackgroundImageLayout = ImageLayout.None;
            receiveBtn.Cursor = Cursors.Hand;
            receiveBtn.FlatAppearance.BorderColor = Color.FromArgb(184, 44, 95);
            receiveBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            receiveBtn.FlatStyle = FlatStyle.Flat;
            receiveBtn.ForeColor = SystemColors.ButtonFace;
            receiveBtn.Location = new Point(25, 43);
            receiveBtn.Margin = new Padding(25, 3, 25, 3);
            receiveBtn.Name = "receiveBtn";
            receiveBtn.Size = new Size(70, 34);
            receiveBtn.TabIndex = 2;
            receiveBtn.Text = "Receiving";
            receiveBtn.UseVisualStyleBackColor = false;
            receiveBtn.Visible = false;
            receiveBtn.Click += receiveBtn_Click_1;
            // 
            // ToolsListPanel
            // 
            ToolsListPanel.AutoSize = true;
            ToolsListPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ToolsListPanel.Controls.Add(methods_Btn);
            ToolsListPanel.Controls.Add(receiveBtn);
            ToolsListPanel.Controls.Add(prercv_Btn);
            ToolsListPanel.Controls.Add(vpnConnect_btn);
            ToolsListPanel.Controls.Add(settings_Btn);
            ToolsListPanel.Controls.Add(testbutton);
            ToolsListPanel.Controls.Add(return_Btn);
            ToolsListPanel.FlowDirection = FlowDirection.TopDown;
            ToolsListPanel.Location = new Point(0, 45);
            ToolsListPanel.Name = "ToolsListPanel";
            ToolsListPanel.Size = new Size(158, 244);
            ToolsListPanel.TabIndex = 6;
            // 
            // methods_Btn
            // 
            methods_Btn.BackColor = Color.FromArgb(47, 56, 74);
            methods_Btn.BackgroundImageLayout = ImageLayout.None;
            methods_Btn.Cursor = Cursors.Hand;
            methods_Btn.FlatAppearance.BorderColor = Color.FromArgb(184, 44, 95);
            methods_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            methods_Btn.FlatStyle = FlatStyle.Flat;
            methods_Btn.ForeColor = SystemColors.ButtonFace;
            methods_Btn.Location = new Point(25, 3);
            methods_Btn.Margin = new Padding(25, 3, 25, 3);
            methods_Btn.Name = "methods_Btn";
            methods_Btn.Size = new Size(70, 34);
            methods_Btn.TabIndex = 8;
            methods_Btn.Text = "Methods";
            methods_Btn.UseVisualStyleBackColor = false;
            methods_Btn.Click += methods_Btn_Click;
            // 
            // prercv_Btn
            // 
            prercv_Btn.BackColor = Color.FromArgb(47, 56, 74);
            prercv_Btn.BackgroundImageLayout = ImageLayout.None;
            prercv_Btn.Cursor = Cursors.Hand;
            prercv_Btn.FlatAppearance.BorderColor = Color.FromArgb(184, 44, 95);
            prercv_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            prercv_Btn.FlatStyle = FlatStyle.Flat;
            prercv_Btn.ForeColor = SystemColors.ButtonFace;
            prercv_Btn.Location = new Point(25, 83);
            prercv_Btn.Margin = new Padding(25, 3, 25, 3);
            prercv_Btn.Name = "prercv_Btn";
            prercv_Btn.Size = new Size(92, 34);
            prercv_Btn.TabIndex = 7;
            prercv_Btn.Text = "Pre-Receiving";
            prercv_Btn.UseVisualStyleBackColor = false;
            prercv_Btn.Visible = false;
            prercv_Btn.Click += prercv_Btn_Click;
            // 
            // vpnConnect_btn
            // 
            vpnConnect_btn.BackColor = Color.FromArgb(47, 56, 74);
            vpnConnect_btn.BackgroundImageLayout = ImageLayout.None;
            vpnConnect_btn.Cursor = Cursors.Hand;
            vpnConnect_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            vpnConnect_btn.FlatAppearance.BorderSize = 0;
            vpnConnect_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            vpnConnect_btn.FlatStyle = FlatStyle.Flat;
            vpnConnect_btn.ForeColor = SystemColors.ButtonFace;
            vpnConnect_btn.Location = new Point(25, 123);
            vpnConnect_btn.Margin = new Padding(25, 3, 25, 3);
            vpnConnect_btn.Name = "vpnConnect_btn";
            vpnConnect_btn.Size = new Size(108, 25);
            vpnConnect_btn.TabIndex = 4;
            vpnConnect_btn.Text = "Connect to VPN";
            vpnConnect_btn.UseVisualStyleBackColor = false;
            vpnConnect_btn.Visible = false;
            vpnConnect_btn.Click += vpnConnect_btn_Click;
            // 
            // settings_Btn
            // 
            settings_Btn.BackColor = Color.FromArgb(47, 56, 74);
            settings_Btn.BackgroundImageLayout = ImageLayout.None;
            settings_Btn.Cursor = Cursors.Hand;
            settings_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            settings_Btn.FlatAppearance.BorderSize = 0;
            settings_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            settings_Btn.FlatStyle = FlatStyle.Flat;
            settings_Btn.ForeColor = SystemColors.ButtonFace;
            settings_Btn.Location = new Point(25, 154);
            settings_Btn.Margin = new Padding(25, 3, 25, 3);
            settings_Btn.Name = "settings_Btn";
            settings_Btn.Size = new Size(70, 25);
            settings_Btn.TabIndex = 5;
            settings_Btn.Text = "Settings";
            settings_Btn.UseVisualStyleBackColor = false;
            settings_Btn.Click += settings_Btn_Click;
            // 
            // testbutton
            // 
            testbutton.BackColor = Color.FromArgb(47, 56, 74);
            testbutton.BackgroundImageLayout = ImageLayout.None;
            testbutton.Cursor = Cursors.Hand;
            testbutton.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            testbutton.FlatAppearance.BorderSize = 0;
            testbutton.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            testbutton.FlatStyle = FlatStyle.Flat;
            testbutton.ForeColor = SystemColors.ButtonFace;
            testbutton.Location = new Point(25, 185);
            testbutton.Margin = new Padding(25, 3, 25, 3);
            testbutton.Name = "testbutton";
            testbutton.Size = new Size(79, 25);
            testbutton.TabIndex = 6;
            testbutton.Text = "TestButton";
            testbutton.UseVisualStyleBackColor = false;
            testbutton.Visible = false;
            testbutton.Click += testbutton_Click;
            // 
            // return_Btn
            // 
            return_Btn.BackColor = Color.FromArgb(47, 56, 74);
            return_Btn.BackgroundImageLayout = ImageLayout.None;
            return_Btn.Cursor = Cursors.Hand;
            return_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            return_Btn.FlatAppearance.BorderSize = 0;
            return_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(184, 44, 95);
            return_Btn.FlatStyle = FlatStyle.Flat;
            return_Btn.ForeColor = SystemColors.ButtonFace;
            return_Btn.Location = new Point(25, 216);
            return_Btn.Margin = new Padding(25, 3, 25, 3);
            return_Btn.Name = "return_Btn";
            return_Btn.Size = new Size(70, 25);
            return_Btn.TabIndex = 9;
            return_Btn.Text = "<- Return";
            return_Btn.UseVisualStyleBackColor = false;
            return_Btn.Visible = false;
            return_Btn.Click += return_Btn_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(184, 44, 95);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 461);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 10);
            panel2.TabIndex = 8;
            // 
            // connectToVpn_Btn
            // 
            connectToVpn_Btn.AutoPopDelay = 5000;
            connectToVpn_Btn.InitialDelay = 200;
            connectToVpn_Btn.ReshowDelay = 100;
            connectToVpn_Btn.ToolTipIcon = ToolTipIcon.Warning;
            connectToVpn_Btn.ToolTipTitle = "Warning";
            // 
            // preRcv_tooltip
            // 
            preRcv_tooltip.AutoPopDelay = 5000;
            preRcv_tooltip.InitialDelay = 200;
            preRcv_tooltip.ReshowDelay = 100;
            preRcv_tooltip.ToolTipIcon = ToolTipIcon.Warning;
            preRcv_tooltip.ToolTipTitle = "Warning";
            // 
            // panel3
            // 
            panel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel3.Controls.Add(ToolsListPanel);
            panel3.Controls.Add(introlabel);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 169);
            panel3.Name = "panel3";
            panel3.Size = new Size(350, 292);
            panel3.TabIndex = 9;
            // 
            // ToolHub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(36, 42, 54);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ToolHub";
            Size = new Size(350, 471);
            SizeChanged += ToolHub_SizeChanged;
            panel1.ResumeLayout(false);
            ToolsListPanel.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
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
        private Button prercv_Btn;
        private ToolTip connectToVpn_Btn;
        private ToolTip preRcv_tooltip;
        private Button methods_Btn;
        private Button return_Btn;
        private Panel panel3;
    }
}
