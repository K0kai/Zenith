﻿namespace ArcherTools_0._0._1
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
            receiveconfig_btn = new Button();
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
            introlabel.AutoSize = true;
            introlabel.Font = new Font("Franklin Gothic Medium Cond", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            introlabel.ForeColor = Color.MediumPurple;
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
            copyrights.BackColor = Color.FromArgb(64, 64, 64);
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
            receiveBtn.BackColor = SystemColors.ControlDarkDark;
            receiveBtn.BackgroundImageLayout = ImageLayout.None;
            receiveBtn.Cursor = Cursors.Hand;
            receiveBtn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            receiveBtn.FlatAppearance.BorderSize = 0;
            receiveBtn.FlatAppearance.MouseOverBackColor = Color.Indigo;
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
            ToolsListPanel.Controls.Add(receiveconfig_btn);
            ToolsListPanel.FlowDirection = FlowDirection.TopDown;
            ToolsListPanel.Location = new Point(218, 188);
            ToolsListPanel.Name = "ToolsListPanel";
            ToolsListPanel.Size = new Size(216, 425);
            ToolsListPanel.TabIndex = 6;
            // 
            // receiveconfig_btn
            // 
            receiveconfig_btn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            receiveconfig_btn.BackColor = SystemColors.ControlDarkDark;
            receiveconfig_btn.BackgroundImageLayout = ImageLayout.None;
            receiveconfig_btn.Cursor = Cursors.Hand;
            receiveconfig_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            receiveconfig_btn.FlatAppearance.BorderSize = 0;
            receiveconfig_btn.FlatAppearance.MouseOverBackColor = Color.Indigo;
            receiveconfig_btn.FlatStyle = FlatStyle.Flat;
            receiveconfig_btn.ForeColor = SystemColors.ButtonFace;
            receiveconfig_btn.Location = new Point(45, 43);
            receiveconfig_btn.Margin = new Padding(25, 3, 25, 3);
            receiveconfig_btn.Name = "receiveconfig_btn";
            receiveconfig_btn.Size = new Size(126, 25);
            receiveconfig_btn.TabIndex = 3;
            receiveconfig_btn.Text = "Receive Properties";
            receiveconfig_btn.UseVisualStyleBackColor = false;
            receiveconfig_btn.Click += receiveconfig_btn_Click;
            // 
            // ToolHub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(panel1);
            Controls.Add(ToolsListPanel);
            Name = "ToolHub";
            Size = new Size(670, 673);
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
        private Button receiveconfig_btn;
    }
}
