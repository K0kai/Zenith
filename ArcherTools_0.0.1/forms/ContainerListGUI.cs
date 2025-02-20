using System.Collections.Concurrent;
using System.Diagnostics;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.logging;
using ArcherTools_0._0._1.methods;
using Org.BouncyCastle.Asn1.X500;

namespace ArcherTools_0._0._1.forms
{
    public partial class ContainerListGUI : Form
    {

        public static Form _instanceForm;
        public static Form viewCfgForm;
        public static ContainerListGUI _instance;
        private static BindingSource containerBs = new BindingSource();
        private static BindingSource releaseBs = new BindingSource();

        public ContainerListGUI(string title)
        {
            InitializeComponent();
            this.Load += ContainerListGUI_Load;
            this.title_Label.Text = title;
            this.VisibleChanged += ContainerListGUI_ShownOrVisible;
            this.Shown += ContainerListGUI_ShownOrVisible;
            this.containerList_listbox.MouseDoubleClick += containerList_Click;
            this.release_cbbox.SelectionChangeCommitted += Release_cbbox_SelectionChangeCommitted;
        }

        private void Release_cbbox_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            if (this.containerList_listbox.SelectedItem != null)
            {
                classes.Container.SetSelectedRelease(release_cbbox.SelectedItem);
            }
        }

        private void ContainerListGUI_Load(object? sender, EventArgs e)
        {
            ColorConfig currentPreset = ColorPresets._instance.GetCurrentPreset();
            _instanceForm = this.FindForm();
            _instance = this;
            _instanceForm.FormClosed += _instanceForm_FormClosed;
            this.addCtn_btn.Click += addContainer_Clicked;
            this.delCtn_btn.Click += delContainer_Clicked;
            this.Invalidated += ContainerListGUI_Invalidated;
            obsolete_Tooltip.SetToolTip(viewCfg_btn, "Experimental, can only view, not edit");
            obsolete_Tooltip.SetToolTip(viewItems_btn, "Feature not implemented yet");

            foreach (ToolStripMenuItem tsmi in containerMenuStrip.Items)
            {
                tsmi.MouseEnter += ToolStripMenuItem_MouseEnter;
                tsmi.MouseLeave += ToolStripMenuItem_MouseLeave;
            }

            Label title = title_Label;
            title.ForeColor = currentPreset.PrimaryLabelColor;
            title.BackColor = Color.Transparent;

            ListBox containerList = containerList_listbox;
            containerList.BackColor = currentPreset.InputBoxColor;
            containerList.ForeColor = currentPreset.TextColor;

            Button addContainer = addCtn_btn;
            addContainer.BackColor = currentPreset.ButtonColor;
            addContainer.ForeColor = currentPreset.TextColor;

            Button delContainer = delCtn_btn;
            delContainer.BackColor = currentPreset.ButtonColor;
            delContainer.ForeColor = currentPreset.TextColor;

            Label selectedContainer = selectCtn_lbl;
            selectedContainer.ForeColor = currentPreset.TextColor;

            containerBs.DataSource = classes.Container.AllContainers;
            classes.Container.StaticPropertyChanged += Container_StaticPropertyChanged;
            this.containerList_listbox.DataSource = containerBs;
            containerList_listbox.MouseDown += ContainerList_listbox_MouseDown;

            if (classes.Container.SelectedContainer != null)
            {
                this.containerList_listbox.SelectedItem = classes.Container.SelectedContainer;
            }

            this.Invalidate();
        }

