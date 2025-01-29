using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;
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
        }

        private void onLoad(object sender, EventArgs e)
        {
            title_Label.Text = _title;
            description_Label.Text = _desc;
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

        private void startscript_Btn_Click(object sender, EventArgs e)
        {
            bool cfgdata = Receiving.validateConfigData();
            bool excel = Receiving.validateExcel();
            bool rects = Receiving.validateRectanglePositions();
            if (cfgdata && excel && rects)
            {
                List<String> dibf = DynamicInputBoxForm.Show("Please enter the container code, release (in number) and owner", new List<string> { "Container", "Release", "Owner" });
                //if (dibf != null && dibf.Count == 3)
               // {
                    //ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> releasesAndItems = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>();
                   // ConcurrentDictionary<int, Item> itemList = new ConcurrentDictionary<int, Item>();
                   // releasesAndItems.TryAdd(int.Parse(dibf[1]), itemList);
                    //Container newCtn = new Container(dibf[0], releasesAndItems);
               // }
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
           if (containerListForm == null)
            {
                ColorConfig currentPreset = ColorPresets._instance.GetCurrentPreset();
                if (currentPreset == null)
                {
                    status_Label.Text = "Warning" + ": Select a theme in settings before displaying containers.";
                    return;
                }
                containerListForm = new Form();                
                containerListForm.FormBorderStyle = FormBorderStyle.None;
                containerListForm.BackColor = currentPreset.BackgroundColor;
                containerListForm.Visible = true;
                containerListForm.Size = this.FindForm().Size;
                containerListForm.Location = new Point(this.FindForm().Location.X + this.FindForm().Size.Width, this.FindForm().Location.Y);

                Label title = new Label();
                title.Text = "Container List";
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = currentPreset.PrimaryLabelColor;
                title.BackColor = Color.Transparent;
                title.Dock = DockStyle.Top;
                title.AutoSize = false;
                title.TextAlign = ContentAlignment.MiddleCenter;
                containerListForm.Controls.Add(title);

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
