using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.classes;

namespace ArcherTools_0._0._1.forms
{
    public partial class ContainerListGUI : Form
    {

        public static Form _instanceForm;
        public static ContainerListGUI _instance;
        private static BindingSource containerBs = new BindingSource();
        
        public ContainerListGUI(string title)
        {
            InitializeComponent();
            this.Load += ContainerListGUI_Load;
            this.title_Label.Text = title;
            this.VisibleChanged += ContainerListGUI_ShownOrVisible;
            this.Shown += ContainerListGUI_ShownOrVisible;
            this.containerList_listbox.MouseDoubleClick += containerList_Click;
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

            containerBs.DataSource = classes.Container.AllContainers;
            this.containerList_listbox.DataSource = containerBs;


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
            this.Invalidate();
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
                    foreach (var release in selectedContainer.ReleasesAndItems)
                    {
                        _instance.release_cbbox.Items.Add(release.Key);
                        _instance.status_lbl.Text = _instance.status_lbl.Text.Split(':')[0] + $": {classes.Container.SelectedContainer.ContainerStatus}";
                    }
                }
                else
                {
                    selectedLabel.Text = selectedLabel.Text.Split(':')[0] + $": Nothing.";
                    _instance.status_lbl.Text = "Status: ";
                }
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
                    if (dibf[1].Contains("ND"))
                    {
                        dibf[1] = "100";
                    }
                    releasesAndItems.TryAdd(int.Parse(dibf[1]), itemList);
                    var newCtn = new Container(dibf[0], releasesAndItems);
                    
                    classes.Container.AddContainer(newCtn);
                    newCtn.SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, newCtn.ContainerId));
                    _instanceForm.Invalidate();
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
                    _instanceForm.Invalidate();
                }
            }
        }


    }
}