        //feat. GPT
        private void ContainerList_listbox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var listBox = sender as ListBox;
                // Check if the mouse is over a valid item in the ListBox
                int index = listBox.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    // Set the selected index based on the right-clicked item
                    listBox.SelectedIndex = index;
                    listBox.SelectedItem = listBox.Items[index];
                    classes.Container.SetSelectedContainer((Container)listBox.SelectedItem);
                    // Show the context menu at the mouse location
                    containerMenuStrip.Show(listBox, e.Location);
                }
            }
        }
        // 

        private void SelectedContainer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ContainerStatus")
            {
                UpdateSelectedContainerStatus();
                UpdateSelectedRelease();
                refreshFilteredList();
            }
            if (e.PropertyName == "ExpectedSize")
            {
                UpdateExpectedSize();
            }
            if (e.PropertyName == "ReleasesAndItems")
            {
                UpdateContainerProgress();
            }
            Debug.WriteLine(e.PropertyName);
        }

        private void Container_StaticPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            UpdateContainerList();
            if (e.PropertyName == "SelectedContainer")
            {
                UpdateSelectedContainer();
                if (classes.Container.SelectedContainer != null)
                {
                    classes.Container.SelectedContainer.PropertyChanged += SelectedContainer_PropertyChanged;
                }
                else
                {
                    if (_instance != null)
                    {
                        _instance.release_cbbox.Text = "";
                    }
                }
                UpdateSelectedContainerStatus();
                UpdateContainerProgress();
            }
            if (e.PropertyName == "SelectedRelease")
            {
                UpdateSelectedRelease();
                classes.Container.SelectedContainer?.UpdateContainerStatus(classes.Container.SelectedRelease);
                UpdateSelectedContainerStatus();
            }
            if (e.PropertyName == "AllContainers")
            {
                refreshFilteredList();
            }
        }

        private void _instanceForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _instance.Dispose();
            _instanceForm.Dispose();
            _instance = null;
            _instanceForm = null;
        }

        private void ContainerListGUI_Invalidated(object? sender, InvalidateEventArgs e)
        {
            UpdateContainerList();
            UpdateSelectedContainer();
        }

        private static void UpdateSelectedContainerStatus()
        {
            if (_instanceForm != null)
            {
                if (classes.Container.SelectedContainer != null && classes.Container.SelectedRelease != 0)
                {
                    if (!_instance.status_lbl.InvokeRequired)
                    {
                        _instance.status_lbl.Text = _instance.status_lbl.Text.Split(':')[0] + $": {classes.Container.SelectedContainer.ContainerStatus}";
                    }
                    else
                    {
                        _instance.status_lbl.Invoke((MethodInvoker)delegate
                        {                        
                            _instance.status_lbl.Text = _instance.status_lbl.Text.Split(':')[0] + $": {classes.Container.SelectedContainer.ContainerStatus}";
                        });
                    }
                }
            }
        }

        private static void UpdateSelectedRelease()
        {
            if (_instanceForm != null)
            {
                if (classes.Container.SelectedContainer != null && classes.Container.SelectedRelease != 0)
                {
                    if (!_instance.release_cbbox.InvokeRequired)
                    {
                        _instance.release_cbbox.SelectedIndex = _instance.release_cbbox.Items.IndexOf(classes.Container.SelectedRelease);
                    }
                    else
                    {
                        _instance.release_cbbox.Invoke((MethodInvoker)delegate
                        {
                            _instance.release_cbbox.SelectedIndex = _instance.release_cbbox.Items.IndexOf(classes.Container.SelectedRelease);
                        });
                    }
                    UpdateContainerProgress();
                }
            }
        }

        private static void UpdateContainerProgress()
        {
            if (_instanceForm != null)
            {
                if (classes.Container.SelectedContainer.ValidateContainerAndRelease(classes.Container.SelectedRelease) == 0)
                {
                    var releasesAndItems = classes.Container.SelectedContainer.ReleasesAndItems[classes.Container.SelectedRelease];
                    var attachedConfigs = classes.Container.SelectedContainer.AttachedConfigurations.ContainsKey(classes.Container.SelectedRelease) ? classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease].Count : 0; ;
                    var percentage = releasesAndItems.Count != 0 && attachedConfigs != 0 ? Math.Round(((decimal)releasesAndItems.Count / (decimal)attachedConfigs) * 100, 1) : 0;
                    if (_instance.progress_lbl.InvokeRequired)
                    {
                        _instance.progress_lbl.Invoke((MethodInvoker)delegate
                        {
                            _instance.progress_lbl.Text = _instance.progress_lbl.Text.Split(':')[0] + $": {decimal.ToInt32(percentage)}%";
                        });
                    }
                    else
                    {
                        _instance.progress_lbl.Text = _instance.progress_lbl.Text.Split(':')[0] + $": {decimal.ToInt32(percentage)}%";
                    }
                }
            }
        }

        private static void UpdateExpectedSize()
        {
            if (_instanceForm != null)
            {
                if (classes.Container.SelectedContainer != null)
                {
                    _instance.size_lbl.Text = _instance.size_lbl.Text.Split(':')[0] + $": {classes.Container.SelectedContainer.ExpectedSize} Items";
                }
                else
                {
                    _instance.size_lbl.Text = _instance.size_lbl.Text.Split(':')[0] + ":";
                }
            }
        }

        private static void UpdateSelectedContainer()
        {
            if (_instanceForm != null)
            {
                Control[] findlbl = _instanceForm.Controls.Find("selectCtn_lbl", true);
                Label selectedLabel = (Label)findlbl[0];
                if (classes.Container.SelectedContainer != null)
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": {classes.Container.SelectedContainer.ToString()}";
                    var selectedContainer = classes.Container.SelectedContainer;
                    _instance.release_cbbox.Items.Clear();
                    foreach (var release in selectedContainer.ReleasesAndItems)
                    {
                        _instance.release_cbbox.Items.Add(release.Key);
                    }
                }
                else
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": Nothing.";
                    _instance.release_cbbox.Items.Clear();
                    _instance.release_cbbox.SelectedItem = null;
                    _instance.status_lbl.Text = "Status: ";

                }
                UpdateExpectedSize();
            }
        }


        internal static void UpdateContainerList()
        {
            containerBs.ResetBindings(false);
        }


        private void ContainerListGUI_ShownOrVisible(object? sender, EventArgs e)
        {
            var containerListForm = _instanceForm;
            if (containerList_listbox != null)
            {
                if (containerListForm.Visible)
                {
                    containerListForm.Invalidate();
                }
            }
        }

        private void containerList_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender.GetType() == typeof(ListBox))
                {
                    var listbox = (ListBox)sender;
                    if (listbox.SelectedItem != null)
                    {
                        classes.Container.SetSelectedContainer((Container)listbox.SelectedItem);
                        classes.Container.SelectedContainer.UpdateContainerStatus(classes.Container.SelectedRelease);
                        Debug.WriteLine(classes.Container.SelectedContainer.ToString());
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
                    switch (dibf[1].ToLower())
                    {
                        case "nd":
                            dibf[1] = 100.ToString();
                            break;
                        case "tef":
                            dibf[1] = 101.ToString();
                            break;
                        case "byd":
                            dibf[1] = 102.ToString();
                            break;
                        case "ima":
                            dibf[1] = 103.ToString();
                            break;
                        case "bio":
                            dibf[1] = 104.ToString();
                            break;
                        default:
                            break;

                    }
                    dibf[0].ToUpper();
                    releasesAndItems.TryAdd(int.Parse(dibf[1]), itemList);
                    var newCtn = new Container(dibf[0], dibf[2], releasesAndItems);

                    classes.Container.AddContainer(newCtn);
                    newCtn?.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, newCtn.ContainerId));
                }
            }
        }

        private async void delContainer_Clicked(object sender, EventArgs e)
        {
            Control[] FindBox = _instanceForm.Controls.Find("containerList_listbox", true);
            ListBox listBox = (ListBox)FindBox[0];

            if (classes.Container.SelectedContainer != null)
            {
                var currentSelectedContainer = classes.Container.SelectedContainer;
                DialogResult dr = MessageBox.Show($"Are you sure you want to delete {currentSelectedContainer.ToString()}?\nThis action is irreversible.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    await classes.Container.DeleteContainerFileAsync(currentSelectedContainer);
                    classes.Container.RemoveContainer(currentSelectedContainer);
                }
            }
        }

        private void viewCfg_btn_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer != null && classes.Container.SelectedRelease != 0)
            {
                ViewContainerGUI viewContainerGUI = new ViewContainerGUI(classes.Container.SelectedContainer);
                viewContainerGUI.ShowDialog(this);
            }
        }

        private void generateReport_Btn_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer.ValidateContainerAndRelease(classes.Container.SelectedRelease) == 0)
            {
                ContainerReport ctnReport = new ContainerReport(classes.Container.SelectedContainer, classes.Container.SelectedRelease);
                ctnReport.GenerateReport();
            }
            else
            {
                MessageBox.Show("Could not validate container and release.\nPlease try reselecting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getEmail_btn_Click(object sender, EventArgs e)
        {
            ContainerReport ctnReport = new ContainerReport(classes.Container.SelectedContainer, classes.Container.SelectedRelease);
            ctnReport.GenerateEmailMessage();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer != null)
            {
                var currentSelectedContainer = classes.Container.SelectedContainer;
                DialogResult dr = MessageBox.Show($"Are you sure you want to delete {currentSelectedContainer.ToString()}?\nThis action is irreversible.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    await classes.Container.DeleteContainerFileAsync(currentSelectedContainer);
                    classes.Container.RemoveContainer(currentSelectedContainer);
                }
            }
        }

        private void ToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            toolStripMenuItem.ForeColor = Color.WhiteSmoke;
        }
        private void ToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            toolStripMenuItem.ForeColor = Color.Black;
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer != null && classes.Container.SelectedRelease != 0)
            {
                classes.Container.SelectedContainer.ReleasesAndItems[classes.Container.SelectedRelease].Clear();
                classes.Container.SelectedContainer.UpdateContainerStatus(classes.Container.SelectedRelease);
                classes.Container.SelectedContainer?.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, classes.Container.SelectedContainer.ContainerId));
            }
        }

        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer.ValidateContainerAndRelease(classes.Container.SelectedRelease) == 0)
            {
                classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease].Clear();
                classes.Container.SelectedContainer.UpdateContainerStatus(classes.Container.SelectedRelease);
                classes.Container.SelectedContainer?.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, classes.Container.SelectedContainer.ContainerId));
            }
        }

        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classes.Container.SelectedContainer.ValidateContainerAndRelease(classes.Container.SelectedRelease) == 0)
            {
                List<int> lines;
                List<string> values;
                classes.Container Ctr = classes.Container.SelectedContainer;
                lines = Ctr.AttachedConfigurations[classes.Container.SelectedRelease].Keys.ToList();
                values = Ctr.AttachedConfigurations[classes.Container.SelectedRelease].Values.ToList();
                if (lines.Count > 0 && values.Count > 0)
                {
                    try
                    {
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

        private void getArraySizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease].Count);
        }

        private void radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdBtn = sender as RadioButton;
            containerBs.DataSource = classes.Container.GetContainersByStatus(rdBtn?.Text.Trim());
            containerBs.ResetBindings(true);
        }

        private void refreshFilteredList()
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            radioButtons = filter_panel.Controls.OfType<RadioButton>().ToList();
            foreach (RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked == true)
                {
                    containerBs.DataSource = classes.Container.GetContainersByStatus(radioButton?.Text.Trim());
                    containerBs.ResetBindings(true);
                    break;
                }
            }
        }
    }
}


