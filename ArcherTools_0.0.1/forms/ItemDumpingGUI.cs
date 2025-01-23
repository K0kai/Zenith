using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;
using System.Data;
using System.Diagnostics;

namespace ArcherTools_0._0._1.forms
{
    public partial class ItemDumpingGUI : Form
    {
        private string _title;
        private string _description;
        private int _lines;
        private Point mouseDownLocation;
        internal static string previousText;

        public ItemDumpingGUI(string title, string desc)
        {
            InitializeComponent();
            this.BackColor = ToolHub._mainForm.BackColor;
            title_Label.TextChanged += labelTextChanged;
            description_Label.TextChanged -= labelTextChanged;
            _title = title;
            _description = desc;
            
            
            this.MouseDown += new MouseEventHandler(ItemDumpGUI_MouseDown);
            this.MouseMove += new MouseEventHandler(ItemDumpGUI_MouseMove);
            this.Load += onLoad;
            this.FormClosed += onClose;

            this.itemValues_Box.TextChanged += textBoxChanged;
            centerLabels();


        }

        private void onClose(object sender, EventArgs e)
        {
            previousText = itemValues_Box.Text;
        }
        private void onLoad(object sender, EventArgs e)
        {
            title_Label.Text = _title;
            description_Label.Text = _description;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() != typeof(Button) && ctrl.GetType() != typeof(TextBox))
                {
                    ctrl.MouseDown += new MouseEventHandler(ItemDumpGUI_MouseDown);
                    ctrl.MouseMove += new MouseEventHandler(ItemDumpGUI_MouseMove);
                }
                if (ctrl.GetType() == typeof(Panel))
                {
                    foreach (Control ctrlInCtrl in ctrl.Controls)
                    {
                        if (ctrlInCtrl.GetType() != typeof(Button) && ctrlInCtrl.GetType() != typeof(TextBox))
                        {
                            ctrlInCtrl.MouseDown += new MouseEventHandler(ItemDumpGUI_MouseDown);
                            ctrlInCtrl.MouseMove += new MouseEventHandler(ItemDumpGUI_MouseMove);
                        }
                    }

                }
            }
            if (previousText != null)
            {
                itemValues_Box.Text = previousText;
            }
        }
        

        private void labelTextChanged(object sender, EventArgs e)
        {
            centerLabels();
        }
        private void centerLabels()
        {
            title_Label.Location = new Point(this.Width / 2, title_Label.Location.Y);
            description_Label.Location = new Point(this.Width / 2, description_Label.Location.Y);

        }

        private void ItemDumpGUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Capture the mouse position when the left button is pressed
                mouseDownLocation = e.Location;
            }
        }

        private void ItemDumpGUI_MouseMove(object sender, MouseEventArgs e)
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

        private void close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void textBoxChanged(object sender, EventArgs e)
        {
            var count = 0;
            _lines = 0;
            var textBox = sender as TextBox;
            string[] lines = textBox.Lines;
            List<String> cleanLines = lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
            foreach (string line in cleanLines)
            {
              count++;
             _lines++;
            }
        this.lines_label.Text = $"Lines: {count}";          
            
        }

        private List<String> itemSeparation()
        {
            string[] lines = this.itemValues_Box.Lines;
            List<String> cleanLines = lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
            return cleanLines;
        } 
        
        private List<int> createListFromCount<T>(List<T> list)
        {
            var count = list.Count + 1;
            List<int> newList = new List<int>();
            for (int i = 1; i < count; i++)
            {                
                newList.Add(i);
            }
            return newList;
        }

        private void sendItems_btn_Click(object sender, EventArgs e)
        {
            try
            {
                ReceivingGUI._instance.updateStatusLabel("Status: Attempting to send items now...");
                Debug.WriteLine("begin");
                List<String> values = itemSeparation();
                List<int> lines = createListFromCount(values);
                var configDataValidation = Receiving.validateConfigData();
                var excelDataValidation = Receiving.validateExcel();
                if (configDataValidation && excelDataValidation)
                {
                    Debug.WriteLine("check success");
                    ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                    ExcelHandler excelHandler = new ExcelHandler(rcvCfg.ExcelFilePath);
                    if (rcvCfg.ExcelSheetNames.Contains("DUMP"))
                    {
                        var workSheetName = "DUMP";
                        Task setValues = Task.Run(() =>
                        {
                            excelHandler.SetColumn(workSheetName, 4, values, 2);
                        });
                        Task.WaitAll(setValues);
                        Task setLines = Task.Run(() => { excelHandler.SetColumn(workSheetName, 3, lines, 2); });
                        Task.WaitAll(setLines);
                        ReceivingGUI._instance.updateStatusLabel("Status: Success");
                        Debug.WriteLine("done");
                    }
                }
                else { ReceivingGUI._instance.updateStatusLabel($"Status: Failed at data validation: {nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); throw new Exception($"Failed at checks: {nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); }
            }
            catch (Exception ex)
            {
                ReceivingGUI._instance.updateStatusLabel($"Status: Failed to send items with error: {ex.StackTrace} || {ex.Message}");
                Debug.WriteLine($"Something went wrong at item translocation\n{ex.StackTrace}\n{ex.Message}");
            }
        }
    }
}
