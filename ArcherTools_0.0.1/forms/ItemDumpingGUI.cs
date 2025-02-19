using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ArcherTools_0._0._1.forms
{
    public partial class ItemDumpingGUI : Form
    {
        private string _title;
        private string _description;
        private int _lines;
        private int _selectedLine;
        private Point mouseDownLocation;
        internal static string previousText;
        internal static Form sendToCtnForm;
        

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
            this.itemValues_Box.MouseClick += textBoxSelectionChangedClick;
            this.itemValues_Box.KeyUp += textBoxSelectionChangedKeyPress;
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

        private int getSelectedLine()
        {
            var currentLine = itemValues_Box.GetLineFromCharIndex(itemValues_Box.SelectionStart);
            return currentLine;
        }

        private void textBoxSelectionChangedClick(object sender, EventArgs e)
        {
            selectedLn_Label.Text = $"Selected Line: {getSelectedLine() + 1}";
        }

        private void textBoxSelectionChangedKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                selectedLn_Label.Text = $"Selected Line: {getSelectedLine() + 1}";
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
            this.selectedLn_Label.Text = $"Selected Line: {getSelectedLine() + 1}";

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

        private void sendToCtn_btn_Click(object sender, EventArgs e)
        {
            ColorConfig currentPreset = ColorPresets._instance.SelectedPreset;
            if (currentPreset != null)
            {
                Form selectCtnForm = new Form();
                selectCtnForm.StartPosition = FormStartPosition.CenterParent;
                selectCtnForm.BackColor = currentPreset.BackgroundColor;
                selectCtnForm.Size = new Size(250, 100);
                selectCtnForm.FormBorderStyle = FormBorderStyle.None;
                selectCtnForm.Name = "Pick a Container";
                selectCtnForm.TopMost = true;

                Label title = new Label();
                title.Name = "title_Lbl";
                title.BackColor = Color.Transparent;
                title.ForeColor = currentPreset.PrimaryLabelColor;
                title.Text = "Choose the Container:";
                title.Dock = DockStyle.Top;
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.AutoSize = false;
                title.TextAlign = ContentAlignment.MiddleCenter;

                ComboBox ctnComboBox = new ComboBox();
                ctnComboBox.Name = "container_ComboBox";
                ctnComboBox.Size = new Size(115, 20);
                ctnComboBox.Location = new Point((int)Math.Round(selectCtnForm.Size.Width * 0.2), (int)Math.Round(selectCtnForm.Size.Height * 0.3));
                ctnComboBox.FlatStyle = FlatStyle.Flat;
                BindingSource ctnBs = new BindingSource();
                ctnBs.DataSource = classes.Container.AllContainers;
                ctnComboBox.DataSource = ctnBs;

                ComboBox releaseComboBox = new ComboBox();
                releaseComboBox.Name = "release_ComboBox";
                releaseComboBox.Size = new Size(49, 23);
                releaseComboBox.Location = new Point(ctnComboBox.Location.X, (int)Math.Round(ctnComboBox.Location.Y * 2.2));
                releaseComboBox.FlatStyle = FlatStyle.Flat;

                Button cancelBtn = new Button();
                cancelBtn.Text = "Cancel";
                cancelBtn.Name = "cancel_Btn";
                cancelBtn.AutoSize = true;
                cancelBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                cancelBtn.BackColor = currentPreset.ButtonColor;
                cancelBtn.FlatStyle = FlatStyle.Flat;
                cancelBtn.FlatAppearance.BorderColor = currentPreset.DetailsColor;
                cancelBtn.ForeColor = currentPreset.TextColor;
                cancelBtn.FlatAppearance.MouseOverBackColor = Color.IndianRed;
                cancelBtn.Location = new Point(selectCtnForm.Location.X + (int)Math.Round(selectCtnForm.Size.Width * 0.5), selectCtnForm.Height - cancelBtn.Height);

                Button okBtn = new Button();
                okBtn.Text = "Ok";
                okBtn.Name = "ok_Btn";
                okBtn.AutoSize = true;
                okBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                okBtn.BackColor = currentPreset.ButtonColor;
                okBtn.FlatStyle = FlatStyle.Flat;
                okBtn.FlatAppearance.BorderColor = currentPreset.DetailsColor;
                okBtn.ForeColor = currentPreset.TextColor;
                okBtn.FlatAppearance.MouseOverBackColor = Color.DarkOliveGreen;
                okBtn.Location = new Point((int)Math.Round(cancelBtn.Location.X * 1.5), cancelBtn.Location.Y);

                selectCtnForm.Controls.Add(ctnComboBox);
                selectCtnForm.Controls.Add(releaseComboBox);
                selectCtnForm.Controls.Add(title);
                selectCtnForm.Controls.Add(cancelBtn);
                selectCtnForm.Controls.Add(okBtn);
                selectCtnForm.AcceptButton = okBtn;
                selectCtnForm.CancelButton = cancelBtn;

                selectCtnForm.Load += SelectCtnForm_Load;
                selectCtnForm.Invalidated += SelectCtnForm_Invalidated;
                selectCtnForm.FormClosed += SelectCtnForm_FormClosed;
                ctnComboBox.SelectionChangeCommitted += CtnComboBox_SelectionChangeCommitted;
                releaseComboBox.SelectionChangeCommitted += ReleaseComboBox_SelectionChangeCommitted;
                okBtn.Click += OkBtn_Click;
                cancelBtn.Click += CancelBtn_Click;
                classes.Container.StaticPropertyChanged += Container_StaticPropertyChanged;


                sendToCtnForm = selectCtnForm;
                DialogResult dr = selectCtnForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    ConfigData.EnsureFolderExists();
                    var configs = new ConcurrentDictionary<int, string>();
                    var Configs = itemSeparation();
                    var Lines = createListFromCount(Configs);
                    
                    for (int i = 0; i < Lines.Count; i++)
                    {                            
                        var line = i < Lines.Count ? Lines[i] : -999;                          
                        var config = i < Configs.Count ? Configs[i] : string.Empty;                           
                       
                        configs.AddOrUpdate(line, config, (key, oldValue) => oldValue = config);                        
                    }                       
                    if (classes.Container.SelectedContainer.AttachedConfigurations.TryAdd(classes.Container.SelectedRelease, configs))                        
                    {                         
                        
                    }                       
                    else                       
                    {                           
                        if (classes.Container.SelectedContainer.AttachedConfigurations.ContainsKey(classes.Container.SelectedRelease))                           
                        {                              
                            classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease] = configs;                           
                        }                      
                    }                      
                    classes.Container.SelectedContainer.CalculateExpectedSize();                      
                    classes.Container.SelectedContainer?.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, classes.Container.SelectedContainer.ContainerId));
                   
                }
            }          

        }

        private void ReleaseComboBox_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            classes.Container.SetSelectedRelease((sender as ComboBox).SelectedItem);
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            var form = (sender as Button).FindForm();
            form.DialogResult = DialogResult.Cancel;
            form.Close(); 
        }

        private void CtnComboBox_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            classes.Container.SetSelectedContainer((classes.Container) (sender as ComboBox).SelectedItem);
            UpdateSelectedRelease(sendToCtnForm.Controls.Find("release_ComboBox", true).FirstOrDefault() as ComboBox);
            
        }

        private void SelectCtnForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            sendToCtnForm.Close();
            sendToCtnForm.Dispose();
            sendToCtnForm = null;
        }

        private void SelectCtnForm_Invalidated(object? sender, InvalidateEventArgs e)
        {
            var SelectedContainer = classes.Container.SelectedContainer;
            var SelectedRelease = classes.Container.SelectedRelease;
            Form thisForm = (sender as Form);
            if (SelectedContainer != null)
            {
                UpdateSelectedCtn(thisForm.Controls.Find("container_ComboBox", true).FirstOrDefault() as ComboBox);
                if (SelectedRelease != 0)
                {
                    
                    UpdateSelectedRelease(thisForm.Controls.Find("release_ComboBox", true).FirstOrDefault() as ComboBox);
                }
            }
        }

        private void Container_StaticPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sendToCtnForm != null)
            {
                sendToCtnForm.Invalidate();
            }
        }

        private void SelectCtnForm_Load(object? sender, EventArgs e)
        {
            (sender as Form).Invalidate();
        }

        private static void UpdateSelectedCtn(ComboBox cbBox)
        {
            cbBox.SelectedItem = classes.Container.SelectedContainer;
            cbBox.SelectedIndex = cbBox.Items.IndexOf(cbBox.SelectedItem);
        }

        private static void UpdateSelectedRelease(ComboBox cbBox)
        {
            var bs = new BindingSource();
            bs.DataSource = classes.Container.SelectedContainer.ReleasesAndItems.Keys.ToArray();
            cbBox = sendToCtnForm.Controls.Find("release_ComboBox", true).FirstOrDefault() as ComboBox;
            cbBox.DataSource = bs;
            cbBox.SelectedItem = classes.Container.SelectedRelease;
            cbBox.SelectedIndex = cbBox.Items.IndexOf(cbBox.SelectedItem);
        }
        private void OkBtn_Click(object? sender, EventArgs e)
        {
            Form thisForm = (sender as Control).FindForm();
            // ComboBox ctnCbBox = thisForm.Controls.Find("container_ComboBox", true).FirstOrDefault() as ComboBox;
            var SelectedContainer = classes.Container.SelectedContainer;
            var SelectedRelease = classes.Container.SelectedRelease;
            if (SelectedContainer != null && SelectedRelease != 0)
            {
                if (classes.Container.ValidateSelectedContainerAndRelease() != 0)
                {
                    MessageBox.Show($"Container: {SelectedContainer} Release: {SelectedRelease} don't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    thisForm.DialogResult = DialogResult.OK;
                    thisForm.Close();
                    thisForm.Dispose();
                }
            }            
        }
    }
}
