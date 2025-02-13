using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;
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
        internal bool isCleaning;
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
            this.LocationChanged += receivingGUI_WindowMoved;
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
            this.StartPosition = FormStartPosition.Manual;
            this.FormClosing += ReceivingGUI_FormClosing;
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
            if (Properties.Settings.Default.ReceivingGUILocation != new Point(0, 0))
            {
                this.FindForm().Location = Properties.Settings.Default.ReceivingGUILocation;
            }
        }

        private void ReceivingGUI_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.ReceivingGUILocation != this.Location)
            {
                Properties.Settings.Default.ReceivingGUILocation = this.Location;
                Properties.Settings.Default.Save();
            }
            
        }

        internal void updateVisibility(Control ctrl, bool visibility)
        {
            try
            {
                if (ctrl.InvokeRequired)
                {
                    ctrl.Invoke((MethodInvoker)delegate
                    {                      
                      ctrl.Visible = visibility;                
                    });
                }
                else
                {
                    ctrl.Visible = visibility;
                }
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
                var thread = new Thread(() =>
                {
                    Receiving.MainCall();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
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
            if (status_Label.InvokeRequired)
            {
                status_Label.Invoke((MethodInvoker)delegate
                {
                    status_Label.Text = text;
                });

            }
            else
            {
                status_Label.Text = text;
            }
        }

        public void setProgressBar(int value)
        {
            if (rcvProgress_pgBar.InvokeRequired)
            {
                rcvProgress_pgBar.Invoke((MethodInvoker)delegate
                {
                    rcvProgress_pgBar.Value = value > rcvProgress_pgBar.Maximum ? rcvProgress_pgBar.Maximum : value;
                });
            }
            else
            {
                rcvProgress_pgBar.Value = value > rcvProgress_pgBar.Maximum ? rcvProgress_pgBar.Maximum : value;
            }
        }

        public void incrementProgressBar(int value)
        {
            if (rcvProgress_pgBar.InvokeRequired)
            {
                rcvProgress_pgBar.Invoke((MethodInvoker)delegate
                {
                    rcvProgress_pgBar.Value += value;
                });
            }
            else
            {
                rcvProgress_pgBar.Value += value;
            }
        }

        public void setProgressBarMaximum(int value)
        {
            if (rcvProgress_pgBar.InvokeRequired)
            {
                rcvProgress_pgBar.Invoke((MethodInvoker)delegate
                {
                    rcvProgress_pgBar.Maximum = value;
                });
            }
            else
            {
                rcvProgress_pgBar.Maximum = value;
            }
        }

        private async void cleanExcel_Btn_Click(object sender, EventArgs e)
        {
            if (!isCleaning)
            {
                isCleaning = true;
                updateStatusLabel("Status: Starting cleaning process");
                var configDataValidation = Receiving.validateConfigData();
                var excelDataValidation = Receiving.validateExcel();
                var thread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;                    
                    try
                    {

                        if (configDataValidation && excelDataValidation)
                        {
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
                            lastFilledRow = lastFilledRow > numOfFilledRows ? numOfFilledRows : lastFilledRow;
                            updateStatusLabel($"Status: Cleaned {lastFilledRow}/{numOfFilledRows} items successfully.");
                        }
                        else { updateStatusLabel($"Status: Failure at data validation.\n{nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation}"); throw new DataException($"Failed at validations:\n{nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); }
                        isCleaning = false;
                    }
                    catch (Exception ex)
                    {
                        isCleaning = false;
                        Debug.WriteLine(ex.StackTrace);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
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
                ctlgui.Show(this);
                containerListForm = ContainerListGUI._instanceForm;
                containerListForm.Location = containerListForm.Location = new Point(this.FindForm().Location.X + this.FindForm().Size.Width, this.FindForm().Location.Y);
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

        private void receivingGUI_WindowMoved(object sender, EventArgs e)
        {
            if (containerListForm != null)
            {
                containerListForm.Location = containerListForm.Location = new Point(this.FindForm().Location.X + this.FindForm().Size.Width, this.FindForm().Location.Y);
                Thread.Sleep(10);
            }
        }
    }       
}

