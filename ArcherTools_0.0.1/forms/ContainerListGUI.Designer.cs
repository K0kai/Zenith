namespace ArcherTools_0._0._1.forms
{
    partial class ContainerListGUI
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
            title_Label = new Label();
            panel1 = new Panel();
            viewItems_btn = new Button();
            viewCfg_btn = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            filter_panel = new Panel();
            filter_lbl = new Label();
            empty_filter_rdBtn = new RadioButton();
            complete_filter_rdBtn = new RadioButton();
            incomplete_filter_rdBtn = new RadioButton();
            getEmail_btn = new Button();
            generateReport_Btn = new Button();
            progress_lbl = new Label();
            size_lbl = new Label();
            status_lbl = new Label();
            release_cbbox = new ComboBox();
            release_lbl = new Label();
            delCtn_btn = new Button();
            addCtn_btn = new Button();
            selectCtn_lbl = new Label();
            containerList_listbox = new ListBox();
            obsolete_Tooltip = new ToolTip(components);
            success_Tooltip = new ToolTip(components);
            containerMenuStrip = new ContextMenuStrip(components);
            toExcelToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            itemsToolStripMenuItem = new ToolStripMenuItem();
            configurationsToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            getArraySizeToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            filter_panel.SuspendLayout();
            containerMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // title_Label
            // 
            title_Label.Dock = DockStyle.Top;
            title_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_Label.ForeColor = Color.Lavender;
            title_Label.Location = new Point(0, 0);
            title_Label.Name = "title_Label";
            title_Label.Size = new Size(370, 30);
            title_Label.TabIndex = 8;
            title_Label.Text = "Title";
            title_Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(viewItems_btn);
            panel1.Controls.Add(viewCfg_btn);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 345);
            panel1.Name = "panel1";
            panel1.Size = new Size(370, 81);
            panel1.TabIndex = 10;
            // 
            // viewItems_btn
            // 
            viewItems_btn.AutoSize = true;
            viewItems_btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            viewItems_btn.BackColor = SystemColors.ControlDarkDark;
            viewItems_btn.BackgroundImageLayout = ImageLayout.None;
            viewItems_btn.Cursor = Cursors.Hand;
            viewItems_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            viewItems_btn.FlatAppearance.BorderSize = 0;
            viewItems_btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
            viewItems_btn.FlatStyle = FlatStyle.Flat;
            viewItems_btn.ForeColor = SystemColors.ButtonFace;
            viewItems_btn.Location = new Point(3, 47);
            viewItems_btn.Margin = new Padding(25, 3, 25, 3);
            viewItems_btn.Name = "viewItems_btn";
            viewItems_btn.Size = new Size(74, 25);
            viewItems_btn.TabIndex = 16;
            viewItems_btn.Text = "View Items";
            viewItems_btn.UseVisualStyleBackColor = false;
            // 
            // viewCfg_btn
            // 
            viewCfg_btn.AutoSize = true;
            viewCfg_btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            viewCfg_btn.BackColor = SystemColors.ControlDarkDark;
            viewCfg_btn.BackgroundImageLayout = ImageLayout.None;
            viewCfg_btn.Cursor = Cursors.Hand;
            viewCfg_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            viewCfg_btn.FlatAppearance.BorderSize = 0;
            viewCfg_btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
            viewCfg_btn.FlatStyle = FlatStyle.Flat;
            viewCfg_btn.ForeColor = SystemColors.ButtonFace;
            viewCfg_btn.Location = new Point(3, 16);
            viewCfg_btn.Margin = new Padding(25, 3, 25, 3);
            viewCfg_btn.Name = "viewCfg_btn";
            viewCfg_btn.Size = new Size(124, 25);
            viewCfg_btn.TabIndex = 15;
            viewCfg_btn.Text = "View Configurations";
            viewCfg_btn.UseVisualStyleBackColor = false;
            viewCfg_btn.Click += viewCfg_btn_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(delCtn_btn);
            panel2.Controls.Add(addCtn_btn);
            panel2.Controls.Add(selectCtn_lbl);
            panel2.Controls.Add(containerList_listbox);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(370, 275);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(filter_panel);
            panel3.Controls.Add(getEmail_btn);
            panel3.Controls.Add(generateReport_Btn);
            panel3.Controls.Add(progress_lbl);
            panel3.Controls.Add(size_lbl);
            panel3.Controls.Add(status_lbl);
            panel3.Controls.Add(release_cbbox);
            panel3.Controls.Add(release_lbl);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(158, 19);
            panel3.Name = "panel3";
            panel3.Size = new Size(212, 256);
            panel3.TabIndex = 11;
            // 
            // filter_panel
            // 
            filter_panel.Controls.Add(filter_lbl);
            filter_panel.Controls.Add(empty_filter_rdBtn);
            filter_panel.Controls.Add(complete_filter_rdBtn);
            filter_panel.Controls.Add(incomplete_filter_rdBtn);
            filter_panel.Location = new Point(5, 120);
            filter_panel.Name = "filter_panel";
            filter_panel.Size = new Size(204, 89);
            filter_panel.TabIndex = 19;
            // 
            // filter_lbl
            // 
            filter_lbl.AutoSize = true;
            filter_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            filter_lbl.ForeColor = Color.WhiteSmoke;
            filter_lbl.Location = new Point(63, 0);
            filter_lbl.Name = "filter_lbl";
            filter_lbl.Size = new Size(60, 17);
            filter_lbl.TabIndex = 20;
            filter_lbl.Text = "Filter by:";
            filter_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // empty_filter_rdBtn
            // 
            empty_filter_rdBtn.AutoSize = true;
            empty_filter_rdBtn.ForeColor = Color.WhiteSmoke;
            empty_filter_rdBtn.Location = new Point(3, 21);
            empty_filter_rdBtn.Name = "empty_filter_rdBtn";
            empty_filter_rdBtn.Size = new Size(59, 19);
            empty_filter_rdBtn.TabIndex = 2;
            empty_filter_rdBtn.TabStop = true;
            empty_filter_rdBtn.Text = "Empty";
            empty_filter_rdBtn.UseVisualStyleBackColor = true;
            empty_filter_rdBtn.CheckedChanged += radiobutton_CheckedChanged;
            // 
            // complete_filter_rdBtn
            // 
            complete_filter_rdBtn.AutoSize = true;
            complete_filter_rdBtn.ForeColor = Color.WhiteSmoke;
            complete_filter_rdBtn.Location = new Point(3, 67);
            complete_filter_rdBtn.Name = "complete_filter_rdBtn";
            complete_filter_rdBtn.Size = new Size(77, 19);
            complete_filter_rdBtn.TabIndex = 1;
            complete_filter_rdBtn.TabStop = true;
            complete_filter_rdBtn.Text = "Complete";
            complete_filter_rdBtn.UseVisualStyleBackColor = true;
            complete_filter_rdBtn.CheckedChanged += radiobutton_CheckedChanged;
            // 
            // incomplete_filter_rdBtn
            // 
            incomplete_filter_rdBtn.AutoSize = true;
            incomplete_filter_rdBtn.ForeColor = Color.WhiteSmoke;
            incomplete_filter_rdBtn.Location = new Point(3, 45);
            incomplete_filter_rdBtn.Name = "incomplete_filter_rdBtn";
            incomplete_filter_rdBtn.Size = new Size(85, 19);
            incomplete_filter_rdBtn.TabIndex = 0;
            incomplete_filter_rdBtn.TabStop = true;
            incomplete_filter_rdBtn.Text = "Incomplete";
            incomplete_filter_rdBtn.UseVisualStyleBackColor = true;
            incomplete_filter_rdBtn.CheckedChanged += radiobutton_CheckedChanged;
            // 
            // getEmail_btn
            // 
            getEmail_btn.AutoSize = true;
            getEmail_btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            getEmail_btn.BackColor = SystemColors.ControlDarkDark;
            getEmail_btn.BackgroundImageLayout = ImageLayout.None;
            getEmail_btn.Cursor = Cursors.Hand;
            getEmail_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            getEmail_btn.FlatAppearance.BorderSize = 0;
            getEmail_btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
            getEmail_btn.FlatStyle = FlatStyle.Flat;
            getEmail_btn.ForeColor = SystemColors.ButtonFace;
            getEmail_btn.Location = new Point(68, 215);
            getEmail_btn.Margin = new Padding(25, 3, 25, 3);
            getEmail_btn.Name = "getEmail_btn";
            getEmail_btn.Size = new Size(46, 25);
            getEmail_btn.TabIndex = 18;
            getEmail_btn.Text = "Email";
            getEmail_btn.UseVisualStyleBackColor = false;
            getEmail_btn.Click += getEmail_btn_Click;
            // 
            // generateReport_Btn
            // 
            generateReport_Btn.AutoSize = true;
            generateReport_Btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            generateReport_Btn.BackColor = SystemColors.ControlDarkDark;
            generateReport_Btn.BackgroundImageLayout = ImageLayout.None;
            generateReport_Btn.Cursor = Cursors.Hand;
            generateReport_Btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            generateReport_Btn.FlatAppearance.BorderSize = 0;
            generateReport_Btn.FlatAppearance.MouseOverBackColor = SystemColors.ControlDark;
            generateReport_Btn.FlatStyle = FlatStyle.Flat;
            generateReport_Btn.ForeColor = SystemColors.ButtonFace;
            generateReport_Btn.Location = new Point(5, 215);
            generateReport_Btn.Margin = new Padding(25, 3, 25, 3);
            generateReport_Btn.Name = "generateReport_Btn";
            generateReport_Btn.Size = new Size(52, 25);
            generateReport_Btn.TabIndex = 17;
            generateReport_Btn.Text = "Report";
            generateReport_Btn.UseVisualStyleBackColor = false;
            generateReport_Btn.Click += generateReport_Btn_Click;
            // 
            // progress_lbl
            // 
            progress_lbl.AutoSize = true;
            progress_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            progress_lbl.ForeColor = Color.WhiteSmoke;
            progress_lbl.Location = new Point(3, 90);
            progress_lbl.Name = "progress_lbl";
            progress_lbl.Size = new Size(64, 17);
            progress_lbl.TabIndex = 16;
            progress_lbl.Text = "Progress:";
            progress_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // size_lbl
            // 
            size_lbl.AutoSize = true;
            size_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            size_lbl.ForeColor = Color.WhiteSmoke;
            size_lbl.Location = new Point(3, 64);
            size_lbl.Name = "size_lbl";
            size_lbl.Size = new Size(34, 17);
            size_lbl.TabIndex = 15;
            size_lbl.Text = "Size:";
            size_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // status_lbl
            // 
            status_lbl.AutoSize = true;
            status_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            status_lbl.ForeColor = Color.WhiteSmoke;
            status_lbl.Location = new Point(3, 37);
            status_lbl.Name = "status_lbl";
            status_lbl.Size = new Size(53, 17);
            status_lbl.TabIndex = 14;
            status_lbl.Text = "Status: ";
            status_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // release_cbbox
            // 
            release_cbbox.BackColor = SystemColors.Window;
            release_cbbox.FlatStyle = FlatStyle.Flat;
            release_cbbox.FormattingEnabled = true;
            release_cbbox.Location = new Point(65, 10);
            release_cbbox.Name = "release_cbbox";
            release_cbbox.Size = new Size(49, 23);
            release_cbbox.TabIndex = 13;
            // 
            // release_lbl
            // 
            release_lbl.AutoSize = true;
            release_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            release_lbl.ForeColor = Color.WhiteSmoke;
            release_lbl.Location = new Point(3, 11);
            release_lbl.Name = "release_lbl";
            release_lbl.Size = new Size(56, 17);
            release_lbl.TabIndex = 12;
            release_lbl.Text = "Release:";
            release_lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // delCtn_btn
            // 
            delCtn_btn.AutoSize = true;
            delCtn_btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            delCtn_btn.BackColor = SystemColors.ControlDarkDark;
            delCtn_btn.BackgroundImageLayout = ImageLayout.None;
            delCtn_btn.Cursor = Cursors.Hand;
            delCtn_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            delCtn_btn.FlatAppearance.BorderSize = 0;
            delCtn_btn.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            delCtn_btn.FlatStyle = FlatStyle.Flat;
            delCtn_btn.ForeColor = SystemColors.ButtonFace;
            delCtn_btn.Location = new Point(118, 234);
            delCtn_btn.Margin = new Padding(25, 3, 25, 3);
            delCtn_btn.Name = "delCtn_btn";
            delCtn_btn.Size = new Size(22, 25);
            delCtn_btn.TabIndex = 14;
            delCtn_btn.Text = "-";
            delCtn_btn.UseVisualStyleBackColor = false;
            // 
            // addCtn_btn
            // 
            addCtn_btn.AutoSize = true;
            addCtn_btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addCtn_btn.BackColor = SystemColors.ControlDarkDark;
            addCtn_btn.BackgroundImageLayout = ImageLayout.None;
            addCtn_btn.Cursor = Cursors.Hand;
            addCtn_btn.FlatAppearance.BorderColor = SystemColors.GradientInactiveCaption;
            addCtn_btn.FlatAppearance.BorderSize = 0;
            addCtn_btn.FlatAppearance.MouseOverBackColor = Color.OliveDrab;
            addCtn_btn.FlatStyle = FlatStyle.Flat;
            addCtn_btn.ForeColor = SystemColors.ButtonFace;
            addCtn_btn.Location = new Point(13, 234);
            addCtn_btn.Margin = new Padding(25, 3, 25, 3);
            addCtn_btn.Name = "addCtn_btn";
            addCtn_btn.Size = new Size(25, 25);
            addCtn_btn.TabIndex = 13;
            addCtn_btn.Text = "+";
            addCtn_btn.UseVisualStyleBackColor = false;
            // 
            // selectCtn_lbl
            // 
            selectCtn_lbl.Dock = DockStyle.Top;
            selectCtn_lbl.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            selectCtn_lbl.ForeColor = Color.LightGray;
            selectCtn_lbl.Location = new Point(0, 0);
            selectCtn_lbl.Name = "selectCtn_lbl";
            selectCtn_lbl.Size = new Size(370, 19);
            selectCtn_lbl.TabIndex = 12;
            selectCtn_lbl.Text = "Selected: None";
            selectCtn_lbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // containerList_listbox
            // 
            containerList_listbox.FormattingEnabled = true;
            containerList_listbox.ItemHeight = 15;
            containerList_listbox.Location = new Point(3, 29);
            containerList_listbox.Name = "containerList_listbox";
            containerList_listbox.Size = new Size(149, 199);
            containerList_listbox.TabIndex = 0;
            // 
            // obsolete_Tooltip
            // 
            obsolete_Tooltip.AutomaticDelay = 250;
            obsolete_Tooltip.ToolTipIcon = ToolTipIcon.Warning;
            obsolete_Tooltip.ToolTipTitle = "Attention";
            // 
            // success_Tooltip
            // 
            success_Tooltip.AutomaticDelay = 100;
            success_Tooltip.ToolTipIcon = ToolTipIcon.Info;
            success_Tooltip.ToolTipTitle = "Success";
            // 
            // containerMenuStrip
            // 
            containerMenuStrip.BackColor = Color.FromArgb(48, 48, 48);
            containerMenuStrip.Items.AddRange(new ToolStripItem[] { toExcelToolStripMenuItem, clearToolStripMenuItem, deleteToolStripMenuItem, getArraySizeToolStripMenuItem });
            containerMenuStrip.Name = "containerMenuStrip";
            containerMenuStrip.RenderMode = ToolStripRenderMode.Professional;
            containerMenuStrip.Size = new Size(159, 92);
            // 
            // toExcelToolStripMenuItem
            // 
            toExcelToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            toExcelToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            toExcelToolStripMenuItem.Image = Properties.Resources.excel;
            toExcelToolStripMenuItem.Name = "toExcelToolStripMenuItem";
            toExcelToolStripMenuItem.Size = new Size(158, 22);
            toExcelToolStripMenuItem.Text = "To Worksheet";
            toExcelToolStripMenuItem.Click += toExcelToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            clearToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemsToolStripMenuItem, configurationsToolStripMenuItem });
            clearToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            clearToolStripMenuItem.Image = Properties.Resources.bin;
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(158, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.ToolTipText = "This action is irreversible.";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // itemsToolStripMenuItem
            // 
            itemsToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            itemsToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            itemsToolStripMenuItem.Size = new Size(153, 22);
            itemsToolStripMenuItem.Text = "Items";
            itemsToolStripMenuItem.Click += itemsToolStripMenuItem_Click;
            // 
            // configurationsToolStripMenuItem
            // 
            configurationsToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            configurationsToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            configurationsToolStripMenuItem.Name = "configurationsToolStripMenuItem";
            configurationsToolStripMenuItem.Size = new Size(153, 22);
            configurationsToolStripMenuItem.Text = "Configurations";
            configurationsToolStripMenuItem.Click += configurationsToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            deleteToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            deleteToolStripMenuItem.Image = Properties.Resources.delete;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Del";
            deleteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
            deleteToolStripMenuItem.Size = new Size(158, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.ToolTipText = "This action is irreversible.";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            deleteToolStripMenuItem.MouseEnter += ToolStripMenuItem_MouseEnter;
            deleteToolStripMenuItem.MouseLeave += ToolStripMenuItem_MouseLeave;
            // 
            // getArraySizeToolStripMenuItem
            // 
            getArraySizeToolStripMenuItem.BackColor = Color.FromArgb(64, 64, 64);
            getArraySizeToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            getArraySizeToolStripMenuItem.Name = "getArraySizeToolStripMenuItem";
            getArraySizeToolStripMenuItem.Size = new Size(158, 22);
            getArraySizeToolStripMenuItem.Text = "Get Array Size";
            getArraySizeToolStripMenuItem.Click += getArraySizeToolStripMenuItem_Click;
            // 
            // ContainerListGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(370, 426);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(title_Label);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ContainerListGUI";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "ContainerListGUI";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            filter_panel.ResumeLayout(false);
            filter_panel.PerformLayout();
            containerMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label title_Label;
        private Panel panel1;
        private Panel panel2;
        private ListBox containerList_listbox;
        private Label selectCtn_lbl;
        private Button delCtn_btn;
        private Button addCtn_btn;
        private Panel panel3;
        private Label release_lbl;
        private Label size_lbl;
        private Label status_lbl;
        private ComboBox release_cbbox;
        private Label progress_lbl;
        private Button viewItems_btn;
        private Button viewCfg_btn;
        private ToolTip obsolete_Tooltip;
        private ToolTip success_Tooltip;
        private Button generateReport_Btn;
        private Button getEmail_btn;
        private ContextMenuStrip containerMenuStrip;
        private ToolStripMenuItem toExcelToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem itemsToolStripMenuItem;
        private ToolStripMenuItem configurationsToolStripMenuItem;
        private ToolStripMenuItem getArraySizeToolStripMenuItem;
        private Panel filter_panel;
        private RadioButton empty_filter_rdBtn;
        private RadioButton complete_filter_rdBtn;
        private RadioButton incomplete_filter_rdBtn;
        private Label filter_lbl;
    }
}