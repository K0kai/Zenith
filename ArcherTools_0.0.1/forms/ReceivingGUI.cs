using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;

namespace ArcherTools_0._0._1.forms
{
    public partial class ReceivingGUI : Form
    {

        private string _title;
        private string _desc;
        private string statusDefaultText;
        public static ReceivingGUI _instance;
        internal static Form containerListForm;
        private Point mouseDownLocation;
        internal bool containerWindowEnabled = false;
        public ReceivingGUI(string title, string desc)
        {
            InitializeComponent();
            _title = title;
            _desc = desc;
            statusDefaultText = status_Label.Text;
            _instance = this;

            this.Load += onLoad;
            this.MouseDown += new MouseEventHandler(ReceivingGUI_MouseDown);
            this.MouseMove += new MouseEventHandler(ReceivingGUI_MouseMove);
        }
        private void close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            _instance = null;
        }
        private void onLoad(object sender, EventArgs e)
        {
            title_Label.Text = _title;
            description_Label.Text = _desc;
            overlayTip_lbl.Visible = false;
            this.FindForm().FormClosing += Form_closing;
            close_Btn.Location = new Point(this.FindForm().Size.Width - close_Btn.Size.Width, 0);
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() != typeof(Button))
                {
                    ctrl.MouseDown += new MouseEventHandler(ReceivingGUI_MouseDown);
                    ctrl.MouseMove += new MouseEventHandler(ReceivingGUI_MouseMove);
                }
                if (ctrl.GetType() == typeof(Panel))
                {
                    foreach (Control ctrlInCtrl in ctrl.Controls)
                    {
                        if (ctrlInCtrl.GetType() != typeof(Button))
                        {
                            ctrlInCtrl.MouseDown += new MouseEventHandler(ReceivingGUI_MouseDown);
                            ctrlInCtrl.MouseMove += new MouseEventHandler(ReceivingGUI_MouseMove);
                        }
                    }

                }
            }
        }

        internal void updateVisibility(Control ctrl, bool visibility)
        {
            try
            {
                ctrl.Visible = visibility;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ctrl.Visible = false;
            }
        }

        private void startscript_Btn_Click(object sender, EventArgs e)
        {
            bool cfgdata = Receiving.validateConfigData();
            bool excel = Receiving.validateExcel();
            bool rects = Receiving.validateRectanglePositions();
            var boxNames = new List<string> { "Container", "Release", "Owner" };
            if (cfgdata && excel && rects)
            {               
                Receiving.MainCall();
            }
            else
            {
                updateStatusLabel("Status: Failed at data validation,\nTry reconfiguring the program and trying again.");
                Debug.WriteLine("Something's not right with your receiving config.");
            }
        }





        private void ReceivingGUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Capture the mouse position when the left button is pressed
                mouseDownLocation = e.Location;
            }
        }

        private void ReceivingGUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Adjust the form's position based on the mouse movement
                var deltaX = e.X - mouseDownLocation.X;
                var deltaY = e.Y - mouseDownLocation.Y;

                this.Location = new Point(
                    this.Location.X + deltaX,
                    this.Location.Y + deltaY
                );
            }
        }
        private void itemToExcel_Btn_Click(object sender, EventArgs e)
        {

            ItemDumpingGUI itDmpGui = new ItemDumpingGUI("Item Dumping GUI", "Follow the placeholder formatting pcs/case_depth/case_width/case_height/case_weight");
            itDmpGui.Show();
        }

        private void Form_closing(object sender, FormClosingEventArgs e)
        {
            if (containerListForm != null)
            {
                containerListForm.Close();
                containerListForm.Dispose();
                containerListForm = null;
            }
        }

        public void updateStatusLabel(string text, int delay = 0)
        {
            if (delay > 0)
            {
                Thread.Sleep(delay);
            }
            status_Label.Text = text;
        }

        private async void cleanExcel_Btn_Click(object sender, EventArgs e)
        {
            updateStatusLabel("Status: Starting cleaning process");
            var configDataValidation = Receiving.validateConfigData();
            var excelDataValidation = Receiving.validateExcel();
            try
            {
                if (configDataValidation && excelDataValidation)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ReceivingConfig rcvConfig = ConfigData._receivingConfig;
                    ExcelHandler exHandler = new ExcelHandler(rcvConfig.ExcelFilePath);
                    var lastFilledRow = exHandler.GetLastFilledRow("DUMP", 3, 2);
                    var numOfFilledRows = exHandler.GetColumn("DUMP", 3, 2).Count;
                    updateStatusLabel("Status: Cleaning items 1/2");
                    Task cleanItems = Task.Run(() => { exHandler.SetColumn("DUMP", 4, new List<string>(), 2, true, lastFilledRow); });
                    Task.WaitAll(cleanItems);
                    updateStatusLabel("Status: Cleaning items 2/2");
                    Task cleanLines = Task.Run(() => { exHandler.SetColumn("DUMP", 3, new List<string>(), 2, true, lastFilledRow); });
                    Task.WaitAll(cleanLines);
                    updateStatusLabel($"Status: Cleaned {numOfFilledRows} items successfully.");
                    this.Cursor = Cursors.Default;


                }
                else { updateStatusLabel($"Status: Failure at data validation.\n{nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation}"); throw new DataException($"Failed at validations:\n{nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); }
            }
            catch (Exception ex)
            {

            }
        }

        private void viewCtn_btn_Click(object sender, EventArgs e)
        {
            ColorConfig currentPreset = ColorPresets._instance.GetCurrentPreset();
            if (currentPreset == null)
            {
                status_Label.Text = "Warning" + ": Select a theme in settings before displaying containers.";
                return;
            }
            if (ContainerListGUI._instance == null)
            {
                ContainerListGUI ctlgui = new ContainerListGUI("Container List GUI");
                ctlgui.Show();
            }
            else
            {
                ContainerListGUI._instance.Focus();
            }
            if (ContainerListGUI._instance == null)
            {
                ArcherTools_0._0._1.classes.Container.DeserializeAllContainers();
               
                containerListForm = new Form();
                containerListForm.FormBorderStyle = FormBorderStyle.None;
                containerListForm.BackColor = currentPreset.BackgroundColor;
                containerListForm.Visible = false;
                containerListForm.Size = new Size(this.FindForm().Size.Width - this.FindForm().Size.Width * 1 / 4, this.FindForm().Size.Height);
                containerListForm.MaximumSize = containerListForm.Size;
                containerListForm.MinimumSize = new Size(0, containerListForm.Size.Height);
                containerListForm.Visible = true;
                containerListForm.Location = new Point(this.FindForm().Location.X + this.FindForm().Size.Width, this.FindForm().Location.Y);
                containerListForm.TopMost = true;

                Label title = new Label();
                title.Name = "title_label";
                title.Text = "Container List";
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = currentPreset.PrimaryLabelColor;
                title.BackColor = Color.Transparent;
                title.Dock = DockStyle.Top;
                title.AutoSize = false;
                title.TextAlign = ContentAlignment.MiddleCenter;


                Panel mainPanel = new Panel();
                mainPanel.BackColor = Color.Transparent;
                mainPanel.Name = "mainPanel";
                mainPanel.Visible = true;
                mainPanel.Size = new Size(containerListForm.Size.Width - 20, containerListForm.Size.Height - (containerListForm.Size.Height * 1 / 4));
                mainPanel.Location = new Point(7, 100);
                mainPanel.BorderStyle = BorderStyle.FixedSingle;

                ListBox containerList = new ListBox();
                containerList.Name = "containerList_listbox";
                containerList.Size = new Size((int)Math.Ceiling(containerListForm.Size.Width * 0.75), (int)Math.Ceiling(containerListForm.Size.Height * 0.4));
                containerList.Visible = true;
                containerList.Location = new Point(mainPanel.Location.X + (int)Math.Round(mainPanel.Location.X * 2.2), mainPanel.Location.Y - (int)Math.Round(mainPanel.Location.Y * 0.8));
                containerList.BackColor = currentPreset.InputBoxColor;
                containerList.ForeColor = currentPreset.TextColor;
                containerList.MouseClick += containerList_Click;

                Button closeButton = new Button();
                closeButton.Name = "closeButton_btn";
                closeButton.BackColor = Color.FromArgb(184, 44, 95);
                closeButton.ForeColor = currentPreset.TextColor;
                closeButton.Text = "X";
                closeButton.Size = new Size(22, 23);
                closeButton.Location = new Point(containerListForm.Size.Width - closeButton.Size.Width, 0);
                closeButton.Visible = true;
                closeButton.FlatStyle = FlatStyle.Flat;
                closeButton.FlatAppearance.BorderSize = 0;

                Button addContainer = new Button();
                addContainer.Name = "addCtn_btn";
                addContainer.BackColor = currentPreset.ButtonColor; addContainer.ForeColor = currentPreset.TextColor;
                addContainer.Text = "+";
                addContainer.Font = new Font("Segoe UI", 7.5f);
                addContainer.Size = new Size(22, 23);
                addContainer.Location = new Point((int)Math.Round((containerList.Size.Width / 2.0) * 0.25), containerList.Size.Height + addContainer.Size.Height);
                addContainer.FlatAppearance.BorderSize = 0;
                addContainer.FlatStyle = FlatStyle.Flat;
                addContainer.AutoSize = true;
                addContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                addContainer.FlatAppearance.MouseOverBackColor = Color.YellowGreen;
                addContainer.Cursor = Cursors.Hand;
                addContainer.MouseClick += addContainer_Clicked;

                Button delContainer = new Button();
                delContainer.Name = "delCtn_btn";
                delContainer.BackColor = currentPreset.ButtonColor; delContainer.ForeColor = currentPreset.TextColor;
                delContainer.Text = "-";
                delContainer.Font = new Font("Segoe UI", 7.5f);
                delContainer.Size = new Size(32, 23);
                delContainer.Location = new Point(containerList.Size.Width, containerList.Size.Height + delContainer.Size.Height);
                delContainer.FlatAppearance.BorderSize = 0;
                delContainer.FlatStyle = FlatStyle.Flat;
                delContainer.AutoSize = true;
                delContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                delContainer.FlatAppearance.MouseOverBackColor = Color.IndianRed;
                delContainer.Cursor = Cursors.Hand;
                delContainer.Click += delContainer_Clicked;

                Label selectedContainer = new Label();
                selectedContainer.Name = "selectCtn_lbl";
                selectedContainer.BackColor = Color.Transparent;
                selectedContainer.AutoSize = true;
                selectedContainer.Text = "Selection: Nothing";
                selectedContainer.Font = new Font("Segoe UI", 8.5f);
                selectedContainer.ForeColor = currentPreset.TextColor;
                selectedContainer.Location = new Point((int)Math.Round(addContainer.Size.Width * 2.25), delContainer.Location.Y + selectedContainer.Size.Height);



                containerListForm.Controls.Add(title);
                containerListForm.Controls.Add(mainPanel);
                containerListForm.Controls.Add(closeButton);
                mainPanel.Controls.Add(selectedContainer);
                containerListForm.Invalidated += ContainerListForm_Invalidated;
                containerListForm.VisibleChanged += containerList_ShownOrVisible;
                containerListForm.Shown += containerList_ShownOrVisible;
                mainPanel.Controls.Add(addContainer);
                mainPanel.Controls.Add(delContainer);

                mainPanel.Controls.Add(containerList);
                closeButton.BringToFront();
                containerWindowEnabled = true;


            }
            else
            {
                if (containerWindowEnabled)
                {
                    containerListForm.Visible = false;
                    containerWindowEnabled = false;
                }
                else
                {
                    containerListForm.Visible = true;
                    containerWindowEnabled = true;
                }
            }
        }

        private void ContainerListForm_Invalidated(object? sender, InvalidateEventArgs e)
        {
            UpdateContainerList();
            UpdateSelectedContainer();
        }

        private void containerList_ShownOrVisible(object? sender, EventArgs e)
        {
            if (containerListForm != null)
            {
                if (containerListForm.Visible)
                {
                    containerListForm.Invalidate();
                }
            }
        }

        private void receivingGUI_WindowMoved(object sender, EventArgs e)
        {
            if (containerListForm != null)
            {
                containerListForm.Location = containerListForm.Location = new Point(this.FindForm().Location.X + this.FindForm().Size.Width, this.FindForm().Location.Y);
                Thread.Sleep(10);
            }
        }

        private static void UpdateSelectedContainer()
        {
            if (containerListForm != null)
            {
                Control[] findlbl = containerListForm.Controls.Find("selectCtn_lbl", true);
                Label selectedLabel = (Label)findlbl[0];
                if (ArcherTools_0._0._1.classes.Container.SelectedContainer != null)
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": {ArcherTools_0._0._1.classes.Container.SelectedContainer.ToString()}";
                }
                else
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": Nothing.";
                }
            }
        }


        private static void UpdateContainerList()
        {
            if (containerListForm != null)
            {
                Control[] FindBox = containerListForm.Controls.Find("containerList_listbox", true);
                ListBox listBox = (ListBox) FindBox[0];
                if (ArcherTools_0._0._1.classes.Container.AllContainers == null)
                {
                    return;
                }
                listBox.Items.Clear();
                foreach(var cont in ArcherTools_0._0._1.classes.Container.AllContainers)
                {
                    if(!listBox.Items.Contains(cont)){
                        listBox.Items.Add(cont);
                    }
                }
            }
        }

        private void containerList_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender.GetType() == typeof(ListBox))
                {
                    var listbox = (ListBox) sender;
                    if (listbox.SelectedItem != null)
                    {
                        ArcherTools_0._0._1.classes.Container.SetSelectedContainer((Container)listbox.SelectedItem);
                        Debug.WriteLine(ArcherTools_0._0._1.classes.Container.SelectedContainer.ToString());
                    }
                }
            }
        }

        private void addContainer_Clicked(object sender, EventArgs e)
        {
            var boxNames = new List<string> { "Container", "Release", "Owner" };
            List<String> dibf = DynamicInputBoxForm.Show("Please enter the container code, release (in number) and owner", boxNames, true);
            var isValid = true;

            if (dibf != null && dibf.Count == 3)
            {
                foreach (var thing in dibf)
                {
                    if (boxNames.Contains(thing))
                    {
                        isValid = false;
                    }
                }
                if (isValid)
                {
                    ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> releasesAndItems = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>();
                    ConcurrentDictionary<int, Item> itemList = new ConcurrentDictionary<int, Item>();
                    releasesAndItems.TryAdd(int.Parse(dibf[1]), itemList);
                    Container newCtn = new Container(dibf[0], releasesAndItems);
                    ArcherTools_0._0._1.classes.Container.AddContainer(newCtn);
                    newCtn.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, newCtn.ContainerId));
                    UpdateContainerList();
                }
            }
        }

        private async void delContainer_Clicked(object sender, EventArgs e)
        {
            Control[] FindBox = containerListForm.Controls.Find("containerList_listbox", true);
            ListBox listBox = (ListBox)FindBox[0];         
           
                if (ArcherTools_0._0._1.classes.Container.SelectedContainer != null)
                {
                var currentSelectedContainer = ArcherTools_0._0._1.classes.Container.SelectedContainer;
                DialogResult dr = MessageBox.Show($"Are you sure you want to delete {currentSelectedContainer.ToString()}?\nThis action is irreversible.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    await ArcherTools_0._0._1.classes.Container.DeleteContainerFileAsync(currentSelectedContainer);
                    ArcherTools_0._0._1.classes.Container.RemoveContainer(currentSelectedContainer);
                    containerListForm.Invalidate();
                }
            }
        }
    }
}

